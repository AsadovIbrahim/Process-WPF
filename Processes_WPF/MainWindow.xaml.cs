using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

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
            catch (Exception ex)
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
                    Process process = Process.GetProcessById(processId);
                    process.Kill();
                    processListBox.Items.Remove(selectedItem);
                    MessageBox.Show("Process terminated succesfully!", "Sucsess", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select process to terminate!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void processListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
