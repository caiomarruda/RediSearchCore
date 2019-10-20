using RediSearchCore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RediSearchCore.Core.Interfaces
{
    public interface IAirportService
    {
        List<Airports> Search(string value);
        bool CreateIndex();
        bool DropIndex();
        void PushSampleData();
        Task<Airports> GetAsync(string key);
        Task<bool> AddAsync(string docId, Dictionary<string, dynamic> docDic);
        Task<bool> DeleteAsync(string docId);
        Task<bool> UpdateAsync(string docId, Dictionary<string, dynamic> docDic);
    }
}
