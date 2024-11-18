namespace Utils_for_PBI.Forms
{
    partial class ConnectDataset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectDataset));
            this.ConnectDatasetSplit = new System.Windows.Forms.SplitContainer();
            this.SelectorTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.DesktopModelLabel = new System.Windows.Forms.Label();
            this.ServiceModelLabel = new System.Windows.Forms.Label();
            this.DesktopModelComboBox = new System.Windows.Forms.ComboBox();
            this.ServiceModelComboBox = new System.Windows.Forms.ComboBox();
            this.ConnectDatasetOkButton = new System.Windows.Forms.Button();
            this.ConnectDatasetCancelButton = new System.Windows.Forms.Button();
            this.ConnectDatasetRefreshButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ConnectDatasetSplit)).BeginInit();
            this.ConnectDatasetSplit.Panel1.SuspendLayout();
            this.ConnectDatasetSplit.Panel2.SuspendLayout();
            this.ConnectDatasetSplit.SuspendLayout();
            this.SelectorTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectDatasetSplit
            // 
            this.ConnectDatasetSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectDatasetSplit.Location = new System.Drawing.Point(0, 0);
            this.ConnectDatasetSplit.Name = "ConnectDatasetSplit";
            this.ConnectDatasetSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ConnectDatasetSplit.Panel1
            // 
            this.ConnectDatasetSplit.Panel1.Controls.Add(this.ConnectDatasetRefreshButton);
            this.ConnectDatasetSplit.Panel1.Controls.Add(this.label1);
            this.ConnectDatasetSplit.Panel1.Controls.Add(this.SelectorTableLayout);
            // 
            // ConnectDatasetSplit.Panel2
            // 
            this.ConnectDatasetSplit.Panel2.Controls.Add(this.ConnectDatasetCancelButton);
            this.ConnectDatasetSplit.Panel2.Controls.Add(this.ConnectDatasetOkButton);
            this.ConnectDatasetSplit.Size = new System.Drawing.Size(669, 261);
            this.ConnectDatasetSplit.SplitterDistance = 193;
            this.ConnectDatasetSplit.TabIndex = 0;
            // 
            // SelectorTableLayout
            // 
            this.SelectorTableLayout.ColumnCount = 2;
            this.SelectorTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.64678F));
            this.SelectorTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.35322F));
            this.SelectorTableLayout.Controls.Add(this.ServiceModelComboBox, 1, 1);
            this.SelectorTableLayout.Controls.Add(this.DesktopModelLabel, 0, 0);
            this.SelectorTableLayout.Controls.Add(this.ServiceModelLabel, 0, 1);
            this.SelectorTableLayout.Controls.Add(this.DesktopModelComboBox, 1, 0);
            this.SelectorTableLayout.Location = new System.Drawing.Point(12, 58);
            this.SelectorTableLayout.Name = "SelectorTableLayout";
            this.SelectorTableLayout.RowCount = 2;
            this.SelectorTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SelectorTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SelectorTableLayout.Size = new System.Drawing.Size(637, 120);
            this.SelectorTableLayout.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connect to Model";
            // 
            // DesktopModelLabel
            // 
            this.DesktopModelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DesktopModelLabel.AutoSize = true;
            this.DesktopModelLabel.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DesktopModelLabel.Location = new System.Drawing.Point(3, 22);
            this.DesktopModelLabel.Name = "DesktopModelLabel";
            this.DesktopModelLabel.Size = new System.Drawing.Size(150, 15);
            this.DesktopModelLabel.TabIndex = 0;
            this.DesktopModelLabel.Text = "Connect to Desktop Model:";
            this.DesktopModelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ServiceModelLabel
            // 
            this.ServiceModelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ServiceModelLabel.AutoSize = true;
            this.ServiceModelLabel.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServiceModelLabel.Location = new System.Drawing.Point(3, 82);
            this.ServiceModelLabel.Name = "ServiceModelLabel";
            this.ServiceModelLabel.Size = new System.Drawing.Size(150, 15);
            this.ServiceModelLabel.TabIndex = 1;
            this.ServiceModelLabel.Text = "Connect to Service Model:";
            this.ServiceModelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DesktopModelComboBox
            // 
            this.DesktopModelComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DesktopModelComboBox.FormattingEnabled = true;
            this.DesktopModelComboBox.Location = new System.Drawing.Point(159, 19);
            this.DesktopModelComboBox.Name = "DesktopModelComboBox";
            this.DesktopModelComboBox.Size = new System.Drawing.Size(475, 21);
            this.DesktopModelComboBox.TabIndex = 2;
            // 
            // ServiceModelComboBox
            // 
            this.ServiceModelComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ServiceModelComboBox.FormattingEnabled = true;
            this.ServiceModelComboBox.Location = new System.Drawing.Point(159, 79);
            this.ServiceModelComboBox.Name = "ServiceModelComboBox";
            this.ServiceModelComboBox.Size = new System.Drawing.Size(475, 21);
            this.ServiceModelComboBox.TabIndex = 3;
            // 
            // ConnectDatasetOkButton
            // 
            this.ConnectDatasetOkButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.ConnectDatasetOkButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConnectDatasetOkButton.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectDatasetOkButton.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ConnectDatasetOkButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ConnectDatasetOkButton.Location = new System.Drawing.Point(440, 23);
            this.ConnectDatasetOkButton.Margin = new System.Windows.Forms.Padding(0);
            this.ConnectDatasetOkButton.Name = "ConnectDatasetOkButton";
            this.ConnectDatasetOkButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ConnectDatasetOkButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectDatasetOkButton.TabIndex = 0;
            this.ConnectDatasetOkButton.Text = "Connect";
            this.ConnectDatasetOkButton.UseVisualStyleBackColor = false;
            this.ConnectDatasetOkButton.Click += new System.EventHandler(this.ConnectDatasetOkButton_Click);
            // 
            // ConnectDatasetCancelButton
            // 
            this.ConnectDatasetCancelButton.AllowDrop = true;
            this.ConnectDatasetCancelButton.BackColor = System.Drawing.Color.Transparent;
            this.ConnectDatasetCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConnectDatasetCancelButton.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectDatasetCancelButton.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ConnectDatasetCancelButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ConnectDatasetCancelButton.Location = new System.Drawing.Point(541, 23);
            this.ConnectDatasetCancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.ConnectDatasetCancelButton.Name = "ConnectDatasetCancelButton";
            this.ConnectDatasetCancelButton.Size = new System.Drawing.Size(80, 23);
            this.ConnectDatasetCancelButton.TabIndex = 1;
            this.ConnectDatasetCancelButton.Text = "Cancel";
            this.ConnectDatasetCancelButton.UseVisualStyleBackColor = false;
            this.ConnectDatasetCancelButton.Click += new System.EventHandler(this.ConnectDatasetCancelButton_Click);
            // 
            // ConnectDatasetRefreshButton
            // 
            this.ConnectDatasetRefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectDatasetRefreshButton.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectDatasetRefreshButton.Image = ((System.Drawing.Image)(resources.GetObject("ConnectDatasetRefreshButton.Image")));
            this.ConnectDatasetRefreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ConnectDatasetRefreshButton.Location = new System.Drawing.Point(480, 6);
            this.ConnectDatasetRefreshButton.Name = "ConnectDatasetRefreshButton";
            this.ConnectDatasetRefreshButton.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.ConnectDatasetRefreshButton.Size = new System.Drawing.Size(169, 40);
            this.ConnectDatasetRefreshButton.TabIndex = 2;
            this.ConnectDatasetRefreshButton.Text = "   Refresh Model List";
            this.ConnectDatasetRefreshButton.UseVisualStyleBackColor = true;
            // 
            // ConnectDataset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 261);
            this.Controls.Add(this.ConnectDatasetSplit);
            this.MaximizeBox = false;
            this.Name = "ConnectDataset";
            this.Text = "Establish Connection to Model";
            this.ConnectDatasetSplit.Panel1.ResumeLayout(false);
            this.ConnectDatasetSplit.Panel1.PerformLayout();
            this.ConnectDatasetSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ConnectDatasetSplit)).EndInit();
            this.ConnectDatasetSplit.ResumeLayout(false);
            this.SelectorTableLayout.ResumeLayout(false);
            this.SelectorTableLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer ConnectDatasetSplit;
        private System.Windows.Forms.TableLayoutPanel SelectorTableLayout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DesktopModelLabel;
        private System.Windows.Forms.Label ServiceModelLabel;
        private System.Windows.Forms.ComboBox DesktopModelComboBox;
        private System.Windows.Forms.ComboBox ServiceModelComboBox;
        private System.Windows.Forms.Button ConnectDatasetCancelButton;
        private System.Windows.Forms.Button ConnectDatasetOkButton;
        private System.Windows.Forms.Button ConnectDatasetRefreshButton;
    }
}