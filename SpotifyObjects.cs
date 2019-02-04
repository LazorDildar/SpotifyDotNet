using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace SpotifyDotNet {
  public class SpotifyBaseObject {
    public override string ToString() {
      return JsonConvert.SerializeObject(this);
    }
  }

  public class SpotifyDataObject : SpotifyBaseObject {
    /// <summary>
    /// Retrieve single data object and deserialize into generic type.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize into.</typeparam>
    /// <param name="query">Endpoint with parameters included</param>
    /// <returns>Spotify data serialized into (T)object </returns>
    protected static async Task<T> GetAsync<T>(string query) {
      if (!SpotifyApi.IsAuthenticated) return default;
      string data = await SpotifyApi.ApiGetAsync(query);
      return JsonConvert.DeserializeObject<T>(data);
    }
    /// <summary>
    /// Retrieve multiple data objects and deserialize into generic type.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize into.</typeparam>
    /// <param name="query">Endpoint with parameters included</param>
    /// <param name="arrayKey">The key of the array spotify puts target data in</param>
    /// <returns>Spotify data serialized into (T)object </returns>
    protected static async Task<T> GetAsync<T>(string query, string arrayKey) {
      if (!SpotifyApi.IsAuthenticated) return default;
      string data = await SpotifyApi.ApiGetAsync(query);
      return JObject.Parse(data)[arrayKey].ToObject<T>();
    }
    /// <summary>
    /// Spotify Search API implementation.
    /// Note: This can only search one type.
    /// </summary>
    /// <typeparam name="T">Object type to parse into.</typeparam>
    /// <param name="q">Search keywords</param>
    /// <param name="type">Spotify object type</param>
    /// <param name="offset">Pagination offset</param>
    /// <param name="limit">Pagination limit</param>
    /// <param name="country">Country/Market availability</param>
    /// <returns>Page containing results</returns>
    protected static async Task<Page<T>> SearchAsync<T>(
      string q, string type,
      int offset = 0, int limit = 20, string country="US") {

      if (!SpotifyApi.IsAuthenticated) return default;
      string endpoint = string.Format(
        "/search?q={0}&type={1}&offset={2}&limit={3}&country={4}",
        q, type, offset, limit, country);
      string data = await SpotifyApi.ApiGetAsync(endpoint);
      return JObject.Parse(data)[type + "s"].ToObject<Page<T>>();
    }
  }
}
