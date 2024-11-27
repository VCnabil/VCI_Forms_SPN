using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCI_Forms_SPN
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btn_SetCoordinates(object sender, EventArgs e)
        {
            double coordLat1 = vCinc_LatLon1.LatitudeDecimal;
            double coordLon1 = vCinc_LatLon1.LongitudeDecimal;

            double coordLat2 = vCinc_LatLon2.LatitudeDecimal;
            double coordLon2 = vCinc_LatLon2.LongitudeDecimal;

            // Convert coordinates to strings for Form1
            string lat1 = coordLat1.ToString();
            string lon1 = coordLon1.ToString();
            string lat2 = coordLat2.ToString();
            string lon2 = coordLon2.ToString();

            // Create and show Form1 with the coordinates
            Form1 form1 = new Form1(lat1, lon1, lat2, lon2);
            form1.Show();

        }
    }
}
