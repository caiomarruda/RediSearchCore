using RediSearchCore.Core.Entities;
using RediSearchCore.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RediSearchCore.Core.Interfaces
{
    public interface IFastFoodService
    {
        Task<IEnumerable<FastFoods>> SearchAsync(FastFoodsInputCommand command);
        bool CreateIndex();
        bool DropIndex();
        void PushSampleData();
        Task<Airports> GetAsync(string key);
        Task<bool> AddAsync(string docId, FastFoods fastFoods);
        Task<bool> DeleteAsync(string docId);
        Task<bool> UpdateAsync(string docId, FastFoods fastFoods);
    }
}
