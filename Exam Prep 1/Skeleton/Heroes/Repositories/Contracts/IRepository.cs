namespace Heroes.Repositories.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IRepository<T>
    {
        IReadOnlyCollection<T> Models { get; }

        void Add(T model);

        bool Remove(T model);

        T FindByName(string name);
    }
}
