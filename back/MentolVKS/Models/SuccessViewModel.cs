using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Models
{
    public class ResponseViewModel<T>
    {
        public int Result { get; set; }
        public T Data {get;set;}
        public string Message { get; set; }
    }
}
