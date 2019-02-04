using System;
using System.Threading.Tasks;

using Newtonsoft.Json;


namespace SpotifyDotNet {

  /// <summary>
  /// A SimpleTrack object from the Spotify Web API
  /// https://developer.spotify.com/documentation/web-api/reference-beta/#object-simplifiedtrackobject
  /// </summary>
  public class SimpleTrack : SpotifyDataObject {
    /// <summary>
    /// The name of the track.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }
    /// <summary>
    /// The artists who performed the track. Each artist object includes a link 
    /// in href to more detailed information about the artist.
    /// </summary>
    [JsonProperty("artists")]
    public SimpleArtist[] Artists { get; set; }
    /// <summary>
    /// The Spotify ID for the track.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }
    /// <summary>
    /// The number of the track. If an album has several discs, 
    /// the track number is the number on the specified disc.
    /// </summary>
    [JsonProperty("track_number")]
    public int TrackNumber { get; set; }
    /// <summary>
    /// The disc number (usually 1 unless the album consists of more than one disc).
    /// </summary>
    [JsonProperty("disc_number")]
    public int DiscNumber { get; set; }
    /// <summary>
    /// Whether or not the track has explicit lyrics ( true = yes it does; false = no it does not OR unknown).
    /// </summary>
    [JsonProperty("explicit")]
    public bool Explicit { get; set; }
    /// <summary>
    /// The track length in milliseconds.
    /// </summary>
    [JsonProperty("duration_ms")]
    public int Duration { get; set; }
    /// <summary>
    /// A URL to a 30 second preview (MP3 format) of the track.
    /// </summary>
    [JsonProperty("preview_url")]
    public string PreviewUrl { get; set; }
    /// <summary>
    /// A link to the Web API endpoint providing full details of the track.
    /// </summary>
    [JsonProperty("href")]
    public string Href { get; set; }
    /// <summary>
    /// The object type: “track”.
    /// </summary>
    [JsonProperty("type")]
    public string SpotifyType { get; set; }
    /// <summary>
    /// The Spotify URI for the track.
    /// </summary>
    [JsonProperty("uri")]
    public string SpotifyUri { get; set; }

    /* Excludes:
     * available_markets: string[]
     * external_urls: external URL object
     * is_playable: bool
     * linked_from: linked track object
     * restrictions: restrictions object
     * is_local: bool
    */

    /// <summary>
    /// Retrieve a track.
    /// </summary>
    public static async Task<SimpleTrack> GetAsync(string id) {
      return await GetAsync<SimpleTrack>(string.Format("/tracks/{0}", id));
    }
    /// <summary>
    /// Retrieve multiple tracks.
    /// </summary>
    public static async Task<SimpleTrack[]> GetAsync(string[] ids) {
      string endpoint = string.Format("/tracks?ids={0}", string.Join(",", ids));
      return await GetAsync<SimpleTrack[]>(endpoint);
    }
    /// <summary>
    /// Find a page of tracks by keyword.
    /// </summary>
    public static async Task<Page<SimpleTrack>> SearchAsync(
      string keywords, int offset=0, int limit=20, string country="US") {

      return await SearchAsync<SimpleTrack>(keywords, "track", offset, limit, country);
    }
  }

  /// <summary>
  /// A SimpleTrack object from the Spotify Web API
  /// https://developer.spotify.com/documentation/web-api/reference-beta/#object-trackobject
  /// </summary>
  public class Track : SimpleTrack {
    /// <summary>
    /// The popularity of the track. The value will be between 0 and 100, 
    /// with 100 being the most popular.
    /// </summary>
    [JsonProperty("popularity")]
    public int Popularity { get; set; }

    /* Excludes:
     * external_ids: external ID object
    */

    /// <summary>
    /// Retrieve a track.
    /// </summary>
    public new static async Task<Track> GetAsync(string id) {
      return await GetAsync<Track>(string.Format("/tracks/{0}", id));
    }
    /// <summary>
    /// Retrieve multiple tracks.
    /// </summary>
    public new static async Task<Track[]> GetAsync(string[] ids) {
      string endpoint = string.Format("/tracks?ids={0}", string.Join(",", ids));
      return await GetAsync<Track[]>(endpoint);
    }
    /// <summary>
    /// Find a page of tracks by keyword.
    /// </summary>
    public new static async Task<Page<Track>> SearchAsync(
      string keywords, int offset = 0, int limit = 20, string country = "US") {

      return await SearchAsync<Track>(keywords, "track", offset, limit, country);
    }

  }
}
