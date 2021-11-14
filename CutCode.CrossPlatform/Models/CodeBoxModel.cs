namespace CutCode.CrossPlatform.Models
{
    public class CodeBoxModel
    {
        public CodeBoxModel(int id, string title, string desc, string language, bool isFav, string code,long timestamp)
        {
            Id = id;
            Title = title;
            Desc = desc;
            Language = language;
            IsFav = isFav;
            Timestamp = timestamp;
            Code = code;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Language { get; set; }
        public bool IsFav { get; set; }
        public long Timestamp { get; set; }
        public string Code { get; set; }
    }
}