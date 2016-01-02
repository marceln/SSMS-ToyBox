namespace ToyBox.UI.ObjectLookup
{
    partial class ObjectLookupForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ctrlObjectLookupContainer = new ToyBox.UI.ObjectLookup.ObjectLookupControl();
            this.ctrlLookupResultsListBox = new ToyBox.UI.CustomControls.ObjectLookupResultsListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ctrlObjectLookupContainer, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ctrlLookupResultsListBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(639, 326);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // ctrlObjectLookupContainer
            // 
            this.ctrlObjectLookupContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(242)))));
            this.ctrlObjectLookupContainer.Location = new System.Drawing.Point(0, 0);
            this.ctrlObjectLookupContainer.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlObjectLookupContainer.Name = "ctrlObjectLookupContainer";
            this.ctrlObjectLookupContainer.Padding = new System.Windows.Forms.Padding(2);
            this.ctrlObjectLookupContainer.Size = new System.Drawing.Size(380, 83);
            this.ctrlObjectLookupContainer.TabIndex = 0;
            // 
            // ctrlLookupResultsListBox
            // 
            this.ctrlLookupResultsListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctrlLookupResultsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlLookupResultsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ctrlLookupResultsListBox.FormattingEnabled = true;
            this.ctrlLookupResultsListBox.ItemHeight = 20;
            this.ctrlLookupResultsListBox.Location = new System.Drawing.Point(0, 88);
            this.ctrlLookupResultsListBox.Margin = new System.Windows.Forms.Padding(0);
            this.ctrlLookupResultsListBox.Name = "ctrlLookupResultsListBox";
            this.ctrlLookupResultsListBox.Size = new System.Drawing.Size(639, 238);
            this.ctrlLookupResultsListBox.TabIndex = 1;
            // 
            // ObjectLookupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(639, 326);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "ObjectLookupForm";
            this.Text = "ObjectLookupForm";
            this.Load += new System.EventHandler(this.ObjectLookupForm_Load);
            this.Shown += new System.EventHandler(this.ObjectLookupForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ObjectLookupControl ctrlObjectLookupContainer;
        private CustomControls.ObjectLookupResultsListBox ctrlLookupResultsListBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}