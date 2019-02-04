using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SpotifyDotNet {
  /// <summary>
  /// A SimpleAlbum from the Spotify Web API
  /// https://developer.spotify.com/documentation/web-api/reference-beta/#object-simplifiedalbumobject
  /// </summary>
  public class SimpleAlbum : SpotifyDataObject {
    /// <summary>
    /// The name of the album. In case of an album takedown, the value may be an empty string.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }
    /// <summary>
    /// The artists of the album. Each artist object includes a link in href to more detailed information about the artist.
    /// </summary>
    [JsonProperty("artists")]
    public SimpleArtist[] Artists { get; set; }
    /// <summary>
    /// The Spotify ID for the album.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }
    /// <summary>
    /// The type of the album: one of “album”, “single”, or “compilation”.
    /// </summary>
    [JsonProperty("album_type")]
    public string AlbumType { get; set; }
    /// <summary>
    /// A link to the Web API endpoint providing full details of the album.
    /// </summary>
    [JsonProperty("href")]
    public string Href { get; set; }
    /// <summary>
    /// The object type: “album”
    /// </summary>
    [JsonProperty("type")]
    public string SpotifyType { get; set; }
    /// <summary>
    /// The Spotify URI for the album.
    /// </summary>
    [JsonProperty("uri")]
    public string SpotifyUri { get; set; }

    /* Excludes:
     * album_group : string (optional) -> this only appears on SimpleAlbum in docs
     * available_markets : string[]
     * external_urls : external URL object
     * images : (image object)[]
     * restrictions : restrictons object
    */

    #region SimpleTrack queries
    /// <summary>
    /// Retrieve the album by id.
    /// </summary>
    public static async Task<SimpleAlbum> GetAsync(string id) {
      return await GetAsync<SimpleAlbum>(string.Format("/albums/{0}", id));
    }
    /// <summary>
    /// Retrieve multiple albums from an array of ids.
    /// </summary>
    /// <param name="ids">A string array of ids.</param>
    /// <returns>SimpleAlbum[]</returns>
    public static async Task<SimpleAlbum[]> GetAsync(string[] ids) {
      string endpoint = string.Format("/albums?ids={0}", string.Join(",", ids));
      return await GetAsync<SimpleAlbum[]>(endpoint, "albums");
    }
    /// <summary>
    /// Search albums by keyword.
    /// </summary>
    /// <param name="keywords">Search keywords</param>
    /// <param name="offset">Pagination offset</param>
    /// <param name="limit">Pagination limit</param>
    /// <param name="country">Country availability</param>
    public static async Task<Page<SimpleAlbum>> SearchAsync(
      string keywords, int offset = 0, int limit=20, string country = "US") {

      return await SearchAsync<SimpleAlbum>(
        keywords, "album", offset, limit, country);
    }
    #endregion
    #region Other queries
    /// <summary>
    /// Retrieve tracks from an album.
    /// </summary>
    /// <param name="id">Album Id</param>
    public static async Task<Page<Track>> GetTracks(
      string id, int offset=0, int limit=20, string country="US") {

      if (!SpotifyApi.IsAuthenticated) return null;
      string endpoint = string.Format(
        "/albums/{0}/tracks?offset={1}&limit={2}&country={3}",
        id, offset, limit, country);
      string tracks = await SpotifyApi.ApiGetAsync(endpoint);
      return JsonConvert.DeserializeObject<Page<Track>>(tracks);
    }
    #endregion
  }
  public class Album : SimpleAlbum {
    /// <summary>
    /// The tracks of the album.
    /// </summary>
    [JsonProperty("total_tracks")]
    public int TotalTracks { get; set; }
    /// <summary>
    /// The label for the album.
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; }
    /// <summary>
    /// The popularity of the album. 
    /// The value will be between 0 and 100, with 100 being the most popular. 
    /// The popularity is calculated from the popularity of the album’s individual tracks.
    /// </summary>
    [JsonProperty("popularity")]
    public int Popularity { get; set; }
    /// <summary>
    /// A list of the genres used to classify the album. (If not yet classified, the array is empty.)
    /// </summary>
    [JsonProperty("genres")]
    public string[] Genres { get; set; }
    /// <summary>
    /// The date the album was first released, for example “1981-12-15”. 
    /// Depending on the precision, it might be shown as “1981” or “1981-12”.
    /// </summary>
    [JsonProperty("release_date")]
    public string ReleaseDate { get; set; }
    /// <summary>
    /// The tracks of the album.
    /// </summary>
    [JsonProperty("tracks")]
    public Page<SimpleTrack> Tracks { get; set; }

    /* Excludes:
     * album_group : string (optional) -> this only appears on SimpleAlbum in docs
     * available_markets : string[]
     * copyrights : (copywrite object)[]
     * external_ids : external ID object
     * release_date_precision : string
    */

    /// <summary>
    /// Retrieve the album by id.
    /// </summary>
    public new static async Task<Album> GetAsync(string id) {
      return await GetAsync<Album>(string.Format("/albums/{0}", id));
    }
    /// <summary>
    /// Retrieve multiple albums from an array of ids.
    /// </summary>
    /// <param name="ids">A string array of ids.</param>
    /// <returns>Album[]</returns>
    public new static async Task<Album[]> GetAsync(string[] ids) {
      string endpoint = string.Format("/albums?ids={0}", string.Join(",", ids));
      return await GetAsync<Album[]>(endpoint, "albums");
    }
    /// <summary>
    /// Search albums by keyword.
    /// </summary>
    /// <param name="keywords">Search keywords</param>
    /// <param name="offset">Pagination offset</param>
    /// <param name="limit">Pagination limit</param>
    /// <param name="country">Country availability</param>
    public new static async Task<Page<Album>> SearchAsync(
      string keywords, int offset = 0, int limit = 20, string country = "US") {

      return await SearchAsync<Album>(
        keywords, "album", offset, limit, country);
    }
  }
}
