using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyDotNet {
  public class SimplePlaylist : SpotifyDataObject {
    //TODO: Map "Tracks" object in a clean way
    //Its not an object web api model.

    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("owner")]
    public PublicUser Owner { get; set; }
    [JsonProperty("public")]
    public object Public { get; set; }
    [JsonProperty("collaborative")]
    public bool Collaborative { get; set; }
    [JsonProperty("snapshot_id")]
    public string SnapshotId { get; set; }
    //[JsonProperty("external_urls")]
    //public ExternalUrls ExternalUrls { get; set; }
    //[JsonProperty("images")]
    //public Image[] Images { get; set; }
    //[JsonProperty("primary_color")]
    //public object PrimaryColor { get; set; }
    //[JsonProperty("tracks")]
    //public Tracks Tracks { get; set; }
    [JsonProperty("href")]
    public Uri Href { get; set; }
    [JsonProperty("type")]
    public string SpotifyType { get; set; }
    [JsonProperty("uri")]
    public Uri SpotifyUri { get; set; }

    #region GET
    /// <summary>
    /// Retrieve a playlist from Spotify
    /// </summary>
    public static async Task<SimplePlaylist> GetAsync(string id) {
      return await GetAsync<SimplePlaylist>(string.Format("/playlists/{0}", id));
    }
    /// <summary>
    /// Retrieve a user's playlists. Defaults to current user.
    /// </summary>
    public static async Task<Page<SimplePlaylist>> GetUserPlaylistAsync(
      string userid = null, int offset = 0, int limit = 20) {
      //Default to the current user's playlist
      string endpoint = string.IsNullOrWhiteSpace(userid) ?
        string.Format("/me/playlists?offset={0}&limit={1}", offset, limit) : 
        string.Format("/users/{0}/playlists?offset={1}&limit={2}", userid, offset, limit);
      return await GetAsync<Page<SimplePlaylist>>(endpoint);
    }
    /// <summary>
    /// Retrieve the playlist tracks.
    /// </summary>
    public static async Task<Page<PlaylistTrack>> GetTracksAsync(
      string id, int offset = 0, int limit = 20, string country = "US") {

      if (!SpotifyApi.IsAuthenticated) return null;
      string endpoint = string.Format(
        "/playlists/{0}/tracks?offset={1}&limit={2}&country={3}", 
        id, offset, limit, country);
      string data = await SpotifyApi.ApiGetAsync(endpoint);
      return JsonConvert.DeserializeObject<Page<PlaylistTrack>>(data);
    }
    #endregion

    /* TODO: Implement
    #region POST
    #endregion
    #region PUT
    #endregion
    #region DELETE
    #endregion
    */
  }
  public class Playlist : SimplePlaylist {
    [JsonProperty("tracks")]
    public Page<PlaylistTrack> Tracks { get; set; }
    //[JsonProperty("followers")]
    //public Followers Followers { get; set; }

    /// <summary>
    /// Retrieve a playlist from Spotify
    /// </summary>
    public new static async Task<Playlist> GetAsync(string id) {
      return await GetAsync<Playlist>(string.Format("/playlists/{0}", id));
    }
    /// <summary>
    /// Retrieve a user's playlists. Defaults to current user.
    /// </summary>
    public new static async Task<Page<Playlist>> GetUserPlaylistAsync(
      string userid = null, int offset = 0, int limit = 20) {
      //Default to the current user's playlist
      string endpoint = string.IsNullOrWhiteSpace(userid) ?
        string.Format("/me/playlists?offset={0}&limit={1}", offset, limit) :
        string.Format("/users/{0}/playlists?offset={1}&limit={2}", userid, offset, limit);
      return await GetAsync<Page<Playlist>>(endpoint);
    }
  }
}
