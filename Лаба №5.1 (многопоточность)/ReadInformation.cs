using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Лаба__5._1__многопоточность_
{
    class ReadInformation
    {
        private List<int> numbers;
        private List<string> words;

        public List<int> Numbers
        {
            get { return numbers; }
            set 
            { 
                numbers = value;
                OnPropertyChanged("Numbers");
            }
        }

        public List<string> Words
        {
            get { return words; }
            set 
            {
                words = value;
                OnPropertyChanged("Words");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
