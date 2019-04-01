using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using Ninject.Modules;
using TAP2018_19.AuctionSite.Database.Interface;


namespace MyDatabase
{
   public class ManagerSetup : IManagerSetup
    {
        private readonly DropCreateDatabaseAlways<MyBdContext> _strategy = new DropCreateDatabaseAlways<MyBdContext>();

        public void SetStrategy() => Database.SetInitializer(_strategy);
        public void Initialize(string connectionString) => _strategy.InitializeDatabase(new MyBdContext(connectionString));
        public bool CheckConnectionString(string connectionString) 
        {
            try
            {
                var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
                return true;
            }
            catch (Exception )
            {
                return false;
            }
            
        }
    }

    class MyBdContext : DbContext
    {
        public MyBdContext(string connectionString) : base(connectionString)
        {
            
        }
    }

    public class NinjectModuleBd : NinjectModule
    {
        public override void Load()
        {
            Bind<IManagerSetup>().To<ManagerSetup>().InSingletonScope();
        }
    }
}
