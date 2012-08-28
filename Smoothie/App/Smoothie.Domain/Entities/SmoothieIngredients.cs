using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoothie.Domain.Entities
{
    public class SmoothieIngredients
    {
        public int Id { get; set;}
        public string NDB_No { get; set; }
        public int Quantity { get; set; }
    }
}
