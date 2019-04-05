using System.Data.Entity;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.MyDatabase.Interfaces;
using Ninject.Modules;



namespace MyImplementation.MyDatabase.Implements
{
    class ManagerSetup : IManagerSetup
    {
        private readonly DropCreateDatabaseAlways<MyDBdContext> _strategy = new DropCreateDatabaseAlways<MyDBdContext>();

        public void SetStrategy() => Database.SetInitializer(_strategy);
        public void Initialize(string connectionString) => _strategy.InitializeDatabase(new MyDBdContext(connectionString));

    }
}
