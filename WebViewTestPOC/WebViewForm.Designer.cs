namespace WebViewTestPOC
{
    partial class WebViewForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.WebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenCsv = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.WebView)).BeginInit();
            this.SuspendLayout();
            // 
            // WebView
            // 
            this.WebView.AllowExternalDrop = true;
            this.WebView.BackColor = System.Drawing.SystemColors.ControlLight;
            this.WebView.CreationProperties = null;
            this.WebView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.WebView.Location = new System.Drawing.Point(12, 41);
            this.WebView.Name = "WebView";
            this.WebView.Size = new System.Drawing.Size(776, 397);
            this.WebView.TabIndex = 3;
            this.WebView.ZoomFactor = 1D;
            this.WebView.NavigationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs>(this.WebView_NavigationCompleted);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Title = "開くファイルを選択してください";
            // 
            // btnOpenCsv
            // 
            this.btnOpenCsv.Enabled = false;
            this.btnOpenCsv.Location = new System.Drawing.Point(12, 12);
            this.btnOpenCsv.Name = "btnOpenCsv";
            this.btnOpenCsv.Size = new System.Drawing.Size(113, 23);
            this.btnOpenCsv.TabIndex = 4;
            this.btnOpenCsv.Text = "CSVデータを開く";
            this.btnOpenCsv.UseVisualStyleBackColor = true;
            this.btnOpenCsv.Click += new System.EventHandler(this.BtnOpenCsv_Click);
            // 
            // WebViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOpenCsv);
            this.Controls.Add(this.WebView);
            this.Name = "WebViewForm";
            this.Text = "WebViewForm";
            ((System.ComponentModel.ISupportInitialize)(this.WebView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.Web.WebView2.WinForms.WebView2 WebView;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnOpenCsv;
    }
}

