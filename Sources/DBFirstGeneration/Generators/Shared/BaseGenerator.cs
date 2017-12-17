using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DBFirstGeneration.DbManagers;

namespace DBFirstGeneration.Generators.Shared
{
    public class BaseGenerator
    {
        protected const string PartPathToroot = "../../../";
        const string PathToDto = "Economy.Dtos";
        const string PartToTemplate = "Generators/Shared/";

        protected static void GenerateDto(string tableName, DbInfoEntity[] tblInfo, DbInfoEntity[] oneToMany)
        {
            var sb = new StringBuilder();

            foreach (var info in tblInfo)
            {
                SetDtoProperty(sb, info);
            }

            foreach (var listProp in oneToMany)
            {
                SetDtoOneToManyProperty(sb, listProp, tableName);
            }

            var template = File.ReadAllText($"{PartToTemplate}Dto.template");
            var entity = string.Format(template, tableName, sb);

            File.WriteAllText($"{PartPathToroot}{PathToDto}/{tableName}Dto.gen.cs", entity);
        }

        protected static void SetDtoProperty(StringBuilder sb, DbInfoEntity info)
        {
            if (info.IsPrimaryKey == true || info.ColumnName.Equals("Version"))
                return;

            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        ///{info.Description}");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine($"        public virtual {info.DataType} {info.ColumnName} {{ get; set; }}{Environment.NewLine}");
            if (info.IsForeignKey == true)
                sb.AppendLine($"        public virtual {info.RefTableName}Dto {info.ColumnObjName} {{ get; set; }}{Environment.NewLine}");
        }

        protected static void SetDtoOneToManyProperty(StringBuilder sb, DbInfoEntity info, string tableName)
        {
            var postfix = GetPostfix(info.ColumnName, tableName);
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        ///{info.Description}");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine($"        public virtual List<{info.TableName}Dto> {info.TableName}{postfix}s {{ get; set; }}{Environment.NewLine}");
        }

        protected static void SetEntityOneToManyProperty(StringBuilder sb, DbInfoEntity info, string tableName, string idName)
        {
            var postfix = GetPostfix(info.ColumnName, tableName);
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        ///{info.Description}");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine($"        [Association(ThisKey=\"{info.ColumnName}\", OtherKey=\"{idName}\", CanBeNull=true)]");
            sb.AppendLine($"        public virtual List<{info.TableName}Entity> {info.TableName}{postfix}s {{ get; set; }}{Environment.NewLine}");
        }

        protected static string GetPostfix(string colId, string tableName)
        {
            if (colId.EndsWith("Id"))
            {
                colId = colId.Remove(colId.Length - 2);
                if (!string.IsNullOrEmpty(tableName) && colId.Length >= tableName.Length)
                    colId = colId.Remove(0, tableName.Length);
                return colId;
            }
            return colId;
        }

        protected static string GetObjName(string colId)
        {
            return GetPostfix(colId, string.Empty);
        }

        protected static string ConvertDbTypeToSystemType(DbInfoEntity info)
        {
            var isNullable = info.IsNullable.StartsWith("y", StringComparison.OrdinalIgnoreCase);
            switch (info.DataType)
            {
                case "uuid": return "Guid";
                case "timestamp with time zone": return isNullable ? "DateTime?" : "DateTime";
                case "timestamp without time zone": return isNullable ? "DateTime?" : "DateTime";
                case "character varying": return "string";
                case "integer": return isNullable ? "int?" : "int";
                case "numeric": return isNullable ? "decimal?" : "decimal";
                case "money": return isNullable ? "decimal?" : "decimal";
                case "double precision": return isNullable ? "double?" : "double";
            }
            return info.DataType;
        }

        protected static List<DbInfoEntity> GetDbInfos()
        {
            using (var db = new PostgreDbBase())
            {
                var list = db.SetCommand(@"SELECT DISTINCT c.*,pgd.description, 
                                                  (SELECT true 
                                                   FROM information_schema.key_column_usage kcu
                                                   LEFT JOIN information_schema.table_constraints tc ON tc.constraint_name = kcu.constraint_name
                                                   WHERE kcu.column_name = c.column_name and kcu.table_name  = c.table_name AND tc.constraint_type = 'PRIMARY KEY'
                                                   LIMIT 1) AS IsPrimaryKey,
                                                   (SELECT true 
                                                    FROM information_schema.key_column_usage kcu
                                                    LEFT JOIN information_schema.table_constraints tc ON tc.constraint_name = kcu.constraint_name
                                                    WHERE kcu.column_name = c.column_name and kcu.table_name  = c.table_name AND tc.constraint_type = 'FOREIGN KEY'
                                                    LIMIT 1) AS IsForeignKey,
                                                   (SELECT true 
                                                    FROM information_schema.key_column_usage kcu
                                                    LEFT JOIN information_schema.table_constraints tc ON tc.constraint_name = kcu.constraint_name
                                                    WHERE kcu.column_name = c.column_name and kcu.table_name  = c.table_name AND tc.constraint_type = 'UNIQUE'
                                                    LIMIT 1) AS IsUniqueKey
                                           FROM pg_catalog.pg_statio_all_tables as st
                                           JOIN information_schema.columns c on c.table_schema=st.schemaname and c.table_name=st.relname
                                           LEFT JOIN pg_catalog.pg_description pgd on pgd.objoid=st.relid and pgd.objsubid=c.ordinal_position
                                           WHERE c.table_schema = 'public'
                                           ORDER BY table_name, ordinal_position;")
                    .ExecuteList<DbInfoEntity>();

                var allTables = list.Select(i => i.TableName).Distinct().ToArray();
                foreach (var item in list)
                {
                    item.DataType = ConvertDbTypeToSystemType(item);
                    if (item.IsForeignKey == true)
                    {
                        item.ColumnObjName = GetObjName(item.ColumnName);
                        item.RefTableName = allTables.FirstOrDefault(i => item.ColumnName.StartsWith(i));
                        if (string.IsNullOrEmpty(item.RefTableName))
                            item.RefTableName = allTables.FirstOrDefault(i => item.ColumnName.Contains(i));
                    }
                }
                return list;
            }
        }

        protected static void GenerateDaoMap(List<DbInfoEntity> info, string projName)
        {
            var sb = new StringBuilder();

            foreach (var table in info.Select(i => i.TableName).Distinct())
            {
                sb.AppendLine($"            Bind<{table}Dao>().ToSelf().InSingletonScope();");
            }

            var template = File.ReadAllText($"{PartToTemplate}DataAccessModule.Dao.template");
            var file = string.Format(template, projName, sb);

            File.WriteAllText($"{PartPathToroot}{projName}/IoC/DataAccessModule.Dao.gen.cs", file);
        }

        protected static void GenerateEntityDtoMapping(List<DbInfoEntity> info, string projName)
        {
            var sb = new StringBuilder();

            foreach (var table in info.Select(i => i.TableName).Distinct())
            {
                sb.AppendLine($"            cfg.CreateMap<{table}Dto, {table}Entity>();");
                sb.AppendLine($"            cfg.CreateMap<{table}Entity, {table}Dto>();");
            }

            var template = File.ReadAllText($"{PartToTemplate}EntityDtoMappings.template");
            var file = string.Format(template, projName, sb);

            File.WriteAllText($"{PartPathToroot}{projName}//AutomapperMappings//EntityDtoMappings.gen.cs", file);
        }
    }
}