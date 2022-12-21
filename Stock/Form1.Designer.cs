namespace Stock
{
    partial class Stock
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TotDataGrid = new System.Windows.Forms.DataGridView();
            this.ReLoadBtn = new System.Windows.Forms.Button();
            this.OutputBtn = new System.Windows.Forms.Button();
            this.SearchText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.TotDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TotDataGrid
            // 
            this.TotDataGrid.AllowUserToAddRows = false;
            this.TotDataGrid.AllowUserToDeleteRows = false;
            this.TotDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TotDataGrid.Location = new System.Drawing.Point(18, 43);
            this.TotDataGrid.Name = "TotDataGrid";
            this.TotDataGrid.ReadOnly = true;
            this.TotDataGrid.RowHeadersWidth = 20;
            this.TotDataGrid.RowTemplate.Height = 25;
            this.TotDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TotDataGrid.Size = new System.Drawing.Size(1210, 280);
            this.TotDataGrid.TabIndex = 0;
            this.TotDataGrid.TabStop = false;
            this.TotDataGrid.Visible = false;
            // 
            // ReLoadBtn
            // 
            this.ReLoadBtn.Location = new System.Drawing.Point(211, 10);
            this.ReLoadBtn.Name = "ReLoadBtn";
            this.ReLoadBtn.Size = new System.Drawing.Size(75, 23);
            this.ReLoadBtn.TabIndex = 1;
            this.ReLoadBtn.Text = "刷新";
            this.ReLoadBtn.UseVisualStyleBackColor = true;
            this.ReLoadBtn.Click += new System.EventHandler(this.ReLoad_Click);
            // 
            // OutputBtn
            // 
            this.OutputBtn.Location = new System.Drawing.Point(310, 10);
            this.OutputBtn.Name = "OutputBtn";
            this.OutputBtn.Size = new System.Drawing.Size(75, 23);
            this.OutputBtn.TabIndex = 2;
            this.OutputBtn.Text = "匯出";
            this.OutputBtn.UseVisualStyleBackColor = true;
            this.OutputBtn.Click += new System.EventHandler(this.InputBtn_Click);
            // 
            // SearchText
            // 
            this.SearchText.Location = new System.Drawing.Point(18, 11);
            this.SearchText.Name = "SearchText";
            this.SearchText.Size = new System.Drawing.Size(144, 23);
            this.SearchText.TabIndex = 3;
            this.SearchText.TextChanged += new System.EventHandler(this.SearchText_TextChanged);
            // 
            // Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 450);
            this.Controls.Add(this.SearchText);
            this.Controls.Add(this.OutputBtn);
            this.Controls.Add(this.ReLoadBtn);
            this.Controls.Add(this.TotDataGrid);
            this.Name = "Stock";
            this.Text = "Stock";
            ((System.ComponentModel.ISupportInitialize)(this.TotDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView TotDataGrid;
        private Button ReLoadBtn;
        private Button OutputBtn;
        private TextBox SearchText;
    }
}