using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameCard_OpenCV
{
    class NameCard_List
    {
        private string num;
        private string pic1;
        private string pic2;
        private string pic3;
        private string pic4;
        private string pic5;
        private string pic6;
        private string txt;

        public NameCard_List(string num, string pic1, string pic2, string pic3, string pic4, string pic5, string pic6, string txt)
        {
            this.num = num;
            this.pic1 = pic1;
            this.pic2 = pic2;
            this.pic3 = pic3;
            this.pic4 = pic4;
            this.pic5 = pic5;
            this.pic6 = pic6;
            this.txt = txt;

        }

        public string Num
        {
            get
            {
                return num;
            }
        }

        public string Pic1
        {
            get
            {
                return pic1;
            }
        }

        public string Pic2
        {
            get
            {
                return pic2;
            }
        }

        public string Pic3
        {
            get
            {
                return pic3;
            }
        }

        public string Pic4
        {
            get
            {
                return pic4;
            }
        }

        public string Pic5
        {
            get
            {
                return pic5;
            }
        }

        public string Pic6
        {
            get
            {
                return pic6;
            }
        }

        public string Txt
        {
            get
            {
                return txt;
            }
        }

        public override string ToString()
        {
            return string.Format("<pic1 ={0}, pic2 ={1}, pic3 ={2}, pic4 ={3}, pic5 ={4}, pic6 ={5}>", pic1, pic2, pic3, pic4, pic5, pic6, txt);
        }
    }
}
