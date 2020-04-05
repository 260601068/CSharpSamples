using EmailClient.Dal;
using EmailClient.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.BLL
{
    public class LetterBll
    {
        public static bool DealWriter(LetterModel letterModel)
        {

            return new LetterDal().add(letterModel)!=0;
        }
        public static DataTable GetList(string where)
        {
            return new LetterDal().GetListByPage("", "ID asc", 1, 3).Tables[0];
        }
        public static DataTable GetListByPage(string where,string orderBy,int startIndex,int endIndex)
        {
            return new LetterDal().GetListByPage("","ID asc", startIndex, endIndex).Tables[0];
        }

        public static LetterModel GetModelById(int selectId)
        {
            return new LetterDal().getModelById(selectId);
        }
    }
}
