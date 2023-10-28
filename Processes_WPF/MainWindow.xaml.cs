using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Processes_WPF
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowProcesses_Click(object sender, RoutedEventArgs e)
        {
            processListBox.Items.Clear();
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                processListBox.Items.Add($"{process.Id}-{process.ProcessName}");
            }
        }

        private void CreateNewProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("notepad.exe");
            }
            catch(Exception ex )
            {
                MessageBox.Show($"Error:{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void KillProcess_Click(object sender, RoutedEventArgs e)
        {
            if (processListBox.SelectedIndex >= 0)
            {
                string selectedItem = processListBox.SelectedItem.ToString()!;
                int processId = Convert.ToInt32(selectedItem.Split('-')[0].Trim());
                try
                {
                    Process process=Process.GetProcessById(processId);
                    process.Kill();
                    MessageBox.Show("Process terminated succesfully!","Sucsess",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select process to terminate!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
