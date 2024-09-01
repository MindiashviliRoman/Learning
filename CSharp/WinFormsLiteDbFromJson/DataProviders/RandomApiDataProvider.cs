using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;

namespace WinFormsLiteDbFromJson.DataProviders
{
    internal class RandomApiDataProvider: IDataProvider
    {
        private string _uri;
        private HttpClient _httpClient;
        public RandomApiDataProvider(string uri)
        {
            _uri = uri;
        }
        public async Task<JsonObject> GetJSONFromRequestAsync()
        {
            return await GetDataAsync();
        }

        private async Task<JsonObject> GetDataAsync()
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
             
            var content = await _httpClient.GetStringAsync(_uri);
            return await Task.Run(() => JsonObject.Parse(content)?.AsObject());
        }
    }
}
