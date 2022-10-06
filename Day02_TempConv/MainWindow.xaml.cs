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

namespace Day02_TempConv
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

        private void TbxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            double inValue = Double.Parse(TbxInput.Text);
            if (RbInputC.IsChecked == true && RbOutputC.IsChecked == true)
            {
                TbxOutput.Text = $"{inValue}";
            } 
            else if (RbInputC.IsChecked == true && RbOutputF.IsChecked == true)
            {
                double result = Math.Round((inValue * 1.8 + 32), 2);
                TbxOutput.Text = $"{result}";
            }
            else if (RbInputC.IsChecked == true && RbOutputK.IsChecked == true)
            {
                double result = Math.Round((inValue + 273.15), 2);
                TbxOutput.Text = $"{result}";
            }
            else if (RbInputF.IsChecked == true && RbOutputC.IsChecked == true)
            {
                double result = Math.Round(((inValue - 32) / 1.8), 2);
                TbxOutput.Text = $"{result}";
            }
            else if (RbInputF.IsChecked == true && RbOutputF.IsChecked == true)
            {
                TbxOutput.Text = $"{inValue}";
            }
            else if (RbInputF.IsChecked == true && RbOutputK.IsChecked == true)
            {
                double result = Math.Round((((inValue - 32) * 5) / 9 +273.15), 2);
                TbxOutput.Text = $"{result}";
            }
            else if (RbInputK.IsChecked == true && RbOutputC.IsChecked == true)
            {
                double result = Math.Round((inValue - 273.15), 2);
                TbxOutput.Text = $"{result}";
            }
            else if (RbInputK.IsChecked == true && RbOutputF.IsChecked == true)
            {
                double result = Math.Round((((inValue - 273.15)*9)/5 +32), 2);
                TbxOutput.Text = $"{result}";
            }
            else if (RbInputK.IsChecked == true && RbOutputK.IsChecked == true)
            {
                TbxOutput.Text = $"{inValue}";
            }
            else
            {
                MessageBox.Show("Make input and output scale selection first", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



        }

    }
}
