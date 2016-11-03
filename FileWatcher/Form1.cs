using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileWatcher
{
    public partial class MainForm : Form
    {
        private bool fileWatching = false;
        private string fileName = "";
        private string pathName = "";
        private string fullPath = "";
        private FileSystemWatcher fileWatcher = new FileSystemWatcher();
        private string sToPrint = "";
        private bool mutex = false;
       

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
            sToPrint = "";
            fileWatcher = new FileSystemWatcher();
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

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            using (StreamReader reader = new StreamReader(fullPath))
            {
                string s = "";
                
                while( (s = reader.ReadLine()) != null)
                {
                    sToPrint = s;
                    
                }
            }
            
            mutex = true;
        }
        

        private void aboutButton_Click(object sender, EventArgs e)
        {
            String msg = "Hello!\nMy name is shawnkoon.\n" +
                         "Please come visit : https://github.com/shawnkoon \n" +
                         "Thanks!\n"+
                         "\n\nHow to use :\n"+
                         "1. Open File.\n2. Press Start!\n3. Watch it";
            MessageBox.Show(msg,"About the File Watcher!");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(mutex == true)
            {
                mutex = false;
                resultListBox.Items.Add(sToPrint);
            }
        }
    }
}
