using System.Collections.Generic;

namespace ClassLib.Data
{
    public interface FitchData<out T> where T : class
    {
        IEnumerable<T> getAll();
        T getById(int id);
        IEnumerable<T> getAllByCustomer(int id);
        IEnumerable<T> search(string searchString);
        T verifyLogin(string email, string password);
    }
}
