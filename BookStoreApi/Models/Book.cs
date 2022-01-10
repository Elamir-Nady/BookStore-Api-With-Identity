using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApi.Models
{
    public class Book: BaseModel
    {
        public string Title { get; set; }
        public string Descryption { get; set; }
        public string ImageURL { get; set; }
        [ForeignKey("Auther")]
        public int AutherID { get; set; }
        public Auther Auther { get; set; }
    }
}
