using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace Laba2
{
    public partial class Form1 : Form
    {
        private string path = "DataBase.xml";
        private string pathXsl = "DataBase.xsl";
        public Form1()
        {
            InitializeComponent();
            buildBox(comboBoxCity, comboBoxCinema, comboBoxMovie, comboBoxDate, comboBoxTime, comboBoxPrice);
            comboBoxCity.Enabled = false;
            comboBoxCinema.Enabled = false;
            comboBoxMovie.Enabled = false;
            comboBoxDate.Enabled = false;
            comboBoxTime.Enabled = false;
            comboBoxPrice.Enabled = false;
            radioLINQ.Checked = true;
        }
        public void buildBox(ComboBox cityBox, ComboBox cinemaBox, ComboBox movieBox, ComboBox dateBox, ComboBox timeBox, ComboBox priceBox)
        {
            IStrategy p = new LINQToXML();
            List<Films> res = p.AnalyzeFile(new Films(), path);
            List<string> city = new List<string>();
            List<string> cinema = new List<string>();
            List<string> movie = new List<string>();
            List<string> date = new List<string>();
            List<string> time = new List<string>();
            List<string> price = new List<string>();
            foreach(Films elem in res)
            {
                if (!city.Contains(elem.city))
                {
                    city.Add(elem.city);
                }
                if (!cinema.Contains(elem.cinema))
                {
                    cinema.Add(elem.cinema);
                }
                if (!movie.Contains(elem.movie))
                {
                    movie.Add(elem.movie);
                }
                if (!date.Contains(elem.date))
                {
                    date.Add(elem.date);
                }
                if (!time.Contains(elem.time))
                {
                    time.Add(elem.time);
                }
                if (!price.Contains(elem.price))
                {
                    price.Add(elem.price);
                }
            }
            city = city.OrderBy(x => x).ToList();
            cinema = cinema.OrderBy(x => x).ToList();
            movie = movie.OrderBy(x => x).ToList();
            date = date.OrderBy(x => x).ToList();
            time = time.OrderBy(x => x).ToList();
            price = price.OrderBy(x => x).ToList();
            cityBox.Items.AddRange(city.ToArray());
            cinemaBox.Items.AddRange(cinema.ToArray());
            movieBox.Items.AddRange(movie.ToArray());
            dateBox.Items.AddRange(date.ToArray());
            timeBox.Items.AddRange(time.ToArray());
            priceBox.Items.AddRange(price.ToArray());
        }
        private Films OurSearch()
        {
            string[] info = new string[7];
            if (checkBoxCity.Checked) { info[0] = Convert.ToString(comboBoxCity.Text); }
            if (checkBoxCinema.Checked) { info[1] = Convert.ToString(comboBoxCinema.Text); }
            if (checkBoxMovie.Checked) { info[2] = Convert.ToString(comboBoxMovie.Text); }
            if (checkBoxDate.Checked) { info[3] = Convert.ToString(comboBoxDate.Text); }
            if (checkBoxTime.Checked) { info[4] = Convert.ToString(comboBoxTime.Text); }
            if (checkBoxPrice.Checked) { info[5] = Convert.ToString(comboBoxPrice.Text); }
            Films idealSearch = new Films(info);
            return idealSearch;
        }
        private void Parsing4XML()
        {
            Films myTemplate = OurSearch();
            List<Films> res;
            if (radioDOM.Checked)
            {
                IStrategy parser = new DOM();
                res = parser.AnalyzeFile(myTemplate, path);
                Output(res);
            }
            else if (radioSAX.Checked)
            {
                IStrategy parser = new SAX();
                res = parser.AnalyzeFile(myTemplate, path);
                Output(res);
            }
            else if (radioLINQ.Checked)
            {
                IStrategy parser = new LINQToXML();
                res = parser.AnalyzeFile(myTemplate, path);
                Output(res);
            }
        }
        private void Output(List<Films> res)
        {
            richTextBox1.Clear();
            foreach(Films n in res)
            {
                richTextBox1.AppendText("City: " + n.city + "\n");
                richTextBox1.AppendText("Cinema: " + n.cinema + "\n");
                richTextBox1.AppendText("Movie: " + n.movie + "\n");
                richTextBox1.AppendText("Data: " + n.date + "\n");
                richTextBox1.AppendText("Time: " + n.time + "\n");
                richTextBox1.AppendText("Price: " + n.price + "\n");
                richTextBox1.AppendText("--------------------------" + "\n");
            }
        }
        private void IntoHTML()
        {
            XslCompiledTransform xsl = new XslCompiledTransform();
            xsl.Load(pathXsl);
            string input = path;
            string result = @"HTML.html";
            xsl.Transform(input, result);
            MessageBox.Show("Done!");
        }
        private void Clear()
        {
            richTextBox1.Clear();
            radioDOM.Checked = false;
            radioSAX.Checked = false;
            radioLINQ.Checked = false;
            comboBoxCity.Text = null;
            comboBoxCinema.Text = null;
            comboBoxMovie.Text = null;
            comboBoxDate.Text = null;
            comboBoxTime.Text = null;
            comboBoxPrice.Text = null;
            checkBoxCity.Checked = false;
            checkBoxCinema.Checked = false;
            checkBoxMovie.Checked = false;
            checkBoxDate.Checked = false;
            checkBoxTime.Checked = false;
            checkBoxPrice.Checked = false;
        }
        private void Help()
        {
            MessageBox.Show("1) Оберіть критерії пошуку.\n" +
                "2) Оберіть метож пошуку (За замовчуванням LINQ to XML).\n" +
                "3) Натисніть кнопку SEARCH.\n" +
                "4) Якщо хочете перевести файл у HTML натисніть Transform to HTML та Open HTML.\n" +
                "5) Щоб очистити поля та виконати новий пошук, натисніть Clear.",
                "Help", MessageBoxButtons.OK);
        }
        private void OpenHTML()
        {
            buttonOpenHTML.Enabled = true;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Parsing4XML();
        }

        private void buttonTransformTOHTML_Click(object sender, EventArgs e)
        {
            IntoHTML();
            OpenHTML();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void buttonOpenHTML_Click(object sender, EventArgs e)
        {
            var openHTML = System.Diagnostics.Process.Start("HTML.html");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Close the program?", "Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Help();
        }

        private void checkBoxCity_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCity.Checked)
            {
                comboBoxCity.Enabled = true;
            }
            else
            {
                comboBoxCity.Enabled = false;
            }
        }

        private void checkBoxCinema_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCinema.Checked)
            {
                comboBoxCinema.Enabled = true;
            }
            else
            {
                comboBoxCinema.Enabled = false;
            }
        }

        private void checkBoxMovie_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMovie.Checked)
            {
                comboBoxMovie.Enabled = true;
            }
            else
            {
                comboBoxMovie.Enabled = false;
            }
        }

        private void checkBoxDate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDate.Checked)
            {
                comboBoxDate.Enabled = true;
            }
            else
            {
                comboBoxDate.Enabled = false;
            }
        }

        private void checkBoxTime_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTime.Checked)
            {
                comboBoxTime.Enabled = true;
            }
            else
            {
                comboBoxTime.Enabled = false;
            }
        }

        private void checkBoxPrice_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPrice.Checked)
            {
                comboBoxPrice.Enabled = true;
            }
            else
            {
                comboBoxPrice.Enabled = false;
            }
        }
    }
}
