using System;
using Smoothie.Domain.Enums;

namespace Smoothie.Domain.Entities
{
    public class Smoothie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public SmoothieStatus Status { get; set; }
        public int UserId { get; set; }
    }
}
