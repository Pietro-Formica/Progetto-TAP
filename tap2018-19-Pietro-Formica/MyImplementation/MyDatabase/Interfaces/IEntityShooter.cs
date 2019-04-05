namespace MyImplementation.MyDatabase.Interfaces
{
    public interface IEntityShooter<in T> where T : IEntity
    {
        void ControlConnectionString(string connectionString);
        
        void Add(T entity);
    }
}
