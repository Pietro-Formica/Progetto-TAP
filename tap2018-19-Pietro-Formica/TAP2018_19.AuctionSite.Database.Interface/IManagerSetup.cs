namespace TAP2018_19.AuctionSite.Database.Interface
{
    public interface IManagerSetup
    {
        void SetStrategy();
        void Initialize(string connectionString);
        bool CheckConnectionString(string connectionString);
    }
}
