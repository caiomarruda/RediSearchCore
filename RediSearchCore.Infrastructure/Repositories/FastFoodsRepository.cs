using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NRediSearch;
using RediSearchCore.Core.Entities;
using RediSearchCore.Core.Interfaces;
using RediSearchCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RediSearchCore.Infrastructure.Repositories
{
    public sealed class FastFoodsRepository : BaseCacheRepository, IFastFoodsRepository
    {
        private static readonly string ixName = "fastfoods";
        private readonly IConfiguration _configuration;

        public FastFoodsRepository(IConfiguration configuration) : base(configuration["redisConnection"], ixName)
        {
            _configuration = configuration;
        }

        public List<FastFoods> Search(FastFoodsInputCommand command)
        {
            Query q = new Query($"(@name:{ command.Sentence })*|(@address:{ command.Sentence }*)|(@country:BR*)")
                .SetLanguage("portuguese");

            //todo: insert this validation in model
            //check if exists lat, lon and radius
            if (!Equals(0,command.Latitude) && !Equals(0,command.Longitude) && !Equals(0,command.Kilometers))
                q.AddFilter(new Query.GeoFilter("GeoPoint", command.Longitude, command.Latitude, command.Kilometers, StackExchange.Redis.GeoUnit.Kilometers));

            var result = _client.Search(q).Documents;

            return CastRedisValues<FastFoods>(result);
        }

        public async Task<IEnumerable<FastFoods>> SearchAsync(FastFoodsInputCommand command)
        {
            Query q = new Query($"(@Name:{ command.Sentence }*)|(@Address:{ command.Sentence }*)")
                .SetLanguage("portuguese");

            //todo: insert this validation in model
            //check if exists lat, lon and radius
            if (!Equals(0, command.Latitude) && !Equals(0, command.Longitude) && !Equals(0, command.Kilometers))
                q.AddFilter(new Query.GeoFilter("GeoPoint", command.Longitude, command.Latitude, command.Kilometers, StackExchange.Redis.GeoUnit.Kilometers));

            var result = await _client.SearchAsync(q);
            var stringResponse = result.Documents;

            return CastRedisValues<FastFoods>(stringResponse);
        }

        public override bool CreateIndex()
        {
            Schema sch = new Schema();
            sch.AddTextField("Name");
            sch.AddTextField("Country");
            sch.AddTextField("Address");
            sch.AddGeoField("GeoPoint");

            return _client.CreateIndex(sch, Client.IndexOptions.Default);
        }

        public void PushSampleData()
        {
            string url = _configuration["fastFoodsRepository"];
            string jsonData = new WebClient().DownloadString(url);
            var result = JsonConvert.DeserializeObject<List<FastFoods>>(jsonData);

            foreach (var item in result)
            {
                Guid g = Guid.NewGuid();
                var dictItem = new Dictionary<string, dynamic>
                {
                    { "Name", item.Name },
                    { "Country", item.Country },
                    { "Address", item.Address },
                    { "Lat", item.Lat },
                    { "Lon", item.Lon },
                    { "GeoPoint", $"{ item.Lon },{ item.Lat }" }
                };

                this.Add(g.ToString(), dictItem);
            }
        }
    }
}
