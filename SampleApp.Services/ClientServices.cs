using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.Model.Request;
using SampleApp.Model.ViewModel;
using System.Security.Principal;
using System.Data;

namespace SampleApp.Services
{
    public class ClientServices
    {

        public LoginViewModel Login(LoginAddRequest model)
        {
            LoginViewModel viewModel = new LoginViewModel();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    string sqlCmd = "ClientIsLoggedIn";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@Password", model.Password);

                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (reader.Read())
                        {
                            int index = 0;
                            viewModel.Id = reader.GetInt32(index++);
                            viewModel.FirstName = reader.GetString(index++);
                            viewModel.LastName = reader.GetString(index++);
                            viewModel.Email = reader.GetString(index++);

                            return viewModel;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public void Update(ClientAddRequest model)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "Client_Update";
                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter parm = new SqlParameter();

                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", model.LastName);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@Password", model.Password);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public int? Insert(ClientAddRequest model)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    string sqlCmd = "Client_Insert";
                    using (SqlCommand cmd = new SqlCommand(sqlCmd, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter parm = new SqlParameter();
                        parm.SqlDbType = System.Data.SqlDbType.Int;
                        parm.Direction = System.Data.ParameterDirection.Output;
                        parm.ParameterName = "@Id";

                        cmd.Parameters.Add(parm);
                        cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", model.LastName);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@Password", model.Password);

                        cmd.ExecuteNonQuery();

                        int id = (int)cmd.Parameters["@Id"].Value;

                        return id;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
