using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.ComponentModel;
using System.Collections;

namespace ServiceApplication
{
    public partial class ImageDownload : PhoneApplicationPage
    {
        private BackgroundWorker bw = new BackgroundWorker();
        List<string> list = new List<string>();
        public string strFileName = "default";
        public int i = 0;
        public ImageDownload()
        {

            InitializeComponent();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            list.Add(this.tb1.Text);
            list.Add(this.tb2.Text);
            list.Add(this.tb3.Text);
            list.Add(this.tb4.Text);
            list.Add(this.tb5.Text);
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {

            Uri uri;
            BackgroundWorker worker = sender as BackgroundWorker;
            WebClient webClient = new WebClient();
            System.Diagnostics.Debug.WriteLine(bw.IsBusy);
            if (i < 5)
            {
                String strUrl = list.ElementAt(i);
                uri = new Uri(strUrl);
                String[] strArr = new String[] { "/" };
                String[] strArr1 = strUrl.Split(strArr, StringSplitOptions.RemoveEmptyEntries);
                strFileName = strArr1[strArr1.Length - 1];
                webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(client_OpenReadCompleted);
                webClient.OpenReadAsync(uri);
            }
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if(i > 4)
            //MessageBox.Show("Download Complete");
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //this.tbProgress.Text = (e.ProgressPercentage.ToString() + "%");
        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(bw.IsBusy);
            if (bw.IsBusy != true)
            {
                i = 0;
                bw.RunWorkerAsync();
            }

        }
        void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Console.WriteLine("Downloaded");
            System.Diagnostics.Debug.WriteLine("Downloaded " + strFileName);
            var file = IsolatedStorageFile.GetUserStoreForApplication();
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(strFileName, System.IO.FileMode.Create, file))
            {
                byte[] buffer = new byte[1024];
                while (e.Result.Read(buffer, 0, buffer.Length) > 0)
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
            if (bw.IsBusy != true && i < 5)
            {
                i++;
                bw.RunWorkerAsync();
            }

        }
    }
}