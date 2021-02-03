using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAIST.Domain;
using System.Data;
using System.Data.SqlClient;


namespace ClubBAIST.TechnicalService
{

    public class StandingTeeTImes
    {
        const string connectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAIST;server=BBS\SQLEXPRESS";
        public bool IsShareholdHadRequest(string MemberNumber,string StartDate)
        {
            bool result = false;
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand SampleCommand = new SqlCommand();
            SampleCommand.Connection = SampleConnection;
            SampleCommand.CommandType = CommandType.StoredProcedure;
            SampleCommand.CommandText = "IsShareholdHadRequest";

            SqlParameter SampleCommandParameter = new SqlParameter
            {
                ParameterName = "@MemberNumber",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = MemberNumber

            };
            SampleCommand.Parameters.Add(SampleCommandParameter);

            SqlParameter SampleCommandParameter2 = new SqlParameter
            {
                ParameterName = "@StartDate",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = StartDate

            };
            SampleCommand.Parameters.Add(SampleCommandParameter2);

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
        public bool AddStandingTeeTimeRequest(StandingTeetimeRequest DesireStandingTeetimeRequest)
        {
            bool confirmation = false;
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand addTeeTime = new SqlCommand();
            addTeeTime.CommandType = CommandType.StoredProcedure;
            addTeeTime.Connection = SampleConnection;
            addTeeTime.CommandText = "AddStandingTeeTimeRequest";
            addTeeTime.Parameters.AddWithValue("@ShareholderNumber", DesireStandingTeetimeRequest.ShareholderNumber);
            addTeeTime.Parameters.AddWithValue("@MemberTwoNumber", DesireStandingTeetimeRequest.MemberTwoNumber);
            addTeeTime.Parameters.AddWithValue("@MemberThreeNumber", DesireStandingTeetimeRequest.MemberThreeNumber);
            addTeeTime.Parameters.AddWithValue("@MemberFourNumber", DesireStandingTeetimeRequest.MemberFourNumber);
            addTeeTime.Parameters.AddWithValue("@TeeTime", DesireStandingTeetimeRequest.TeeTime);
            addTeeTime.Parameters.AddWithValue("@StartDate", DesireStandingTeetimeRequest.StartDate);
            addTeeTime.Parameters.AddWithValue("@EndDate", DesireStandingTeetimeRequest.EndDate);

            SqlParameter returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.Direction = ParameterDirection.ReturnValue;
            returnCode.SqlDbType = SqlDbType.Int;
            addTeeTime.Parameters.Add(returnCode);

            SqlDataReader DataReader;
            DataReader = addTeeTime.ExecuteReader();
            SampleConnection.Close();
            confirmation = (((int)returnCode.Value) == 0);
            return confirmation;

        }
        public bool CancleStandingTeeTime(string MemberNumber, string StartDate)
        {
            bool confirmation = false;
            SqlConnection CSConnection = new SqlConnection();
            CSConnection.ConnectionString = connectionString;
            CSConnection.Open();

            SqlCommand addTeeTime = new SqlCommand();
            addTeeTime.CommandType = CommandType.StoredProcedure;
            addTeeTime.Connection = CSConnection;
            addTeeTime.CommandText = "DeleteStandingTeeTime";
            addTeeTime.Parameters.AddWithValue("@MemberNumber", MemberNumber);
            addTeeTime.Parameters.AddWithValue("@StartDate", StartDate);

            SqlParameter returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.Direction = ParameterDirection.ReturnValue;
            returnCode.SqlDbType = SqlDbType.Int;
            addTeeTime.Parameters.Add(returnCode);

            SqlDataReader DataReader;
            DataReader = addTeeTime.ExecuteReader();
            CSConnection.Close();
            confirmation = (((int)returnCode.Value) == 0);
            return confirmation;
        }
    }

}
