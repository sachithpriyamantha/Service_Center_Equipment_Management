using Service_Center_Equipment_Management.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Center_Equipment_Management.BLL
{
    class BLL_CS_TRN
    {
        DAL_CS_TRN DAL_CS_TRN = new DAL_CS_TRN();
        internal DataTable Search_Job_Details()
        {
            return DAL_CS_TRN.Search_Job_Details();
        }
        internal DataTable Search_Sub_Details(string jcat, string jmain)
        {
            return DAL_CS_TRN.Search_Sub_Details(jcat, jmain);
        }
        internal DataTable Search_Rec_No()
        {
            return DAL_CS_TRN.Search_Rec_No();
        }
        internal DataTable loadEmployees()
        {
            return DAL_CS_TRN.loadEmployees();
        }
        internal DataTable Select_Material_Code()
        {
            return DAL_CS_TRN.Select_Material_Code();
        }
        internal string Get_TRN_No()
        {
            return DAL_CS_TRN.Get_TRN_No();
        }
        internal DataTable getSearch_Trn_No(string trn, string code)
        {
            return DAL_CS_TRN.getSearch_Trn_No(trn, code);
        }
        internal DataTable getMatTrnNo(string trn)
        {
            return DAL_CS_TRN.getMatTrnNo(trn);
        }
        internal bool[] SaveDocumentDetails(string lCode, string trn, string dType, string date1, string jcat, string jmain, string sub, string spec, string extc, string ori, string oLoc)
        {
            oLoc = oLoc.Split('-')[0].Trim();
            return DAL_CS_TRN.SaveDocumentDetails(lCode, trn, dType, date1, jcat, jmain, sub, spec, extc, ori, oLoc);
        }
        internal bool[] SaveAssetTransactionDetails(string lCode, string dType, string trn,  string matCode, string qty, string bal, string remark, string date1, string avgRate, string refNo, string rType)
        {
            return DAL_CS_TRN.SaveAssetTransactionDetails(lCode, dType, trn, matCode, qty, bal, remark, date1, avgRate, refNo, rType);
        }
        internal DataTable Select_Transaction_Details(string mCode)//(string docNo, string type)
        {
            return DAL_CS_TRN.Select_Transaction_Details(mCode); //(docNo, type);
        }
    }

}
