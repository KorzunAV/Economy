using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using CQRS.Dtos;
using Economy.DataAccess.NHibernate.Entities;
using Economy.DataAccess.NHibernate.NHibernate;
using NHibernate.Criterion;

namespace Economy.DataAccess.NHibernate.Daos
{
    /// <summary>
    /// Base DAO class 
    /// </summary>
    public abstract class BaseDao
    {
        protected const string ContainsFormat = "%{0}%";
        protected const string StartsWithFormat = "{0}%";

        /// <summary>
        /// Session manager 
        /// </summary>
        protected ISessionManager SessionManager;

        protected BaseDao(ISessionManager sessionManager)
        {
            SessionManager = sessionManager;
        }


        /// <summary>
        ///Save entity
        /// </summary>
        /// <returns></returns>
        protected TDto Save<TDto, TEntity>(TDto dto)
            where TDto : BaseDto
            where TEntity : BaseEntity
        {
            TEntity entity = Mapper.Map<TDto, TEntity>(dto);
            SessionManager.CurrentSession.SaveOrUpdate(entity);
            SessionManager.CurrentSession.Flush();

            return Mapper.Map<TEntity, TDto>(entity);
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
        protected void Delete<TEntity>(Guid id)
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