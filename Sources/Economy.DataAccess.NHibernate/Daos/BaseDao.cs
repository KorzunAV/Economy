using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using CQRS.Common;
using Economy.DataAccess.NHibernate.Entities;
using Economy.DataAccess.NHibernate.NHibernate;
using Economy.Dtos.Commands;
using NHibernate.Criterion;

namespace Economy.DataAccess.NHibernate.Daos
{
    /// <summary>
    /// Base DAO class 
    /// </summary>
    public abstract class BaseDao
    {
        protected const string CONTAINS_FORMAT = "%{0}%";
        protected const string STARTS_WITH_FORMAT = "{0}%";

        /// <summary>
        /// Session manager 
        /// </summary>
        protected SessionManager SessionManager;
        
        protected BaseDao(IBaseSessionManager sessionManager)
        {
            SessionManager = (SessionManager)sessionManager;
        }


        /// <summary>
        ///Save entity
        /// </summary>
        /// <returns></returns>
        protected long Save<TDto, TEntity>(TDto dto)
            where TDto : BaseDto
            where TEntity : BaseEntity
        {
            TEntity entity = Mapper.Map<TDto, TEntity>(dto);
            SessionManager.CurrentSession.SaveOrUpdate(entity);
            SessionManager.CurrentSession.Flush();
            return entity.Id;
        }

        /// <summary>
        ///Save entities
        /// </summary>
        /// <returns></returns>
        protected void Save<TDto, TEntity>(IList<TDto> dtos)
            where TDto : BaseDto
            where TEntity : BaseEntity
        {
            foreach (var dto in dtos)
            {
                TEntity entity = Mapper.Map<TDto, TEntity>(dto);
                SessionManager.CurrentSession.SaveOrUpdate(entity);
            }
            SessionManager.CurrentSession.Flush();
        }
        

        /// <summary>
        ///Delete entity
        /// </summary>
        /// <returns></returns>
        protected void Delete<TEntity>(int id)
            where TEntity : BaseEntity
        {
            var entity = SessionManager.CurrentSession.Get<TEntity>(id);
            if (entity != null)
            {
                SessionManager.CurrentSession.Delete(entity);
                SessionManager.CurrentSession.Flush();
            }
            else
            {
                throw new ConcurrencyException();
            }
        }

        /// <summary>
        ///Check is exist entity in bd
        /// </summary>
        /// <returns></returns>
        /// /// TODO: CR: SAS-FIX: It shouldn't be a public method
        protected bool IsExist<T>(Expression<Func<T, bool>> func)
            where T : BaseEntity
        {
            return SessionManager.CurrentSession.QueryOver<T>().Select(
                new[] { Projections.Constant(true) }).Where(func).Take(1).SingleOrDefault<bool>();
        }
    }
}