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

namespace Day05TodosEF
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
            TodoDbContext ctx = new TodoDbContext();
            Random random = new Random();

            Todo todo1 = new Todo
            {
                Task = "Make Coffee " + random.Next(100),
                Difficulty = 4,
                // DueDate = Convert.ToDateTime("01/01/2002"), 
                Status = Todo.StatusEnum.Pending
            };

            ctx.TodoTasks.Add(todo1); 
            ctx.SaveChanges(); 
            Console.WriteLine("record added");

            try
            {
                Globals.DbContext = new TodoDbContext();
                LvTodoTasks.ItemsSource = Globals.DbContext.TodoTasks.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Fatal database error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }





        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExportAll_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
