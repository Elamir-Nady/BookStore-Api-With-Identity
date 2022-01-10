using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.ViewModels
{
    public class ResultDto
    {
        public bool ISuccessed { get; set; } = true;
        public string Message { get; set; }
        public object Data { get; set; }

      
    }
}
