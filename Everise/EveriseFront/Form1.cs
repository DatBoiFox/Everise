using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveriseFront.Helpers;
using EveriseFront.Properties;

namespace EveriseFront
{
    public partial class Form1 : Form
    {
        private string in_file;
        private string out_file;

        private string csFileContent;
        private string csvFileContent;

        public Form1()
        {
            InitializeComponent();
            statusMessage.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            in_file = Settings.Default["inputFilePath"].ToString();
            out_file = Settings.Default["outputFilePath"].ToString();

            if(in_file != null || in_file != "")
            {
                inFile.Text = in_file;
            }

            if (out_file != null || out_file != "")
            {
                outFile.Text = out_file;
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            csvFileContent = GetFileContent(in_file);
            csFileContent = GetFileContent(out_file);

            statusMessage.Text = "Loading..";

            var responseMessage = await HTTPHelper.Instance.Post(textBox1.Text, new Models.Data(csvFileContent, csFileContent));

            if(responseMessage.Status == HTTPHelper.ResponseStatus.Successful)
            {
                WriteResults(out_file, responseMessage.Message);
                statusMessage.Text = "Finished";
            }
            else
            {
                statusMessage.Text = responseMessage.Message;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            in_file = openFileDialog1.FileName;
            inFile.Text = in_file;
            Settings.Default["inputFilePath"] = in_file;
            Settings.Default.Save();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            out_file = openFileDialog2.FileName;
            outFile.Text = out_file;
            Settings.Default["outputFilePath"] = out_file;
            Settings.Default.Save();
        }


        private string GetFileContent(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);
        }

        private void WriteResults(string filePath, string content)
        {
            System.IO.File.WriteAllText(filePath, content);
        }

    }
}
