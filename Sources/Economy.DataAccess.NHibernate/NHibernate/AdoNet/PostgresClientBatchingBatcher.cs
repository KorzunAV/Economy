using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text;
using NHibernate;
using NHibernate.AdoNet;
using Npgsql;

namespace Economy.DataAccess.NHibernate.NHibernate.AdoNet
{
    /// <summary>
    /// Summary description for PostgresClientBatchingBatcher.
    /// </summary>
    public class PostgresClientBatchingBatcher : AbstractBatcher
    {

        private int batchSize;
        private int countOfCommands = 0;
        private int totalExpectedRowsAffected;
        private StringBuilder sbBatchCommand;
        private int m_ParameterCounter;

        private IDbCommand currentBatch;

        public PostgresClientBatchingBatcher(ConnectionManager connectionManager, IInterceptor interceptor)
            : base(connectionManager, interceptor)
        {
            batchSize = Factory.Settings.AdoBatchSize;
        }


        private string NextParam()
        {
            return ":p" + m_ParameterCounter++;
        }

        public override void AddToBatch(IExpectation expectation)
        {
            if (expectation.CanBeBatched && !(CurrentCommand.CommandText.StartsWith("INSERT INTO") && CurrentCommand.CommandText.Contains("VALUES")))
            {
                //NonBatching behavior
                IDbCommand cmd = CurrentCommand;
                LogCommand(CurrentCommand);
                int rowCount = ExecuteNonQuery(cmd);
                expectation.VerifyOutcomeNonBatched(rowCount, cmd);
                currentBatch = null;
                return;
            }

            totalExpectedRowsAffected += expectation.ExpectedRowCount;


            int len = CurrentCommand.CommandText.Length;
            int idx = CurrentCommand.CommandText.IndexOf("VALUES");
            int endidx = idx + "VALUES".Length + 2;

            if (currentBatch == null)
            {
                // begin new batch. 
                currentBatch = new NpgsqlCommand();
                sbBatchCommand = new StringBuilder();
                m_ParameterCounter = 0;

                string preCommand = CurrentCommand.CommandText.Substring(0, endidx);
                sbBatchCommand.Append(preCommand);
            }
            else
            {
                //only append Values
                sbBatchCommand.Append(", (");
            }

            //append values from CurrentCommand to sbBatchCommand
            string values = CurrentCommand.CommandText.Substring(endidx, len - endidx - 1);
            //get all values
            string[] split = values.Split(',');

            ArrayList paramName = new ArrayList(split.Length);
            for (int i = 0; i < split.Length; i++)
            {
                if (i != 0)
                    sbBatchCommand.Append(", ");

                string param = null;
                if (split[i].StartsWith(":"))   //first named parameter
                {
                    param = NextParam();
                    paramName.Add(param);
                }
                else if (split[i].StartsWith(" :")) //other named parameter
                {
                    param = NextParam();
                    paramName.Add(param);
                }
                else if (split[i].StartsWith(" "))  //other fix parameter
                {
                    param = split[i].Substring(1, split[i].Length - 1);
                }
                else
                {
                    param = split[i];   //first fix parameter
                }

                sbBatchCommand.Append(param);
            }
            sbBatchCommand.Append(")");

            //rename & copy parameters from CurrentCommand to currentBatch
            int iParam = 0;
            foreach (NpgsqlParameter param in CurrentCommand.Parameters)
            {
                param.ParameterName = (string)paramName[iParam++];

                NpgsqlParameter newParam = /*Clone()*/new NpgsqlParameter(param.ParameterName, param.NpgsqlDbType, param.Size, param.SourceColumn, param.Direction, param.IsNullable, param.Precision, param.Scale, param.SourceVersion, param.Value);
                currentBatch.Parameters.Add(newParam);
            }

            countOfCommands++;
            //check for flush
            if (countOfCommands >= batchSize)
            {
                DoExecuteBatch(currentBatch);
            }
        }

        protected override void DoExecuteBatch(IDbCommand ps)
        {
            if (currentBatch != null)
            {
                //Batch command now needs its terminator
                sbBatchCommand.Append(";");

                countOfCommands = 0;
                
                CheckReaders();

                //set prepared batchCommandText
                string commandText = sbBatchCommand.ToString();
                currentBatch.CommandText = commandText;

                LogCommand(currentBatch);

                Prepare(currentBatch);

                int rowsAffected = 0;
                try
                {
                    rowsAffected = currentBatch.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();
                    throw;
                }

                Expectations.VerifyOutcomeBatched(totalExpectedRowsAffected, rowsAffected);

                totalExpectedRowsAffected = 0;
                currentBatch = null;
                sbBatchCommand = null;
                m_ParameterCounter = 0;
            }
        }

        protected override int CountOfStatementsInCurrentBatch
        {
            get { return countOfCommands; }
        }

        public override int BatchSize
        {
            get { return batchSize; }
            set { batchSize = value; }
        }
    }
}
