using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using ClubBAIST.Domain;
using System.Text;
using System.Globalization;
using System.ComponentModel.DataAnnotations;


namespace ClubBAIST.Pages
{

    public class ReviewMonthlyHandicapReportModel : PageModel
    {
        [BindProperty,Required]
        public DateTime Time { get; set; }

        [TempData]
        public string Message {get;set;}

        MainDerictor RequestDirector = new MainDerictor();

        public List<HandicapReport> MonthlyHandicapReport = new List<HandicapReport>();
        public void OnGet()
        {
            Message = "";
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                MonthlyHandicapReport = RequestDirector.GetHandicapReport(Time);
                if (MonthlyHandicapReport != null)
                {
                    foreach (HandicapReport handicapReport in MonthlyHandicapReport)
                    {
                        if (handicapReport.Last20Rounds != null)
                        {
                            handicapReport.Last20Rounds = handicapReport.Last20Rounds.Take(20).ToList();
                            double avg = 0.00;
                            for (int i = 0; i < handicapReport.Last20Rounds.Count; i++)
                            {
                                avg += handicapReport.Last20Rounds[i];
                            }
                            handicapReport.Average = DropToOneDecimal(avg / handicapReport.Last20Rounds.Count);
                            List<double> BestTen = handicapReport.Last20Rounds;
                            BestTen.Sort();
                            BestTen = BestTen.Take(10).ToList();
                            for (int i = 0; i < BestTen.Count(); i++)
                            {
                                handicapReport.Best10Average += BestTen[i];
                            }
                            handicapReport.Best10Average = DropToOneDecimal(handicapReport.Best10Average / BestTen.Count());
                            handicapReport.Handicap = DropToOneDecimal(handicapReport.Best10Average * 0.96);
                        }
                    }
                    Message = "Record loaded";
                }
                else 
                {
                    Message = "No record finded";
                }

            }
        }
        public double DropToOneDecimal(double num)
        {
            string number = num.ToString();
            number = number.Substring(0, number.IndexOf(".", 0) + 2);
            num = double.Parse(number);
            return num;
        }
    }
}