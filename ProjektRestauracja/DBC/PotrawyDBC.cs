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
    public class PotrawyDBC
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        //Połączenie z bazą danych
        public List<Potrawy> GetAllPotrawy()
        {
            List<Potrawy> potrawyList = new List<Potrawy>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllPotrawy";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtPotrawy = new DataTable();

                connection.Open();
                sqlDA.Fill(dtPotrawy);
                connection.Close();

                foreach (DataRow dr in dtPotrawy.Rows)
                {
                    potrawyList.Add(new Potrawy
                    {
                        ID_Potrawy = Convert.ToInt32(dr["ID_Potrawy"]),
                        Nazwa_Potrawy = Convert.ToString(dr["Nazwa_Potrawy"]),
                        Cena = Convert.ToDecimal(dr["Cena"]),
                        Typ_Potrawy = Convert.ToString(dr["Typ_Potrawy"]),


                    });
                }

            }
            return potrawyList;
        }

        public bool InsertPotrawy(Potrawy potrawy)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertPotrawy", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nazwa_Potrawy", potrawy.Nazwa_Potrawy);
                command.Parameters.AddWithValue("@Cena", potrawy.Cena);
                command.Parameters.AddWithValue("@Typ_Potrawy", potrawy.Typ_Potrawy);
            
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

        public List<Potrawy> EditPotrawy(int ID_Potrawy)
        {
            List<Potrawy> potrawyList = new List<Potrawy>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_EditPotrawy";
                command.Parameters.AddWithValue("@ID_Potrawy", ID_Potrawy);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtPotrawy = new DataTable();

                connection.Open();
                sqlDA.Fill(dtPotrawy);
                connection.Close();

                foreach (DataRow dr in dtPotrawy.Rows)
                {
                    potrawyList.Add(new Potrawy
                    {
                        ID_Potrawy = Convert.ToInt32(dr["ID_Potrawy"]),
                        Nazwa_Potrawy = Convert.ToString(dr["Nazwa_Potrawy"]),
                        Cena = Convert.ToDecimal(dr["Cena"]),
                        Typ_Potrawy = Convert.ToString(dr["Typ_Potrawy"]),


                    });
                }

            }
            return potrawyList;
        }

        public bool UpdatePotrawy(Potrawy potrawy)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdatePotrawy", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_Potrawy", potrawy.ID_Potrawy);
                command.Parameters.AddWithValue("@Nazwa_Potrawy", potrawy.Nazwa_Potrawy);
                command.Parameters.AddWithValue("@Cena", potrawy.Cena);
                command.Parameters.AddWithValue("@Typ_Potrawy", potrawy.Typ_Potrawy);

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
        public string DeletePotrawy(int potrawyid)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeletePotrawy", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_Potrawy", potrawyid);
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

