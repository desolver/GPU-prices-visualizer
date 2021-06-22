
namespace GPU_Prices_Parser
{
    partial class GpuForm
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
            this.storeList = new System.Windows.Forms.ListBox();
            this.plotView = new OxyPlot.WindowsForms.PlotView();
            this.sendRequestBtn = new System.Windows.Forms.Button();
            this.gpuGroupBox = new System.Windows.Forms.GroupBox();
            this.storesGroupBox = new System.Windows.Forms.GroupBox();
            this.pricesTable = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.gpuGroupBox.SuspendLayout();
            this.storesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pricesTable)).BeginInit();
            this.SuspendLayout();
            // 
            // storeList
            // 
            this.storeList.ItemHeight = 17;
            this.storeList.Location = new System.Drawing.Point(7, 23);
            this.storeList.Name = "storeList";
            this.storeList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.storeList.Size = new System.Drawing.Size(194, 89);
            this.storeList.TabIndex = 0;
            // 
            // plotView
            // 
            this.plotView.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.plotView.Location = new System.Drawing.Point(12, 301);
            this.plotView.Name = "plotView";
            this.plotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView.Size = new System.Drawing.Size(776, 406);
            this.plotView.TabIndex = 0;
            this.plotView.Text = "plotView";
            this.plotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // sendRequestBtn
            // 
            this.sendRequestBtn.Location = new System.Drawing.Point(5, 70);
            this.sendRequestBtn.Name = "sendRequestBtn";
            this.sendRequestBtn.Size = new System.Drawing.Size(195, 32);
            this.sendRequestBtn.TabIndex = 3;
            this.sendRequestBtn.Text = "Find information";
            this.sendRequestBtn.UseVisualStyleBackColor = true;
            this.sendRequestBtn.Click += new System.EventHandler(this.RequestButtonClick);
            // 
            // gpuGroupBox
            // 
            this.gpuGroupBox.Controls.Add(this.comboBox1);
            this.gpuGroupBox.Controls.Add(this.sendRequestBtn);
            this.gpuGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gpuGroupBox.Location = new System.Drawing.Point(806, 12);
            this.gpuGroupBox.Name = "gpuGroupBox";
            this.gpuGroupBox.Size = new System.Drawing.Size(207, 116);
            this.gpuGroupBox.TabIndex = 5;
            this.gpuGroupBox.TabStop = false;
            this.gpuGroupBox.Text = "GPU Model";
            // 
            // storesGroupBox
            // 
            this.storesGroupBox.Controls.Add(this.storeList);
            this.storesGroupBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.storesGroupBox.Location = new System.Drawing.Point(806, 301);
            this.storesGroupBox.Name = "storesGroupBox";
            this.storesGroupBox.Size = new System.Drawing.Size(207, 128);
            this.storesGroupBox.TabIndex = 6;
            this.storesGroupBox.TabStop = false;
            this.storesGroupBox.Text = "Stores to search";
            // 
            // pricesTable
            // 
            this.pricesTable.AllowUserToAddRows = false;
            this.pricesTable.AllowUserToDeleteRows = false;
            this.pricesTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.pricesTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.pricesTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pricesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pricesTable.Location = new System.Drawing.Point(12, 13);
            this.pricesTable.Name = "pricesTable";
            this.pricesTable.RowHeadersWidth = 140;
            this.pricesTable.RowTemplate.Height = 25;
            this.pricesTable.Size = new System.Drawing.Size(776, 271);
            this.pricesTable.TabIndex = 8;
            this.pricesTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PricesTable_CellClick);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(5, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(194, 25);
            this.comboBox1.TabIndex = 4;
            // 
            // GpuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 719);
            this.Controls.Add(this.pricesTable);
            this.Controls.Add(this.storesGroupBox);
            this.Controls.Add(this.gpuGroupBox);
            this.Controls.Add(this.plotView);
            this.Name = "GpuForm";
            this.Text = "Form1";
            this.gpuGroupBox.ResumeLayout(false);
            this.storesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pricesTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plotView;
        private System.Windows.Forms.Button sendRequestBtn;
        private System.Windows.Forms.GroupBox gpuGroupBox;
        private System.Windows.Forms.GroupBox storesGroupBox;
        private System.Windows.Forms.ListBox storeList;
        private System.Windows.Forms.DataGridView pricesTable;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

