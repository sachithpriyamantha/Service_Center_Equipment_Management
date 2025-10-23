using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Center_Equipment_Management.BLL
{
    class BLL_CS_REC
    {
        DAL.DAL_CS_REC DAL_CS_REC = new DAL.DAL_CS_REC();

        internal DataTable Select_Material_Code()
        {
            return DAL_CS_REC.Select_Material_Code();
        }
        internal DataTable Load_Select_Material_Code(string jcat, string jmain, string sub, string spec, string extc)
        {
            return DAL_CS_REC.Load_Select_Material_Code(jcat, jmain, sub, spec, extc);//anumi 04/21;
        }
        internal DataTable loadTOOL()
        {
            return DAL_CS_REC.loadTOOL();//anumi04/28
        }

        internal DataTable select_TOOL(string matcode)
        {
            return DAL_CS_REC.select_TOOL(matcode);//anumi05/02
        }
        internal DataTable Select_Transaction_Details(string docNo,  string type)
        {
            return DAL_CS_REC.Select_Transaction_Details(docNo, type);
        }

        internal bool[] SaveDocumentDetails(string wCode, string tdate1, string dType, string recNo, /*string refNo, string refType,*/ string dNo, string jcat, string jmain, string sub, string spec, string extc)
        {
            wCode = wCode.Split('-')[0].Trim();
            return DAL_CS_REC.SaveDocumentDetails(wCode, tdate1,  dType, recNo, /*refNo, refType,*/ dNo, jcat, jmain, sub, spec, extc);
        }

        //internal bool[] SaveDocumentDetails(string wCode, string pNo, string tdate, string loc, string dType, string recNo, string refNo, string refType)
        //{
        //    return DAL_CS_REC.SaveDocumentDetails(wCode, pNo, tdate, loc, dType, recNo, refNo, refType);
        //}

        internal bool[] SaveAssetTransactionDetails(string wCode/*lCode*/, string dType, string recNo/*string DNo, string Dtype*/, string matCode, string qty, string bal, string remark, string tool, string refNo, string line, string rType, string avgRate , string tdate1, string mrqQty)
        {
            wCode = wCode.Split('-')[0].Trim();
            return DAL_CS_REC.SaveAssetTransactionDetails(wCode/*lCode*/, /*DNo, Dtype*/dType, recNo, matCode, qty, bal, remark, tool, refNo, line, rType, avgRate, tdate1, mrqQty);
        }
        internal bool[] SaveMaterialDetails(string wCode /*loc*/, string matcode, string bal, string line, string count)
        {
            wCode = wCode.Split('-')[0].Trim();
            return DAL_CS_REC.SaveMaterialDetails(wCode /*loc*/, matcode, bal, line, count);
        }

        internal string Get_Rec_No()
        {
            return DAL_CS_REC.Get_Rec_No();
        }
        internal string[] Get_Edate_SDate()
        {
        //    string Sdate = "";
        //    string Edate = "";
            return DAL_CS_REC.Get_Edate_SDate();
        }

        internal DataTable Search_Rec_No()
        {
            return DAL_CS_REC.Search_Rec_No();
        }

        internal DataTable getSearchRecNo(string rec)
        {
            
            return DAL_CS_REC.getSearchRecNo(rec);
        }
        internal DataTable getMatRecNo(string rec)
        {
            return DAL_CS_REC.getMatRecNo(rec);
        }
        internal DataTable Search_Job_Details()
        {
            return DAL_CS_REC.Search_Job_Details();
        }
        internal DataTable Search_Sub_Details(string jcat, string jmain)
        {
            return DAL_CS_REC.Search_Sub_Details(jcat, jmain);
        }
    }
}
