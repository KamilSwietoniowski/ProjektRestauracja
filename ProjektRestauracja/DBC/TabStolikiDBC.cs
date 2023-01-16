using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ProjektRestauracja.Models;

namespace ProjektRestauracja.DBC
{
    public class TabStolikiDBC
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        public List<TabStoliki> GetAllTabStoliki()
        {
            List<TabStoliki> TabStolikiList = new List<TabStoliki>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllTabStoliki";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtTabStoliki = new DataTable();

                connection.Open();
                sqlDA.Fill(dtTabStoliki);
                connection.Close();

                foreach (DataRow dr in dtTabStoliki.Rows)
                {
                    TabStolikiList.Add(new TabStoliki
                    {
                        Numer_stolika = Convert.ToInt32(dr["Numer_stolika"]),
                        Ilość_miejsc = Convert.ToInt32(dr["Ilość_miejsc"]),

                    });
                }

            }

            return TabStolikiList;
        }
        public bool InsertTabStoliki(TabStoliki tabStoliki)
        {

            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertTabStoliki", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Ilość_miejsc", tabStoliki.Ilość_miejsc);
                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<TabStoliki> EditTabStoliki(int Numer_stolika)
        {
            List<TabStoliki> tabStolikiList = new List<TabStoliki>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_EditTabStoliki";
                command.Parameters.AddWithValue("@Numer_stolika", Numer_stolika);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtTabStoliki = new DataTable();

                connection.Open();
                sqlDA.Fill(dtTabStoliki);
                connection.Close();

                foreach (DataRow dr in dtTabStoliki.Rows)
                {
                    tabStolikiList.Add(new TabStoliki
                    {
                        Numer_stolika = Convert.ToInt32(dr["Numer_stolika"]),
                        Ilość_miejsc = Convert.ToInt32(dr["Ilość_miejsc"]),
                     

                    });
                }

            }

            return tabStolikiList;
        }

        public bool UpdateTabStoliki(TabStoliki tabStoliki)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateTabStoliki", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Numer_stolika", tabStoliki.Numer_stolika);
                command.Parameters.AddWithValue("@Ilość_miejsc", tabStoliki.Ilość_miejsc);       
                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string DeleteTabStoliki(int Numer_stolika)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteTabStoliki", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Numer_stolika", Numer_stolika);
                command.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OUTPUTMESSAGE"].Value.ToString();
                connection.Close();
            }

            return result;
        }
    }
}