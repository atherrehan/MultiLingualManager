namespace MLSM.UI.Client.Models
{
    public class LanguageStringFiltered
    {
        public string LastUpdateTimeStamp { get; set; } = string.Empty;
    }

    public class LanguageStringResponseDto
    {
        public string Code { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
    }
    public class LanguageStringComponentResponseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ButtonText { get; set; } = string.Empty;
        public string ButtonColor { get; set; } = string.Empty;

    }
}
