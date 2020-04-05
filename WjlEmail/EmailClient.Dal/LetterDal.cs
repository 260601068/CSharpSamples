using EmailClient.Common;
using EmailClient.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Dal
{
    public class LetterDal
    {
        public int add(LetterModel letterModel)
        {
            string sql = "insert into letter(Title,Content,Receiver,AddTime) values(@Title,@Content,@Receiver,@AddTime);select @@identity";
            SqlParameter[] cmdParms={ new SqlParameter("@Title",letterModel.Title),new SqlParameter("@Content", letterModel.Content),
                new SqlParameter("@Receiver", letterModel.Receiver),new SqlParameter("@AddTime", letterModel.AddTime)};
            object obj = SqlHelper.selectSingle(sql, cmdParms);
            if (obj == null)
                return 0;
            else 
                return Convert.ToInt32(obj);
        }

        public DataSet GetListByPage(string where, string orderBy, int startIndex, int endIndex)
        {
            StringBuilder sb = new StringBuilder("select * from (select row_number() over(order by ");
            if (string.IsNullOrEmpty(orderBy))
                sb.Append("ID asc");
            else sb.Append(orderBy);
            sb.Append(") as Row,T.* from letter T ");
            if (!string.IsNullOrEmpty(where))
                sb.AppendFormat(" where {0}",where);
            sb.AppendFormat(")TT where TT.Row between {0} and {1}",startIndex,endIndex);
            return SqlHelper.Query(sb.ToString());
        }

        public DataSet GetList(string where)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ID,Title,Content,Receiver,AddTime from letter");
            if (where.Trim() != "")
                sb.Append(" where "+ where);
            return SqlHelper.Query(sb.ToString());
        }

        public LetterModel getModelById(int id)
        {
            string sql = "select ID,Title,Content,Receiver,AddTime from letter where id=@id";
            SqlParameter[] cmdparms = { new SqlParameter("@id", id) };
            DataSet ds = SqlHelper.Query(sql,cmdparms);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }

        private LetterModel DataRowToModel(DataRow row)
        {
            LetterModel letter = new LetterModel();
            if(row != null)
            {
                if(row["id"]!=null && row["id"].ToString() != "")
                {
                    letter.ID = Convert.ToInt32(row["id"]);
                }
                if(row["Title"] != null)
                {
                    letter.Title = row["Title"].ToString();
                }                
                if(row["Content"] != null)
                {
                    letter.Content = row["Content"].ToString();
                }                
                if(row["Receiver"] != null)
                {
                    letter.Receiver = row["Receiver"].ToString();
                }                
                if(row["AddTime"] != null && row["AddTime"].ToString() != null)
                {
                    letter.AddTime = Convert.ToDateTime(row["AddTime"].ToString());
                }
            }
            return letter;
        }
    }
}
