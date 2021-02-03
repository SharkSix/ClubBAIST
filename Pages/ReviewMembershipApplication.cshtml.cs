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
    public class ReviewMembershipApplicationModel : PageModel
    {
        MainDerictor RequestDirector = new MainDerictor();
        public List<MembershipApplication> ApplicationList { get; set; } = new List<MembershipApplication>();
        public void OnGet()
        {
            ApplicationList = RequestDirector.GetAllMembershipAppliactions();
        }
    }
}