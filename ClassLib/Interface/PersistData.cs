namespace ClassLib.Data
{
    public interface PersistData<in T> where T : class
    {
        void save(T t);
        int saveGetId(T t);
        void update(T t);
        void delete(int id);
    }
}
