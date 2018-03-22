using System.Threading.Tasks;
using Orders.Core.Domain;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IPreOrderService : IService
    {
        Task Create(string name);
        Order Get(string name);
        void Remove(string name);
    }
}