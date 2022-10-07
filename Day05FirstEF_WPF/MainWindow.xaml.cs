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

namespace Day05FirstEF_WPF
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Globals.DbContext = new SocietyDbContext(); // FIXME: exceptions
                LvPeople.ItemsSource = Globals.DbContext.People.ToList(); // equivalent of SELECT * FROM People
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }



        private void BtnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            Person currSelectedPerson = LvPeople.SelectedItem as Person;
            if (currSelectedPerson == null) return; // nothing selected, nothing to delete
            try
            {
                string name = TbxName.Text; // FIXME: validation
                int.TryParse(TbxAge.Text, out int age); // FIXME!
                currSelectedPerson.Name = name;
                currSelectedPerson.Age = age;
                Globals.DbContext.SaveChanges(); // ex
                LvPeople.ItemsSource = Globals.DbContext.People.ToList(); // ex, equivalent of SELECT * FROM People
                ResetFields();
                LvPeople.SelectedItem = null;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            Person currSelectedPerson = LvPeople.SelectedItem as Person;
            if (currSelectedPerson == null) return; // nothing selected, nothing to delete
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                Globals.DbContext.People.Remove(currSelectedPerson);
                Globals.DbContext.SaveChanges(); // ex
                LvPeople.ItemsSource = Globals.DbContext.People.ToList(); // ex, equivalent of SELECT * FROM People
                ResetFields();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = TbxName.Text; // FIXME! validation
                int.TryParse(TbxAge.Text, out int age); // FIXME! validation
                Globals.DbContext.People.Add(new Person() { Name = name, Age = age });
                Globals.DbContext.SaveChanges(); // ex
                LvPeople.ItemsSource = Globals.DbContext.People.ToList(); // ex, equivalent of SELECT * FROM People
                ResetFields();
                LvPeople.SelectedItem = null;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person currSelectedPerson = LvPeople.SelectedItem as Person;
            BtnUpdatePerson.IsEnabled = (currSelectedPerson != null);
            BtnDeletePerson.IsEnabled = (currSelectedPerson != null);
            if (currSelectedPerson == null)
            {
                ResetFields();
            }
            else
            {
                TbxName.Text = currSelectedPerson.Name;
                TbxAge.Text = currSelectedPerson.Age + "";
            }
        }

        private void ResetFields()
        {
            TbxName.Text = "";
            TbxAge.Text = "";
        }
    }
}
