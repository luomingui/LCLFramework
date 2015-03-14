using LCL.OpenLicense;
using LCL.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace LCL.LicensseManage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.txt_PastDate.Value = DateTime.Now.AddDays(90);
        }
        private void but_Save_Click(object sender, EventArgs e)
        {
            LicenseEntity license = new LicenseEntity();
            license.ID = Guid.NewGuid();
            license.Name = txt_Name.Text;
            license.AssemblyName = txt_AssemblyName.Text;
            license.PastDate = txt_PastDate.Value.ToString("yyyy-MM-dd");
            license.UseNumber = int.Parse(txt_UseNumber.Text);
            license.InstallNumber = int.Parse(txt_InstallNumber.Text);
            license.VersionUpgrade = txt_VersionUpgrade.Checked;

            string context = DESEncrypt.Encrypt(XmlFormatterSerializer.SerializeToXml(license, license.GetType()));

            string licenseFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LCL.lic");
            if (!File.Exists(licenseFile) == true)
            {
                File.Delete(licenseFile);
            }
            File.WriteAllText(licenseFile, context);

            MessageBox.Show("写入成功");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog1 = new OpenFileDialog();
            fileDialog1.Filter = "文件类型(*.dll)|*.dll|All files (*.*)|*.*";
            fileDialog1.FilterIndex = 1;
            fileDialog1.RestoreDirectory = true;
            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                var ass = Assembly.LoadFile(fileDialog1.FileName);
                this.txt_AssemblyName.Text = ass.FullName;
            }
            else
            {
                this.txt_AssemblyName.Text = "LCL, Version=3.0.0.0, Culture=neutral, PublicKeyToken=7e4a2438e8435554";
            }
        }
    }
}
