using Service_Center_Equipment_Management.DAL.DataSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Center_Equipment_Management.DAL
{
    class DAL_CS_Issue_Note
    {
        DataSource.DAL_DS_portable DAL_DS_Portable = new DataSource.DAL_DS_portable();

        internal DataTable loadMobile()
        {
            
            DBconnect.connect();
            DAL_DS_portable ds = new DAL_DS_portable();
            DataTable dt = ds.first;


            string loadQuery = @"SELECT 
                                    psm_material_code,
                                    mmspack.get_material_description(psm_material_code) AS MatDesc,
                                    psm_balance_quantity,
                                    psm_balance_value,
                                    psm_average_price 
                                FROM 
                                    pms_servicecenter_materials 
                               WHERE psm_balance_quantity NOT IN 
                                        (SELECT psm_balance_quantity 
                                         FROM pms_servicecenter_materials
                                         WHERE psm_balance_quantity = 0.00)
                                --AND psm_material_code NOT IN (
                                        --SELECT
                                            --pst_material_code
                                        --FROM
                                            --pms_servicecenter_transactions
                                        --WHERE
                                            --psm_material_code = pst_material_code
                                        --AND pst_document_type = 'TIN') ";


            //string loadQuery = @"SELECT 
            //                        psm_material_code,
            //                        mmspack.get_material_description(psm_material_code) AS MatDesc,
            //                        psm_balance_quantity,
            //                        psm_balance_value,
            //                        psm_average_price 
            //                    FROM 
            //                        pms_servicecenter_materials 
            //                    WHERE
            //                        psm_material_code NOT IN (
            //                            SELECT
            //                                pst_material_code
            //                            FROM
            //                                pms_servicecenter_transactions
            //                            WHERE
            //                                psm_material_code = pst_material_code
            //                            AND pst_document_type = 'TIN'
            //                        )";


            OracleDataReader odr = DBconnect.readtable(loadQuery);
            while (odr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["qty"] = Convert.ToDouble(string.IsNullOrEmpty(odr["psm_balance_quantity"].ToString()) ? "0.00" : odr["psm_balance_quantity"].ToString());
                drow["value"] = Convert.ToDouble(string.IsNullOrEmpty(odr["psm_balance_value"].ToString()) ? "0.00" : odr["psm_balance_value"].ToString());
                drow["avg"] = Convert.ToDouble(string.IsNullOrEmpty(odr["psm_average_price"].ToString()) ? "0.00" : odr["psm_average_price"].ToString());
                drow["matDes"] = odr["MatDesc"].ToString();
                ////drow["dType"] = odr["PST_DOCUMENT_TYPE"].ToString();
                drow["matCode"] = odr["psm_material_code"].ToString();

                dt.Rows.Add(drow);
            }
            odr.Close();
            return dt;
        }
        internal DataTable Search_Job_Details()
        {
            DBconnect.connect();
            DAL_DS_portable ds = new DAL_DS_portable();
            DataTable dt = ds.JobDetails;

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
                r["jcat"] = odr["pmd_jcat"].ToString();
                r["jmain"] = odr["pmd_jmain"].ToString();
                r["des"] = odr["pmd_description"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }
        internal DataTable Search_Sub_Details(string jcat, string jmain)
        {
            DBconnect.connect();
            DAL_DS_portable ds = new DAL_DS_portable();
            DataTable dt = ds.JobDetails;

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
                r["des"] = odr["psd_description"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }
        internal DataTable loadEmployees()
        {
            DAL_DS_Portable.Tables["Employees"].Clear();
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
                DataRow drow = DAL_DS_Portable.Employees.NewRow();
                drow["service"] = odr["ced_service_no"].ToString();
                drow["name"] = odr["ced_report_name"].ToString();
                drow["loc"] = odr["ced_location_code"].ToString() + " - " + odr["locdes"].ToString();
                drow["barcode"] = odr["ced_barcode_cardno"].ToString();
                DAL_DS_Portable.Employees.Rows.Add(drow);
            }
            odr.Close();
            DBconnect.conn_new.Close();
            return DAL_DS_Portable.Tables["Employees"];
        }

        internal DataTable Select_Transaction_Details (string mCode)//(string docNo, string type)
        {
            DBconnect.connect();
            DAL_DS_portable ds = new DAL_DS_portable();
            DataTable dt = ds.SelectedMaterials;

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
                                        (SELECT psm_balance_quantity FROM pms_servicecenter_materials  WHERE psm_material_code = PST_MATERIAL_CODE) AS  BALANCE_QUANTITY , 
                                        PST_REMARKS , 
                                        PST_LINE,
                                        PST_REFERENCE_NO
					      	                        --(select SUM(PST_quantity) AS PST_quantity from PMS_SERVICECENTER_TRANSACTIONS where PST_material_code = mtd_material_code AND PST_reference_no = mtd_document_no AND PST_reference_type = mtd_document_type ) AS preQtySum,    
                                       -- mmspack.Get_Balance_Quantity(mtd_loc_code, mtd_material_code) as balQty,
                                       -- (  SELECT mmd_average_price AS avgRate FROM mms_material_details WHERE mmd_loc_code = mtd_loc_code AND mmd_material_code= mtd_material_code) AS avgRate 
                                FROM PMS_SERVICECENTER_TRANSACTIONS 
                                WHERE PST_MATERIAL_CODE = '" + mCode + "'";
            //WHERE PST_DOCUMENT_NO = '" + docNo + "'" +
            //"AND PST_DOCUMENT_TYPE = '" + type + "'";
            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["des"] = odr["MatDesc"].ToString();
                r["matCode"] = odr["PST_MATERIAL_CODE"].ToString();
                r["unit"] = odr["UM"].ToString();
                r["value"] = odr["PST_VALUE"].ToString();
                //r["remarks"] = odr["mtd_document_no"].ToString();
                //r["bal"] = odr["PST_QUANTITY"].ToString();
                r["bal"] = Convert.ToDouble(string.IsNullOrEmpty(odr["BALANCE_QUANTITY"].ToString()) ? "0.00" : odr["BALANCE_QUANTITY"].ToString());
                r["iType"] = "Returnable";
                //r["docNo"] = odr["PST_DOCUMENT_NO"].ToString();
                //r["dType"] = odr["PST_DOCUMENT_TYPE"].ToString();
                //r["docNo"] = odr["mtd_document_no"].ToString();
                dt.Rows.Add(r);
                
            }
            odr.Close();
            return dt;

        }
        internal string Get_TIN_No()
        {
            DBconnect.connect();
            string tin = "";
            string query = @"SELECT COALESCE(MAX(PSD_document_no), 0) +1 as maxnum
                                FROM PMS_SERVICECENTER_DOCUMENTS  
                                WHERE PSD_document_type = 'TIN'";

            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                tin = odr["maxnum"].ToString();
            }
            odr.Close();
            return tin;
        }
        internal DataTable Search_Tin_No()
        {
            DBconnect.connect();
            DAL_DS_portable ds = new DAL_DS_portable();
            DataTable dt = ds.tbl_Search;

            string query = @"SELECT 
                                PSD_document_no,
                                PSD_jcat,
                                PSD_jmain,
                                PSD_sub ,
                                PSD_spec,
                                PSD_extc,
                                TO_CHAR (PSD_date, 'YYYY-MM-DD') AS M_DATE 
                            FROM 
                                PMS_SERVICECENTER_DOCUMENTS 
                            WHERE 
                                PSD_document_type = 'TIN'
                            ORDER BY 
                                PSD_document_no";

            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["tin"] = odr["PSD_document_no"].ToString();
                r["jcat"] = odr["PSD_jcat"].ToString();
                r["jmain"] = odr["PSD_jmain"].ToString();
                r["sub"] = odr["PSD_sub"].ToString();
                r["spec"] = odr["PSD_spec"].ToString();
                r["extc"] = odr["PSD_extc"].ToString();
                r["date1"] = odr["M_DATE"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }
        internal DataTable getSearch_Tin_No(string tin/*, string code*/)
        {
            DBconnect.connect();
            DAL_DS_portable ds = new DAL_DS_portable();
            DataTable dt = ds.tbl_Search;

            string query = @"SELECT 
                                PSD_originator_location,
                                (cdlpack.Get_Location_Name(PSD_originator_location))AS locdes,
                                (SELECT ced_report_name FROM employee_details WHERE ced_service_no = PSD_originator) AS name,
                                PSD_originator,
                                PSD_date,
                                PSD_loc_code,
                                PSD_document_no,
                                PSD_document_type,
                                PSD_jcat,
                                PSD_jmain,
                                PSD_sub,
                                PSD_spec,
                                PSD_extc,
                                PSD_remarks,
						        (select pmd_description from pms_mainjob_details where pmd_jcat = PSD_jcat and pmd_jmain = PSD_jmain) AS description
                             FROM
                                PMS_SERVICECENTER_DOCUMENTS 
                             WHERE 
	                            PSD_document_no = '" + tin + @"'
                            AND
	                            PSD_document_type = 'TIN'";

            OracleDataReader odr = DBconnect.readtable(query);  
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["pcode"] = odr["PSD_originator"].ToString();
                r["oloc"] = odr["PSD_originator_location"].ToString() + " - " + odr["locdes"].ToString();
                //r["oloc"] = odr["PSD_originator_location"].ToString();
                r["date1"] = odr["PSD_date"].ToString();
                r["loc"] = odr["PSD_loc_code"].ToString();
                r["doc"] = odr["PSD_document_no"].ToString();
                r["dtype"] = odr["PSD_document_type"].ToString();
                r["jcat"] = odr["PSD_jcat"].ToString();
                r["jmain"] = odr["PSD_jmain"].ToString();
                r["sub"] = odr["PSD_sub"].ToString();
                r["spec"] = odr["PSD_spec"].ToString();
                r["extc"] = odr["PSD_extc"].ToString();
                r["desc"] = odr["description"].ToString();
                r["remark"] = odr["PSD_remarks"].ToString();
                r["name"] = odr["name"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }
        internal DataTable getMatTinNo(string tin , string iType)
        {
            DBconnect.connect();
            DAL_DS_portable ds = new DAL_DS_portable();
            DataTable dt = ds.tbl_mat;

            string query = @"SELECT 
                                PST_MATERIAL_CODE,
                                mmspack.get_material_description(PST_MATERIAL_CODE ) AS MatDesc,
                                mmspack.get_material_unit(PST_MATERIAL_CODE ) AS UM,
                                PST_QUANTITY,
                                PST_BALANCE_QUANTITY,
                                PST_REMARKS,
                                PST_ISSUE_TYPE,DECODE(PST_ISSUE_TYPE,'R', 'Returnable ', 'C', 'Consumable') as type
                                FROM 
	                                PMS_SERVICECENTER_TRANSACTIONS
                                WHERE 
	                                PST_DOCUMENT_NO = '" + tin+@"'
                                AND 
	                                PST_DOCUMENT_TYPE = 'TIN'";
            OracleDataReader odr = DBconnect.readtable(query); 
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["matCode"] = odr["PST_MATERIAL_CODE"].ToString();
                r["des"] = odr["MatDesc"].ToString();
                r["unit"] = odr["UM"].ToString();
                r["bal"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PST_BALANCE_QUANTITY"].ToString()) ? "0.00" : odr["PST_BALANCE_QUANTITY"].ToString());
                r["isQty"] = Convert.ToDouble(string.IsNullOrEmpty(odr["PST_QUANTITY"].ToString()) ? "0.00" : odr["PST_QUANTITY"].ToString());
                r["iType"] = odr["type"].ToString();
                r["remarks"] = odr["PST_REMARKS"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;

        }
        internal DataTable get_Job_Details(string jcat, string jmain)
        {
            DBconnect.connect();
            DAL_DS_portable ds = new DAL_DS_portable();
            DataTable dt = ds.JobDetails;

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
                r["jcat"] = odr["pmd_jcat"].ToString();
                r["jmain"] = odr["pmd_jmain"].ToString();
                r["des"] = odr["pmd_description"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }
        internal bool [] SaveDocumentDetails(string lCode, string tinNo, string dType, string date1, string jcat, string jmain, string sub, string spec, string extc, string ori, string oLoc, string remark)
        {
            Boolean[] result = new Boolean[2];
            Boolean save = false;
            Boolean update = false;
            DBconnect.connect();

            string update_query = "";
            string insert_Query = "";

            update_query = @"UPDATE 
                                    PMS_SERVICECENTER_DOCUMENTS  
                            SET 
                                    PSD_DATE = TO_DATE('" + date1 + @"', 'YYYY-MM-DD'),
                                    PSD_ORIGINATOR_LOCATION = '" + lCode + @"',
                                    updated_by = '"+Connection.UserName+ @"',
                                    updated_date = SYSDATE
                            WHERE 
                                    PSD_LOC_CODE =  '" + lCode + @"'  
                            AND     
                                    PSD_document_type = '" + dType + @"' 
                            AND 
                                    PSD_document_no = '" + tinNo + @"'";

            update = DBconnect.AddEditDel(update_query);

            if (!update)
            {
                insert_Query = @"INSERT INTO PMS_SERVICECENTER_DOCUMENTS  (
                                                    PSD_loc_code,
                                                    PSD_document_no,
                                                    PSD_document_type,
                                                    PSD_date,
                                                    PSD_jcat,
                                                    PSD_jmain,
                                                    PSD_sub,
                                                    PSD_spec,
                                                    PSD_extc,
                                                    PSD_originator,
                                                    PSD_originator_location,
                                                    PSD_REMARKS,
                                                    created_by,
                                                    created_date)
                                            ValueS (
                                                    'SEC',
                                                    '" + tinNo + @"',
                                                    '" + dType + @"',
                                                    TO_DATE('" + date1 + @"', 'YYYY-MM-DD'),
                                                    '" + jcat + @"',
                                                    '" + jmain + @"',
                                                    '" + sub + @"',
                                                    '" + spec + @"',
                                                    '" + extc + @"',
                                                    '" + ori + @"',
                                                    '" + oLoc + @"',
                                                    '" + remark + @"',
                                                    '" + Connection.UserName + @"',
                                                     SYSDATE)";
                update = DBconnect.AddEditDel(insert_Query);
            }

            result[0] = update;
            return result;
            
        }
         internal bool[] SaveAssetTransactionDetails(string lCode, string dType, string tinNo, string iType, string matCode, string qty, string bal, string remarks, string date1, string avgRate)
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
                                        SET psm_balance_quantity = psm_balance_quantity - " + dQty + "," +
                                            "psm_line = NVL(psm_line,0) +1 , psm_balance_value = (psm_balance_quantity - " + dQty + ") * psm_average_price, " +
                                            "updated_by = '" + Connection.UserName + @"'," +
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

                insert_Query = @"INSERT INTO PMS_SERVICECENTER_TRANSACTIONS(
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
                                PST_ISSUE_TYPE,
                                PST_VALUE)

                             VALUES (
                                'SEC' ,
                                '" + tinNo + @"',
                                '" + dType + @"',
                                '" + matCode + @"',
                                '" + qty + @"',
                                '" + BalanceQuantity + @"',
                                '" + remarks + @"', 
                                '" + MaxLineNo + @"',
                                '" + Connection.UserName + @"',
                                SYSDATE ,
                                TO_DATE('" + date1 + @"', 'YYYY-MM-DD'),
                                '" + AvgPrice + @"',
                                'D',
                                'R',
                                '" + Value + @"')";

                save = DBconnect.AddEditDel(insert_Query);
            }

            result[0] = save;
            return result;
            
        }
        
    }
}
