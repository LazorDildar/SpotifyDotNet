/*
 * Spotify's Library API contains no objects, but rather returns objects from
 * the current user's library. As such, this API implementation is placed into
 * appropriate classes.
 * Note: Library requires a specific scope permission to access.
 * 
 * More info here:
 * https://developer.spotify.com/console/library/
*/


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyDotNet {
  public partial class PublicUser : SpotifyBaseObject {
    #region GET
    /// <summary>
    /// Retrieve current user's saved albums.
    /// </summary>
    public static async Task<Page<Album>> GetSavedAlbumsAsync(
      int offset = 0, int limit = 20) {

      if (!SpotifyApi.IsAuthenticated) return null;
      string endpoint = string.Format("/me/albums?offset={0}&limit={1}", offset, limit);
      string albums = await SpotifyApi.ApiGetAsync(endpoint);
      return JsonConvert.DeserializeObject<Page<Album>>(albums);
    }
    /// <summary>
    /// Retrieve current user's saved tracks.
    /// </summary>
    public static async Task<Page<Track>> GetSavedTracksAsync(
      int offset = 0, int limit = 20){

      if (!SpotifyApi.IsAuthenticated) return null;
      string endpoint = string.Format("/me/tracks?offset={0}&limit={1}", offset, limit);
      string tracks = await SpotifyApi.ApiGetAsync(endpoint);
      return JsonConvert.DeserializeObject<Page<Track>>(tracks);
    }

    ///<summary>
    /// Check for a list of albums within current users saved albums.
    /// </summary>
    public static async Task<bool[]> CheckSavedAlbumsAsync(string[] ids) {
      return await _CheckSavedAsync(ids, "albums");
    }

    ///<summary>
    /// Check for a list of albums within current users saved albums.
    /// </summary>
    public static async Task<bool[]> CheckSavedTracksAsync(string[] ids) {
      return await _CheckSavedAsync(ids, "tracks");
    }

    private static async Task<bool[]> _CheckSavedAsync(string[] ids, string type) {
      if (!SpotifyApi.IsAuthenticated) return null;
      string endpoint = string.Format(
        "/me/{0}/contains?ids={1}", type, string.Join(",", ids));
      string data = await SpotifyApi.ApiGetAsync(endpoint);
      return JsonConvert.DeserializeObject<bool[]>(data);
    }
    #endregion
    #region DELETE
    #endregion
    #region PUT
    #endregion
  }
}
