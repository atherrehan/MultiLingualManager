namespace MLSM.UI.Models
{
    public class LangEdit
    {
        public string Code { get; set; } = string.Empty;
    }
    public class LangManagerActionRequestDto
    {
        public string OldCode { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public char Action { get; set; }

    }
    public class LanguageStringResponseDto
    {
        public string Code { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public string LastUpdateTimeStamp { get; set; } = string.Empty;
    }
    public class LanguageStringComponentResponseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ButtonText { get; set; } = string.Empty;
        public string ButtonColor { get; set; } = string.Empty;

    }
}
