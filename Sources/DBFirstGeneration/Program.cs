using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DBFirstGeneration.DbManagers;

namespace DBFirstGeneration
{
    public class Program
    {
        const string PathToDto = "..\\..\\..\\Economy.Dtos";
        const string PathToDataAccess = "..\\..\\..\\Economy.DataAccess.NHibernate";

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

            GenerateEntityDtoMapping(list);
        }

        private static List<DbInfoEntity> GetDbInfos()
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
                                             LIMIT 1) AS IsForeignKey
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

        private static string ConvertDbTypeToSystemType(DbInfoEntity info)
        {
            var isNullable = info.IsNullable.StartsWith("y", StringComparison.OrdinalIgnoreCase);
            switch (info.DataType)
            {
                case "uuid": return "Guid";
                case "timestamp with time zone": return isNullable ? "DateTime?" : "DateTime";
                case "timestamp without time zone": return isNullable ? "DateTime?" : "DateTime";
                case "character varying": return "string";
                case "integer": return isNullable ? "int?" : "int";
                case "money": return isNullable ? "decimal?" : "decimal";
                case "double precision": return isNullable ? "double?" : "double";
            }
            return info.DataType;
        }

        private static string GetObjName(string colId)
        {
            return GetPostfix(colId, string.Empty);
        }

        private static string GetPostfix(string colId, string tableName)
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

                sb.AppendLine($@"        public override bool Equals(object obj){Environment.NewLine}        {{{Environment.NewLine}            if (obj is {itm.Key}Entity){Environment.NewLine}            {{{Environment.NewLine}                var typed = ({itm.Key}Entity)obj;");
                foreach (var pk in itm.Where(i => i.IsPrimaryKey == true))
                {
                    if (pk.IsForeignKey == true)
                        sb.AppendLine($@"                if (typed.{pk.ColumnName} != {pk.ColumnName} && (typed.{pk.ColumnObjName} == null || {pk.ColumnObjName} == null || typed.{pk.ColumnObjName} != {pk.ColumnObjName})){Environment.NewLine}                    return false;");
                    else
                        sb.AppendLine($@"                if (typed.{pk.ColumnName} != {pk.ColumnName}){Environment.NewLine}                    return false;");

                }
                sb.Append($@"                return true;{Environment.NewLine}            }}{Environment.NewLine}            return false;{Environment.NewLine}        }}");


                //        public override int GetHashCode()
                //{
                //    return base.GetHashCode();
                //}


                var template = File.ReadAllText("Entity.template");

                var entity = string.Format(template, itm.Key, sb);

                var outPath = $"{PathToDataAccess}//Entities";
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

            var template = File.ReadAllText("NHMappings.template");

            var entity = string.Format(template, tableName, sb);

            var outPath = $"{PathToDataAccess}//NHMappings";
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
                sb.AppendLine($"            Id(v => v.Id).GeneratedBy.{(pks[0].DataType.Equals("Guid") ? "Guid" : "Increment")}();");
            }
        }

        private static void SetMappings(StringBuilder sb, DbInfoEntity info)
        {
            if (info.ColumnName.Equals("Id"))
            {
                if (info.DataType.Equals("Guid"))
                    sb.AppendLine($"            Id(v => v.Id).GeneratedBy.Guid();");
                else
                    sb.AppendLine($"            Id(v => v.Id).GeneratedBy.Increment();");
            }
            else if (info.ColumnName.Equals("Version"))
            {
                sb.AppendLine($"            OptimisticLock.Version();");
                sb.AppendLine($"            Version(entity => entity.Version);");
            }
            else
            {
                sb.AppendLine($"            Map(v => v.{info.ColumnName});");
                if (info.ColumnName.EndsWith("Id"))
                    sb.AppendLine($"            References(v => v.{info.ColumnObjName}).Column(Column(v => v.{info.ColumnName})).ReadOnly().LazyLoad();");

            }
        }


        private static void GenerateDto(string tableName, DbInfoEntity[] tblInfo, DbInfoEntity[] oneToMany)
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

            var template = File.ReadAllText("Dto.template");
            var entity = string.Format(template, tableName, sb);

            File.WriteAllText($"{PathToDto}//{tableName}Dto.cs", entity);
        }

        private static void SetDtoProperty(StringBuilder sb, DbInfoEntity info)
        {
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        ///{info.Description}");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine($"        public virtual {info.DataType} {info.ColumnName} {{ get; set; }}{Environment.NewLine}");
            if (info.IsForeignKey == true)
                sb.AppendLine($"        public virtual {info.RefTableName}Dto {info.ColumnObjName} {{ get; set; }}{Environment.NewLine}");
        }

        private static void SetDtoOneToManyProperty(StringBuilder sb, DbInfoEntity info, string tableName)
        {
            var postfix = GetPostfix(info.ColumnName, tableName);
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        ///{info.Description}");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine($"        public virtual List<{info.TableName}Dto> {info.TableName}{postfix}s {{ get; set; }}{Environment.NewLine}");
        }

        private static void GenerateEntityDtoMapping(List<DbInfoEntity> info)
        {
            var sb = new StringBuilder();

            foreach (var table in info.Select(i => i.TableName).Distinct())
            {
                sb.AppendLine($"            cfg.CreateMap<{table}Dto, {table}Entity>();");
                sb.AppendLine($"            cfg.CreateMap<{table}Entity, {table}Dto>();");
            }

            var template = File.ReadAllText("EntityDtoMappings.template");
            var file = string.Format(template, sb);

            File.WriteAllText($"{PathToDataAccess}//AutomapperMappings//EntityDtoMappings.cs", file);
        }

        private static void GenerateDao(string tableName, DbInfoEntity[] tblInfo)
        {
            var filePath = $"{PathToDataAccess}//Daos//{tableName}Dao.cs";
            if (!File.Exists(filePath))
            {
                var template = File.ReadAllText("Dao.template");
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