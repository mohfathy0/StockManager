namespace StockManager.Forms
{
    partial class frm_User
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.lkp_AccessProfile = new DevExpress.XtraEditors.LookUpEdit();
            this.lkp_SettingProfile = new DevExpress.XtraEditors.LookUpEdit();
            this.lkp_UserType = new DevExpress.XtraEditors.LookUpEdit();
            this.txt_Username = new DevExpress.XtraEditors.TextEdit();
            this.txt_Password = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItemBottom = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemTop = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemLeft = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemRight = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkp_AccessProfile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkp_SettingProfile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkp_UserType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Username.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemRight)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.toggleSwitch1);
            this.layoutControl1.Controls.Add(this.lkp_AccessProfile);
            this.layoutControl1.Controls.Add(this.lkp_SettingProfile);
            this.layoutControl1.Controls.Add(this.lkp_UserType);
            this.layoutControl1.Controls.Add(this.txt_Username);
            this.layoutControl1.Controls.Add(this.txt_Password);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 39);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(551, 246);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Location = new System.Drawing.Point(398, 36);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.OffText = "Off";
            this.toggleSwitch1.Properties.OnText = "On";
            this.toggleSwitch1.Size = new System.Drawing.Size(90, 24);
            this.toggleSwitch1.StyleController = this.layoutControl1;
            this.toggleSwitch1.TabIndex = 13;
            // 
            // lkp_AccessProfile
            // 
            this.lkp_AccessProfile.Location = new System.Drawing.Point(217, 166);
            this.lkp_AccessProfile.Name = "lkp_AccessProfile";
            this.lkp_AccessProfile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkp_AccessProfile.Properties.NullText = "";
            this.lkp_AccessProfile.Size = new System.Drawing.Size(271, 28);
            this.lkp_AccessProfile.StyleController = this.layoutControl1;
            this.lkp_AccessProfile.TabIndex = 11;
            // 
            // lkp_SettingProfile
            // 
            this.lkp_SettingProfile.Location = new System.Drawing.Point(217, 134);
            this.lkp_SettingProfile.Name = "lkp_SettingProfile";
            this.lkp_SettingProfile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkp_SettingProfile.Properties.NullText = "";
            this.lkp_SettingProfile.Size = new System.Drawing.Size(271, 28);
            this.lkp_SettingProfile.StyleController = this.layoutControl1;
            this.lkp_SettingProfile.TabIndex = 9;
            // 
            // lkp_UserType
            // 
            this.lkp_UserType.Location = new System.Drawing.Point(217, 102);
            this.lkp_UserType.Name = "lkp_UserType";
            this.lkp_UserType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkp_UserType.Properties.NullText = "";
            this.lkp_UserType.Size = new System.Drawing.Size(271, 28);
            this.lkp_UserType.StyleController = this.layoutControl1;
            this.lkp_UserType.TabIndex = 8;
            // 
            // txt_Username
            // 
            this.txt_Username.Location = new System.Drawing.Point(217, 36);
            this.txt_Username.Name = "txt_Username";
            this.txt_Username.Size = new System.Drawing.Size(175, 28);
            this.txt_Username.StyleController = this.layoutControl1;
            this.txt_Username.TabIndex = 6;
            // 
            // txt_Password
            // 
            this.txt_Password.Location = new System.Drawing.Point(217, 68);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Properties.UseSystemPasswordChar = true;
            this.txt_Password.Size = new System.Drawing.Size(271, 28);
            this.txt_Password.StyleController = this.layoutControl1;
            this.txt_Password.TabIndex = 7;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItemBottom,
            this.layoutControlGroup1,
            this.emptySpaceItemTop,
            this.emptySpaceItemLeft,
            this.emptySpaceItemRight});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(551, 246);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItemBottom
            // 
            this.emptySpaceItemBottom.AllowHotTrack = false;
            this.emptySpaceItemBottom.Location = new System.Drawing.Point(0, 198);
            this.emptySpaceItemBottom.Name = "emptySpaceItemBottom";
            this.emptySpaceItemBottom.Size = new System.Drawing.Size(529, 26);
            this.emptySpaceItemBottom.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(73, 10);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(421, 188);
            this.layoutControlGroup1.Text = "layoutgroup";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txt_Username;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(297, 32);
            this.layoutControlItem3.Text = "User name";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(102, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txt_Password;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(393, 34);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(393, 34);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(393, 34);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Password";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(102, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lkp_UserType;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 66);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(393, 32);
            this.layoutControlItem5.Text = "User type";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(102, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lkp_SettingProfile;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 98);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(393, 32);
            this.layoutControlItem6.Text = "Setting profile";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(102, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.lkp_AccessProfile;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 130);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(393, 32);
            this.layoutControlItem8.Text = "Screen access profile";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(102, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.toggleSwitch1;
            this.layoutControlItem2.Location = new System.Drawing.Point(297, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(96, 32);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItemTop
            // 
            this.emptySpaceItemTop.AllowHotTrack = false;
            this.emptySpaceItemTop.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItemTop.Name = "emptySpaceItemTop";
            this.emptySpaceItemTop.Size = new System.Drawing.Size(529, 10);
            this.emptySpaceItemTop.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemLeft
            // 
            this.emptySpaceItemLeft.AllowHotTrack = false;
            this.emptySpaceItemLeft.Location = new System.Drawing.Point(0, 10);
            this.emptySpaceItemLeft.Name = "emptySpaceItemLeft";
            this.emptySpaceItemLeft.Size = new System.Drawing.Size(73, 188);
            this.emptySpaceItemLeft.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemRight
            // 
            this.emptySpaceItemRight.AllowHotTrack = false;
            this.emptySpaceItemRight.Location = new System.Drawing.Point(494, 10);
            this.emptySpaceItemRight.Name = "emptySpaceItemRight";
            this.emptySpaceItemRight.Size = new System.Drawing.Size(35, 188);
            this.emptySpaceItemRight.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frm_User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 285);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frm_User";
            this.Text = "New user";
            this.Load += new System.EventHandler(this.Frm_User_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkp_AccessProfile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkp_SettingProfile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkp_UserType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Username.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemRight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LookUpEdit lkp_SettingProfile;
        private DevExpress.XtraEditors.LookUpEdit lkp_UserType;
        private DevExpress.XtraEditors.TextEdit txt_Username;
        private DevExpress.XtraEditors.TextEdit txt_Password;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemBottom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.LookUpEdit lkp_AccessProfile;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemTop;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemLeft;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemRight;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}