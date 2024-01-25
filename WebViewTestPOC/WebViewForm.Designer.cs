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
            this.TxtUrl = new System.Windows.Forms.TextBox();
            this.BtnMove = new System.Windows.Forms.Button();
            this.WebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.WebView)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtUrl
            // 
            this.TxtUrl.Location = new System.Drawing.Point(12, 12);
            this.TxtUrl.Name = "TxtUrl";
            this.TxtUrl.Size = new System.Drawing.Size(695, 19);
            this.TxtUrl.TabIndex = 0;
            this.TxtUrl.Text = "https://qiita.com/signup";
            // 
            // BtnMove
            // 
            this.BtnMove.Location = new System.Drawing.Point(713, 10);
            this.BtnMove.Name = "BtnMove";
            this.BtnMove.Size = new System.Drawing.Size(75, 23);
            this.BtnMove.TabIndex = 2;
            this.BtnMove.Text = "移動";
            this.BtnMove.UseVisualStyleBackColor = true;
            // 
            // WebView
            // 
            this.WebView.AllowExternalDrop = true;
            this.WebView.BackColor = System.Drawing.SystemColors.ControlLight;
            this.WebView.CreationProperties = null;
            this.WebView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.WebView.Location = new System.Drawing.Point(12, 37);
            this.WebView.Name = "WebView";
            this.WebView.Size = new System.Drawing.Size(776, 401);
            this.WebView.TabIndex = 3;
            this.WebView.ZoomFactor = 1D;
            this.WebView.NavigationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs>(this.WebView_NavigationCompleted);
            // 
            // WebViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.WebView);
            this.Controls.Add(this.BtnMove);
            this.Controls.Add(this.TxtUrl);
            this.Name = "WebViewForm";
            this.Text = "WebViewForm";
            this.Resize += new System.EventHandler(this.WebViewForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.WebView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtUrl;
        private System.Windows.Forms.Button BtnMove;
        private Microsoft.Web.WebView2.WinForms.WebView2 WebView;
    }
}

