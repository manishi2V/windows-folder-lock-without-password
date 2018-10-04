﻿using System;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace FolderLock
{
    public partial class Form_FolderLock : Form
    {
        public Form_FolderLock()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // Select the folder to lock
                tbxPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath = tbxPath.Text;
                string adminUserName = Environment.UserName;
                DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                ds.AddAccessRule(fsa);
                Directory.SetAccessControl(folderPath, ds);
                MessageBox.Show("Locked");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUnLock_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath = tbxPath.Text;
                string adminUserName = Environment.UserName;// getting your adminUserName
                DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                ds.RemoveAccessRule(fsa);
                Directory.SetAccessControl(folderPath, ds);
                MessageBox.Show("UnLocked");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
