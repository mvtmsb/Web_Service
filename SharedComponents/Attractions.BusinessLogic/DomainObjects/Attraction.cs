using System;
using System.Collections.Generic;
using System.Text;

namespace Attractions.DomainObjects
{
  public class Attraction : DomainObject
    {
    
        public string NameObject { get; set; }
        public string Location { get; set; }
        public string Admission { get; set; }
        public string PeriodA { get; set; }

    }
}
