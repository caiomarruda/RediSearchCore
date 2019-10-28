using RediSearchCore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RediSearchCore.Core.Interfaces
{
    public interface IAirportService
    {
        Task<IEnumerable<Airports>> SearchAsync(string value);
        List<Airports> Search(string value);
        bool CreateIndex();
        bool DropIndex();
        void PushSampleData();
        Task<Airports> GetAsync(string key);
        Task<bool> AddAsync(string docId, Airports airports);
        Task<bool> DeleteAsync(string docId);
        Task<bool> UpdateAsync(string docId, Airports airports);
    }
}
