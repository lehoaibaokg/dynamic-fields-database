using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoTruongDuLieuDong.Data.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<bool> Update(int id, T entity);
        Task<bool> Delete(int id);
        Task<bool> ChangeStatus(int id);
    }
}
