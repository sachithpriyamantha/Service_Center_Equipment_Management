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
    class DAL_CS_Return
    {
        //no use 
        //DataSource.DAL_DS_Return DAL_DS_Return = new DataSource.DAL_DS_Return();
        DataSource.DAL_DS_Return DAL_DS_Return = new DataSource.DAL_DS_Return();

        internal DataTable Select_Tin_Code()
        {
            DBconnect.connect();
            DAL_DS_Return ds = new DAL_DS_Return();
            DataTable dt = ds.firstReturn;

            string query = @"Select
                                    FDD_DOCUMENT_NO,
                                    FDD_DOCUMENT_TYPE,
                                    TO_CHAR (FDD_DATE, 'YYYY-MM-DD') AS M_DATE,
                                    FDD_LOC_CODE, 
                                    FDD_ORIGINATOR,
                                    FDD_ORIGINATOR_LOCATION 
                                FROM FMS_DOCUMENT_DETAILS 
                               WHERE FDD_DOCUMENT_TYPE = 'TIN'
                               ORDER BY fdd_document_no";

            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["docNo"] = odr["FDD_DOCUMENT_NO"].ToString();
                r["dType"] = odr["FDD_DOCUMENT_TYPE"].ToString();
                r["tdate"] = odr["M_DATE"].ToString();
                r["loc"] = odr["FDD_LOC_CODE"].ToString();
                r["ori"] = odr["FDD_ORIGINATOR"].ToString();
                r["origiLoc"] = odr["FDD_ORIGINATOR_LOCATION"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }
        

        internal string GenerateTransferNo(string type)
        {
            DBconnect.connect();
            string transferNo_query = "";
            string val = "";
            string finalVal = "";

            //transferNo_query = "SELECT (LTRIM(TO_CHAR(NVL(MAX(TO_NUMBER(SUBSTR(fat_transfernote_no,4,4),'9999')),0)+1,'0000'))||'/'||TO_CHAR(SYSDATE,'RR'))AS transferNoteNo "
            //                + " FROM fms_asset_transactions "
            //                + " WHERE TO_CHAR(fat_transfer_date,'RRRR') = TO_CHAR(SYSDATE,'RRRR') ";

            //transferNo_query = "SELECT (NVL(MAX(substr(fat_transfernote_no,0,4)),0)+1 ||'/'||TO_CHAR(SYSDATE,'YY')) max FROM fms_asset_transactions WHERE fat_year = TO_CHAR(SYSDATE,'RRRR') ";

            transferNo_query = "SELECT (NVL(MAX(substr(fat_transfernote_no, 2, 4)), 0) + 1|| '/' || TO_CHAR(SYSDATE, 'YY')) max "
                            + " FROM fms_asset_transactions "
                            + " WHERE fat_year = TO_CHAR(SYSDATE, 'RRRR') AND SUBSTR(fat_transfernote_no,1,1) = '" + type + "'";

            OracleDataReader odr = DBconnect.readtable(transferNo_query);
            while (odr.Read())
            {
                val = odr["max"].ToString().Trim();

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
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return finalVal;
        }

        internal string GenerateLineNo(string transferNo)
        {
            string val = "";
            DBconnect.connect();
            string lineNo_query = "";
            lineNo_query = "SELECT (NVL(MAX(fad_transaction_no),0) + 1)AS maxLine  "
                        + " FROM fms_assettranscation_details "
                        + " WHERE fad_transfernote_no = '" + transferNo + "' ";
            OracleDataReader odr = DBconnect.readtable(lineNo_query);
            if (odr.Read())
            {
                val = odr["maxLine"].ToString();
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return val;
        }

        internal string GetResponsibleOfficerWorkCat(string serviceNo)
        {
            string val = "";
            DBconnect.connect();
            string workcat_query = "";
            workcat_query = "SELECT ced_work_category "
                        + " FROM employee_details "
                        + " WHERE ced_service_no = '" + serviceNo + "' "
                        + " AND ced_newold = 'A' ";
            OracleDataReader odr = DBconnect.readtable(workcat_query);
            if (odr.Read())
            {
                val = odr["ced_work_category"].ToString();
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return val;
        }

        internal DataTable loadProjects(string jcat)
        {
            DAL_DS_Return.Tables["JobDetails"].Clear();
            DBconnect.connect();
            string loadprojects = "";
            loadprojects = "SELECT pmd_jcat, pmd_jmain, "
                               + " pmd_description "
                        + " FROM pms_mainjob_details "
                        + " WHERE pmd_status = 'A' "
                        + " AND pmd_jcat = '" + jcat + "' "
                        + " ORDER BY pmd_jcat ";
            OracleDataReader odr = DBconnect.readtable(loadprojects);
            while (odr.Read())
            {
                DataRow drow = DAL_DS_Return.JobDetails.NewRow();
                drow["jcat"] = odr["pmd_jcat"].ToString();
                drow["jmain"] = odr["pmd_jmain"].ToString();
                drow["des"] = odr["pmd_description"].ToString();
                DAL_DS_Return.JobDetails.Rows.Add(drow);
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Return.Tables["JobDetails"];
        }

        internal DataTable loadLocations()
        {
            DAL_DS_Return.Tables["LocationDetails"].Clear();
            DBconnect.connect();
            string loadloc = "";
            loadloc = "SELECT hld_loc_code, hld_loc_desc "
                        + " FROM hrm_location_details "
                        + " WHERE hld_current_status = 'A' "
                        + " ORDER BY hld_loc_code ";
            OracleDataReader odr = DBconnect.readtable(loadloc);
            while (odr.Read())
            {
                DataRow drow = DAL_DS_Return.LocationDetails.NewRow();
                drow["locCode"] = odr["hld_loc_code"].ToString();
                drow["locDes"] = odr["hld_loc_desc"].ToString();
                DAL_DS_Return.LocationDetails.Rows.Add(drow);
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Return.Tables["LocationDetails"];
        }

        internal DataTable loadEmployees()
        {
            DAL_DS_Return.Tables["Employees"].Clear();
            DBconnect.connect();
            string loadEmployees = "";
            loadEmployees = "SELECT ced_service_no, ced_report_name,ced_location_code,(cdlpack.Get_Location_Name(ced_location_code))AS locdes,ced_barcode_cardno "
                         // + " (DECODE(ced_first_employer,'1','CDL-Permanent','2','CDL-Contract','3','DGES-Permanent','4','DGES-Contract','5','CDL-Training','6','DGES-Training',  '7','CDL-Consultancy','8','Traing Facility'))AS ced_first_employer "
                         + " FROM employee_details "
                         + " WHERE ced_newold = 'A' "
                         + " AND ced_work_category = 'E' "
                         + " AND ced_division_code = 'DPR' "
                         + " ORDER BY ced_service_no";
            OracleDataReader odr = DBconnect.readtable(loadEmployees);
            while (odr.Read())
            {
                DataRow drow = DAL_DS_Return.Employees.NewRow();
                drow["service"] = odr["ced_service_no"].ToString();
                drow["name"] = odr["ced_report_name"].ToString();
                drow["loc"] = odr["ced_location_code"].ToString() + " - " + odr["locdes"].ToString();
                drow["barcode"] = odr["ced_barcode_cardno"].ToString();
                DAL_DS_Return.Employees.Rows.Add(drow);
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Return.Tables["Employees"];
        }


        internal DataTable LoadIssuedItems()
        {
            DAL_DS_Return.Tables["IssueItems"].Clear();
            DBconnect.connect();
            string loadIssuedItems = "";
            loadIssuedItems = "SELECT fat_transfernote_no,fat_iwo_no, fat_jcat, fat_jamin, "
                                + " (SELECT pmd_description FROM pms_mainjob_details WHERE pmd_jcat = fat_jcat AND pmd_jmain = fat_jamin)jobdes, "
                                + " (fat_responsible_officer || ' - ' || (SELECT ced_report_name FROM employee_details WHERE ced_service_no = fat_responsible_officer))AS fat_responsible_officer, "
                                + " (fat_officer_incharge || ' - ' || (SELECT ced_report_name FROM employee_details WHERE ced_service_no = fat_officer_incharge))AS fat_officer_incharge, "
                                + " (SELECT DISTINCT(fad_transfering_to) FROM fms_assettranscation_details WHERE fad_transfernote_no = fat_transfernote_no)AS origiLoc "
                            //+ " ((SELECT DISTINCT(fad_transfering_to) FROM fms_assettranscation_details WHERE fad_transfernote_no = fat_transfernote_no) || ' - ' || (cdlpack.Get_Location_Name(SELECT DISTINCT(fad_transfering_to) FROM fms_assettranscation_details WHERE fad_transfernote_no = fat_transfernote_no)))AS origiLoc "
                            + " FROM fms_asset_transactions "
                            + " WHERE SUBSTR(fat_transfernote_no,0,1) IN('I') "
                            + " AND fat_type NOT IN('R')"
                            + " ORDER BY fat_transfernote_no";
            OracleDataReader odr = DBconnect.readtable(loadIssuedItems);
            while (odr.Read())
            {
                DataRow drow = DAL_DS_Return.IssueItems.NewRow();
                drow["issueno"] = odr["fat_transfernote_no"].ToString();
                drow["iwo"] = odr["fat_iwo_no"].ToString();
                drow["jcat"] = odr["fat_jcat"].ToString();
                drow["jmain"] = odr["fat_jamin"].ToString();
                drow["des"] = odr["jobdes"].ToString();
                drow["reofficer"] = odr["fat_responsible_officer"].ToString();
                drow["offincharge"] = odr["fat_officer_incharge"].ToString();
                drow["origiloc"] = odr["origiLoc"].ToString();
                DAL_DS_Return.IssueItems.Rows.Add(drow);
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Return.Tables["IssueItems"];
        }


        internal DataTable LoadRetunItems()
        {
            DAL_DS_Return.Tables["IssueItems"].Clear();
            DBconnect.connect();
            string loadIssuedItems = "";
            loadIssuedItems = "SELECT fat_transfernote_no,fat_iwo_no, fat_jcat, fat_jamin, "
                                + " (SELECT pmd_description FROM pms_mainjob_details WHERE pmd_jcat = fat_jcat AND pmd_jmain = fat_jamin)jobdes, "
                                + " (fat_responsible_officer || ' - ' || (SELECT ced_report_name FROM employee_details WHERE ced_service_no = fat_responsible_officer))AS fat_responsible_officer, "
                                + " (fat_officer_incharge || ' - ' || (SELECT ced_report_name FROM employee_details WHERE ced_service_no = fat_officer_incharge))AS fat_officer_incharge "
                            //   + " (SELECT DISTINCT(fad_transfering_to) FROM fms_assettranscation_details WHERE fad_transfernote_no = fat_transfernote_no)AS origiLoc "
                            /////   //+ " ((SELECT DISTINCT(fad_transfering_to) FROM fms_assettranscation_details WHERE fad_transfernote_no = fat_transfernote_no) || ' - ' || (cdlpack.Get_Location_Name(SELECT DISTINCT(fad_transfering_to) FROM fms_assettranscation_details WHERE fad_transfernote_no = fat_transfernote_no)))AS origiLoc "
                            + " FROM fms_asset_transactions "
                            + " WHERE SUBSTR(fat_transfernote_no,0,1) IN('R') "
                            + " AND fat_type NOT IN('I')"
                            + " ORDER BY fat_transfernote_no";
            OracleDataReader odr = DBconnect.readtable(loadIssuedItems);
            while (odr.Read())
            {
                DataRow drow = DAL_DS_Return.IssueItems.NewRow();
                drow["issueno"] = odr["fat_transfernote_no"].ToString();
                drow["iwo"] = odr["fat_iwo_no"].ToString();
                drow["jcat"] = odr["fat_jcat"].ToString();
                drow["jmain"] = odr["fat_jamin"].ToString();
                drow["des"] = odr["jobdes"].ToString();
                drow["reofficer"] = odr["fat_responsible_officer"].ToString();
                drow["offincharge"] = odr["fat_officer_incharge"].ToString();
                // drow["origiloc"] = odr["origiLoc"].ToString();
                DAL_DS_Return.IssueItems.Rows.Add(drow);
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Return.Tables["IssueItems"];
        }



        internal DataTable loadResponsibilityHolder(string loc)
        {
            DAL_DS_Return.Tables["Holder"].Clear();
            DBconnect.connect();
            string loadEmployees = "";
            loadEmployees = "SELECT ced_service_no, ced_report_name,ced_location_code,(cdlpack.Get_Location_Name(ced_location_code))AS locdes,ced_barcode_cardno "
                         + " FROM employee_details "
                         + " WHERE ced_newold = 'A' "
                         + " AND ced_work_category IN('S','I') "
                         + " AND ced_location_code = '" + loc + "' "
                         + " ORDER BY ced_service_no";
            OracleDataReader odr = DBconnect.readtable(loadEmployees);
            while (odr.Read())
            {
                DataRow drow = DAL_DS_Return.Holder.NewRow();
                drow["service"] = odr["ced_service_no"].ToString();
                drow["name"] = odr["ced_report_name"].ToString();
                drow["loc"] = odr["ced_location_code"].ToString() + " - " + odr["locdes"].ToString();
                drow["barcode"] = odr["ced_barcode_cardno"].ToString();
                DAL_DS_Return.Holder.Rows.Add(drow);
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Return.Tables["Holder"];
        }

        internal DataTable loadFirst()
        {
            DAL_DS_Return.Tables["first"].Clear();
            DBconnect.connect();
            string loadQuery = "";
            loadQuery = "SELECT fca_main||'-'||fca_sub AS AssetCode, "
                                    + " fca_description, "
                                    + " fca_loc_code , "
                                    + " fca_make , "
                                    + " fca_model, "
                                    + " fca_serial_no,  "
                                    + " fca_material_code , "
                                    + " mmspack.get_material_description(fca_material_code) AS MatDesc, "
                                    + " mmspack.get_material_unit(fca_material_code) AS UM "
                        + " FROM fms_cdl_assets "
                        + " WHERE fca_cur_stat = 'U' "
                        + " AND SUBSTR(fca_main,1,2) IN('13') "
                        + " AND fca_account_code NOT IN('21112') "
                        + " AND (fca_main||'-'||fca_sub) NOT IN(SELECT (fad_main||'-'||fad_sub)AS assetCode "
                                                            + " FROM fms_assettranscation_details) ";

            OracleDataReader odr = DBconnect.readtable(loadQuery);
            while (odr.Read())
            {
                DataRow drow = DAL_DS_Return.first.NewRow();
                drow["select"] = false;
                drow["asCode"] = odr["AssetCode"].ToString();
                drow["des"] = odr["fca_description"].ToString();
                drow["asLoc"] = odr["fca_loc_code"].ToString();
                drow["make"] = odr["fca_make"].ToString();
                drow["model"] = odr["fca_model"].ToString();
                drow["serial"] = odr["fca_serial_no"].ToString();
                drow["matCode"] = odr["fca_material_code"].ToString();
                drow["matDes"] = odr["MatDesc"].ToString();
                drow["unit"] = odr["UM"].ToString();
                DAL_DS_Return.first.Rows.Add(drow);
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Return.Tables["first"];
        }

        internal DataTable loadSecond()
        {
            DAL_DS_Return.Tables["second"].Clear();
            DBconnect.connect();
            string loadQuery = "";
            loadQuery = "SELECT fca_main||'-'||fca_sub AS AssetCode, "
                                + " fca_description , "
                                + " fca_loc_code , "
                                + " fca_make , "
                                + " fca_model, "
                                + " fca_serial_no , "
                                + " fca_material_code, "
                                + " mmspack.get_material_description(fca_material_code) AS MatDesc, "
                                + " mmspack.get_material_unit(fca_material_code) AS UM "
                        + " FROM fms_cdl_assets "
                        + " WHERE fca_cur_stat = 'U' "
                        + " AND SUBSTR(fca_main,1,2) IN('15') "
                        + " AND fca_account_code NOT IN('21112') "
                        + " AND (fca_main||'-'||fca_sub) NOT IN(SELECT (fad_main||'-'||fad_sub)AS assetCode "
                                                            + " FROM fms_assettranscation_details) ";

            OracleDataReader odr = DBconnect.readtable(loadQuery);
            while (odr.Read())
            {
                DataRow drow = DAL_DS_Return.second.NewRow();
                drow["select"] = false;
                drow["asCode"] = odr["AssetCode"].ToString();
                drow["des"] = odr["fca_description"].ToString();
                drow["asLoc"] = odr["fca_loc_code"].ToString();
                drow["make"] = odr["fca_make"].ToString();
                drow["model"] = odr["fca_model"].ToString();
                drow["serial"] = odr["fca_serial_no"].ToString();
                drow["matCode"] = odr["fca_material_code"].ToString();
                drow["matDes"] = odr["MatDesc"].ToString();
                drow["unit"] = odr["UM"].ToString();
                DAL_DS_Return.second.Rows.Add(drow);
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Return.Tables["second"];
        }

        internal DataTable loadFirstReturn()
        {
            DAL_DS_Return.Tables["firstReturn"].Clear();
            DBconnect.connect();
            string loadQuery = "";
            loadQuery = "SELECT fad_transfernote_no, fad_transaction_no, "
                    + " (fad_main || '-' || fad_sub)asCode, fad_transfering_from,fad_transfering_to, fad_remarks, "
                    + " (SELECT fca_description FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub))AS assetDes, "
                    + " (SELECT fca_material_code FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub))AS matCode, "
                    + " (SELECT fca_make FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub))AS make, "
                    + " (SELECT fca_model FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub))AS model, "
                    + " (SELECT fca_serial_no FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub))AS serial, "
                    + " (SELECT fat_iwo_no FROM fms_asset_transactions WHERE fat_transfernote_no = fad_transfernote_no)AS iwo "
                    //--(mmspack.get_material_description((SELECT fca_material_code FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub)))AS MatDesc
                    + " FROM FMS_ASSETTRANSCATION_DETAILS "
                    + " WHERE SUBSTR(fad_main,1,2) IN('13') "
                    + " AND fad_reftransfernote_no IS NULL "
                    // + " AND (fca_main||'-'||fca_sub) NOT IN(SELECT (fad_main||'-'||fad_sub)AS assetCode  FROM fms_assettranscation_details WHERE fad_tools_condition IN ('1','2','4','5','6')) "
                    + " ORDER BY fad_transfernote_no ";

            OracleDataReader odr = DBconnect.readtable(loadQuery);
            while (odr.Read())
            {
                DataRow drow = DAL_DS_Return.firstReturn.NewRow();
                drow["select"] = false;
                drow["asCode"] = odr["asCode"].ToString();
                drow["issueno"] = odr["fad_transfernote_no"].ToString();
                drow["des"] = odr["assetDes"].ToString();
                drow["asLoc"] = odr["fad_transfering_from"].ToString();
                drow["origiLoc"] = odr["fad_transfering_to"].ToString();
                drow["make"] = odr["make"].ToString();
                drow["model"] = odr["model"].ToString();
                drow["serial"] = odr["serial"].ToString();
                drow["matCode"] = odr["matCode"].ToString();
                drow["tran"] = odr["fad_transaction_no"].ToString();
                drow["iwo"] = odr["iwo"].ToString();
                DAL_DS_Return.firstReturn.Rows.Add(drow);
            }

            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Return.Tables["firstReturn"];
        }

        internal DataTable loadSecondReturn()
        {
            DAL_DS_Return.Tables["secondReturn"].Clear();
            DBconnect.connect();
            string loadQuery = "";
            loadQuery = "SELECT fad_transfernote_no, fad_transaction_no, (fad_main || '-' || fad_sub)asCode, fad_transfering_from,fad_transfering_to, fad_remarks, "
                 + " (SELECT fca_description FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub))AS assetDes, "
                 + " (SELECT fca_material_code FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub))AS matCode, "
                 + " (SELECT fca_make FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub))AS make, "
                 + " (SELECT fca_model FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub))AS model, "
                 + " (SELECT fca_serial_no FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub))AS serial, "
                 + " (SELECT fat_iwo_no FROM fms_asset_transactions WHERE fat_transfernote_no = fad_transfernote_no)AS iwo "
                 //--(mmspack.get_material_description((SELECT fca_material_code FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub)))AS MatDesc
                 + " FROM FMS_ASSETTRANSCATION_DETAILS "
                 + " WHERE SUBSTR(fad_main,1,2) IN('15') "
                 + " AND fad_reftransfernote_no IS NULL"
                 //                  + " AND (fca_main||'-'||fca_sub) NOT IN(SELECT (fad_main||'-'||fad_sub)AS assetCode  FROM fms_assettranscation_details WHERE fad_tools_condition IN ('1','2','4','5','6')) "

                 + " ORDER BY fad_transfernote_no ";

            OracleDataReader odr = DBconnect.readtable(loadQuery);
            while (odr.Read())
            {
                DataRow drow = DAL_DS_Return.secondReturn.NewRow();
                drow["select"] = false;
                drow["asCode"] = odr["asCode"].ToString();
                drow["issueno"] = odr["fad_transfernote_no"].ToString();
                drow["des"] = odr["assetDes"].ToString();
                drow["asLoc"] = odr["fad_transfering_from"].ToString();
                drow["origiLoc"] = odr["fad_transfering_to"].ToString();
                drow["make"] = odr["make"].ToString();
                drow["model"] = odr["model"].ToString();
                drow["serial"] = odr["serial"].ToString();
                drow["matCode"] = odr["matCode"].ToString();
                drow["iwo"] = odr["iwo"].ToString();
                DAL_DS_Return.secondReturn.Rows.Add(drow);
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Return.Tables["secondReturn"];
        }

        internal DataTable loadIWODetails(string iwono)
        {
            DAL_DS_Return.Tables["IWO"].Clear();
            DBconnect.connect();
            string loadiwodetails = "";
            loadiwodetails = "SELECT pid_jcat, pid_jmain, "
                                + " (pid_orgloc_code || ' - ' || (SELECT hld_loc_desc FROM hrm_location_details WHERE hld_loc_code = pid_orgloc_code AND hld_current_status = 'A'))AS pid_orgloc_code,  "
                                + " officer_incharge, "
                                + " (officer_incharge || ' - ' || (SELECT ced_report_name FROM employee_details WHERE ced_service_no = officer_incharge AND ced_newold = 'A'))AS officer, "
                                + " pmspack.get_mainjob_description(pid_jcat,pid_jmain)jobDes, "
                                + " (SELECT fat_transfernote_no FROM FMS_ASSET_TRANSACTIONS WHERE fat_iwo_no =  '" + iwono + "')AS issueno, "
                               // + " (SELECT fat_responsible_officer FROM FMS_ASSET_TRANSACTIONS WHERE fat_iwo_no = '12988')AS resOfficer "
                               + " ((SELECT fat_responsible_officer FROM FMS_ASSET_TRANSACTIONS WHERE fat_iwo_no = '" + iwono + "') || ' - ' || (SELECT ced_report_name FROM employee_details WHERE ced_service_no = (SELECT fat_responsible_officer FROM FMS_ASSET_TRANSACTIONS WHERE fat_iwo_no = '" + iwono + "') AND ced_newold = 'A'))AS resOfficer "
                        + " FROM pms_iwo_details "
                        + " WHERE pid_iwo_no = '" + iwono + "' ";
            OracleDataReader odr = DBconnect.readtable(loadiwodetails);
            while (odr.Read())
            {
                DataRow drow = DAL_DS_Return.IWO.NewRow();
                drow["jcat"] = odr["pid_jcat"].ToString();
                drow["jmain"] = odr["pid_jmain"].ToString();
                drow["origiloc"] = odr["pid_orgloc_code"].ToString();
                drow["resperson"] = odr["officer"].ToString();
                drow["des"] = odr["jobDes"].ToString();
                drow["service"] = odr["officer_incharge"].ToString();
                drow["issueno"] = odr["issueno"].ToString();
                drow["resOfficer"] = odr["resOfficer"].ToString();
                DAL_DS_Return.IWO.Rows.Add(drow);
            }
            if (!odr.HasRows)
            {
                DataRow drow = DAL_DS_Return.IWO.NewRow();
                DAL_DS_Return.IWO.Rows.Add(drow);
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Return.Tables["IWO"];
        }

        internal DataTable LoadTransactionsIssued(string issueNo)
        {
            DAL_DS_Return.Tables["SelectedMaterialsReturn"].Clear();
            DBconnect.connect();
            string loadQuery = "";
            loadQuery = "SELECT fad_transfernote_no, fad_transaction_no, fad_transfering_from, fad_reftransfernote_no,fad_tools_condition,"
                            + " (fad_main || '-' || fad_sub)AS assetCode, fad_remarks, "
                            + " (SELECT fca_description FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub))AS assetDes, "
                            + " (SELECT fca_material_code FROM fms_cdl_assets WHERE (fca_main||'-'||fca_sub) = (fad_main || '-' || fad_sub))AS matCode "
                    + " FROM FMS_ASSETTRANSCATION_DETAILS "
                    + " WHERE fad_transfernote_no = '" + issueNo + "' ";
            //+ " AND fad_reftransfernote_no IS NULL ";

            OracleDataReader odr = DBconnect.readtable(loadQuery);
            while (odr.Read())
            {
                DataRow drow = DAL_DS_Return.SelectedMaterialsReturn.NewRow();
                drow["issueno"] = odr["fad_transfernote_no"].ToString();
                drow["tran"] = odr["fad_transaction_no"].ToString();
                drow["asCode"] = odr["assetCode"].ToString();
                drow["remarks"] = odr["fad_remarks"].ToString();
                drow["des"] = odr["assetDes"].ToString();
                drow["matCode"] = odr["matCode"].ToString();
                drow["asLoc"] = odr["fad_transfering_from"].ToString();
                drow["lineNo"] = odr["fad_reftransfernote_no"].ToString();
                drow["toolcon"] = odr["fad_tools_condition"].ToString();

                if (odr["fad_tools_condition"].ToString() == "3")
                    drow["toolcon"] = "Good";
                else if (odr["fad_tools_condition"].ToString() == "2")
                    drow["toolcon"] = "Damage";
                else if (odr["fad_tools_condition"].ToString() == "1")
                    drow["toolcon"] = "Lost";
                else if (odr["fad_tools_condition"].ToString() == "4")
                    drow["toolcon"] = "Worn Out";
                else if (odr["fad_tools_condition"].ToString() == "5")
                    drow["toolcon"] = "Damage - Repairable";
                else if (odr["fad_tools_condition"].ToString() == "6")
                    drow["toolcon"] = "Damage - Non Repairable";





                DAL_DS_Return.SelectedMaterialsReturn.Rows.Add(drow);
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Return.Tables["SelectedMaterialsReturn"];
        }



        internal bool[] SaveAssetTransactions(string year, string issueno, string jcat, string jmain, string resofficer, string tdate, string status, string remarks, string resHolder, string cat, string type)
        {
            Boolean[] result = new Boolean[2];
            Boolean save = false;
            Boolean update = false;
            DBconnect.connect();

            string update_query = "";
            string insert_Query = "";

            update_query = "UPDATE FMS_ASSET_TRANSACTIONS "
                        + " SET fat_transfer_date = TO_DATE('" + tdate + "','RRRR-MM-DD'), "
                            + " fat_jcat = '" + jcat + "', "
                            + " fat_jamin = '" + jmain + "', "
                            + " fat_responsibility_holder = '" + cat + "', "
                            + " fat_responsible_officer = '" + resHolder + "' , "
                            + " fat_officer_incharge = '" + resofficer + "', "
                            + " fat_status = '" + status + "', "
                            + " fat_remarks = '" + remarks + "', "
                            + " fat_type = '" + type + "', "
                            + " updated_by ='" + Connection.UserName + "', "
                            + " updated_date = SYSDATE "
                        + " WHERE fat_year = '" + year + "' "
                        + " AND fat_transfernote_no = '" + issueno + "' ";

            update = DBconnect.AddEditDel(update_query);

            if (!update)
            {
                insert_Query = "INSERT INTO FMS_ASSET_TRANSACTIONS"
                                             + " (fat_year, "
                                             + " fat_transfernote_no, "
                                             + " fat_jcat, "
                                             + " fat_jamin, "
                                             + " fat_responsibility_holder, "
                                             + " fat_responsible_officer, "
                                             + " fat_officer_incharge, "
                                             + " fat_transfer_date, "
                                             + " fat_status, "
                                             + " fat_remarks,"
                                             + " fat_type, "
                                             + " created_by, "
                                             + " crated_date) "
                               + " VALUES ("
                                             + " '" + year + "', "
                                             + " '" + issueno + "', "
                                             + " '" + jcat + "',"
                                             + " '" + jmain + "', "
                                             + " '" + cat + "', "
                                             + " '" + resHolder + "', "
                                             + " '" + resofficer + "', "
                                             + " TO_DATE('" + tdate + "','RRRR-MM-DD'),"
                                             + " '" + status + "', "
                                             + " '" + remarks + "', "
                                             + " '" + type + "', "
                                             + " '" + Connection.UserName + "', "
                                             + " SYSDATE)";

                save = DBconnect.AddEditDel(insert_Query);
            }

            result[0] = update;
            result[0] = save;
            return result;
        }

        internal bool[] SaveAssetTransactionDetails(string year, string issueno, string main, string sub, string status, string remarks, string transactionno, string asLocCode, string origiLoc)
        {
            Boolean[] result = new Boolean[2];
            Boolean save = false;
            Boolean update = false;
            DBconnect.connect();

            string update_query = "";
            string insert_Query = "";

            update_query = "UPDATE fms_assettranscation_details "
                        + " SET  "
                            + " fad_main = '" + main + "', "
                            + " fad_sub = '" + sub + "', "
                            + " fad_transfering_from = '" + asLocCode + "' , "
                            + " fad_transfering_to = '" + origiLoc + "', "
                            + " fad_status = '" + status + "', "
                            + " fad_remarks = '" + remarks + "', "
                            + " update_by ='" + Connection.UserName + "', "
                            + " update_date = SYSDATE "
                        + " WHERE fad_year = '" + year + "' "
                        + " AND fad_transfernote_no = '" + issueno + "' "
                        + " AND fad_transaction_no = '" + transactionno + "' ";

            update = DBconnect.AddEditDel(update_query);

            if (!update)
            {
                insert_Query = "INSERT INTO fms_assettranscation_details"
                                             + " (fad_year, "
                                             + " fad_transfernote_no, "
                                             + " fad_transaction_no, "
                                             + " fad_main, "
                                             + " fad_sub, "
                                             + " fad_transfering_from, "
                                             + " fad_transfering_to, "
                                             + " fad_status, "
                                             + " fad_remarks, "
                                             + " created_by, "
                                             + " created_date) "
                               + " VALUES ("
                                             + " '" + year + "', "
                                             + " '" + issueno + "', "
                                             + " '" + transactionno + "', "
                                             + " '" + main + "',"
                                             + " '" + sub + "', "
                                             + " '" + asLocCode + "', "
                                             + " '" + origiLoc + "', "
                                             + " '" + status + "', "
                                             + " '" + remarks + "', "
                                             + " '" + Connection.UserName + "', "
                                             + " SYSDATE)";

                save = DBconnect.AddEditDel(insert_Query);
            }

            result[0] = update;
            result[0] = save;
            return result;
        }

        internal bool[] SaveAssetTransactionDetailsReturn(string year, string issueno, string main, string sub, string status, string remarks, string transactionno, string asLocCode, string origiLoc, string refNoteNo, string reftranNo, string toolcondi)
        {
            Boolean[] result = new Boolean[2];
            Boolean save = false;
            Boolean update = false;
            DBconnect.connect();

            string update_query = "";
            string insert_Query = "";

            update_query = "UPDATE fms_assettranscation_details "
                        + " SET  "
                            + " fad_main = '" + main + "', "
                            + " fad_sub = '" + sub + "', "
                            + " fad_transfering_from = '" + asLocCode + "' , "
                            + " fad_transfering_to = '" + origiLoc + "', "
                            + " fad_status = '" + status + "', "
                            + " fad_remarks = '" + remarks + "', "
                            + " fad_ref_year = '" + year + "', "
                            + " fad_reftransfernote_no = '" + refNoteNo + "', "
                            + " fad_reftransaction_no = '" + reftranNo + "', "
                            + " fad_tools_condition = '" + toolcondi + "', "
                            + " update_by ='" + Connection.UserName + "', "
                            + " update_date = SYSDATE "
                        + " WHERE fad_year = '" + year + "' "
                        + " AND fad_transfernote_no = '" + issueno + "' "
                        + " AND fad_transaction_no = '" + transactionno + "' ";

            update = DBconnect.AddEditDel(update_query);

            if (!update)
            {
                insert_Query = "INSERT INTO fms_assettranscation_details"
                                             + " (fad_year, "
                                             + " fad_transfernote_no, "
                                             + " fad_transaction_no, "
                                             + " fad_main, "
                                             + " fad_sub, "
                                             + " fad_transfering_from, "
                                             + " fad_transfering_to, "
                                             + " fad_status, "
                                             + " fad_remarks, "
                                             + " fad_ref_year, "
                                             + " fad_reftransfernote_no, "
                                             + " fad_reftransaction_no, "
                                             + " fad_tools_condition, "
                                             + " created_by, "
                                             + " created_date) "
                               + " VALUES ("
                                             + " '" + year + "', "
                                             + " '" + issueno + "', "
                                             + " '" + transactionno + "', "
                                             + " '" + main + "',"
                                             + " '" + sub + "', "
                                             + " '" + asLocCode + "', "
                                             + " '" + origiLoc + "', "
                                             + " '" + status + "', "
                                             + " '" + remarks + "', "
                                             + " '" + year + "', "
                                             + " '" + refNoteNo + "', "
                                             + " '" + reftranNo + "', "
                                             + " '" + toolcondi + "', "
                                             + " '" + Connection.UserName + "', "
                                             + " SYSDATE)";

                save = DBconnect.AddEditDel(insert_Query);
            }

            result[0] = update;
            result[0] = save;
            return result;
        }

        internal bool[] updateReturnNo(string issueno, string main, string sub, string transactionno, string refNoteNo, string reftranNo)
        {
            Boolean[] result = new Boolean[2];

            Boolean update = false;
            DBconnect.connect();
            string update_query = "";

            update_query = "UPDATE fms_assettranscation_details "
                        + " SET  "
                            + " fad_reftransfernote_no = '" + refNoteNo + "', "
                            + " fad_reftransaction_no = '" + reftranNo + "', "
                            + " update_by ='" + Connection.UserName + "', "
                            + " update_date = SYSDATE "
                        + " WHERE fad_transfernote_no = '" + issueno + "' "
                        + " AND fad_transaction_no = '" + transactionno + "' "
                        + " AND fad_main = '" + main + "' "
                        + " AND fad_sub ='" + sub + "' ";

            update = DBconnect.AddEditDel(update_query);
            result[0] = update;
            return result;
        }

    }
}

