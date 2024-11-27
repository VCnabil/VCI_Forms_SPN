using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_SPN.MyForms;

namespace VCI_Forms_SPN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          

            webView2Obj.Source = new Uri("https://www.movable-type.co.uk/scripts/latlong.html");
            webView2Obj.NavigationCompleted += WebView_NavigationCompleted;
        }
        private void WebView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
               // MessageBox.Show("Page loaded successfully!");
            }
            else
            {
                MessageBox.Show($"Failed to load page: {e.WebErrorStatus}");
            }
        }


        public Form1(string lat1, string lon1, string lat2, string lon2)
        {
            InitializeComponent();

            webView2Obj.Source = new Uri("https://www.movable-type.co.uk/scripts/latlong.html");
            webView2Obj.NavigationCompleted += (sender, e) =>
            {
                if (e.IsSuccess)
                {
                    FillForm(lat1, lon1, lat2, lon2);
                }
                else
                {
                    MessageBox.Show($"Failed to load page: {e.WebErrorStatus}");
                }
            };
        }

        private async void FillForm(string lat1, string lon1, string lat2, string lon2)
        {
            string script = $@"
            document.querySelector('.lat1').value = '{lat1}';
            document.querySelector('.lon1').value = '{lon1}';
            document.querySelector('.lat2').value = '{lat2}';
            document.querySelector('.lon2').value = '{lon2}';
            ";
            await webView2Obj.CoreWebView2.ExecuteScriptAsync(script);
        }

    }
}
