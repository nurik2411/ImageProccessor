using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProccessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image File (*.bmp,*.jpg,*.jpeg)|*.bmp;*.jpg;*.jpeg;*.BMP;*.JPG;*.JPEG|All Files (*.*)|*.*";

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                this.picOriginal.Image = new Bitmap(openFileDialog.FileName);

            }
        }

        public static string[] GetFilesFrom(string searchFolder, string[] filters, bool isRecursive)
        {
            List<string> filesFound = new List<string>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, string.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string searchFolder = @"C:\Users\NurulloAlijonov\Desktop\Color";
            var filters = new string[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
            var files = GetFilesFrom(searchFolder, filters, false);

            Bitmap proccessedImg = new Bitmap((Bitmap)this.picOriginal.Image);
            Proccess.ConvertToGray(proccessedImg, files);
            this.picProccessed.Image = proccessedImg;
        }
    }
}
