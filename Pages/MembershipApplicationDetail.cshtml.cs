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
    public class MembershipApplicationDetailModel : PageModel
    {
        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string PostCode { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public string AlternatePhone { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string DateOfBirth { get; set; }

        [BindProperty]
        public string Occupation { get; set; }

        [BindProperty]
        public string CompanyName { get; set; }

        [BindProperty]
        public string CompanyAddress { get; set; }

        [BindProperty]
        public string CompanyPhone { get; set; }

        [BindProperty]
        public string CompanyPostCode { get; set; }

        public string Date { get; set; }

        [BindProperty]
        public string Shareholder1Number { get; set; }

        [BindProperty]
        public string Shareholder2Number { get; set; }

        [BindProperty]
        public string Shareholder1SignDate { get; set; }

        [BindProperty]
        public string Shareholder2SignDate { get; set; }

        [BindProperty]
        public string Status { get; set; }
        public MembershipApplication Application { get; set; } = new MembershipApplication();
        MainDerictor RequestDirector = new MainDerictor();
        public void OnGet(string id)
        {
            string[] sArray = id.Split(",");
            FirstName = sArray[0];
            LastName = sArray[1];

            Application = RequestDirector.GetMembershipAppliactionByName(FirstName, LastName);
        }
    }
}