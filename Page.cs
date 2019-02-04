using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace SpotifyDotNet {
  /// <summary>
  /// A paging object from the Spotify Web API
  /// https://developer.spotify.com/documentation/web-api/reference-beta/#object-pagingobject
  /// </summary>
  public class Page<T> : SpotifyBaseObject {
    /// <summary>
    /// The requested data.
    /// </summary>
    [JsonProperty("items")]
    public T[] Items { get; set; }
    /// <summary>
    /// The offset of the items returned (as set in the query or by default)
    /// </summary>
    [JsonProperty("offset")]
    public int Offset { get; set; }
    /// <summary>
    /// The maximum number of items in the response (as set in the query or by default).
    /// </summary>
    [JsonProperty("limit")]
    public int Limit { get; set; }
    /// <summary>
    /// URL to the next page of items. ( null if none )
    /// </summary>
    [JsonProperty("next")]
    public string Next { get; set; }
    /// <summary>
    /// URL to the previous page of items. ( null if none )
    /// </summary>
    [JsonProperty("previous")]
    public string Previous { get; set; }
    /// <summary>
    /// The total number of items available to return.
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }
    /// <summary>
    /// A link to the Web API endpoint returning the full result of the request
    /// </summary>
    [JsonProperty("href")]
    public string Href { get; set; }

    /// <summary>
    /// Retrieve the next page from Spotify API.
    /// Updates the Page object.
    /// </summary>
    public async Task NextPage() {
      if (Next == null) return;
      string nextPageStr = await SpotifyApi.ApiGetAsync(Next, fullUrl: true);
      //Probably not the best way to do this.
      string key = (typeof(T) + "s").ToLower().Split('.')[1];
      Page<T> nextPage = JObject.Parse(nextPageStr)[key].ToObject<Page<T>>();
      CopyPage(nextPage);
    }
    /// <summary>
    /// Retrieve the previous page from Spotify API.
    /// Updates the Page object.
    /// </summary>
    public async Task PreviousPage() {
      if (Next == null) return;
      string prevPageStr = await SpotifyApi.ApiGetAsync(Next, fullUrl: true);
      //Probably not the best way to do this.
      string key = (typeof(T) + "s").ToLower().Split('.')[1];
      Page<T> prevPage = JObject.Parse(prevPageStr)[key].ToObject<Page<T>>();
      CopyPage(prevPage);
    }

    /// <summary>
    /// Retrieve the previous page from Spotify API.
    /// Updates the Page object.
    /// </summary>*
    /// <summary>
    /// Copies all of the properties of page into 'this'
    /// </summary>
    private void CopyPage(Page<T> page) {
      Items = page.Items.Clone() as T[];
      Offset = page.Offset; Limit = page.Limit;
      Next = page.Next; Previous = page.Previous;
      Total = page.Total; Href = page.Href;
    }

  }
}
