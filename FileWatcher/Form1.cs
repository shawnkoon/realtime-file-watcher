using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FileWatcher
{
    public partial class MainForm : Form
    {
        private bool fileWatching;
        private string fileName;
        private string pathName;
        private string fullPath;
        private FileSystemWatcher fileWatcher;
        private Queue<String> fileItemQueue;
       

        public MainForm()
        {
            InitializeComponent();
            init();
            
        }

        private void init()
        {
            startWatchingButton.Enabled = false;
            fileWatching = false;
            fileName = "";
            pathName = "";
            fileWatcher = new FileSystemWatcher();
            fileItemQueue = new Queue<string>();
        }
        
        private void openFileButton_Click(object sender, EventArgs e)
        {
            if(fileWatching == true)
            {
                MessageBox.Show("Please press Stop first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                OpenFileDialog fileDialog = new OpenFileDialog();

                fileDialog.Title = "Open file to be watched.";
                fileDialog.InitialDirectory = @"C:\";


                if(fileDialog.ShowDialog() == DialogResult.OK)
                {
                    fullPath = fileDialog.FileName;
                    pathName = Path.GetDirectoryName(fullPath);
                    fileName = fileDialog.SafeFileName;
                }

                openFileButton.Enabled = false;
                startWatchingButton.Enabled = true;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if(resultListBox.Items.Count > 0)
            {
                resultListBox.Items.Clear();
            }
        }

        private void startWatchingButton_Click(object sender, EventArgs e)
        {
            if(fileWatching == false)
            {
                fileWatching = true;
                openFileButton.Enabled = false;

                fileWatcher.Path = pathName;
                fileWatcher.Filter = fileName;

                fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
                fileWatcher.Changed += new FileSystemEventHandler(OnChanged);
                
            

                fileWatcher.EnableRaisingEvents = true;
                startWatchingButton.Text = "STOP";
                timer1.Start();
            }
            else
            {
                fileWatcher.EnableRaisingEvents = false;
                startWatchingButton.Text = "Start Watching";
                timer1.Stop();
                openFileButton.Enabled = true;
                fileWatching = false;
            }
        }
        private void aboutButton_Click(object sender, EventArgs e)
        {
            String msg = "Hello!\nMy name is shawnkoon.\n" +
                         "Please come visit : https://github.com/shawnkoon \n" +
                         "Thanks!\n" +
                         "\n\nHow to use :\n" +
                         "1. Open File.\n2. Press Start!\n3. Watch it";
            MessageBox.Show(msg, "About the File Watcher!");
        }


        /*
         * This method will put newly added lines in the file into a Queue.
         */
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            var fileShare = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (StreamReader reader = new StreamReader(fileShare))
            {
                string s = "";
                
                while( (s = reader.ReadLine()) != null)
                {
                    // Add s to a Queue.
                    fileItemQueue.Enqueue(s);
                }
            }

            
        }
        
        /*
         * This method will write Queue out to a listBox.
         */
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(fileItemQueue.Count > 0)
            {
                resultListBox.Items.Add(fileItemQueue.Dequeue());

                resultListBox.SelectedIndex = resultListBox.Items.Count - 1;
                resultListBox.SelectedIndex = -1;
            }
        }
    }
}
