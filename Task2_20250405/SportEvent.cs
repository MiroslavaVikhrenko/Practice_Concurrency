using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_20250405
{
    // спортивное мероприятие с указанием названия, даты, места проведения и других деталей.
    public class SportEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
    }
}
