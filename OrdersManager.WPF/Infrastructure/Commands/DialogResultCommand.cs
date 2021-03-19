using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrdersManager.WPF.Infrastructure.Commands
{
    class DialogResultCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool? DialogResult { get; set; }
        public bool CanExecute(object parameter) => App.CurrentWindow != null;
       

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            var window = App.CurrentWindow;            
            if (parameter != null)
                DialogResult = Convert.ToBoolean(parameter);
            window.DialogResult = DialogResult;
            window.Close();
        }
    }
}
