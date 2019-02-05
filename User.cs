using System;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace SpotifyDotNet {
  //TODO: implement PrivateUser

  /// <summary>
  /// The public user profile from the Spotify Web API
  /// https://developer.spotify.com/documentation/web-api/reference-beta/#object-publicuserobject
  /// </summary>
  public partial class PublicUser : SpotifyBaseObject {
    /// <summary>
    /// The Spotify user ID for this user.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }
    /// <summary>
    /// The name displayed on the user’s profile. null if not available.
    /// </summary>
    [JsonProperty("display_name")]
    public string DisplayName { get; set; }
    /// <summary>
    /// A link to the Web API endpoint for this user.
    /// </summary>
    [JsonProperty("href")]
    public Uri Href { get; set; }
    /// <summary>
    /// The object type: “user”
    /// </summary>
    [JsonProperty("type")]
    public string SpotifyType { get; set; }
    /// <summary>
    /// The Spotify URI for this user.
    /// </summary>
    [JsonProperty("uri")]
    public Uri SpotifyUri { get; set; }

    /* Excludes:
     * images : (image object)[]
     * followers : followers object
     * external_url : external URL object
    */

    /// <summary>
    /// Get the authenticated PublicUser from Spotify
    /// </summary>
    public static async Task<PublicUser> GetCurrentAsync() {
      if (!SpotifyApi.IsAuthenticated) return null;
      string profile = await SpotifyApi.ApiGetAsync("/me");
      return JsonConvert.DeserializeObject<PublicUser>(profile);
    }

    /// <summary>
    /// Get profile of a user from Spotify
    /// </summary>
    public static async Task<PublicUser> GetAsync(string id) {
      if (!SpotifyApi.IsAuthenticated) return null;
      string profile = await SpotifyApi.ApiGetAsync(String.Format("users/{0}", id)).
        ConfigureAwait(false);
      return JsonConvert.DeserializeObject<PublicUser>(profile);
    }
   
  }
}
