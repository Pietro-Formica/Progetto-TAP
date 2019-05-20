using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.Core.Internal;
using MyImplementation.Builders;
using MyImplementation.ConcreteClasses;
using MyImplementation.Exceptions.Interface;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Extensions
{
    public static class ExtensionMethods
    {
        public static IEnumerable<SiteEntity> GetAllSiteName(this SiteBuilder siteBuilder)
        {
            if(siteBuilder.ConnectionString is null)
                throw new ArgumentNullException();
            using (var context = new MyDBdContext(siteBuilder.ConnectionString))
            {
                IEnumerable<SiteEntity> siteName = context.SiteEntities.ToList();
                return siteName;
            }
        }
        public static IEnumerable<UserEntity> GetUsers(this UserBuilder userBuilder, string siteName)
        {
            if (userBuilder.ConnectionString is null)
                throw new ArgumentNullException();
            using (var context = new MyDBdContext(userBuilder.ConnectionString))
            {
                IEnumerable<UserEntity> userEntities = context.SiteEntities.Where(s => s.Id == siteName).Select(s => s.Users).Single();
                return userEntities;
            }
        }
        public static SiteBuilder SearchEntity(this SiteBuilder siteBuilder, string name, IExceptionDb exception)
        {
            if (siteBuilder.ConnectionString is null)
                throw new ArgumentNullException(nameof(siteBuilder));
            Control.CheckName(DomainConstraints.MaxSiteName, DomainConstraints.MinSiteName, name);
            using (var context = new MyDBdContext(siteBuilder.ConnectionString))
            {
                var siteEntity = context.SiteEntities.Find(name);

                if (siteEntity is null)
                {
                    exception.GetException();
                }
                siteBuilder.SiteEntity = siteEntity;
                return siteBuilder;
            }           
        }
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static void SaveEntityOnDb(this UserEntity userEntity, string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            using (var contextDb = new MyDBdContext(connectionString))
            {
                
                try
                {

                    contextDb.UserEntities.Add(userEntity);
                    contextDb.SaveChanges();
                }
                catch (DbUpdateException exception) //da rivedere lol
                {
                    var sqlException = exception.GetBaseException() as SqlException;
                    if (sqlException.Number == 2627)
                    {   
                        throw new NameAlreadyInUseException(userEntity.Id);
                        
                    }                  
                    throw new InvalidOperationException();
                }
            }
        }
        public static void SaveEntityOnDb(this SiteEntity siteEntity, string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            using (var contextDb = new MyDBdContext(connectionString))
            {
                contextDb.SiteEntities.Add(siteEntity);
                try
                {
                    contextDb.SaveChanges();
                }
                catch (DbUpdateException)//da rivedere lol
                {
                    throw new NameAlreadyInUseException(siteEntity.Id);
                }
            }
        }
        public static void DestroyEntity(this SiteBuilder siteBuilder)
        {
            using (var contextDb = new MyDBdContext(siteBuilder.ConnectionString))
            {
                if(siteBuilder.SiteEntity is null)
                    throw new ArgumentNullException();
                contextDb.SiteEntities.Attach(siteBuilder.SiteEntity);
                contextDb.SiteEntities.Remove(siteBuilder.SiteEntity);
                try
                {
                    contextDb.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new InvalidOperationException();
                    
                }

            }
        }
    }
}
