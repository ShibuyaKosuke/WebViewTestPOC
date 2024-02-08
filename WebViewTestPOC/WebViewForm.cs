using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
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

            WebView.CoreWebView2.Settings.IsScriptEnabled = true;

            WebView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            // WebView2にHTMLをセットする
            string htmlContent = "<html><head></head><body><form><input type='text' name='txt' /><input type='text' name='url' /></form></body></html>";
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

                // Log
                await AddLogStoker();
            }

            btnOpenCsv.Enabled = true;
        }

        private async Task EditAndSetHTMLAsync()
        {
            // WebView2でJavaScriptを実行してHTMLを編集する
            await WebView.CoreWebView2.ExecuteScriptAsync(@"document.addEventListener('mouseover', function(event) { 
                    event.target.style.outline = '3px solid red'    
                } )");
            await WebView.CoreWebView2.ExecuteScriptAsync(@"document.addEventListener('mouseout', function(event) { 
                    event.target.style.outline = ''    
                } )");
        }

        private async Task AddLogStoker()
        {
            await WebView.CoreWebView2.ExecuteScriptAsync(@"" + ReadResource("resources/log-stoker.js"));
        }

        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            // JavaScriptからのメッセージを受信
            if (e.WebMessageAsJson == null || e.WebMessageAsJson == "null") return;
            var message = e.TryGetWebMessageAsString();

            textBox1.Text += message + "\n\r=====================\n\r";
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
            // CSS
            await WebView.CoreWebView2.ExecuteScriptAsync(@"const style = document.createElement('style');");
            await WebView.CoreWebView2.ExecuteScriptAsync(@"style.innerText = '" + ReadResource("resources/toolbar.css") + "';");
            await WebView.CoreWebView2.ExecuteScriptAsync(@"document.head.appendChild(style);");


            // Toolbar
            await WebView.CoreWebView2.ExecuteScriptAsync(@"" + ReadResource("resources/toolbar.js"));

            string headersScript = @"var scenario_header = [" + string.Join(", ", header.Select(h => "'" + h + "'")) + "];";
            await WebView.CoreWebView2.ExecuteScriptAsync(headersScript);

            await WebView.CoreWebView2.ExecuteScriptAsync(@"scenario_toolbar(scenario_header);");
        }

        private string ReadResource(string filePath)
        {
            // 実行中のアセンブリを取得
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            // アセンブリの場所 (ファイルパス) を取得
            string assemblyLocation = assembly.Location;

            // 実行ファイルのディレクトリを取得
            string executableDirectory = System.IO.Path.GetDirectoryName(assemblyLocation);

            string resourceFile = Path.Combine(executableDirectory, filePath);


            // ファイルが存在するか確認
            if (File.Exists(resourceFile))
            {
                // ファイルを読み込む
                string content = File.ReadAllText(resourceFile);

                // 改行を取り除く
                return content.Replace("\r", "").Replace("\n", "");

            }
            else
            {
                throw new Exception("指定したファイルは存在しません。");
            }
        }
    }
}
