using Service_Center_Equipment_Management.PAL.USER_CONTROLS;
using Service_Center_Equipment_Management.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace Service_Center_Equipment_Management
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private UC_REC UC_REC;
        private UC_IssueNote UC_IssueNote;
        private UC_TRN UC_TRN;
        private UC_BinCard UC_BinCard;

        public Form1()
        {
            InitializeComponent();

        }
        public void loadNavBarsElements()
        {
            item_rec.Visible = false;
            item_in.Visible = false;
            item_rn.Visible = false;
            item_bc.Visible = false;
        }

        public void loadNavBar()
        {

        }
        private void item_rec_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            if (UC_REC != null)
            {
                Controls.Remove(UC_REC);
                UC_REC.Dispose(); // Optional: Dispose if you no longer need it
            }

            UC_REC = new UC_REC();
            Controls.Add(UC_REC);

            // Bring the user control to the front
            UC_REC.BringToFront();

            // Set the user control's location to the right side of the form
            int newX = this.Width - UC_REC.Width - 50; // Leave a small margin on the right
            //int newY = (this.Height - UC_REC.Height) / 8; // Center it vertically
            UC_REC.Location = new Point(newX/*, newY*/);
            splashScreenManager1.CloseWaitForm();
        }

        private void item_in_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            UC_IssueNote = new UC_IssueNote();
            Controls.Add(UC_IssueNote);

            // Bring the user control to the front
            UC_IssueNote.BringToFront();

            // Set the user control's location to the right side of the form
            int newX = this.Width - UC_IssueNote.Width - 50; // Leave a small margin on the right
            //int newY = (this.Height - UC_IssueNote.Height) / 8; // Center it vertically
            UC_IssueNote.Location = new Point(newX/*, newY*/);
            splashScreenManager1.CloseWaitForm();
        }

        private void item_rn_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            UC_TRN = new UC_TRN();
            Controls.Add(UC_TRN);

            // Bring the user control to the front
            UC_TRN.BringToFront();

            // Set the user control's location to the right side of the form
            int newX = this.Width - UC_TRN.Width - 50; // Leave a small margin on the right
            //int newY = (this.Height - UC_TRN.Height) / 8; // Center it vertically
            UC_TRN.Location = new Point(newX/*, newY*/);
            splashScreenManager1.CloseWaitForm();
        }

        private void item_bc_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            UC_BinCard = new UC_BinCard();
            Controls.Add(UC_BinCard);

            // Bring the user control to the front
            UC_BinCard.BringToFront();

            // Set the user control's location to the right side of the form
            int newX = this.Width - UC_BinCard.Width - 50; // Leave a small margin on the right
            //int newY = (this.Height - UC_BinCard.Height) / 8; // Center it vertically
            UC_BinCard.Location = new Point(newX/*, newY*/);
            splashScreenManager1.CloseWaitForm();
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }
        //// BLL.BLL_CS_Bin BLL_CS_Bin = new BLL.BLL_CS_Bin();

        // private UC_REC UC_REC = new UC_REC();
        // private UC_IssueNote UC_IssueNote = new UC_IssueNote();
        // private UC_TRN UC_TRN = new UC_TRN();
        // private UC_BinCard UC_BinCard = new UC_BinCard();

        // public Form1()
        // {
        //     InitializeComponent();

        //     UC_REC = new UC_REC();
        //     UC_REC.Dock = DockStyle.Fill;
        //     panelControl5.Controls.Add(UC_REC);
        //     UC_REC.Hide();

        // }
        ///newwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww
        //private int a = 0;
        //private Timer FR_Timer;
        //private Timer animationTimer;
        //private bool isOpen = true;
        //private bool isNavOpen = true;
        //Control selectedControl = null;
        //PAL.USER_CONTROLS.UC_REC UC_REC = new PAL.USER_CONTROLS.UC_REC();
        //PAL.USER_CONTROLS.UC_BinCard UC_BinCard = new PAL.USER_CONTROLS.UC_BinCard();
        //PAL.USER_CONTROLS.UC_IssueNote UC_IssueNote = new PAL.USER_CONTROLS.UC_IssueNote();
        ////PAL.USER_CONTROLS.UC_ReturnNote UC_ReturnNote = new PAL.USER_CONTROLS.UC_ReturnNote();
        //PAL.USER_CONTROLS.UC_TRN UC_TRN = new UC_TRN();
        ////private int slideStep = 70;
        //private int slideStep = 3;
        //// private int slideStep = 2;
        //private bool isAnimating = false;


        //Bitmap arrowRight;
        //Bitmap arrowLeft;
        //public Form1()
        //{
        //    InitializeComponent();
        //    FR_Timer = new Timer();
        //    FR_Timer.Interval = (50);
        //    //FR_Timer.Interval = (10); dn kreeee
        //    FR_Timer.Tick += new EventHandler(EvtNavController);
        //    ResourceManager rm = Resources.ResourceManager;
        //    arrowRight = (Bitmap)rm.GetObject("right");
        //    arrowLeft = (Bitmap)rm.GetObject("left");


        //}
        //private void EvtNavController(object sender, EventArgs e)
        //{
        //    if (!isNavOpen)
        //    {
        //        a = a - 10;
        //        tileControl1.Location = new Point(a, 0);
        //        //btnNavControl.Location = new Point(220 + a, 518);
        //        if (a == -150)
        //        {
        //            //btnNavControl.ImageOptions.Image = arrowRight;
        //            FR_Timer.Stop();
        //            tileControl1.Location = new Point(0, 0);
        //            //btnNavControl.Location = new Point(1, 518);

        //        }
        //    }
        //    else
        //    {
        //        a = a + 10;
        //        tileControl1.Location = new Point(a, 0);
        //        // btnNavControl.Location = new Point(a + 220, 518);
        //        if (a >= 4)
        //        {
        //            // btnNavControl.ImageOptions.Image = arrowLeft;
        //            FR_Timer.Stop();
        //            tileControl1.Location = new Point(1, 0);
        //            //btnNavControl.Location = new Point(220, 518);

        //        }
        //    }

        //}
        //private void UC_Sliding_Evn(Control control)
        //{
        //    //removeNavController();
        //    this.Controls.Add(control);
        //    selectedControl = control;
        //    int slideStep = 15;
        //    int animationInterval = 10;
        //    if (!isOpen)
        //    {
        //        if (!isAnimating)
        //        {
        //            isAnimating = true;
        //            animationTimer = new Timer();
        //            animationTimer.Interval = 20;
        //            animationTimer.Tick += (s, args) =>
        //            {
        //                if (control.Left > -control.Width)
        //                {
        //                    control.Left -= slideStep;
        //                }
        //                else
        //                {
        //                    isAnimating = false;
        //                    animationTimer.Stop();
        //                }
        //            };
        //            animationTimer.Start();
        //        }
        //    }
        //    else
        //    {
        //        if (!isAnimating)
        //        {
        //            isAnimating = true;
        //            animationTimer = new Timer();
        //            animationTimer.Interval = 20;
        //            animationTimer.Tick += (s, args) =>
        //            {
        //                if (control.Left < (control.Parent.Width - control.Width + 280) / 2)
        //                {
        //                    control.Left += slideStep;
        //                    //control.Left = control.Left;
        //                }
        //                else
        //                {
        //                    isAnimating = false;
        //                    animationTimer.Stop();
        //                }
        //            };
        //            animationTimer.Start();

        //        }

        //    }
        //    isOpen = !isOpen;
        //    //control.BringToFront();

        //}

        //private void removeNavController()
        //{
        //    FR_Timer.Start();
        //    this.Controls.Add(tileControl1);
        //    isNavOpen = !isNavOpen;
        //    ////  removeScrn();
        //    //FR_Timer.Start();
        //    //this.Controls.Add(tileControl1);
        //    //isNavOpen = !isNavOpen;

        //}

        //private void item_rec_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        //{
        //    //UC_IssueNote.Visible = false;
        //    //UC_TRN.Visible = false;
        //    //UC_BinCard.Visible = false;
        //    UC_Sliding_Evn(UC_REC);
        //    //MessageBox.Show("tttt");
        //   // UC_REC.BringToFront();

        //}

        //private void item_bc_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        //{
        //    //UC_IssueNote.Visible = false;
        //    //UC_TRN.Visible = false;
        //    //UC_REC.Visible = false;
        //    UC_Sliding_Evn(UC_BinCard);
        //    //MessageBox.Show("juyutg");
        //     //UC_BinCard.BringToFront();
        //}

        //private void item_in_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        //{
        //    //UC_REC.Visible = false;
        //    //UC_TRN.Visible = false;
        //    //UC_BinCard.Visible = false;
        //    UC_Sliding_Evn(UC_IssueNote);
        //    //UC_IssueNote.BringToFront();
        //}


        //private void item_rn_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        //{
        //    //UC_IssueNote.Visible = false;
        //    //UC_REC.Visible = false;
        //    //UC_BinCard.Visible = false;
        //    //UC_Sliding_Evn(UC_ReturnNote);
        //    UC_Sliding_Evn(UC_TRN);
        //   // UC_TRN.BringToFront();
        //}

        //private void btnNavControl_Click(object sender, EventArgs e)
        //{
        //    if (selectedControl != null)
        //    {
        //        UC_Sliding_Evn(selectedControl);
        //        selectedControl = null;
        //    }
        //    else
        //    {
        //        removeNavController();
        //    }
        //}
    }
}
