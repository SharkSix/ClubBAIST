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
    public class RecordPlayerScoreManModel : PageModel
    {
        MainDerictor RequestDirector = new MainDerictor();
        [BindProperty,Required]
        public string Date { get; set; }
        [BindProperty, Required]
        public string TeeTime { get; set; }
        [BindProperty, Required]
        public string Teetime { get; set; }
        [BindProperty, Required]
        public string MemberNumber { get; set; }
        [BindProperty, Required]
        public string Course { get; set; }
        [BindProperty, Required]
        public double CourseRating { get; set; }
        [BindProperty, Required]
        public double CourseSlope { get; set; }
        [BindProperty, Required]
        public int Hole1 { get; set; }
        [BindProperty, Required]
        public int Hole2 { get; set; }
        [BindProperty, Required]
        public int Hole3 { get; set; }
        [BindProperty, Required]
        public int Hole4 { get; set; }
        [BindProperty, Required]
        public int Hole5 { get; set; }
        [BindProperty, Required]
        public int Hole6 { get; set; }
        [BindProperty, Required]
        public int Hole7 { get; set; }
        [BindProperty, Required]
        public int Hole8 { get; set; }
        [BindProperty, Required]
        public int Hole9 { get; set; }
        [BindProperty, Required]
        public int Hole10 { get; set; }
        [BindProperty, Required]
        public int Hole11 { get; set; }
        [BindProperty, Required]
        public int Hole12 { get; set; }
        [BindProperty, Required]
        public int Hole13 { get; set; }
        [BindProperty, Required]
        public int Hole14 { get; set; }
        [BindProperty, Required]
        public int Hole15 { get; set; }
        [BindProperty, Required]
        public int Hole16 { get; set; }
        [BindProperty, Required]
        public int Hole17 { get; set; }
        [BindProperty, Required]
        public int Hole18 { get; set; }
        [BindProperty, Required]
        public int Out { get; set; }
        [BindProperty, Required]
        public int In { get; set; }
        [BindProperty, Required]
        public int Total { get; set; }

        [TempData]
        public string Message { get; set; }

        public List<string> TableHeading = new List<string>();
        public List<string> Par = new List<string>();
        public List<string> White = new List<string>();
        public List<string> Bule = new List<string>();
        public List<string> Handicap = new List<string>();
        public PlayerScore playerScore = new PlayerScore();
        public void OnGet()
        {
            Message = "";
            InitializeTable();
        }
        public void OnPost()
        {
            InitializeTable();
            if (ModelState.IsValid)
            {
                bool result = false;
                playerScore.Course = Course;
                playerScore.Rating = CourseRating;
                playerScore.Slope = CourseSlope;
                playerScore.Date = DateTime.ParseExact(Date, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture); ;
                playerScore.Teetime = Teetime;
                playerScore.MemberNumber = MemberNumber;
                playerScore.Hole1 = Hole1;
                playerScore.Hole2 = Hole2;
                playerScore.Hole3 = Hole3;
                playerScore.Hole4 = Hole4;
                playerScore.Hole5 = Hole5;
                playerScore.Hole6 = Hole6;
                playerScore.Hole7 = Hole7;
                playerScore.Hole8 = Hole8;
                playerScore.Hole9 = Hole9;
                playerScore.Hole10 = Hole10;
                playerScore.Hole11 = Hole11;
                playerScore.Hole12 = Hole12;
                playerScore.Hole13 = Hole13;
                playerScore.Hole14 = Hole14;
                playerScore.Hole15 = Hole15;
                playerScore.Hole16 = Hole16;
                playerScore.Hole17 = Hole17;
                playerScore.Hole18 = Hole18;
                double a = (Total - CourseRating) * 113 / CourseSlope;
                string num = a.ToString();
                num = num.Substring(0, num.IndexOf(".", 0) + 2);
                playerScore.HandicapDifferential = double.Parse(num);

                result = RequestDirector.AddPlayerScore(playerScore);
                if (result == true)
                {
                    Message = "Player score is Recroded";
                }
                else
                {
                    Message = "Player score failed to recrod";
                }
            }
            else 
            {
                Message = "All Fields are required";
            }
        }


        public void InitializeTable()
        {
            TableHeading.Add("Hole");
            TableHeading.Add("1");
            TableHeading.Add("2");
            TableHeading.Add("3");
            TableHeading.Add("4");
            TableHeading.Add("5");
            TableHeading.Add("6");
            TableHeading.Add("7");
            TableHeading.Add("8");
            TableHeading.Add("9");
            TableHeading.Add("Out");
            TableHeading.Add("10");
            TableHeading.Add("11");
            TableHeading.Add("12");
            TableHeading.Add("13");
            TableHeading.Add("14");
            TableHeading.Add("15");
            TableHeading.Add("16");
            TableHeading.Add("17");
            TableHeading.Add("18");
            TableHeading.Add("In");
            TableHeading.Add("Total");
            Par.Add("Par");
            Par.Add("4");
            Par.Add("5");
            Par.Add("3");
            Par.Add("4");
            Par.Add("4");
            Par.Add("4");
            Par.Add("3");
            Par.Add("5");
            Par.Add("4");
            Par.Add("36");
            Par.Add("4");
            Par.Add("4");
            Par.Add("3");
            Par.Add("5");
            Par.Add("4");
            Par.Add("4");
            Par.Add("3");
            Par.Add("4");
            Par.Add("4");
            Par.Add("35");
            Par.Add("71");
            White.Add("White");
            White.Add("417");
            White.Add("492");
            White.Add("117");
            White.Add("288");
            White.Add("363");
            White.Add("369");
            White.Add("221");
            White.Add("492");
            White.Add("335");
            White.Add("3094");
            White.Add("349");
            White.Add("356");
            White.Add("197");
            White.Add("491");
            White.Add("390");
            White.Add("323");
            White.Add("132");
            White.Add("402");
            White.Add("365");
            White.Add("3005");
            White.Add("6099");
            Bule.Add("Bule");
            Bule.Add("430");
            Bule.Add("505");
            Bule.Add("144");
            Bule.Add("293");
            Bule.Add("380");
            Bule.Add("386");
            Bule.Add("235");
            Bule.Add("515");
            Bule.Add("361");
            Bule.Add("3249");
            Bule.Add("378");
            Bule.Add("382");
            Bule.Add("214");
            Bule.Add("511");
            Bule.Add("402");
            Bule.Add("337");
            Bule.Add("138");
            Bule.Add("415");
            Bule.Add("381");
            Bule.Add("3158");
            Bule.Add("6407");
            Handicap.Add("Man's Handicap");
            Handicap.Add("1");
            Handicap.Add("5");
            Handicap.Add("17");
            Handicap.Add("11");
            Handicap.Add("9");
            Handicap.Add("7");
            Handicap.Add("15");
            Handicap.Add("3");
            Handicap.Add("13");
            Handicap.Add("-");
            Handicap.Add("12");
            Handicap.Add("6");
            Handicap.Add("16");
            Handicap.Add("2");
            Handicap.Add("10");
            Handicap.Add("14");
            Handicap.Add("18");
            Handicap.Add("4");
            Handicap.Add("8");
            Handicap.Add("-");
            Handicap.Add("-");


        }
    }
}