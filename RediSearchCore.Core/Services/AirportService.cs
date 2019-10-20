using RediSearchCore.Core.Entities;
using RediSearchCore.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RediSearchCore.Core.Services
{
    public class AirportService : IAirportService
    {
        private readonly IAirportsRepository _airportsRepository;

        public AirportService(IAirportsRepository airportsRepository)
        {
            _airportsRepository = airportsRepository;
        }

        public bool CreateIndex()
        {
            return _airportsRepository.CreateIndex();
        }

        public bool DropIndex()
        {
            return _airportsRepository.DropIndex();
        }

        public void PushSampleData()
        {
            _airportsRepository.PushSampleData();
        }

        public List<Airports> Search(string value)
        {
            return _airportsRepository.Search(value);
        }

        public Task<Airports> GetAsync(string key)
        {
            return _airportsRepository.GetAsync<Airports>(key);
        }

        public Task<bool> AddAsync(string docId, Dictionary<string, dynamic> docDic)
        {
            return _airportsRepository.AddAsync(docId, docDic);
        }

        public Task<bool> DeleteAsync(string docId)
        {
            return _airportsRepository.DeleteAsync(docId);
        }

        public Task<bool> UpdateAsync(string docId, Dictionary<string, dynamic> docDic)
        {
            return _airportsRepository.UpdateAsync(docId, docDic);
        }
    }
}
