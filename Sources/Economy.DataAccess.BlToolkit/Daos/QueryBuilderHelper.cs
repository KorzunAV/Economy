//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using BLToolkit.Data;
//using BLToolkit.DataAccess;
//using BLToolkit.Mapping;

//namespace Economy.DataAccess.BlToolkit.Daos
//{
//    public static class QueryBuilderHelper
//    {
//        public const string FromValue = "@pFrom";
//        public const string ToValue = "@pTo";
//        public const string Prefix = "@p";
//        public const string Version = "Version";
//        public const string PKey = "P_";
//        public const string MaxRow = "@pMaxRow";

//        //private static readonly HashSet<Type> AllowedTypes = new HashSet<Type>
//        //                                                         {
//        //                                                             typeof (int),
//        //                                                             typeof (long),
//        //                                                             typeof (string),
//        //                                                             typeof (decimal),
//        //                                                             typeof (double),
//        //                                                             typeof (Int32),
//        //                                                             typeof (Int64),
//        //                                                             typeof (String),
//        //                                                             typeof (Decimal),
//        //                                                             typeof (Double),
//        //                                                             typeof (DateTime)
//        //                                                         };

//        public static IEnumerable<MapFieldAttribute> GetColumns<T>(IEnumerable<string> except = null)
//        {
//            return GetColumns(typeof(T), except);
//        }

//        public static IEnumerable<MapFieldAttribute> GetColumns(Type type, IEnumerable<string> except = null)
//        {
//            foreach (var pr in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
//            {
//                var ca = pr.GetCustomAttribute<MapFieldAttribute>();
//                if (ca != null)
//                    yield return ca;
//            }
//        }

//        private static StringBuilder GetPatronimicColunmsLine(IEnumerable<Type> types)
//        {
//            var sb = new StringBuilder();
//            foreach (var type in types)
//            {
//                var colums = GetColumns(type);
//                foreach (var colum in colums)
//                {
//                    if (sb.Length != 0)
//                    {
//                        sb.Append(", ");
//                    }
//                    sb.AppendFormat("{0}.{1} {2}{0}{1}", type.Name, colum, PKey);
//                }
//            }
//            return sb;
//        }

//        public static string Select(Type type, IEnumerable<string> where = null, IEnumerable<string> except = null)
//        {
//            return $"SELECT {string.Join(",", GetColumns(type, except))} FROM {type.Name}{Where(@where)}";
//        }

//        public static StringBuilder Where(IEnumerable<string> where = null)
//        {
//            if (where == null)
//                where = new List<string>();

//            var sb = new StringBuilder();
//            foreach (var columnName in where)
//            {
//                sb.Append(sb.Length == 0 ? " WHERE " : " AND ");
//                sb.AppendFormat("{0} = {1}{0}", columnName, Prefix);
//            }
//            return sb;
//        }

//        public static string Join(Type table1, string column1, Type table2, string column2)
//        {
//            return string.Format(" JOIN {2} ON {0}.{1} = {2}.{3} ", table1.Name, column1, table2.Name, column2);
//        }

//        public static string LeftJoin(Type table1, string column1, Type table2, string column2)
//        {
//            return string.Format(" LEFT JOIN {2} ON {0}.{1} = {2}.{3} ", table1.Name, column1, table2.Name, column2);
//        }


//        public static string CompositeSelect(Type[] types, string joins, IEnumerable<string> where = null, IEnumerable<string> except = null)
//        {
//            return $"SELECT {GetPatronimicColunmsLine(types)} FROM {types[0].Name}{joins}{Where(@where)}";
//        }

//        public static string IsActual(Type type, string idName)
//        {
//            return $"SELECT Count(*) FROM {type.Name} WHERE {idName} = {Prefix}{idName} AND {Version} = {Prefix}{Version}";
//        }

//        public static string SelectCount(Type type)
//        {
//            return $"SELECT Count(*) FROM {type.Name}";
//        }

//        public static string Update(Type type, string idName)
//        {
//            var sb = new StringBuilder();
//            foreach (var columnName in GetColumns(type, new List<string> { idName }))
//            {
//                if (sb.Length != 0)
//                    sb.Append(", ");

//                sb.AppendFormat("{0} = {1}{0}", columnName, Prefix);
//            }
//            return $"UPDATE {type.Name} Set {sb} WHERE {idName} = {Prefix}{idName}";
//        }

//        public static string Insert<T>(IEnumerable<string> except = null)
//        {
//            return Insert(typeof(T), except);
//        }

//        public static string Insert(Type type, IEnumerable<string> except = null)
//        {
//            var sb = new StringBuilder();
//            var columns = GetColumns(type, except).ToArray();

//            PrimaryKeyAttribute primaryKey;

//            foreach (var pr in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
//            {
//                var ca = pr.GetCustomAttribute<MapFieldAttribute>();
//                if (ca != null)
//                {
//                    var idt = pr.GetCustomAttribute<PrimaryKeyAttribute>();
//                    if (idt != null)
//                    {
//                        primaryKey = idt;
//                    }

//                    if (sb.Length != 0)
//                        sb.Append(", ");

//                    sb.AppendFormat("{0}{1}", Prefix, ca.MapName.Trim('"'));
//                }
//            }


//            var tn = type.GetCustomAttribute<TableNameAttribute>();

//            return $"INSERT INTO  {tn.Owner}.{tn.Name}({string.Join(",", columns.Select(i => i.MapName))}) Values({sb}) returning {primaryKey.MapName};";
//        }

//        public static string Delete(Type type, IEnumerable<string> where = null)
//        {
//            return $"DELETE FROM {type.Name}{Where(@where)}";
//        }

//        public static string Max(Type type, string column)
//        {
//            return "SELECT MAX(" + column + ") FROM " + type.Name;

//        }

//        //public static T ReadEntity<T>(IDataReader reader) where T : new()
//        //{
//        //    var entity = new T();
//        //    var propertys = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
//        //    foreach (var propertyInfo in propertys)
//        //    {
//        //        if (AllowedTypes.Contains(propertyInfo.PropertyType))
//        //        {
//        //            var value = reader[propertyInfo.Name];

//        //            if (value is IComparable)
//        //            {
//        //                propertyInfo.SetValue(entity, value, null);
//        //            }
//        //        }
//        //    }
//        //    return entity;
//        //}

//        //public static T ReadCompositeEntity<T>(SqlDataReader reader) where T : class, new()
//        //{
//        //    var entity = new T();
//        //    var entityType = typeof(T);
//        //    var propertys = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
//        //    try
//        //    {
//        //        foreach (var propertyInfo in propertys)
//        //        {
//        //            if (AllowedTypes.Contains(propertyInfo.PropertyType))
//        //            {
//        //                propertyInfo.SetValue(entity, reader[PKey + entityType.Name + propertyInfo.Name], null);
//        //            }
//        //        }
//        //        return entity;
//        //    }
//        //    catch
//        //    {
//        //        return null;
//        //    }
//        //}



//        public static IEnumerable<IDbDataParameter> InitParametrs<T>(T entity, DbManager db, IEnumerable<string> except = null)
//        {
//            return InitParametrs(typeof(T), entity, db, except);
//        }


//        public static IEnumerable<IDbDataParameter> InitParametrs(Type type, object entity, DbManager db, IEnumerable<string> except = null)
//        {
//            foreach (var pr in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
//            {
//                var ca = pr.GetCustomAttribute<MapFieldAttribute>();
//                if (ca != null)
//                {
//                    var val = pr.GetValue(entity, null);
//                    yield return db.Parameter($"{Prefix}{ca.MapName.Trim('"')}", val);
//                }
//            }
//        }


//        public static StringBuilder Select(this StringBuilder sb)
//        {
//            return sb.Append("SELECT ");
//        }

//        public static StringBuilder SelectTop(this StringBuilder sb)
//        {
//            return sb.AppendFormat("SELECT TOP({0})", MaxRow);
//        }

//        //public static StringBuilder Columns(this StringBuilder sb, Type type, bool columnName = true, bool patronimic = false,
//        //                                    List<string> except = null)
//        //{
//        //    if (except == null)
//        //    {
//        //        except = new List<string>();
//        //    }
//        //    bool skip = true;
//        //    foreach (var propInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
//        //    {
//        //        if (AllowedTypes.Contains(propInfo.PropertyType))
//        //        {
//        //            if (!except.Contains(propInfo.Name))
//        //            {
//        //                if (skip)
//        //                {
//        //                    skip = false;
//        //                }
//        //                else
//        //                {
//        //                    sb.Append(", ");
//        //                }
//        //                if (columnName)
//        //                {
//        //                    sb.AppendFormat("{0}.{1}", type.Name, propInfo.Name);
//        //                }
//        //                if (patronimic)
//        //                {
//        //                    sb.AppendFormat(" {2}{0}{1}", type.Name, propInfo.Name, PKey);
//        //                }
//        //            }
//        //        }
//        //    }
//        //    return sb;
//        //}

//        public static StringBuilder From(this StringBuilder sb)
//        {
//            return sb.AppendFormat(" FROM ");
//        }

//        public static StringBuilder Where(this StringBuilder sb)
//        {
//            return sb.Append(" WHERE ");
//        }

//        public static StringBuilder LeftJoin(this StringBuilder sb, Type table1, string column1, Type table2, string column2)
//        {
//            return sb.AppendFormat(" LEFT JOIN {2} ON {0}.{1} = {2}.{3} ", table1.Name, column1, table2.Name, column2);
//        }

//        public static StringBuilder AliasLeftJoin(this StringBuilder sb, Type table1, string column1, Type table2, string column2)
//        {
//            return sb.AppendFormat(" LEFT JOIN {2} ON {4}{0}{1} = {2}.{3} ", table1.Name, column1, table2.Name, column2, PKey);
//        }
//    }
//}
