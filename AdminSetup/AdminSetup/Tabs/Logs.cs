using Core.Models;
using Core.Views;

namespace AdminSetup.Tabs
{
    public partial class Logs : Form, ILogsView
    {
        public event EventHandler? FetchLogFileEvent;

        public string[] LogFileNames
        {
            set
            {
                LogFilesComboBox.Items.Clear();
                LogFilesComboBox.Items.AddRange(value);
            }
        }

        public string? SelectedLogFile => LogFilesComboBox?.SelectedItem?.ToString();

        public bool UiBlocked
        {
            get => !DownloadBtn.Enabled;
            set => DownloadBtn.Enabled = !value;
        }

        public Logs()
        {
            TopLevel = false;
            InitializeComponent();

            DownloadBtn.Click += (sender, args) => FetchLogFileEvent!.Invoke(sender, args);
        }

        public void ShowAlert(string text) => MessageBox.Show(text, Constants.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.None);

        public void SaveLogFile(string fileName, byte[] fileData)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = $"Save {fileName}";
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.FileName = fileName;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(saveFileDialog.FileName, fileData);
            }
        }
    }
}
