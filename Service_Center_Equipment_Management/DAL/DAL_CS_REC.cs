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
    class DAL_CS_REC
    {
        
        DataSource.DAL_DS_Service DAL_DS_Service = new DataSource.DAL_DS_Service();

        internal DataTable Load_Select_Material_Code(string jcat, string jmain, string sub, string spec, string extc)//anumi 04/21
        {
            DBconnect.connect();
            DAL_DS_Service ds = new DAL_DS_Service();
            DataTable dt = ds.tbl_mrq;

            string query01 = @"SELECT
                                    mdd_document_no,
                                    mdd_document_type,
                                    to_char(mdd_date, 'YYYY-MM-DD')      AS m_date,
                                    mdd_loc_code,
                                    mdd_originator,
                                    mdd_originator_location,
                                    mdd_jcat,
                                    mdd_jmain,
                                    mdd_sub,
                                    mdd_spec,
                                    mdd_extc,
                                    (
                                        SELECT
                                            pmd_description
                                        FROM
                                            pms_mainjob_details
                                        WHERE
                                                mdd_jcat = pmd_jcat
                                            AND mdd_jmain = pmd_jmain
                                    )                                    AS desx
                                FROM
                                    mms_document_details
                                WHERE
                                        mdd_loc_code = 'STC'
                                    AND mdd_originator_location = 'STC'
                                    AND mdd_document_type = 'MRQ'
                                    AND mdd_document_no NOT IN (
                                        SELECT
                                            mdd_document_no
                                        FROM
                                            pms_servicecenter_transactions
                                        WHERE
                                                pst_reference_type = 'MRQ'
                                            AND pst_reference_no = mdd_document_no
                                    )
                                    AND mdd_jcat IN ( 'OR', 'RM', 'SR','NC' )
                                    AND mdd_date >= add_months(trunc(sysdate), - 36) -- Fetch last 3 years of data
                                     
                                ORDER BY
                                    mdd_document_no";

            //  -- AND mdd_jcat = '" + jcat+@"'
           // AND mdd_jmain = '"+jmain+@"'

            OracleDataReader odr = DBconnect.readtable(query01);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["doc no"] = odr["MDD_DOCUMENT_NO"].ToString();
                r["doc type"] = odr["MDD_DOCUMENT_TYPE"].ToString();
                r["date"] = odr["M_DATE"].ToString();
                r["loc"] = odr["MDD_LOC_CODE"].ToString();
                r["o"] = odr["MDD_ORIGINATOR"].ToString();
                r["oLoc"] = odr["MDD_ORIGINATOR_LOCATION"].ToString();
                r["jcat"] = odr["mdd_jcat"].ToString();
                r["jmain"] = odr["mdd_jmain"].ToString();
                r["sub"] = odr["mdd_sub"].ToString();
                r["spec"] = odr["mdd_spec"].ToString();
                r["extc"] = odr["mdd_extc"].ToString();
                r["desc"] = odr["desx"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }

        internal DataTable loadTOOL()//anumi04/28
        {
            DBconnect.connect();
            DAL_DS_Service ds = new DAL_DS_Service();
            DataTable dt = ds.tbl_tool;

            string query = @"SELECT
                                mmd_material_code,
                                mmspack.get_material_description(mmd_material_code)       AS material_description,
                                decode(mmd_status, 'I', 'Inventory', 'R', 'Repair Item',
                                       'C', 'Consumable')                                  AS mmd_status,
                                mmspack.get_material_unit(mmd_material_code)              AS material_unit,
                                mmd_balance_quantity,
                                mmd_average_price,
                                mmd_balance_value
                            FROM
                                mms_material_details
                            WHERE
                                mmd_component_status IS NOT NULL
                                AND mmd_status IS NOT NULL
                                AND mmd_loc_code = 'MTS'
                                AND mmd_material_code NOT IN (
                                        SELECT
                                            mmd_material_code
                                        FROM
                                            PMS_SERVICECENTER_MATERIALS
                                        WHERE
                                                mmd_material_code = psm_material_code)";

            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["mcode"] = odr["mmd_material_code"].ToString();
                r["desc"] = odr["material_description"].ToString();
                r["stts"] = odr["mmd_status"].ToString();
                r["unit"] = odr["material_unit"].ToString();
                r["qty"] = odr["mmd_balance_quantity"].ToString();
                r["avg"] = Convert.ToDouble(string.IsNullOrEmpty(odr["mmd_average_price"].ToString()) ? "0" : odr["mmd_average_price"].ToString());  //odr["mmd_average_price"].ToString();
                r["value"] = odr["mmd_balance_value"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }


        internal DataTable select_TOOL(string matcode)//anumi05/02
        {
            DBconnect.connect();
            DAL_DS_Service ds = new DAL_DS_Service();
            DataTable dt = ds.tbl_tool;

            string query = @"SELECT
                                mmd_material_code,
                                mmspack.get_material_description(mmd_material_code)       AS material_description,
                                decode(mmd_status, 'I', 'Inventory', 'R', 'Repair Item',
                                       'C', 'Consumable')                                  AS mmd_status,
                                mmspack.get_material_unit(mmd_material_code)              AS material_unit,
                                mmd_balance_quantity,
                                mmd_average_price,
                                mmd_balance_value,
                                (SELECT 
                                    psm_balance_quantity 
                                 FROM 
                                    PMS_SERVICECENTER_MATERIALS 
                                 WHERE psm_material_code = mmd_material_code) AS bal
                            FROM
                                mms_material_details
                            WHERE
                                mmd_component_status IS NOT NULL
                                AND mmd_status IS NOT NULL
                                AND mmd_loc_code = 'MTS'
                                AND mmd_material_code = '" + matcode+@"'";

            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["mcode"] = odr["mmd_material_code"].ToString();
                r["desc"] = odr["material_description"].ToString();
                r["stts"] = odr["mmd_status"].ToString();
                r["unit"] = odr["material_unit"].ToString();
                r["qty"] = odr["mmd_balance_quantity"].ToString();
                r["bal"] = Convert.ToDouble(string.IsNullOrEmpty(odr["bal"].ToString()) ? "0" : odr["bal"].ToString()); //odr["bal"].ToString();
                r["value"] = odr["mmd_balance_value"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }







        internal DataTable Select_Material_Code()
        {
            DBconnect.connect();
            DAL_DS_Service ds = new DAL_DS_Service();
            DataTable dt = ds.tbl_mrq;

            string query01 = @"Select
                                    MDD_DOCUMENT_NO,
                                    MDD_DOCUMENT_TYPE,
                                    TO_CHAR (MDD_DATE, 'YYYY-MM-DD') AS M_DATE,
                                    MDD_LOC_CODE, 
                                    MDD_ORIGINATOR,
                                    MDD_ORIGINATOR_LOCATION,
                                    mdd_jcat,
                                    mdd_jmain,
                                    mdd_sub,
                                    mdd_spec,
                                    mdd_extc,
                                (SELECT 
                                                                   pmd_description 
                                                            FROM 
                                                                   pms_mainjob_details 
                                                            WHERE 
                                                                   mdd_jcat = pmd_jcat AND mdd_jmain = pmd_jmain) AS desx


                                FROM MMS_DOCUMENT_DETAILS 
                               WHERE MDD_LOC_CODE = 'STC'
                               --AND mdd_originator_location = 'STC'
                               AND MDD_DOCUMENT_TYPE = 'MRQ'
                               AND MDD_DOCUMENT_NO NOT IN 
                                        (SELECT 
                                                MDD_DOCUMENT_NO 
                                        FROM 
                                                pms_servicecenter_transactions 
                                        WHERE 
                                                pst_reference_type = 'MRQ'  
                                        AND 
                                                pst_reference_no = MDD_DOCUMENT_NO)
                                AND mdd_jcat IN ( 'OR', 'RM', 'SR','NC' )
                                AND MDD_DATE >= ADD_MONTHS(TRUNC(SYSDATE), -36) -- Fetch last 3 years of data

                               ORDER BY mdd_document_no";

            OracleDataReader odr = DBconnect.readtable(query01);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["doc no"] = odr["MDD_DOCUMENT_NO"].ToString();
                r["doc type"] = odr["MDD_DOCUMENT_TYPE"].ToString();
                r["date"] = odr["M_DATE"].ToString();
                r["loc"] = odr["MDD_LOC_CODE"].ToString();
                r["o"] = odr["MDD_ORIGINATOR"].ToString();
                r["oLoc"] = odr["MDD_ORIGINATOR_LOCATION"].ToString();
                r["jcat"] = odr["mdd_jcat"].ToString();
                r["jmain"] = odr["mdd_jmain"].ToString();
                r["sub"] = odr["mdd_sub"].ToString();
                r["spec"] = odr["mdd_spec"].ToString();
                r["extc"] = odr["mdd_extc"].ToString();
                r["desc"] = odr["desx"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }

        internal string Get_Rec_No()
        {
            DBconnect.connect();
            string rec = "";
            string query = @"SELECT COALESCE(MAX(psd_document_no), 0) +1 as maxnum
                                FROM pms_servicecenter_documents  
                                WHERE psd_document_type = 'REC'";

            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                rec = odr["maxnum"].ToString();
            }
            odr.Close();
            return rec;
        }
        internal string[] Get_Edate_SDate()
        {
            DBconnect.connect();
            string Sdate = "";
            string Edate = "";

            string query = @"SELECT 
                                mwd_dentry_sdate,
                                mwd_dentry_edate,
                                mwd_loc_code
                            FROM
                                mms_warehouse_details
                            WHERE
                                mwd_loc_code = 'SEC'";
            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                Sdate = odr["mwd_dentry_sdate"].ToString();
                Edate = odr["mwd_dentry_edate"].ToString();
            }
            odr.Close();
            
            return new string[] { Sdate, Edate };
        }

        //internal string Get_Edate_SDate()
        //{
        //    DBconnect.connect();
        //    string Sdate = "";
        //    string Edate = "";

        //    string query = @"SELECT 
        //                        mwd_dentry_sdate,
        //                        mwd_dentry_edate,
        //                        mwd_loc_code
        //                    FROM
        //                        mms_warehouse_details
        //                    WHERE
        //                        mwd_loc_code = 'SEC'";
        //    OracleDataReader odr = DBconnect.readtable(query);
        //    while (odr.Read())
        //    {
        //        Sdate = odr["mwd_dentry_sdate"].ToString();
        //        Edate = odr["mwd_dentry_edate"].ToString();
        //    }
        //    odr.Close();
        //    return Sdate,Edate
        //}

        internal DataTable Search_Rec_No()
        {
            DBconnect.connect();
            DAL_DS_Service ds = new DAL_DS_Service();
            DataTable dt = ds.tblSearch;

            string query = @"SELECT 
                                psd_document_no,
                                psd_jcat,
                                psd_jmain,
                                psd_sub ,
                                psd_spec,
                                psd_extc,
                                psd_date
                            FROM 
                                pms_servicecenter_documents 
                            WHERE 
                                psd_document_type = 'REC'
                            ORDER BY 
                                psd_document_no";

            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["recNo"] = odr["psd_document_no"].ToString();
                r["jcat"] = odr["psd_jcat"].ToString();
                r["jmain"] = odr["psd_jmain"].ToString();
                r["sub"] = odr["psd_sub"].ToString();
                r["spec"] = odr["psd_spec"].ToString();
                r["extc"] = odr["psd_extc"].ToString();
                r["cdate"] = odr["psd_date"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }
        
        internal DataTable Search_Job_Details()
        {
            DBconnect.connect();
            DAL_DS_Service ds = new DAL_DS_Service();
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
                r["jdesc"] = odr["pmd_description"].ToString();
                r["jcat"] = odr["pmd_jcat"].ToString();
                r["jmain"] = odr["pmd_jmain"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }

        internal DataTable Search_Sub_Details(string jcat , string jmain)
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
                                psd_jcat = '"+jcat+@"'
                             AND 
                                psd_jmain = '"+jmain+@"' 
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

        internal DataTable getMatRecNo(string rec)
        {
            DBconnect.connect();
            DAL_DS_Service ds = new DAL_DS_Service();
            DataTable dt = ds.tbl_mat;

            string query = @"SELECT 
                                pst_LOC_CODE,
                                pst_DOCUMENT_NO ,
                                pst_DOCUMENT_TYPE, 
                                pst_MATERIAL_CODE ,
                                pst_DATE,
                                mmspack.get_material_description(pst_MATERIAL_CODE ) AS MatDesc,
                                mmspack.get_material_unit(pst_MATERIAL_CODE ) AS UM,
                                (SELECT   psd_JCAT FROM pms_servicecenter_documents  WHERE   psd_DOCUMENT_NO = pst_DOCUMENT_NO AND  psd_DOCUMENT_TYPE = pst_document_type) AS jcat, 
                                (SELECT   psd_JMAIN FROM pms_servicecenter_documents  WHERE   psd_DOCUMENT_NO = pst_DOCUMENT_NO AND  psd_DOCUMENT_TYPE = pst_document_type) AS jmain,
                                (SELECT   psd_SUB FROM pms_servicecenter_documents  WHERE   psd_DOCUMENT_NO = pst_DOCUMENT_NO AND  psd_DOCUMENT_TYPE = pst_document_type) AS sub,
                                (SELECT   psd_SPEC FROM pms_servicecenter_documents  WHERE   psd_DOCUMENT_NO = pst_DOCUMENT_NO AND  psd_DOCUMENT_TYPE = pst_document_type) AS spec,
                                (SELECT   psd_EXTC FROM pms_servicecenter_documents  WHERE   psd_DOCUMENT_NO = pst_DOCUMENT_NO AND  psd_DOCUMENT_TYPE = pst_document_type) AS extc,
                                (SELECT 
                                mtd_quantity 
                                FROM 
	                                MMS_TRANSACTION_DETAILS
                                WHERE 
	                                mtd_loc_code = pst_LOC_CODE
                                AND 
	                                mtd_document_type = pst_REFERENCE_TYPE
                                AND
	                                mtd_document_no = pst_REFERENCE_NO
                                AND 
	                                mtd_material_code =  pst_MATERIAL_CODE ) AS MRQ_Quantity,
                                            (SELECT psm_balance_quantity FROM pms_servicecenter_materials  WHERE pst_MATERIAL_CODE =  psm_MATERIAL_CODE) as balqty,
                                            (SELECT psm_AVERAGE_PRICE FROM pms_servicecenter_materials  WHERE pst_MATERIAL_CODE =  psm_MATERIAL_CODE) as avrg,
                                pst_QUANTITY , 
                                pst_REMARKS, 
                                pst_TOOL_STATUS , 
                                pst_REFERENCE_NO ,
                                pst_REFERENCE_TYPE,
                                pst_LINE,
                                pst_REFERENCE_QTY
                            from pms_servicecenter_transactions
                            where 
	                            pst_DOCUMENT_NO  = '" + rec+ @"'
                            AND 
                                pst_DOCUMENT_TYPE = 'REC'";

            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["loc_code"] = odr["pst_LOC_CODE"].ToString();
                r["docNo"] = odr["pst_DOCUMENT_NO"].ToString();
                r["dType"] = odr["pst_DOCUMENT_TYPE"].ToString();
                r["matCode"] = odr["pst_MATERIAL_CODE"].ToString();
                r["desc"] = odr["MatDesc"].ToString();
                r["unit"] = odr["UM"].ToString();
                r["quntity"] = Convert.ToDouble( string.IsNullOrEmpty(odr["pst_QUANTITY"].ToString()) ? "0" : odr["pst_QUANTITY"].ToString());
                r["umr"] = Convert.ToDouble(string.IsNullOrEmpty(odr["pst_REFERENCE_QTY"].ToString()) ? "0" : odr["pst_REFERENCE_QTY"].ToString());
                //r["umr"] = Convert.ToDouble(string.IsNullOrEmpty(odr["MRQ_Quantity"].ToString()) ? "0" : odr["MRQ_Quantity"].ToString());
                //r["umr"] = odr["pst_QUANTITY"].ToString();
                r["remark"] = odr["pst_REMARKS"].ToString();
                r["stts"] = odr["pst_TOOL_STATUS"].ToString();
                r["refNo"] = odr["pst_REFERENCE_NO"].ToString();
                r["refType"] = odr["pst_REFERENCE_TYPE"].ToString();
                r["avrg"] = Convert.ToDouble(string.IsNullOrEmpty(odr["avrg"].ToString()) ? "0" : odr["avrg"].ToString()); 
                r["bal"] = Convert.ToDouble(string.IsNullOrEmpty(odr["balqty"].ToString()) ? "0.00" : odr["balqty"].ToString()); 
                r["line"] = odr["pst_LINE"].ToString();
                r["mrqQTY"] = odr["pst_REFERENCE_QTY"].ToString();
                r["jcat"] = odr["jcat"].ToString(); 
                r["jmain"] = odr["jmain"].ToString();
                r["sub"] = odr["sub"].ToString();
                r["spec"] = odr["spec"].ToString();
                r["extc"] = odr["extc"].ToString();
                r["date"] = odr["pst_DATE"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }
        internal DataTable getSearchRecNo(string rec)
        {
            DBconnect.connect();
            DAL_DS_Service ds = new DAL_DS_Service();
            DataTable dt = ds.tbl_search;

            string query = @"SELECT 
                                psd_originator_location,
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
	                            psd_document_no = '" + rec+@"'";

            OracleDataReader odr = DBconnect.readtable(query);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["wcode"] = odr["psd_loc_code"].ToString();
                r["date1"] = odr["psd_date"].ToString();
                r["loc"] = odr["psd_originator_location"].ToString();
                r["doc"] = odr["psd_document_no"].ToString();
                r["dtype"] = odr["psd_document_type"].ToString();
                r["jcat"] = odr["psd_jcat"].ToString();
                r["jmain"] = odr["psd_jmain"].ToString();
                r["sub"] = odr["psd_sub"].ToString();
                r["spec"] = odr["psd_spec"].ToString();
                r["extc"] = odr["psd_extc"].ToString();
                r["name"] = odr["description"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }
        internal DataTable Select_Transaction_Details(string docNo, string type)
        {
            DBconnect.connect();
            DAL_DS_Service ds = new DAL_DS_Service();
            DataTable dt = ds.tbl_mat;
            
            string query02 = @"SELECT 
                                        mtd_document_no ,
                                        MTD_DOCUMENT_TYPE,
                                        mtd_material_code ,
                                        mmspack.get_material_description(mtd_material_code ) AS MatDesc,
                                        mmspack.get_material_unit(mtd_material_code ) AS UM,
                                        MTD_TOOL_STATUS,
                                        mtd_date , 
                                        mtd_quantity , 
                                        mtd_average_rate ,  
                                        mtd_value , 
                                        mtd_balance_quantity , 
                                        mtd_remarks , 
                                        mtd_line,
                                        MTD_REFERENCE_NO,
					      	                        (select SUM(pst_quantity) AS pst_quantity from pms_servicecenter_transactions 
                                                        where pst_material_code = mtd_material_code
                                                        AND pst_reference_no = mtd_document_no
                                                        AND pst_reference_type = mtd_document_type ) AS preQtySum,    
                                       -- mmspack.Get_Balance_Quantity(mtd_loc_code, mtd_material_code) as balQty,
                                        (SELECT mmd_average_price AS avgRate FROM mms_material_details WHERE mmd_loc_code = mtd_loc_code AND mmd_material_code= mtd_material_code) AS avgRate

                                FROM MMS_TRANSACTION_DETAILS 
                                WHERE mtd_document_no = '"+docNo+@"'
                                AND mtd_document_type = '"+type+@"'";
            // preQtySum - column returns the sum of previous enterd qqty for that meterial & document no

            OracleDataReader odr = DBconnect.readtable(query02);
            while (odr.Read())
            {
                DataRow r = dt.NewRow();
                r["docNo"] = odr["mtd_document_no"].ToString();
                r["matCode"] = odr["mtd_material_code"].ToString();
                r["desc"] = odr["MatDesc"].ToString();
                r["unit"] = odr["UM"].ToString();
                r["stts"] = odr["MTD_TOOL_STATUS"].ToString();
                r["date"] = odr["mtd_date"].ToString();
                r["umr"] = Convert.ToDouble(string.IsNullOrEmpty(odr["mtd_quantity"].ToString()) ? "0" : odr["mtd_quantity"].ToString()) - Convert.ToDouble(string.IsNullOrEmpty(odr["preQtySum"].ToString()) ? "0" : odr["preQtySum"].ToString());
                //r["umr"] = odr["mtd_quantity"].ToString();
                r["avg"] = odr["avgRate"].ToString();
                r["value"] = odr["mtd_value"].ToString();
                r["quntity"] = Convert.ToDouble(string.IsNullOrEmpty(odr["mtd_quantity"].ToString()) ? "0" : odr["mtd_quantity"].ToString()) - Convert.ToDouble(string.IsNullOrEmpty(odr["preQtySum"].ToString()) ? "0" : odr["preQtySum"].ToString());
                r["bal"] = 0.00;
                //r["bal"] = Convert.ToDouble(string.IsNullOrEmpty(odr["mtd_quantity"].ToString()) ? "0" : odr["mtd_quantity"].ToString()) - Convert.ToDouble(string.IsNullOrEmpty(odr["preQtySum"].ToString()) ? "0" : odr["preQtySum"].ToString());
                r["remark"] = odr["mtd_remarks"].ToString();
                r["line"] = odr["mtd_line"].ToString();
                r["refNo"] = odr["MTD_REFERENCE_NO"].ToString();
                r["dType"] = odr["MTD_DOCUMENT_TYPE"].ToString();
                dt.Rows.Add(r);
            }
            odr.Close();
            return dt;
        }

        internal bool[] SaveDocumentDetails(string wCode, string tdate1 ,  string dType, string recNo, /*string refNo,string refType,*/ string dNo, string jcat, string jmain, string sub, string spec, string extc)
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
                                    psd_DATE = TO_DATE('"+tdate1+ @"', 'YYYY-MM-DD'),
                                    psd_ORIGINATOR_LOCATION = '" + wCode + @"',
                                    updated_by = '"+Connection.UserName+ @"',
                                    updated_date = SYSDATE
                            WHERE 
                                    psd_LOC_CODE =  '" + wCode + @"'  
                            AND     
                                    psd_document_type = '"+dType+@"' 
                            AND 
                                    psd_document_no = '"+recNo+@"'";

            update = DBconnect.AddEditDel(update_query);

            if(!update)
            {
                insert_Query = @"INSERT INTO pms_servicecenter_documents  (
                                psd_DATE,
                                psd_LOC_CODE,
                                psd_DOCUMENT_NO,
                                psd_DOCUMENT_TYPE,
                                psd_JCAT,
                                psd_JMAIN,
                                psd_SUB,
                                psd_SPEC,
                                psd_EXTC,
                                created_by,
                                created_date

                            )
                            VALUES (
                                TO_DATE('" + tdate1+@"', 'YYYY-MM-DD'),
                                'SEC',
                                '"+ recNo + @"',
                                '"+dType+@"',
                                '"+jcat+@"',
                                '"+jmain+@"',
                                '"+sub+@"',
                                '"+spec+@"',
                                '"+ extc+@"',
                                '" + Connection.UserName + "', "
                               +" SYSDATE)";
                update = DBconnect.AddEditDel(insert_Query);
            }

            result[0] = update;
           // result[0] = save;
            return result;
        }
        

        internal bool[] SaveAssetTransactionDetails(string wCode /*lCode*/, string dType, string recNo/*string DNo, string Dtype*/, string matCode, string qty, string bal, string remark, string tool, string refNo, string line, string rType, string avgRate,  string tdate1, string mrqQty)
        {
            Boolean[] result = new Boolean[2];
            Boolean save = false;
            //Boolean update = false;
            DBconnect.connect();
            int lineNo = 0;

            //string update_query = "";
            string insert_Query = "";

            //update_query = @"UPDATE pms_servicecenter_transactions 
            //                    SET pst_REMARKS = '"+remark+@"'
            //                    WHERE pst_loc_code = '"+lCode+@"'
            //                    AND pst_document_type = '"+ rType+@"'
            //                    AND pst_document_no = '"+refNo+@"'
            //                    AND pst_material_code = '"+matCode+@"'";

            //update = DBconnect.AddEditDel(update_query);

            //if (!update)
            //{
            double dQty = Convert.ToDouble(qty);
            //avgRate = "10";
            double dAvgRate = Convert.ToDouble(string.IsNullOrEmpty(avgRate) ? "0" : avgRate);

            double balValue = dQty * dAvgRate;
            string matUpdate_Query = @"UPDATE pms_servicecenter_materials  
                                        SET psm_balance_quantity = psm_balance_quantity + '" + dQty + @"',
                                            psm_line = NVL(psm_line,0) +1 , psm_balance_value = (psm_balance_quantity + '" + dQty + @"') * psm_average_price, 
                                            updated_by = '" + Connection.UserName + @"',
                                            updated_date = SYSDATE  
                                    WHERE psm_material_code = '" + matCode + @"'
                                    AND psm_loc_code = '"+ wCode/*lCode*/ +@"'";

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
                                                    '"+ wCode/*lCode*/+ @"',
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
                                    AND psm_loc_code = '" + wCode/*lCode*/ + @"'";

                OracleDataReader odr1 = DBconnect.readtable(Query1);
                while (odr1.Read())
                {
                    BalanceQuantity = odr1["psm_balance_quantity"].ToString();
                    MaxLineNo = odr1["psm_line"].ToString();
                    AvgPrice = odr1["psm_average_price"].ToString();
                }
                odr1.Close();

                double  Value = Convert.ToDouble(qty) * Convert.ToDouble(AvgPrice);
                
                insert_Query = @"INSERT INTO pms_servicecenter_transactions(
                                PST_LOC_CODE,
                                PST_DOCUMENT_NO ,
                                PST_DOCUMENT_TYPE, 
                                PST_MATERIAL_CODE , 
                                PST_QUANTITY ,
                                PST_BALANCE_QUANTITY,
                                PST_REMARKS, 
                                PST_TOOL_STATUS , 
                                PST_REFERENCE_NO ,
                                PST_REFERENCE_TYPE,
                                PST_LINE ,
                                CREATED_BY , 
                                CREATED_DATE,
                                PST_DATE,
                                PST_AVERAGE_RATE,
                                PST_REFERENCE_QTY,
                                PST_CALCULATION_TYPE,
                                PST_VALUE)

                             VALUES (
                                'SEC' ,
                                '" + recNo + @"',
                                '" + dType + @"',
                                '" + matCode + @"',
                                '" + qty + @"',
                                '" + BalanceQuantity + @"',
                                '" + remark + @"', 
                                '" + tool + @"' ,
                                '" + refNo + @"',
                                '" + rType + @"',
                                '" + MaxLineNo + @"',
                                '" + Connection.UserName + @"',
                                SYSDATE ,
                                TO_DATE('" + tdate1 + @"', 'YYYY-MM-DD'),
                                '  "+ AvgPrice+@"  ',
                                '"+mrqQty+@"',
                                'A',
                                '" + Value + @"' )";

                save = DBconnect.AddEditDel(insert_Query);
            }
            
            result[0] = save;
            return result;
        }
        internal bool[] SaveMaterialDetails(string wCode /*loc*/, string matcode, string bal, string line, string count)
        {
            Boolean[] result = new Boolean[2];
            Boolean save = false;
            Boolean update = false;
            DBconnect.connect();
            int cnt = 0;

            string update_query = "";
            string insert_Query = "";

            string selSql = @"SELECT 
                                count(*) as count  
                              FROM 
                                pms_servicecenter_materials 
                              WHERE 
                                psm_material_code = '" + matcode + @"'";
            OracleDataReader odr = DBconnect.readtable(selSql);
            while (odr.Read())
            {
                cnt = Convert.ToInt32(string.IsNullOrEmpty(odr["count"].ToString()) ? "0" : odr["count"].ToString());
            }
            odr.Close();
            if (cnt != 0)
            {
                update_query = @"UPDATE 
                                    pms_servicecenter_materials  
                                 SET 
                                    psm_balance_quantity = '"+bal+ @"',
                                    updated_by = '"+Connection.UserName+ @"',
                                    updated_date = SYSDATE
                                 WHERE psm_material_code = ''" + matcode+@"''";

                update = DBconnect.AddEditDel(update_query);

                if (!update)
                {
                    insert_Query = @"INSERT INTO pms_servicecenter_materials (
                                            psm_LOC_CODE,
                                            psm_MATERIAL_CODE,
                                            psm_BALANCE_QUANTITY,
                                            psm_LINE,
                                            CREATED_DATE,
                                            CREATED_BY)
                                    VALUES(
                                            'SEC',
                                            '"+matcode+@"',
                                            '"+bal+@"',
                                            '"+ line+ @"',
                                            SYSDATE,
                                            '" + Connection.UserName + @"')";
                    update = DBconnect.AddEditDel(insert_Query);
                }
                
            }
            result[0] = update;
            // result[0] = save;
            return result;
        }
    }
}
