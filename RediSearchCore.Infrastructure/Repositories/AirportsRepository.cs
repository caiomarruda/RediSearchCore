using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NRediSearch;
using RediSearchCore.Core.Entities;
using RediSearchCore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RediSearchCore.Infrastructure.Repositories
{
    public sealed class AirportsRepository : BaseCacheRepository, IAirportsRepository
    {
        private static readonly string ixName = "airports";
        private readonly IConfiguration _configuration;

        public AirportsRepository(IConfiguration configuration) : base(configuration["redisConnection"], ixName)
        {
            _configuration = configuration;
        }

        public List<Airports> Search(string sentence)
        {
            Query q = new Query($"(@code:{ sentence })|(@city:{ sentence }*)|(@Tag:{{{ sentence }}})")
                .SetLanguage("portuguese");

            //.AddFilter(new Query.NumericFilter("price", 0, 1000))
            //.Limit(0, 5);
            //.AddFilter(new Query.GeoFilter("GeoPoint", 10.0, 20.0, 10, StackExchange.Redis.GeoUnit.Kilometers));

            var result = _client.Search(q).Documents;

            return CastRedisValues<Airports>(result);
        }

        public async Task<IEnumerable<Airports>> SearchAsync(string sentence)
        {
            Query q = new Query($"(@code:{ sentence })|(@city:{ sentence }*)|(@Tag:{{{ sentence }}})")
                .SetLanguage("portuguese");

            //.AddFilter(new Query.NumericFilter("price", 0, 1000))
            //.Limit(0, 5);
            //.AddFilter(new Query.GeoFilter("GeoPoint", 10.0, 20.0, 10, StackExchange.Redis.GeoUnit.Kilometers));

            var result = await _client.SearchAsync(q);
            var stringResponse = result.Documents;

            return CastRedisValues<Airports>(stringResponse);
        }

        public override bool CreateIndex()
        {
            Schema sch = new Schema();
            sch.AddTextField("Code");
            sch.AddTextField("Name");
            sch.AddTextField("City", 5);
            sch.AddTextField("State");
            sch.AddTextField("Country");            
            sch.AddTagField("Tag");
            sch.AddGeoField("GeoPoint");

            return _client.CreateIndex(sch, new Client.ConfiguredIndexOptions(Client.IndexOptions.Default));
        }

        public void PushSampleData()
        {
            string url = _configuration["airportsRepository"];
            string jsonData = new WebClient().DownloadString(url);
            var result = JsonConvert.DeserializeObject<List<Airports>>(jsonData);

            foreach (var item in result)
            {
                Guid g = Guid.NewGuid();
                var dictItem = new Dictionary<string, dynamic>
                {
                    { "Code", item.Code },
                    { "Name", item.Name },
                    { "City", item.City },
                    { "State", item.State },
                    { "Country", item.Country },
                    { "Tag", item.Tag },
                    { "Lat", item.Lat },
                    { "Lon", item.Lon },
                    { "GeoPoint", $"{ item.Lon },{ item.Lat }" }
                };

                this.Add(g.ToString(), dictItem);
            }
        }
    }
}
