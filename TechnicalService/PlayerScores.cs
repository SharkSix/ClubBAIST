using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAIST.Domain;
using System.Data;
using System.Data.SqlClient;

namespace ClubBAIST.TechnicalService
{
    public class PlayerScores
    {
        const string connectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAIST;server=BBS\SQLEXPRESS";
        public bool AddPlayerScore(PlayerScore NewPlayerScores)
        {
            bool confirmation = false;
            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand AddPlayerScore = new SqlCommand();
            AddPlayerScore.CommandType = CommandType.StoredProcedure;
            AddPlayerScore.Connection = SampleConnection;
            AddPlayerScore.CommandText = "AddPlayerScore";
            AddPlayerScore.Parameters.AddWithValue("@Course", NewPlayerScores.Course);
            AddPlayerScore.Parameters.AddWithValue("@Rating", NewPlayerScores.Rating);
            AddPlayerScore.Parameters.AddWithValue("@Slope", NewPlayerScores.Slope);
            AddPlayerScore.Parameters.AddWithValue("@Date", NewPlayerScores.Date);
            AddPlayerScore.Parameters.AddWithValue("@Teetime", NewPlayerScores.Teetime);
            AddPlayerScore.Parameters.AddWithValue("@MemberNumber", NewPlayerScores.MemberNumber);
            AddPlayerScore.Parameters.AddWithValue("@Hole1", NewPlayerScores.Hole1);
            AddPlayerScore.Parameters.AddWithValue("@Hole2", NewPlayerScores.Hole2);
            AddPlayerScore.Parameters.AddWithValue("@Hole3", NewPlayerScores.Hole3);
            AddPlayerScore.Parameters.AddWithValue("@Hole4", NewPlayerScores.Hole4);
            AddPlayerScore.Parameters.AddWithValue("@Hole5", NewPlayerScores.Hole5);
            AddPlayerScore.Parameters.AddWithValue("@Hole6", NewPlayerScores.Hole6);
            AddPlayerScore.Parameters.AddWithValue("@Hole7", NewPlayerScores.Hole7);
            AddPlayerScore.Parameters.AddWithValue("@Hole8", NewPlayerScores.Hole8);
            AddPlayerScore.Parameters.AddWithValue("@Hole9", NewPlayerScores.Hole9);
            AddPlayerScore.Parameters.AddWithValue("@Hole10", NewPlayerScores.Hole10);
            AddPlayerScore.Parameters.AddWithValue("@Hole11", NewPlayerScores.Hole11);
            AddPlayerScore.Parameters.AddWithValue("@Hole12", NewPlayerScores.Hole12);
            AddPlayerScore.Parameters.AddWithValue("@Hole13", NewPlayerScores.Hole13);
            AddPlayerScore.Parameters.AddWithValue("@Hole14", NewPlayerScores.Hole14);
            AddPlayerScore.Parameters.AddWithValue("@Hole15", NewPlayerScores.Hole15);
            AddPlayerScore.Parameters.AddWithValue("@Hole16", NewPlayerScores.Hole16);
            AddPlayerScore.Parameters.AddWithValue("@Hole17", NewPlayerScores.Hole17);
            AddPlayerScore.Parameters.AddWithValue("@Hole18", NewPlayerScores.Hole18);
            AddPlayerScore.Parameters.AddWithValue("@HandicapDifferential", NewPlayerScores.HandicapDifferential);

            SqlParameter returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.Direction = ParameterDirection.ReturnValue;
            returnCode.SqlDbType = SqlDbType.Int;
            AddPlayerScore.Parameters.Add(returnCode);

            SqlDataReader DataReader;
            DataReader = AddPlayerScore.ExecuteReader();
            SampleConnection.Close();
            confirmation = (((int)returnCode.Value) == 0);
            return confirmation;

        }
        public List<HandicapReport> GetHandicapReport(DateTime Time)
        {
            List<HandicapReport> handicapReport = new List<HandicapReport>();
            List<Member> members = new List<Member>();
            Members MemberManager = new Members();
            members = MemberManager.GetAllMember();
            foreach (Member member in members)
            {
                HandicapReport MemberReport = new HandicapReport();
                MemberReport.MemberNumber = member.MemberNumber;
                MemberReport.MemberName = member.FirstName + ' ' + member.LastName;
                handicapReport.Add(MemberReport);
            }

            SqlConnection SampleConnection = new SqlConnection();
            SampleConnection.ConnectionString = connectionString;
            SampleConnection.Open();

            SqlCommand GetHandicapReport = new SqlCommand();
            GetHandicapReport.CommandType = CommandType.StoredProcedure;
            GetHandicapReport.Connection = SampleConnection;
            GetHandicapReport.CommandText = "GetHandicapReport";
            GetHandicapReport.Parameters.AddWithValue("@Time", Time);

            SqlParameter returnCode = new SqlParameter();
            returnCode.ParameterName = "@ReturnCode";
            returnCode.Direction = ParameterDirection.ReturnValue;
            returnCode.SqlDbType = SqlDbType.Int;
            GetHandicapReport.Parameters.Add(returnCode);

            SqlDataReader DataReader;
            DataReader = GetHandicapReport.ExecuteReader();

            if (DataReader.HasRows)
                while (DataReader.Read())
                {
                    for (int i = 0; i < handicapReport.Count; i++)
                    {
                        if (handicapReport[i].MemberNumber == DataReader["MemberNumber"].ToString() && handicapReport[i].Last20Rounds==null)
                        {
                            
                            List<double> HandicapDifferentialList = new List<double>();
                            double num = double.Parse(DataReader["HandicapDifferential"].ToString());
                            HandicapDifferentialList.Add(num);
                            handicapReport[i].Last20Rounds = HandicapDifferentialList;
                        }
                        else if (handicapReport[i].MemberNumber == DataReader["MemberNumber"].ToString())
                        {
                            handicapReport[i].Last20Rounds.Add(double.Parse(DataReader["HandicapDifferential"].ToString()));
                        }
                    }
                }
            else
            {
                handicapReport = null;
            }
            SampleConnection.Close();
            return handicapReport;

        }
    }
}
