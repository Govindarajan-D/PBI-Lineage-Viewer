namespace Utils_for_PBI.Forms
{
    partial class MainWindow
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
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            DisplayLineageWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            StatusBar = new System.Windows.Forms.StatusStrip();
            ConnectDatasetPlaceholderLabel = new System.Windows.Forms.Label();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DisplayLineageWebView).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(933, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { connectToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // connectToolStripMenuItem
            // 
            connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            connectToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            connectToolStripMenuItem.Text = "Connect Model";
            connectToolStripMenuItem.Click += connectDesktopModelToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            aboutToolStripMenuItem.Text = "About";
            // 
            // DisplayLineageWebView
            // 
            DisplayLineageWebView.AllowExternalDrop = true;
            DisplayLineageWebView.CreationProperties = null;
            DisplayLineageWebView.DefaultBackgroundColor = System.Drawing.Color.White;
            DisplayLineageWebView.Dock = System.Windows.Forms.DockStyle.Fill;
            DisplayLineageWebView.Location = new System.Drawing.Point(0, 24);
            DisplayLineageWebView.Name = "DisplayLineageWebView";
            DisplayLineageWebView.Size = new System.Drawing.Size(933, 473);
            DisplayLineageWebView.Source = new System.Uri(" https://www.microsoft.com", System.UriKind.Absolute);
            DisplayLineageWebView.TabIndex = 0;
            DisplayLineageWebView.Visible = false;
            DisplayLineageWebView.ZoomFactor = 1D;
            // 
            // StatusBar
            // 
            StatusBar.Location = new System.Drawing.Point(0, 497);
            StatusBar.Name = "StatusBar";
            StatusBar.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            StatusBar.Size = new System.Drawing.Size(933, 22);
            StatusBar.TabIndex = 2;
            StatusBar.Text = "StatusBar";
            // 
            // ConnectDatasetPlaceholderLabel
            // 
            ConnectDatasetPlaceholderLabel.AutoSize = true;
            ConnectDatasetPlaceholderLabel.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ConnectDatasetPlaceholderLabel.Location = new System.Drawing.Point(285, 243);
            ConnectDatasetPlaceholderLabel.Name = "ConnectDatasetPlaceholderLabel";
            ConnectDatasetPlaceholderLabel.Size = new System.Drawing.Size(394, 33);
            ConnectDatasetPlaceholderLabel.TabIndex = 3;
            ConnectDatasetPlaceholderLabel.Text = "Connect to a Dataset to get started";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(ConnectDatasetPlaceholderLabel);
            Controls.Add(DisplayLineageWebView);
            Controls.Add(StatusBar);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MainWindow";
            Text = "Utils for PBI";
            Load += MainWindow_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DisplayLineageWebView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip StatusBar;
        private Microsoft.Web.WebView2.WinForms.WebView2 DisplayLineageWebView;
        private System.Windows.Forms.Label ConnectDatasetPlaceholderLabel;
    }
}