using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAIST.Domain;
using System.Data;
using System.Data.SqlClient;

namespace ClubBAIST.TechnicalService
{
    public class TeeTimes
    {
        const string connectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAIST;server=BBS\SQLEXPRESS";
        bool confirmation = false;

        public List<TeeTime> GetAvailableTeeTimeList(List<TeeTime> AvailableTeeTimeList, string Date)
        {
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand SampleCommand = new SqlCommand();
            SampleCommand.Connection = SampleConnection;
            SampleCommand.CommandType = CommandType.StoredProcedure;
            SampleCommand.CommandText = "GetTeeTime";

            SqlParameter SampleCommandParameter = new SqlParameter
            {
                ParameterName = "@Date",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = Date

            };
            SampleCommand.Parameters.Add(SampleCommandParameter);

            SqlDataReader SampleDataReader;
            SampleDataReader = SampleCommand.ExecuteReader();

            if (SampleDataReader.HasRows)
            {
                while (SampleDataReader.Read())
                {
                    TeeTime BookedTeeTime = new TeeTime();
                    BookedTeeTime.Date = SampleDataReader["Date"].ToString();
                    BookedTeeTime.Teetime = SampleDataReader["Teetime"].ToString();
                    for (int i = 0; i < AvailableTeeTimeList.Count; i++)
                    {
                        if (AvailableTeeTimeList[i].Teetime == BookedTeeTime.Teetime)
                            AvailableTeeTimeList.Remove(AvailableTeeTimeList[i]);
                    }
                }
            }
            SampleDataReader.Close();
            SampleConnection.Close();

            return AvailableTeeTimeList;

        }

        public List<DailyTeeTimeSheet> GetDailyTeeTimeSheet(List<DailyTeeTimeSheet> InitialDailyTeeTimeSheet, string Date)
        {
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand SampleCommand = new SqlCommand();
            SampleCommand.Connection = SampleConnection;
            SampleCommand.CommandType = CommandType.StoredProcedure;
            SampleCommand.CommandText = "GetDailyTeeSheet";

            SqlParameter SampleCommandParameter = new SqlParameter
            {
                ParameterName = "@Date",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = Date

            };
            SampleCommand.Parameters.Add(SampleCommandParameter);
            SqlDataReader SampleDataReader;
            SampleDataReader = SampleCommand.ExecuteReader();

            if (SampleDataReader.HasRows)
            {
                while (SampleDataReader.Read())
                {
                    DailyTeeTimeSheet BookedTeeTime = new DailyTeeTimeSheet();
                    BookedTeeTime.Date = SampleDataReader["Date"].ToString();
                    BookedTeeTime.Teetime = SampleDataReader["Teetime"].ToString();
                    BookedTeeTime.Phone = SampleDataReader["Phone"].ToString();
                    BookedTeeTime.NumberOfCarts = int.Parse(SampleDataReader["NumberOfCarts"].ToString());
                    BookedTeeTime.Time = decimal.Parse(SampleDataReader["Time"].ToString());
                    BookedTeeTime.MemberName = SampleDataReader["MemberName"].ToString();
                    for (int i = 0; i < InitialDailyTeeTimeSheet.Count; i++)
                    {
                        if (BookedTeeTime.Teetime == InitialDailyTeeTimeSheet[i].Teetime)
                        {
                            InitialDailyTeeTimeSheet[i].Date = BookedTeeTime.Date;
                            InitialDailyTeeTimeSheet[i].Phone = BookedTeeTime.Phone;
                            InitialDailyTeeTimeSheet[i].NumberOfCarts = BookedTeeTime.NumberOfCarts;
                            InitialDailyTeeTimeSheet[i].Time = BookedTeeTime.Time;
                            InitialDailyTeeTimeSheet[i].MemberName = InitialDailyTeeTimeSheet[i].MemberName + BookedTeeTime.MemberName + ',';
                            InitialDailyTeeTimeSheet[i].NumberOfPlayer++;
                        }
                    }
                }
            }
            SampleDataReader.Close();
            SampleConnection.Close();

            return InitialDailyTeeTimeSheet;

        }
        public bool AddTeeTime(DailyTeeTimeSheet DedesireTeeTime,List<TeeTimeMember> Members)
        {
            SqlConnection CSConnection = new SqlConnection();
            CSConnection.ConnectionString = connectionString;
            CSConnection.Open();

            SqlCommand addTeeTime = new SqlCommand();
            addTeeTime.CommandType = CommandType.StoredProcedure;
            addTeeTime.Connection = CSConnection;
            addTeeTime.CommandText = "AddTeeTime";
            addTeeTime.Parameters.AddWithValue("@Date", DedesireTeeTime.Date);
            addTeeTime.Parameters.AddWithValue("@Teetime", DedesireTeeTime.Teetime);
            addTeeTime.Parameters.AddWithValue("@Phone", DedesireTeeTime.Phone);
            addTeeTime.Parameters.AddWithValue("@NumberOfCarts", DedesireTeeTime.NumberOfCarts);
            addTeeTime.Parameters.AddWithValue("@Time", DedesireTeeTime.Time);

            SqlParameter returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.Direction = ParameterDirection.ReturnValue;
            returnCode.SqlDbType = SqlDbType.Int;
            addTeeTime.Parameters.Add(returnCode);

            SqlDataReader DataReader;
            DataReader = addTeeTime.ExecuteReader();
            CSConnection.Close();
            confirmation = (((int)returnCode.Value) == 0);
            addTeeTimeMember(Members);
            return confirmation;
        }
        public void addTeeTimeMember(List<TeeTimeMember> Members)
        {
            foreach (TeeTimeMember member in Members)
            {
                if (confirmation == true)
                {
                    SqlConnection CSConnection = new SqlConnection();
                    CSConnection.ConnectionString = connectionString;
                    CSConnection.Open();

                    SqlCommand addTeeTimeMember = new SqlCommand();
                    addTeeTimeMember.CommandType = CommandType.StoredProcedure;
                    addTeeTimeMember.Connection = CSConnection;
                    addTeeTimeMember.CommandText = "AddTeeTimeMember";
                    addTeeTimeMember.Parameters.AddWithValue("@Date", member.Date);
                    addTeeTimeMember.Parameters.AddWithValue("@Teetime", member.Teetime);
                    addTeeTimeMember.Parameters.AddWithValue("@MemberNumber", member.MemberNumber);

                    SqlParameter returnCode = new SqlParameter();
                    returnCode.ParameterName = "@ReturnCode";
                    returnCode.Direction = ParameterDirection.ReturnValue;
                    returnCode.SqlDbType = SqlDbType.Int;
                    addTeeTimeMember.Parameters.Add(returnCode);

                    SqlDataReader DataReader;
                    DataReader = addTeeTimeMember.ExecuteReader();
                    CSConnection.Close();
                    confirmation = (((int)returnCode.Value) == 0);
                }
            }
        }

        public DailyTeeTimeSheet GetBookedTeeTime(string MemberNumber,string Date,string TeeTime)
        {
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand GetTeeTime = new SqlCommand();
            GetTeeTime.Connection = SampleConnection;
            GetTeeTime.CommandType = CommandType.StoredProcedure;
            GetTeeTime.CommandText = "GetBookedTeeTime";

            GetTeeTime.Parameters.AddWithValue("@Date", Date);
            GetTeeTime.Parameters.AddWithValue("@TeeTime", TeeTime);
            GetTeeTime.Parameters.AddWithValue("@MemberNumber", MemberNumber);

            SqlDataReader SampleDataReader;
            SampleDataReader = GetTeeTime.ExecuteReader();

            DailyTeeTimeSheet BookedTeeTime = new DailyTeeTimeSheet();
            List<string> BookedMembers = new List<string>();

            if (SampleDataReader.HasRows)
            {
                while (SampleDataReader.Read())
                {
                    BookedTeeTime.Date = SampleDataReader["Date"].ToString();
                    BookedTeeTime.Teetime = SampleDataReader["Teetime"].ToString();
                    BookedTeeTime.Member1Number = SampleDataReader["MemberNumber"].ToString();
                    BookedTeeTime.Phone = SampleDataReader["Phone"].ToString();
                    BookedTeeTime.NumberOfCarts = int.Parse(SampleDataReader["NumberOfCarts"].ToString());
                }
                SampleDataReader.Close();
                SqlCommand GetTeeTimeWithMembers = new SqlCommand();
                GetTeeTimeWithMembers.Connection = SampleConnection;
                GetTeeTimeWithMembers.CommandType = CommandType.StoredProcedure;
                GetTeeTimeWithMembers.CommandText = "GetTeeTimeWithMembers";

                GetTeeTimeWithMembers.Parameters.AddWithValue("@Date", Date);
                GetTeeTimeWithMembers.Parameters.AddWithValue("@TeeTime", TeeTime);

                SqlDataReader SampleDataReader2;
                SampleDataReader2 = GetTeeTimeWithMembers.ExecuteReader();

                if (SampleDataReader2.HasRows)
                    while (SampleDataReader2.Read())
                    {
                        BookedMembers.Add(SampleDataReader2["MemberNumber"].ToString());
                    }
                int memberCount = BookedMembers.Count();

                while(memberCount<4)
                {
                    BookedMembers.Add(null);
                    memberCount++;
                }

                BookedMembers.Remove(BookedTeeTime.Member1Number);
                BookedTeeTime.Member2Number = BookedMembers[0];
                BookedTeeTime.Member3Number = BookedMembers[1];
                BookedTeeTime.Member4Number = BookedMembers[2];


                SampleDataReader2.Close();
                SampleConnection.Close();



            }
            else
            {
                SampleDataReader.Close();
                SampleConnection.Close();
            }

            return BookedTeeTime;

        }

        public bool UpdateTeeTime(DailyTeeTimeSheet DedesireTeeTime, List<TeeTimeMember> Members)
        {
            SqlConnection CSConnection = new SqlConnection();
            CSConnection.ConnectionString = connectionString;
            CSConnection.Open();

            SqlCommand addTeeTime = new SqlCommand();
            addTeeTime.CommandType = CommandType.StoredProcedure;
            addTeeTime.Connection = CSConnection;
            addTeeTime.CommandText = "UpdateTeeTime";
            addTeeTime.Parameters.AddWithValue("@Date", DedesireTeeTime.Date);
            addTeeTime.Parameters.AddWithValue("@Teetime", DedesireTeeTime.Teetime);
            addTeeTime.Parameters.AddWithValue("@Phone", DedesireTeeTime.Phone);
            addTeeTime.Parameters.AddWithValue("@NumberOfCarts", DedesireTeeTime.NumberOfCarts);

            SqlParameter returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.Direction = ParameterDirection.ReturnValue;
            returnCode.SqlDbType = SqlDbType.Int;
            addTeeTime.Parameters.Add(returnCode);

            SqlDataReader DataReader;
            DataReader = addTeeTime.ExecuteReader();
            CSConnection.Close();
            confirmation = (((int)returnCode.Value) == 0);
            addTeeTimeMember(Members);
            return confirmation;
        }

        public bool DeleteTeeTime(string MemberNumber, string Date, string TeeTime)
        {
            SqlConnection CSConnection = new SqlConnection();
            CSConnection.ConnectionString = connectionString;
            CSConnection.Open();

            SqlCommand addTeeTime = new SqlCommand();
            addTeeTime.CommandType = CommandType.StoredProcedure;
            addTeeTime.Connection = CSConnection;
            addTeeTime.CommandText = "DeleteTeeTime";
            addTeeTime.Parameters.AddWithValue("@Date", Date);
            addTeeTime.Parameters.AddWithValue("@TeeTime", TeeTime);

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
