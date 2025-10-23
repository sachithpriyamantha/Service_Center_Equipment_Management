using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Center_Equipment_Management.BLL
{
    class BLL_CS_Issue_Note
    {
        DAL.DAL_CS_Issue_Note DAL_CS_Issue_Note = new DAL.DAL_CS_Issue_Note();

        internal string GenerateTransferNo(string type)
        {
            string cmbReferenceType = "";
            string val = "";
            string finalVal = "";
            string[] array = val.Split('/');
            string first = array[0].Trim();

            if (type == "I")
            {
                if (first.Length == 1)
                {
                    finalVal = "I000" + val.Trim();
                }
                else if (first.Length == 2)
                {
                    finalVal = "I00" + val.Trim();
                }
                else if (first.Length == 3)
                {
                    finalVal = "I0" + val.Trim();
                }
            }

            else if (type == "R")
            {
                if (first.Length == 1)
                {
                    finalVal = "R000" + val.Trim();
                }
                else if (first.Length == 2)
                {
                    finalVal = "R00" + val.Trim();
                }
                else if (first.Length == 3)
                {
                    finalVal = "R0" + val.Trim();
                }
            }

            return finalVal;
        }
        internal DataTable Search_Job_Details()
        {
            return DAL_CS_Issue_Note.Search_Job_Details();
        }
        internal DataTable loadMobile()
        {
            return DAL_CS_Issue_Note.loadMobile();
        }
        internal DataTable Search_Sub_Details(string jcat, string jmain)
        {
            return DAL_CS_Issue_Note.Search_Sub_Details(jcat, jmain);
        }
        internal DataTable loadEmployees()
        {
            return DAL_CS_Issue_Note.loadEmployees();
        }
        internal DataTable Select_Transaction_Details(string mCode)//(string docNo, string type)
        {
            return DAL_CS_Issue_Note.Select_Transaction_Details(mCode); //(docNo, type);
        }
        internal string Get_TIN_No()
        {
            return DAL_CS_Issue_Note.Get_TIN_No();
        }
        internal DataTable Search_Tin_No()
        {
            return DAL_CS_Issue_Note.Search_Tin_No();
        }
        internal DataTable getSearch_Tin_No(string tin/*, string code*/)
        {
            return DAL_CS_Issue_Note.getSearch_Tin_No(tin/*, code*/);
        }
        internal DataTable getMatTinNo(string tin,string iType)
        {
            switch (iType)
            {
                case "R":
                    iType = "Returnable";
                    break;

                case "C":
                    iType = "Consumable";
                    break;
            }
            return DAL_CS_Issue_Note.getMatTinNo(tin, iType);
        }
        internal bool[] SaveDocumentDetails(string lCode, string tinNo, string dType, string date1, string jcat, string jmain, string sub, string spec, string extc, string ori, string oLoc, string remark)
        {
            // Split oLoc by the delimiter '-' and get the first part (trim whitespace if necessary)
            oLoc = oLoc.Split('-')[0].Trim();
            return DAL_CS_Issue_Note.SaveDocumentDetails(lCode, tinNo, dType, date1, jcat, jmain, sub, spec, extc, ori, oLoc, remark);
        }
        internal bool[] SaveAssetTransactionDetails(string lCode, string dType, string tinNo, string iType,  string matCode, string qty, string bal, string remarks,  string date1, string avgRate)
        {
            switch (iType)
            {
                case "Returnable":
                    iType = "R";
                    break;

                case "Consumable":
                    iType = "C";
                    break;
            }
            return DAL_CS_Issue_Note.SaveAssetTransactionDetails(lCode, dType, tinNo, iType,  matCode, qty, bal, remarks, date1,  avgRate);

        }
    }

}
