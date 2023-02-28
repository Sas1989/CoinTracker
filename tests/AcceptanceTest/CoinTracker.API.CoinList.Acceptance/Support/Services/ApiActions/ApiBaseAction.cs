using CoinTracker.API.CoinList.Acceptance.Support.Services;
using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions.Interface;
using CoinTracker.API.CoinList.Acceptance.Support.Services.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions
{
    public class ApiBaseAction<T> : IApiActionBase where T : BaseEntity
    {
        protected readonly string singleEndPoint;
        protected readonly string bulkEndPoint;
        protected readonly HttpClient client;


        protected ApiBaseAction(string SingleEndPoint, string BulkEndPoint)
        {
            singleEndPoint = SingleEndPoint;
            bulkEndPoint = BulkEndPoint;
            client = new HttpClientFactory().Build();
        }

        public async Task<T> Create(object data)
        {
            var result = await client.PostAsJsonAsync(singleEndPoint, data);
            return await result.Content.ReadFromJsonAsync<T>();
        }

        public async Task<IEnumerable<T>> CreateMassive<RecivedObject>(List<RecivedObject> data)
        {
            var result = await client.PostAsJsonAsync(bulkEndPoint, data);
            return await result.Content.ReadFromJsonAsync<IEnumerable<T>>();
        }

        public async Task<T> Get(Guid id)
        {
            var result = await client.GetAsync($"{singleEndPoint}/{id}");
            return await result.Content.ReadFromJsonAsync<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await client.GetAsync($"{singleEndPoint}");
            return await result.Content.ReadFromJsonAsync<IEnumerable<T>>();
        }

        public async Task Delete(Guid id)
        {
            await client.DeleteAsync($"{singleEndPoint}/{id}");
        }

        public async Task<bool> Exist(Guid id)
        {
            var result = await client.GetAsync($"{singleEndPoint}/{id}");
            switch (result.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return true;
                case System.Net.HttpStatusCode.NotFound:
                    return false;
                default:

                    throw new Exception($"Error status code : {result.StatusCode}");
            }
        }


        public async Task Clean()
        {
            var elements = await GetAll();

            foreach (T e in elements)
            {
                await Delete(e.Id);
            }
        }
    }
}
