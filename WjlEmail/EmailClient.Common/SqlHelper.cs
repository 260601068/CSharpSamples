using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Common
{
    public class SqlHelper
    {
        static string connectionString = ConfigurationManager.AppSettings["connectionString"];
        public static object selectSingle(string sql,params SqlParameter[] cmdParms)
        {
            using (SqlConnection con=new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd=new SqlCommand(sql,con))
                {
                    foreach(SqlParameter param in cmdParms)
                    {
                        if (param.Value == null) param.Value = DBNull.Value;
                        cmd.Parameters.Add(param);
                    }
                    object obj = cmd.ExecuteScalar();
                    cmd.Dispose();
                    con.Close();
                    if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
                        return null;
                    else return obj;
                }
            }
        }

        public static DataSet Query(string sql)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(sql,con);
                sda.Fill(ds);
                return ds;
            }
        }
        public static DataSet Query(string sql,params SqlParameter[] cmdParms)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    foreach (SqlParameter param in cmdParms)
                    {
                        if (param.Value == null) param.Value = DBNull.Value;
                        cmd.Parameters.Add(param);
                    }
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        return ds;
                    }
                }
            }
        }
    }
}
