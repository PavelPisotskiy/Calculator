using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator.Infrastructure
{
    class MessageService : IMessageService
    {
        public void ShowError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }
    }
}
