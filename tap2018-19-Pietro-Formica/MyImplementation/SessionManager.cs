using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation
{

    public class SessionManager : IManager<SessionEntity>
    {
        private readonly string _connectionString;
        private readonly string _mySite;
        public SessionManager(string connectionString, string mySite)
        {
            _connectionString = connectionString;
            _mySite = mySite;
        }
        public SessionEntity SearchEntity(string key)
        {
            if(key is null)
                throw new ArgumentNullException();
            using (var context = new MyDBdContext(_connectionString))
            {
                var session = context.SiteEntities
                    .Where(site => site.Id.Equals(_mySite))
                    .Select(site => site.SessionEntities)
                    .Single()
                    .SingleOrDefault(sessions => sessions.Id.Equals(key));

                return session;
            }
        }
        public IEnumerable<SessionEntity> SearchAllEntities()
        {
            using (var context = new MyDBdContext(_connectionString))
            {
                var sessions = context.SiteEntities
                    .Where(site => site.Id.Equals(_mySite))
                    .Select(site => site.SessionEntities)
                    .Single();

                return sessions;
            }


        }
        public void SaveOnDb(SessionEntity entity, bool upDate = false)
        {
            using (var contextDb = new MyDBdContext(_connectionString))
            {
                if (upDate is false)
                {
                    contextDb.Session.Add(entity);
                    try
                    {
                        contextDb.SaveChanges();
                    }
                    catch (DbUpdateException exception)//da rivedere lol
                    {
                        throw new NameAlreadyInUseException(entity.Id, "booo", exception.InnerException);
                    }
                }
                else
                {
                    try
                    {
                        contextDb.Session.Attach(entity);
                        contextDb.Entry(entity).State = EntityState.Modified;
                        contextDb.SaveChanges();

                    }
                    catch (DbUpdateException )
                    {
                       throw new InvalidOperationException();
                    }
                }
           
            }
        }
    }
}
