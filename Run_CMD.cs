using NameCard_OpenCV.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameCard_OpenCV
{
    class Run_CMD
    {
        System.Diagnostics.ProcessStartInfo proInfo = new System.Diagnostics.ProcessStartInfo();
        System.Diagnostics.Process pro = new System.Diagnostics.Process();

        public void run_cmd(string file_path)
        {
            // 실행할 파일명 입력 -- cmd
            proInfo.FileName = @"cmd";
            // cmd 창 띄우기 -- true(띄우지 않기.) false(띄우기)
            proInfo.CreateNoWindow = true;
            proInfo.UseShellExecute = false;
            // cmd 데이터 받기
            proInfo.RedirectStandardOutput = true;
            // cmd 데이터 보내기
            proInfo.RedirectStandardInput = true;
            // cmd 오류내용 받기
            proInfo.RedirectStandardError = true;

            pro.StartInfo = proInfo;
            pro.Start();
            
            pro.StandardInput.Write(@"python final_opencv.py -img " + file_path + Environment.NewLine);

            pro.StandardInput.Close();

            // 결과 값을 리턴 받습니다.
            string resultValue = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();
            pro.Close();
        }
    }
}
