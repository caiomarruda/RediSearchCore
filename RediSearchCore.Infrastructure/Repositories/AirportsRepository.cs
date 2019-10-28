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

        public AirportsRepository(string con) : base(con, ixName)
        {
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

        public async Task<List<Airports>> SearchAsync(string sentence)
        {
            Query q = new Query($"(@code:{ sentence })|(@city:{ sentence }*)|(@Tag:{{{ sentence }}})")
                .SetLanguage("portuguese")

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

            return _client.CreateIndex(sch, Client.IndexOptions.Default);
        }

        public void PushSampleData()
        {
            string url = "https://gist.githubusercontent.com/caiomarruda/c043955e5e8ba4398f363b7604be4cac/raw/7b0762c09b519f40397e4c3e100b097d861f5588/airports.json";
            string jsonData = new WebClient().DownloadString(url);
            var result = JsonConvert.DeserializeObject<List<Airports>>(jsonData);

            foreach (var item in result)
            {
                Guid g = Guid.NewGuid();
                var dictItem = new Dictionary<string, dynamic>();

                dictItem.Add("Code", item.Code);
                dictItem.Add("Name", item.Name);
                dictItem.Add("City", item.City);
                dictItem.Add("State", item.State);
                dictItem.Add("Country", item.Country);
                dictItem.Add("Tag", item.Tag);
                dictItem.Add("Lat", item.Lat);
                dictItem.Add("Lon", item.Lon);
                dictItem.Add("GeoPoint", $"{ item.Lon },{ item.Lat }");

                this.Add(g.ToString(), dictItem);
            }
        }
    }
}
