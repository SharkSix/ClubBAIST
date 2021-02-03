using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAIST.Domain
{
    public class HandicapReport
    {
        public DateTime MonthTime { get; set; }
        public string MemberName { get; set; }
        public string MemberNumber { get; set; }
        public double Handicap { get; set; }
        public double Average { get; set; }
        public double Best10Average { get; set; }
        public List<double> Last20Rounds { get; set; }

    }
}
