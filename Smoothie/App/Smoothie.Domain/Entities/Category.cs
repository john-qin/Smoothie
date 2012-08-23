using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoothie.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public int ReOrder { get; set; }
    }
}
