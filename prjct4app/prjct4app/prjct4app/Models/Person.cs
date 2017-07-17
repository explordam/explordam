using SQLite.Net.Attributes;

namespace People.Models
{
    [Table("people")]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250), Unique]
        public string Name { get; set; }

        [MaxLength(250), Column("category")]
        public string category { get; set; }
    }
}