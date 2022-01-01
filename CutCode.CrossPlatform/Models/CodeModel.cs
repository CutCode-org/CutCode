using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CutCode.CrossPlatform.Models
{
    public class CodeModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cells { get; set; }
        public long LastModificationTime { get; set; }
        public bool IsFavourite { get; set; }
        public string Language { get; set; }

        public CodeModel(string title, List<Dictionary<string, string>> cells, string language, long lastModificationTime, bool isFavourite)
        {
            Title = title;
            Cells = JsonConvert.SerializeObject(cells);
            Language = language;

            LastModificationTime = lastModificationTime;
            IsFavourite = isFavourite;
        }

        public void SetId(int id) => Id = id;

        public List<Dictionary<string, string>> ParseCells() => JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(Cells);
    }
}