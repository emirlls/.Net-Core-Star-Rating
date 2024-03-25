using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Degerlendirme.Model
{
    public class Rating
    {

        //[Column("Id")]
        //public int Id { get; set; }

        [Key]
        [Column("StarId")]
        public string StarId { get; set; } = "";

        [Column("VoteCount")]
        public int VoteCount { get; set; }


    }
}

