using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQLab02_Part03
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int PublisherId { get; set; }
        public int SubjectId { get; set; }
    }
}