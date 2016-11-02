using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileWatcher
{
    public partial class MainForm : Form
    {
        private bool fileWatching = false;

        public MainForm()
        {
            InitializeComponent();

            startWatchingButton.Enabled = false;
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {

        }

        private void clearButton_Click(object sender, EventArgs e)
        {

        }

        private void startWatchingButton_Click(object sender, EventArgs e)
        {

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
    }
}
