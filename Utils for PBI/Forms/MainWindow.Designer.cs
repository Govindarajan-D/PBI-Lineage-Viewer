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
            ActivityArea = new System.Windows.Forms.SplitContainer();
            view_dependencies = new System.Windows.Forms.Button();
            DisplayLineageWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            StatusBar = new System.Windows.Forms.StatusStrip();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ActivityArea).BeginInit();
            ActivityArea.Panel1.SuspendLayout();
            ActivityArea.Panel2.SuspendLayout();
            ActivityArea.SuspendLayout();
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
            connectToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            connectToolStripMenuItem.Text = "Connect Desktop Model";
            connectToolStripMenuItem.Click += connectDesktopModelToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
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
            // ActivityArea
            // 
            ActivityArea.Dock = System.Windows.Forms.DockStyle.Fill;
            ActivityArea.Location = new System.Drawing.Point(0, 24);
            ActivityArea.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ActivityArea.Name = "ActivityArea";
            // 
            // ActivityArea.Panel1
            // 
            ActivityArea.Panel1.Controls.Add(view_dependencies);
            // 
            // ActivityArea.Panel2
            // 
            ActivityArea.Panel2.Controls.Add(DisplayLineageWebView);
            ActivityArea.Size = new System.Drawing.Size(933, 495);
            ActivityArea.SplitterDistance = 232;
            ActivityArea.SplitterWidth = 2;
            ActivityArea.TabIndex = 1;
            // 
            // view_dependencies
            // 
            view_dependencies.Location = new System.Drawing.Point(3, 3);
            view_dependencies.Name = "view_dependencies";
            view_dependencies.Size = new System.Drawing.Size(226, 65);
            view_dependencies.TabIndex = 0;
            view_dependencies.Text = "View Dependencies";
            view_dependencies.UseVisualStyleBackColor = true;
            view_dependencies.Click += viewdependencies_Click;
            // 
            // DisplayLineageWebView
            // 
            DisplayLineageWebView.AllowExternalDrop = true;
            DisplayLineageWebView.CreationProperties = null;
            DisplayLineageWebView.DefaultBackgroundColor = System.Drawing.Color.White;
            DisplayLineageWebView.Dock = System.Windows.Forms.DockStyle.Fill;
            DisplayLineageWebView.Location = new System.Drawing.Point(0, 0);
            DisplayLineageWebView.Name = "DisplayLineageWebView";
            DisplayLineageWebView.Size = new System.Drawing.Size(699, 495);
            DisplayLineageWebView.Source = new System.Uri(" https://www.microsoft.com", System.UriKind.Absolute);
            DisplayLineageWebView.TabIndex = 0;
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
            // MainWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(StatusBar);
            Controls.Add(ActivityArea);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MainWindow";
            Text = "Utils for PBI";
            Load += MainWindow_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ActivityArea.Panel1.ResumeLayout(false);
            ActivityArea.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ActivityArea).EndInit();
            ActivityArea.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer ActivityArea;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.Button view_dependencies;
        private Microsoft.Web.WebView2.WinForms.WebView2 DisplayLineageWebView;
    }
}