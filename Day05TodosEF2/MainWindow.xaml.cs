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
using System.Xml.Linq;
using static Day05TodosEF2.Todo;

namespace Day05TodosEF2
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
            DateTime date1 = new DateTime(2020, 03, 04);
            Todo2DbContext ctx = new Todo2DbContext();
            Random random = new Random();

            Todo todo1 = new Todo 
            { 
                Task = "Call Alex" + random.Next(100), 
                Difficulty = 3,
                // DueDate = Convert.ToDateTime("01/01/2002"), 
                // DueDate = date1.ToString(),
                DueDate = date1,
                Status = Todo.StatusEnum.Pending 
            };

            ctx.TodoTasks.Add(todo1); 
            ctx.SaveChanges();
            Console.WriteLine("record added");
            */


            try
            {
                Globals.DbContext = new Todo2DbContext();
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
            
            try
            {
                if (!AreTodoInputsValid()) return;
                string task = TbxTask.Text; 
                int difficulty = (int)SliderDifficulty.Value; 
 //               DateTime dueDate = new DateTime();
                DateTime dueDate = DpDate.SelectedDate.Value;

                string status = ComboStatus.SelectedValue?.ToString();
                StatusEnum comboStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), status);

                Globals.DbContext.TodoTasks.Add(new Todo() { Task = task, Difficulty = difficulty, DueDate = dueDate, Status = comboStatus });
                Globals.DbContext.SaveChanges(); 
                LvTodoTasks.ItemsSource = Globals.DbContext.TodoTasks.ToList(); 
                ResetFields();
                LvTodoTasks.SelectedItem = null;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Todo selectedTodo = LvTodoTasks.SelectedItem as Todo;
            if (selectedTodo == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                Globals.DbContext.TodoTasks.Remove(selectedTodo);
                Globals.DbContext.SaveChanges();
                LvTodoTasks.ItemsSource = Globals.DbContext.TodoTasks.ToList(); 
                ResetFields();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Todo selectedTodo = LvTodoTasks.SelectedItem as Todo;
            if (selectedTodo == null) return; // nothing selected, nothing to delete
            try
            {
                string task = TbxTask.Text; 
                int difficulty = (int)SliderDifficulty.Value;
                DateTime dueDate = DpDate.SelectedDate.Value;
                string status = ComboStatus.SelectedValue?.ToString();
                StatusEnum comboStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), status);

                selectedTodo.Task = task;
                selectedTodo.Difficulty = difficulty;
                selectedTodo.DueDate = dueDate;
                selectedTodo.Status = comboStatus;

                Globals.DbContext.SaveChanges();
                LvTodoTasks.ItemsSource = Globals.DbContext.TodoTasks.ToList();
                ResetFields();
                LvTodoTasks.SelectedItem = null;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void LvTodoTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Todo selectedTodo = LvTodoTasks.SelectedItem as Todo;
            BtnUpdate.IsEnabled = (selectedTodo != null);
            BtnDelete.IsEnabled = (selectedTodo != null);
            if (selectedTodo == null)
            {
                ResetFields();
            }
            else
            {
                TbxTask.Text = selectedTodo.Task;
                SliderDifficulty.Value = selectedTodo.Difficulty;
                DpDate.SelectedDate = selectedTodo.DueDate;
                ComboStatus.SelectedValue = selectedTodo.Status.ToString();
            }
        }



        private void BtnExportAll_Click(object sender, RoutedEventArgs e)
        {

        }

        
        private bool AreTodoInputsValid()
        {
            string task = TbxTask.Text;

            if (!Todo.IsTaskValid(task, out string errorTask))
            {
                MessageBox.Show(this, errorTask, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            DateTime dueDate = DpDate.SelectedDate.Value;
            if (!Todo.IsDateValid(dueDate, out string errorDate))
            {
                MessageBox.Show(this, errorDate, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        


        private void ResetFields()
        {
            TbxTask.Text = "";
            SliderDifficulty.Value = 1;
            DpDate.SelectedDate = DateTime.Now;
            ComboDefault.IsSelected = true;
        }


    }
}
