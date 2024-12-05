namespace Utils_for_PBI.Forms
{
    partial class SelectModelForm
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
            DatabaseListBox = new System.Windows.Forms.ListBox();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // DatabaseListBox
            // 
            DatabaseListBox.FormattingEnabled = true;
            DatabaseListBox.ItemHeight = 15;
            DatabaseListBox.Location = new System.Drawing.Point(18, 19);
            DatabaseListBox.Name = "DatabaseListBox";
            DatabaseListBox.Size = new System.Drawing.Size(350, 214);
            DatabaseListBox.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(414, 64);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(102, 45);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(414, 144);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(99, 42);
            button2.TabIndex = 2;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // SelectModelForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(549, 265);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(DatabaseListBox);
            Name = "SelectModelForm";
            Text = "Select Model";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox DatabaseListBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}