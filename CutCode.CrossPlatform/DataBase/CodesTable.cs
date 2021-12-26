using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode.DataBase
{
    [Table("CodesTable")]
    public class CodesTable
    {
        [PrimaryKey, AutoIncrement]
        [SQLite.Column("Id")]
        public int Id { get; set; }

        [SQLite.Column("Title")]
        public string Title { get; set; }

        [SQLite.Column("Cells")]
        public string Cells { get; set; }

        [SQLite.Column("IsFavourite")]
        public bool IsFavourite { get; set; }

        [SQLite.Column("Language")]
        public string Language { get; set; }

        [SQLite.Column("LastModificationTime")]
        public long LastModificationTime { get; set; }
    }
}
