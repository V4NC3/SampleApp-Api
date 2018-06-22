using SampleApp.Model.Request;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public class ClientServices
    {

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
