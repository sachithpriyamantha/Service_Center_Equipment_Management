using Service_Center_Equipment_Management.DAL.DataSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Center_Equipment_Management.DAL
{
    class DAL_CS_Bin
    {
        DataSource.DAL_DS_BinCard DAL_DS_BinCard = new DataSource.DAL_DS_BinCard();

        internal DataTable Select_Material()
        {
            DBconnect.connect();
            DAL_DS_BinCard ds = new DAL_DS_BinCard();
            DataTable dt = ds.tbl_mat;

            string query = @"SELECT 
                                PSM_LOC_CODE, 
                                PSM_MATERIAL_CODE,
                                mmspack.get_material_description( PSM_MATERIAL_CODE ) AS MatDesc,
                                mmspack.get_material_unit(PSM_MATERIAL_CODE ) AS UM,
                                PSM_BALANCE_QUANTITY, 
                                PSM_BALANCE_VALUE, 
                                PSM_LINE, 
                                PSM_AVERAGE_PRICE
                            FROM 
                                PMS_SERVICECENTER_MATERIALS ";
            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["loc"] = odr["PSM_LOC_CODE"].ToString();
                r["mCode"] = odr["PSM_MATERIAL_CODE"].ToString();
                r["unit"] = odr["UM"].ToString();
                r["desc"] = odr["MatDesc"].ToString();
                r["balqty"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PSM_BALANCE_QUANTITY"].ToString()) ? "0" : odr["PSM_BALANCE_QUANTITY"].ToString());
                r["balV"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PSM_BALANCE_VALUE"].ToString()) ? "0" : odr["PSM_BALANCE_VALUE"].ToString());
                r["line"] = odr["PSM_LINE"].ToString();
                r["avgP"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PSM_AVERAGE_PRICE"].ToString()) ? "0" : odr["PSM_AVERAGE_PRICE"].ToString());
                dt.Rows.Add(r);

            }
            odr.Close();
            return dt;
        }

        internal DataTable Select_Transaction(string MatCode)
        {
            DBconnect.connect();
            DAL_DS_BinCard ds = new DAL_DS_BinCard();
            DataTable dt = ds.tbl_trans;

            string query = @"SELECT 
                                    PST.PST_LOC_CODE,
                                    PST.PST_DOCUMENT_TYPE,
                                    PST.PST_DOCUMENT_NO,
                                    PST.PST_MATERIAL_CODE,
                                    mmspack.get_material_description(PST.PST_MATERIAL_CODE) AS MatDesc,
                                    TO_CHAR(PST.PST_DATE, 'YYYY-MM-DD') AS M_DATE,
                                    PST.PST_QUANTITY,
                                    PST.PST_AVERAGE_RATE,
                                    PST.PST_VALUE,
                                    PST.PST_BALANCE_QUANTITY,
                                    PST.PST_REMARKS,
                                    DECODE(PST.PST_CALCULATION_TYPE, 'A', 'Stores +', 'D', 'Stores -') AS cal,
                                    PST.PST_LINE,
                                    PST.PST_REFERENCE_NO,
                                    PST.PST_REFERENCE_TYPE,
                                    PSD.PSD_JCAT AS jcat,
                                    PSD.PSD_JMAIN AS jmain,
                                    PSD.PSD_SUB AS sub,
                                    PSD.PSD_SPEC AS spec,
                                    PSD.PSD_EXTC AS extc,
                                    PMD.pmd_description AS jobdesc
                                FROM 
                                    PMS_SERVICECENTER_TRANSACTIONS PST
                                LEFT JOIN 
                                    PMS_SERVICECENTER_DOCUMENTS  PSD
                                ON 
                                    PST.PST_LOC_CODE = PSD.PSD_LOC_CODE
                                    AND PST.PST_DOCUMENT_TYPE = PSD.PSD_DOCUMENT_TYPE
                                    AND PST.PST_DOCUMENT_NO = PSD.PSD_DOCUMENT_NO
                                LEFT JOIN 
                                    pms_mainjob_details PMD
                                ON 
                                    PSD.PSD_JCAT = PMD.pmd_jcat
                                    AND PSD.PSD_JMAIN = PMD.pmd_jmain
                                WHERE 
                                    PST.PST_MATERIAL_CODE = '"+MatCode+@"'
                                ORDER BY 
                                    PST.PST_LINE";
            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["loc"] = odr["PST_LOC_CODE"].ToString();
                r["dType"] = odr["PST_DOCUMENT_TYPE"].ToString();
                r["dNo"] = odr["PST_DOCUMENT_NO"].ToString();
                r["mCode"] = odr["PST_MATERIAL_CODE"].ToString();
                r["desc"] = odr["MatDesc"].ToString();
                r["date"] = odr["M_DATE"].ToString();
                r["qty"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PST_QUANTITY"].ToString()) ? "0" : odr["PST_QUANTITY"].ToString());
                r["avgRate"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PST_AVERAGE_RATE"].ToString()) ? "0" : odr["PST_AVERAGE_RATE"].ToString());
                r["value"] =  Convert.ToDouble(string.IsNullOrEmpty(odr["PST_VALUE"].ToString()) ? "0" : odr["PST_VALUE"].ToString());
                r["balqty"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PST_BALANCE_QUANTITY"].ToString()) ? "0" : odr["PST_BALANCE_QUANTITY"].ToString());
                r["remark"] = odr["PST_REMARKS"].ToString();
                r["calType"] = odr["cal"].ToString();
                r["line"] = odr["PST_LINE"].ToString();
                r["rno"] = odr["PST_REFERENCE_NO"].ToString();
                r["rtype"] = odr["PST_REFERENCE_TYPE"].ToString();
                r["jcat"] = odr["jcat"].ToString();
                r["jmain"] = odr["jmain"].ToString();
                r["sub"] = odr["sub"].ToString();
                r["spec"] = odr["spec"].ToString();
                r["extc"] = odr["extc"].ToString();
                r["jobDesc"] = odr["jobdesc"].ToString();
                dt.Rows.Add(r);

            }
            odr.Close();
            return dt;
        }


        internal DataTable Select_All_Transaction()
        {
            DBconnect.connect();
            DAL_DS_BinCard ds = new DAL_DS_BinCard();
            DataTable dt = ds.tbl_trans1;

            string query = @"SELECT 
                                    PST.PST_LOC_CODE,
                                    PST.PST_DOCUMENT_TYPE,
                                    PST.PST_DOCUMENT_NO,
                                    PST.PST_MATERIAL_CODE,
                                    mmspack.get_material_description(PST.PST_MATERIAL_CODE) AS MatDesc,
                                    TO_CHAR(PST.PST_DATE, 'YYYY-MM-DD') AS M_DATE,
                                    PST.PST_QUANTITY,
                                    PST.PST_AVERAGE_RATE,
                                    PST.PST_VALUE,
                                    PST.PST_BALANCE_QUANTITY,
                                    PST.PST_REMARKS,
                                    DECODE(PST.PST_CALCULATION_TYPE, 'A', 'Stores +', 'D', 'Stores -') AS cal,
                                    PST.PST_LINE,
                                    PST.PST_REFERENCE_NO,
                                    PST.PST_REFERENCE_TYPE,
                                    PSD.PSD_JCAT AS jcat,
                                    PSD.PSD_JMAIN AS jmain,
                                    PSD.PSD_SUB AS sub,
                                    PSD.PSD_SPEC AS spec,
                                    PSD.PSD_EXTC AS extc,
                                    PMD.pmd_description AS jobdesc
                                FROM 
                                    PMS_SERVICECENTER_TRANSACTIONS PST
                                LEFT JOIN 
                                    PMS_SERVICECENTER_DOCUMENTS  PSD
                                ON 
                                    PST.PST_LOC_CODE = PSD.PSD_LOC_CODE
                                    AND PST.PST_DOCUMENT_TYPE = PSD.PSD_DOCUMENT_TYPE
                                    AND PST.PST_DOCUMENT_NO = PSD.PSD_DOCUMENT_NO
                                LEFT JOIN 
                                    pms_mainjob_details PMD
                                ON 
                                    PSD.PSD_JCAT = PMD.pmd_jcat
                                    AND PSD.PSD_JMAIN = PMD.pmd_jmain
                                ORDER BY 
                                    PST.PST_LINE";
            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["loc"] = odr["PST_LOC_CODE"].ToString();
                r["dType"] = odr["PST_DOCUMENT_TYPE"].ToString();
                r["dNo"] = odr["PST_DOCUMENT_NO"].ToString();
                r["mCode"] = odr["PST_MATERIAL_CODE"].ToString();
                r["desc"] = odr["MatDesc"].ToString();
                r["date"] = odr["M_DATE"].ToString();
                r["qty"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PST_QUANTITY"].ToString()) ? "0" : odr["PST_QUANTITY"].ToString());
                r["avgRate"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PST_AVERAGE_RATE"].ToString()) ? "0" : odr["PST_AVERAGE_RATE"].ToString());
                r["value"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PST_VALUE"].ToString()) ? "0" : odr["PST_VALUE"].ToString());
                r["balqty"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PST_BALANCE_QUANTITY"].ToString()) ? "0" : odr["PST_BALANCE_QUANTITY"].ToString());
                r["remark"] = odr["PST_REMARKS"].ToString();
                r["calType"] = odr["cal"].ToString();
                r["line"] = odr["PST_LINE"].ToString();
                r["rno"] = odr["PST_REFERENCE_NO"].ToString();
                r["rtype"] = odr["PST_REFERENCE_TYPE"].ToString();
                r["jcat"] = odr["jcat"].ToString();
                r["jmain"] = odr["jmain"].ToString();
                r["sub"] = odr["sub"].ToString();
                r["spec"] = odr["spec"].ToString();
                r["extc"] = odr["extc"].ToString();
                r["jobDesc"] = odr["jobdesc"].ToString();
                dt.Rows.Add(r);

            }
            odr.Close();
            return dt;
        }

    }
}
