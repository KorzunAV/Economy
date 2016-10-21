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

        public static int Insert<T, TD>(DbManager db, TD dto, IEnumerable<string> except = null)
        {
            var entity = Mapper.Map<TD, T>(dto);
            var type = typeof(T);

            MapFieldAttribute primaryKey = null;
            var columns = new List<string>();
            var dbDataParam = new List<IDbDataParameter>();

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

            return db.SetCommand(comm, dbDataParam.ToArray())
                .ExecuteScalar<int>();
        }
    }
}