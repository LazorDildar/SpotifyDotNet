using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyDotNet {
  /// <summary>
  /// A SimpleArtist object from the Spotify Web API
  /// https://developer.spotify.com/documentation/web-api/reference/object-model/#artist-object-simplified
  /// </summary>
  public class SimpleArtist : SpotifyDataObject {
    /// <summary>
    /// The name of the artist
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }
    /// <summary>
    /// The Spotify ID for the artist.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }
    /// <summary>
    /// A link to the Web API endpoint providing full details of the artist.
    /// </summary>
    [JsonProperty("href")]
    public string Href { get; set; }
    /// <summary>
    /// The object type: "artist"
    /// </summary>
    [JsonProperty("type")]
    public string SpotifyType { get; set; }
    /// <summary>
    /// The Spotify URI for the artist.
    /// </summary>
    [JsonProperty("uri")]
    public string SpotifyUri { get; set; }

    /* Excludes:
     * external_urls : external Url object
    */

    #region Artist API implementation
    #region SimpleArtist queries
    //These are the Artist API calls as listed here:
    //https://developer.spotify.com/console/artists/
    /// <summary>
    /// Retrieve artist by id from Spotify.
    /// </summary>
    public static async Task<SimpleArtist> GetAsync(string id) {
      return await GetAsync<SimpleArtist>(string.Format("/albums/{0}", id));
    }
    /// <summary>
    /// Retrieve multiple artists using an array of ids
    /// </summary>
    public static async Task<SimpleArtist[]> GetAsync(string[] ids) {
      string endpoint = string.Format("/artists?ids={0}", string.Join(",", ids));
      return await GetAsync<SimpleArtist[]>(endpoint, "artists");
    }
    /// <summary>
    /// Retrieve a list of up to 20 related artists.
    /// </summary>
    public static async Task<SimpleArtist[]> GetRelatedAsync(string id) {
      if (!SpotifyApi.IsAuthenticated) return null;
      string endpoint = string.Format("/artists/{0}/related-artists", id);
      string artists = await SpotifyApi.ApiGetAsync(endpoint);
      return JObject.Parse(artists)["artists"].ToObject<SimpleArtist[]>();
    }
    /// <summary>
    /// Search for artists by keyword.
    /// </summary>
    /// <param name="query">Keywords</param>
    /// <param name="offset">Pagination offset. Default: 0</param>
    /// <param name="limit">Results per page. Default: 20</param>
    /// <param name="market">Country to search. Default: US</param>
    public static async Task<Page<SimpleArtist>> SearchAsync(
      string keywords, int offset = 0, int limit = 10, string country = "US") {
      return await SearchAsync<SimpleArtist>(keywords, "artist", offset, limit, country);
    }
    #endregion
    #region Other queries
    /// <summary>
    /// Retrieve up to 10 of the most popular tracks from an artist.
    /// </summary>
    public static async Task<Track[]> GetTopTracksAsync(string id, string country="US") {
      if (!SpotifyApi.IsAuthenticated) return null;
      string endpoint = string.Format("/artists/{0}/top-tracks?country={1}", id, country);
      string tracks = await SpotifyApi.ApiGetAsync(endpoint);
      return JObject.Parse(tracks)["tracks"].ToObject<Track[]>();
    }
    /// <summary>
    /// Retrieve the albums by the artist of id.
    /// </summary>
    public static async Task<Page<Album>> GetAlbumsAsync(
      string id, int offset = 0, int limit = 20, string[] groups = null) {

      if (!SpotifyApi.IsAuthenticated) return null;
      //Build a default groups string[] if needed
      string[] include_groups = groups ?? (new string[] { "album" });
      //Build the endpoint string using passed params
      string endpoint = string.Format(
        "/artists/{0}/albums?offset={1}&limit={2}&include_groups={3}",
        id, offset, limit, string.Join(",", include_groups));

      string albums = await SpotifyApi.ApiGetAsync(endpoint);
      return JsonConvert.DeserializeObject<Page<Album>>(albums);
    }
    #endregion
    #endregion
  }

  /// <summary>
  /// An Artist object from the Spotify Web API
  /// https://developer.spotify.com/documentation/web-api/reference-beta/#object-artistobject
  /// </summary>
  public class Artist : SimpleArtist {
    /// <summary>
    /// A list of the genres the artist is associated with. 
    /// (If not yet classified, the array is empty.)
    /// </summary>
    [JsonProperty("genres")]
    public string[] Genres { get; set; }
    /// <summary>
    /// The popularity of the artist. 
    /// The value will be between 0 and 100, with 100 being the most popular. 
    /// The artist’s popularity is calculated from the popularity of all the artist’s tracks.
    /// </summary>
    [JsonProperty("popularity")]
    public int Popularity { get; set; }

    /* Excludes:
    * followers : followers object
    * images : (image object)[]
   */
    #region Artist API Implementation
    //These are the Artist API calls as listed here:
    //https://developer.spotify.com/console/artists/
    /// <summary>
    /// Retrieve artist by id from Spotify.
    /// </summary>
    public new static async Task<Artist> GetAsync(string id) {
      return await GetAsync<Artist>(string.Format("/albums/{0}", id));
    }
    /// <summary>
    /// Retrieve multiple artists using an array of ids
    /// </summary>
    public new static async Task<Artist[]> GetAsync(string[] ids) {
      string endpoint = string.Format("/artists?ids={0}", string.Join(",", ids));
      return await GetAsync<Artist[]>(endpoint, "artists");
    }
    /// <summary>
    /// Retrieve a list of up to 20 related artists.
    /// </summary>
    public new static async Task<Artist[]> GetRelatedAsync(string id) {
      if (!SpotifyApi.IsAuthenticated) return null;
      string endpoint = string.Format("/artists/{0}/related-artists", id);
      string artists = await SpotifyApi.ApiGetAsync(endpoint);
      return JObject.Parse(artists)["artists"].ToObject<Artist[]>();
    }
    /// <summary>
    /// Search for artists by keyword.
    /// </summary>
    /// <param name="query">Keywords</param>
    /// <param name="offset">Pagination offset. Default: 0</param>
    /// <param name="limit">Results per page. Default: 20</param>
    /// <param name="market">Country to search. Default: US</param>
    public new static async Task<Page<Artist>> SearchAsync(
      string keywords, int offset = 0, int limit = 10, string country = "US") {
      return await SearchAsync<Artist>(keywords, "artist", offset, limit, country);
    }
    #endregion
  }
}
