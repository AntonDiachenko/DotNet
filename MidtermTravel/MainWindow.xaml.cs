using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace MidtermTravel
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
            /*
            DateTime depDate = new DateTime(2022, 11, 01);
            DateTime retDate = new DateTime(2022, 11, 15);
            TravelDbContext trDbC = new TravelDbContext();

            Trip trip1 = new Trip
            {
                Destination = "Hawaii",
                TravellerName = "Peppa",
                TravellerPassport = "AA123456",
                DepartureDate = depDate,
                ReturnDate = retDate
            };

            trDbC.Travels.Add(trip1);
            trDbC.SaveChanges();
            Console.WriteLine("record added");
            */


            try
            {
                Globals.DbContext = new TravelDbContext();
                LvTrips.ItemsSource = Globals.DbContext.Travels.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        private void LvTrips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trip selectedTrip = LvTrips.SelectedItem as Trip;
            BtnUpdate.IsEnabled = (selectedTrip != null);
            BtnDelete.IsEnabled = (selectedTrip != null);
            if (selectedTrip == null)
            {
                ResetFields();
            }
            else
            {
                TbxDestination.Text = selectedTrip.Destination;
                TbxName.Text = selectedTrip.TravellerName;
                TbxPassport.Text = selectedTrip.TravellerPassport;
                DepDate.SelectedDate = selectedTrip.DepartureDate;
                ReturnDate.SelectedDate = selectedTrip.ReturnDate;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!AreTripInputsValid()) return;
                string destination = TbxDestination.Text;
                string name = TbxName.Text;
                string passport = TbxPassport.Text;
                DateTime depDate = DepDate.SelectedDate.Value;
                DateTime retDate = ReturnDate.SelectedDate.Value;

                Globals.DbContext.Travels.Add(new Trip() { Destination = destination, TravellerName = name, TravellerPassport = passport, DepartureDate = depDate, ReturnDate = retDate });
                Globals.DbContext.SaveChanges();
                LvTrips.ItemsSource = Globals.DbContext.Travels.ToList();
                ResetFields();
                LvTrips.SelectedItem = null;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Trip selectedTrip = LvTrips.SelectedItem as Trip;
            if (selectedTrip == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                Globals.DbContext.Travels.Remove(selectedTrip);
                Globals.DbContext.SaveChanges();
                LvTrips.ItemsSource = Globals.DbContext.Travels.ToList();
                ResetFields();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Trip selectedTrip = LvTrips.SelectedItem as Trip;
            if (selectedTrip == null) return; // nothing selected, nothing to delete
            try
            {
                if (!AreTripInputsValid()) return;
                string destination = TbxDestination.Text;
                string name = TbxName.Text;
                string passport = TbxPassport.Text;
                DateTime depDate = DepDate.SelectedDate.Value;
                DateTime retDate = ReturnDate.SelectedDate.Value;

                selectedTrip.Destination = destination;
                selectedTrip.TravellerName = name;
                selectedTrip.TravellerPassport = passport;
                selectedTrip.DepartureDate = depDate;
                selectedTrip.ReturnDate = retDate;

                Globals.DbContext.SaveChanges();
                LvTrips.ItemsSource = Globals.DbContext.Travels.ToList();
                ResetFields();
                LvTrips.SelectedItem = null;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSaveSelected_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveIt = new SaveFileDialog();
            saveIt.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            if(saveIt.ShowDialog() != true) return;

//            List<Trip> tripExport = Globals.DbContext.Travels.ToList();
            List<string> lines = new List<string>();
            foreach (Trip t in LvTrips.SelectedItems)
            {
                lines.Add($"{t.Destination};{t.TravellerName};{t.TravellerPassport};{t.DepartureDate};{t.ReturnDate}");
            }
            try
            {
                File.WriteAllLines(saveIt.FileName, lines);
                MessageBox.Show(this, "Export successful!", "Export Status", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException)
            {
                MessageBox.Show(this, "Export failed\n" + ex.Message, "Export Status", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private bool AreTripInputsValid()
        {
            string destination = TbxDestination.Text;
            if (!Trip.IsDestinationValid(destination, out string errorDestination))
            {
                MessageBox.Show(this, errorDestination, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string name = TbxName.Text;
            if (!Trip.IsNameValid(destination, out string errorName))
            {
                MessageBox.Show(this, errorName, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            string passport = TbxPassport.Text;
            if (!Regex.IsMatch(passport, @"[A-Z]{2}[0-9]{6}"))
            {
                MessageBox.Show("Passport must be in AA123456 format", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            DateTime depDate = DepDate.SelectedDate.Value;
            if (!Trip.IsDepDateValid(depDate, out string errorDepDate))
            {
                MessageBox.Show(this, errorDepDate, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            DateTime retDate = ReturnDate.SelectedDate.Value;
            if (!Trip.IsReturnDateValid(depDate, retDate, out string errorRetDate))
            {
                MessageBox.Show(this, errorRetDate, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }



        private void ResetFields()
        {
            TbxDestination.Text = "";
            TbxName.Text = "";
            TbxPassport.Text = "";
            DepDate.SelectedDate = DateTime.Now;
            ReturnDate.SelectedDate = DateTime.Now;
        }
    }
}
