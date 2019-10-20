using Newtonsoft.Json;
using NRediSearch;
using RediSearchCore.Core.Entities;
using RediSearchCore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;

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
            Query q = new Query(sentence)
                .SetLanguage("portuguese");

            //.AddFilter(new Query.NumericFilter("price", 0, 1000))
            //.Limit(0, 5);

            List<Airports> airports = new List<Airports>();
            var result2 = _client.Search(new Query($"(@code:{ sentence })|(@city:{ sentence }*)"));
            var result = _client.Search(new Query($"(@code:{ sentence })|(@city:{ sentence }*)")).Documents;

            return CastRedisValues<Airports>(result);
        }

        public override bool CreateIndex()
        {
            Schema sch = new Schema();
            sch.AddTextField("Code");
            sch.AddTextField("Name");
            sch.AddTextField("City", 5);
            sch.AddTextField("State");
            sch.AddTextField("Country");

            return _client.CreateIndex(sch, Client.IndexOptions.Default);
        }

        public void PushSampleData()
        {
            string url = "https://gist.githubusercontent.com/tdreyno/4278655/raw/7b0762c09b519f40397e4c3e100b097d861f5588/airports.json";
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

                this.Add(g.ToString(), dictItem);
            }
        }
    }
}
