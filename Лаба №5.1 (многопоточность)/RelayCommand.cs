using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Лаба__5._1__многопоточность_
{
    public class RelayCommand : ICommand
    {
        private Action<object> _action;
        private Func<object, bool> _func;

        public RelayCommand(Action<object> action)
            : this(action, null)
        {
        }

        public RelayCommand(Action<object> action, Func<object, bool> func)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            _action = action;
            _func = func;
        }

        public bool CanExecute(object parameter)
        {
            if (_func != null)
            {
                return _func(parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
