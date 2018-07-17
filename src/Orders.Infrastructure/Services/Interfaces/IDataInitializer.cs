using System.Threading.Tasks;
using Orders.Infrastructure.Settings;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IDataInitializer : IService
    {
         Task InitializeData(DataSettings dataSettings);
    }
}