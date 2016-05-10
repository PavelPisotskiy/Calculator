using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Calculator.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void expressionTextBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            expressionTextBox.SelectAll();
        }

        private void expressionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            expressionTextBox.Focusable = true;
            expressionTextBox.Focus();
            expressionTextBox.SelectionStart = expressionTextBox.Text.Length;
            expressionTextBox.Focusable = false;
        }

        private void busyIndicator_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (busyIndicator.IsEnabled)
                busyIndicator.Visibility = Visibility.Visible;
            else
                busyIndicator.Visibility = Visibility.Collapsed;
        }
    }
}
