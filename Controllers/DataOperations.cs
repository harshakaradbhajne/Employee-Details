using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System. Data;
using DataTableDemo.Models;

namespace DataTableDemo.Controllers
{
    public class DataOperations
    {
        private SqlConnection connect()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn1"].ConnectionString);
                conn.Open();
                return conn;

            }
            catch (Exception)
            {

                throw;
            }


        }
        public DataSet GetCountryData()
        {
            var conn = connect();
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_Employee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GETCOUNTRY");
                SqlDataAdapter da = new SqlDataAdapter();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet GetStateData(int CountryId)
        {
            var conn = connect();
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_Employee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CountryId", CountryId);
                cmd.Parameters.AddWithValue("@Action", "GETSTATEBYID");
                SqlDataAdapter da = new SqlDataAdapter();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<EmpModel> GetEmpData()
        {
            var conn = connect();
            List<EmpModel> emplist = new List<EmpModel>();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Employee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECTALL");
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    EmpModel emp = new EmpModel();
                    emp.id = Convert.ToInt32(dr["EmployeeId"].ToString());
                    emp.Name = dr["Name"].ToString();
                    emp.Age = Convert.ToInt32(dr["Age"].ToString());
                    emp.State = dr["StateName"].ToString();
                    emp.Country = dr["CountryName"].ToString();
                    emplist.Add(emp);



                }
                return emplist;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public DataSet GetEmpDatabyID(int empID)
        {
            var conn = connect();
            try
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_Employee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", empID);
                cmd.Parameters.AddWithValue("@Action", "GetEmpDatabyID");
                SqlDataAdapter da = new SqlDataAdapter();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int InsertUpdateEmp(EmpModel emp)
        {
            var conn = connect();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Employee", conn);
                cmd.CommandType = CommandType.StoredProcedure; 
               cmd.Parameters.AddWithValue("@id", emp.id);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Age", emp.Age);
                cmd.Parameters.AddWithValue("@StateId", emp.StateId);
                cmd.Parameters.AddWithValue("@CountryId", emp.CountryId);
                cmd.Parameters.AddWithValue("@Action", "INSERTUPDATE");
                int i = cmd.ExecuteNonQuery();
                return i;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public int DeleteEmployee(int ID)
        {
            var conn = connect();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Employee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", ID);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}