using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace TeamViewer_Cleanup_Tool
{
    public partial class Form1 : Form
    {
        Int32 er = 0;
        public Form1()
        {
            InitializeComponent();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_cleanup_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Attention: Make sure you've uninstalled the TeamViewer otherwise you will get clean up error");

            var confirmResult = MessageBox.Show("Are you sure you've uninstalled the TeamViewer?",
                                     "Confirm Cleanup!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                btn_cleanup.Text = "Cleaning Please wait...";
                btn_cleanup.Enabled = false;

                try
                {

                    if (Directory.Exists(@"C:\Program Files\TeamViewer"))
                    {
                        System.IO.Directory.Delete(@"C:\Program Files\TeamViewer", true);
                    }
                    if (Directory.Exists(@"C:\Program Files(x86)\TeamViewer"))
                    {
                        System.IO.Directory.Delete(@"C:\Program Files(x86)\TeamViewer", true);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    er = 1;
                }


                try
                {
                    RegistryKey regKey = Registry.LocalMachine.OpenSubKey("Software", true);
                    //regKey.DeleteSubKey("TeamViewer", true);

                    regKey.DeleteSubKeyTree("TeamViewer", true);
                    regKey.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());

                }


                var localApplicationData = Environment.ExpandEnvironmentVariables("%appdata%");
                string localpath = localApplicationData + @"\TeamViewer";

                try
                {
                    if (Directory.Exists(@localpath))
                    {
                        System.IO.Directory.Delete(@localpath, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    er = 1;
                }



                btn_cleanup.Text = "Start Clean up Now";
                btn_cleanup.Enabled = true;
                if (er == 0)
                {
                    MessageBox.Show("Cleanup has been completed without any errors");
                }
                else
                {
                    MessageBox.Show("Cleanup has been completed with some errors");
                }
            }
        }
    }
}
