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
    class DAL_CS_TRN
    {
        DataSource.DAL_DS_Return DAL_DS_Return = new DataSource.DAL_DS_Return();


        internal DataTable Search_Job_Details()
        {
            DBconnect.connect();
            DAL_DS_Return ds = new DAL_DS_Return();
            DataTable dt = ds.tbl_pro;

            string query = @"SELECT 
                                   pmd_jcat, 
                                   pmd_jmain, 
                                   pmd_description 
                            FROM 
                                   pms_mainjob_details 
                            WHERE 
                                   pmd_status = 'A' AND pmd_jcat IN ('OR', 'RM', 'SR') 
                            ORDER BY 
                                   pmd_jcat ";
            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["desc"] = odr["pmd_description"].ToString();
                r["jcat"] = odr["pmd_jcat"].ToString();
                r["jmain"] = odr["pmd_jmain"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
            
        }
        internal DataTable Search_Sub_Details(string jcat, string jmain)
        {
            DBconnect.connect();
            DAL_DS_Service ds = new DAL_DS_Service();
            DataTable dt = ds.tbl_pro;

            string query = @"SELECT 
                                psd_sub,
                                psd_spec,
                                psd_extc,
                                psd_description 
                             FROM 
                                pms_subjob_details 
                             WHERE 
                                psd_jcat = '" + jcat + @"'
                             AND 
                                psd_jmain = '" + jmain + @"' 
                             ORDER BY  
                                psd_sub";
            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["sub"] = odr["psd_sub"].ToString();
                r["spec"] = odr["psd_spec"].ToString();
                r["extc"] = odr["psd_extc"].ToString();
                r["sdesc"] = odr["psd_description"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }
        internal DataTable Search_Rec_No()
        {
            DBconnect.connect();
            DAL_DS_Return ds = new DAL_DS_Return();
            DataTable dt = ds.tblSearch;

            string query = @"SELECT 
                                psd_document_no,
                                psd_jcat,
                                psd_jmain,
                                psd_sub ,
                                psd_spec,
                                psd_extc,
                                TO_CHAR (psd_date, 'YYYY-MM-DD') AS M_DATE
                            FROM 
                                pms_servicecenter_documents 
                            WHERE
	                            psd_document_type = 'TRN'
                            ORDER BY 
                                psd_document_no";

            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["TRNNO"] = odr["psd_document_no"].ToString();
                r["jcat"] = odr["psd_jcat"].ToString();
                r["jmain"] = odr["psd_jmain"].ToString();
                r["sub"] = odr["psd_sub"].ToString();
                r["spec"] = odr["psd_spec"].ToString();
                r["extc"] = odr["psd_extc"].ToString();
                r["cdate"] = odr["M_DATE"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }
        internal DataTable getSearch_Trn_No(string trn, string code)
        {
            DBconnect.connect();
            DAL_DS_Return ds = new DAL_DS_Return();
            DataTable dt = ds.tbl_Search;

            string query = @"SELECT 
                                psd_originator_location,
                                (cdlpack.Get_Location_Name(psd_originator_location))AS locdes,
                                (SELECT ced_report_name FROM employee_details WHERE ced_service_no = psd_originator) AS name,
                                psd_originator,
                                psd_date,
                                psd_loc_code,
                                psd_document_no,
                                psd_document_type,
                                psd_jcat,
                                psd_jmain,
                                psd_sub,
                                psd_spec,
                                psd_extc,
						        (select pmd_description from pms_mainjob_details where pmd_jcat = psd_jcat and pmd_jmain = psd_jmain) AS description
                             FROM
                                pms_servicecenter_documents 
                             WHERE 
	                            psd_document_no = '" + trn + @"'
                            AND 
	                            psd_document_type = 'TRN'";

            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["name"] = odr["name"].ToString();
                r["oloc"] = odr["psd_originator_location"].ToString() + " - " + odr["locdes"].ToString();
                r["date1"] = odr["psd_date"].ToString();
                r["loc"] = odr["psd_loc_code"].ToString();
                r["TRNNO"] = odr["psd_document_no"].ToString();
                r["dtype"] = odr["psd_document_type"].ToString();
                r["jcat"] = odr["psd_jcat"].ToString();
                r["jmain"] = odr["psd_jmain"].ToString();
                r["sub"] = odr["psd_sub"].ToString();
                r["spec"] = odr["psd_spec"].ToString();
                r["extc"] = odr["psd_extc"].ToString();
                r["desc"] = odr["description"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }
        internal DataTable getMatTrnNo(string trn)
        {
            DBconnect.connect();
            DAL_DS_Return ds = new DAL_DS_Return();
            DataTable dt = ds.tbl_mat;

            string query = @"SELECT 
                                t1.PST_MATERIAL_CODE,
                                mmspack.get_material_description(t1.PST_MATERIAL_CODE) AS MatDesc,
                                mmspack.get_material_unit(t1.PST_MATERIAL_CODE) AS UM,
                                t1.PST_QUANTITY,
                                (SELECT t2.pst_quantity 
                                 FROM PMS_SERVICECENTER_TRANSACTIONS t2
                                 WHERE t2.pst_document_type = t1.pst_reference_type 
                                   AND t2.pst_document_no = t1.pst_reference_no
                                   AND ROWNUM = 1) AS preqty,
                                t1.PST_BALANCE_QUANTITY,
                                t1.PST_REMARKS,
                                t1.PST_ISSUE_TYPE
                                --(SELECT SUM(t3.pst_quantity)    
                               -- FROM pms_servicecenter_transactions t3
                                --WHERE t3.pst_material_code = t1.PST_MATERIAL_CODE 
                               -- AND t3.pst_document_type = 'TRN') AS qty
                            FROM 
                                pms_servicecenter_transactions t1
                            WHERE 
                                t1.PST_DOCUMENT_NO = '" + trn + @"'
                                AND t1.PST_DOCUMENT_TYPE = 'TRN'";


            //string query = @"SELECT 
            //                    PST_MATERIAL_CODE,
            //                    mmspack.get_material_description(PST_MATERIAL_CODE ) AS MatDesc,
            //                    mmspack.get_material_unit(PST_MATERIAL_CODE ) AS UM,
            //                    PST_QUANTITY,
            //                    PST_BALANCE_QUANTITY,
            //                    PST_REMARKS,
            //                    PST_ISSUE_TYPE
            //                    FROM 
	           //                     pms_servicecenter_transactions
            //                    WHERE 
	           //                     PST_DOCUMENT_NO = '" + trn + @"'
                                //AND 
	                               // PST_DOCUMENT_TYPE = 'TRN'";
            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["matCode"] = odr["PST_MATERIAL_CODE"].ToString();
                r["dec"] = odr["MatDesc"].ToString();
                r["unit"] = odr["UM"].ToString();
                r["bal"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PST_BALANCE_QUANTITY"].ToString()) ? "0.00" : odr["PST_BALANCE_QUANTITY"].ToString());
                r["iqty"] = Convert.ToDouble(string.IsNullOrEmpty(odr["preqty"].ToString()) ? "0.00" : odr["preqty"].ToString());
                r["rqty"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PST_QUANTITY"].ToString()) ? "0.00" : odr["PST_QUANTITY"].ToString());
                r["remark"] = odr["PST_REMARKS"].ToString();
                //r["trnBal"] = Convert.ToDouble(string.IsNullOrEmpty(odr["preqty"].ToString()) ? "0.00" : odr["preqty"].ToString()) - Convert.ToDouble(string.IsNullOrEmpty(odr["qty"].ToString()) ? "0.00" : odr["qty"].ToString());
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;

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
                         //  + " AND ced_work_category = 'E' "
                         // + " AND ced_division_code = 'DPR' "
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
        internal DataTable Select_Material_Code()
        {
            DBconnect.connect();
            DAL_DS_Return ds = new DAL_DS_Return();
            DataTable dt = ds.tbl_tin;



            string query01 = @"SELECT 
                                psd.psd_DOCUMENT_NO AS docNo,
                                psd.psd_DOCUMENT_TYPE AS docType,
                                TO_CHAR(psd.psd_DATE, 'YYYY-MM-DD') AS tdate,
                                PST.PST_MATERIAL_CODE AS mcode,
                                mmspack.get_material_description(PST.PST_MATERIAL_CODE) AS MatDesc,
                                (SELECT psm_balance_quantity 
                                 FROM PMS_SERVICECENTER_MATERIALS 
                                 WHERE  psm_material_code = PST.PST_MATERIAL_CODE) as bal,--PST.PST_BALANCE_QUANTITY AS bal --newwwww,
                                PST.PST_QUANTITY AS issQty,
                                mmspack.get_material_unit(PST.PST_MATERIAL_CODE) AS UM,
                                psd.psd_JCAT AS jcat,
                                psd.psd_JMAIN AS jmain,
                                psd.psd_SUB AS sub,
                                psd.psd_SPEC AS spec,
                                psd.psd_EXTC AS extc,
                                PST.PST_REMARKS AS remark,
                                (SELECT CED_REPORT_NAME 
                                 FROM employee_details 
                                 WHERE CED_SERVICE_NO = psd.psd_ORIGINATOR) AS ori,
                                (cdlpack.Get_Location_Name(psd.psd_ORIGINATOR_LOCATION)) AS locdes, 
                                psd.psd_ORIGINATOR_LOCATION AS oLoc,
                                psd.psd_ORIGINATOR AS ocode,
                                -- Sum of quantities from pms_servicecenter_transactions
                                (SELECT NVL(SUM(PST_SUB.PST_QUANTITY), 0.00)
                                 FROM pms_servicecenter_transactions PST_SUB
                                 WHERE PST_SUB.PST_MATERIAL_CODE = PST.PST_MATERIAL_CODE
                                  AND PST_SUB.PST_DOCUMENT_NO = PST.PST_DOCUMENT_NO
                                   AND PST_SUB.PST_DOCUMENT_TYPE = 'TRN') AS sum_trn_qty,
                                -- Balance calculation
                                NVL(PST.PST_QUANTITY, 0.00) - 
                                NVL((SELECT NVL(SUM(PST_SUB.PST_QUANTITY), 0.00)
                                     FROM pms_servicecenter_transactions PST_SUB
                                     WHERE PST_SUB.PST_MATERIAL_CODE = PST.PST_MATERIAL_CODE
                                       AND PST_SUB.pst_reference_no = PST.PST_DOCUMENT_NO
                                       AND PST_SUB.pst_reference_type = 'TIN'), 0.00) AS TRNbal_qty,
                                -- Fixed alias issue for description
                                (SELECT pmd_description 
                                 FROM pms_mainjob_details 
                                 WHERE pmd_jcat = psd.psd_JCAT 
                                   AND pmd_jmain = psd.psd_JMAIN) AS jobDesc 
                            FROM 
                                pms_servicecenter_documents  psd
                            LEFT JOIN 
                                pms_servicecenter_transactions PST
                                ON psd.psd_DOCUMENT_NO = PST.PST_DOCUMENT_NO
                                AND psd.psd_DOCUMENT_TYPE = PST.PST_DOCUMENT_TYPE
                            WHERE 
                                psd.psd_DOCUMENT_TYPE = 'TIN'
                                  
                            ORDER BY
                               docNo";



            //string query01 = @"SELECT 
            //                    psd.psd_DOCUMENT_NO AS docNo,
            //                    psd.psd_DOCUMENT_TYPE AS docType,
            //                    TO_CHAR(psd.psd_DATE, 'YYYY-MM-DD') AS tdate,
            //                    PST.PST_MATERIAL_CODE AS mcode,
            //                    mmspack.get_material_description(PST.PST_MATERIAL_CODE) AS MatDesc,
            //                    PST.PST_BALANCE_QUANTITY AS bal,
            //                    PST.PST_QUANTITY AS issQty,
            //                    mmspack.get_material_unit(PST.PST_MATERIAL_CODE) AS UM,
            //                    psd.psd_JCAT AS jcat,
            //                    psd.psd_JMAIN AS jmain,
            //                    psd.psd_SUB AS sub,
            //                    psd.psd_SPEC AS spec,
            //                    psd.psd_EXTC AS extc,
            //                    PST.PST_REMARKS AS remark,
            //                    (SELECT CED_REPORT_NAME 
            //                     FROM employee_details 
            //                     WHERE CED_SERVICE_NO = psd.psd_ORIGINATOR) AS ori,
            //                    (cdlpack.Get_Location_Name(psd.psd_ORIGINATOR_LOCATION)) AS locdes, 
            //                    psd.psd_ORIGINATOR_LOCATION AS oLoc,
            //                    psd.psd_ORIGINATOR AS ocode,

            //                    -- Sum of quantities from pms_servicecenter_transactions
            //                    (SELECT NVL(SUM(PST_SUB.PST_QUANTITY), 0.00)
            //                     FROM pms_servicecenter_transactions PST_SUB
            //                     WHERE PST_SUB.PST_LOC_CODE = psd.psd_ORIGINATOR_LOCATION
            //                       AND PST_SUB.PST_MATERIAL_CODE = PST.PST_MATERIAL_CODE
            //                       AND PST_SUB.PST_DOCUMENT_NO = PST.PST_DOCUMENT_NO
            //                       AND PST_SUB.PST_DOCUMENT_TYPE = 'TRN') AS sum_trn_qty,

            //                    -- Balance calculation
            //                    NVL(PST.PST_QUANTITY, 0.00) - 
            //                    NVL((SELECT NVL(SUM(PST_SUB.PST_QUANTITY), 0.00)
            //                         FROM pms_servicecenter_transactions PST_SUB
            //                         WHERE PST_SUB.PST_LOC_CODE = psd.psd_ORIGINATOR_LOCATION
            //                           AND PST_SUB.PST_MATERIAL_CODE = PST.PST_MATERIAL_CODE
            //                           AND PST_SUB.PST_DOCUMENT_NO = PST.PST_DOCUMENT_NO
            //                           AND PST_SUB.PST_DOCUMENT_TYPE = 'TRN'), 0.00) AS balance_qty,

            //                    -- Fixed alias issue for description
            //                    (SELECT pmd_description 
            //                     FROM pms_mainjob_details 
            //                     WHERE pmd_jcat = psd.psd_JCAT 
            //                       AND pmd_jmain = psd.psd_JMAIN) AS jobDesc 

            //                FROM 
            //                    pms_servicecenter_documents  psd
            //                LEFT JOIN 
            //                    pms_servicecenter_transactions PST
            //                    ON psd.psd_DOCUMENT_NO = PST.PST_DOCUMENT_NO
            //                    AND psd.psd_DOCUMENT_TYPE = PST.PST_DOCUMENT_TYPE
            //                WHERE 
            //                    psd.psd_DOCUMENT_TYPE = 'TIN' 

            //                    AND  psd.psd_DOCUMENT_NO NOT IN 
            //                            (SELECT PST_document_no 
            //                             FROM pms_servicecenter_transactions
            //                             WHERE PST_reference_type = 'TIN'
            //                             AND PST_reference_no = psd.psd_DOCUMENT_NO)

            //                ORDER BY
            //                   docNo";

            OracleDataReader odr = DBconnect.readtable(query01);
            while (odr.Read())
            {
                double trnBalQty = Convert.ToDouble(string.IsNullOrEmpty(odr["TRNbal_qty"].ToString()) ? "0.00" : odr["TRNbal_qty"].ToString());
                if (trnBalQty != 0.00)
                {
                    DataRow r = dt.NewRow();
                    r["dNo"] = odr["docNo"].ToString();
                    r["tdate"] = odr["tdate"].ToString();
                    r["mcode"] = odr["mcode"].ToString();
                    r["desc"] = odr["MatDesc"].ToString();
                    r["bal"] = Convert.ToDouble(string.IsNullOrEmpty(odr["bal"].ToString()) ? "0.00" : odr["bal"].ToString());
                    r["trn_bal"] = Convert.ToDouble(string.IsNullOrEmpty(odr["TRNbal_qty"].ToString()) ? "0.00" : odr["TRNbal_qty"].ToString());
                    r["iqty"] = Convert.ToDouble(string.IsNullOrEmpty(odr["issQty"].ToString()) ? "0.00" : odr["issQty"].ToString());
                    r["um"] = odr["UM"].ToString();
                    r["remark"] = odr["remark"].ToString();
                    r["ori"] = odr["ori"].ToString();
                    r["oriloc"] = odr["oLoc"].ToString() + " - " + odr["locdes"].ToString();
                    r["avg"] = odr["docType"].ToString();
                    r["jcat"] = odr["jcat"].ToString();
                    r["jmain"] = odr["jmain"].ToString();
                    r["sub"] = odr["sub"].ToString();
                    r["spec"] = odr["spec"].ToString();
                    r["extc"] = odr["extc"].ToString();
                    r["ocode"] = odr["ocode"].ToString();
                    r["jobDesc"] = odr["jobDesc"].ToString();
                    dt.Rows.Add(r);
                }
                //DataRow r = dt.NewRow();
                //r["dNo"] = odr["docNo"].ToString();
                //r["tdate"] = odr["tdate"].ToString();
                //r["mcode"] = odr["mcode"].ToString();
                //r["desc"] = odr["MatDesc"].ToString();
                //r["bal"] = Convert.ToDouble(string.IsNullOrEmpty(odr["bal"].ToString()) ? "0.00" : odr["bal"].ToString());
                //r["trn_bal"] = Convert.ToDouble(string.IsNullOrEmpty(odr["TRNbal_qty"].ToString()) ? "0.00" : odr["TRNbal_qty"].ToString());
                //r["iqty"] = Convert.ToDouble(string.IsNullOrEmpty(odr["issQty"].ToString()) ? "0.00" : odr["issQty"].ToString());
                //r["um"] = odr["UM"].ToString();
                //r["remark"] = odr["remark"].ToString();
                //r["ori"] = odr["ori"].ToString();
                //r["oriloc"] = odr["oLoc"].ToString() + " - " + odr["locdes"].ToString();
                //r["avg"] = odr["docType"].ToString();
                //r["jcat"] = odr["jcat"].ToString();
                //r["jmain"] = odr["jmain"].ToString();
                //r["sub"] = odr["sub"].ToString();
                //r["spec"] = odr["spec"].ToString();
                //r["extc"] = odr["extc"].ToString();
                //r["ocode"] = odr["ocode"].ToString();
                //r["jobDesc"] = odr["jobDesc"].ToString();
                //dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
            
        }

        internal string Get_TRN_No()
        {
            DBconnect.connect();
            string rec = "";
            string query = @"SELECT COALESCE(MAX(psd_document_no), 0) +1 as maxnum
                                FROM pms_servicecenter_documents  
                                WHERE psd_document_type = 'TRN'";

            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                rec = odr["maxnum"].ToString();
            }
            odr.Close();
            return rec;
        }
        internal DataTable Select_Transaction_Details(string mCode)//(string docNo, string type)
        {
            DBconnect.connect();
            DAL_DS_Return ds = new DAL_DS_Return();
            DataTable dt = ds.tbl_return;

            string query = @"SELECT 
                                        PST_DOCUMENT_NO ,
                                        PST_DOCUMENT_TYPE,
                                        PST_MATERIAL_CODE ,
                                        mmspack.get_material_description(PST_MATERIAL_CODE ) AS MatDesc,
                                        mmspack.get_material_unit(PST_MATERIAL_CODE ) AS UM,
                                        PST_DATE , 
                                        PST_QUANTITY , 
                                        PST_AVERAGE_RATE ,  
                                        PST_VALUE , 
                                        PST_BALANCE_QUANTITY , 
                                        PST_REMARKS , 
                                        PST_LINE,
                                        PST_REFERENCE_NO
					      	                        --(select SUM(PST_quantity) AS PST_quantity from pms_servicecenter_transactions where PST_material_code = mtd_material_code AND PST_reference_no = mtd_document_no AND PST_reference_type = mtd_document_type ) AS preQtySum,    
                                       -- mmspack.Get_Balance_Quantity(mtd_loc_code, mtd_material_code) as balQty,
                                       -- (  SELECT mmd_average_price AS avgRate FROM mms_material_details WHERE mmd_loc_code = mtd_loc_code AND mmd_material_code= mtd_material_code) AS avgRate 
                                FROM pms_servicecenter_transactions 
                                WHERE PST_MATERIAL_CODE = '" + mCode + "'";
            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["dec"] = odr["MatDesc"].ToString();
                r["matCode"] = odr["PST_MATERIAL_CODE"].ToString();
                r["unit"] = odr["UM"].ToString();
                r["bal"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PST_BALANCE_QUANTITY"].ToString()) ? "0.00" : odr["PST_BALANCE_QUANTITY"].ToString());
                r["remark"] = odr["PST_REMARKS"].ToString();
                dt.Rows.Add(r);

            }
            odr.Close();
            return dt;

        }
        internal bool[] SaveDocumentDetails(string lCode, string trn, string dType, string date1, string jcat, string jmain, string sub, string spec, string extc, string ori, string oLoc)
        {
            Boolean[] result = new Boolean[2];
            Boolean save = false;
            Boolean update = false;
            DBconnect.connect();

            string update_query = "";
            string insert_Query = "";

            update_query = @"UPDATE 
                                    pms_servicecenter_documents  
                            SET 
                                    psd_DATE = TO_DATE('" + date1 + @"', 'YYYY-MM-DD'),
                                    psd_ORIGINATOR_LOCATION = '" + lCode + @"',
                                    updated_by = '"+Connection.UserName+ @"',
                                    updated_date = SYSDATE
                            WHERE 
                                    psd_LOC_CODE =  'SEC'  
                            AND     
                                    psd_document_type = '" + dType + @"' 
                            AND 
                                    psd_document_no = '" + trn + @"'";

            update = DBconnect.AddEditDel(update_query);

            if (!update)
            {
                insert_Query = @"INSERT INTO pms_servicecenter_documents  (
                                                    psd_loc_code,
                                                    psd_document_no,
                                                    psd_document_type,
                                                    psd_date,
                                                    psd_jcat,
                                                    psd_jmain,
                                                    psd_sub,
                                                    psd_spec,
                                                    psd_extc,
                                                    psd_originator,
                                                    psd_originator_location,
                                                    created_by,
                                                    created_date)
                                            ValueS (
                                                    'SEC',
                                                    '" + trn + @"',
                                                    '" + dType + @"',
                                                    TO_DATE('" + date1 + @"', 'YYYY-MM-DD'),
                                                    '" + jcat + @"',
                                                    '" + jmain + @"',
                                                    '" + sub + @"',
                                                    '" + spec + @"',
                                                    '" + extc + @"',
                                                    '" + ori + @"',
                                                    '" + oLoc + @"',
                                                    '" + Connection.UserName + @"',
                                                     SYSDATE)";
                update = DBconnect.AddEditDel(insert_Query);
            }

            result[0] = update;
            return result;
        }
        internal bool[] SaveAssetTransactionDetails(string lCode, string dType, string trn,  string matCode, string qty, string bal, string remark, string date1, string avgRate, string refNo, string rType)
        {
            Boolean[] result = new Boolean[2];
            Boolean save = false;
            DBconnect.connect();
            int lineNo = 0;

            string insert_Query = "";

            double dQty = Convert.ToDouble(qty);
            //avgRate = "10";
            double dAvgRate = Convert.ToDouble(string.IsNullOrEmpty(avgRate) ? "0" : avgRate);

            double balValue = dQty * dAvgRate;
            string matUpdate_Query = @"UPDATE pms_servicecenter_materials  
                                        SET psm_balance_quantity = psm_balance_quantity + " + dQty + "," +
                                            "psm_line = NVL(psm_line,0) +1 , psm_balance_value = (psm_balance_quantity + " + dQty + ") * psm_average_price, " +
                                            "updated_by = '"+Connection.UserName+"'," +
                                            "updated_date = SYSDATE  " +
                                    "WHERE psm_material_code = '" + matCode + "' " +
                                    "AND psm_loc_code = 'SEC'";

            bool matsave = DBconnect.AddEditDel(matUpdate_Query);

            if (!matsave)
            {
                string matinsert_Query = @" INSERT INTO pms_servicecenter_materials  (
                                                    psm_loc_code,
                                                    psm_material_code,
                                                    psm_balance_quantity,
                                                    psm_balance_value,
                                                    psm_line,
                                                    created_date,
                                                    created_by,
                                                    psm_average_price,
                                                    psm_last_purprice
                                                )
                                                VALUES (
                                                    'SEC',
                                                    '" + matCode + @"',
                                                    " + dQty + @",
                                                    " + balValue + @",
                                                    1,
                                                    SYSDATE,
                                                    '" + Connection.UserName + @"',
                                                    " + dAvgRate + @",
                                                    " + dAvgRate + @"
                                                )";
                matsave = DBconnect.AddEditDel(matinsert_Query);
            }

            if (matsave)
            {
                string BalanceQuantity = "";
                string MaxLineNo = "";
                string AvgPrice = "";

                string Query1 = @"SELECT psm_balance_quantity, psm_line, psm_average_price  
                                    FROM pms_servicecenter_materials  
                                    WHERE psm_material_code = '" + matCode + @"' 
                                    AND psm_loc_code = 'SEC'";

                OracleDataReader odr1 = DBconnect.readtable(Query1);
                while (odr1.Read())
                {
                    BalanceQuantity = odr1["psm_balance_quantity"].ToString();
                    MaxLineNo = odr1["psm_line"].ToString();
                    AvgPrice = odr1["psm_average_price"].ToString();
                }
                odr1.Close();

                double Value = Convert.ToDouble(qty) * Convert.ToDouble(AvgPrice);

                insert_Query = @"INSERT INTO pms_servicecenter_transactions(
                                PST_LOC_CODE,
                                PST_DOCUMENT_NO ,
                                PST_DOCUMENT_TYPE, 
                                PST_MATERIAL_CODE , 
                                PST_QUANTITY ,
                                PST_BALANCE_QUANTITY,
                                PST_REMARKS, 
                                PST_LINE ,
                                CREATED_BY , 
                                CREATED_DATE,
                                PST_DATE,
                                PST_AVERAGE_RATE,
                                PST_CALCULATION_TYPE,
                                PST_VALUE,
                                PST_REFERENCE_NO ,
                                PST_REFERENCE_TYPE)

                             VALUES (
                                'SEC' ,
                                '" + trn + @"',
                                '" + dType + @"',
                                '" + matCode + @"',
                                '" + qty + @"',
                                '" + BalanceQuantity + @"',
                                '" + remark + @"', 
                                '" + MaxLineNo + @"',
                                '" + Connection.UserName + @"',
                                SYSDATE ,
                                TO_DATE('" + date1 + @"', 'YYYY-MM-DD'),
                                '" + AvgPrice + @"',
                                'A',
                                '" + Value + @"',
                                '" + refNo + @"',
                                '" + rType + @"')";

                save = DBconnect.AddEditDel(insert_Query);
            }

            result[0] = save;
            return result;
            
        }
    }
}
