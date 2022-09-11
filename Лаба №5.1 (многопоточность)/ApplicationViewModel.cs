using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Лаба__5._1__многопоточность_
{
    class ApplicationViewModel: INotifyPropertyChanged
    {
        private string[] data;
        private string pathFile;
        public ReadInformation readInformation = new ReadInformation();

        public string PathFile
        {
            get { return pathFile; }
            set
            {
                pathFile = value;
                OnPropertyChanged("PathFile");
            }
        }

        private RelayCommand findFile;
        public RelayCommand FindFile
        {
            get
            {
                return findFile ??
                  (findFile = new RelayCommand(async obj =>
                  {
                      OpenFileDialog openFileDialog = new OpenFileDialog();
                      if (openFileDialog.ShowDialog() == true)
                      {
                          PathFile = openFileDialog.FileName;
                          StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                          string inf = await streamReader.ReadToEndAsync();
                          data = inf.Split(new char[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries);
                      }
                  }));
            }
        }

        private RelayCommand dataProcessing;
        public RelayCommand DataProcessing
        {
            get 
            {
                return dataProcessing ??
                  (dataProcessing = new RelayCommand(async obj =>
                  {
                      //не має сенсу робити синхронізацію, бо спільний ресурс не змінюється, а лише використовується
                      var t1 = Task.Run(() => FindNumbers());
                      var t2 = Task.Run(() => FindWords());

                      await Task.WhenAll(t1, t2);
                      //Task t1 = Task.Run(() => FindNumbers());
                      //t1.Wait();

                      InformationWindow infWindow = new InformationWindow();
                      infWindow.Show();
                  }));
            }
        }

        private void FindNumbers()
        {
            Thread.Sleep(5000);
            List<int> helpList = new List<int>();
            foreach (string b in data)
            {
                if (int.TryParse(b, out int n))
                {
                    helpList.Add(n);
                }
            }
            readInformation.Numbers = helpList;
        }

        private void FindWords()
        {
            List<string> helpList = new List<string>();
            foreach (string b in data)
            {
                if (!int.TryParse(b, out int n) && !helpList.Contains(b))
                {
                    helpList.Add(b);
                }
            }
            readInformation.Words = helpList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
