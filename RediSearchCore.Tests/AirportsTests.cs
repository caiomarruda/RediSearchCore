using Microsoft.AspNetCore.Mvc.Testing;
using RediSearchCore.Core.Entities;
using RediSearchCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RediSearchCore.Tests
{
    public class AirportsTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly HttpClient _client;

        public AirportsTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("sao paulo")]
        [InlineData("rio")]
        public async Task Airports_Search_Success(string sentence)
        {
            try
            {
                DeleteIndex();
                CreateIndex();
                PushSampleData();

                var response = await _client.GetAsync($"/api/airports/search/?value={ sentence }");
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();

                var notificationResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Notification>(stringResponse);
                var dataObj = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Airports>>(notificationResult.Data.ToString());


                // Assert
                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && notificationResult.Success && dataObj.Count() > 0);
            }
            catch (Exception ex)
            {
                Assert.True(false, "Error in Search Method");
            }
        }

        [Theory]
        [InlineData("sao p")]
        [InlineData("zzz")]
        public async Task Airports_Search_Empty(string sentence)
        {
            try
            {
                DeleteIndex();
                CreateIndex();
                PushSampleData();

                var response = await _client.GetAsync($"/api/airports/search/?value={ sentence }");
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();

                var notificationResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Notification>(stringResponse);
                var dataObj = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Airports>>(notificationResult.Data.ToString());

                // Assert
                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && notificationResult.Success && dataObj.Count() == 0);
            }
            catch (Exception ex)
            {
                Assert.True(false, "Error in Search Method");
            }
        }

        [Fact]
        public async Task Airports_Add_Success()
        {
            try
            {
                DeleteIndex();
                CreateIndex();

                Airports airports = new Airports
                {
                    Code = "AAA",
                    Name = "Airport Test",
                    City = "City Test",
                    Country = "Country Test",
                    Id = Guid.NewGuid().ToString(),
                    State = "State Test",
                    Score = 0,
                    Lat = "",
                    Lon = "",
                    Tag = ""
                };

                var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(airports), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"/api/airports/?docId={ airports.Id }", stringContent);
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();

                var notificationResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Notification>(stringResponse);

                // Assert
                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && notificationResult.Success);
            }
            catch (Exception ex)
            {
                Assert.True(false, "Error in Add Method");
            }
        }

        [Fact]
        public async Task Airports_Update_Success()
        {
            try
            {
                DeleteIndex();
                CreateIndex();
                string docId = Guid.NewGuid().ToString();

                Airports airports = new Airports
                {
                    Code = "AAA",
                    Name = "Airport Test",
                    City = "City Test",
                    Country = "Country Test",
                    Id = docId,
                    State = "State Test",
                    Score = 0,
                    Lat = "",
                    Lon = "",
                    Tag = ""
                };

                var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(airports), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"/api/airports/?docId={ airports.Id }", stringContent);
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();

                var notificationResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Notification>(stringResponse);

                // Update
                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && notificationResult.Success);

                airports = new Airports
                {
                    Code = "BBB",
                    Name = "Airport Test2",
                    City = "City Test2",
                    Country = "Country Test2",
                    Id = docId,
                    State = "State Test",
                    Score = 1,
                    Lat = "",
                    Lon = "",
                    Tag = ""
                };

                stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(airports), Encoding.UTF8, "application/json");

                response = await _client.PutAsync($"/api/airports/?docId={ airports.Id }", stringContent);
                response.EnsureSuccessStatusCode();
                stringResponse = await response.Content.ReadAsStringAsync();

                notificationResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Notification>(stringResponse);

                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && notificationResult.Success);

                //Get
                response = await _client.GetAsync($"/api/airports/{ airports.Id }");
                response.EnsureSuccessStatusCode();
                stringResponse = await response.Content.ReadAsStringAsync();

                notificationResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Notification>(stringResponse);
                var dataObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Airports>(notificationResult.Data.ToString());

                Assert.True(dataObj.Code == "BBB");
            }
            catch (Exception ex)
            {
                Assert.True(false, "Error in Update Method");
            }
        }

        [Fact]
        public async Task Airports_Delete_Success()
        {
            try
            {
                DeleteIndex();
                CreateIndex();
                string docId = Guid.NewGuid().ToString();

                Airports airports = new Airports
                {
                    Code = "AAA",
                    Name = "Airport Test",
                    City = "City Test",
                    Country = "Country Test",
                    Id = docId,
                    State = "State Test",
                    Score = 0,
                    Lat = "",
                    Lon = "",
                    Tag = ""
                };

                var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(airports), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"/api/airports/?docId={ airports.Id }", stringContent);
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();

                var notificationResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Notification>(stringResponse);

                // Delete
                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && notificationResult.Success);

                response = await _client.DeleteAsync($"/api/airports/{ airports.Id }");
                response.EnsureSuccessStatusCode();
                stringResponse = await response.Content.ReadAsStringAsync();

                notificationResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Notification>(stringResponse);

                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && notificationResult.Success);
            }
            catch (Exception ex)
            {
                Assert.True(false, "Error in Delete Method");
            }
        }

        [Fact]
        public async Task Airports_Get_Success()
        {
            try
            {
                DeleteIndex();
                CreateIndex();
                string docId = Guid.NewGuid().ToString();

                Airports airports = new Airports
                {
                    Code = "AAA",
                    Name = "Airport Test",
                    City = "City Test",
                    Country = "Country Test",
                    Id = docId,
                    State = "State Test",
                    Score = 0,
                    Lat = "",
                    Lon = "",
                    Tag = ""
                };

                var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(airports), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"/api/airports/?docId={ airports.Id }", stringContent);
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();

                var notificationResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Notification>(stringResponse);

                // Get
                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && notificationResult.Success);

                response = await _client.GetAsync($"/api/airports/{ airports.Id }");
                response.EnsureSuccessStatusCode();
                stringResponse = await response.Content.ReadAsStringAsync();

                notificationResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Notification>(stringResponse);
                var dataObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Airports>(notificationResult.Data.ToString());

                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && notificationResult.Success && dataObj.Code == "AAA");
            }
            catch (Exception ex)
            {
                Assert.True(false, "Error in Get Method");
            }
        }

        private HttpResponseMessage DeleteIndex()
        {
            return _client.DeleteAsync($"/api/airports/DropIndex").Result;
        }

        private HttpResponseMessage CreateIndex()
        {
            var stringContent = new StringContent("", Encoding.UTF8, "application/json");

            return _client.PostAsync($"/api/airports/CreateIndex", stringContent).Result;
        }

        private HttpResponseMessage PushSampleData()
        {
            var stringContent = new StringContent("", Encoding.UTF8, "application/json");
            return _client.PostAsync($"/api/airports/PushSampleData", stringContent).Result;
        }
    }
}
