using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IPreOrderService : IService
    {
        Task CreateAsync(string name);
        PreOrder Get(string name);
        void Update(PreOrder preOrder);
        void Remove(string name);
        Task SaveAsync(string name);
    }
}