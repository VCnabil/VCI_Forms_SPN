using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCI_Forms_SPN.MyForms.pdfFormSysdiag
{
    public partial class UC_NavButton : UserControl
    {
        public string ItemName
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public string ItemNumber
        {
            get => button1.Text;
            set => button1.Text = value;
        }

        // Event declaration
        public event Action<string> OnUCNavButtonClicked;

        public UC_NavButton()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OnUCNavButtonClicked?.Invoke(this.ItemNumber);
        }
    }
}
