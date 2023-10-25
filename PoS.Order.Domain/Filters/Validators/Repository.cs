using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PoS.Infra.Domain.Interfaces;
using PoS.Order.Domain.Interfaces.Repository;
using PoS.Order.Domain.Models;
using PoS.Order.Domain.Settings;

namespace PoS.Order.Domain.Filters.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public static class Repository<RepositoryType> where RepositoryType : IPoSRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableType"></param>
        /// <param name="orm"></param>
        /// <returns></returns>
        public static Boolean HasTableFor(Type tableType, ORM orm = ORM.EntityFramewokCore)
        {
            if(orm.Equals(ORM.EntityFramewokCore))
                return typeof(RepositoryType).GetProperties().Any<PropertyInfo>(property => property.PropertyType.GenericTypeArguments.Any<Type>(type => type == tableType));
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Table"></typeparam>
        /// <param name="repositoryInstance"></param>
        /// <param name="orm"></param>
        /// <returns></returns>
        public static IDbOperation<Object> GetTableReferenceFor<Table>(RepositoryType repositoryInstance, ORM orm = ORM.EntityFramewokCore) where Table : OrderType
        {
            if(orm.Equals(ORM.EntityFramewokCore))
            {
                Object? methodReference = typeof(RepositoryType).GetProperties().First<PropertyInfo>
                (
                    property => property.PropertyType.GenericTypeArguments.First<Type>(type => type == typeof(Table)) is not null
                )
                .GetMethod?.Invoke(repositoryInstance, null);

                return new DbOperation<Object>(methodReference, methodReference is not null ? DbOperationsStatus.Success : DbOperationsStatus.Failed);
            }
            return new DbOperation<Object>(operationsStatus: DbOperationsStatus.Unallowed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Schema"></typeparam>
        /// <param name="tableReference"></param>
        /// <param name="orm"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IDbOperation<DbSet<Schema>> GetTableThroughReferenceFor<Schema>(Object tableReference, ORM orm = ORM.EntityFramewokCore) where Schema : OrderType
        {
            try 
            {
                if(orm.Equals(ORM.EntityFramewokCore))
                    return new DbOperation<DbSet<Schema>>((DbSet<Schema>)tableReference, operationsStatus: DbOperationsStatus.Success);

                throw new ArgumentOutOfRangeException("ORM specification is not allowed.", new Exception("ORM specification had no implementation at runtime."));
            }
            catch
            { 
                throw new ArgumentException($"Invalid reference for {nameof(tableReference)}."); 
            }
        }
    }
}
