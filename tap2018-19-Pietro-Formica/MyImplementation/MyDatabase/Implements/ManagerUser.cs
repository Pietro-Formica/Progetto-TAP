using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.MyDatabase.Implements
{
    class ManagerUser
    {
        private readonly string _connectionString = ManagerSetup.ConnectionString;
        public void AddUserOnDb(string nameSite, UserEntity user )
        {
            using (var context = new MyDBdContext(_connectionString))
            {
                context.UserEntities.Add(user);
                try
                {
                    context.SaveChanges();

                }
                catch (DbUpdateException)
                {

                        throw new NameAlreadyInUseException(user.Id);
        
                }

            }
        }

    }
}
