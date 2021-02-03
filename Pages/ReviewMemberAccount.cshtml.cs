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
    public class ReviewMemberAccountModel : PageModel
    {
        MainDerictor RequestDirector = new MainDerictor();
        public List<Member> MemberList{ get; set; } = new List<Member>();
        public void OnGet()
        {
            MemberList = RequestDirector.GetAllMember();
        }

    }
}