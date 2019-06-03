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

    public static class SessionBuilderExtension
    {
        /*public static SessionBuilder SearchEntity(this SessionBuilder sessionBuilder, string siteName, string sessionId = null, string userName = null, string password = null)
        {
            using (var context = new MyDBdContext(sessionBuilder.ConnectionString))
            {
                if (sessionId != null)
                {

                    var query = (from site in context.SiteEntities
                        where site.ID == siteName
                        let sessions = site.SessionEntities
                        from session in sessions
                        where session.ID == sessionId
                        select session).Single();

                    sessionBuilder.SetSessionEntity(query);
                    return sessionBuilder;

                }

                if (userName != null && password != null)
                {
                    var sessionEntity = context.SiteEntities.Where(s => s.ID == siteName)
                        .Select(u => u.Users).Single().SingleOrDefault(u => u.ID == userName && u.Password == password);
                    if (sessionEntity is null)
                        return null;
                    sessionBuilder.SetSessionEntity(sessionEntity.Session);
                    return sessionBuilder;
                }

                return sessionBuilder;
            }
        }*/
/*        public static EntitySessionBuilder EntityUser(this EntitySessionBuilder entitySessionBuilder, string username, string password, string nameSite, string connectionString)
        {
            using (var context = new MyDBdContext(connectionString))
            {
                entitySessionBuilder.SetEntityUser(context.SiteEntities.Where(s => s.ID == nameSite)
                    .Select(u => u.Users).Single().SingleOrDefault(u => u.ID == username && u.Password == password));
                return entitySessionBuilder;
            }



        }*/
        public static IEnumerable<SessionEntity> GetAllSessions(this SessionBuilder sessionBuilder, string siteName)
        {
            using (var context = new MyDBdContext(sessionBuilder.ConnectionString))
            {
                IEnumerable<SessionEntity> sessionEntities = context.SiteEntities.Where(s => s.Id == siteName).Select(s => s.SessionEntities).Single();
                return sessionEntities;
            }
        }
        public static void SaveEntityOnDb(this SessionEntity sessionEntity, string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            using (var contextDb = new MyDBdContext(connectionString))
            {
                contextDb.Session.Add(sessionEntity);
                try
                {
                    contextDb.SaveChanges();
                }
                catch (DbUpdateException exception)//da rivedere lol
                {
                    throw new NameAlreadyInUseException(sessionEntity.Id, "booo", exception.InnerException);
                }
            }

        }
    }
}
