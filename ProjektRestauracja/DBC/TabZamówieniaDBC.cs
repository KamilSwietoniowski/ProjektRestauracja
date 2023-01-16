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
    public class TabZamówieniaDBC
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();
        public List<TabZamówienia> GetAllTabZamówienia()
        {
            List<TabZamówienia> tabzamówieniaList = new List<TabZamówienia>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllZamówienia";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtZamówienia = new DataTable();

                connection.Open();
                sqlDA.Fill(dtZamówienia);
                connection.Close();

                foreach (DataRow dr in dtZamówienia.Rows)
                {
                    tabzamówieniaList.Add(new TabZamówienia
                    {
                        ID_Zamówienia = Convert.ToInt32(dr["ID_Zamówienia"]),
                        ID_Potrawy = Convert.ToInt32(dr["ID_Potrawy"]),
                        ID_Pracownika = Convert.ToInt32(dr["ID Pracownika"]),
                        Status_Zamówienia = Convert.ToString(dr["Status_Zamówienia"]),
                        Numer_Stolika = Convert.ToInt32(dr["Numer_Stolika"]),

                    });
                }

            }
            return tabzamówieniaList;
        }

        public bool insertZamówienia(TabZamówienia tabZamówienia)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertZamówienia", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_Potrawy", tabZamówienia.ID_Potrawy);
                command.Parameters.AddWithValue("@ID_Pracownika",   tabZamówienia.ID_Pracownika);
                command.Parameters.AddWithValue("@Status_Zamówienia", tabZamówienia.Status_Zamówienia);
                command.Parameters.AddWithValue("@Numer_Stolika", tabZamówienia.Numer_Stolika);
  
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


        public List<TabZamówienia> EditZamówienia(int ID_Zamówienia)
        {
            List<TabZamówienia> tabzamówieniaList = new List<TabZamówienia>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_EditZamówienia";
                command.Parameters.AddWithValue("@ID_Zamówienia", ID_Zamówienia);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtZamówienia = new DataTable();

                connection.Open();
                sqlDA.Fill(dtZamówienia);
                connection.Close();

                foreach (DataRow dr in dtZamówienia.Rows)
                {
                    tabzamówieniaList.Add(new TabZamówienia
                    {
                        ID_Zamówienia = Convert.ToInt32(dr["ID_Zamówienia"]),
                        ID_Potrawy = Convert.ToInt32(dr["ID_Potrawy"]),
                        ID_Pracownika = Convert.ToInt32(dr["ID Pracownika"]),
                        Status_Zamówienia = Convert.ToString(dr["Status_Zamówienia"]),
                        Numer_Stolika = Convert.ToInt32(dr["Numer_Stolika"]),
                     

                    });
                }

            }

            return tabzamówieniaList;
        }

        public bool UpdateZamówienia(TabZamówienia tabZamówienia)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateZamówienia", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_Zamówienia", tabZamówienia.ID_Zamówienia);
                command.Parameters.AddWithValue("@ID_Potrawy", tabZamówienia.ID_Potrawy);
                command.Parameters.AddWithValue("@ID_Pracownika", tabZamówienia.ID_Pracownika);
                command.Parameters.AddWithValue("@Status_Zamówienia", tabZamówienia.Status_Zamówienia);
                command.Parameters.AddWithValue("@Numer_Stolika", tabZamówienia.Numer_Stolika);
                
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

        public string DeleteZamówienia(int tabZamówieniaid)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeleteZamówienia", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_Zamówienia", tabZamówieniaid);
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