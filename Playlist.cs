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

    /// <summary>
    /// Retrieve a playlist from Spotify
    /// </summary>
    public static async Task<SimplePlaylist> GetAsync(string id) {
      return await GetAsync<SimplePlaylist>(string.Format("/playlists/{0}", id));
    }
  }
  public class Playlist : SimplePlaylist {
    [JsonProperty("tracks")]
    public Page<PlaylistTrack> Tracks { get; set; }
    //[JsonProperty("followers")]
    //public Followers Followers { get; set; }
  }
}
