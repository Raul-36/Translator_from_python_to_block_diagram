using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace Translator_from_python_to_block_diagram
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string filePath;

        public string FilePath
        {
            get => filePath;
            set
            {
                if (filePath != value)
                {
                    filePath = value;
                    OnPropertyChanged(nameof(FilePath));
                }
            }
        }
        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void CodeUpdate(string newCode)
        {
            this.pythonCodeEditor.Text = newCode;
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Stream stream = new FileStream(FilePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        CodeUpdate(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"File \"{FilePath}\" not found");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (FilePath != null)
                File.WriteAllText(FilePath, this.pythonCodeEditor.Text);
        }
    }
}