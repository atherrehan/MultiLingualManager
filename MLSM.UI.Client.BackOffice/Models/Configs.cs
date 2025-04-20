namespace MLSM.UI.Client.BackOffice.Models
{
    public static class Configs
    {
        public static string BaseUrl { get; set; } = string.Empty;
        public static void SetBaseUrl(string url)
        {
            BaseUrl = url;
        }
    }
}
