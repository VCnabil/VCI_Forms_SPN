using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_SPN._GLobalz;

namespace VCI_Forms_SPN.MyForms.pdfFormSysdiag
{
    public partial class DEBUGER_StandaloneForm : Form
    {
        StringBuilder _sb;
        private List<string> messages;
        public DEBUGER_StandaloneForm()
        {
            InitializeComponent();
            if (_sb == null)
                _sb = new StringBuilder();
            if (messages == null)
                messages = new List<string>();
            EventsManagerLib.OnLogConsoleEvent += AddTextToConsole;
            btn_clear.Click += Btn_clear_Click;
        }
        private void Btn_clear_Click(object sender, EventArgs e)
        {
            ClearConsole();
        }
        private void ClearConsole()
        {
            _sb.Clear();
            textBox_Display.Text = _sb.ToString();
        }
        void AddTextToConsole(string argText)
        {
            //_sb.AppendLine(argText);
            //textBox_Display.Text = _sb.ToString();

            if (textBox_Display.InvokeRequired)
            {
                textBox_Display.Invoke(new MethodInvoker(delegate
                {
                    _sb.AppendLine(argText);
                    textBox_Display.Text = _sb.ToString();
                }));
            }
            else
            {
                _sb.AppendLine(argText);
                textBox_Display.Text = _sb.ToString();
            }
        }
    }
}
