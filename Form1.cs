using MetroFramework.Forms;
using NameCard_OpenCV.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NameCard_OpenCV
{
    public partial class Form1 : MetroForm
    {
        Run_CMD rc = new Run_CMD();

        List<NameCard_List> nc_list = new List<NameCard_List>();

        public Form1()
        {
            InitializeComponent();
        }

        Bitmap no_image = new Bitmap(Resources.no_image);
        Bitmap img1;
        Bitmap img2;
        Bitmap img3;
        Bitmap img4;
        Bitmap img5;
        Bitmap img6;
        string img_path;

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = no_image;
            pictureBox2.Image = no_image;
            pictureBox3.Image = no_image;
            pictureBox4.Image = no_image;
            pictureBox5.Image = no_image;
            pictureBox6.Image = no_image;



            string sDirPath;
            sDirPath = Application.StartupPath + "\\namecard_img";
            DirectoryInfo di = new DirectoryInfo(sDirPath);
            if (di.Exists == false)
            {
                di.Create();
            }

            //nc_list.Add(new NameCard_List("AAAA", 123));
            //nc_list.Add(new NameCard_List("BBBB", 343434));
            //nc_list.Add(new NameCard_List("accaacA", 1242342));

            //listBox1.Items.Add(nc_list[0].Name);
            //listBox1.Items.Add("명함2");
            //listBox1.Items.Add("명함3");
            //listBox1.Items.Add("명함4");
            //listBox1.Items.Add("명함5");
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            String file_path = null;
            String fname = null;

            progressBar1.Value = 0;

            openFileDialog1.InitialDirectory = "";
            openFileDialog1.DefaultExt = ".jpg";
            openFileDialog1.Filter = "이미지 파일(*.jpg; *.jpeg; *.bmp; *.png)|*.jpg;*.jpeg; *.bmp;*.png";

            progressBar1.PerformStep();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file_path = openFileDialog1.FileName;
                fname = Path.GetFileName(file_path);

                rc.run_cmd(file_path);
                //MessageBox.Show("");

                progressBar1.PerformStep();

                string[] fname_split = fname.Split('.');


                //MessageBox.Show(fname_split[0]);
                // MessageBox.Show(fname_split[1]);

                string testpath = Directory.GetCurrentDirectory();

                img_path = testpath + "\\namecard_img";

                //MessageBox.Show(Directory.GetCurrentDirectory());

                List<string> filelist = new List<string>();

                progressBar1.PerformStep();

                if (Directory.Exists(img_path))
                {
                    // DirectoryInfo Class 를 생성 합니다.
                    DirectoryInfo di = new DirectoryInfo(img_path);

                    // foreach 구문을 이용하여 폴더 내부에 있는 파일정보를 가져옵니다.
                    foreach (var item in di.GetFiles())
                    {
                        filelist.Add(item.ToString());
                    }


                    //MessageBox.Show(img_path + "\\" + fname, "didididid");

                    progressBar1.PerformStep();
                    string pic1 = fname_split[0] + "." + fname_split[1];
                    string pic2 = fname_split[0] + "_1." + fname_split[1];
                    progressBar1.PerformStep();
                    string pic3 = fname_split[0] + "_2." + fname_split[1];
                    string pic4 = fname_split[0] + "_3." + fname_split[1];
                    string pic5 = fname_split[0] + "_4." + fname_split[1];
                    string pic6 = fname_split[0] + "_5." + fname_split[1];
                    string txt = fname_split[0] + "_5_txt.txt";
                    progressBar1.PerformStep();


                    img1 = new Bitmap(img_path + "\\" + pic1);
                    img2 = new Bitmap(img_path + "\\" + pic2);
                    img3 = new Bitmap(img_path + "\\" + pic3);
                    img4 = new Bitmap(img_path + "\\" + pic4);
                    progressBar1.PerformStep();
                    img5 = new Bitmap(img_path + "\\" + pic5);
                    img6 = new Bitmap(img_path + "\\" + pic6);
                    progressBar1.PerformStep();

                    pictureBox1.Image = img1;
                    pictureBox2.Image = img2;
                    pictureBox3.Image = img3;
                    pictureBox4.Image = img4;
                    pictureBox5.Image = img5;
                    progressBar1.PerformStep();
                    pictureBox6.Image = img6;





                    nc_list.Add(new NameCard_List((listBox1.Items.Count + 1) + "번째 명함", pic1, pic2, pic3, pic4, pic5, pic6, txt));
                    listBox1.Items.Add(nc_list[nc_list.Count - 1].Num);
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    progressBar1.PerformStep();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            no_overlap_form(pictureBox1);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            no_overlap_form(pictureBox2); ;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            no_overlap_form(pictureBox3);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            no_overlap_form(pictureBox4);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            no_overlap_form(pictureBox5);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            no_overlap_form(pictureBox6);
        }

        private void no_overlap_form(PictureBox pb)
        {
            Double_Clicked_Image dci = new Double_Clicked_Image();

            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "Double_Clicked_Image")
                {
                    frm.Activate();
                    return;
                }
            }
            dci.PIcture = pb.Image;
            dci.Show();
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            int sel = listBox1.SelectedIndex;
            img1 = new Bitmap(img_path + "\\" + nc_list[sel].Pic1);
            img2 = new Bitmap(img_path + "\\" + nc_list[sel].Pic2);
            img3 = new Bitmap(img_path + "\\" + nc_list[sel].Pic3);
            img4 = new Bitmap(img_path + "\\" + nc_list[sel].Pic4);
            img5 = new Bitmap(img_path + "\\" + nc_list[sel].Pic5);
            img6 = new Bitmap(img_path + "\\" + nc_list[sel].Pic6);
            string text = File.ReadAllText(img_path + "\\" + nc_list[sel].Txt);

            pictureBox1.Image = img1;
            pictureBox2.Image = img2;
            pictureBox3.Image = img3;
            pictureBox4.Image = img4;
            pictureBox5.Image = img5;
            pictureBox6.Image = img6;
            textBox1.Text = text;

            Regex rg = new Regex(@"01{1}[016789]{1}-[0-9]{3,4}-[0-9]{4}");
            //textBox2.Text = rg.ToString();

            metroTextBox1.Text = string.Format("");

            Match m = rg.Match(textBox1.Text);
            if (m.Success)
            {
                metroTextBox2.Text = m.ToString();
            }

            Regex em = new Regex(@"(\w+\.)*\w+@(\w+\.)+[A-Za-z]+");
            m = em.Match(text);
            if (m.Success)
            {
                metroTextBox3.Text = m.ToString();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                metroTextBox1.Enabled = false;
                metroTextBox2.Enabled = false;
                metroTextBox3.Enabled = false;
            }
            else
            {
                metroTextBox1.Enabled = true;
                metroTextBox2.Enabled = true;
                metroTextBox3.Enabled = true;
            }
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("저장 완료", "알림" );
        }

        private void Update_Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("업데이트 완료", "알림");
        }
    }
}
