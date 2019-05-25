using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MyImplementation.Builders;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Extensions
{

    public static class UserBuilderExtension
    {
        public static IEnumerable<UserEntity> GetUsers(this UserBuilder userBuilder, string siteName)
        {
            using (var context = new MyDBdContext(userBuilder.ConnectionString))
            {
                IEnumerable<UserEntity> userEntities = context.SiteEntities.Where(s => s.Id == siteName).Select(s => s.Users).Single();
                return userEntities;
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
    }
}
