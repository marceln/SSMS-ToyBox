namespace ToyBox.UI.ObjectLookup
{
    partial class ObjectLookupControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectLookupControl));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ctlTxtLookup = new System.Windows.Forms.TextBox();
            this.ctlComboServer = new System.Windows.Forms.ComboBox();
            this.ctlComboDatabase = new System.Windows.Forms.ComboBox();
            this.ctrlLoadingCircle = new MRG.Controls.UI.LoadingCircle();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 24);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Image = global::ToyBox.UI.Properties.Resources.Database32;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 24);
            this.label2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.Image = global::ToyBox.UI.Properties.Resources.Lookup32;
            this.label3.Location = new System.Drawing.Point(3, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 24);
            this.label3.TabIndex = 2;
            // 
            // ctlTxtLookup
            // 
            this.ctlTxtLookup.Location = new System.Drawing.Point(33, 59);
            this.ctlTxtLookup.Name = "ctlTxtLookup";
            this.ctlTxtLookup.Size = new System.Drawing.Size(342, 20);
            this.ctlTxtLookup.TabIndex = 3;
            // 
            // ctlComboServer
            // 
            this.ctlComboServer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ctlComboServer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ctlComboServer.FormattingEnabled = true;
            this.ctlComboServer.Location = new System.Drawing.Point(33, 5);
            this.ctlComboServer.Name = "ctlComboServer";
            this.ctlComboServer.Size = new System.Drawing.Size(342, 21);
            this.ctlComboServer.TabIndex = 1;
            // 
            // ctlComboDatabase
            // 
            this.ctlComboDatabase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ctlComboDatabase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ctlComboDatabase.FormattingEnabled = true;
            this.ctlComboDatabase.Location = new System.Drawing.Point(33, 32);
            this.ctlComboDatabase.Name = "ctlComboDatabase";
            this.ctlComboDatabase.Size = new System.Drawing.Size(342, 21);
            this.ctlComboDatabase.TabIndex = 2;
            // 
            // ctrlLoadingCircle
            // 
            this.ctrlLoadingCircle.Active = false;
            this.ctrlLoadingCircle.Color = System.Drawing.Color.MediumSeaGreen;
            this.ctrlLoadingCircle.InnerCircleRadius = 6;
            this.ctrlLoadingCircle.Location = new System.Drawing.Point(3, 57);
            this.ctrlLoadingCircle.Name = "ctrlLoadingCircle";
            this.ctrlLoadingCircle.NumberSpoke = 9;
            this.ctrlLoadingCircle.OuterCircleRadius = 7;
            this.ctrlLoadingCircle.RotationSpeed = 100;
            this.ctrlLoadingCircle.Size = new System.Drawing.Size(24, 24);
            this.ctrlLoadingCircle.SpokeThickness = 4;
            this.ctrlLoadingCircle.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.Firefox;
            this.ctrlLoadingCircle.TabIndex = 5;
            this.ctrlLoadingCircle.TabStop = false;
            this.ctrlLoadingCircle.Visible = false;
            // 
            // ObjectLookupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.ctrlLoadingCircle);
            this.Controls.Add(this.ctlComboDatabase);
            this.Controls.Add(this.ctlComboServer);
            this.Controls.Add(this.ctlTxtLookup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "ObjectLookupControl";
            this.Size = new System.Drawing.Size(380, 83);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ctlTxtLookup;
        private System.Windows.Forms.ComboBox ctlComboServer;
        private System.Windows.Forms.ComboBox ctlComboDatabase;
        private MRG.Controls.UI.LoadingCircle ctrlLoadingCircle;
    }
}
