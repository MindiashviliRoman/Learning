using System.Text.Json.Nodes;
namespace WinFormsLiteDbFromJson.DataProviders
{
    internal interface IDataProvider
    {
        Task<JsonObject> GetJSONFromRequestAsync();
    }
}
