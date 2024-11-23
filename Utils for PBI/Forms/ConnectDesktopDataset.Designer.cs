namespace Utils_for_PBI.Forms
{
    partial class ConnectDesktopDataset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectDesktopDataset));
            ConnectDatasetSplit = new System.Windows.Forms.SplitContainer();
            ConnectDatasetRefreshButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            SelectorTableLayout = new System.Windows.Forms.TableLayoutPanel();
            DesktopModelLabel = new System.Windows.Forms.Label();
            DesktopModelComboBox = new System.Windows.Forms.ComboBox();
            ConnectDesktopDatasetCancelButton = new System.Windows.Forms.Button();
            ConnectDesktopDatasetOkButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)ConnectDatasetSplit).BeginInit();
            ConnectDatasetSplit.Panel1.SuspendLayout();
            ConnectDatasetSplit.Panel2.SuspendLayout();
            ConnectDatasetSplit.SuspendLayout();
            SelectorTableLayout.SuspendLayout();
            SuspendLayout();
            // 
            // ConnectDatasetSplit
            // 
            ConnectDatasetSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            ConnectDatasetSplit.Location = new System.Drawing.Point(0, 0);
            ConnectDatasetSplit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ConnectDatasetSplit.Name = "ConnectDatasetSplit";
            ConnectDatasetSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ConnectDatasetSplit.Panel1
            // 
            ConnectDatasetSplit.Panel1.Controls.Add(ConnectDatasetRefreshButton);
            ConnectDatasetSplit.Panel1.Controls.Add(label1);
            ConnectDatasetSplit.Panel1.Controls.Add(SelectorTableLayout);
            // 
            // ConnectDatasetSplit.Panel2
            // 
            ConnectDatasetSplit.Panel2.Controls.Add(ConnectDesktopDatasetCancelButton);
            ConnectDatasetSplit.Panel2.Controls.Add(ConnectDesktopDatasetOkButton);
            ConnectDatasetSplit.Size = new System.Drawing.Size(798, 238);
            ConnectDatasetSplit.SplitterDistance = 175;
            ConnectDatasetSplit.SplitterWidth = 5;
            ConnectDatasetSplit.TabIndex = 0;
            // 
            // ConnectDatasetRefreshButton
            // 
            ConnectDatasetRefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ConnectDatasetRefreshButton.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ConnectDatasetRefreshButton.Image = (System.Drawing.Image)resources.GetObject("ConnectDatasetRefreshButton.Image");
            ConnectDatasetRefreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            ConnectDatasetRefreshButton.Location = new System.Drawing.Point(560, 7);
            ConnectDatasetRefreshButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ConnectDatasetRefreshButton.Name = "ConnectDatasetRefreshButton";
            ConnectDatasetRefreshButton.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            ConnectDatasetRefreshButton.Size = new System.Drawing.Size(197, 46);
            ConnectDatasetRefreshButton.TabIndex = 2;
            ConnectDatasetRefreshButton.Text = "   Refresh Model List";
            ConnectDatasetRefreshButton.UseVisualStyleBackColor = true;
            ConnectDatasetRefreshButton.Click += ConnectDatasetRefreshButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(42, 22);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(164, 17);
            label1.TabIndex = 1;
            label1.Text = "Connect to Desktop Model";
            // 
            // SelectorTableLayout
            // 
            SelectorTableLayout.ColumnCount = 2;
            SelectorTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.64678F));
            SelectorTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.35322F));
            SelectorTableLayout.Controls.Add(DesktopModelLabel, 0, 0);
            SelectorTableLayout.Controls.Add(DesktopModelComboBox, 1, 0);
            SelectorTableLayout.Location = new System.Drawing.Point(14, 67);
            SelectorTableLayout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            SelectorTableLayout.Name = "SelectorTableLayout";
            SelectorTableLayout.RowCount = 1;
            SelectorTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            SelectorTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            SelectorTableLayout.Size = new System.Drawing.Size(743, 105);
            SelectorTableLayout.TabIndex = 0;
            // 
            // DesktopModelLabel
            // 
            DesktopModelLabel.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            DesktopModelLabel.AutoSize = true;
            DesktopModelLabel.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            DesktopModelLabel.Location = new System.Drawing.Point(4, 45);
            DesktopModelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            DesktopModelLabel.Name = "DesktopModelLabel";
            DesktopModelLabel.Size = new System.Drawing.Size(175, 15);
            DesktopModelLabel.TabIndex = 0;
            DesktopModelLabel.Text = "Connect to Desktop Model:";
            DesktopModelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DesktopModelComboBox
            // 
            DesktopModelComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            DesktopModelComboBox.FormattingEnabled = true;
            DesktopModelComboBox.Location = new System.Drawing.Point(187, 41);
            DesktopModelComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            DesktopModelComboBox.Name = "DesktopModelComboBox";
            DesktopModelComboBox.Size = new System.Drawing.Size(552, 23);
            DesktopModelComboBox.TabIndex = 2;
            // 
            // ConnectDesktopDatasetCancelButton
            // 
            ConnectDesktopDatasetCancelButton.AllowDrop = true;
            ConnectDesktopDatasetCancelButton.BackColor = System.Drawing.Color.Transparent;
            ConnectDesktopDatasetCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            ConnectDesktopDatasetCancelButton.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ConnectDesktopDatasetCancelButton.ForeColor = System.Drawing.SystemColors.WindowText;
            ConnectDesktopDatasetCancelButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            ConnectDesktopDatasetCancelButton.Location = new System.Drawing.Point(631, 27);
            ConnectDesktopDatasetCancelButton.Margin = new System.Windows.Forms.Padding(0);
            ConnectDesktopDatasetCancelButton.Name = "ConnectDesktopDatasetCancelButton";
            ConnectDesktopDatasetCancelButton.Size = new System.Drawing.Size(93, 27);
            ConnectDesktopDatasetCancelButton.TabIndex = 1;
            ConnectDesktopDatasetCancelButton.Text = "Cancel";
            ConnectDesktopDatasetCancelButton.UseVisualStyleBackColor = false;
            ConnectDesktopDatasetCancelButton.Click += ConnectDatasetCancelButton_Click;
            // 
            // ConnectDesktopDatasetOkButton
            // 
            ConnectDesktopDatasetOkButton.BackColor = System.Drawing.Color.LightGray;
            ConnectDesktopDatasetOkButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            ConnectDesktopDatasetOkButton.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ConnectDesktopDatasetOkButton.ForeColor = System.Drawing.SystemColors.WindowText;
            ConnectDesktopDatasetOkButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            ConnectDesktopDatasetOkButton.Location = new System.Drawing.Point(513, 27);
            ConnectDesktopDatasetOkButton.Margin = new System.Windows.Forms.Padding(0);
            ConnectDesktopDatasetOkButton.Name = "ConnectDesktopDatasetOkButton";
            ConnectDesktopDatasetOkButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            ConnectDesktopDatasetOkButton.Size = new System.Drawing.Size(88, 27);
            ConnectDesktopDatasetOkButton.TabIndex = 0;
            ConnectDesktopDatasetOkButton.Text = "Connect";
            ConnectDesktopDatasetOkButton.UseVisualStyleBackColor = false;
            ConnectDesktopDatasetOkButton.Click += ConnectDatasetOkButton_Click;
            // 
            // ConnectDesktopDataset
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(798, 238);
            Controls.Add(ConnectDatasetSplit);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "ConnectDesktopDataset";
            Text = "Establish Connection to Desktop Model";
            ConnectDatasetSplit.Panel1.ResumeLayout(false);
            ConnectDatasetSplit.Panel1.PerformLayout();
            ConnectDatasetSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ConnectDatasetSplit).EndInit();
            ConnectDatasetSplit.ResumeLayout(false);
            SelectorTableLayout.ResumeLayout(false);
            SelectorTableLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer ConnectDatasetSplit;
        private System.Windows.Forms.TableLayoutPanel SelectorTableLayout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DesktopModelLabel;
        private System.Windows.Forms.ComboBox DesktopModelComboBox;
        private System.Windows.Forms.Button ConnectDesktopDatasetCancelButton;
        private System.Windows.Forms.Button ConnectDesktopDatasetOkButton;
        private System.Windows.Forms.Button ConnectDatasetRefreshButton;
    }
}