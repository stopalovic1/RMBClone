using System.Net.Http;

namespace RmbRazorPages.Library.ApiClient
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }
    }
}