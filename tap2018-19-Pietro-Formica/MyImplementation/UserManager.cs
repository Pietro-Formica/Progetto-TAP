using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation
{

    public class UserManager : IManager<UserEntity,string>
    {
        private readonly string _connectionString;
        private readonly string _mySite;
        public UserManager(string connectionString, string mySite)
        {
            _connectionString = connectionString;
            _mySite = mySite;
        }
        public UserEntity SearchEntity(string key)
        {
            Control.CheckName(DomainConstraints.MaxUserName, DomainConstraints.MinUserName, key);
            using (var context = new MyDBdContext(_connectionString))
            {

                var site = context.SiteEntities.Find(_mySite) ?? throw new InvalidOperationException();
                context.Entry(site)
                    .Collection(users => users.Users)
                    .Load();
;               
               var userEntity = context.UserEntities.Local.SingleOrDefault(user => user.Id.Equals(key));
               if (userEntity is null)
                   return null;
                context.Entry(userEntity)
                    .Reference(session => session.Session)
                    .Load();
                context.Entry(userEntity)
                    .Collection(au => au.WinnerAuctionEntities)
                    .Load();
                context.Entry(userEntity)
                    .Collection(au => au.SellerAuctionEntities)
                    .Load();

                return userEntity;

            }
        }
        public IEnumerable<UserEntity> SearchAllEntities()
        {
            using (var context = new MyDBdContext(_connectionString))
            {
                var users = context.SiteEntities
                    .Where(site => site.Id.Equals(_mySite))
                    .Select(site => site.Users)
                    .Single();

                return users;
            }
        }
        public void DeleteEntity(UserEntity entity)
        {
            using (var context = new MyDBdContext(_connectionString))
            {
                try
                {
                    context.UserEntities.Attach(entity);
                    context.UserEntities.Remove(entity);
                    context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        public void SaveOnDb(UserEntity entity, bool upDate = false)
        {
            using (var contextDb = new MyDBdContext(_connectionString))
            {
                if (upDate is false) 
                {
                    try
                    {

                        contextDb.UserEntities.Add(entity);
                        contextDb.SaveChanges();
                    }
                    catch (DbUpdateException exception) //da rivedere lol
                    {
                        var sqlException = exception.GetBaseException() as SqlException;
                        if (sqlException.Number == 2627)
                        {
                            throw new NameAlreadyInUseException(entity.Id);

                        }

                        throw new InvalidOperationException();
                    }
                }
                else
                {
                    try
                    {
                        contextDb.UserEntities.Attach(entity);
                        contextDb.Entry(entity).State = EntityState.Modified;
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
}
