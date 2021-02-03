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
    public class CancelStandingTeeTimeModel : PageModel
    {
        MainDerictor RequestDirector = new MainDerictor();

        [BindProperty, Required, StringLength(7)]
        public string ShareholderNumber { get; set; }

        [BindProperty, Required, StringLength(10)]
        public string StartDate { get; set; }

        [TempData]
        public string Message { get; set; }

        public string convertStartDate;
        public string convertEndDate;

        public List<TeeTime> InitialTeeTimes { get; set; } = new List<TeeTime>();

        public StandingTeetimeRequest DesireStandingTeetimeRequest { get; set; } = new StandingTeetimeRequest();
        public void OnGet()
        {
            Message = "";
        }
        public void OnPost()
        {
            
            bool result = false;
            if (ModelState.IsValid)
            {
                StartDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                bool AllMemberQualified = true;
                if (CheckShareholder(ShareholderNumber) == false)
                {
                    Message = "Member Number: " + ShareholderNumber + " does not exist or is not a Shareholder";
                    AllMemberQualified = false;
                }
               
                else
                {
                    AllMemberQualified = true;
                }

                if (AllMemberQualified == true)
                {
                    if (RequestDirector.IsShareholdHadRequest(ShareholderNumber,StartDate) == false)
                    {
                        Message = "Shareholder does not have standing TeeTime on this start date";
                    }
                    else
                    {
                       
                        result = RequestDirector.CancleStandingTeeTime(ShareholderNumber, StartDate);
                        if (result == true)
                        {
                            Message = "Standing Tee Time request on " + StartDate + "for " + ShareholderNumber + " is canceled";
                        }
                        else
                        {
                            Message = "Cancel Standing Tee Time failed";
                        }
                    }
                }
            }

        }
        public bool CheckShareholder(string MemberNumber)
        {
            bool result = false;
            result = RequestDirector.CheckShareholder(MemberNumber);
            return result;
        }
    }
}