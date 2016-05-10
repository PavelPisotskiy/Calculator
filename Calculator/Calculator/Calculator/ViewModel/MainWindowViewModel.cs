using Calculator.Infrastructure;
using Calculator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private readonly ICalculator calculator = new Model.Calculator();
        private readonly IMessageService messageService = new MessageService();

        private string expression = string.Empty;

        public string Expression
        {
            get { return expression; }
            set
            {
                expression = value;
                OnPropertyChanged("Expression");
            }
        }



        ICommand calculateCommand;

        public ICommand CalculateCommand
        {
            get
            {
                if (calculateCommand == null)
                    calculateCommand = new RelayCommand(CalculateCommandExecute);

                return calculateCommand;
            }
        }

        private async void CalculateCommandExecute(object obj)
        {
            IsCalculating = true;
            string result = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(Expression))
                    result = await calculator.CalculateAsync(Expression);
            }
            catch (DivideByZeroException zeroEx)
            {
                messageService.ShowError(zeroEx.Message);
            }
            catch (FormatException formatEx)
            {
                messageService.ShowError(formatEx.Message);
            }

            Expression = result.Equals(string.Empty) ? Expression : result;
            IsCalculating = false;
        }

        ICommand inputNumberCommand;

        public ICommand InputNumberCommand
        {
            get
            {
                if (inputNumberCommand == null)
                    inputNumberCommand = new RelayCommand(InputNumberCommandExecute);

                return inputNumberCommand;
            }
        }

        private void InputNumberCommandExecute(object obj)
        {
            Expression += obj;
        }

        ICommand inputOperatorCommand;

        public ICommand InputOperatorCommand
        {
            get
            {
                if (inputOperatorCommand == null)
                    inputOperatorCommand = new RelayCommand(InputOperatorCommandExecute);

                return inputOperatorCommand;
            }
        }

        private void InputOperatorCommandExecute(object obj)
        {
            Expression += obj;
        }

        ICommand inputParenthesesCommand;

        public ICommand InputParenthesesCommand
        {
            get
            {
                if (inputParenthesesCommand == null)
                    inputParenthesesCommand = new RelayCommand(InputParenthesesCommandExecute);

                return inputParenthesesCommand;
            }
        }

        private void InputParenthesesCommandExecute(object obj)
        {
            Expression += obj;
        }

        ICommand backspaceCommand;

        public ICommand BackspaceCommand
        {
            get
            {
                if (backspaceCommand == null)
                    backspaceCommand = new RelayCommand(BackspaceCommandExecute);

                return backspaceCommand;
            }
        }

        private void BackspaceCommandExecute(object obj)
        {
            if (Expression.Length > 0)
                Expression = Expression.Remove(Expression.Length - 1, 1);
        }

        ICommand clearExpressionCommand;

        public ICommand ClearExpressionCommand
        {
            get
            {
                if (clearExpressionCommand == null)
                    clearExpressionCommand = new RelayCommand(ClearExpressionCommandExecute);

                return clearExpressionCommand;
            }
        }

        private void ClearExpressionCommandExecute(object obj)
        {
            Expression = string.Empty;
        }

        ICommand inputCommaCommand;

        public ICommand InputCommaCommand
        {
            get
            {
                if (inputCommaCommand == null)
                    inputCommaCommand = new RelayCommand(InputCommaCommandExecute);

                return inputCommaCommand;
            }
        }

        private void InputCommaCommandExecute(object obj)
        {
            Expression += ",";
        }

        private bool isCalculating = false;

        public bool IsCalculating
        {
            get
            {
                return isCalculating;
            }
            private set
            {
                isCalculating = value;
                OnPropertyChanged("IsCalculating");
            }
        }
    }
}
