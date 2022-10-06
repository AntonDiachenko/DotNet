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

namespace Day4ListGridViewPeople
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Person> peopleList = new List<Person>();


        public MainWindow()
        {
            InitializeComponent();
            LvPeople.ItemsSource = peopleList;
        }

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            if(!ArePersonInputsValid()) return;
            string name = TbxName.Text;
            int.TryParse(TbxAge.Text, out int age); // ok no need to validate again
            peopleList.Add(new Person(name, age));
            LvPeople.Items.Refresh();  // tell ListView data has changed
            ResetField();
        }

        private void BtnDeletePerson_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            Person currSelPer = LvPeople.SelectedItem as Person;
            if (currSelPer == null) return;
            if (!ArePersonInputsValid()) return;
            string name = TbxName.Text;
            int.TryParse(TbxAge.Text, out int age); 
            currSelPer.Name = name;
            currSelPer.Age = age;
            LvPeople.Items.Refresh();  // tell ListView data has changed
            ResetField();
        }


        private bool ArePersonInputsValid()
        {
            string name = TbxName.Text;
            if (!Person.IsNameValid(name, out string errorName))
            {
                MessageBox.Show(this, errorName, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string strAge = TbxAge.Text;
            if(!Person.IsAgeValid(strAge, out string errorAge))
            {
                MessageBox.Show(this, errorAge, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        private void ResetField() 
        {
            TbxName.Text = "";
            TbxAge.Text = "";
        }

        private void LvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person currSelPer = LvPeople.SelectedItem as Person;
            if (currSelPer == null)
            {
                ResetField();
            }
            else 
            {
                TbxName.Text = currSelPer.Name;
                TbxAge.Text = currSelPer.Age.ToString();
            }
        }

        private void MiExport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
