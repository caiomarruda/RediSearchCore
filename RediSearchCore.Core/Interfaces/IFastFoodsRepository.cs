using RediSearchCore.Core.Entities;
using RediSearchCore.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RediSearchCore.Core.Interfaces
{
    public interface IFastFoodsRepository
    {
        Task<bool> AddAsync(string docId, Dictionary<string, dynamic> docDic);
        bool Add(string docId, Dictionary<string, dynamic> docDic);
        bool Delete(string docId);
        Task<bool> DeleteAsync(string docId);
        bool Update(string docId, Dictionary<string, dynamic> docDic);
        Task<bool> UpdateAsync(string docId, Dictionary<string, dynamic> docDic);
        List<FastFoods> Search(FastFoodsInputCommand command);
        Task<IEnumerable<FastFoods>> SearchAsync(FastFoodsInputCommand command);
        bool CreateIndex();
        bool DropIndex();
        void PushSampleData();
        Task<T> GetAsync<T>(string docId) where T : class;
        T Get<T>(string docId) where T : class;
    }
}
