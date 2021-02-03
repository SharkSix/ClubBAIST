using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAIST.Domain
{
    public class StandingTeetimeRequest
    {
        public string ShareholderNumber { get; set; }
        public string MemberTwoNumber { get; set; }
        public string MemberThreeNumber { get; set; }
        public string MemberFourNumber { get; set; }
        public string TeeTime { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}
