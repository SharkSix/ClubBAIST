using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAIST.Domain
{
    public class DailyTeeTimeSheet
    {
        public string Date { get; set; }
        public string Teetime { get; set; }
        public string Phone { get; set; }
        public int NumberOfCarts { get; set; }
        public decimal Time { get; set; }
        public int NumberOfPlayer { get; set; }
        public string MemberName{ get; set; }
        public string Member1Number { get; set; }
        public string Member1Name { get; set; }

        public string Member2Number { get; set; }
        public string Member2Name { get; set; }

        public string Member3Number { get; set; }
        public string Member3Name { get; set; }

        public string Member4Number { get; set; }
        public string Member4Name { get; set; }
    }
}
