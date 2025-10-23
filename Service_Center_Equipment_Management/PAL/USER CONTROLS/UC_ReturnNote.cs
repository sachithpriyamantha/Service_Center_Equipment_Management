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

namespace Service_Center_Equipment_Management.PAL.USER_CONTROLS
{
    public partial class UC_ReturnNote : DevExpress.XtraEditors.XtraUserControl
    {
        //private BLL.BLL_CS_Return BLL_CS_Return = new BLL.BLL_CS_Return();

        //----------------------------------------------------------------------------------------------------------------
        string value = "";
        DAL.DAL_CS_Return da_obl = new DAL.DAL_CS_Return();
        string flag;
        public UC_ReturnNote()
        {
            InitializeComponent();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {

        }

        private void labelControl7_Click(object sender, EventArgs e)
        {
            pnlIssueItems.Hide();
        }

        private void UC_ReturnNote_Load(object sender, EventArgs e)
        {
            //pnlIssueItems.Hide();
            //pnlJobDetails.Hide();
            //pnlLocation.Hide();
            //pnlEmployees.Hide();
            //pnlResponsiblHolder.Hide();
            //txtReturnNoteNo.Focus();
            //gcPendingGrn.DataSource = da_obl.loadFirstReturn();
            //gcSecond.DataSource = da_obl.loadSecondReturn();
            //dtpDate.Text = DateTime.Today.ToString();


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Boolean[] assetTransactionRet = new Boolean[2];
            Boolean[] assetTransactionDetailsRet = new Boolean[2];
            Boolean[] updateReturn = new Boolean[2];

            string year = "";
            string returnNo = "";
            string jcat = "";
            string jmain = "";
            string resofficer = "";
            string tdate = "";
            string status = "";
            string remarks = "";
            string resHolder = "";
            string cat = "";
            string type = "";

            string temp = "";
            string lineNo = "";
            string asLoc = "";
            string origiLoc = "";
            string main = "";
            string sub = "";
            string refNoteNo = "";
            string reftranNo = "";
            string toolcondi = "";
            //string origiLoc = "";

            if (dtpDate.Text != "")
            {
                year = dtpDate.Text.Substring(0, 4);
            }

            jcat = cmbJobCat.Text;
            jmain = txtJobNo.Text;
            resofficer = txtServiceNo.Text;
            tdate = dtpDate.Text;
            status = "A";
            type = "R";

            //if (txtResponsibilityHolder.Text != "")
            //{
            //    resHolder = txtResponsibilityHolder.Text.Substring(0, 7);
            //    cat = da_obl.GetResponsibleOfficerWorkCat(resHolder);
            //}

            if (txtReturnNoteNo.Text == "")
            {
                returnNo = da_obl.GenerateTransferNo(type);
            }
            else if (txtReturnNoteNo.Text != "")
            {
                returnNo = txtReturnNoteNo.Text;
            }

            //if (txtWorkshopLoc.Text != "")
            //{
            //origiLoc = txtWorkshopLoc.Text.Substring(0, 3);
            //}

            if (gvSelectedMaterials.RowCount != 0)
            {
                // if (resHolder != "")
                //  {
                if (dtpDate.Text != "")
                {
                    assetTransactionRet = da_obl.SaveAssetTransactions(year, returnNo, jcat, jmain, resofficer, tdate, status, remarks, resHolder, cat, type);

                    for (int i = 0; i < gvSelectedMaterials.DataRowCount; i++)
                    {
                        temp = gvSelectedMaterials.GetRowCellDisplayText(i, "temp");
                        if (temp == "1")
                        {
                            lineNo = da_obl.GenerateLineNo(returnNo);
                            asLoc = gvSelectedMaterials.GetRowCellDisplayText(i, "asLoc");
                            remarks = gvSelectedMaterials.GetRowCellDisplayText(i, "remarks");
                            refNoteNo = gvSelectedMaterials.GetRowCellDisplayText(i, "issueno");
                            reftranNo = gvSelectedMaterials.GetRowCellDisplayText(i, "tran");
                            origiLoc = gvSelectedMaterials.GetRowCellDisplayText(i, "origiLoc");

                            if (gvSelectedMaterials.GetRowCellDisplayText(i, "asCode") != "")
                            {
                                main = gvSelectedMaterials.GetRowCellDisplayText(i, "asCode").Substring(0, 4);
                                sub = gvSelectedMaterials.GetRowCellDisplayText(i, "asCode").Substring(5);
                            }

                            if (gvSelectedMaterials.GetRowCellDisplayText(i, "toolcon") != "")
                            {
                                if (gvSelectedMaterials.GetRowCellDisplayText(i, "toolcon") == "Good")
                                    toolcondi = "3";
                                else if (gvSelectedMaterials.GetRowCellDisplayText(i, "toolcon") == "Damage")
                                    toolcondi = "2";
                                else if (gvSelectedMaterials.GetRowCellDisplayText(i, "toolcon") == "Lost")
                                    toolcondi = "1";
                                else if (gvSelectedMaterials.GetRowCellDisplayText(i, "toolcon") == "Worn Out")
                                    toolcondi = "4";
                                else if (gvSelectedMaterials.GetRowCellDisplayText(i, "toolcon") == "Damage - Repairable")
                                    toolcondi = "5";
                                else if (gvSelectedMaterials.GetRowCellDisplayText(i, "toolcon") == "Damage - Non Repairable")
                                    toolcondi = "6";

                                assetTransactionDetailsRet = da_obl.SaveAssetTransactionDetailsReturn(year, returnNo, main, sub, status, remarks, lineNo, asLoc, origiLoc, refNoteNo, reftranNo, toolcondi);
                                if (refNoteNo != "")
                                {
                                    if (refNoteNo.Substring(0, 1) == "I")
                                    {
                                        updateReturn = da_obl.updateReturnNo(refNoteNo, main, sub, reftranNo, returnNo, lineNo);
                                    }
                                }
                                //if (txtIssueNoteno.Text.Substring(0, 1) == "I")
                                //{
                                //    string issueno = txtIssueNoteno.Text;
                                //    updateReturn = da_obl.updateReturnNo(issueno, main, sub, reftranNo, returnNo, lineNo);
                                //}

                                gvSelectedMaterials.SetRowCellValue(i, "lineNo", lineNo);
                                txtReturnNoteNo.Text = returnNo;
                            }
                            else if (gvSelectedMaterials.GetRowCellDisplayText(i, "toolcon") == "")
                            {
                                XtraMessageBox.Show(this.LookAndFeel, "Please select a return tool status!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            //assetTransactionDetailsRet = da_obl.SaveAssetTransactionDetailsReturn(year, returnNo, main, sub, status, remarks, lineNo, asLoc, origiLoc, refNoteNo, reftranNo, toolcondi);
                            //gvSelectedMaterials.SetRowCellValue(i, "lineNo", lineNo);
                            //txtReturnNoteNo.Text = returnNo;
                        }
                    }
                }
                else if (dtpDate.Text == "")
                {
                    XtraMessageBox.Show(this.LookAndFeel, "Please select a return date!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //}
                // else if (resHolder == "")
                // {
                //    XtraMessageBox.Show(this.LookAndFeel, "Please select the responsibility holder!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //     return;
                //}
            }
            else if (gvSelectedMaterials.RowCount == 0)
            {
                XtraMessageBox.Show(this.LookAndFeel, "Please select the items to issue!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (assetTransactionRet[0] || assetTransactionRet[1] || assetTransactionDetailsRet[0] || assetTransactionDetailsRet[1])
            {
                XtraMessageBox.Show(this.LookAndFeel, "Successfully saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gcPendingGrn.DataSource = da_obl.loadFirstReturn();
                gcSecond.DataSource = da_obl.loadSecondReturn();

                //gcPendingGrn.DataSource = da_obl.loadFirst();
                //gcSecond.DataSource = da_obl.loadSecond();
                return;
            }
        }

        private void gvIssue_DoubleClick(object sender, EventArgs e)
        {
            GridView detailGrid = (sender as GridView);
            GridHitInfo hitInfo = (detailGrid.CalcHitInfo((e as MouseEventArgs).Location));

            if (hitInfo.Column == detailGrid.Columns["issueno"] || hitInfo.Column == detailGrid.Columns["jcat"] || hitInfo.Column == detailGrid.Columns["jmain"])
            {
                //splashScreenManager1.ShowWaitForm();
                string issueno = gvIssue.GetFocusedRowCellDisplayText("issueno");
                string jcat = gvIssue.GetFocusedRowCellDisplayText("jcat");
                string jmain = gvIssue.GetFocusedRowCellDisplayText("jmain");
                string origiloc = gvIssue.GetFocusedRowCellDisplayText("origiloc");
                string resofficer = gvIssue.GetFocusedRowCellDisplayText("reofficer");
                string offincharge = gvIssue.GetFocusedRowCellDisplayText("offincharge");
                string des = gvIssue.GetFocusedRowCellDisplayText("des");
                string iwo = gvIssue.GetFocusedRowCellDisplayText("iwo");

                if (flag == "R")
                {
                    txtReturnNoteNo.Text = issueno;
                }
                else if (flag == "I")
                {
                    txtIssueNoteno.Text = issueno;
                }
                txtWorkshopLoc.Text = origiloc;
                txtResponsibilityHolder.Text = resofficer;
                txtResponsiblePerson.Text = offincharge;
                cmbJobCat.Text = jcat;
                txtJobNo.Text = jmain;
                txtDescription.Text = des;
                txtIWONo.Text = iwo;
                if (jcat != "")
                {
                    cmbReferenceType.Text = "Project";
                }

                pnlIssueItems.Hide();

                gcPendingGrn.DataSource = da_obl.loadFirstReturn();
                gcSecond.DataSource = da_obl.loadSecondReturn();

                string issueNo = txtReturnNoteNo.Text;
                gcSelectedMaterials.DataSource = da_obl.LoadTransactionsIssued(issueNo);


                //splashScreenManager1.CloseWaitForm();
            }
        }

        private void gvPendingGrn_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            for (int i = gvSelectedMaterials.RowCount; i > -1; i--)
            {
                gvSelectedMaterials.DeleteRow(i);
            }

            if (e.Value != null && e.Column.FieldName == "select")
            {
                foreach (DataRow row in ((DataTable)gcPendingGrn.DataSource).Rows)
                {
                    if (row["select"].ToString() == "True")
                    {
                        //gvSelectedMaterials.PostEditor();
                        gvSelectedMaterials.AddNewRow();
                        gvSelectedMaterials.SetFocusedRowCellValue("asCode", row["asCode"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("des", row["des"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("matCode", row["matCode"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("issueno", row["issueno"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("asLoc", row["asLoc"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("tran", row["tran"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("origiLoc", row["origiLoc"].ToString());
                    }
                }
                foreach (DataRow row in ((DataTable)gcSecond.DataSource).Rows)
                {
                    if (row["select"].ToString() == "True")
                    {
                        //gvSelectedMaterials.PostEditor();
                        gvSelectedMaterials.AddNewRow();
                        gvSelectedMaterials.SetFocusedRowCellValue("asCode", row["asCode"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("des", row["des"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("matCode", row["matCode"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("issueno", row["issueno"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("asLoc", row["asLoc"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("tran", row["tran"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("origiLoc", row["origiLoc"].ToString());
                    }
                }
            }
        }

        private void gvSecond_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            for (int i = gvSelectedMaterials.RowCount; i > -1; i--)
            {
                gvSelectedMaterials.DeleteRow(i);
            }

            if (e.Value != null && e.Column.FieldName == "select")
            {
                foreach (DataRow row in ((DataTable)gcPendingGrn.DataSource).Rows)
                {
                    if (row["select"].ToString() == "True")
                    {
                        //gvSelectedMaterials.PostEditor();
                        gvSelectedMaterials.AddNewRow();
                        gvSelectedMaterials.SetFocusedRowCellValue("asCode", row["asCode"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("des", row["des"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("matCode", row["matCode"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("issueno", row["issueno"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("asLoc", row["asLoc"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("tran", row["tran"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("origiLoc", row["origiLoc"].ToString());
                    }
                }
                foreach (DataRow row in ((DataTable)gcSecond.DataSource).Rows)
                {
                    if (row["select"].ToString() == "True")
                    {
                        //gvSelectedMaterials.PostEditor();
                        gvSelectedMaterials.AddNewRow();
                        gvSelectedMaterials.SetFocusedRowCellValue("asCode", row["asCode"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("des", row["des"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("matCode", row["matCode"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("issueno", row["issueno"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("asLoc", row["asLoc"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("tran", row["tran"].ToString());
                        gvSelectedMaterials.SetFocusedRowCellValue("origiLoc", row["origiLoc"].ToString());
                    }
                }
            }
        }

        private void gvSelectedMaterials_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            value = e.Column.FieldName.ToString();
            if (value == "lineNo" || value == "asCode" || value == "des" || value == "unit" || value == "matCode" || value == "remarks" || value == "issueno" || value == "toolcon")
            {
                gvSelectedMaterials.PostEditor();
                gvSelectedMaterials.SetFocusedRowCellValue("temp", "1");
            }
        }

        private void gvPendingGrn_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null && e.Column.FieldName == "select")
            {
                gvPendingGrn.SetRowCellValue(e.RowHandle, "select", e.Value);
            }
        }

        private void gvSecond_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null && e.Column.FieldName == "select")
            {
                gvSecond.SetRowCellValue(e.RowHandle, "select", e.Value);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            cmbReferenceType.Text = "";
            cmbJobCat.Text = "";
            txtJobNo.Text = "";
            txtDescription.Text = "";
            txtResponsiblePerson.Text = "";
            txtWorkshopLoc.Text = "";
            txtServiceNo.Text = "";
            //dtpDate.Text = "";
            txtIWONo.Text = "";
            txtResponsibilityHolder.Text = "";
            txtIssueNoteno.Text = "";
            txtReturnNoteNo.Text = "";

            cmbJobCat.BackColor = Color.White;
            txtJobNo.BackColor = Color.White;
            txtDescription.BackColor = Color.White;
            txtWorkshopLoc.BackColor = Color.White;
            txtResponsiblePerson.BackColor = Color.White;

            gcPendingGrn.DataSource = da_obl.loadFirstReturn();
            gcSecond.DataSource = da_obl.loadSecondReturn();

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
            //pctUser.Image = (Image)Properties.Resources.User;
        }

        private void gvSelectedMaterials_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //if (e.Column.FieldName == "lineNo")
            if (gvSelectedMaterials.GetFocusedRowCellDisplayText("lineNo") != "")
            {
                gvSelectedMaterials.Columns["remarks"].OptionsColumn.AllowEdit = false;
                //repositoryItemComboBox2.ReadOnly = true;

            }
            else if (gvSelectedMaterials.GetFocusedRowCellDisplayText("lineNo") == "")
            {
                gvSelectedMaterials.Columns["remarks"].OptionsColumn.AllowEdit = true;
                //repositoryItemComboBox2.ReadOnly = false;
            }
        }

        private void labelControl11_Click(object sender, EventArgs e)
        {

        }

        private void labelControl13_Click(object sender, EventArgs e)
        {

        }

        private void labelControl29_Click(object sender, EventArgs e)
        {

        }

        private void labelControl17_Click(object sender, EventArgs e)
        {

        }

        private void labelControl21_Click(object sender, EventArgs e)
        {

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl19_Click(object sender, EventArgs e)
        {

        }

        private void txtIWONo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string iwono = txtIWONo.Text;
                DataTable dt = da_obl.loadIWODetails(iwono);
                foreach (DataRow rows in dt.Rows)
                {
                    cmbJobCat.Text = rows["jcat"].ToString();
                    txtJobNo.Text = rows["jmain"].ToString();
                    txtWorkshopLoc.Text = rows["origiloc"].ToString();
                    txtResponsiblePerson.Text = rows["resperson"].ToString();
                    txtDescription.Text = rows["des"].ToString();
                    txtServiceNo.Text = rows["service"].ToString();
                    txtIssueNoteno.Text = rows["issueno"].ToString();
                    txtResponsibilityHolder.Text = rows["resOfficer"].ToString();

                    if (rows["jcat"].ToString() != "")
                    {
                        cmbReferenceType.Text = "Project";
                    }
                }
            }
            catch (Exception)
            {
            }
            txtWorkshopLoc.BackColor = Color.White;
        }

        private void txtIssueNoteno_TextChanged(object sender, EventArgs e)
        {
            string issueNo = txtIssueNoteno.Text;
            gcSelectedMaterials.DataSource = da_obl.LoadTransactionsIssued(issueNo);
        }

        private void txtReturnNoteNo_DoubleClick(object sender, EventArgs e)
        {
            flag = "R";
            pnlIssueItems.Visible = true;
            gcIssue.DataSource = da_obl.LoadRetunItems();
        }

        private void gcPendingGrn_Load(object sender, EventArgs e)
        {
           // gcPendingGrn.DataSource = BLL_CS_Return.Select_Tin_Code();
        }
    }
}
