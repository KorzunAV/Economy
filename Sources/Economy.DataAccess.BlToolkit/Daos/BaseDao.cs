using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using AutoMapper;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using CQRS.Common;
using CQRS.Dtos;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.DataAccess.BlToolkit.Entities;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public abstract class BaseDao
    {
        public Dictionary<string, int> Cash = new Dictionary<string, int>();
        public const string Prefix = "@p";

        private List<TE> InsertOrSelectList<TE>(DbManager db, List<TE> entities)
            where TE : BaseEntity
        {
            var type = typeof(TE);
            foreach (var entity in entities)
            {
                InsertOrSelect(type, db, entity);
            }
            return entities;
        }

        private object InsertOrSelect(Type type, DbManager db, BaseEntity entity)
        {
            MapFieldAttribute primaryKey = null;
            PropertyInfo primaryKeyProperty = null;
            var columns = new Dictionary<string, string>();
            var dbDataParam = new List<IDbDataParameter>();

            foreach (var pr in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var assotiation = pr.GetCustomAttribute<AssociationAttribute>();
                if (assotiation != null && !(pr.PropertyType.IsArray || pr.PropertyType.GetInterfaces().Contains(typeof(IEnumerable))))
                {
                    var val = pr.GetValue(entity) as BaseEntity;
                    if (val != null && val.Id == 0)
                        InsertOrSelect(pr.PropertyType, db, val);
                }
            }

            var tn = type.GetCustomAttribute<TableNameAttribute>();
            var unf = new Dictionary<string, string>();
            foreach (var pr in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var ca = pr.GetCustomAttribute<MapFieldAttribute>();
                if (ca != null)
                {
                    var val = pr.GetValue(entity);
                    var idt = pr.GetCustomAttribute<IdentityAttribute>();
                    if (idt != null)
                    {
                        primaryKeyProperty = pr;
                        primaryKey = ca;
                        var pkVal = (int)val;
                        if (pkVal <= 0)
                        {
                            var isEmpty = db.SetCommand($"SELECT 1 FROM {tn.Owner}.{tn.Name} limit 1;").ExecuteScalar<int>();
                            columns.Add(ca.MapName, isEmpty == 1 ? $"(SELECT MAX({ca.MapName}) + 1 FROM {tn.Owner}.{tn.Name})" : "1");
                            continue;
                        }
                    }
                    var paramName = $"{Prefix}{ca.MapName.Trim('"')}";
                    columns.Add(ca.MapName, paramName);
                    dbDataParam.Add(db.Parameter(paramName, val));

                    var udt = pr.GetCustomAttribute<UniqueAttribute>();
                    if (udt != null)
                    {
                        unf.Add(ca.MapName, paramName);
                    }
                }
            }
            var id = 0;
            var ukey = $"{tn.Name.Trim('"')}_{string.Join("_", unf.Select(i => $"{i.Key.Trim('"')}_{dbDataParam.Single(itm => itm.ParameterName == i.Value).Value}"))}";
            if (unf.Count > 0 && primaryKey != null)
            {
                if (Cash.ContainsKey(ukey))
                {
                    id = Cash[ukey];
                }
                else
                {
                    var find = $@"SELECT {primaryKey.MapName} 
                                  FROM {tn.Owner}.{tn.Name}
                                  WHERE {string.Join(" AND ", unf.Select(i => $"{i.Key} = {i.Value}"))};";
                    id = db.SetCommand(find, dbDataParam.ToArray())
                        .ExecuteScalar<int>();
                }
            }
            bool isNew = false;
            if (id < 1)
            {
                var returning = (primaryKey != null ? $"returning {primaryKey.MapName}" : string.Empty);
                var comm = $@"INSERT INTO {tn.Owner}.{tn.Name}({string.Join(",", columns.Select(i => i.Key))}) 
                          VALUES({string.Join(",", columns.Select(i => i.Value))}) {returning};";

                id = db.SetCommand(comm, dbDataParam.ToArray())
                    .ExecuteScalar<int>();
                isNew = true;
            }

            if (id > 0 && primaryKeyProperty != null)
            {
                primaryKeyProperty.SetValue(entity, id);

                if (unf.Count > 0 && !Cash.ContainsKey(ukey))
                {
                    Cash.Add(ukey, id);
                }

                entity.UpdateAssociations();

                if (isNew)
                {
                    foreach (var pr in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        var assotiation = pr.GetCustomAttribute<AssociationAttribute>();

                        if (assotiation != null && pr.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                        {
                            var vals = pr.GetValue(entity) as IEnumerable;
                            if (vals != null)
                            {
                                var vType = pr.PropertyType.GenericTypeArguments[0];
                                foreach (BaseEntity val in vals)
                                {
                                    if (val != null && val.Id == 0)
                                    {
                                        InsertOrSelect(vType, db, val);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return entity;
        }

        internal TD InsertOrSelect<TD, TE>(IBaseSessionManager manager, TD dto)
            where TD : BaseDto
            where TE : BaseEntity
        {
            var entity = Mapper.Map<TD, TE>(dto);
            var db = (EconomyDb)manager;
            var rez = InsertOrSelect(typeof(TE), db, entity);
            return Mapper.Map<TE, TD>((TE)rez);
        }

        internal List<TD> InsertOrSelectList<TD, TE>(IBaseSessionManager manager, List<TD> dtos)
           where TD : BaseDto
           where TE : BaseEntity
        {

            var entities = Mapper.Map<List<TD>, List<TE>>(dtos);
            var db = (EconomyDb)manager;
            var rez = InsertOrSelectList(db, entities);
            return Mapper.Map<List<TE>, List<TD>>(rez);
        }
    }
}