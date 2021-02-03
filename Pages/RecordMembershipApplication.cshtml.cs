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
    public class RecordMembershipApplicationModel : PageModel
    {
        MainDerictor RequestDirector = new MainDerictor();

        [BindProperty, Required, StringLength(25)]
        public string LastName { get; set; }

        [BindProperty, Required, StringLength(25)]
        public string FirstName { get; set; }

        [BindProperty, Required, StringLength(200)]
        public string Address { get; set; }

        [BindProperty, Required, StringLength(6)]
        public string PostCode { get; set; }

        [BindProperty, Required, StringLength(11)]
        public string Phone { get; set; }

        [BindProperty, StringLength(11)]
        public string AlternatePhone { get; set; }

        [BindProperty, Required, StringLength(50)]
        public string Email { get; set; }

        [BindProperty, Required, StringLength(10)]
        public string DateOfBirth { get; set; }

        [BindProperty, Required, StringLength(50)]
        public string Occupation { get; set; }

        [BindProperty, Required, StringLength(100)]
        public string CompanyName { get; set; }

        [BindProperty, Required, StringLength(200)]
        public string CompanyAddress { get; set; }

        [BindProperty, Required, StringLength(100)]
        public string CompanyPhone { get; set; }

        [BindProperty, Required, StringLength(6)]
        public string CompanyPostCode { get; set; }

        public string Date { get; set; }

        [BindProperty, Required, StringLength(7)]
        public string Shareholder1Number { get; set; }

        [BindProperty, Required, StringLength(7)]
        public string Shareholder2Number { get; set; }

        [BindProperty, Required]
        public string Shareholder1SignDate { get; set; }

        [BindProperty, Required]
        public string Shareholder2SignDate { get; set; }

        [BindProperty, Required]
        public string Status { get; set; }

        [TempData]
        public string Message { get; set; }

        public MembershipApplication NewApplication { get; set; } = new MembershipApplication();

        public void OnGet()
        {
            Message = "";
        }

        public void OnPost()
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                NewApplication.LastName = LastName;
                NewApplication.FirstName = FirstName;
                NewApplication.Address = Address;
                NewApplication.PostalCode = PostCode;
                NewApplication.Phone = Phone;
                NewApplication.AlternatePhone = AlternatePhone;
                NewApplication.Email = Email;
                NewApplication.DateOfBirth = DateOfBirth;
                NewApplication.Occupation = Occupation;
                NewApplication.CompanyName = CompanyName;
                NewApplication.CompanyAddress = CompanyAddress;
                NewApplication.CompanyPostalCode = CompanyPostCode;
                NewApplication.CompanyPhone = CompanyPhone;
                NewApplication.Date = DateTime.Now.ToString("MM/dd/yyyy");
                NewApplication.ShareholderOneNumber = Shareholder1Number;
                NewApplication.ShareholderOneSignDate = DateTime.ParseExact(Shareholder1SignDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                NewApplication.ShareholderTwoNumber = Shareholder2Number;
                NewApplication.ShareholderTwoSignDate = Shareholder2SignDate = DateTime.ParseExact(Shareholder2SignDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                NewApplication.Status = Status;

                result = RequestDirector.AddMembershipApplication(NewApplication);
                if (result == true)
                {
                    Message = "Membership appliaction Recroded";
                }
                else
                {
                    Message = "Membership appliaction failed to record";
                }
            }
        }
    }
}