using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomePay.ViewModels
{
    public class RelayCommand : ICommand
    {

        // Fields 
        #region Fields
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        #endregion

        // Constructors 
        #region Constructors
        public RelayCommand(Action<object> execute) : this(execute, null)
        {

        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            _execute = execute;
            _canExecute = canExecute ?? (x => true);
        }
        #endregion

        //Members
        #region ICommand Members 

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
   
        public void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion // ICommand Members
    }
}
