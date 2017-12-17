using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DBFirstGeneration.Generators.Shared;

namespace DBFirstGeneration.Generators.BlToolKitGenerator
{
    public class BlToolKitGenerator : BaseGenerator
    {
        const string PathToBlToolkitDataAccess = "Economy.DataAccess.BlToolkit";
        const string PathToLogic = "Economy.Logic";
        const string PartToTemplate = "Generators/BlToolKitGenerator/";
        const bool ReCreateAll = true;

        static void Main(string[] args)
        {
            var list = GetDbInfos();

            var grp = list.GroupBy(i => i.TableName).ToArray();
            foreach (var itm in grp)
            {
                var oneToMany = list.Where(i => i.ColumnName.StartsWith(itm.Key) && i.ColumnName.EndsWith("Id")).ToArray();
                var tblInfo = itm.ToArray();

                GenerateEntity(itm.Key, tblInfo, oneToMany);
                GenerateDto(itm.Key, tblInfo, oneToMany);
                GenerateDao(itm.Key);
                GenerateBlo(itm.Key);
                GenerateSaveCommand(itm.Key);
                GenerateGetByIdQuery(itm.Key);
                GenerateSaveListCommand(itm.Key);
            }

            GenerateEntityDtoMapping(list, PathToBlToolkitDataAccess);
            GenerateDaoMap(list, PathToBlToolkitDataAccess);
            GenerateEcomonyDb(list, PathToBlToolkitDataAccess);
        }

        private static void GenerateGetByIdQuery(string tableName)
        {
            var filePath = $"{PartPathToroot}{PathToLogic}/Queries/GetByIdQueries/Get{tableName}ByIdQuery.gen.cs";
            if (ReCreateAll || !File.Exists(filePath))
            {
                var template = File.ReadAllText($"{PartToTemplate}GetByIdQuery.template");
                var file = string.Format(template, tableName, Guid.NewGuid().ToString().ToUpper());
                File.WriteAllText(filePath, file);
            }
        }

        private static void GenerateEntity(string tableName, DbInfoEntity[] tblInfo, DbInfoEntity[] oneToMany)
        {
            var sb = new StringBuilder();

            foreach (var info in tblInfo)
            {
                SetEntityProperty(sb, info);
            }

            var idName = tblInfo.SingleOrDefault(i => i.IsPrimaryKey.HasValue && i.IsPrimaryKey.Value)?.ColumnName;

            foreach (var listProp in oneToMany)
            {
                SetEntityOneToManyProperty(sb, listProp, tableName, idName);
            }

            SetUpdateAssociations(sb, tableName, tblInfo, oneToMany);

            var template = File.ReadAllText($"{PartToTemplate}Entity.template");

            var entity = string.Format(template, tableName, sb);

            var outPath = $"{PartPathToroot}{PathToBlToolkitDataAccess}/Entities";
            if (!Directory.Exists(outPath))
                Directory.CreateDirectory(outPath);

            File.WriteAllText($"{outPath}/{tableName}Entity.gen.cs", entity);
        }

        private static void SetUpdateAssociations(StringBuilder sb, string tableName, DbInfoEntity[] tblInfo, DbInfoEntity[] oneToMany)
        {
            sb.AppendLine("        public override void UpdateAssociations()");
            sb.AppendLine("        {");

            var idName = tblInfo.SingleOrDefault(i => i.IsPrimaryKey == true)?.ColumnName;

            foreach (var info in oneToMany)
            {
                if (info.IsForeignKey == true)
                {
                    var postfix = GetPostfix(info.ColumnName, tableName);
                    sb.AppendLine($"            foreach (var item in {info.TableName}{postfix}s)");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                item.{info.ColumnName} = {idName};");
                    sb.AppendLine("            }");
                }
            }
            sb.AppendLine("        }");
        }

        private static void SetEntityProperty(StringBuilder sb, DbInfoEntity info)
        {
            if (info.IsPrimaryKey == true || info.ColumnName.Equals("Version"))
                return;

            var isNullable = info.IsNullable.StartsWith("y", StringComparison.OrdinalIgnoreCase);
            var atr = $@"{(isNullable ? ", Nullable" : ", Required")}{(info.IsUniqueKey == true ? ", Unique" : string.Empty)}";
            if (info.IsForeignKey == true)
            {
                var template = File.ReadAllText($"{PartToTemplate}/EntityRefProp.template");
                var buf = $"_{info.ColumnName.ToLower()}";
                var testBuf = $"{(isNullable ? $"(!{buf}.HasValue ||" : string.Empty)} {buf} == 0 {(isNullable ? ")" : string.Empty)}";
                sb.AppendFormat(template, info.DataType, buf, info.Description, info.ColumnName, atr, testBuf, info.ColumnObjName, info.RefTableName);
            }
            else
            {
                var template = File.ReadAllText($"{PartToTemplate}/EntityProp.template");
                sb.AppendFormat(template, info.Description, info.ColumnName, atr, info.DataType);
            }
        }

        private static void GenerateDao(string tableName)
        {
            var filePath = $"{PartPathToroot}{PathToBlToolkitDataAccess}/Daos/{tableName}Dao.gen.cs";
            if (ReCreateAll || !File.Exists(filePath))
            {
                var template = File.ReadAllText($"{PartToTemplate}Dao.template");
                var file = string.Format(template, tableName);
                File.WriteAllText(filePath, file);
            }
        }

        private static void GenerateBlo(string tableName)
        {
            var filePath = $"{PartPathToroot}{PathToLogic}/Blos/{tableName}Blo.gen.cs";
            if (ReCreateAll || !File.Exists(filePath))
            {
                var template = File.ReadAllText($"{PartToTemplate}Blo.template");
                var file = string.Format(template, tableName, tableName.ToLower());
                File.WriteAllText(filePath, file);
            }
        }

        private static void GenerateSaveCommand(string tableName)
        {
            var filePath = $"{PartPathToroot}{PathToLogic}/Commands/SaveCommands/{tableName}SaveCommand.gen.cs";
            if (ReCreateAll || !File.Exists(filePath))
            {
                var template = File.ReadAllText($"{PartToTemplate}SaveCommand.template");
                var file = string.Format(template, tableName, Guid.NewGuid().ToString().ToUpper());
                File.WriteAllText(filePath, file);
            }
        }

        private static void GenerateSaveListCommand(string tableName)
        {
            var filePath = $"{PartPathToroot}{PathToLogic}/Commands/SaveListCommands/{tableName}SaveListCommand.gen.cs";
            if (ReCreateAll || !File.Exists(filePath))
            {
                var template = File.ReadAllText($"{PartToTemplate}SaveListCommand.template");
                var file = string.Format(template, tableName, Guid.NewGuid().ToString().ToUpper());
                File.WriteAllText(filePath, file);
            }
        }

        protected static void GenerateEcomonyDb(List<DbInfoEntity> info, string projName)
        {
            var sb = new StringBuilder();

            foreach (var table in info.Select(i => i.TableName).Distinct())
            {
                sb.AppendLine($"        internal Table<{table}Entity> {table}Table => GetTable<{table}Entity>();");
            }

            var template = File.ReadAllText($"{PartToTemplate}EconomyDb.template");
            var file = string.Format(template, sb);

            File.WriteAllText($"{PartPathToroot}{projName}/DbManagers/EconomyDb.gen.cs", file);
        }
    }
}