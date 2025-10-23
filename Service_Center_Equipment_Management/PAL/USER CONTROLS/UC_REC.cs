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

namespace Service_Center_Equipment_Management.PAL.USER_CONTROLS
{
    public partial class UC_REC : DevExpress.XtraEditors.XtraUserControl
    {
        REPORTS.REC_Report REC_Report = new REPORTS.REC_Report();
        private BLL.BLL_CS_REC BLL_CS_REC = new BLL.BLL_CS_REC();
        public UC_REC()
        {
            InitializeComponent();
            BLL_CS_REC = new BLL.BLL_CS_REC();
            pnlSearch.Visible = false;
            pnlJob.Visible = false;
            pnlSub.Visible = false;
        }

        private void UC_REC_Load(object sender, EventArgs e)
        {
            gc_mrq_details.DataSource = BLL_CS_REC.Select_Material_Code();//anumi04/21
            SetDate();
            tdate.Text = DateTime.Today.ToString();
            gc_Tools.DataSource = BLL_CS_REC.loadTOOL();//anumi04/30
            //tdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //string docNo = gv_mrq_details.GetFocusedRowCellDisplayText("doc no");
            //string type = gv_mrq_details.GetFocusedRowCellDisplayText("doc type");
            //gc_Mat.DataSource = BLL_CS_REC.Select_Transaction_Details(docNo, type);
        }
        public void SetDate()
        {
            string[] dates = BLL_CS_REC.Get_Edate_SDate();

            if (dates.Length >= 2 && !string.IsNullOrWhiteSpace(dates[0]) && !string.IsNullOrWhiteSpace(dates[1]))
            {
                txtSdate.Text = dates[0];
                txtEdate.Text = dates[1];

                if (DateTime.TryParse(dates[0], out DateTime minDate))
                {
                    tdate.Properties.MinValue = minDate;
                }
                else
                {
                    XtraMessageBox.Show("Invalid Start Date format.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);  
                    //MessageBox.Show("Invalid Start Date format.");
                }

                if (DateTime.TryParse(dates[1], out DateTime maxDate))
                {
                    tdate.Properties.MaxValue = maxDate;
                }
                else
                {
                    XtraMessageBox.Show("Invalid End Date format.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //MessageBox.Show("Invalid End Date format.");
                }
            }
            else
            {
                txtSdate.Text = "";
                txtEdate.Text = "";
                tdate.Properties.MinValue = DateTime.MinValue; // or any default
                tdate.Properties.MaxValue = DateTime.MaxValue; // or any default
                                                               // Optionally disable tdate or show a message
                                                               // MessageBox.Show("No available date range found.");
            }
            //string[] dates = BLL_CS_REC.Get_Edate_SDate();

            //if (dates.Length >= 2) 
            //{
            //    txtSdate.Text = dates[0]; 
            //    txtEdate.Text = dates[1];

            //    DateTime minDate = DateTime.Parse(dates[0]);
            //    tdate.Properties.MinValue = minDate;

            //    DateTime maxDate = DateTime.Parse(dates[1]);
            //    tdate.Properties.MaxValue = maxDate;
            //}
            //else
            //{
            //    txtSdate.Text = ""; 
            //    txtEdate.Text = ""; 
            //}
        }
        
        private void gv_mrq_details_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value != null && e.Column.FieldName == "select")
            {
                gv_mrq_details.SetRowCellValue(e.RowHandle, "select", e.Value);
            }
        }

        private void gv_mrq_details_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string jCat = "";
            string jmain = "";
            string sub = "";
            string spec = "";
            string extc = "";
            string descri = "";
            if (e.Column.FieldName == "select")
            {
                for (int i = gv_Mat.RowCount; i > -1; i--)
                {
                    string stts = gv_Mat.GetRowCellDisplayText(i, "srctabl")?.ToString();
                    if (stts == "m")
                    {
                        gv_Mat.DeleteRow(i);
                    }

                    //string stts = gv_mrq_details.GetRowCellValue(i, "srctabl").ToString();
                    //if(stts == "m")
                    //{
                    //    gv_Mat.DeleteRow(i);
                    //}
                    ////gv_Mat.DeleteRow(i);
                }

                bool hasSelectedRow = false; // Flag to check if any row is selected//anumi24/02

                if (e.Value != null && e.Column.FieldName == "select")
                {
                    int rowNo = 0;
                    foreach (DataRow row in ((DataTable)gc_mrq_details.DataSource).Rows)
                    {
                        if (row["select"].ToString() == "True")
                        {
                            hasSelectedRow = true;//anumi24/02
                            string docNo = row["doc no"].ToString(); /*gv_mrq_details.GetRowCellDisplayText(rowNo, "doc no");*/
                            string type = row["doc type"].ToString();/*gv_mrq_details.GetRowCellDisplayText(rowNo, "doc type");*/
                            DataTable dt1 = BLL_CS_REC.Select_Transaction_Details(docNo, type);
                            jCat = gv_mrq_details.GetFocusedRowCellValue("jcat").ToString();
                            jmain = gv_mrq_details.GetFocusedRowCellValue("jmain").ToString();
                            sub = gv_mrq_details.GetFocusedRowCellValue("sub").ToString();
                            spec = gv_mrq_details.GetFocusedRowCellValue("spec").ToString();
                            extc = gv_mrq_details.GetFocusedRowCellValue("extc").ToString();
                            descri = gv_mrq_details.GetFocusedRowCellValue("desc").ToString();
                            // string job = gv_Mat.GetFocusedRowCellValue("jcat").ToString();

                            //jCat = job;
                            txtJcat.Text = jCat;
                            jMain.Text = jmain;
                            txtsub.Text = sub;
                            txtspec.Text = spec;
                            txtextc.Text = extc;
                            txtpName.Text = descri;
                            // string job = gv_mrq_details.GetFocusedRowCellValue("jcat").ToString();
                            //// string job = gv_Mat.GetFocusedRowCellValue("jcat").ToString();

                            //job = jCat;
                            //for (int i = gv_Mat.RowCount; i > -1; i--)
                            //{
                            //    gv_Mat.DeleteRow(i);
                            //}

                            foreach (DataRow row1 in dt1.Rows)
                            {

                                gv_Mat.AddNewRow();
                                gv_Mat.SetFocusedRowCellValue("loc_code", row["loc"].ToString());
                                gv_Mat.SetFocusedRowCellValue("matCode", row1["matCode"].ToString());
                                gv_Mat.SetFocusedRowCellValue("desc", row1["desc"].ToString());
                                gv_Mat.SetFocusedRowCellValue("unit", row1["unit"].ToString());
                                gv_Mat.SetFocusedRowCellValue("quntity", row1["quntity"].ToString());
                                gv_Mat.SetFocusedRowCellValue("umr", row1["umr"].ToString());
                                gv_Mat.SetFocusedRowCellValue("remark", row1["remark"].ToString());
                                gv_Mat.SetFocusedRowCellValue("stts", row1["stts"].ToString());
                                gv_Mat.SetFocusedRowCellValue("refNo", row1["refNo"].ToString());
                                gv_Mat.SetFocusedRowCellValue("line", row1["line"].ToString());
                                gv_Mat.SetFocusedRowCellValue("docNo", row1["docNo"].ToString());
                                gv_Mat.SetFocusedRowCellValue("dType", row1["dType"].ToString());
                                gv_Mat.SetFocusedRowCellValue("bal", row1["bal"].ToString());
                                gv_Mat.SetFocusedRowCellValue("avg", row1["avg"].ToString());
                                gv_Mat.SetFocusedRowCellValue("srctabl", "m");
                                //gv_Mat.SetFocusedRowCellValue('m', row1["srctabl"].ToString());
                            }
                            //string job =  gv_Mat.GetFocusedRowCellValue("jcat").ToString();
                            // job = jcat;
                            gv_Mat.Focus();
                            gv_Mat.FocusedRowHandle = gv_Mat.RowCount - 1;
                        }
                        rowNo++;
                    }
                }
                if (!hasSelectedRow)
                {
                    txtJcat.Text = "";
                    jMain.Text = "";
                    txtsub.Text = "";
                    txtspec.Text = "";
                    txtextc.Text = "";
                    txtpName.Text = "";
                }
            }
        }
        

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            
        }

        private void gv_Mat_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if(txtRecNo.Text == "")
            {
                if (e.HitInfo.InRow)
                {
                    e.Menu.Items.Clear();

                    e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("Remove", (o, args) =>
                    {
                        GridView view = sender as GridView;
                        if (view != null && view.FocusedRowHandle >= 0)
                        {
                            string docNo = gv_Mat.GetRowCellDisplayText(view.FocusedRowHandle, "docNo");
                            string docType = gv_Mat.GetRowCellDisplayText(view.FocusedRowHandle, "dType");
                            string matCode = gv_Mat.GetRowCellDisplayText(view.FocusedRowHandle, "matCode");

                            view.DeleteRow(view.FocusedRowHandle);

                            bool hasRow = false;
                            for (int i = 0; i < gv_Mat.RowCount; i++)
                            {
                                string docNo2 = gv_Mat.GetRowCellDisplayText(i, "docNo");
                                string docType2 = gv_Mat.GetRowCellDisplayText(i, "dType");
                                string matCode2 = gv_Mat.GetRowCellDisplayText(i, "matCode");

                                if (docNo2 == docNo && docType2 == docType && matCode2 != matCode)
                                {
                                    hasRow = true;
                                }
                            }

                            if (!hasRow)
                            {
                                for (int i = 0; i < gv_mrq_details.RowCount; i++)
                                {
                                    string docNo3 = gv_mrq_details.GetRowCellDisplayText(i, "doc no");
                                    string docType3 = gv_mrq_details.GetRowCellDisplayText(i, "doc type");

                                    if (docNo3 == docNo && docType3 == docType)
                                    {
                                        gv_mrq_details.SetRowCellValue(i, "select", "False");
                                        //gv_Tools.RefreshData();
                                    }
                                }


                                for (int i = 0; i < gv_Tools.RowCount; i++)
                                {
                                    string mcode1 = gv_Tools.GetRowCellDisplayText(i, "mcode");
                                    //string mcode2 = gv_Mat.GetRowCellDisplayText(i, "matCode");

                                    if (mcode1 == matCode)
                                    {
                                        gv_Tools.SetRowCellValue(i, "select", "False");
                                    }
                                }
                            }
                        }
                    }));
                }
            }
            
        }

        public string SetRecNo()
        {
            string REC = txtRecNo.Text;
            string rec = BLL_CS_REC.Get_Rec_No();
            //  txtTinNo.Text = tin;
            return rec;
            //string REC = txtRecNo.Text;
            //string rec = BLL_CS_REC.Get_Rec_No();
            //txtRecNo.Text = rec;
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            string RecNo = SetRecNo();
            Boolean[] saveDocument = new Boolean[2];
            Boolean[] saveTransaction = new Boolean[2];

            string wCode = txtwCode.Text;
            string sub = txtsub.Text;
            string spec = txtspec.Text;
            string jcat = txtJcat.Text;
            string jmain = jMain.Text;
            string extc = txtextc.Text;
            string tdate1 = tdate.Text;
            string loc = txtLoc.Text;
            string dType = txtdType.Text;
            string recNo = RecNo;//
            //string lCode = txtwCode
            string dNo = "";
            
            if(wCode == "")
            {
                XtraMessageBox.Show("Please Select Warehouse.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show("Please Select Warehouse.");
            }
            else
            {
                if(jcat == "")
                {
                    XtraMessageBox.Show("Please Select Main Job.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtJcat.Focus();
                    //MessageBox.Show("Please Select Main Job.");
                }
                else
                {
                    if (jmain == "")
                    {
                        XtraMessageBox.Show("Please Select Main Job.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //MessageBox.Show("Please Select Sub Job.");
                        jMain.Focus();
                    }
                    else
                    {
                        if (tdate1 == "")
                        {
                            XtraMessageBox.Show("Please Select The Date.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //MessageBox.Show("Please Select The Date.");
                        }
                        else
                        {
                            //if (tdate1 != "")
                            //{
                            DateTime sdate = Convert.ToDateTime(txtSdate.Text);
                            DateTime edate = Convert.ToDateTime(txtEdate.Text);
                            DateTime selectDate = Convert.ToDateTime(tdate.Text);

                            if (selectDate < sdate || selectDate > edate)
                            {
                                XtraMessageBox.Show("Please select the date between '" + sdate.ToString("yyyy-MM-dd") + "' and '" + edate.ToString("yyyy-MM-dd") + "'.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tdate.Text = "";
                            }
                            //}
                            else
                            {
                                if (gv_Mat.RowCount <= 0)
                                {
                                    XtraMessageBox.Show("Please Select Materials.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    //MessageBox.Show("Please Select Materials .");
                                }
                                else
                                {
                                    bool haserr = false;
                                    for (int i = 0; i < gv_Mat.RowCount; i++)
                                    {
                                        string lCode = gv_Mat.GetRowCellDisplayText(i, "loc_code");
                                        //string DNo = gv_Mat.GetRowCellDisplayText("docNo");
                                        //string Dtype = gv_Mat.GetRowCellDisplayText("dType");
                                        string desc = gv_Mat.GetRowCellDisplayText(i, "desc");
                                        string matCode = gv_Mat.GetRowCellDisplayText(i, "matCode");
                                        double qty = 0.0;
                                        string qtyString = gv_Mat.GetRowCellValue/*GetRowCellDisplayText*/(i, "quntity").ToString();
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
                                        string remark = gv_Mat.GetRowCellDisplayText(i, "remark");
                                        string tool = gv_Mat.GetRowCellDisplayText(i, "stts");
                                        string refno = gv_Mat.GetRowCellDisplayText(i, "docNo");
                                        string line = gv_Mat.GetRowCellDisplayText(i, "line");
                                        string rType = gv_Mat.GetRowCellDisplayText(i, "dType");
                                        string mrq = gv_Mat.GetRowCellDisplayText(i, "umr");
                                        string mrqV = gv_Mat.GetRowCellValue(i, "umr").ToString();

                                        double dobMrq = Convert.ToDouble(string.IsNullOrEmpty(mrq) ? "0" : mrq);

                                        if (dobMrq < qty)
                                        {
                                            XtraMessageBox.Show("Quentity Received should be less than or Equal to MRQ Quentity .", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            haserr = true;
                                            return;
                                        }
                                        else
                                        {
                                            if (0 > qty)
                                            {
                                                XtraMessageBox.Show("Quentity Received should be more than or Equal to 0 .", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                haserr = true;
                                                return;
                                            }
                                        }

                                    }

                                    if (!haserr)
                                    {
                                        //SetRecNo();
                                        saveDocument = BLL_CS_REC.SaveDocumentDetails(wCode, tdate1, dType, recNo, dNo, jcat, jmain, sub, spec, extc);

                                        if (saveDocument[0])
                                        {
                                            for (int i = 0; gv_Mat.RowCount > i; i++)
                                            {
                                                string lCode = gv_Mat.GetRowCellDisplayText(i, "loc_code");
                                                //string DNo = gv_Mat.GetRowCellDisplayText("docNo");
                                                //string Dtype = gv_Mat.GetRowCellDisplayText("dType");
                                                string desc = gv_Mat.GetRowCellDisplayText(i, "desc");
                                                string matCode = gv_Mat.GetRowCellDisplayText(i, "matCode");
                                                double qty = 0.0;
                                                string qtyString = gv_Mat.GetRowCellValue(i, "quntity").ToString();
                                                //if (!string.IsNullOrEmpty(qtyString))
                                                //{
                                                //    if (!double.TryParse(qtyString, out qty))
                                                //    {
                                                //        MessageBox.Show("Invalid quantity value. Please check the data.");
                                                //        qty = 0.0;
                                                //        haserr = true;
                                                //        return;
                                                //    }
                                                //}
                                                //else
                                                //{
                                                //    qty = 0.0;
                                                //}
                                                string bal = gv_Mat.GetRowCellDisplayText(i, "bal");
                                                string remark = gv_Mat.GetRowCellDisplayText(i, "remark");
                                                string tool = gv_Mat.GetRowCellDisplayText(i, "stts");
                                                string refno = gv_Mat.GetRowCellDisplayText(i, "docNo");
                                                string line = gv_Mat.GetRowCellDisplayText(i, "line");
                                                string rType = gv_Mat.GetRowCellDisplayText(i, "dType");
                                                string mrq = gv_Mat.GetRowCellDisplayText(i, "umr");
                                                string mrqV = gv_Mat.GetRowCellValue(i, "umr").ToString();
                                                string avgRate = gv_Mat.GetRowCellValue(i, "avg").ToString();
                                                double dobMrq = Convert.ToDouble(string.IsNullOrEmpty(mrq) ? "0" : mrq);
                                                string mrqQty = gv_Mat.GetRowCellDisplayText(i, "bal").ToString();

                                                //SetRecNo();
                                                saveTransaction = BLL_CS_REC.SaveAssetTransactionDetails(wCode/*lCode*/, dType, recNo, matCode, qtyString, bal, remark, tool, refno, line, rType, avgRate, tdate1, mrqQty);

                                                if (!saveTransaction[0])
                                                {
                                                    XtraMessageBox.Show("Unsuccessful.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);// messge unsuccess
                                                    return;
                                                }
                                            }
                                            if (saveTransaction[0])
                                            {
                                                XtraMessageBox.Show("Saved Successfully.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                txtRecNo.Text = recNo;
                                                btn_Save.Enabled = false;
                                                //  btn_Save.Enabled = false;
                                                btnRefresh.Enabled = false;
                                                txtwCode.ReadOnly = true;
                                                txtJcat.ReadOnly = true;
                                                txtsub.ReadOnly = true;
                                                txtspec.ReadOnly = true;
                                                txtextc.ReadOnly = true;
                                                tdate.ReadOnly = true;
                                                txtRecNo.ReadOnly = true;
                                                txtdType.ReadOnly = true;
                                                txtJcat.ReadOnly = true;
                                                pnlJob.SendToBack();
                                                pnlSub.SendToBack();
                                                gv_Mat.OptionsBehavior.Editable = false;
                                                gv_mrq_details.OptionsBehavior.Editable = false;
                                                gv_Tools.OptionsBehavior.Editable = false;
                                                //
                                                gc_mrq_details.DataSource = null;
                                                //if(txtJcat.Text != "" && jMain.Text != "")
                                                //{
                                                //    gc_mrq_details.DataSource = BLL_CS_REC.Load_Select_Material_Code(jcat, jmain, sub, spec, extc);//anumi 04/21
                                                //}
                                                gc_mrq_details.DataSource = BLL_CS_REC.Select_Material_Code();
                                                SetRecNo();
                                                gc_Tools.DataSource = null;
                                                gc_Tools.DataSource = BLL_CS_REC.loadTOOL();
                                            }

                                        }
                                        else
                                        {
                                            XtraMessageBox.Show("Unsuccessful.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);// messge unsuccess
                                        }

                                    }

                                    //if(gv_Mat.GetFocusedRowCellValue("qty").ToString()!="")
                                    //{
                                    //    saveDocument = BLL_CS_REC.SaveDocumentDetails(wCode, tdate1, loc, dType, recNo, dNo, jcat, jmain, sub, spec, extc);
                                    //    //saveDocument = BLL_CS_REC.SaveDocumentDetails(wCode, pNo, tdate, loc, dType, recNo, refNo, refType);
                                    //    saveTransaction = BLL_CS_REC.SaveAssetTransactionDetails(lCode, dType, recNo, matCode, qtyString, remark, tool, refno, line, rType);
                                    //    //XtraMessageBox.Show("Please Enter The Quentity.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    //}
                                    //else
                                    //{
                                    //    XtraMessageBox.Show("Please Enter The Quentity.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                    //}
                                }
                            }
                            //if(gv_Mat.RowCount <= 0)
                            //{
                            //    XtraMessageBox.Show("Please Select Materials.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //    //MessageBox.Show("Please Select Materials .");
                            //}
                            //else
                            //{
                            //    bool haserr = false;
                            //    for(int i = 0; i < gv_Mat.RowCount ; i++)
                            //    {
                            //        string lCode = gv_Mat.GetRowCellDisplayText(i, "loc_code");
                            //        //string DNo = gv_Mat.GetRowCellDisplayText("docNo");
                            //        //string Dtype = gv_Mat.GetRowCellDisplayText("dType");
                            //        string desc = gv_Mat.GetRowCellDisplayText(i, "desc");
                            //        string matCode = gv_Mat.GetRowCellDisplayText(i, "matCode");
                            //        double qty = 0.0;
                            //        string qtyString = gv_Mat.GetRowCellDisplayText(i, "quntity");
                            //        if (!string.IsNullOrEmpty(qtyString))
                            //        {
                            //            if (!double.TryParse(qtyString, out qty))
                            //            {
                            //                XtraMessageBox.Show("Invalid quantity value. Please check the data.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //                //MessageBox.Show("Invalid quantity value. Please check the data.");
                            //                qty = 0.0;
                            //                haserr = true;
                            //                return;
                            //            }
                            //        }
                            //        else
                            //        {
                            //            qty = 0.0;
                            //            XtraMessageBox.Show("Invalid quantity value. Please check the data.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //            haserr = true;
                            //            return;
                            //        }
                            //        string remark = gv_Mat.GetRowCellDisplayText(i, "remark");
                            //        string tool = gv_Mat.GetRowCellDisplayText(i, "stts");
                            //        string refno = gv_Mat.GetRowCellDisplayText(i, "docNo");
                            //        string line = gv_Mat.GetRowCellDisplayText(i, "line");
                            //        string rType = gv_Mat.GetRowCellDisplayText(i, "dType");
                            //        string mrq = gv_Mat.GetRowCellDisplayText(i, "umr");
                            //        string mrqV = gv_Mat.GetRowCellValue(i, "umr").ToString();

                            //        double dobMrq = Convert.ToDouble(string.IsNullOrEmpty(mrq) ? "0" : mrq);

                            //        if(dobMrq < qty)
                            //        {
                            //            XtraMessageBox.Show("Quentity Received should be less than or Equal to MRQ Quentity .", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //            haserr = true;
                            //            return;
                            //        }
                            //        else
                            //        {
                            //            if (0 > qty)
                            //            {
                            //                XtraMessageBox.Show("Quentity Received should be more than or Equal to 0 .", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //                haserr = true;
                            //                return;
                            //            }
                            //        }

                            //    }

                            //    if(!haserr)
                            //    {
                            //        //SetRecNo();
                            //        saveDocument = BLL_CS_REC.SaveDocumentDetails(wCode, tdate1,  dType, recNo, dNo, jcat, jmain, sub, spec, extc);

                            //        if (saveDocument[0])
                            //        {
                            //            for (int i = 0; gv_Mat.RowCount > i; i++)
                            //            {
                            //                string lCode = gv_Mat.GetRowCellDisplayText(i, "loc_code");
                            //                //string DNo = gv_Mat.GetRowCellDisplayText("docNo");
                            //                //string Dtype = gv_Mat.GetRowCellDisplayText("dType");
                            //                string desc = gv_Mat.GetRowCellDisplayText(i, "desc");
                            //                string matCode = gv_Mat.GetRowCellDisplayText(i, "matCode");
                            //                double qty = 0.0;
                            //                string qtyString = gv_Mat.GetRowCellDisplayText(i, "quntity");
                            //                //if (!string.IsNullOrEmpty(qtyString))
                            //                //{
                            //                //    if (!double.TryParse(qtyString, out qty))
                            //                //    {
                            //                //        MessageBox.Show("Invalid quantity value. Please check the data.");
                            //                //        qty = 0.0;
                            //                //        haserr = true;
                            //                //        return;
                            //                //    }
                            //                //}
                            //                //else
                            //                //{
                            //                //    qty = 0.0;
                            //                //}
                            //                string bal = gv_Mat.GetRowCellDisplayText(i,"bal");
                            //                string remark = gv_Mat.GetRowCellDisplayText(i, "remark");
                            //                string tool = gv_Mat.GetRowCellDisplayText(i, "stts");
                            //                string refno = gv_Mat.GetRowCellDisplayText(i, "docNo");
                            //                string line = gv_Mat.GetRowCellDisplayText(i, "line");
                            //                string rType = gv_Mat.GetRowCellDisplayText(i, "dType");
                            //                string mrq = gv_Mat.GetRowCellDisplayText(i, "umr");
                            //                string mrqV = gv_Mat.GetRowCellValue(i, "umr").ToString();
                            //                string avgRate = gv_Mat.GetRowCellValue(i, "avg").ToString(); 
                            //                double dobMrq = Convert.ToDouble(string.IsNullOrEmpty(mrq) ? "0" : mrq);
                            //                string mrqQty = gv_Mat.GetRowCellDisplayText(i, "bal").ToString();

                            //                //SetRecNo();
                            //                saveTransaction = BLL_CS_REC.SaveAssetTransactionDetails(wCode/*lCode*/, dType, recNo, matCode, qtyString, bal, remark, tool, refno, line, rType, avgRate, tdate1, mrqQty);

                            //                if (!saveTransaction[0])
                            //                {
                            //                    XtraMessageBox.Show("Unsuccessful.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);// messge unsuccess
                            //                    return;
                            //                }
                            //            }
                            //            if (saveTransaction[0])
                            //            {
                            //                XtraMessageBox.Show("Saved Successfully.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //                txtRecNo.Text = recNo;
                            //                btn_Save.Enabled = false;
                            //            //  btn_Save.Enabled = false;
                            //                btnRefresh.Enabled = false;
                            //                txtwCode.ReadOnly = true;
                            //                txtJcat.ReadOnly = true;
                            //                txtsub.ReadOnly = true;
                            //                txtspec.ReadOnly = true;
                            //                txtextc.ReadOnly = true;
                            //                tdate.ReadOnly = true;
                            //                txtRecNo.ReadOnly = true;
                            //                txtdType.ReadOnly = true;
                            //                txtJcat.ReadOnly = true;
                            //                pnlJob.SendToBack();
                            //                pnlSub.SendToBack();
                            //                gv_Mat.OptionsBehavior.Editable = false;
                            //                gv_mrq_details.OptionsBehavior.Editable = false;
                            //                //
                            //                gc_mrq_details.DataSource = null;
                            //                //if(txtJcat.Text != "" && jMain.Text != "")
                            //                //{
                            //                //    gc_mrq_details.DataSource = BLL_CS_REC.Load_Select_Material_Code(jcat, jmain, sub, spec, extc);//anumi 04/21
                            //                //}
                            //                gc_mrq_details.DataSource = BLL_CS_REC.Select_Material_Code();
                            //                SetRecNo();
                            //            }

                            //        }
                            //        else
                            //        {
                            //            XtraMessageBox.Show("Unsuccessful.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);// messge unsuccess
                            //        }

                            //    }

                            //    //if(gv_Mat.GetFocusedRowCellValue("qty").ToString()!="")
                            //    //{
                            //    //    saveDocument = BLL_CS_REC.SaveDocumentDetails(wCode, tdate1, loc, dType, recNo, dNo, jcat, jmain, sub, spec, extc);
                            //    //    //saveDocument = BLL_CS_REC.SaveDocumentDetails(wCode, pNo, tdate, loc, dType, recNo, refNo, refType);
                            //    //    saveTransaction = BLL_CS_REC.SaveAssetTransactionDetails(lCode, dType, recNo, matCode, qtyString, remark, tool, refno, line, rType);
                            //    //    //XtraMessageBox.Show("Please Enter The Quentity.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //    //}
                            //    //else
                            //    //{
                            //    //    XtraMessageBox.Show("Please Enter The Quentity.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                
                            //    //}
                            //}
                        }
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            btn_Save.Enabled = true;
            btnRefresh.Enabled = true;
            txtwCode.ReadOnly = true;
            txtJcat.ReadOnly = false;
            txtsub.ReadOnly = true;
            txtspec.ReadOnly = true;
            txtextc.ReadOnly = true;
            tdate.ReadOnly = false;
            txtRecNo.ReadOnly = true;
            txtdType.ReadOnly = true;
            //txtJcat.ReadOnly = true;
            pnlJob.BringToFront();
            pnlSub.BringToFront();
            gv_Mat.OptionsBehavior.Editable = true;
            gv_mrq_details.OptionsBehavior.Editable = true;
            gv_Tools.OptionsBehavior.Editable = true;


            gc_mrq_details.DataSource = null;
            gc_mrq_details.DataSource = BLL_CS_REC.Select_Material_Code();
            gc_Tools.DataSource = null;
            gc_Tools.DataSource = BLL_CS_REC.loadTOOL();

            tdate.Text = DateTime.Today.ToString();
            txtRecNo.Text = "";
            txtserch.Text = "";
            txtJcat.Text = "";
            jMain.Text = "";
            txtsub.Text = "";
            txtspec.Text = "";
            txtextc.Text = "";
            txtpName.Text = "";
            //tdate.Text = "";
            //gc_Mat.DataSource = null;
            for (int i = 0; i < gv_mrq_details.RowCount; i++)
            {
                if(gv_mrq_details.GetRowCellValue(i, "select").ToString() == "True")
                {
                    gv_mrq_details.SetRowCellValue(i, "select", "False");
                }
                //string docNo3 = gv_mrq_details.GetRowCellDisplayText(i, "doc no");
                //string docType3 = gv_mrq_details.GetRowCellDisplayText(i, "doc type");
                //string docNo = gv_Mat.GetRowCellDisplayText(i/*gv_Mat.FocusedRowHandle*/, "docNo");
                //string docType = gv_Mat.GetRowCellDisplayText(i/*gv_Mat.FocusedRowHandle*/, "dType");
                //if (docNo3 == docNo && docType3 == docType)
                //{
                //    gv_mrq_details.SetRowCellValue(i, "select", "False");
                //}
            }

            for (int i = 0; i < gv_Tools.RowCount; i++)
            {
                if (gv_Tools.GetRowCellValue(i, "select").ToString() == "True")
                {
                    gv_Tools.SetRowCellValue(i, "select", "False");
                }
            }
            for (int i = gv_Mat.RowCount; i > -1; i--)
            {
                gv_Mat.DeleteRow(i);
            }


        }

        private void txtserch_DoubleClick(object sender, EventArgs e)
        {
            pnlSearch.Visible = true;
            gcSearch.DataSource = BLL_CS_REC.Search_Rec_No();
            //pnlSearch.Visible = false;
        }

        private void labelControl9_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
        }
        

        private void gvSearch_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Test");


            string rec = "";
            DataTable dt = new DataTable();

            if(gvSearch.RowCount > 0)
            {
                rec = gvSearch.GetRowCellValue(gvSearch.FocusedRowHandle, "recNo").ToString();
                if(rec != "")
                {
                    txtRecNo.Text = rec;
                }
                dt = BLL_CS_REC.getSearchRecNo(rec);
                foreach (DataRow drow in dt.Rows)
                {
                    txtwCode.Text = "SEC - Service Center";
                    txtJcat.Text = drow["jcat"].ToString();
                    jMain.Text = drow["jmain"].ToString();
                    txtsub.Text = drow["sub"].ToString();
                    txtspec.Text = drow["spec"].ToString();
                    txtextc.Text = drow["extc"].ToString();
                    txtpName.Text = drow["name"].ToString();
                    tdate.Text = drow["date1"].ToString();
                    txtLoc.Text = drow["loc"].ToString();
                    txtRecNo.Text = drow["doc"].ToString();
                    txtdType.Text = drow["dtype"].ToString();
                    txtserch.Text = drow["doc"].ToString();
                    txtpName.Text = drow["name"].ToString();


                }
                dt = BLL_CS_REC.getMatRecNo(rec);

                gc_Mat.DataSource = dt;

                
                pnlSearch.Visible = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //txtwCode.Text = "";
            txtJcat.Text = "";
            jMain.Text = "";
            txtsub.Text = "";
            txtspec.Text = "";
            txtextc.Text = "";
            txtpName.Text = "";
            tdate.Text = DateTime.Today.ToString();
            //txtLoc.Text = "";
            txtserch.Text = "";
            txtRecNo.Text = "";
            txtdType.Text = "";
        }

        private void labelControl14_Click(object sender, EventArgs e)
        {
            pnlJob.Visible = false;
        }

        

        private void txtJcat_DoubleClick(object sender, EventArgs e)
        {
            pnlJob.Visible = true;
            gcJob.DataSource = BLL_CS_REC.Search_Job_Details();
        }

        private void gvJob_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            string jcat = "";
            string jmain = "";
            string jdesc = "";

            jcat = gvJob.GetFocusedRowCellValue("jcat").ToString();
            jmain = gvJob.GetFocusedRowCellValue("jmain").ToString();
            jdesc = gvJob.GetFocusedRowCellValue("jdesc").ToString();
            txtJcat.Text = jcat;
            jMain.Text = jmain;
            txtpName.Text = jdesc;
            pnlJob.Visible = false;
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
                gcSub.DataSource = BLL_CS_REC.Search_Sub_Details(jcat, jmain);
            }

        }

        private void labelControl25_Click(object sender, EventArgs e)
        {
            pnlSub.Visible = false;
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

        private void txtserch_TextChanged(object sender, EventArgs e)
        {
           if(txtRecNo.Text != "")
            {
                btn_Save.Enabled = false;
                btnRefresh.Enabled = false;
                txtwCode.ReadOnly = true;
                txtJcat.ReadOnly = true;
                txtsub.ReadOnly = true;
                txtspec.ReadOnly = true;
                txtextc.ReadOnly = true;
                tdate.ReadOnly = true;
                txtRecNo.ReadOnly = true;
                txtdType.ReadOnly = true;
                txtJcat.ReadOnly = true;
                pnlJob.SendToBack();
                pnlSub.SendToBack();
                gv_Mat.OptionsBehavior.Editable = false;
                gv_mrq_details.OptionsBehavior.Editable = false;
                gv_Tools.OptionsBehavior.Editable = false;
            }
            else
            {
                btn_Save.Enabled = true;
                btnRefresh.Enabled = true;
                txtwCode.ReadOnly = true;
                txtJcat.ReadOnly = false;
                txtsub.ReadOnly = true;
                txtspec.ReadOnly = true;
                txtextc.ReadOnly = true;
                tdate.ReadOnly = false;
                txtRecNo.ReadOnly = true;
                txtdType.ReadOnly = true;
                txtJcat.ReadOnly = false;
                pnlJob.BringToFront();
                pnlSub.BringToFront();
                gv_Mat.OptionsBehavior.Editable = true;
                gv_mrq_details.OptionsBehavior.Editable = true;
                gv_Tools.OptionsBehavior.Editable = true;
            }
        }

        private void gv_Mat_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string stts = gv_Mat.GetFocusedRowCellValue("srctabl")?.ToString();

            if(stts == "m")
            {
                try
                {
                    if (e.Column.FieldName == "quntity")
                    {
                        string mrqQty = gv_Mat.GetFocusedRowCellValue("umr").ToString();
                        string qRec = gv_Mat.GetFocusedRowCellValue("quntity").ToString() ?? "0";


                        double mrqValue = Convert.ToDouble(mrqQty);
                        double qRecValue = Convert.ToDouble(qRec);

                        double balQ = mrqValue - qRecValue;

                        string balQV = Convert.ToString(balQ);

                        gv_Mat.SetFocusedRowCellValue("bal", balQ);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in gv_Mat_CellValueChanged: " + ex.Message);

                    gv_Mat.SetFocusedRowCellValue("balQ", 0);
                }
            }
            //try
            //{
            //    if (e.Column.FieldName == "quntity")
            //    {
            //        string mrqQty = gv_Mat.GetFocusedRowCellValue("umr").ToString();
            //        string qRec = gv_Mat.GetFocusedRowCellValue("quntity").ToString() ?? "0";
                    

            //        double mrqValue = Convert.ToDouble(mrqQty);
            //        double qRecValue = Convert.ToDouble(qRec);

            //        double balQ = mrqValue - qRecValue;

            //        string balQV = Convert.ToString(balQ);

            //        gv_Mat.SetFocusedRowCellValue("bal", balQ);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error in gv_Mat_CellValueChanged: " + ex.Message);
                
            //    gv_Mat.SetFocusedRowCellValue("balQ", 0);
            //}



            //string stts = gv_Mat.GetFocusedRowCellValue("srctabl")?.ToString();
            else
            {
                if (e.Column.FieldName == "quntity")
                {
                    string req_qty = gv_Mat.GetFocusedRowCellValue("quntity").ToString() ?? "0";
                    gv_Mat.SetFocusedRowCellValue("umr", req_qty);

                    try
                    {
                        if (e.Column.FieldName == "quntity")
                        {
                            string mrqQty = gv_Mat.GetFocusedRowCellValue("umr").ToString();
                            string qRec = gv_Mat.GetFocusedRowCellValue("quntity").ToString() ?? "0";


                            double mrqValue = Convert.ToDouble(mrqQty);
                            double qRecValue = Convert.ToDouble(qRec);

                            double balQ = mrqValue - qRecValue;

                            string balQV = Convert.ToString(balQ);

                            gv_Mat.SetFocusedRowCellValue("bal", balQ);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in gv_Mat_CellValueChanged: " + ex.Message);

                        gv_Mat.SetFocusedRowCellValue("balQ", 0);
                    }
                }
            }
        }
    
        private void gv_Mat_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if(gv_Mat.CellValueChanged != true)
            try
            {
                // Retrieve values
                string mrqQty = gv_Mat.GetFocusedRowCellValue("umr")?.ToString() ?? "0"; // Default to "0" if null
                string qRec = gv_Mat.GetFocusedRowCellValue("quntity")?.ToString() ?? "0"; // Default to "0" if null

                // Validate and convert to double
                if (double.TryParse(mrqQty, out double mrqValue) && double.TryParse(qRec, out double qRecValue))
                {
                    // Perform calculation
                    double balQ = mrqValue - qRecValue;

                    // Set the calculated value to the "balQ" column
                    gv_Mat.SetFocusedRowCellValue("balQ", balQ);
                }
                else
                {
                    // Handle invalid inputs
                    gv_Mat.SetFocusedRowCellValue("balQ", 0);
                    Console.WriteLine("Invalid numeric input in 'umr' or 'quntity' columns.");
                }
            }
            catch (Exception ex)
            {
                // Log error for debugging
                Console.WriteLine("Error in gv_Mat_CellValueChanged: " + ex.Message);

                // Set balance column to 0 as fallback
                gv_Mat.SetFocusedRowCellValue("balQ", 0);
            }
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
           // Util.Animate(this, Util.Effect.Slide, 1000, 0);
        }

        private void labelControl31_Click(object sender, EventArgs e)
        {
            panelControl5.Hide();
        }

        private void gv_Mat_KeyPress(object sender, KeyPressEventArgs e)
        {
            var gridView = sender as DataGridView;

            // Check if the DataGridView and CurrentCell are not null
            if (gridView?.CurrentCell != null)
            {
                // Check if the current column is "isqty"
                if (gridView.Columns[gridView.CurrentCell.ColumnIndex].Name == "quntity")
                {
                    // Allow only numeric input and control keys
                    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        // Show an error message for invalid input
                        XtraMessageBox.Show("Please enter numbers only.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // Block invalid input
                        e.Handled = true;
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //if (txtJcat.Text != "" && jMain.Text != "")
            //{
            //    string sub = txtsub.Text;
            //    string spec = txtspec.Text;
            //    string jcat = txtJcat.Text;
            //    string jmain = jMain.Text;
            //    string extc = txtextc.Text;
            //    gc_mrq_details.DataSource = BLL_CS_REC.Load_Select_Material_Code(jcat, jmain, sub, spec, extc);//anumi 04/21
            //}
            //else
            //{
            //    gc_mrq_details.DataSource = null;
            //    gc_mrq_details.DataSource = BLL_CS_REC.Select_Material_Code();
            //}
            gc_mrq_details.DataSource = null;
            gc_mrq_details.DataSource = BLL_CS_REC.Select_Material_Code();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string sysID = "";
            string appID = "";
            string date = "";
            string time = "";
            string sdate = "";

            sysID = "FMS";
            appID = "FMSR0169";
            //date = pDate.Text;
            time = DateTime.Now.ToString("HH:mm:ss");
            sdate = DateTime.Now.ToString("yyyy-MM-dd");

            // DataTable dt1 = bll.docDet();
            //ref.pa["recNo"].value = dt1.Rows[0]["colName"].ToString();

            //REC_Report.Parameters["paramDate"].Value = "Terminal Gratuity as at " + " " + deDate.Text;
            REC_Report.Parameters["paramsysID"].Value = sysID.ToString();
            REC_Report.Parameters["paramappID"].Value = appID.ToString();
            REC_Report.Parameters["paramTime"].Value = DateTime.Now.ToString("HH:mm");
            REC_Report.Parameters["paramRdate"].Value = sdate;
            //string  rec = gvSearch.GetRowCellValue(gvSearch.FocusedRowHandle, "recNo").ToString();
            ////REC_Report.DataSource = BLL_CS_REC.getSearchRecNo(rec);
            //REC_Report.DataSource = BLL_CS_REC.getMatRecNo( rec);
            //REC_Report.DataSource = BLL_CS_REC.getSearchRecNo(rec);
            //ReportPrintTool PrintTool = new ReportPrintTool(REC_Report);
            //PrintTool.ShowRibbonPreview(UserLookAndFeel.Default);

        }

        private void tdate_EditValueChanged(object sender, EventArgs e)
        {
            DateTime sdate = Convert.ToDateTime (txtSdate.Text);
            DateTime edate = Convert.ToDateTime (txtEdate.Text);
            DateTime selectDate = Convert.ToDateTime (tdate.Text);

            if (selectDate < sdate || selectDate > edate)
            {
                XtraMessageBox.Show("Please select the date between '"+sdate.ToString("yyyy-MM-dd") + "' and '"+edate.ToString("yyyy-MM-dd") + "'.", " Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void txtJcat_EditValueChanged(object sender, EventArgs e)
        {
            txtsub.Text = "";
            txtspec.Text = "";
            txtextc.Text = "";
        }

        private void gv_Tools_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {//anumi04/30
            if (e.Value != null && e.Column.FieldName == "select")
            {
                gv_Tools.SetRowCellValue(e.RowHandle, "select", e.Value);
            }
        }

        private void gv_Tools_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {//anumi04/30
            if (e.Column.FieldName == "select")
            {
                for (int i = gv_Mat.RowCount; i > -1; i--)
                {
                    string stts = gv_Mat.GetRowCellDisplayText(i, "srctabl")?.ToString();
                    if (stts == "t")
                    {
                        gv_Mat.DeleteRow(i);
                    }

                    //string stts = gv_mrq_details.GetRowCellValue(i, "srctabl").ToString();
                    //if (stts != "t")
                    //{
                    //    gv_Mat.DeleteRow(i);
                    //}
                    ////gv_Mat.DeleteRow(i);
                }

                bool hasSelectedRow = false; // Flag to check if any row is selected//anumi24/02

                if (e.Value != null && e.Column.FieldName == "select")
                {
                    int rowNo = 0;
                    foreach (DataRow row in ((DataTable)gc_Tools.DataSource).Rows)
                    {
                        if (row["select"].ToString() == "True")
                        {
                            hasSelectedRow = true;//anumi24/02
                            string matcode = row["mcode"].ToString();
                            DataTable dt1 = BLL_CS_REC.select_TOOL(matcode);//anumi05/02 //BLL_CS_REC.loadTOOL();

                            foreach (DataRow row1 in dt1.Rows)
                            {

                                gv_Mat.AddNewRow();
                                gv_Mat.SetFocusedRowCellValue("matCode", row["mcode"].ToString());
                                gv_Mat.SetFocusedRowCellValue("desc", row1["desc"].ToString());
                                gv_Mat.SetFocusedRowCellValue("bal", 0.00);
                                gv_Mat.SetFocusedRowCellValue("unit", row1["unit"].ToString());
                                gv_Mat.SetFocusedRowCellValue("quntity", 0.00);
                                gv_Mat.SetFocusedRowCellValue("avg", row["avg"].ToString());
                                gv_Mat.SetFocusedRowCellValue("umr", 0.00);
                                gv_Mat.SetFocusedRowCellValue("srctabl", "t");
                            }
                            gv_Mat.Focus();
                            gv_Mat.FocusedRowHandle = gv_Mat.RowCount - 1;
                        }
                        rowNo++;
                    }
                }

            }
        }
    }
}