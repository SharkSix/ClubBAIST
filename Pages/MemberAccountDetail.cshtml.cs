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
    public class MemberAccountDetailModel : PageModel
    {
        public Member member { get; set; } = new Member();
        MainDerictor RequestDirector = new MainDerictor();
        public void OnGet(string id)
        {
            member = RequestDirector.GetMemberByNumber(id);
        }
    }
}