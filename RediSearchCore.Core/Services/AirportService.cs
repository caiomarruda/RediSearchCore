using RediSearchCore.Core.Entities;
using RediSearchCore.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public Task<IEnumerable<Airports>> SearchAsync(string value)
        {
            value = value.Trim();
            return _airportsRepository.SearchAsync(value);
        }

        public List<Airports> Search(string value)
        {
            value = value.Trim();
            return _airportsRepository.Search(value);
        }

        public Task<Airports> GetAsync(string key)
        {
            return _airportsRepository.GetAsync<Airports>(key);
        }

        public Task<bool> AddAsync(string docId, Airports airports)
        {

            var docDic = CastEntityToDict(airports);

            return _airportsRepository.AddAsync(docId, docDic);
        }

        public Task<bool> DeleteAsync(string docId)
        {
            return _airportsRepository.DeleteAsync(docId);
        }

        public Task<bool> UpdateAsync(string docId, Airports airports)
        {
            var docDic = CastEntityToDict(airports);

            return _airportsRepository.UpdateAsync(docId, docDic);
        }

        private Dictionary<string, object> CastEntityToDict<T>(T data) where T : class
        {
            return data.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .ToDictionary(prop => prop.Name, prop => prop.GetValue(data, null));
        }
    }
}
