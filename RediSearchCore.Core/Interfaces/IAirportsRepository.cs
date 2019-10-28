using RediSearchCore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RediSearchCore.Core.Interfaces
{
    public interface IAirportsRepository
    {
        Task<bool> AddAsync(string docId, Dictionary<string, dynamic> docDic);
        bool Add(string docId, Dictionary<string, dynamic> docDic);
        bool Delete(string docId);
        Task<bool> DeleteAsync(string docId);
        bool Update(string docId, Dictionary<string, dynamic> docDic);
        Task<bool> UpdateAsync(string docId, Dictionary<string, dynamic> docDic);
        List<Airports> Search(string sentence);
        Task<IEnumerable<Airports>> SearchAsync(string sentence);
        bool CreateIndex();
        bool DropIndex();
        void PushSampleData();
        Task<T> GetAsync<T>(string docId) where T : class;
        T Get<T>(string docId) where T : class;
    }
}
