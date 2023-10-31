using System.Text.Json.Serialization;

namespace Academic_Blog_App.Services.Helper.Odata
{
    public class ODataResponse<T>
    {
        [JsonPropertyName("@odata.context")]
        public string? ODataContext { get; set; }
        [JsonPropertyName("value")]
        public T? Value { get; set; }
    }

}
