using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;
using Service_Center_Equipment_Management.DAL.DataSource;

namespace Service_Center_Equipment_Management.PAL.USER_CONTROLS
{
    public partial class UC_TRN : DevExpress.XtraEditors.XtraUserControl
    {
        BLL.BLL_CS_TRN BLL_CS_TRN = new BLL.BLL_CS_TRN();
        REPORTS.TRN_Report TRN_Report = new REPORTS.TRN_Report();
        public UC_TRN()
        {
            InitializeComponent(); 
        }

        private void UC_TRN_Load(object sender, EventArgs e)
        {
            pnlJob.Hide();
            pnlSub.Hide();
            pnlEmployees.Hide();
            pnlSearch.Hide();
            gcIssue.DataSource = BLL_CS_TRN.Select_Material_Code();
            dtpDate.Text = DateTime.Today.ToString();
            btnPrintTRN.Enabled = false;
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {
            pnlJob.Visible = false;
        }

        private void labelControl25_Click(object sender, EventArgs e)
        {
            pnlSub.Visible = false;
        }

        private void labelControl22_Click(object sender, EventArgs e)
        {
            pnlEmployees.Visible = false;
        }

        private void labelControl32_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
        }

        private void txtJcat_DoubleClick(object sender, EventArgs e)
        {
            //pnlJob.Visible = true;
            //gcJob.DataSource = BLL_CS_TRN.Search_Job_Details();
        }

        private void txtsub_DoubleClick(object sender, EventArgs e)
        {
            //string jcat = txtJcat.Text;
            //string jmain = jMain.Text;
            //if (jcat == "")
            //{
            //    XtraMessageBox.Show("Please Select Main Job.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    //MessageBox.Show("Please Select Main Job.");
            //}
            //else
            //{
            //    pnlSub.Visible = true;
            //    gcSub.DataSource = BLL_CS_TRN.Search_Sub_Details(jcat, jmain);
            //}
        }

        private void gvJob_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string jcat = "";
            string jmain = "";
            string jdesc = "";

            jcat = gvJob.GetFocusedRowCellValue("jcat").ToString();
            jmain = gvJob.GetFocusedRowCellValue("jmain").ToString();
            jdesc = gvJob.GetFocusedRowCellValue("desc").ToString();

            txtJcat.Text = jcat;
            jMain.Text = jmain;
            txtDescription.Text = jdesc;

            pnlJob.Visible = false;
        }

        private void gvSub_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string sub = "";
            string spec = "";
            string extc = "";

            sub = gvSub.GetFocusedRowCellValue("sub").ToString();
            spec = gvSub.GetFocusedRowCellValue("spec").ToString();
            extc = gvSub.GetFocusedRowCellValue("extc").ToString();

            txtsub.Text = sub;
            txtspec.Text = spec;
            txtextc.Text = extc;
            pnlSub.Visible = false;
        }

        private void txtResponsiblePerson_DoubleClick(object sender, EventArgs e)
        {
            pnlEmployees.Visible = true;
            gcEmployees.DataSource = BLL_CS_TRN.loadEmployees();
        }

        private void gvEmployees_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string RName = "";
            string RLocation = "";
            string Code = "";
            RName = gvEmployees.GetFocusedRowCellValue("name").ToString();
            RLocation = gvEmployees.GetFocusedRowCellValue("loc").ToString();
            Code = gvEmployees.GetFocusedRowCellValue("service").ToString();
            txtResponsiblePerson.Text = RName;
            txtWorkshopLoc.Text = RLocation;
            ocode.Text = Code;
            pnlEmployees.Visible = false;
        }
        public string SetTRNNo()
        {//rec ektath
            string TRN = txtTinNo.Text;
            string tin = BLL_CS_TRN.Get_TRN_No();
            //  txtTinNo.Text = tin;
            return tin;
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            string TrnNo = SetTRNNo();
            Boolean[] saveDocument = new Boolean[2];
            Boolean[] saveTransaction = new Boolean[2];
            string lCode = txtloc.Text;
            string trn = TrnNo;//rec ektath
            string dType = txtdType.Text;
            string date1 = dtpDate.Text;
            string jcat = txtJcat.Text;
            string jmain = jMain.Text;
            string sub = txtsub.Text;
            string spec = txtspec.Text;
            string extc = txtextc.Text;
            string ori = ocode.Text;
            string oLoc = txtWorkshopLoc.Text;
            string remarks = "";
            string dNo = trn;
            //double isqty = gvReturn.GetRowCellDisplayText("")
            //SetTINNo();
            if(lCode == "")
            {
                XtraMessageBox.Show("Please Select Warehouse.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if(jmain == "")
                {
                    XtraMessageBox.Show("Please Select Main Job Details.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if(sub == "")
                    {
                        XtraMessageBox.Show("Please Select Sub Job Details.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if(date1 == "")
                        {
                            XtraMessageBox.Show("Please Select Date.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            if(ori == "")
                            {
                                XtraMessageBox.Show("Please Select Responsible Person.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                if (gvReturn.RowCount <= 0)
                                {
                                    XtraMessageBox.Show("Please Select Materials.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else
                                {
                                    bool haserr = false;
                                    for (int i = 0; i < gvReturn.RowCount; i++)
                                    {
                                        string desc = gvReturn.GetRowCellDisplayText(i, "dec");
                                        string matCode = gvReturn.GetRowCellDisplayText(i, "matCode");
                                        string unit = gvReturn.GetRowCellDisplayText(i, "unit");
                                        string remark = gvReturn.GetRowCellDisplayText(i, "remark");
                                        double qty = 0.0;
                                        string qtyString = gvReturn.GetRowCellDisplayText(i, "rqty"); //QtyReturn
                                        string bal = gvReturn.GetRowCellDisplayText(i, "bal");
                                        double dobRet = Convert.ToDouble(string.IsNullOrEmpty(bal) ? "0" : bal);
                                        string trnqty = gvReturn.GetRowCellDisplayText(i, "trnBal");
                                        double iqty = Convert.ToDouble(string.IsNullOrEmpty(trnqty) ? "0" : trnqty);
                                        if (!string.IsNullOrEmpty(qtyString))
                                        {
                                            if (!double.TryParse(qtyString, out qty))
                                            {
                                                XtraMessageBox.Show("Invalid quantity value. Please check the data.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                //MessageBox.Show("Invalid quantity value. Please check the data.");
                                                qty = 0.0;
                                                haserr = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            qty = 0.0;
                                            XtraMessageBox.Show("Invalid quantity value. Please check the data.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            haserr = true;
                                            return;
                                        }
                                        
                                        string avgRate = "";//gvSelectedMaterials.GetRowCellValue(i, "avg").ToString();
                                        if (iqty < qty)
                                        {
                                            XtraMessageBox.Show("Return Quentity should be less than or Equal to TRN Balance Quentity .", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            haserr = true;
                                            return;
                                        }
                                        else
                                        {
                                            if (0 > qty)
                                            {
                                                XtraMessageBox.Show("Quentity Return should be more than or Equal to 0 .", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                haserr = true;
                                                return;
                                            }
                                        }
                                    }
                                    if (!haserr)
                                    {
                                        saveDocument = BLL_CS_TRN.SaveDocumentDetails(lCode, trn, dType, date1, jcat, jmain, sub, spec, extc, ori, oLoc);


                                        if (saveDocument[0])
                                        {
                                            for (int i = 0; gvReturn.RowCount > i; i++)
                                            {

                                                string desc = gvReturn.GetRowCellDisplayText(i, "dec");
                                                string matCode = gvReturn.GetRowCellDisplayText(i, "matCode");
                                                string unit = gvReturn.GetRowCellDisplayText(i, "unit");
                                                string remark = gvReturn.GetRowCellDisplayText(i, "remark");
                                                double qty = 0.0;
                                                string qtyString = gvReturn.GetRowCellDisplayText(i, "rqty"); //QtyReturn
                                                string bal = gvReturn.GetRowCellDisplayText(i, "bal");
                                                double dobRet = Convert.ToDouble(string.IsNullOrEmpty(bal) ? "0" : bal);
                                                string avgRate = "";
                                                string refNo = gvIssues.GetRowCellDisplayText(i, "dNo");
                                                string rType = gvIssues.GetRowCellDisplayText(i, "avg");
                                                saveTransaction = BLL_CS_TRN.SaveAssetTransactionDetails(lCode, dType, trn, matCode, qtyString, bal, remark, date1, avgRate, refNo, rType);
                                                //gvIssues.Columns.Clear();
                                                //gcIssue.DataSource = null;
                                                //gcIssue.DataSource = BLL_CS_TRN.Select_Material_Code();
                                                // gcIssue.Refresh();

                                                if (!saveTransaction[0])
                                                {
                                                    XtraMessageBox.Show("Unsuccessful.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);// messge unsuccess
                                                    return;
                                                }
                                            }
                                            if (saveTransaction[0])
                                            {
                                                XtraMessageBox.Show("Saved Successfully.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                txtTinNo.Text = trn;
                                                //btn_Save.Enabled = false;
                                                btn_Save.Enabled = false;
                                                btnRefresh.Enabled = false;
                                                btnPrintTRN.Enabled = true;
                                                txtJcat.ReadOnly = true;
                                                jMain.ReadOnly = true;
                                                txtsub.ReadOnly = true;
                                                txtspec.ReadOnly = true;
                                                txtextc.ReadOnly = true;
                                                txtloc.ReadOnly = true;
                                                txtResponsiblePerson.ReadOnly = true;
                                                txtDescription.ReadOnly = true;
                                                dtpDate.ReadOnly = true;
                                                txtWorkshopLoc.ReadOnly = true;
                                                txtdType.ReadOnly = true;
                                                txtTinNo.ReadOnly = true;
                                                txtserch.ReadOnly = true;
                                                pnlEmployees.SendToBack();
                                                pnlJob.SendToBack();
                                                //pnlSearch.SendToBack();
                                                pnlSub.SendToBack();
                                                gvIssues.OptionsBehavior.Editable = false;
                                                gvReturn.OptionsBehavior.Editable = false;
                                               // gcIssue.RefreshDataSource();
                                                gcIssue.DataSource = null;
                                                gcIssue.DataSource = BLL_CS_TRN.Select_Material_Code();
                                            }

                                        }
                                        else
                                        {
                                            XtraMessageBox.Show("Unsuccessful.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);// messge unsuccess
                                        }

                                    }
                                    //saveDocument = BLL_CS_TRN.SaveDocumentDetails(lCode, trn, dType, date1, jcat, jmain, sub, spec, extc, ori, oLoc);

                                    //for (int i = 0; gvReturn.RowCount > i; i++)
                                    //{
                                    //    string matCode = gvReturn.GetRowCellDisplayText(i, "matCode");
                                    //    string qty = gvReturn.GetRowCellDisplayText(i, "bal");
                                    //    string bal = gvReturn.GetRowCellDisplayText(i, "bal");
                                    //    string remark = gvReturn.GetRowCellDisplayText(i, "remark");
                                    //    string avgRate = "";
                                    //    saveTransaction = BLL_CS_TRN.SaveAssetTransactionDetails(lCode, dType, dNo, matCode, qty, bal, remark, date1, avgRate);
                                    //    if (!saveTransaction[0])
                                    //    {
                                    //        XtraMessageBox.Show("Unsuccessful.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);// messge unsuccess
                                    //        return;
                                    //    }
                                    //}
                                    //if (saveTransaction[0])
                                    //{
                                    //    XtraMessageBox.Show("Saved Successfully.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    //SetRecNo();
                                    //    txtTinNo.Text = trn; //rec ektath
                                    //}
                                }
                            }
                        }
                    }
                }
            }

        }

        private void gvIssues_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null && e.Column.FieldName == "select")
            {
                gvIssues.SetRowCellValue(e.RowHandle, "select", e.Value);
            }
        }

        private void gvIssues_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string material = "";    // To store material code
            string quantity = "";    // To store quantity
            string description = ""; // To store description
            string unit = "";
            string isqty = "";
            string refeNo = "";
            string reftype = "";
            string jCat = "";
            string jmain = "";
            string sub = "";
            string spec = "";
            string extc = "";
            string descri = "";
            string ori = "";
            string oriloc = "";
            string oricode = "";
            string trnBal = "";

            if (e.Column.FieldName == "select")
            {
                // Clear existing rows in gvSelectedMaterials
                for (int i = gvReturn.RowCount - 1; i >= 0; i--)
                {
                    gvReturn.DeleteRow(i);
                }

                if (e.Value != null && e.Column.FieldName == "select")
                {
                    int rowNo = 0;
                    txtJcat.Text = "";
                    jMain.Text = "";
                    txtsub.Text = "";
                    txtspec.Text = "";
                    txtextc.Text = "";
                    txtResponsiblePerson.Text = "";
                    txtWorkshopLoc.Text = "";
                    txtDescription.Text = "";
                    ocode.Text = "";

                    //****
                    DAL_DS_Return ds = new DAL_DS_Return();
                    DataTable dtR = ds.tbl_return;

                    foreach (DataRow row in ((DataTable)gcIssue.DataSource).Rows)
                    {
                        if (row["select"].ToString() == "True")
                        {
                            material = row["mcode"].ToString(); /*gvIssues.GetRowCellDisplayText(rowNo, "mcode");*/
                            description = row["desc"].ToString();  //gvIssues.GetRowCellDisplayText(rowNo, "desc");
                            quantity = row["bal"].ToString(); /*gvIssues.GetRowCellDisplayText(rowNo, "bal");*/
                            unit = row["um"].ToString(); //gvIssues.GetRowCellDisplayText(rowNo, "um");
                            isqty= row["iqty"].ToString(); //gvIssues.GetRowCellDisplayText(rowNo, "iqty");
                            refeNo = row["dNo"].ToString();// gvIssues.GetRowCellDisplayText(rowNo, "dNo");
                            reftype = row["avg"].ToString();  //gvIssues.GetRowCellDisplayText(rowNo, "avg");
                            trnBal = row["trn_bal"].ToString();
                            jCat = row["jcat"].ToString();  //gvIssues.GetRowCellDisplayText(rowNo, "jcat");
                            jmain = row["jmain"].ToString();  //gvIssues.GetRowCellDisplayText(rowNo, "jmain");
                            sub = row["sub"].ToString();  //gvIssues.GetRowCellDisplayText(rowNo, "sub");
                            spec = row["spec"].ToString();  //gvIssues.GetRowCellDisplayText(rowNo, "spec");
                            extc = row["extc"].ToString();  //gvIssues.GetRowCellDisplayText(rowNo, "extc");
                            ori = row["ori"].ToString();  //gvIssues.GetRowCellDisplayText(rowNo, "ori");
                            oriloc = row["oriloc"].ToString();  //gvIssues.GetRowCellDisplayText(rowNo, "oriloc");
                            descri = row["jobDesc"].ToString();  //gvIssues.GetRowCellDisplayText(rowNo, "jobDesc");
                            oricode = row["ocode"].ToString();

                            txtJcat.Text = jCat;
                            jMain.Text = jmain;
                            txtsub.Text = sub;
                            txtspec.Text = spec;
                            txtextc.Text = extc;
                            txtResponsiblePerson.Text = ori;
                            txtWorkshopLoc.Text = oriloc;
                            txtDescription.Text = descri;//get the value to textbox from the left side grid. the columnas are hide.
                            ocode.Text = oricode;

                            DataRow r = dtR.NewRow();
                            r["matCode"] = material;
                            r["dec"] = description;
                            r["bal"] = quantity;
                            r["unit"] = unit;
                            r["iqty"] = isqty;
                            r["rqty"] = trnBal;
                            r["rno"] = refeNo;
                            r["rtype"] = reftype;
                            r["trnBal"] = trnBal;


                            dtR.Rows.Add(r);//

                            //gvReturn.Focus();
                            //gvReturn.AddNewRow();
                            //int j = gvReturn.FocusedRowHandle;
                            //gvReturn.SetFocusedRowCellValue("matCode", material);
                            //gvReturn.SetFocusedRowCellValue("dec", description);
                            //gvReturn.SetFocusedRowCellValue("bal", quantity);
                            //gvReturn.SetFocusedRowCellValue("unit", unit);
                            //gvReturn.SetFocusedRowCellValue("iqty", isqty);
                            //gvReturn.SetFocusedRowCellValue("rno", refeNo);
                            //gvReturn.SetFocusedRowCellValue("rtype", reftype);


                            //gvReturn.FocusedRowHandle = gvReturn.RowCount + 1;
                        }
                        
                        rowNo++;
                    }
                    gcReturn.DataSource = dtR;
                }
            }
        }

        private void txtserch_DoubleClick(object sender, EventArgs e)
        {
            pnlSearch.Visible = true;
            gcSearch.DataSource = BLL_CS_TRN.Search_Rec_No();
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {
            pnlJob.Visible = false;
        }

        private void gvSearch_Click(object sender, EventArgs e)
        {
            string trn = "";
            string code = ocode.Text;
            DataTable dt = new DataTable();

            if (gvSearch.RowCount > 0)
            {
                trn = gvSearch.GetRowCellValue(gvSearch.FocusedRowHandle, "TRNNO").ToString();
                if (trn != "")
                {
                    txtTinNo.Text = trn;
                }

                dt = BLL_CS_TRN.getSearch_Trn_No(trn, code);
                foreach (DataRow drow in dt.Rows)
                {
                    dtpDate.Text = drow["date1"].ToString();
                    txtResponsiblePerson.Text = drow["name"].ToString();
                    txtWorkshopLoc.Text = drow["oloc"].ToString();
                    txtserch.Text = drow["TRNNO"].ToString();
                    txtloc.Text = "SEC - Service Center";
                    txtdType.Text = drow["dtype"].ToString();
                    txtTinNo.Text = drow["TRNNO"].ToString();
                    txtDescription.Text = drow["desc"].ToString();
                    txtextc.Text = drow["extc"].ToString();
                    txtspec.Text = drow["spec"].ToString();
                    txtsub.Text = drow["sub"].ToString();
                    jMain.Text = drow["jmain"].ToString();
                    txtJcat.Text = drow["jcat"].ToString();

                }
                dt = BLL_CS_TRN.getMatTrnNo(trn);


                gcReturn.DataSource = dt;


                pnlSearch.Visible = false;
            }
        }

        private void txtserch_TextChanged(object sender, EventArgs e)
        {
            if(txtTinNo.Text != "")
            {
                btn_Save.Enabled = false;
                btnRefresh.Enabled = false;
                btnPrintTRN.Enabled = true;
                txtJcat.ReadOnly = true;
                jMain.ReadOnly = true;
                txtsub.ReadOnly = true;
                txtspec.ReadOnly = true;
                txtextc.ReadOnly = true;
                txtloc.ReadOnly = true;
                txtResponsiblePerson.ReadOnly = true;
                txtDescription.ReadOnly = true;
                dtpDate.ReadOnly = true;
                txtWorkshopLoc.ReadOnly = true;
                txtdType.ReadOnly = true;
                txtTinNo.ReadOnly = true;
                txtserch.ReadOnly = true;
                pnlEmployees.SendToBack();
                pnlJob.SendToBack();
                //pnlSearch.SendToBack();
                pnlSub.SendToBack();
                gvIssues.OptionsBehavior.Editable = false;
                gvReturn.OptionsBehavior.Editable = false;
            }
            else
            {
                btnRefresh.Enabled = true;
                btn_Save.Enabled = true;
                btnPrintTRN.Enabled = false;
                txtJcat.ReadOnly = true;
                jMain.ReadOnly = true;
                txtsub.ReadOnly = true;
                txtspec.ReadOnly = true;
                txtextc.ReadOnly = true;
                txtloc.ReadOnly = true;
                txtResponsiblePerson.ReadOnly = true;
                txtDescription.ReadOnly = true;
                dtpDate.ReadOnly = false;
                txtWorkshopLoc.ReadOnly = true;
                txtdType.ReadOnly = true;
                txtTinNo.ReadOnly = true;
                txtserch.ReadOnly = true;
                pnlEmployees.BringToFront();
                pnlJob.BringToFront();
                //pnlSearch.SendToBack();
                pnlSub.BringToFront();
                gvIssues.OptionsBehavior.Editable = true;
                gvReturn.OptionsBehavior.Editable = true;
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void gvReturn_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if(txtTinNo.Text == "")
            {
                if (e.HitInfo.InRow)
                {
                    e.Menu.Items.Clear();

                    e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("Remove", (o, args) =>
                    {
                        GridView view = sender as GridView;
                        if (view != null && view.FocusedRowHandle >= 0)
                        {
                            string docNo = gvReturn.GetRowCellDisplayText(view.FocusedRowHandle, "rno");
                            string docType = gvReturn.GetRowCellDisplayText(view.FocusedRowHandle, "rtype");
                            string matCode = gvReturn.GetRowCellDisplayText(view.FocusedRowHandle, "matCode");


                            view.DeleteRow(view.FocusedRowHandle);

                            bool hasRow = false;
                            for (int i = 0; i < gvReturn.RowCount; i++)
                            {
                                string docNo2 = gvReturn.GetRowCellDisplayText(i, "rno");
                                string docType2 = gvReturn.GetRowCellDisplayText(i, "rtype");
                                string matCode2 = gvReturn.GetRowCellDisplayText(i, "matCode");

                                if (docNo2 == docNo && docType2 == docType && matCode2 != matCode)
                                {
                                    hasRow = true;
                                }
                            }
                            if (!hasRow)
                            {
                                for (int i = 0; i < gvIssues.RowCount; i++)
                                {
                                    string docNo3 = gvIssues.GetRowCellDisplayText(i, "dNo");
                                    string matCode2 = gvIssues.GetRowCellDisplayText(i, "mcode");

                                    if (matCode2 == matCode && docNo == docNo3)
                                    {
                                        gvIssues.SetRowCellValue(i, "select", "False");
                                    }
                                }
                            }
                        }
                    }));
                }
            }
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtJcat.Text = "";
            jMain.Text = "";
            txtsub.Text = "";
            txtspec.Text = "";
            txtextc.Text = "";
            txtResponsiblePerson.Text = "";
            txtDescription.Text = "";
            txtWorkshopLoc.Text = "";
            txtTinNo.Text = "";//TRN NO
            txtserch.Text = "";

            for (int i = 0; i < gvIssues.RowCount; i++)
            {
                if (gvIssues.GetRowCellValue(i,"select").ToString()=="True")
                {
                    gvIssues.SetRowCellValue(i, "select", "False");
                }

                //string mCode = gvIssues.GetRowCellDisplayText(i, "matCode");
                ////string docType3 = gvMobile.GetRowCellDisplayText(i, "doc type");
                //string mCode1 = gvReturn.GetRowCellDisplayText(gvReturn.FocusedRowHandle, "matCode");
                ////string docType = gvSelectedMaterials.GetRowCellDisplayText(gvSelectedMaterials.FocusedRowHandle, "dType");
                //if (mCode == mCode1)
                //{
                //    gvIssues.SetRowCellValue(i, "select", "False");
                //}
            }
            for (int i = gvReturn.RowCount; i > -1; i--)
            {
                gvReturn.DeleteRow(i);
            }

        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            //anumi02/28
            btnRefresh.Enabled = true;
            btn_Save.Enabled = true;
            btnPrintTRN.Enabled = false;
            txtJcat.ReadOnly = true;
            jMain.ReadOnly = true;
            txtsub.ReadOnly = true;
            txtspec.ReadOnly = true;
            txtextc.ReadOnly = true;
            txtloc.ReadOnly = true;
            txtResponsiblePerson.ReadOnly = true;
            txtDescription.ReadOnly = true;
            dtpDate.ReadOnly = false;
            txtWorkshopLoc.ReadOnly = true;
            txtdType.ReadOnly = true;
            txtTinNo.ReadOnly = true;
            txtserch.ReadOnly = true;
            pnlEmployees.BringToFront();
            pnlJob.BringToFront();
            //pnlSearch.SendToBack();
            pnlSub.BringToFront();
            gvIssues.OptionsBehavior.Editable = true;
            gvReturn.OptionsBehavior.Editable = true; //anumi02/28





            txtTinNo.Text = "";
            txtJcat.Text = "";
            jMain.Text = "";
            txtsub.Text = "";
            txtspec.Text = "";
            txtextc.Text = "";
            txtDescription.Text = "";
            txtserch.Text = "";
            txtResponsiblePerson.Text = "";
            txtWorkshopLoc.Text = "";
            dtpDate.Text = DateTime.Today.ToString();
            //txtTinNo.Text = "";
            ocode.Text = "";
            txtDescription.Text = "";
            txtResponsiblePerson.Text = "";
            txtWorkshopLoc.Text = "";

            //dtpDate.Text = "";
            //txtIWONo.Text = "";
            //txtResponsibilityHolder.Text = "";
            txtTinNo.Text = "";
            for (int i = 0; i < gvIssues.RowCount; i++)
            {
                //string mCode = gvIssues.GetRowCellDisplayText(i, "mcode");
                ////string docType3 = gvMobile.GetRowCellDisplayText(i, "doc type");
                //string mCode1 = gvReturn.GetRowCellDisplayText(gvReturn.FocusedRowHandle, "matCode");
                ////string docType = gvSelectedMaterials.GetRowCellDisplayText(gvSelectedMaterials.FocusedRowHandle, "dType");
                //if (mCode == mCode1)
                //{
                if(gvIssues.GetRowCellValue(i,"select").ToString() == "True")
                {
                    gvIssues.SetRowCellValue(i, "select", "False");
                }
                    
                //}
            }
            for (int i = gvReturn.RowCount; i > -1; i--)
            {
                gvReturn.DeleteRow(i);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            gcIssue.DataSource = null;
            gcIssue.DataSource = BLL_CS_TRN.Select_Material_Code();
        }

        private void btnPrintTRN_Click(object sender, EventArgs e)
        {
            string sysID = "FMS";
            string appID = "FMSR0169";
            string time = DateTime.Now.ToString("HH:mm:ss");
            string sdate = DateTime.Now.ToString("yyyy-MM-dd");
            string jcat = txtJcat.Text;
            string jmain = jMain.Text;
            string sub = txtsub.Text;
            string spec = txtspec.Text;
            string extc = txtextc.Text;
            string ori = txtResponsiblePerson.Text;
            string oriloc = txtWorkshopLoc.Text;
            string pname = txtDescription.Text;
            string tdate = dtpDate.Text;
            string dtype = txtdType.Text;
            string trnNo = txtTinNo.Text;//trn no
            string loc = txtloc.Text;



            TRN_Report.Parameters["paramsysID"].Value = sysID.ToString();
            TRN_Report.Parameters["paramappID"].Value = appID.ToString();
            TRN_Report.Parameters["paramTime"].Value = DateTime.Now.ToString("HH:mm");
            TRN_Report.Parameters["paramRdate"].Value = sdate;
            TRN_Report.Parameters["paramTrnNo"].Value = trnNo;
            TRN_Report.Parameters["paramjcat"].Value = jcat;
            TRN_Report.Parameters["paramjmain"].Value = jmain;
            TRN_Report.Parameters["paramsub"].Value = sub;
            TRN_Report.Parameters["paramspec"].Value = spec;
            TRN_Report.Parameters["paramextc"].Value = extc;
            TRN_Report.Parameters["parampName"].Value = pname;
            TRN_Report.Parameters["paramdtype"].Value = dtype;
            TRN_Report.Parameters["paramtdate"].Value = tdate;
            TRN_Report.Parameters["paramtloc"].Value = loc;
            TRN_Report.Parameters["paramresloc"].Value = oriloc;
            TRN_Report.Parameters["paramresper"].Value = ori;

            string trn = txtTinNo.Text;//gvSearch.GetRowCellValue(gvSearch.FocusedRowHandle, "TRNNO").ToString();
            TRN_Report.DataSource = null;
            TRN_Report.DataSource = BLL_CS_TRN.getMatTrnNo( trn);
            TRN_Report.CreateDocument();
            ReportPrintTool PrintTool = new ReportPrintTool(TRN_Report);
            PrintTool.ShowRibbonPreview(UserLookAndFeel.Default);
        }
    }
}
