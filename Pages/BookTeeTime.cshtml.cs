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
    public class BookTeeTimeModel : PageModel
    {
        MainDerictor RequestDirector = new MainDerictor();

        [BindProperty]
        public string datepicker { get; set; }
        [BindProperty]
        public string MemberNumber { get; set; }
        [BindProperty]
        public string AList { get; set; }
        [BindProperty]
        public string Date { get; set; }
        [BindProperty]
        public string Member1Number { get; set; }
        [BindProperty]
        public string Member2Number { get; set; }
        [BindProperty]
        public string Member3Number { get; set; }
        [BindProperty]
        public string Member4Number { get; set; }
        [BindProperty]
        public string Phone { get; set; }
        [BindProperty]
        public int NumOfCarts { get; set; }
        [BindProperty]
        public decimal Time { get; set; }
        [BindProperty]
        public string DateFormMessage { get; set; }
        [BindProperty]
        public string TeeTimeFormMessage { get; set; }

        public string NoTeeTimeMsg;
        public string BookerMemberShipCode { get; set; }
        public string Member1Name { get; set; }

        List<string> GoldMember = new List<string>();
        List<string> SilverMember = new List<string>();
        List<string> BronzeMember = new List<string>();
        List<string> CopperMember = new List<string>();

        public string convertDate;
        public DayOfWeek DayOfTheWeek;
        public string Message { get; set; }

        public string TeeTimeMessage { get; set; }

        public List<DailyTeeTimeSheet> InitialDailyTeeTimeSheet { get; set; } = new List<DailyTeeTimeSheet>();
        public List<DailyTeeTimeSheet> DailyTeeSheet { get; set; } = new List<DailyTeeTimeSheet>();
        public List<TeeTime> AvailableTeeTimeList { get; set; } = new List<TeeTime>();
        public List<TeeTime> GoldLvLAvailableTeeTimeList { get; set; } = new List<TeeTime>();
        public List<TeeTime> SilverLvLAvailableTeeTimeList { get; set; } = new List<TeeTime>();
        public List<TeeTime> BronzeLvLAvailableTeeTimeList { get; set; } = new List<TeeTime>();
        public List<TeeTimeMember> TeeTimeMembers { get; set; } = new List<TeeTimeMember>();
        public List<TeeTime> BookedTeeTimeList { get; set; } = new List<TeeTime>();

        public List<Member> MemberList { get; set; } = new List<Member>();


        public void OnGet()
        {
            Message = "";
        }
        public bool ISMemberNumberQualified(string MemberNumber)
        {
            bool result = false;
            result = RequestDirector.ISMemberNumberQualified(MemberNumber);
            return result;
        }
        public void OnPostGetAvilableTeeTime()
        {
            InitialMembershipGroups();
            if (IsMemberExexist(MemberNumber) == true)
            {
                string MemberShip = CheckMembership();
                
                if (MemberShip == "Bronze")
                {
                    InitialBronzeLvLTeeTimeList();
                }
                else if (MemberShip == "Silver")
                {
                    InitialSilverLvLTeeTimeList();
                }
                else if (MemberShip == "Gold")
                {
                    InitialGoldLvLTeeTimeList();
                }

                else if (MemberShip == "Copper")
                {
                    InitialCopperLvLTeeTimeList();
                }

                DateFormMessage = "Welcome " + Member1Name;
                GetAvilableTeeTime();
                GetInitialDailyTeeTimeSheet();
            }
            else
            {
                DateFormMessage = "Member Number: " + MemberNumber + " does not exist";
            }

        }
        public string CheckMembership()
        {
            string memberhsip = "Copper";

            BookerMemberShipCode = RequestDirector.GetBookerMembershipCode(MemberNumber);

            if (BookerMemberShipCode == "C1")
            {
                NoTeeTimeMsg = "Copper Member can't Book a tee time";
                return memberhsip;
            }
            else 
            {
                foreach (string item in GoldMember)
                {
                    if (BookerMemberShipCode == item)
                    {
                        memberhsip = "Gold";
                    }
                }

                foreach (string item in SilverMember)
                {
                    if (BookerMemberShipCode == item)
                    {
                        memberhsip = "Silver";
                    }
                }

                foreach (string item in BronzeMember)
                {
                    if (BookerMemberShipCode == item)
                    {
                        memberhsip = "Bronze";
                    }
                }


                return memberhsip;
            }
        }
        public void OnPostAddTeeTime()
        {
            AddTeeTime();
        }
        public void InitialMembershipGroups()
        {
            GoldMember.Add("G1");
            GoldMember.Add("G2");
            GoldMember.Add("G3");
            SilverMember.Add("S1");
            SilverMember.Add("S2");
            BronzeMember.Add("B1");
            BronzeMember.Add("B2");
            BronzeMember.Add("B3");
            CopperMember.Add("C1");
        }
        public void InitialGoldLvLTeeTimeList()
        {
            convertDate = DateTime.ParseExact(datepicker, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            TeeTime InitialTeeTime;
            DateTime InitialTime = new DateTime(2020, 05, 01, 07, 00, 00);
            InitialTeeTime = new TeeTime();
            InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
            InitialTeeTime.Date = convertDate;
            GoldLvLAvailableTeeTimeList.Add(InitialTeeTime);

            for (int i = 1; i < 97; i++)
            {
                if (i % 2 > 0)
                {
                    InitialTeeTime = new TeeTime();
                    InitialTime = InitialTime.AddMinutes(7);
                    InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                    InitialTeeTime.Date = convertDate;
                    GoldLvLAvailableTeeTimeList.Add(InitialTeeTime);
                }
                else
                {
                    InitialTeeTime = new TeeTime();
                    InitialTime = InitialTime.AddMinutes(8);
                    InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                    InitialTeeTime.Date = convertDate;
                    GoldLvLAvailableTeeTimeList.Add(InitialTeeTime);
                }
            }
            AvailableTeeTimeList = GoldLvLAvailableTeeTimeList;
        }
        public void InitialSilverLvLTeeTimeList()
        {
            DayOfTheWeek = DateTime.ParseExact(datepicker, "yyyy-MM-dd", CultureInfo.InvariantCulture).DayOfWeek;
            if (DayOfTheWeek == DayOfWeek.Sunday || DayOfTheWeek == DayOfWeek.Saturday)
            {
                convertDate = DateTime.ParseExact(datepicker, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                TeeTime InitialTeeTime;
                DateTime InitialTime = new DateTime(2020, 05, 01, 11, 00, 00);
                InitialTeeTime = new TeeTime();
                InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                InitialTeeTime.Date = convertDate;
                SilverLvLAvailableTeeTimeList.Add(InitialTeeTime);

                for (int i = 1; i < 65; i++)
                {
                    if (i % 2 > 0)
                    {
                        InitialTeeTime = new TeeTime();
                        InitialTime = InitialTime.AddMinutes(7);
                        InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                        InitialTeeTime.Date = convertDate;
                        SilverLvLAvailableTeeTimeList.Add(InitialTeeTime);
                    }
                    else
                    {
                        InitialTeeTime = new TeeTime();
                        InitialTime = InitialTime.AddMinutes(8);
                        InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                        InitialTeeTime.Date = convertDate;
                        SilverLvLAvailableTeeTimeList.Add(InitialTeeTime);
                    }
                }
                AvailableTeeTimeList = SilverLvLAvailableTeeTimeList;
            }
            else
            {
                convertDate = DateTime.ParseExact(datepicker, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                TeeTime InitialTeeTime;
                DateTime InitialTime = new DateTime(2020, 05, 01, 07, 00, 00);
                InitialTeeTime = new TeeTime();
                InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                InitialTeeTime.Date = convertDate;
                SilverLvLAvailableTeeTimeList.Add(InitialTeeTime);

                for (int i = 1; i < 65; i++)
                {
                    if (i % 2 > 0)
                    {
                        InitialTeeTime = new TeeTime();
                        InitialTime = InitialTime.AddMinutes(7);
                        InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                        InitialTeeTime.Date = convertDate;
                        SilverLvLAvailableTeeTimeList.Add(InitialTeeTime);
                    }
                    else
                    {
                        InitialTeeTime = new TeeTime();
                        InitialTime = InitialTime.AddMinutes(8);
                        InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                        InitialTeeTime.Date = convertDate;
                        SilverLvLAvailableTeeTimeList.Add(InitialTeeTime);
                    }
                }

                InitialTime = new DateTime(2020, 05, 01, 17, 30, 00);
                AvailableTeeTimeList = SilverLvLAvailableTeeTimeList;
                InitialTeeTime = new TeeTime();
                InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                InitialTeeTime.Date = convertDate;
                SilverLvLAvailableTeeTimeList.Add(InitialTeeTime);
                for (int i = 1; i < 13; i++)
                {
                    if (i % 2 > 0)
                    {
                        InitialTeeTime = new TeeTime();
                        InitialTime = InitialTime.AddMinutes(7);
                        InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                        InitialTeeTime.Date = convertDate;
                        SilverLvLAvailableTeeTimeList.Add(InitialTeeTime);
                    }
                    else
                    {
                        InitialTeeTime = new TeeTime();
                        InitialTime = InitialTime.AddMinutes(8);
                        InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                        InitialTeeTime.Date = convertDate;
                        SilverLvLAvailableTeeTimeList.Add(InitialTeeTime);
                    }
                }


                AvailableTeeTimeList = SilverLvLAvailableTeeTimeList;
            }
            
        }
        public void InitialBronzeLvLTeeTimeList()
        {
            DayOfTheWeek = DateTime.ParseExact(datepicker, "yyyy-MM-dd", CultureInfo.InvariantCulture).DayOfWeek;
            if (DayOfTheWeek == DayOfWeek.Sunday || DayOfTheWeek == DayOfWeek.Saturday)
            {
                convertDate = DateTime.ParseExact(datepicker, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                TeeTime InitialTeeTime;
                DateTime InitialTime = new DateTime(2020, 05, 01, 13, 00, 00);
                InitialTeeTime = new TeeTime();
                InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                InitialTeeTime.Date = convertDate;
                BronzeLvLAvailableTeeTimeList.Add(InitialTeeTime);

                for (int i = 1; i < 49; i++)
                {
                    if (i % 2 > 0)
                    {
                        InitialTeeTime = new TeeTime();
                        InitialTime = InitialTime.AddMinutes(7);
                        InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                        InitialTeeTime.Date = convertDate;
                        BronzeLvLAvailableTeeTimeList.Add(InitialTeeTime);
                    }
                    else
                    {
                        InitialTeeTime = new TeeTime();
                        InitialTime = InitialTime.AddMinutes(8);
                        InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                        InitialTeeTime.Date = convertDate;
                        BronzeLvLAvailableTeeTimeList.Add(InitialTeeTime);
                    }
                }
                AvailableTeeTimeList = BronzeLvLAvailableTeeTimeList;
            }
            else
            {
                convertDate = DateTime.ParseExact(datepicker, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                TeeTime InitialTeeTime;
                DateTime InitialTime = new DateTime(2020, 05, 01, 07, 00, 00);
                InitialTeeTime = new TeeTime();
                InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                InitialTeeTime.Date = convertDate;
                BronzeLvLAvailableTeeTimeList.Add(InitialTeeTime);

                for (int i = 1; i < 65; i++)
                {
                    if (i % 2 > 0)
                    {
                        InitialTeeTime = new TeeTime();
                        InitialTime = InitialTime.AddMinutes(7);
                        InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                        InitialTeeTime.Date = convertDate;
                        BronzeLvLAvailableTeeTimeList.Add(InitialTeeTime);
                    }
                    else
                    {
                        InitialTeeTime = new TeeTime();
                        InitialTime = InitialTime.AddMinutes(8);
                        InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                        InitialTeeTime.Date = convertDate;
                        BronzeLvLAvailableTeeTimeList.Add(InitialTeeTime);
                    }
                }

                InitialTime = new DateTime(2020, 05, 01, 06, 00, 00);
                InitialTeeTime = new TeeTime();
                InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                InitialTeeTime.Date = convertDate;
                BronzeLvLAvailableTeeTimeList.Add(InitialTeeTime);
                for (int i = 1; i < 9; i++)
                {
                    if (i % 2 > 0)
                    {
                        InitialTeeTime = new TeeTime();
                        InitialTime = InitialTime.AddMinutes(7);
                        InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                        InitialTeeTime.Date = convertDate;
                        BronzeLvLAvailableTeeTimeList.Add(InitialTeeTime);
                    }
                    else
                    {
                        InitialTeeTime = new TeeTime();
                        InitialTime = InitialTime.AddMinutes(8);
                        InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                        InitialTeeTime.Date = convertDate;
                        BronzeLvLAvailableTeeTimeList.Add(InitialTeeTime);
                    }
                }


                AvailableTeeTimeList = BronzeLvLAvailableTeeTimeList;
            }
        }
        public void InitialCopperLvLTeeTimeList()
        {
            convertDate = DateTime.ParseExact(datepicker, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            TeeTime InitialTeeTime;
            InitialTeeTime = new TeeTime();
            InitialTeeTime.Teetime = "";
            InitialTeeTime.Date = convertDate;
            GoldLvLAvailableTeeTimeList.Add(InitialTeeTime);
        }
        public bool IsMemberExexist(string CheckMember)
        {

            bool result = false;
            result = RequestDirector.IsMemberExexist(MemberNumber);
            return result;
        }
        public void GetAvilableTeeTime()
        {
            AvailableTeeTimeList = RequestDirector.GetAvilableTeeTime(AvailableTeeTimeList, convertDate);
        }
        public void GetInitialDailyTeeTimeSheet()
        {
            DailyTeeTimeSheet InitialTeeTime;
            DateTime InitialTime = new DateTime(2020, 05, 01, 07, 00, 00);
            InitialTeeTime = new DailyTeeTimeSheet();
            InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
            InitialDailyTeeTimeSheet.Add(InitialTeeTime);

            for (int i = 1; i < 97; i++)
            {
                if (i % 2 > 0)
                {
                    InitialTeeTime = new DailyTeeTimeSheet();
                    InitialTime = InitialTime.AddMinutes(7);
                    InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                    InitialDailyTeeTimeSheet.Add(InitialTeeTime);
                }
                else
                {
                    InitialTeeTime = new DailyTeeTimeSheet();
                    InitialTime = InitialTime.AddMinutes(8);
                    InitialTeeTime.Teetime = InitialTime.ToString("HH:mm");
                    InitialDailyTeeTimeSheet.Add(InitialTeeTime);
                }
            }
            DailyTeeSheet = RequestDirector.GetDailyTeeTimeSheet(InitialDailyTeeTimeSheet, convertDate);
        }
        public void AddTeeTime()
        {
            bool AllMemberQualified = true;

                DailyTeeTimeSheet DedesireTeeTime = new DailyTeeTimeSheet();
                DedesireTeeTime.Date = Date;
                DedesireTeeTime.Teetime = AList;
                DedesireTeeTime.Member1Number = Member1Number;
                DedesireTeeTime.Member2Number = Member2Number;
                DedesireTeeTime.Member3Number = Member3Number;
                DedesireTeeTime.Member4Number = Member4Number;
                DedesireTeeTime.Phone = Phone;
                DedesireTeeTime.NumberOfCarts = NumOfCarts;
                DedesireTeeTime.Time = Time;

                List<TeeTimeMember> MemberList = new List<TeeTimeMember>();
                TeeTimeMember member = new TeeTimeMember();
                member.Date = DedesireTeeTime.Date;
                member.Teetime = DedesireTeeTime.Teetime;
                member.MemberNumber = DedesireTeeTime.Member1Number;
                MemberList.Add(member);

                if (Member2Number != null)
                {
                    member = new TeeTimeMember();
                    member.Date = DedesireTeeTime.Date;
                    member.Teetime = DedesireTeeTime.Teetime;
                    member.MemberNumber = DedesireTeeTime.Member2Number;
                    MemberList.Add(member);
                }
                if (Member3Number != null)
                {
                    member = new TeeTimeMember();
                    member.Date = DedesireTeeTime.Date;
                    member.Teetime = DedesireTeeTime.Teetime;
                    member.MemberNumber = DedesireTeeTime.Member3Number;
                    MemberList.Add(member);
                }
                if (Member4Number != null)
                {
                    member = new TeeTimeMember();
                    member.Date = DedesireTeeTime.Date;
                    member.Teetime = DedesireTeeTime.Teetime;
                    member.MemberNumber = DedesireTeeTime.Member4Number;
                    MemberList.Add(member);
                }


                foreach (TeeTimeMember member1 in MemberList)
                {
                    if (ISMemberNumberQualified(member1.MemberNumber) == false)
                    {
                        Message = "Number Number: " + member1.MemberNumber + " Not exist or not qualified; " + Message;
                        AllMemberQualified = false;
                    }
                }

                if (AllMemberQualified == true)
                {
                     bool Confirmation = RequestDirector.AddTeeTime(DedesireTeeTime, MemberList);
                     if (Confirmation == true)
                     {
                       Message = "Successfully Booked TeeTime on " + Date + " at " + AList;
                     }
                     else
                     {
                       Message = "Booked TeeTime: Failed";
                     }
                }  
        }
    }
}
