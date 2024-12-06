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
            SelectModelOkButton = new System.Windows.Forms.Button();
            SelectModelCancelButton = new System.Windows.Forms.Button();
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
            // SelectModelOkButton
            // 
            SelectModelOkButton.Location = new System.Drawing.Point(414, 64);
            SelectModelOkButton.Name = "SelectModelOkButton";
            SelectModelOkButton.Size = new System.Drawing.Size(102, 45);
            SelectModelOkButton.TabIndex = 1;
            SelectModelOkButton.Text = "OK";
            SelectModelOkButton.UseVisualStyleBackColor = true;
            SelectModelOkButton.Click += SelectModelOkButton_Click;
            // 
            // SelectModelCancelButton
            // 
            SelectModelCancelButton.Location = new System.Drawing.Point(414, 144);
            SelectModelCancelButton.Name = "SelectModelCancelButton";
            SelectModelCancelButton.Size = new System.Drawing.Size(99, 42);
            SelectModelCancelButton.TabIndex = 2;
            SelectModelCancelButton.Text = "Cancel";
            SelectModelCancelButton.UseVisualStyleBackColor = true;
            SelectModelCancelButton.Click += SelectModelCancelButton_Click;
            // 
            // SelectModelForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(549, 265);
            Controls.Add(SelectModelCancelButton);
            Controls.Add(SelectModelOkButton);
            Controls.Add(DatabaseListBox);
            Name = "SelectModelForm";
            Text = "Select Model";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox DatabaseListBox;
        private System.Windows.Forms.Button SelectModelOkButton;
        private System.Windows.Forms.Button SelectModelCancelButton;
    }
}