namespace Service_Center_Equipment_Management
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.TileItemElement tileItemElement5 = new DevExpress.XtraEditors.TileItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            DevExpress.XtraEditors.TileItemElement tileItemElement6 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement7 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement8 = new DevExpress.XtraEditors.TileItemElement();
            this.tileControl1 = new DevExpress.XtraEditors.TileControl();
            this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
            this.item_rec = new DevExpress.XtraEditors.TileItem();
            this.item_in = new DevExpress.XtraEditors.TileItem();
            this.item_rn = new DevExpress.XtraEditors.TileItem();
            this.item_bc = new DevExpress.XtraEditors.TileItem();
            this.btnNavControl = new DevExpress.XtraEditors.LabelControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Service_Center_Equipment_Management.PAL.FORMS.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // tileControl1
            // 
            this.tileControl1.Groups.Add(this.tileGroup2);
            this.tileControl1.Location = new System.Drawing.Point(10, 10);
            this.tileControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tileControl1.MaxId = 11;
            this.tileControl1.Name = "tileControl1";
            this.tileControl1.Padding = new System.Windows.Forms.Padding(0, 15, 15, 15);
            this.tileControl1.Size = new System.Drawing.Size(234, 630);
            this.tileControl1.TabIndex = 0;
            this.tileControl1.Text = "tileControl1";
            // 
            // tileGroup2
            // 
            this.tileGroup2.Items.Add(this.item_rec);
            this.tileGroup2.Items.Add(this.item_in);
            this.tileGroup2.Items.Add(this.item_rn);
            this.tileGroup2.Items.Add(this.item_bc);
            this.tileGroup2.Name = "tileGroup2";
            // 
            // item_rec
            // 
            this.item_rec.AllowAnimation = true;
            this.item_rec.AppearanceItem.Normal.BackColor = System.Drawing.Color.SteelBlue;
            this.item_rec.AppearanceItem.Normal.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.item_rec.AppearanceItem.Normal.ForeColor = System.Drawing.Color.White;
            this.item_rec.AppearanceItem.Normal.Options.UseBackColor = true;
            this.item_rec.AppearanceItem.Normal.Options.UseFont = true;
            this.item_rec.AppearanceItem.Normal.Options.UseForeColor = true;
            this.item_rec.AppearanceItem.Normal.Options.UseTextOptions = true;
            this.item_rec.AppearanceItem.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.item_rec.AppearanceItem.Normal.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.item_rec.AppearanceItem.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.item_rec.BorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Always;
            tileItemElement5.Appearance.Normal.BackColor = System.Drawing.Color.SteelBlue;
            tileItemElement5.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileItemElement5.Appearance.Normal.Options.UseBackColor = true;
            tileItemElement5.Appearance.Normal.Options.UseFont = true;
            tileItemElement5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            tileItemElement5.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileItemElement5.ImageOptions.ImageSize = new System.Drawing.Size(2, 2);
            tileItemElement5.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement5.Text = "RECEIVE NOTE (REC)";
            tileItemElement5.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.item_rec.Elements.Add(tileItemElement5);
            this.item_rec.Id = 16;
            this.item_rec.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.item_rec.Name = "item_rec";
            this.item_rec.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.item_rec_ItemClick);
            // 
            // item_in
            // 
            this.item_in.AppearanceItem.Normal.BackColor = System.Drawing.Color.SteelBlue;
            this.item_in.AppearanceItem.Normal.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.item_in.AppearanceItem.Normal.Options.UseBackColor = true;
            this.item_in.AppearanceItem.Normal.Options.UseFont = true;
            tileItemElement6.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileItemElement6.Appearance.Normal.Options.UseFont = true;
            tileItemElement6.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            tileItemElement6.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileItemElement6.ImageOptions.ImageSize = new System.Drawing.Size(2, 2);
            tileItemElement6.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement6.Text = "ISSUE NOTE (TIN)";
            tileItemElement6.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.item_in.Elements.Add(tileItemElement6);
            this.item_in.Id = 7;
            this.item_in.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.item_in.Name = "item_in";
            this.item_in.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.item_in_ItemClick);
            // 
            // item_rn
            // 
            this.item_rn.AppearanceItem.Normal.BackColor = System.Drawing.Color.SteelBlue;
            this.item_rn.AppearanceItem.Normal.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.item_rn.AppearanceItem.Normal.Options.UseBackColor = true;
            this.item_rn.AppearanceItem.Normal.Options.UseFont = true;
            tileItemElement7.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileItemElement7.Appearance.Normal.Options.UseFont = true;
            tileItemElement7.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            tileItemElement7.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileItemElement7.ImageOptions.ImageSize = new System.Drawing.Size(2, 2);
            tileItemElement7.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement7.Text = "RETURN NOTE (TRN)";
            tileItemElement7.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.item_rn.Elements.Add(tileItemElement7);
            this.item_rn.Id = 8;
            this.item_rn.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.item_rn.Name = "item_rn";
            this.item_rn.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.item_rn_ItemClick);
            // 
            // item_bc
            // 
            this.item_bc.AllowAnimation = true;
            this.item_bc.AppearanceItem.Normal.BackColor = System.Drawing.Color.SteelBlue;
            this.item_bc.AppearanceItem.Normal.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.item_bc.AppearanceItem.Normal.Options.UseBackColor = true;
            this.item_bc.AppearanceItem.Normal.Options.UseFont = true;
            this.item_bc.BorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Always;
            tileItemElement8.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileItemElement8.Appearance.Normal.Options.UseFont = true;
            tileItemElement8.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image3")));
            tileItemElement8.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileItemElement8.ImageOptions.ImageIndex = 0;
            tileItemElement8.ImageOptions.ImageSize = new System.Drawing.Size(2, 2);
            tileItemElement8.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement8.Text = "BIN CARD";
            tileItemElement8.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.item_bc.Elements.Add(tileItemElement8);
            this.item_bc.Id = 9;
            this.item_bc.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.item_bc.Name = "item_bc";
            this.item_bc.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.item_bc_ItemClick);
            // 
            // btnNavControl
            // 
            this.btnNavControl.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNavControl.ImageOptions.Image")));
            this.btnNavControl.Location = new System.Drawing.Point(229, 25);
            this.btnNavControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNavControl.Name = "btnNavControl";
            this.btnNavControl.Size = new System.Drawing.Size(32, 32);
            this.btnNavControl.TabIndex = 1;
            this.btnNavControl.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(266, 10);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1353, 772);
            this.pnlMain.TabIndex = 2;
            this.pnlMain.Visible = false;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1610, 771);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnNavControl);
            this.Controls.Add(this.tileControl1);
            this.IconOptions.ShowIcon = false;
            this.LookAndFeel.SkinName = "Metropolis";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service Center Equipment Management";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TileControl tileControl1;
        private DevExpress.XtraEditors.TileGroup tileGroup2;
        private DevExpress.XtraEditors.TileItem item_rec;
        private DevExpress.XtraEditors.TileItem item_in;
        private DevExpress.XtraEditors.TileItem item_rn;
        private DevExpress.XtraEditors.TileItem item_bc;
        private DevExpress.XtraEditors.LabelControl btnNavControl;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}

