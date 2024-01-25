using Microsoft.Web.WebView2.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebViewTestPOC
{
    public partial class WebViewForm : Form
    {
        private bool navigationCompleted = false;

        public WebViewForm()
        {
            InitializeComponent();
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            WebView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
            WebView.NavigationCompleted += WebView_NavigationCompleted;
            await WebView.EnsureCoreWebView2Async();

            WebView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            // WebView2にHTMLをセットする
            string htmlContent = "<html><body><form><input type='text' name='txt' /><input type='text' name='url' /></form></body></html>";
            WebView.CoreWebView2.NavigateToString(htmlContent);
        }

        private async void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (!navigationCompleted)
            {
                // フラグを設定
                navigationCompleted = true;

                // WebView2のナビゲーションが完了したら、非同期メソッドを呼び出す
                await EditAndSetHTMLAsync();
            }
        }

        private async Task EditAndSetHTMLAsync()
        {
            // WebView2でJavaScriptを実行してHTMLを編集する
            await WebView.CoreWebView2.ExecuteScriptAsync("document.querySelector('form').addEventListener('click', function(event) { window.chrome.webview.postMessage(event.target.getAttribute('name')); });");
        }

        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            // JavaScriptからのメッセージを受信
            string message = e.TryGetWebMessageAsString();

            // メッセージを解析してクリックされた要素などの情報を取得
            MessageBox.Show($"Clicked Element: {message}");
        }
    }
}
