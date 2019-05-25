using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MyImplementation.Builders;
using MyImplementation.Exceptions.Interface;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Extensions
{

    public static class SiteBuilderExtension
    {
/*        public static SiteBuilder SearchEntity(this SiteBuilder siteBuilder, string name, IExceptionDb exception)
        {
            Control.CheckName(DomainConstraints.MaxSiteName, DomainConstraints.MinSiteName, name);
            using (var context = new MyDBdContext(siteBuilder.ConnectionString))
            {
                var siteEntity = context.SiteEntities.Find(name);

                if (siteEntity is null)
                {
                    exception.GetException();
                }
                siteBuilder.SetEntity()
                return siteBuilder;
            }
        }*/
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
        public static void DestroyEntity(this SiteBuilder siteBuilder, string siteName)
        {
            using (var contextDb = new MyDBdContext(siteBuilder.ConnectionString))
            {
                var siteEntity = contextDb.SiteEntities.Find(siteName);
                if (siteEntity is null) throw new InvalidOperationException();
                contextDb.SiteEntities.Remove(siteEntity);
                contextDb.SaveChanges();

            }
        }
        public static IEnumerable<SiteEntity> GetAllSiteName(this SiteBuilder siteBuilder)
        {
            using (var context = new MyDBdContext(siteBuilder.ConnectionString))
            {
                IEnumerable<SiteEntity> siteName = context.SiteEntities.ToList();
                return siteName;
            }
        }
    }
}
