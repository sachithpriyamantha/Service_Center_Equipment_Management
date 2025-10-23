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
using System.Diagnostics;

namespace Service_Center_Equipment_Management.PAL.USER_CONTROLS
{
    public partial class UC_BinCard : DevExpress.XtraEditors.XtraUserControl
    {
        private BLL.BLL_CS_Bin BLL_CS_Bin = new BLL.BLL_CS_Bin();
        public UC_BinCard()
        {
            InitializeComponent();
        }

        private void UC_BinCard_Load(object sender, EventArgs e)
        {
            gcMat.DataSource = BLL_CS_Bin.Select_Material();
            string MatCode = gvMat.GetFocusedRowCellDisplayText("mCode").ToString();
            gcTrans.DataSource = BLL_CS_Bin.Select_Transaction(MatCode);
            //gcTrans.DataSource = BLL_CS_Bin.Select_Transaction();
        }

        private void btnPrintxl_Click(object sender, EventArgs e)
        {
            if (gvMat.RowCount == 0)
            {
                XtraMessageBox.Show("Material Details Grid is Empty.", "Service Center Equipment Management.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    gcMat.ExportToXlsx("C:\\EXE\\Service Center Equipment Management.xlsx");
                    Process.Start("C:\\EXE\\Service Center Equipment Management.xlsx");

                }
                catch (Exception)
                {
                    XtraMessageBox.Show("Excel Sheet is already open, Close it and export a new Excel Sheet", "Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (gvTrans.RowCount == 0)
            {
                XtraMessageBox.Show("Transaction Details Grid is Empty.", "Service Center Equipment Management.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    gcTrans.ExportToXlsx("C:\\EXE\\Service Center Equipment Management.xlsx");
                    Process.Start("C:\\EXE\\Service Center Equipment Management.xlsx");

                }
                catch (Exception)
                {
                    XtraMessageBox.Show("Excel Sheet is already open, Close it and export a new Excel Sheet", "Service Center Equipment Management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void gvMat_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string MatCode = gvMat.GetFocusedRowCellDisplayText("mCode").ToString();
            gcTrans.DataSource = BLL_CS_Bin.Select_Transaction(MatCode);
        }

        private void gvTrans_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // caltype
            string selectedValue = gvTrans.GetRowCellValue(e.RowHandle, "calType").ToString(); 

            if (selectedValue.Equals("Stores +"))
            {
                if (e.Column.FieldName == "calType")
                {
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.BackColor = Color.ForestGreen;
                }
            }
            else if (selectedValue.Equals("Stores -"))
            {
                if (e.Column.FieldName == "calType")
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            else
            {
                e.Appearance.BackColor = Color.Cornsilk;
                e.Appearance.ForeColor = Color.Black;
            }

            //dtype
            string selectedtype = gvTrans.GetRowCellValue(e.RowHandle, "dType").ToString();

            if (selectedtype.Equals("REC"))
            {
                if (e.Column.FieldName == "dType")
                {
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.BackColor = Color.PeachPuff;
                }
            }
            else if (selectedtype.Equals("TIN"))
            {
                if (e.Column.FieldName == "dType")
                {
                    e.Appearance.BackColor = Color.LightYellow;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            else if (selectedtype.Equals("TRN"))
            {
                if(e.Column.FieldName == "dType")
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            else
            {
                e.Appearance.BackColor = Color.Cornsilk;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            gcMat.DataSource = null;
            gcMat.DataSource = BLL_CS_Bin.Select_Material();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gcTrans.DataSource = null;
            gcTrans.DataSource = BLL_CS_Bin.Select_All_Transaction( );
        }
    }
}
