using System;
using System.Collections.Generic;
using System.IO;
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
        const string DataFileName = @"..\..\people.txt";

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
            Person currSelPer = LvPeople.SelectedItem as Person;
            if (currSelPer == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.No) return;
            peopleList.Remove(currSelPer);
            LvPeople.Items.Refresh();  // tell ListView data has changed
            ResetField();
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
            // either this or binding in XAML
            // BtnUpdatePerson.IsEnabled = (currSelPer != null);
            // BtnDeletePerson.IsEnabled = (currSelPer != null);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataFromFile();
            LvPeople.Items.Refresh();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveDataToFile();
        }

        private void SaveDataToFile()
        {
            List<string> lines = new List<string>();
            foreach (Person p in peopleList)
            {
                lines.Add($"{p.Name};{p.Age}");
            }
            try
            {
                File.WriteAllLines(DataFileName, lines);
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException)
            { 
                MessageBox.Show(this, "Error writing to file\n" + ex.Message, "File access error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void LoadDataFromFile()
        {
            try
            {
                if (!File.Exists(DataFileName)) return; // ignore if no file
                List<string> errorsList = new List<string>();
                string[] linesArray = File.ReadAllLines(DataFileName);
                for (int i = 0; i < linesArray.Length; i++)
                {
                    string line = linesArray[i];    
                    var data = line.Split(';');
                    if (data.Length != 2)
                    {
                        errorsList.Add($"Each line must have exactly 2 fields (line {i + 1})" + "\n" + line);
                        continue;
                    }
                    // TODO: setters would allow us to avoid this code duplication
                    string name = data[0];
                    string ageStr = data[1];
                    if (!Person.IsNameValid(name, out string errorName))
                    {
                        errorsList.Add($"{errorName} (line {i + 1})");
                        continue;
                    }
                    int.TryParse(ageStr, out int age);
                    if (!Person.IsAgeValid(age, out string errorAge))
                    {
                        errorsList.Add($"{errorAge} (line {i + 1})");
                        continue;
                    }
                    peopleList.Add(new Person(name, age));
                    if (errorsList.Count != 0)
                    {
                        string allErrors = String.Join("\n", errorsList);
                        MessageBox.Show(this, $"Warning: some lines were ignored due to {errorsList.Count} errors:\n" + allErrors, "Data errors", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException)
            {
                MessageBox.Show(this, "Error reading from file\n" + ex.Message, "File access error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


    }
}
