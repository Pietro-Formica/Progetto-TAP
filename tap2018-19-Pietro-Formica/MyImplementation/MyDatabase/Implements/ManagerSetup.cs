using System.Data.Entity;
using MyImplementation.MyDatabase.Context;


namespace MyImplementation.MyDatabase.Implements
{
    class ManagerSetup
    {
        public static string ConnectionString { get; private set; }

        private readonly DropCreateDatabaseAlways<MyDBdContext> _strategy = new DropCreateDatabaseAlways<MyDBdContext>();
        public void SetStrategy() => Database.SetInitializer(_strategy);
        public void Initialize(string connectionString)
        {
            _strategy.InitializeDatabase(new MyDBdContext(connectionString));
            ConnectionString = connectionString;
        }


    }

}
