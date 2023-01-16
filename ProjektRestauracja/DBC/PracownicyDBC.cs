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
    public class PracownicyDBC
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        //Połączenie z bazą danych
        public List<Pracownicy> GetAllPracownicy()
        {
            List<Pracownicy> pracownicyList = new List<Pracownicy>();

                using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllPracownicy";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtPracownicy = new DataTable();

                connection.Open();
                sqlDA.Fill(dtPracownicy);
                connection.Close();

                foreach (DataRow dr in dtPracownicy.Rows)
                {
                    pracownicyList.Add(new Pracownicy
                    {
                        ID_Pracownika = Convert.ToInt32 (dr["ID Pracownika"]),
                        Imię = Convert.ToString(dr["Imię"]),
                        Nazwisko = Convert.ToString(dr["Nazwisko"]),
                        Stanowisko= Convert.ToString(dr["Stanowisko"]),
                        Data_Zatrudnienia = Convert.ToDateTime(dr["Data_Zatrudnienia"]),
                        Pensja = Convert.ToDecimal(dr["Pensja"]),
                        Płeć = Convert.ToString(dr["Płeć"]),

                    });
                }

            }

                return pracownicyList;
        }

        //Wprowadzanie danych do tabeli
        public bool insertPracownicy(Pracownicy pracownicy)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertPracownicy",connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Imię",pracownicy.Imię);
                command.Parameters.AddWithValue("@Nazwisko", pracownicy.Nazwisko);
                command.Parameters.AddWithValue("@Stanowisko", pracownicy.Stanowisko);
                command.Parameters.AddWithValue("@Data_Zatrudnienia", pracownicy.Data_Zatrudnienia);
                command.Parameters.AddWithValue("@Pensja", pracownicy.Pensja);
                command.Parameters.AddWithValue("@Płeć", pracownicy.Płeć);
                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if(id>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        public List<Pracownicy> EditPracownicy(int ID_Pracownika)
        {
            List<Pracownicy> pracownicyList = new List<Pracownicy>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_EditPracownicy";
                command.Parameters.AddWithValue("@ID_Pracownika",ID_Pracownika);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtPracownicy = new DataTable();

                connection.Open();
                sqlDA.Fill(dtPracownicy);
                connection.Close();

                foreach (DataRow dr in dtPracownicy.Rows)
                {
                    pracownicyList.Add(new Pracownicy
                    {
                        ID_Pracownika = Convert.ToInt32(dr["ID Pracownika"]),
                        Imię = Convert.ToString(dr["Imię"]),
                        Nazwisko = Convert.ToString(dr["Nazwisko"]),
                        Stanowisko = Convert.ToString(dr["Stanowisko"]),
                        Data_Zatrudnienia = Convert.ToDateTime(dr["Data_Zatrudnienia"]),
                        Pensja = Convert.ToDecimal(dr["Pensja"]),
                        Płeć = Convert.ToString(dr["Płeć"]),

                    });
                }

            }

            return pracownicyList;
        }

        //Edytowanie danych w tabeli 
        public bool UpdatePracownicy(Pracownicy pracownicy)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdatePracownicy", connection);
                command.CommandType = CommandType.StoredProcedure; 
                command.Parameters.AddWithValue("@ID_Pracownika", pracownicy.ID_Pracownika);
                command.Parameters.AddWithValue("@Imię", pracownicy.Imię);
                command.Parameters.AddWithValue("@Nazwisko", pracownicy.Nazwisko);
                command.Parameters.AddWithValue("@Stanowisko", pracownicy.Stanowisko);
                command.Parameters.AddWithValue("@Data_Zatrudnienia", pracownicy.Data_Zatrudnienia);
                command.Parameters.AddWithValue("@Pensja", pracownicy.Pensja);
                command.Parameters.AddWithValue("@Płeć", pracownicy.Płeć);
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

        //Usuwanie z tabeli
        public string DeletePracownicy(int pracownicyid)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_DeletePracownicy",connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_pracownika",pracownicyid);
                command.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar,50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OUTPUTMESSAGE"].Value.ToString();
                connection.Close();
            }

                return result;
        }

    }
}