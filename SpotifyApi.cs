using System;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SpotifyDotNet {
  public static class SpotifyApi {
    private const string _API_URL = @"https://api.spotify.com/v1";

    public static string AccessToken { get; private set; }
    public static string RefreshToken { get; private set; }
    public static bool IsAuthenticated { get; private set; }

    static SpotifyApi() {
      AccessToken = null;
      RefreshToken = null;
      IsAuthenticated = false;
    }

    public static void SetAuthTokens(string accessToken, string refreshToken=null) {
      AccessToken = accessToken;
      RefreshToken = refreshToken;

      IsAuthenticated = true;
      //TODO: test authentication
    }


    /// <summary>
    /// Queries the Spotify API asynchronously.
    /// </summary>
    /// <param name="endpoint">The spotify endpoint to query. e.g. "/me"</param>
    /// <param name="fullUrl">Optional: If true endpoint must include the api url</param>
    /// <returns>Raw Json string response</returns>
    internal static async Task<string> ApiGetAsync(string endpoint, bool fullUrl = false) {
      using (var client = new HttpClient()) {
        //Build the GET request
        var request = new HttpRequestMessage() {
          RequestUri = new Uri(string.Format("{0}{1}", fullUrl ? "" : _API_URL, endpoint)),
          Method = HttpMethod.Get,
        };
        request.Headers.Authorization = 
          new AuthenticationHeaderValue("Bearer", AccessToken);

        //Send the GET
        var responseMsg = await client.SendAsync(request)
          .ConfigureAwait(false);
        //Read the response into a string
        string content = await responseMsg.Content.ReadAsStringAsync()
          .ConfigureAwait(false);

        if (responseMsg.StatusCode != HttpStatusCode.OK) {
          throw new HttpRequestException("Spotify API returned a bad reply: " + content);
        }

        return content;
      }
    } 
  }
}