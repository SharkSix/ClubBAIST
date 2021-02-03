using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAIST.TechnicalService;
namespace ClubBAIST.Domain
{
    public class MainDerictor
    {
        public bool AddTeeTime(DailyTeeTimeSheet DedesireTeeTime, List<TeeTimeMember> Members)
        {
            bool confirmation;
            TeeTimes TeeTImeManager = new TeeTimes();
            confirmation = TeeTImeManager.AddTeeTime(DedesireTeeTime,Members);
            return confirmation;
        }

        public List<TeeTime> GetAvilableTeeTime(List<TeeTime> AvailableTeeTimeList, string Date)
        {
            TeeTimes TeeTImeManager = new TeeTimes();
            List<TeeTime> AvailableTeeTime = TeeTImeManager.GetAvailableTeeTimeList(AvailableTeeTimeList, Date);
            return AvailableTeeTime;
        }
        public List<DailyTeeTimeSheet> GetDailyTeeTimeSheet(List<DailyTeeTimeSheet> InitialDailyTeeTimeSheet, string Date)
        {
            TeeTimes TeeTImeManager = new TeeTimes();
            List<DailyTeeTimeSheet> DailyTeeSheet = TeeTImeManager.GetDailyTeeTimeSheet(InitialDailyTeeTimeSheet, Date);
            return DailyTeeSheet;
        }

        public bool IsMemberExexist(string MemberNumber)
        {
            Members MemberManager = new Members();
            bool confirmation;
            confirmation = MemberManager.IsMemberExexist(MemberNumber);
            return confirmation;
        }

        public string GetBookerMembershipCode(string MemberNumber)
        {
            Members MemberManager = new Members();
            string MembershipCode;
            MembershipCode = MemberManager.GetBookerMembershipCode(MemberNumber);
            return MembershipCode;
        }
        public bool ISMemberNumberQualified(string MemberNumber)
        {
            Members MemberManager = new Members();
            bool confirmation;
            confirmation = MemberManager.ISMemberNumberQualified(MemberNumber);
            return confirmation;
        }
        public bool CheckShareholder(string MemberNumber)
        {
            Members MemberManager = new Members();
            bool confirmation;
            confirmation = MemberManager.CheckShareholder(MemberNumber);
            return confirmation;
        }
        public bool IsShareholdHadRequest(string MemberNumber,string StartDate)
        {
            StandingTeeTImes StandingTeeTImeManager = new StandingTeeTImes();
            bool confirmation;
            confirmation = StandingTeeTImeManager.IsShareholdHadRequest(MemberNumber, StartDate);
            return confirmation;
        }
        public bool AddStandingTeeTimeRequest(StandingTeetimeRequest DesireStandingTeetimeRequest)
        {
            StandingTeeTImes StandingTeeTImeManager = new StandingTeeTImes();
            bool confirmation;
            confirmation = StandingTeeTImeManager.AddStandingTeeTimeRequest(DesireStandingTeetimeRequest);
            return confirmation;
        }

        public DailyTeeTimeSheet GetBookedTeeTime(string MemberNumber, string Date, string TeeTime)
        {
            TeeTimes TeeTImeManager = new TeeTimes();
            DailyTeeTimeSheet BookedTeeTime = TeeTImeManager.GetBookedTeeTime(MemberNumber, Date,TeeTime);
            return BookedTeeTime;
        }

        public bool UpdateTeeTime(DailyTeeTimeSheet ModifyTeeTime, List<TeeTimeMember> Members)
        {
            bool confirmation = false;
            TeeTimes TeeTImeManager = new TeeTimes();
            confirmation = TeeTImeManager.UpdateTeeTime(ModifyTeeTime, Members);
            return confirmation;
        }

        public bool DeleteTeeTime(string MemberNumber, string Date, string TeeTime)
        {
            bool confirmation = false;
            TeeTimes TeeTImeManager = new TeeTimes();
            confirmation = TeeTImeManager.DeleteTeeTime(MemberNumber, Date, TeeTime);
            return confirmation;
        }

        public bool CancleStandingTeeTime(string MemberNumber, string StartDate)
        {
            bool confirmation = false;
            StandingTeeTImes TeeTImeManager = new StandingTeeTImes();
            confirmation = TeeTImeManager.CancleStandingTeeTime(MemberNumber, StartDate);
            return confirmation;
        }

        public bool AddMembershipApplication(MembershipApplication NewApplication)
        {
            MembershipApplications MembershipApplicationManager = new MembershipApplications();
            bool confirmation;
            confirmation = MembershipApplicationManager.AddMembershipApplication(NewApplication);
            return confirmation;
        }

        public List<MembershipApplication> GetAllMembershipAppliactions()
        {
            MembershipApplications MembershipApplicationManager = new MembershipApplications();
            List<MembershipApplication> ApplicationList = new List<MembershipApplication>();
            ApplicationList = MembershipApplicationManager.GetAllMembershipAppliactions();
            return ApplicationList;
        }

        public MembershipApplication GetMembershipAppliactionByName(string FirstName,string LastName)
        {
            MembershipApplications MembershipApplicationManager = new MembershipApplications();
            MembershipApplication Application = new MembershipApplication();
            Application = MembershipApplicationManager.GetMembershipAppliactionByName(FirstName,LastName);
            return Application;
        }

        public List<Member> GetAllMember()
        {
            Members MemberManager = new Members();
            List<Member> Members = new List<Member>();
            Members = MemberManager.GetAllMember();
            return Members;
        }

        public bool AddMember(Member NewMember)
        {
            Members MemberManager = new Members();
            bool confirmation;
            confirmation = MemberManager.AddMember(NewMember);
            return confirmation;
        }

        public Member GetMemberByNumber(string MemberNumber)
        {
            Members MemberManager = new Members();
            Member member = new Member();
            member = MemberManager.GetMemberByNumber(MemberNumber);
            return member;
        }

        public bool AddPlayerScore(PlayerScore NewPlayerScore)
        {
            PlayerScores PlayerScoreManager = new PlayerScores();
            bool confirmation;
            confirmation = PlayerScoreManager.AddPlayerScore(NewPlayerScore);
            return confirmation;
        }

        public List<HandicapReport> GetHandicapReport(DateTime Time)
        {
            PlayerScores PlayerScoreManager = new PlayerScores();
            List<HandicapReport> handicapReport = new List<HandicapReport>();
            handicapReport = PlayerScoreManager.GetHandicapReport(Time);
            return handicapReport;
        }
    }
}
