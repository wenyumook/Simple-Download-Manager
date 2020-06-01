using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using MiniBlinkPinvoke;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Panuon.UI.Silver;
using PlugBase;

namespace Simple_Download_Manager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowX
    {

        public static string Aria2Web = "http://aria2c.com/";

        /// <summary>
        /// 插件加载路径
        /// </summary>
        public string PlugPath { get; private set; }
        
        XmlConfig config;
        
        public MainWindow()
        {
            InitializeComponent();
            
            // 初始化程序
            InitializeProgram();
            // 初始化全局组件后启动浏览器
            InitializeChromium();
        }


        /// <summary>
        /// 初始化程序
        /// </summary>
        void InitializeProgram()
        {
            config = new XmlConfig();
            //启动aria2
            Util.StartProcess("aria2c.exe", "--conf-path=aria2/aria2.conf");
            //加载插件
            LoadPlugs();
            foreach (Grid g in PlugsGrid.Children)
            {
                g.Visibility = Visibility.Hidden;
            }
            DownloadGrid.Visibility = Visibility.Visible;
            DownloadTreeViewItem.IsSelected = true;
            
        }

        /// <summary>
        /// 初始化浏览器
        /// </summary>
        public void InitializeChromium()
        {
            Download.GlobalObjectJs = this;
            
            Download.Url = Aria2Web;
            Download.Focus();
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        void LoadPlugs()
        {
            XmlNodeList plugs = config.GetChildNodes();
            
            foreach (XmlNode plug in plugs)
            {
                string plugName = plug.Attributes["Name"].Value;
                string plugDLLName = plug.Attributes["DLLName"].Value;
                string plugClassType = plug.Attributes["ClassType"].Value;

                TreeViewItem treeViewItemPlug = new TreeViewItem();
                treeViewItemPlug.Header = plugName;
                treeViewItemPlug.Padding = new Thickness(40, 0, 0, 0);
                
                Assembly asm = Assembly.LoadFrom(string.Format("{0}\\Plugs\\{1}", Environment.CurrentDirectory, plugDLLName));
                Type assemblyType = asm.GetType(plugClassType);
                IPlugBase newPlug = (IPlugBase)Activator.CreateInstance(assemblyType);
                newPlug.Initialize(PlugsGrid);
                treeViewItemPlug.MouseLeftButtonUp += newPlug.Click;

                PlugTreeViewItem.Items.Add(treeViewItemPlug);
            }

        }

        #region Menu

        private void DownloadTreeViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            /*if (DownloadGrid.Visibility != Visibility.Visible)
            {
                Download.Load(aria2Web);
            }*/
            foreach (Grid g in PlugsGrid.Children)
            {
                g.Visibility = Visibility.Hidden;
            }
            DownloadGrid.Visibility = Visibility.Visible;
        }

        #endregion


        protected override void OnClosing(CancelEventArgs e)
        {
            Process[] ps = Process.GetProcessesByName("aria2c");
            foreach (Process p in ps)
            {
                Console.WriteLine(p.ProcessName);
                p.Kill();
            }
            
            Console.WriteLine("主程序结束运行");
            //base.OnClosing(e);
        }

    }



}
