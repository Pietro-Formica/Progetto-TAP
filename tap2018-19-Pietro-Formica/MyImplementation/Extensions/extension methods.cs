using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MyImplementation.Builders;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Extensions
{
    public static class ExtensionMethods
    {
        public static IEnumerable<string> GetAllSiteName(this SiteBuilder siteBuilder)
        {
            if(siteBuilder.ConnectionString is null)
                throw new ArgumentNullException();
            using (var context = new MyDBdContext(siteBuilder.ConnectionString))
            {
                var query = from tmp in context.SiteEntities
                    select tmp.Id;
                foreach (var s in query.ToList()) yield return s;
            }
        }
        public static SiteBuilder SearchEntity(this SiteBuilder siteBuilder, string name)
        {
            if (siteBuilder.ConnectionString is null)
                throw new ArgumentNullException();
            Control.CheckName(DomainConstraints.MaxSiteName, DomainConstraints.MinSiteName, name);
            using (var context = new MyDBdContext(siteBuilder.ConnectionString))
            {

                var siteEntity = context.SiteEntities.Find(name) ?? throw new InexistentNameException(name);
               /* var property = siteBuilder.GetType();

                property.GetProperty("Id")?.SetValue(siteBuilder, siteEntity.Id);
                property.GetProperty("Timezone")?.SetValue(siteBuilder,siteEntity.Timezone);
                property.GetProperty("SessionExpirationInSeconds")?.SetValue(siteBuilder, siteEntity.SessionExpirationInSeconds);
                property.GetProperty("MinimumBidIncrement")?.SetValue(siteBuilder, siteEntity.MinimumBidIncrement);

*/
               siteBuilder.SiteEntity = siteEntity;

 

                return siteBuilder;
            }           
        }
        public static void SaveEntityOnDb(this UserEntity userEntity, string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            using (var contextDb = new MyDBdContext(connectionString))
            {
               // if (_userEntity is null) throw new ArgumentNullException();
                contextDb.UserEntities.Add(userEntity);
                try
                {
                    contextDb.SaveChanges();
                }
                catch (DbUpdateException)//da rivedere lol
                {
                    throw new NameAlreadyInUseException(userEntity.Id);
                }
            }
        }
    }
}
