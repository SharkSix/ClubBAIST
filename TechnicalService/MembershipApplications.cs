using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAIST.Domain;
using System.Data;
using System.Data.SqlClient;

namespace ClubBAIST.TechnicalService
{
    public class MembershipApplications
    {
        const string connectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAIST;server=BBS\SQLEXPRESS";

        public bool AddMembershipApplication(MembershipApplication NewApplication)
        {
            bool confirmation = false;
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand addApplication = new SqlCommand();

            addApplication.CommandType = CommandType.StoredProcedure;
            addApplication.Connection = SampleConnection;
            addApplication.CommandText = "AddMembershipApplication";
            addApplication.Parameters.AddWithValue("@LastName", NewApplication.LastName);
            addApplication.Parameters.AddWithValue("@FirstName", NewApplication.FirstName);
            addApplication.Parameters.AddWithValue("@Address", NewApplication.Address);
            addApplication.Parameters.AddWithValue("@PostalCode", NewApplication.PostalCode);
            addApplication.Parameters.AddWithValue("@Phone", NewApplication.Phone);
            addApplication.Parameters.AddWithValue("@AlternatePhone", NewApplication.AlternatePhone);
            addApplication.Parameters.AddWithValue("@Email", NewApplication.Email);
            addApplication.Parameters.AddWithValue("@DateOfBirth", NewApplication.DateOfBirth);
            addApplication.Parameters.AddWithValue("@Occupation", NewApplication.Occupation);
            addApplication.Parameters.AddWithValue("@CompanyName", NewApplication.CompanyName);
            addApplication.Parameters.AddWithValue("@CompanyAddress", NewApplication.CompanyAddress);
            addApplication.Parameters.AddWithValue("@CompanyPostalCode", NewApplication.CompanyPostalCode);
            addApplication.Parameters.AddWithValue("@CompanyPhone", NewApplication.CompanyPhone);
            addApplication.Parameters.AddWithValue("@Date", NewApplication.Date);
            addApplication.Parameters.AddWithValue("@ShareholderOneNumber", NewApplication.ShareholderOneNumber);
            addApplication.Parameters.AddWithValue("@ShareholderOneSignDate", NewApplication.ShareholderOneSignDate);
            addApplication.Parameters.AddWithValue("@ShareholderTwoNumber", NewApplication.ShareholderTwoNumber);
            addApplication.Parameters.AddWithValue("@ShareholderTwoSignDate", NewApplication.ShareholderTwoSignDate);
            addApplication.Parameters.AddWithValue("@Status", NewApplication.Status);

            SqlParameter returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.Direction = ParameterDirection.ReturnValue;
            returnCode.SqlDbType = SqlDbType.Int;
            addApplication.Parameters.Add(returnCode);

            SqlDataReader DataReader;
            DataReader = addApplication.ExecuteReader();
            SampleConnection.Close();
            confirmation = (((int)returnCode.Value) == 0);
            return confirmation;

        }

        public List<MembershipApplication> GetAllMembershipAppliactions()
        {
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand getApplications = new SqlCommand();
            List<MembershipApplication> ApplicationList = new List<MembershipApplication>();

            getApplications.CommandType = CommandType.StoredProcedure;
            getApplications.Connection = SampleConnection;
            getApplications.CommandText = "GetAllMembershipAppliactions";
            

            SqlParameter returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.Direction = ParameterDirection.ReturnValue;
            returnCode.SqlDbType = SqlDbType.Int;
            getApplications.Parameters.Add(returnCode);

            SqlDataReader DataReader;
            DataReader = getApplications.ExecuteReader();

            if (DataReader.HasRows)
                while (DataReader.Read())
                {
                    MembershipApplication membershipApplication = new MembershipApplication();
                    membershipApplication.Date = DataReader["Date"].ToString();
                    membershipApplication.FirstName = DataReader["FirstName"].ToString();
                    membershipApplication.LastName = DataReader["LastName"].ToString();
                    membershipApplication.Status = DataReader["Status"].ToString();
                    ApplicationList.Add(membershipApplication);
                }

            DataReader.Close();
            SampleConnection.Close();
            return ApplicationList;



        }

        public MembershipApplication GetMembershipAppliactionByName(string FirstName, string LastName)
        {
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand GetApplication = new SqlCommand();
            MembershipApplication Application = new MembershipApplication();

            GetApplication.CommandType = CommandType.StoredProcedure;
            GetApplication.Connection = SampleConnection;
            GetApplication.CommandText = "GetMembershipAppliactionByName";
            GetApplication.Parameters.AddWithValue("@LastName", LastName);
            GetApplication.Parameters.AddWithValue("@FirstName", FirstName);
            

            SqlParameter returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.Direction = ParameterDirection.ReturnValue;
            returnCode.SqlDbType = SqlDbType.Int;
            GetApplication.Parameters.Add(returnCode);

            SqlDataReader DataReader;
            DataReader = GetApplication.ExecuteReader();

            if (DataReader.HasRows)
                while (DataReader.Read())
                {
                    Application.LastName = DataReader["LastName"].ToString();
                    Application.FirstName = DataReader["FirstName"].ToString();
                    Application.Address = DataReader["Address"].ToString();
                    Application.PostalCode = DataReader["PostalCode"].ToString();
                    Application.Phone = DataReader["Phone"].ToString();
                    Application.AlternatePhone = DataReader["AlternatePhone"].ToString();
                    Application.Email = DataReader["Email"].ToString();
                    Application.DateOfBirth = DataReader["DateOfBirth"].ToString();
                    Application.Occupation = DataReader["Occupation"].ToString();
                    Application.CompanyName = DataReader["CompanyName"].ToString();
                    Application.CompanyAddress = DataReader["CompanyAddress"].ToString();
                    Application.CompanyPostalCode = DataReader["CompanyPostalCode"].ToString();
                    Application.CompanyPhone = DataReader["CompanyPhone"].ToString();
                    Application.Date = DataReader["Date"].ToString();
                    Application.ShareholderOneNumber = DataReader["ShareholderOneNumber"].ToString();
                    Application.ShareholderOneSignDate = DataReader["ShareholderOneSignDate"].ToString();
                    Application.ShareholderTwoNumber = DataReader["ShareholderTwoNumber"].ToString();
                    Application.ShareholderTwoSignDate = DataReader["ShareholderTwoSignDate"].ToString();
                    Application.Status = DataReader["Status"].ToString();
                }

            DataReader.Close();
            SampleConnection.Close();
            return Application;
        }
    }
}
