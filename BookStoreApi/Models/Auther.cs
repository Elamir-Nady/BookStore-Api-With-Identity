using Models;
using System.Collections.Generic;

namespace BookStoreApi.Models
{
    public class Auther: BaseModel
    {
        public string FullName { get; set; }
        public virtual IList<Book> Books { get; set; }

    }
}
