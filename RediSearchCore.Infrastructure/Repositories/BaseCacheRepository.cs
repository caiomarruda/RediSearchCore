using Newtonsoft.Json;
using NRediSearch;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RediSearchCore.Infrastructure.Repositories
{
    public abstract class BaseCacheRepository
    {
        protected ConnectionMultiplexer _redis;
        protected IDatabase _database;
        protected Client _client;

        public BaseCacheRepository(string con, string indexName)
        {
            ConnectRedisServer(con);
            _database = _redis.GetDatabase();
            _client = new Client(indexName, _database);
        }

        private void ConnectRedisServer(string con)
        {
            var config = ConfigurationOptions.Parse(con);
            config.AbortOnConnectFail = true;
            config.KeepAlive = 10;
            config.SyncTimeout = 1000;
            config.ReconnectRetryPolicy = new ExponentialRetry(5000); // defaults maxDeltaBackoff to 10000 ms

            _redis = ConnectionMultiplexer.Connect(config);
        }

        public async Task<bool> AddAsync(string docId, Dictionary<string, dynamic> docDic)
        {
            Dictionary<string, RedisValue> dictDocRedis = new Dictionary<string, RedisValue>();

            foreach (var item in docDic)
            {
                dictDocRedis.Add(item.Key, item.Value ?? "");
            }

            return await _client.AddDocumentAsync(docId, dictDocRedis);
        }

        public T Get<T>(string docId) where T : class
        {
            var retObj = _client.GetDocument(docId);

            var jsonRedisItem = JsonConvert.SerializeObject(retObj.GetProperties());
            var jsonResObj = JsonConvert.DeserializeObject<T>(jsonRedisItem);

            return jsonResObj;
        }

        public async Task<T> GetAsync<T>(string docId) where T : class
        {
            var retObj = await _client.GetDocumentAsync(docId);

            var jsonRedisItem = JsonConvert.SerializeObject(retObj.GetProperties());
            var jsonResObj = JsonConvert.DeserializeObject<T>(jsonRedisItem);

            return jsonResObj;
        }

        public bool Add(string docId, Dictionary<string, dynamic> docDic)
        {
            Dictionary<string, RedisValue> dictDocRedis = new Dictionary<string, RedisValue>();

            foreach (var item in docDic)
            {
                dictDocRedis.Add(item.Key, item.Value ?? "");
            }

            return _client.AddDocument(docId, dictDocRedis);
        }

        public bool Delete(string docId)
        {
            return _client.DeleteDocument(docId);
        }

        public async Task<bool> DeleteAsync(string docId)
        {
            return await _client.DeleteDocumentAsync(docId);
        }

        public bool Update(string docId, Dictionary<string, dynamic> docDic)
        {
            Dictionary<string, RedisValue> dictDocRedis = new Dictionary<string, RedisValue>();

            foreach (var item in docDic)
            {
                dictDocRedis.Add(item.Key, item.Value ?? "");
            }

            return _client.UpdateDocument(docId, dictDocRedis);
        }

        public async Task<bool> UpdateAsync(string docId, Dictionary<string, dynamic> docDic)
        {
            Dictionary<string, RedisValue> dictDocRedis = new Dictionary<string, RedisValue>();

            foreach (var item in docDic)
            {
                dictDocRedis.Add(item.Key, item.Value ?? "");
            }

            return await _client.UpdateDocumentAsync(docId, dictDocRedis);
        }

        protected List<T> CastRedisValues<T>(List<Document> docList) where T : class
        {
            List<T> newDoc = new List<T>();

            foreach (var item in docList)
            {
                var jsonRedisItem = JsonConvert.SerializeObject(item.GetProperties());
                var jsonResObj = JsonConvert.DeserializeObject<T>(jsonRedisItem);
                newDoc.Add(jsonResObj);
            }

            return newDoc;
        }

        public abstract bool CreateIndex();
        public bool DropIndex()
        {
            return _client.DropIndex();
        }
    }
}
