namespace MyImplementation.MyDatabase.Interfaces
{
    public interface IManagerSetup
    {
        void SetStrategy();
        void Initialize(string connectionString);
       
    }
}
