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
            ConnectDatasetSplit = new System.Windows.Forms.SplitContainer();
            ConnectDatasetRefreshButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            SelectorTableLayout = new System.Windows.Forms.TableLayoutPanel();
            ServiceModelComboBox = new System.Windows.Forms.ComboBox();
            DesktopModelLabel = new System.Windows.Forms.Label();
            ServiceModelLabel = new System.Windows.Forms.Label();
            DesktopModelComboBox = new System.Windows.Forms.ComboBox();
            ConnectDatasetCancelButton = new System.Windows.Forms.Button();
            ConnectDatasetOkButton = new System.Windows.Forms.Button();
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
            ConnectDatasetSplit.Panel2.Controls.Add(ConnectDatasetCancelButton);
            ConnectDatasetSplit.Panel2.Controls.Add(ConnectDatasetOkButton);
            ConnectDatasetSplit.Size = new System.Drawing.Size(780, 301);
            ConnectDatasetSplit.SplitterDistance = 222;
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
            label1.Location = new System.Drawing.Point(14, 21);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(112, 17);
            label1.TabIndex = 1;
            label1.Text = "Connect to Model";
            // 
            // SelectorTableLayout
            // 
            SelectorTableLayout.ColumnCount = 2;
            SelectorTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.64678F));
            SelectorTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.35322F));
            SelectorTableLayout.Controls.Add(ServiceModelComboBox, 1, 1);
            SelectorTableLayout.Controls.Add(DesktopModelLabel, 0, 0);
            SelectorTableLayout.Controls.Add(ServiceModelLabel, 0, 1);
            SelectorTableLayout.Controls.Add(DesktopModelComboBox, 1, 0);
            SelectorTableLayout.Location = new System.Drawing.Point(14, 67);
            SelectorTableLayout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            SelectorTableLayout.Name = "SelectorTableLayout";
            SelectorTableLayout.RowCount = 2;
            SelectorTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            SelectorTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            SelectorTableLayout.Size = new System.Drawing.Size(743, 138);
            SelectorTableLayout.TabIndex = 0;
            // 
            // ServiceModelComboBox
            // 
            ServiceModelComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            ServiceModelComboBox.FormattingEnabled = true;
            ServiceModelComboBox.Location = new System.Drawing.Point(187, 92);
            ServiceModelComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ServiceModelComboBox.Name = "ServiceModelComboBox";
            ServiceModelComboBox.Size = new System.Drawing.Size(552, 23);
            ServiceModelComboBox.TabIndex = 3;
            // 
            // DesktopModelLabel
            // 
            DesktopModelLabel.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            DesktopModelLabel.AutoSize = true;
            DesktopModelLabel.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            DesktopModelLabel.Location = new System.Drawing.Point(4, 27);
            DesktopModelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            DesktopModelLabel.Name = "DesktopModelLabel";
            DesktopModelLabel.Size = new System.Drawing.Size(175, 15);
            DesktopModelLabel.TabIndex = 0;
            DesktopModelLabel.Text = "Connect to Desktop Model:";
            DesktopModelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ServiceModelLabel
            // 
            ServiceModelLabel.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ServiceModelLabel.AutoSize = true;
            ServiceModelLabel.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ServiceModelLabel.Location = new System.Drawing.Point(4, 96);
            ServiceModelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ServiceModelLabel.Name = "ServiceModelLabel";
            ServiceModelLabel.Size = new System.Drawing.Size(175, 15);
            ServiceModelLabel.TabIndex = 1;
            ServiceModelLabel.Text = "Connect to Service Model:";
            ServiceModelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DesktopModelComboBox
            // 
            DesktopModelComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            DesktopModelComboBox.FormattingEnabled = true;
            DesktopModelComboBox.Location = new System.Drawing.Point(187, 23);
            DesktopModelComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            DesktopModelComboBox.Name = "DesktopModelComboBox";
            DesktopModelComboBox.Size = new System.Drawing.Size(552, 23);
            DesktopModelComboBox.TabIndex = 2;
            // 
            // ConnectDatasetCancelButton
            // 
            ConnectDatasetCancelButton.AllowDrop = true;
            ConnectDatasetCancelButton.BackColor = System.Drawing.Color.Transparent;
            ConnectDatasetCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            ConnectDatasetCancelButton.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ConnectDatasetCancelButton.ForeColor = System.Drawing.SystemColors.WindowText;
            ConnectDatasetCancelButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            ConnectDatasetCancelButton.Location = new System.Drawing.Point(631, 27);
            ConnectDatasetCancelButton.Margin = new System.Windows.Forms.Padding(0);
            ConnectDatasetCancelButton.Name = "ConnectDatasetCancelButton";
            ConnectDatasetCancelButton.Size = new System.Drawing.Size(93, 27);
            ConnectDatasetCancelButton.TabIndex = 1;
            ConnectDatasetCancelButton.Text = "Cancel";
            ConnectDatasetCancelButton.UseVisualStyleBackColor = false;
            ConnectDatasetCancelButton.Click += ConnectDatasetCancelButton_Click;
            // 
            // ConnectDatasetOkButton
            // 
            ConnectDatasetOkButton.BackColor = System.Drawing.Color.DodgerBlue;
            ConnectDatasetOkButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            ConnectDatasetOkButton.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ConnectDatasetOkButton.ForeColor = System.Drawing.SystemColors.WindowText;
            ConnectDatasetOkButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            ConnectDatasetOkButton.Location = new System.Drawing.Point(513, 27);
            ConnectDatasetOkButton.Margin = new System.Windows.Forms.Padding(0);
            ConnectDatasetOkButton.Name = "ConnectDatasetOkButton";
            ConnectDatasetOkButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            ConnectDatasetOkButton.Size = new System.Drawing.Size(88, 27);
            ConnectDatasetOkButton.TabIndex = 0;
            ConnectDatasetOkButton.Text = "Connect";
            ConnectDatasetOkButton.UseVisualStyleBackColor = false;
            ConnectDatasetOkButton.Click += ConnectDatasetOkButton_Click;
            // 
            // ConnectDataset
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(780, 301);
            Controls.Add(ConnectDatasetSplit);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "ConnectDataset";
            Text = "Establish Connection to Model";
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
        private System.Windows.Forms.Label ServiceModelLabel;
        private System.Windows.Forms.ComboBox DesktopModelComboBox;
        private System.Windows.Forms.ComboBox ServiceModelComboBox;
        private System.Windows.Forms.Button ConnectDatasetCancelButton;
        private System.Windows.Forms.Button ConnectDatasetOkButton;
        private System.Windows.Forms.Button ConnectDatasetRefreshButton;
    }
}