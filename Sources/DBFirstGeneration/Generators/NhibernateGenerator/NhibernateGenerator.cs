using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DBFirstGeneration.Generators.Shared;

namespace DBFirstGeneration.Generators.NhibernateGenerator
{
    public class NhibernateGenerator : BaseGenerator
    {
        const string PathToNHibernateDataAccess = "Economy.DataAccess.NHibernate";
        const string PartToTemplate = "Generators/NhibernateGenerator/";

        static void Main(string[] args)
        {
            var list = GetDbInfos();
            GenerateEntity(list);

            var grp = list.GroupBy(i => i.TableName).ToArray();
            foreach (var itm in grp)
            {
                var oneToMany = list.Where(i => i.ColumnName.StartsWith(itm.Key) && i.ColumnName.EndsWith("Id")).ToArray();
                var tblInfo = itm.ToArray();

                GenerateMapping(itm.Key, tblInfo);
                GenerateDto(itm.Key, tblInfo, oneToMany);
                GenerateDao(itm.Key, tblInfo);
            }

            GenerateEntityDtoMapping(list, PathToNHibernateDataAccess);
            GenerateDaoMap(list, PathToNHibernateDataAccess);
        }

        private static void GenerateEntity(List<DbInfoEntity> list)
        {
            var grp = list.GroupBy(i => i.TableName).ToArray();

            foreach (var itm in grp)
            {
                var sb = new StringBuilder();

                foreach (var info in itm)
                {
                    SetEntityProperty(sb, info);
                }

                var oneToMany = list.Where(i => i.ColumnName.StartsWith(itm.Key) && i.ColumnName.EndsWith("Id")).ToArray();
                foreach (var listProp in oneToMany)
                {
                    SetEntityOneToManyProperty(sb, listProp, itm.Key);
                }

                //sb.AppendLine($@"        public override bool Equals(object obj){Environment.NewLine}        {{{Environment.NewLine}            if (obj is {itm.Key}Entity){Environment.NewLine}            {{{Environment.NewLine}                var typed = ({itm.Key}Entity)obj;");
                //foreach (var pk in itm.Where(i => i.IsPrimaryKey == true))
                //{
                //    if (pk.IsForeignKey == true)
                //        sb.AppendLine($@"                if (typed.{pk.ColumnName} != {pk.ColumnName} && (typed.{pk.ColumnObjName} == null || {pk.ColumnObjName} == null || typed.{pk.ColumnObjName} != {pk.ColumnObjName})){Environment.NewLine}                    return false;");
                //    else
                //        sb.AppendLine($@"                if (typed.{pk.ColumnName} != {pk.ColumnName}){Environment.NewLine}                    return false;");

                //}
                //sb.Append($@"                return true;{Environment.NewLine}            }}{Environment.NewLine}            return false;{Environment.NewLine}        }}");

                //sb.AppendLine($@"        int _hashCode;");
                //sb.AppendLine($@"        public override int GetHashCode(){Environment.NewLine}        {{{Environment.NewLine}                if(_hashCode == null){Environment.NewLine}                {{");
                //foreach (var pk in itm.Where(i => i.IsPrimaryKey == true))
                //{
                //    if (pk.IsForeignKey == true)
                //        sb.AppendLine($@"if(typed.{pk.ColumnName} != {pk.ColumnName} && (typed.{pk.ColumnObjName} == null || {pk.ColumnObjName} == null || typed.{pk.ColumnObjName} != {pk.ColumnObjName})){Environment.NewLine}                    return false;");
                //    else
                //        sb.AppendLine($@"                if (typed.{pk.ColumnName} != {pk.ColumnName}){Environment.NewLine}                    return false;");
                //}
                //sb.AppendLine(@"            }}{Environment.NewLine}            return _hashCode;{Environment.NewLine}        }}");



                var template = File.ReadAllText($"{PartToTemplate}Entity.template");

                var entity = string.Format(template, itm.Key, sb);

                var outPath = $"{PartPathToroot}{PathToNHibernateDataAccess}//Entities";
                if (!Directory.Exists(outPath))
                    Directory.CreateDirectory(outPath);

                File.WriteAllText($"{outPath}//{itm.Key}Entity.cs", entity);
            }
        }

        private static void SetAttribute(StringBuilder sb, DbInfoEntity info)
        {
            var isNullable = info.IsNullable.StartsWith("y", StringComparison.OrdinalIgnoreCase);
            if (!isNullable)
            {
                sb.AppendLine($"        [NotNull]");
                if (info.DataType.Equals("string"))
                    sb.AppendLine($"        [NotEmpty]");
            }

            if (info.CharacterMaximumLength > 0)
                sb.AppendLine($"        [Length({info.CharacterMaximumLength})] ");
        }

        private static void SetEntityProperty(StringBuilder sb, DbInfoEntity info)
        {
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        ///{info.Description}");
            sb.AppendLine($"        /// </summary>");
            SetAttribute(sb, info);
            sb.AppendLine($"        public virtual {info.DataType} {info.ColumnName} {{ get; set; }}{Environment.NewLine}");
            if (info.IsForeignKey == true)
                sb.AppendLine($"        public virtual {info.RefTableName}Entity {info.ColumnObjName} {{ get; set; }}{Environment.NewLine}");
        }

        private static void SetEntityOneToManyProperty(StringBuilder sb, DbInfoEntity info, string tableName)
        {
            var postfix = GetPostfix(info.ColumnName, tableName);
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        ///{info.Description}");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine($"        public virtual List<{info.TableName}Entity> {info.TableName}{postfix}s {{ get; set; }}{Environment.NewLine}");
        }

        private static void GenerateMapping(string tableName, DbInfoEntity[] tblInfo)
        {
            var sb = new StringBuilder();

            SetIsPrimaryKey(tblInfo, sb);

            foreach (var info in tblInfo.Where(i => i.IsPrimaryKey != true))
            {
                SetMappings(sb, info);
            }

            var template = File.ReadAllText($"{PartToTemplate}NHMappings.template");

            var entity = string.Format(template, tableName, sb);

            var outPath = $"{PartPathToroot}{PathToNHibernateDataAccess}//NHMappings";
            if (!Directory.Exists(outPath))
                Directory.CreateDirectory(outPath);

            File.WriteAllText($"{outPath}/{tableName}Mapping.cs", entity);
        }

        private static void SetIsPrimaryKey(DbInfoEntity[] tblInfo, StringBuilder sb)
        {
            var pks = tblInfo.Where(i => i.IsPrimaryKey == true).ToArray();
            if (pks.Length > 1)
            {
                sb.Append($"            CompositeId()");
                foreach (var pk in pks)
                {
                    if (pk.IsForeignKey == true)
                        sb.Append($"{Environment.NewLine}                .KeyReference(x => x.{pk.ColumnObjName}, \"{pk.ColumnName}\")");
                    else
                        sb.Append($"{Environment.NewLine}                .KeyReference(x => x.{pk.ColumnName})");
                }
                sb.AppendLine(";");
            }
            else
            {
                sb.AppendLine($"            Id(v => v.Id, \"\\\"Id\\\"\").GeneratedBy.{(pks[0].DataType.Equals("Guid") ? "Guid" : "Increment")}();");
            }
        }

        private static void SetMappings(StringBuilder sb, DbInfoEntity info)
        {
            if (info.ColumnName.Equals("Id"))
            {
                if (info.DataType.Equals("Guid"))
                    sb.AppendLine($"            Id(v => v.Id, \"\\\"Id\\\"\").GeneratedBy.Guid();");
                else
                    sb.AppendLine($"            Id(v => v.Id, \"\\\"Id\\\"\").GeneratedBy.Increment();");
            }
            else if (info.ColumnName.Equals("Version"))
            {
                sb.AppendLine($"            OptimisticLock.Version();");
                sb.AppendLine($"            Version(entity => entity.Version).Column(\"\\\"Version\\\"\");");
            }
            else
            {
                sb.AppendLine($"            Map(v => v.{info.ColumnName}, \"\\\"{info.ColumnName}\\\"\");");
                if (info.ColumnName.EndsWith("Id"))
                    sb.AppendLine($"            References(v => v.{info.ColumnObjName}, \"\\\"{info.ColumnName}\\\"\").Column(Column(v => v.{info.ColumnName})).ReadOnly().LazyLoad();");

            }
        }

        private static void GenerateDao(string tableName, DbInfoEntity[] tblInfo)
        {
            var filePath = $"{PartPathToroot}{PathToNHibernateDataAccess}//Daos//{tableName}Dao.cs";
            if (!File.Exists(filePath))
            {
                var template = File.ReadAllText($"{PartToTemplate}/Dao.template");
                var idType = "int";
                var sb = new StringBuilder();
                foreach (var info in tblInfo)
                {
                    if (info.ColumnName.Equals("Id"))
                        idType = info.DataType;
                    sb.AppendLine($"                Projections.Alias(Projections.Property(() => alias.{info.ColumnName}), \"{info.ColumnName}\"),");
                }
                var file = string.Format(template, tableName, idType, sb);
                File.WriteAllText(filePath, file);
            }
        }
    }
}
