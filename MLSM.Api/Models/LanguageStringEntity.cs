namespace MLSM.Api.Models
{
    public class LanguageStringEntity
    {
        public string Code { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string? Tags { get; set; } = string.Empty;
        public DateTime? LastUpdateTimeStamp { get; set; }
    }
}
