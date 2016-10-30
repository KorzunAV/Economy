using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using AutoMapper;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public abstract class BaseDao
    {
        public const string Prefix = "@p";

        public static int Insert(Type type, DbManager db, object entity)
        {
            MapFieldAttribute primaryKey = null;
            PropertyInfo primaryKeyProperty = null;
            var columns = new List<string>();
            var dbDataParam = new List<IDbDataParameter>();

            foreach (var pr in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var assotiation = pr.GetCustomAttribute<AssociationAttribute>();
                if (assotiation != null)
                {
                    var val = pr.GetValue(entity);
                    if (val != null)
                        Insert(pr.PropertyType, db, val);
                }
            }
            foreach (var pr in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var ca = pr.GetCustomAttribute<MapFieldAttribute>();
                if (ca != null)
                {
                    var val = pr.GetValue(entity);
                    var param = db.Parameter($"{Prefix}{ca.MapName.Trim('"')}", val);

                    var idt = pr.GetCustomAttribute<PrimaryKeyAttribute>();
                    if (idt != null)
                    {
                        primaryKeyProperty = pr;
                        primaryKey = ca;
                        var pkVal = (int)val;
                        if (pkVal > 0)
                        {
                            columns.Add(ca.MapName);
                            dbDataParam.Add(param);
                        }
                    }
                    else
                    {
                        columns.Add(ca.MapName);
                        dbDataParam.Add(param);
                    }
                }
            }


            var tn = type.GetCustomAttribute<TableNameAttribute>();
            var returning = (primaryKey != null ? $"returning {primaryKey.MapName}" : string.Empty);
            var comm = $"INSERT INTO  {tn.Owner}.{tn.Name}({string.Join(",", columns)}) Values({string.Join(",", columns.Select(i => $"{Prefix}{i.Trim('"')}"))}) {returning};";

            var result = db.SetCommand(comm, dbDataParam.ToArray())
                .ExecuteScalar<int>();
            if (result > 0 && primaryKeyProperty != null)
                primaryKeyProperty.SetValue(entity, result);
            return result;
        }


        public static int Insert<T, TD>(DbManager db, TD dto)
        {
            var entity = Mapper.Map<TD, T>(dto);
            return Insert(typeof(T), db, entity);
        }
    }
}