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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;

namespace Service_Center_Equipment_Management.PAL.USER_CONTROLS
{
    public partial class UC_IssueNote : DevExpress.XtraEditors.XtraUserControl
    {
        REPORTS.TIN_Report TIN_Report = new REPORTS.TIN_Report();
        BLL.BLL_CS_Issue_Note BLL_CS_Issue_Note = new BLL.BLL_CS_Issue_Note();
       // DAL.DAL_CS_Issue_Note dAL_CS_Issue_Note = new DAL.DAL_CS_Issue_Note();
        string value = "";

        public UC_IssueNote()
        {
            InitializeComponent();
        }

        private void UC_IssueNote_Load(object sender, EventArgs e)
        {
           gcMobile.DataSource = BLL_CS_Issue_Note.loadMobile();
            tdate/* dtpDate*/.Text = DateTime.Today.ToString();
            pnlJobDetails.Hide();
            pnlSub.Hide();
            pnlEmployees.Hide();
            pnlSearch.Hide();
            btnTinPrint.Enabled = false;
        }

        private void cmbReferenceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //splashScreenManager1.ShowWaitForm();
            //gcPendingGrn.DataSource = da_obl.loadFirst();
            //gcSecond.DataSource = da_obl.loadSecond();
            gcMobile.DataSource = BLL_CS_Issue_Note.loadMobile();
           //// gcPortable.DataSource = dAL_CS_Issue_Note.loadPortable();

            gvSelectedMaterials.DeleteRow(gvSelectedMaterials.FocusedRowHandle);
            gvSelectedMaterials.DeleteRow(gvSelectedMaterials.FocusedRowHandle);
            gvSelectedMaterials.DeleteRow(gvSelectedMaterials.FocusedRowHandle);
            gvSelectedMaterials.DeleteRow(gvSelectedMaterials.FocusedRowHandle);
            gvSelectedMaterials.DeleteRow(gvSelectedMaterials.FocusedRowHandle);
            gvSelectedMaterials.DeleteRow(gvSelectedMaterials.FocusedRowHandle);
            gvSelectedMaterials.DeleteRow(gvSelectedMaterials.FocusedRowHandle);
            gvSelectedMaterials.DeleteRow(gvSelectedMaterials.FocusedRowHandle);
            gvSelectedMaterials.DeleteRow(gvSelectedMaterials.FocusedRowHandle);
            gvSelectedMaterials.DeleteRow(gvSelectedMaterials.FocusedRowHandle);

            if (cmbReferenceType.SelectedIndex == 0)
            {
                txtWorkshopLoc.ReadOnly = true;

                txtWorkshopLoc.BackColor = Color.LightGray;
                cmbJobCat.BackColor = Color.White;
                ocode.BackColor = Color.White;
                txtDescription.BackColor = Color.White;

                cmbJobCat.Properties.ReadOnly = false;

            }
            else if (cmbReferenceType.SelectedIndex == 1)
            {
                //txtWorkshopLoc.ReadOnly = false;
                cmbJobCat.Properties.ReadOnly = true;
                ocode.ReadOnly = true;
                txtDescription.ReadOnly = true;

                cmbJobCat.BackColor = Color.LightGray;
                ocode.BackColor = Color.LightGray;
                txtDescription.BackColor = Color.LightGray;
                txtWorkshopLoc.BackColor = Color.White;

                cmbJobCat.Text = "";
                ocode.Text = "";
            }
            else if (cmbReferenceType.SelectedIndex == 2)
            {
                //txtWorkshopLoc.ReadOnly = false;
                cmbJobCat.Properties.ReadOnly = true;
                ocode.ReadOnly = true;
                txtDescription.ReadOnly = true;

                cmbJobCat.BackColor = Color.LightGray;
                ocode.BackColor = Color.LightGray;
                txtDescription.BackColor = Color.LightGray;
                txtWorkshopLoc.BackColor = Color.LightGray;

                cmbJobCat.Text = "";
                ocode.Text = "";
                txtWorkshopLoc.Text = "";
            }
            txtWorkshopLoc.Text = "";
            txtResponsiblePerson.Text = "";
            txtIWONo.Text = "";
            tdate/*dtpDate*/.Text = DateTime.Today.ToString();
            //pctUser.Image = (Image)Properties.Resources.User;
            //splashScreenManager1.CloseWaitForm();
        }

        private void txtJobNo_DoubleClick(object sender, EventArgs e)
        {
            if (txtIWONo.Text == "")
            {
                if (cmbReferenceType.SelectedIndex == 0)
                {
                    if (cmbJobCat.Text != "")
                    {
                        pnlJobDetails.Visible = true;//--------- me tynne ain krpu pnl eka 
                      //  gcJobDetails.DataSource = dAL_CS_Issue_Note.loadProjects(cmbJobCat.Text);
                    }
                    else
                    {
                        XtraMessageBox.Show(this.LookAndFeel, "Please select a Job Category code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            pnlJobDetails.Hide();//--------- me tynne ain krpu pnl eka 
        }
        

        private void txtWorkshopLoc_DoubleClick(object sender, EventArgs e)
        {
            if (txtIWONo.Text == "")
            {
                if (cmbReferenceType.SelectedIndex == 1)
                {
                    //pnlLocation.Visible = true;
                    //gcLocation.DataSource = dAL_CS_Issue_Note.loadLocations();
                }
                else if (cmbReferenceType.SelectedIndex == 2)
                {
                    txtWorkshopLoc.ReadOnly = true;
                    txtWorkshopLoc.BackColor = Color.LightGray;
                }
                else if (cmbReferenceType.SelectedIndex == 0)
                {
                    txtWorkshopLoc.ReadOnly = true;
                    txtWorkshopLoc.BackColor = Color.LightGray;
                }
            }
        }

        

        private void txtIWONo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string iwono = txtIWONo.Text;
            }
            catch (Exception)
            {
            }
            txtWorkshopLoc.BackColor = Color.White;

            if (txtIWONo.Text != "")
            {
                cmbJobCat.Properties.ReadOnly = true;
                cmbReferenceType.ReadOnly = true;
                cmbReferenceType.BackColor = Color.White;
            }
            else if (txtIWONo.Text == "")
            {
                cmbJobCat.Properties.ReadOnly = false;
                cmbReferenceType.ReadOnly = false;
            }
        }

        private void txtServiceNo_TextChanged(object sender, EventArgs e)
        {
            getUserPictutre();
        }
        public void getUserPictutre()
        {
            try
            {
                string pre = extc.Text.Trim().Split('-')[0].Substring(0);
                //pctUser.Text = pre;
                //pctUser.Image = Image.FromFile((@"\\cdplc-apps\CDPLC_PROD\PHOTOS\cdlemp_photo\\" + pre.ToString()));
            }
            catch (Exception)
            {
                //pctUser.Image = (Image)Properties.Resources.User;
            }
        }


        private void gvMobile_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string material = "";    // To store material code
            string quantity = "";    // To store quantity
            string description = ""; // To store description

            if (e.Column.FieldName == "select")
            {
                // Clear existing rows in gvSelectedMaterials
                for (int i = gvSelectedMaterials.RowCount - 1; i >= 0; i--)
                {
                    gvSelectedMaterials.DeleteRow(i);
                }

                if (e.Value != null && e.Column.FieldName == "select")
                {
                    int rowNo = 0;

                    
                    foreach (DataRow row in ((DataTable)gcMobile.DataSource).Rows)
                    {
                        if (row["select"].ToString() == "True")
                        {
                            // Retrieve values from gvMobile grid for the current row
                            material = row["matCode"].ToString();     // gvMobile.GetRowCellDisplayText(rowNo, "matCode"); // grid eka nmi dnn oni data table eke records tika
                            description = row["matDes"].ToString();    ///gvMobile.GetRowCellDisplayText(rowNo, "matDes");
                            quantity = row["qty"].ToString(); //gvMobile.GetRowCellDisplayText(rowNo, "qty");

                            // Populate the gvSelectedMaterials grid
                            gvSelectedMaterials.AddNewRow();
                            gvSelectedMaterials.SetFocusedRowCellValue("matCode", material);
                            gvSelectedMaterials.SetFocusedRowCellValue("des", description);
                            gvSelectedMaterials.SetFocusedRowCellValue("bal", quantity);

                            // If additional data is needed from another source (e.g., BLL_CS_Issue_Note.Select_Transaction_Details)
                            DataTable dt1 = BLL_CS_Issue_Note.Select_Transaction_Details(material);
                            foreach (DataRow row1 in dt1.Rows)
                            {
                                gvSelectedMaterials.SetFocusedRowCellValue("unit", row1["unit"].ToString());
                                gvSelectedMaterials.SetFocusedRowCellValue("remark", row1["remarks"].ToString());
                                gvSelectedMaterials.SetFocusedRowCellValue("bal", row1["bal"].ToString());
                                gvSelectedMaterials.SetFocusedRowCellValue("isQty", row1["bal"].ToString());
                                gvSelectedMaterials.SetFocusedRowCellValue("avg", row1["iType"].ToString());
                            }

                            // Focus on the last row of gvSelectedMaterials
                            gvSelectedMaterials.Focus();
                            gvSelectedMaterials.FocusedRowHandle = gvSelectedMaterials.RowCount - 1;
                        }

                        rowNo++;
                    }
                }
            }
            
        }

        
        bool status = false;

        private void gvMobile_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Value != null && e.Column.FieldName == "select")
            {
                gvMobile.SetRowCellValue(e.RowHandle, "select", e.Value);
            }

        }

        private void gvMobile_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvMobile.FocusedColumn.FieldName == "select")
            {
                if (gvMobile.GetFocusedRowCellDisplayText("v_type") == "1")// || gvMobile.GetFocusedRowCellDisplayText("v_type") == "2")
                {
                    e.Cancel = true;
                }
            }

            if (gvMobile.FocusedColumn.FieldName == "isqty")
            {
                if (gvMobile.GetFocusedRowCellDisplayText("v_type") == "3")
                {
                    e.Cancel = true;
                }
            }
        }

        private void gvPortable_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "select")
            {
                for (int i = gvSelectedMaterials.RowCount; i > -1; i--)
                {
                    gvSelectedMaterials.DeleteRow(i);
                }

                if (e.Value != null && e.Column.FieldName == "select")
                {
                    int rowNo = 0;
                    foreach (DataRow row in ((DataTable)gcMobile.DataSource).Rows)
                    {
                        if (row["select"].ToString() == "True")
                        {
                            //string docNo = gvMobile.GetFocusedRowCellDisplayText("doc no");
                            //string type = gvMobile.GetFocusedRowCellDisplayText("doc type");
                            //string docNo = gvMobile.GetRowCellDisplayText(rowNo, "doc no");
                            //string type = gvMobile.GetRowCellDisplayText(rowNo, "doc type");
                            string mCode = gvMobile.GetRowCellDisplayText(rowNo, "matCode");
                            DataTable dt1 = BLL_CS_Issue_Note.Select_Transaction_Details(mCode);
                            //for (int i = gv_Mat.RowCount; i > -1; i--)
                            //{
                            //    gv_Mat.DeleteRow(i);
                            //}

                            foreach (DataRow row1 in dt1.Rows)
                            {
                                gvSelectedMaterials.AddNewRow();
                               // gvSelectedMaterials.SetFocusedRowCellValue("loc_code", row["loc"].ToString());
                                gvSelectedMaterials.SetFocusedRowCellValue("matCode", row1["matCode"].ToString());
                                gvSelectedMaterials.SetFocusedRowCellValue("desc", row1["desc"].ToString());
                                gvSelectedMaterials.SetFocusedRowCellValue("unit", row1["unit"].ToString());
                                gvSelectedMaterials.SetFocusedRowCellValue("quntity", row1["quntity"].ToString());
                             //   gvSelectedMaterials.SetFocusedRowCellValue("umr", row1["umr"].ToString());
                                gvSelectedMaterials.SetFocusedRowCellValue("remark", row1["remark"].ToString());
                              //  gvSelectedMaterials.SetFocusedRowCellValue("stts", row1["stts"].ToString());
                              //  gvSelectedMaterials.SetFocusedRowCellValue("refNo", row1["refNo"].ToString());
                               // gvSelectedMaterials.SetFocusedRowCellValue("line", row1["line"].ToString());
                               // gvSelectedMaterials.SetFocusedRowCellValue("docNo", row1["docNo"].ToString());
                              //  gvSelectedMaterials.SetFocusedRowCellValue("dType", row1["dType"].ToString());
                                gvSelectedMaterials.SetFocusedRowCellValue("bal", row1["bal"].ToString());
                              //  gvSelectedMaterials.SetFocusedRowCellValue("avg", row1["avg"].ToString());
                            }
                            gvSelectedMaterials.Focus();
                            gvSelectedMaterials.FocusedRowHandle = gvSelectedMaterials.RowCount - 1;
                        }
                        rowNo++;
                    }
                }
            }
        }

        private void gvPortable_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null && e.Column.FieldName == "select")
            {
                gvPortable.SetRowCellValue(e.RowHandle, "select", e.Value);
            }
        }
        

        private void gvMobile_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            gvMobile.PostEditor();
            if (e.Column.FieldName == "qty")
            {
                if (gvMobile.GetFocusedRowCellDisplayText("v_type") == "3" || gvMobile.GetFocusedRowCellDisplayText("v_type") == "2")
                {
                    string prevQty = gvMobile.GetRowCellValue(gvMobile.FocusedRowHandle - 1, "v_type").ToString();
                }
            }
        }

        private void gvMobile_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && (gvMobile.GetFocusedRowCellValue("v_type").ToString() == "2" || gvMobile.GetFocusedRowCellValue("v_type") == "1"))
            {
                // conditions();
            }
        }
        

        private void gvPortable_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvPortable.FocusedColumn.FieldName == "select")
            {
                if (gvPortable.GetFocusedRowCellDisplayText("v_type") == "1")
                {
                    e.Cancel = true;
                }
            }
            if (gvPortable.FocusedColumn.FieldName == "isqty")
            {
                if (gvPortable.GetFocusedRowCellDisplayText("v_type") == "3")
                {
                    e.Cancel = true;
                }
            }

        }

        private void gvSelectedMaterials_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            value = e.Column.FieldName.ToString();
            if (value == "lineNo" || value == "asCode" || value == "des" || value == "unit" || value == "matCode" || value == "remarks")
            {
                gvSelectedMaterials.PostEditor();
                gvSelectedMaterials.SetFocusedRowCellValue("temp", "1");
            }
        }
        public string SetTINNo()
        {//rec ektath
            string TIN = txtTinNo.Text;
            string tin = BLL_CS_Issue_Note.Get_TIN_No();
          //  txtTinNo.Text = tin;
            return tin;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string TinNo = SetTINNo();
            Boolean[] saveDocument = new Boolean[2];
            Boolean[] saveTransaction = new Boolean[2];
            string lCode = txtloc.Text;
            string tinNo/*tin*/ = TinNo;//rec ektath
            string dType = txtdType.Text;
            string date1 = tdate /*dtpDate*/.Text;
            string jcat = txtJcat.Text;
            string jmain = jMain.Text;
            string sub = txtsub.Text;
            string spec = txtspec.Text;
            string extc = txtextc.Text;
            string ori = ocode.Text;
            string oLoc = txtWorkshopLoc.Text;
            string remark = txtRemark.Text;
            ////string dNo = tin;
            ////SetTINNo();
            ///
            if(lCode == "")
            {
                XtraMessageBox.Show("Please Select Warehouse.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if(jcat == "")
                {
                    XtraMessageBox.Show("Please Select Main Job Details.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtJcat.Focus();
                }
                else
                {
                    if(sub == "")
                    {
                        XtraMessageBox.Show("Please Select Sub Job Details.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtsub.Focus();
                    }
                    else
                    {
                        if (date1 == "")
                        {
                            XtraMessageBox.Show("Please Select Date.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            if (ori == "")
                            {
                                XtraMessageBox.Show("Please Select The Responsible Person.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                ocode.Focus();
                            }
                            else
                            {
                                if (gvSelectedMaterials.RowCount <= 0)
                                {
                                    XtraMessageBox.Show("Please Select Materials.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else
                                {
                                    bool haserr = false;
                                    for (int i = 0; i < gvSelectedMaterials.RowCount; i++)
                                    {
                                        string desc = gvSelectedMaterials.GetRowCellDisplayText(i, "des");
                                        string matCode = gvSelectedMaterials.GetRowCellDisplayText(i, "matCode");
                                        double qty = 0.0;
                                        string qtyString = gvSelectedMaterials.GetRowCellDisplayText(i, "isQty"); //QtyIsuued
                                        string bal = gvSelectedMaterials.GetRowCellDisplayText(i, "bal");
                                        double dobRec = Convert.ToDouble(string.IsNullOrEmpty(bal) ? "0" : bal);
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

                                        string remarks = gvSelectedMaterials.GetRowCellDisplayText(i, "remarks");
                                        string iType = gvSelectedMaterials.GetRowCellDisplayText(i, "iType");
                                        string avgRate = "";//gvSelectedMaterials.GetRowCellValue(i, "avg").ToString();
                                        if (dobRec < qty)
                                        {
                                            XtraMessageBox.Show("Quentity Issued should be less than or Equal to Balance Quentity .", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            haserr = true;
                                            return;
                                        }
                                        else
                                        {
                                            if (0 > qty)
                                            {
                                                XtraMessageBox.Show("Quentity Issued should be more than or Equal to 0 .", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                haserr = true;
                                                return;
                                            }
                                        }
                                    }
                                    if (!haserr)
                                    {
                                        saveDocument = BLL_CS_Issue_Note.SaveDocumentDetails(lCode, tinNo, dType, date1, jcat, jmain, sub, spec, extc, ori, oLoc, remark);


                                        if (saveDocument[0])
                                        {
                                            for (int i = 0; gvSelectedMaterials.RowCount > i; i++)
                                            {
                                                string desc = gvSelectedMaterials.GetRowCellDisplayText(i, "des");
                                                string matCode = gvSelectedMaterials.GetRowCellDisplayText(i, "matCode");
                                                double qty = 0.0;
                                                string qtyString = gvSelectedMaterials.GetRowCellDisplayText(i, "isQty"); //QtyIsuued
                                                string bal = gvSelectedMaterials.GetRowCellDisplayText(i, "bal");
                                                double dobRec = Convert.ToDouble(string.IsNullOrEmpty(bal) ? "0" : bal);
                                                string remarks = gvSelectedMaterials.GetRowCellDisplayText(i, "remarks");
                                                string iType = gvSelectedMaterials.GetRowCellDisplayText(i, "iType");
                                                string avgRate = "";
                                                saveTransaction = BLL_CS_Issue_Note.SaveAssetTransactionDetails(lCode, dType, tinNo, iType, matCode, qtyString, bal, remarks, date1, avgRate);
                                                gvMobile.RefreshData();

                                                if (!saveTransaction[0])
                                                {
                                                    XtraMessageBox.Show("Unsuccessful.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);// messge unsuccess
                                                    return;
                                                }
                                            }
                                            if (saveTransaction[0])
                                            {
                                                XtraMessageBox.Show("Saved Successfully.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                txtTinNo.Text = tinNo;
                                                // btn_Save.Enabled = false;
                                                btn_Save.Enabled = false;
                                                btnRefresh.Enabled = false;
                                                btnTinPrint.Enabled = true;
                                                txtJcat.ReadOnly = true;
                                                jMain.ReadOnly = true;
                                                txtsub.ReadOnly = true;
                                                txtspec.ReadOnly = true;
                                                txtextc.ReadOnly = true;
                                                txtloc.ReadOnly = true;
                                                txtResponsiblePerson.ReadOnly = true;
                                                txtRemark.ReadOnly = true;
                                                txtDescription.ReadOnly = true;
                                                tdate.ReadOnly = true;
                                                txtWorkshopLoc.ReadOnly = true;
                                                txtdType.ReadOnly = true;
                                                txtTinNo.ReadOnly = true;
                                                txtserch.ReadOnly = true;
                                                pnlEmployees.SendToBack();
                                                pnlJobDetails.SendToBack();
                                                //pnlSearch.SendToBack();
                                                pnlSub.SendToBack();
                                                gvMobile.OptionsBehavior.Editable = false;
                                                gvSelectedMaterials.OptionsBehavior.Editable = false;
                                                gcMobile.RefreshDataSource();
                                                gcMobile.DataSource = null;
                                                gcMobile.DataSource = BLL_CS_Issue_Note.loadMobile();
                                            }

                                        }
                                        else
                                        {
                                            XtraMessageBox.Show("Unsuccessful.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);// messge unsuccess
                                        }

                                    }
                                }
                            }
                        }
                    }

                }
                
                //else
                //{
                //    if(date1 == "")
                //    {
                //        XtraMessageBox.Show("Please Select Date.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    }
                //    else
                //    {
                //        if(ori == "")
                //        {
                //            XtraMessageBox.Show("Please Select The Responsible Person.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        }
                //        else
                //        {
                //            if(gvSelectedMaterials.RowCount <= 0)
                //            {
                //                XtraMessageBox.Show("Please Select Materials.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //            }
                //            else
                //            {
                //                bool haserr = false;
                //                for (int i = 0; i < gvSelectedMaterials.RowCount; i++)
                //                {
                //                    string desc = gvSelectedMaterials.GetRowCellDisplayText(i, "des");
                //                    string matCode = gvSelectedMaterials.GetRowCellDisplayText(i, "matCode");
                //                    double qty = 0.0;
                //                    string qtyString = gvSelectedMaterials.GetRowCellDisplayText(i, "isQty"); //QtyIsuued
                //                    string bal = gvSelectedMaterials.GetRowCellDisplayText(i, "bal");
                //                    double dobRec = Convert.ToDouble(string.IsNullOrEmpty(bal) ? "0" : bal);
                //                    if (!string.IsNullOrEmpty(qtyString))
                //                    {
                //                        if (!double.TryParse(qtyString, out qty))
                //                        {
                //                            XtraMessageBox.Show("Invalid quantity value. Please check the data.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //                            //MessageBox.Show("Invalid quantity value. Please check the data.");
                //                            qty = 0.0;
                //                            haserr = true;
                //                            return;
                //                        }
                //                    }
                //                    else
                //                    {
                //                        qty = 0.0;
                //                        XtraMessageBox.Show("Invalid quantity value. Please check the data.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //                        haserr = true;
                //                        return;
                //                    }
                                    
                //                    string remarks = gvSelectedMaterials.GetRowCellDisplayText(i, "remarks");
                //                    string iType = gvSelectedMaterials.GetRowCellDisplayText(i, "iType");
                //                    string avgRate = "";//gvSelectedMaterials.GetRowCellValue(i, "avg").ToString();
                //                    if (dobRec < qty)
                //                    {
                //                        XtraMessageBox.Show("Quentity Issued should be less than or Equal to Balance Quentity .", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //                        haserr = true;
                //                        return;
                //                    }
                //                    else
                //                    {
                //                        if (0 > qty)
                //                        {
                //                            XtraMessageBox.Show("Quentity Issued should be more than or Equal to 0 .", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //                            haserr = true;
                //                            return;
                //                        }
                //                    }
                //                }
                //                if (!haserr)
                //                {
                //                    saveDocument = BLL_CS_Issue_Note.SaveDocumentDetails(lCode, tinNo, dType, date1, jcat, jmain, sub, spec, extc, ori, oLoc, remark);


                //                    if (saveDocument[0])
                //                    {
                //                        for (int i = 0; gvSelectedMaterials.RowCount > i; i++)
                //                        {
                //                            string desc = gvSelectedMaterials.GetRowCellDisplayText(i, "des");
                //                            string matCode = gvSelectedMaterials.GetRowCellDisplayText(i, "matCode");
                //                            double qty = 0.0;
                //                            string qtyString = gvSelectedMaterials.GetRowCellDisplayText(i, "isQty"); //QtyIsuued
                //                            string bal = gvSelectedMaterials.GetRowCellDisplayText(i, "bal");
                //                            double dobRec = Convert.ToDouble(string.IsNullOrEmpty(bal) ? "0" : bal);
                //                            string remarks = gvSelectedMaterials.GetRowCellDisplayText(i, "remarks");
                //                            string iType = gvSelectedMaterials.GetRowCellDisplayText(i, "iType");
                //                            string avgRate = "";
                //                            saveTransaction = BLL_CS_Issue_Note.SaveAssetTransactionDetails(lCode, dType, tinNo, iType, matCode, qtyString, bal, remarks, date1, avgRate);
                //                            gvMobile.RefreshData();

                //                            if (!saveTransaction[0])
                //                            {
                //                                XtraMessageBox.Show("Unsuccessful.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);// messge unsuccess
                //                                return;
                //                            }
                //                        }
                //                        if (saveTransaction[0])
                //                        {
                //                            XtraMessageBox.Show("Saved Successfully.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                            txtTinNo.Text = tinNo;
                //                            // btn_Save.Enabled = false;
                //                            btn_Save.Enabled = false;
                //                            btnRefresh.Enabled = false;
                //                            btnTinPrint.Enabled = false;
                //                            txtJcat.ReadOnly = true;
                //                            jMain.ReadOnly = true;
                //                            txtsub.ReadOnly = true;
                //                            txtspec.ReadOnly = true;
                //                            txtextc.ReadOnly = true;
                //                            txtloc.ReadOnly = true;
                //                            txtResponsiblePerson.ReadOnly = true;
                //                            txtRemark.ReadOnly = true;
                //                            txtDescription.ReadOnly = true;
                //                            tdate.ReadOnly = true;
                //                            txtWorkshopLoc.ReadOnly = true;
                //                            txtdType.ReadOnly = true;
                //                            txtTinNo.ReadOnly = true;
                //                            txtserch.ReadOnly = true;
                //                            pnlEmployees.SendToBack();
                //                            pnlJobDetails.SendToBack();
                //                            //pnlSearch.SendToBack();
                //                            pnlSub.SendToBack();
                //                            gvMobile.OptionsBehavior.Editable = false;
                //                            gvSelectedMaterials.OptionsBehavior.Editable = false;
                //                            gcMobile.RefreshDataSource();
                //                            //gcMobile.DataSource = null;
                //                            //gcMobile.DataSource = BLL_CS_Issue_Note.loadMobile();
                //                        }

                //                    }
                //                    else
                //                    {
                //                        XtraMessageBox.Show("Unsuccessful.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);// messge unsuccess
                //                    }

                //                }
                //            }
                //        }
                //    }
                //}
            }
            //saveDocument = BLL_CS_Issue_Note.SaveDocumentDetails(lCode, tin, dType, date1, jcat, jmain, sub, spec, extc, ori, oLoc, remark);

            //for (int i = 0; gvSelectedMaterials.RowCount > i; i++)
            //{
            //    string desc = gvSelectedMaterials.GetRowCellDisplayText(i, "des");
            //    string matCode = gvSelectedMaterials.GetRowCellDisplayText(i, "matCode");
            //    double qty = 0.0;
            //    string qtyString = gvSelectedMaterials.GetRowCellDisplayText(i, "isQty"); //QtyIsuued
            //    string bal = gvSelectedMaterials.GetRowCellDisplayText(i, "bal");
            //    string remarks = gvSelectedMaterials.GetRowCellDisplayText(i, "remarks");
            //    string iType = gvSelectedMaterials.GetRowCellDisplayText(i, "iType");
            //    string avgRate = "";//gvSelectedMaterials.GetRowCellValue(i, "avg").ToString();
                
            //    saveTransaction = BLL_CS_Issue_Note.SaveAssetTransactionDetails(lCode, dType, tin, iType,  /*Dtype*/ matCode, qtyString, bal, remarks, date1, avgRate);

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
            //    txtTinNo.Text = tin; //rec ektath
            //}
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnTinPrint.Enabled = false;
            btnRefresh.Enabled = true;
            btn_Save.Enabled = true;
            txtJcat.ReadOnly = true;
            jMain.ReadOnly = true;
            txtsub.ReadOnly = true;
            txtspec.ReadOnly = true;
            txtextc.ReadOnly = true;
            txtloc.ReadOnly = true;
            txtResponsiblePerson.ReadOnly = true;
            txtRemark.ReadOnly = false;
            txtDescription.ReadOnly = true;
            tdate.ReadOnly = false;
            txtWorkshopLoc.ReadOnly = true;
            txtdType.ReadOnly = true;
            txtTinNo.ReadOnly = true;
            txtserch.ReadOnly = true;
            pnlEmployees.BringToFront();
            pnlJobDetails.BringToFront();
            pnlSearch.BringToFront();
            pnlSub.BringToFront();
            gvMobile.OptionsBehavior.Editable = true;
            gvSelectedMaterials.OptionsBehavior.Editable = true;



            txtTinNo.Text = "";
            txtserch.Text = "";
            txtJcat.Text = "";
            jMain.Text = "";
            txtsub.Text = "";
            txtspec.Text = "";
            txtextc.Text = "";
            txtDescription.Text = "";
            txtserch.Text = "";
            txtResponsiblePerson.Text = "";
            txtWorkshopLoc.Text = "";
            tdate.Text = DateTime.Today.ToString();
            txtTinNo.Text = "";
            cmbReferenceType.Text = "";
            cmbJobCat.Text = "";
            ocode.Text = "";
            txtDescription.Text = "";
            txtResponsiblePerson.Text = "";
            txtWorkshopLoc.Text = "";
            extc.Text = "";
            txtRemark.Text = "";

            //dtpDate.Text = "";
            //txtIWONo.Text = "";
            //txtResponsibilityHolder.Text = "";
            txtIssueNoteNo.Text = "";
            for (int i = 0; i < gvMobile.RowCount; i++)
            {
                if (gvMobile.GetRowCellValue(i,"select").ToString() == "True")
                {
                    gvMobile.SetRowCellValue(i, "select", "False");
                }

                //string mCode = gvMobile.GetRowCellDisplayText(i, "matCode");
                ////string docType3 = gvMobile.GetRowCellDisplayText(i, "doc type");
                //string mCode1 = gvSelectedMaterials.GetRowCellDisplayText(gvSelectedMaterials.FocusedRowHandle, "matCode");
                ////string docType = gvSelectedMaterials.GetRowCellDisplayText(gvSelectedMaterials.FocusedRowHandle, "dType");
                //if (mCode == mCode1)
                //{
                //    gvMobile.SetRowCellValue(i, "select", "False");
                //}
            }
            for (int i = gvSelectedMaterials.RowCount; i > -1; i--)
            {
                gvSelectedMaterials.DeleteRow(i);
            }
        }

        private void cmbJobCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ocode.Text = "";
            txtDescription.Text = "";
        }

        private void txtResponsiblePerson_DoubleClick(object sender, EventArgs e)
        {
            pnlEmployees.Visible = true;
            gcEmployees.DataSource = BLL_CS_Issue_Note.loadEmployees();
        }
        

        private void txtResponsibilityHolder_DoubleClick(object sender, EventArgs e)
        {
            // if (txtIWONo.Text == "")
            // {
            //pnlResponsiblHolder.Visible = true;
            //gvHolder.ApplyFindFilter("");
            string loc = "";
            if (txtWorkshopLoc.Text != "")
            {
                loc = txtWorkshopLoc.Text.Substring(0, 3);
            }
           // gcHolder.DataSource = dAL_CS_Issue_Note.loadResponsibilityHolder(loc);
            // }
        }

        

        private void txtJcat_DoubleClick(object sender, EventArgs e)
        {
            pnlJobDetails.Visible = true;
            gcJobDetails.DataSource = BLL_CS_Issue_Note.Search_Job_Details();
        }

        private void txtsub_DoubleClick(object sender, EventArgs e)
        {
            string jcat = txtJcat.Text;
            string jmain = jMain.Text;
            if (jcat == "")
            {
                XtraMessageBox.Show("Please Select Main Job.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show("Please Select Main Job.");
            }
            else
            {
                pnlSub.Visible = true;
                gcSub.DataSource = BLL_CS_Issue_Note.Search_Sub_Details(jcat, jmain);
            }
            //pnlSub.Visible = true;
            //string jcat = txtJcat.Text;
            //string jmain = jMain.Text;
            //gcSub.DataSource = BLL_CS_Issue_Note.Search_Sub_Details (jcat, jmain);
        }

        private void labelControl25_Click(object sender, EventArgs e)
        {
            pnlSub.Hide();
        }

        private void gvJobDetails_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            string jcat = "";
            string jmain = "";
            string desc = "";
            jcat = gvJobDetails.GetFocusedRowCellValue("jcat").ToString();
            jmain = gvJobDetails.GetFocusedRowCellValue("jmain").ToString();
            desc = gvJobDetails.GetFocusedRowCellValue("des").ToString();

            txtJcat.Text = jcat;
            jMain.Text = jmain;
            txtDescription.Text = desc;
            pnlJobDetails.Visible = false;
        }

        private void gvSub_RowCellClick(object sender, RowCellClickEventArgs e)
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

        private void labelControl22_Click(object sender, EventArgs e)
        {
            pnlEmployees.Hide();
        }

        private void gvEmployees_RowCellClick(object sender, RowCellClickEventArgs e)
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

        private void txtserch_DoubleClick(object sender, EventArgs e)
        {
            pnlSearch.Visible = true;
            gcSearch.DataSource = BLL_CS_Issue_Note.Search_Tin_No();
            //pnlSearch.Visible = true;

            //gcSearch.DataSource = BLL_CS_Issue_Note.Search_Tin_No();
            //MessageBox.Show("Test");
            //pnlSearch.BringToFront();
            //pnlSearch.Visible = false;
        }

        private void labelControl32_Click(object sender, EventArgs e)
        {
            pnlSearch.Hide();
        }

        private void gvSearch_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Test");


            string tin = "";
            string iType = "";
            string code = ocode.Text;
            DataTable dt = new DataTable();

            if (gvSearch.RowCount > 0)
            {
                tin = gvSearch.GetRowCellValue(gvSearch.FocusedRowHandle, "tin").ToString();
                if (tin != "")
                {
                    txtTinNo.Text = tin;
                }
                
                dt = BLL_CS_Issue_Note.getSearch_Tin_No(tin/*, code*/);
                foreach (DataRow drow in dt.Rows)
                {
                    txtWorkshopLoc.Text = drow["oloc"].ToString();
                    txtJcat.Text = drow["jcat"].ToString();
                    jMain.Text = drow["jmain"].ToString();
                    txtsub.Text = drow["sub"].ToString();
                    txtspec.Text = drow["spec"].ToString();
                    txtextc.Text = drow["extc"].ToString();
                    txtResponsiblePerson.Text = drow["name"].ToString();
                    tdate/*dtpDate*/.Text = drow["date1"].ToString();
                    txtloc.Text = "SEC - Service Center";
                    txtTinNo.Text = drow["doc"].ToString();
                    txtdType.Text = drow["dtype"].ToString();
                    txtserch.Text = drow["doc"].ToString();
                    txtDescription.Text = drow["desc"].ToString();
                    ocode.Text = drow["pcode"].ToString();
                    txtRemark.Text = drow["remark"].ToString();

                }
                dt = BLL_CS_Issue_Note.getMatTinNo( tin, iType);
                

                gcSelectedMaterials.DataSource = dt;


                pnlSearch.Visible = false;
            }
        }

        private void gvSelectedMaterials_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
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
                            //string docNo = gvSelectedMaterials.GetRowCellDisplayText(view.FocusedRowHandle, "docNo");
                            //string docType = gvSelectedMaterials.GetRowCellDisplayText(view.FocusedRowHandle, "dType");
                            string matCode = gvSelectedMaterials.GetRowCellDisplayText(view.FocusedRowHandle, "matCode");


                            view.DeleteRow(view.FocusedRowHandle);

                            bool hasRow = false;
                            for (int i = 0; i < gvSelectedMaterials.RowCount; i++)
                            {
                                //string docNo2 = gvSelectedMaterials.GetRowCellDisplayText(i, "docNo");
                                //string docType2 = gvSelectedMaterials.GetRowCellDisplayText(i, "dType");
                                string matCode2 = gvSelectedMaterials.GetRowCellDisplayText(i, "matCode");

                                if (/*docNo2 == docNo && docType2 == docType &&*/ matCode2 != matCode)
                                {
                                    hasRow = true;
                                }
                            }
                            if (!hasRow)
                            {
                                for (int i = 0; i < gvMobile.RowCount; i++)
                                {
                                    string matCode2 = gvMobile.GetRowCellDisplayText(i, "matCode");

                                    if (matCode2 == matCode)
                                    {
                                        gvMobile.SetRowCellValue(i, "select", "False");
                                    }
                                }
                            }

                            //if (!hasRow)
                            //{
                            //    for (int i = 0; i < gvMobile.RowCount; i++)
                            //    {
                            //        string docNo3 = gvMobile.GetRowCellDisplayText(i, "doc no");
                            //        string docType3 = gvMobile.GetRowCellDisplayText(i, "doc type");

                            //        if (docNo3 == docNo && docType3 == docType)
                            //        {
                            //            gvMobile.SetRowCellValue(i, "select", "False");
                            //        }
                            //    }
                            //}
                        }
                    }));
                }
            }
            
        }

        private void txtserch_TextChanged(object sender, EventArgs e)
        {
            if(txtTinNo.Text != "")
                {

                    btn_Save.Enabled = false;
                    btnRefresh.Enabled = false;
                    btnTinPrint.Enabled = true;
                    txtJcat.ReadOnly = true;
                    jMain.ReadOnly = true;
                    txtsub.ReadOnly = true;
                    txtspec.ReadOnly = true;
                    txtextc.ReadOnly = true;
                    txtloc.ReadOnly = true;
                    txtResponsiblePerson.ReadOnly = true;
                    txtRemark.ReadOnly = true;
                    txtDescription.ReadOnly = true;
                    tdate.ReadOnly = true;
                    txtWorkshopLoc.ReadOnly = true;
                    txtdType.ReadOnly = true;
                    txtTinNo.ReadOnly = true;
                    txtserch.ReadOnly = true;
                    pnlEmployees.SendToBack();
                    pnlJobDetails.SendToBack();
                    //pnlSearch.SendToBack();
                    pnlSub.SendToBack();
                    gvMobile.OptionsBehavior.Editable = false;
                    gvSelectedMaterials.OptionsBehavior.Editable = false;
                }
                else
                {
                    btnTinPrint.Enabled = false;
                    btnRefresh.Enabled = true;
                    btn_Save.Enabled = true;
                    txtJcat.ReadOnly = true;
                    jMain.ReadOnly = true;
                    txtsub.ReadOnly = true;
                    txtspec.ReadOnly = true;
                    txtextc.ReadOnly = true;
                    txtloc.ReadOnly = true;
                    txtResponsiblePerson.ReadOnly = true;
                    txtRemark.ReadOnly = false;
                    txtDescription.ReadOnly = true;
                    tdate.ReadOnly = false;
                    txtWorkshopLoc.ReadOnly = true;
                    txtdType.ReadOnly = true;
                    txtTinNo.ReadOnly = true;
                    txtserch.ReadOnly = true;
                    pnlEmployees.BringToFront();
                    pnlJobDetails.BringToFront();
                    pnlSearch.BringToFront();
                    pnlSub.BringToFront();
                    gvMobile.OptionsBehavior.Editable = true;
                    gvSelectedMaterials.OptionsBehavior.Editable = true;
                }
            
        }

        private void labelControl29_Click(object sender, EventArgs e)
        {

        }

        private void labelControl37_Click(object sender, EventArgs e)
        {
            panelControl1.Hide();
            
        }

        private void gvSelectedMaterials_KeyPress(object sender, KeyPressEventArgs e)
        {
            //var gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            //// Check if the current column is "isQty"
            //if (gridView != null && gridView.FocusedColumn != null && gridView.FocusedColumn.FieldName == "isQty")
            //{
            //    // Allow only numeric input and control keys
            //    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            //    {
            //        MessageBox.Show("Please enter numbers only.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        e.Handled = true; // Block invalid input
            //    }
            //}
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            gcMobile.DataSource = null;
            gcMobile.DataSource = BLL_CS_Issue_Note.loadMobile();
        }

        private void btnTinPrint_Click(object sender, EventArgs e)
        {
            //REC_Report.DataSource = BLL_CS_REC.getMatRecNo( rec);
            //REC_Report.DataSource = BLL_CS_REC.getSearchRecNo(rec);
            string jcat = txtJcat.Text;
            string jmain = jMain.Text;
            string sub = txtsub.Text;
            string spec = txtspec.Text;
            string extc = txtextc.Text;
            string loc = txtloc.Text;
            string ori = txtResponsiblePerson.Text;
            string oriloc = txtWorkshopLoc.Text;
            string date1 = tdate.Text;
            string desc = txtDescription.Text;
            string doctype = txtdType.Text;
            string docno = txtTinNo.Text;
            string res = txtRemark.Text;
            string sysID = "FMS";
            string appID = "";
            string time = DateTime.Now.ToString("HH:mm:ss");
            string sdate = DateTime.Now.ToString("yyyy-MM-dd");

            TIN_Report.Parameters["paramsysID"].Value = sysID.ToString();
            TIN_Report.Parameters["paramappID"].Value = appID.ToString();
            TIN_Report.Parameters["paramTime"].Value = DateTime.Now.ToString("HH:mm");
            TIN_Report.Parameters["paramRdate"].Value = sdate;
            TIN_Report.Parameters["paramrecNo"].Value = docno;
            TIN_Report.Parameters["paramjcat"].Value = jcat;
            TIN_Report.Parameters["paramjmain"].Value = jmain;
            TIN_Report.Parameters["paramsub"].Value = sub;
            TIN_Report.Parameters["paramspec"].Value = spec;
            TIN_Report.Parameters["paramextc"].Value = extc;
            TIN_Report.Parameters["parampname"].Value = desc;
            TIN_Report.Parameters["paramdtype"].Value = doctype;
            TIN_Report.Parameters["paramloc"].Value = loc;
            TIN_Report.Parameters["paramtdate"].Value = date1;
            TIN_Report.Parameters["paramResPerson"].Value = ori;
            TIN_Report.Parameters["paramWrkLoc"].Value = oriloc;
            TIN_Report.Parameters["paramres"].Value = res;

            string tin = txtTinNo.Text;//gvSearch.GetRowCellValue(gvSearch.FocusedRowHandle, "tin").ToString();
            string iType = gvSelectedMaterials.GetRowCellDisplayText(gvSearch.FocusedRowHandle,"iType").ToString();
            // string iType = gvSelectedMaterials.GetRowCellValue(gvSearch.FocusedRowHandle, "iType").ToString();
            TIN_Report.DataSource = null;
            TIN_Report.DataSource = BLL_CS_Issue_Note.getMatTinNo( tin, iType);
            TIN_Report.CreateDocument();
            ReportPrintTool PrintTool = new ReportPrintTool(TIN_Report);
            PrintTool.ShowRibbonPreview(UserLookAndFeel.Default);
        }
    }

}
