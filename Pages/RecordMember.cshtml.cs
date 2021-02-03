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
    public class RecordMemberModel : PageModel
    {
        [BindProperty, Required, StringLength(7)]
        public string MemberNumber { get; set; }
        [BindProperty, Required, StringLength(25)]
        public string LastName { get; set; }

        [BindProperty, Required, StringLength(25)]
        public string FirstName { get; set; }

        [BindProperty, Required, StringLength(2)]
        public string MemberShipCode { get; set; }

        [TempData]
        public string Message { get; set; }

        public Member NewMember { get; set; } = new Member();
        MainDerictor RequestDirector = new MainDerictor();
        public void OnGet()
        {
            Message = "";
        }

        public void OnPost()
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                NewMember.MemberNumber = MemberNumber;
                NewMember.LastName = LastName;
                NewMember.FirstName = FirstName;
                NewMember.MemberShipCode = MemberShipCode;
                if (IsMemberExexist(MemberNumber) == true)
                {
                    Message = "Member " + NewMember.FirstName + " " + NewMember.LastName + " " + NewMember.MemberNumber + " already recroded";
                }
                else
                {
                    result = RequestDirector.AddMember(NewMember);
                    if (result == true)
                    {
                        Message = "Member " + NewMember.FirstName + " " + NewMember.LastName + " " + NewMember.MemberNumber + " now is recroded";
                    }
                    else
                    {
                        Message = "Member failed to record";
                    }
                }
            }
        }
        public bool IsMemberExexist(string CheckMember)
        {

            bool result = false;
            result = RequestDirector.IsMemberExexist(MemberNumber);
            return result;
        }
    }
}