using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAIST.Domain;
using System.Data;
using System.Data.SqlClient;

namespace ClubBAIST.TechnicalService
{
    public class Members
    {
        const string connectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAIST;server=BBS\SQLEXPRESS";
        public bool IsMemberExexist(string MemberNumber)
        {
            bool result = false;
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand SampleCommand = new SqlCommand();
            SampleCommand.Connection = SampleConnection;
            SampleCommand.CommandType = CommandType.StoredProcedure;
            SampleCommand.CommandText = "IsMemberExexist";

            SqlParameter SampleCommandParameter = new SqlParameter
            {
                ParameterName = "@MemberNumber",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = MemberNumber

            };
            SampleCommand.Parameters.Add(SampleCommandParameter);

            SqlDataReader SampleDataReader;
            SampleDataReader = SampleCommand.ExecuteReader();

            if (SampleDataReader.HasRows)
            {
                while (SampleDataReader.Read())
                {
                    result = true;
                }
            }
            SampleDataReader.Close();
            SampleConnection.Close();
            return result;
        }
        public string GetBookerMembershipCode(string MemberNumber)
        {
            string MembershipCode = "00";
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand SampleCommand = new SqlCommand();
            SampleCommand.Connection = SampleConnection;
            SampleCommand.CommandType = CommandType.StoredProcedure;
            SampleCommand.CommandText = "IsMemberExexist";

            SqlParameter SampleCommandParameter = new SqlParameter
            {
                ParameterName = "@MemberNumber",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = MemberNumber

            };
            SampleCommand.Parameters.Add(SampleCommandParameter);

            SqlDataReader SampleDataReader;
            SampleDataReader = SampleCommand.ExecuteReader();

            if (SampleDataReader.HasRows)
            {
                while (SampleDataReader.Read())
                {
                    MembershipCode = SampleDataReader["MembershipCode"].ToString();
                }
            }
            SampleDataReader.Close();
            SampleConnection.Close();
            return MembershipCode;
        }
        public bool ISMemberNumberQualified(string MemberNumber)
        {
            bool result = false;
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand SampleCommand = new SqlCommand();
            SampleCommand.Connection = SampleConnection;
            SampleCommand.CommandType = CommandType.StoredProcedure;
            SampleCommand.CommandText = "ISMemberNumberQualified";

            SqlParameter SampleCommandParameter = new SqlParameter
            {
                ParameterName = "@MemberNumber",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = MemberNumber

            };
            SampleCommand.Parameters.Add(SampleCommandParameter);

            SqlDataReader SampleDataReader;
            SampleDataReader = SampleCommand.ExecuteReader();

            if (SampleDataReader.HasRows)
            {
                result = true;
            }
            SampleDataReader.Close();
            SampleConnection.Close();
            return result;
        }
        public bool CheckShareholder(string MemberNumber)
        {
            bool result = false;
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand SampleCommand = new SqlCommand();
            SampleCommand.Connection = SampleConnection;
            SampleCommand.CommandType = CommandType.StoredProcedure;
            SampleCommand.CommandText = "CheckShareholder";

            SqlParameter SampleCommandParameter = new SqlParameter
            {
                ParameterName = "@MemberNumber",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = MemberNumber

            };
            SampleCommand.Parameters.Add(SampleCommandParameter);

            SqlDataReader SampleDataReader;
            SampleDataReader = SampleCommand.ExecuteReader();

            if (SampleDataReader.HasRows)
            {
                result = true;
            }
            SampleDataReader.Close();
            SampleConnection.Close();
            return result;
        }
        public List<Member> GetAllMember()
        {
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand getAllMembers = new SqlCommand();
            List<Member> MemberList = new List<Member>();

            getAllMembers.CommandType = CommandType.StoredProcedure;
            getAllMembers.Connection = SampleConnection;
            getAllMembers.CommandText = "GetAllMember";


            SqlParameter returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.Direction = ParameterDirection.ReturnValue;
            returnCode.SqlDbType = SqlDbType.Int;
            getAllMembers.Parameters.Add(returnCode);

            SqlDataReader DataReader;
            DataReader = getAllMembers.ExecuteReader();

            if (DataReader.HasRows)
                while (DataReader.Read())
                {
                    Member member = new Member();
                    member.FirstName = DataReader["FirstName"].ToString();
                    member.LastName = DataReader["LastName"].ToString();
                    member.MemberNumber = DataReader["MemberNumber"].ToString();
                    MemberList.Add(member);
                }

            DataReader.Close();
            SampleConnection.Close();
            return MemberList;
        }
        public bool AddMember(Member NewMember)
        {
            bool confirmation = false;
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand AddMember = new SqlCommand();
            AddMember.CommandType = CommandType.StoredProcedure;
            AddMember.Connection = SampleConnection;
            AddMember.CommandText = "AddMember";
            AddMember.Parameters.AddWithValue("@MemberNumber", NewMember.MemberNumber);
            AddMember.Parameters.AddWithValue("@FirstName", NewMember.FirstName);
            AddMember.Parameters.AddWithValue("@LastName", NewMember.LastName);
            AddMember.Parameters.AddWithValue("@MemberShipCode", NewMember.MemberShipCode);

            SqlParameter returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.Direction = ParameterDirection.ReturnValue;
            returnCode.SqlDbType = SqlDbType.Int;
            AddMember.Parameters.Add(returnCode);

            SqlDataReader DataReader;
            DataReader = AddMember.ExecuteReader();
            SampleConnection.Close();
            confirmation = (((int)returnCode.Value) == 0);
            return confirmation;

        }
        public Member GetMemberByNumber(string MemberNumber)
        {
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand getMember = new SqlCommand();
            Member member = new Member();

            getMember.CommandType = CommandType.StoredProcedure;
            getMember.Connection = SampleConnection;
            getMember.CommandText = "GetMemberByNumber";
            getMember.Parameters.AddWithValue("@MemberNumber",MemberNumber);

            SqlParameter returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.Direction = ParameterDirection.ReturnValue;
            returnCode.SqlDbType = SqlDbType.Int;
            getMember.Parameters.Add(returnCode);

            SqlDataReader DataReader;
            DataReader = getMember.ExecuteReader();

            if (DataReader.HasRows)
                while (DataReader.Read())
                {
                    member.FirstName = DataReader["FirstName"].ToString();
                    member.LastName = DataReader["LastName"].ToString();
                    member.MemberNumber = DataReader["MemberNumber"].ToString();
                    member.MemberShipCode = DataReader["Description"].ToString();
                }

            DataReader.Close();
            SampleConnection.Close();
            return member;
        }
    }
}
