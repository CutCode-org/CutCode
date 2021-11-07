using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode.DataBase
{
    [Table("CodeTable")]
    public class CodeTable
    {
        [PrimaryKey, AutoIncrement]
        [SQLite.Column("id")]
        public int Id { get; set; }

        [SQLite.Column("title")]
        public string title { get; set; }

        [SQLite.Column("description")]
        public string desc { get; set; }

        [SQLite.Column("code")]
        public string code { get; set; }

        [SQLite.Column("Favourite")]
        public bool isFav { get; set; }

        [SQLite.Column("Language")]
        public string lang { get; set; }

        [SQLite.Column("timestamp")]
        public long timestamp { get; set; }
    }
}
