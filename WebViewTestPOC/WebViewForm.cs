using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

            btnOpenCsv.Enabled = true;
        }

        private async Task EditAndSetHTMLAsync()
        {
            // WebView2でJavaScriptを実行してHTMLを編集する
            await WebView.CoreWebView2.ExecuteScriptAsync("document.querySelector('body').addEventListener('click', function(event) { window.chrome.webview.postMessage(event.target.getAttribute('name')); });");
            await WebView.CoreWebView2.ExecuteScriptAsync( @"document.addEventListener('mouseover', function(event) { 
                    event.target.style.border = '3px solid red'    
                } )");
            await WebView.CoreWebView2.ExecuteScriptAsync(@"document.addEventListener('mouseout', function(event) { 
                    event.target.style.border = ''    
                } )");
        }

        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            // JavaScriptからのメッセージを受信
            if( e.WebMessageAsJson == null || e.WebMessageAsJson == "null") return;
            var message = e.TryGetWebMessageAsString();

            // メッセージを解析してクリックされた要素などの情報を取得
            MessageBox.Show($"Clicked Element: {message.ToString()}");
        }

        private async void BtnOpenCsv_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                string[] header;

                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        if (!reader.EndOfStream)
                        {
                            // 最初の行を読み込み、カンマで分割して配列に格納
                            string headerLine = reader.ReadLine();
                            header = headerLine.Split(',');

                            await MakeToolbox(header);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("エラーが発生しました: " + ex.Message);
                    return;
                }
            }
        }

        private async Task MakeToolbox(string[] header)
        {
            string headersScript = @"var scenario_header = [" + string.Join(", ", header.Select(h => "'" + h + "'")) + "];";
            await WebView.CoreWebView2.ExecuteScriptAsync(headersScript);

            await WebView.CoreWebView2.ExecuteScriptAsync(@"const scenario_toolbar = document.createElement('div');");
            await WebView.CoreWebView2.ExecuteScriptAsync(@"scenario_toolbar.id = 'scenario_toolbar';");
            await WebView.CoreWebView2.ExecuteScriptAsync(@"scenario_toolbar.style.width = '100px';");
            await WebView.CoreWebView2.ExecuteScriptAsync(@"scenario_toolbar.style.backgroundColor = 'rgba(0,0,0,0.2)';");
            await WebView.CoreWebView2.ExecuteScriptAsync(@"scenario_toolbar.style.padding = '0.5rem';");
            await WebView.CoreWebView2.ExecuteScriptAsync(@"scenario_toolbar.style.float = 'left';");
            await WebView.CoreWebView2.ExecuteScriptAsync(@"scenario_toolbar.style.fontSize = '.75rem';");

            await WebView.CoreWebView2.ExecuteScriptAsync(@"const scenario_toolbar_title = document.createElement('div');");
            await WebView.CoreWebView2.ExecuteScriptAsync(@"scenario_toolbar_title.innerText = '項目をドラッグ';");

            await WebView.CoreWebView2.ExecuteScriptAsync(@"document.body.appendChild(scenario_toolbar);");

            await WebView.CoreWebView2.ExecuteScriptAsync(@"const items = scenario_header.map(header => { const item = document.createElement('div'); item.innerText = header; scenario_toolbar.appendChild(item); return item; });");
        }
    }
}
