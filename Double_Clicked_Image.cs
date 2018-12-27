using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NameCard_OpenCV
{
    public partial class Double_Clicked_Image : Form
    {
        public Double_Clicked_Image()
        {
            InitializeComponent();
        }

        Image picture;
        public Image PIcture
        {
            get
            {
                return picture;
            }

            set
            {
                picture = value;
                pictureBox1.Image = picture;
            }
        }

        private void Double_Clicked_Image_Load(object sender, EventArgs e)
        {
            this.Width = picture.Width;
            this.Height = picture.Height;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Double_Clicked_Image_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

        }
    }
}
