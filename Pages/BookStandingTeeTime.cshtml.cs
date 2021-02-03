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
    public class BookStandingTeeTimeModel : PageModel
    {
        MainDerictor RequestDirector = new MainDerictor();

        [BindProperty, Required, StringLength(7)]
        public string ShareholderNumber { get; set; }

        [BindProperty, Required, StringLength(7)]
        public string Member2Number { get; set; }

        [BindProperty, Required, StringLength(7)]
        public string Member3Number { get; set; }

        [BindProperty, Required, StringLength(7)]
        public string Member4Number { get; set; }

        [BindProperty, Required, StringLength(5)]
        public string TeeTime { get; set; }
        [BindProperty, Required, StringLength(10)]
        public string StartDate { get; set; }
        [BindProperty, Required, StringLength(10)]
        public string EndDate { get; set; }
        [TempData]
        public string Message { get; set; }

        public string convertStartDate;
        public string convertEndDate;

        public List<TeeTime> InitialTeeTimes { get; set; } = new List<TeeTime>();

        public StandingTeetimeRequest DesireStandingTeetimeRequest { get; set; } = new StandingTeetimeRequest();
        public void OnGet()
        {
            Message = "";
            InitialTeeTimeList();
        }
        public void OnPost()
        {
            bool result = false;
            InitialTeeTimeList();
            if (ModelState.IsValid)
            {
                StartDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                EndDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                bool AllMemberQualified = true;
                if (CheckShareholder(ShareholderNumber) == false)
                {
                    Message = "Member Number: " + ShareholderNumber + " does not exist or is not a Shareholder";
                    AllMemberQualified = false;
                }
                else if ((RequestDirector.ISMemberNumberQualified(Member2Number) == false))
                {
                    Message = "Member Number: " + Member2Number + " does not exist or is not Qualified";
                    AllMemberQualified = false;
                }
                else if ((RequestDirector.ISMemberNumberQualified(Member3Number) == false))
                {
                    Message = "Member Number: " + Member3Number + " does not exist or is not Qualified";
                    AllMemberQualified = false;
                }
                else if ((RequestDirector.ISMemberNumberQualified(Member4Number) == false))
                {
                    Message = "Member Number: " + Member4Number + " does not exist or is not Qualified";
                    AllMemberQualified = false;
                }
                else
                {
                    AllMemberQualified = true;
                }

                if (AllMemberQualified == true)
                {
                    if (RequestDirector.IsShareholdHadRequest(ShareholderNumber, StartDate) == true)
                    {
                        Message = "Shareholder has request for this week already";
                    }
                    else
                    {
                        DesireStandingTeetimeRequest.ShareholderNumber = ShareholderNumber;
                        DesireStandingTeetimeRequest.MemberTwoNumber = Member2Number;
                        DesireStandingTeetimeRequest.MemberThreeNumber = Member3Number;
                        DesireStandingTeetimeRequest.MemberFourNumber = Member4Number;
                        DesireStandingTeetimeRequest.TeeTime = TeeTime;
                        DesireStandingTeetimeRequest.StartDate = StartDate;
                        DesireStandingTeetimeRequest.EndDate = EndDate;

                        result = RequestDirector.AddStandingTeeTimeRequest(DesireStandingTeetimeRequest);
                        if (result == true)
                        {
                            Message = "Standing Tee Time request Recived";
                        }
                        else
                        {
                            Message = "Standing Tee Time request failed";
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
        public void InitialTeeTimeList()
        {
            TeeTime InitialTeeTime;
            InitialTeeTime = new TeeTime();
            DateTime InitialTime = new DateTime(2020, 05, 01, 07, 00, 00);

            for (int i = 1; i < 24; i++)
            {
                    InitialTeeTime = new TeeTime();
                    InitialTime = InitialTime.AddMinutes(30);
                    InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                    InitialTeeTimes.Add(InitialTeeTime);
            }
        }
    }
}
