using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Download_Manager
{
    public static class Util
    {
        // netdisk;4.4.0.6;PC;PC-Windows;6.2.9200;WindowsBaiduYunGuanJia
        // netdisk;6.3.2.2;PC;PC-Windows;10.0.16299;WindowsBaiduYunGuanJia
        public static string UserAgent = "netdisk;6.9.7.4;PC;PC-Windows;10.0.18363;WindowsBaiduYunGuanJia";
        public static string Domain = "pan.baidu.com";
        public static string PanHome = "";
        public static string Order = "time";
        public static int Desc = 0;
        public static int Showempty = 0;
        public static int Web = 1;
        //public static int Page = 1;
        public static int Num = 100;
        public static string Dir = "%2F";
        public static string T = "0.21849106194607182";
        public static string Channel = "chunlei";

        public static int App_id = 250528;
        public static string Bdstoken = "";
        public static string Logid = "";
        public static int Clienttype = 0;
        public static string Sign = "";
        //public static string startLogTime = "1587282794399";

        public static string PanList = "";


        /// <summary>
        /// 获取秒时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetSecondsTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            Console.WriteLine(Convert.ToInt64(ts.TotalSeconds).ToString());
            return Convert.ToInt64(ts.TotalSeconds).ToString();

        }

        /// <summary>
        /// 获取毫秒时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetMillisecondsTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            Console.WriteLine(Convert.ToInt64(ts.TotalMilliseconds).ToString());
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// 执行外部程序,带参数
        /// </summary>
        /// <param name="runFilePath">执行文件所在路径</param>
        /// <param name="args">参数集合</param>
        /// <returns></returns>
        public static bool StartProcess(string runFilePath, params string[] args)
        {
            try
            {
                string s = "";
                foreach (string arg in args)
                {
                    s = s + arg + " ";
                }
                s = s.Trim();
                Process process = new Process();//创建进程对象
                ProcessStartInfo startInfo = new ProcessStartInfo(runFilePath, s);
                process.StartInfo = startInfo;
                process.StartInfo.UseShellExecute = false;
                startInfo.RedirectStandardInput = false;//不重定向输入
                startInfo.RedirectStandardOutput = false; //重定向输出
                startInfo.CreateNoWindow = true;//不创建窗口
                process.EnableRaisingEvents = true;//程序退出引发事件
                process.Exited += Process_Exited;
                process.Start();

                return true;
            }
            catch (Exception ex)
            {
                MessageBoxX.Show("启动应用程序时出错！原因：" + ex.Message, "");
            }
            return false;
        }

        private static void Process_Exited(object sender, EventArgs e)
        {
            Console.WriteLine("aria2.exe 结束运行");
        }
    }
}
