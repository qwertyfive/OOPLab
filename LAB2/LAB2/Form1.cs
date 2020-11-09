using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using System.Diagnostics;


namespace LAB2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void GetAllTournaments()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:/Users/qwert/Desktop/LAB2/LAB2/Tournaments.xml");
            XmlElement xRoot = doc.DocumentElement;
            XmlNodeList childNodes = xRoot.SelectNodes("Tournament");

            for (int i = 0; i < childNodes.Count; i++)
            {
                XmlNode n = childNodes.Item(i);
                addItems(n);
            }

            string cheapPrice = "0 - 700";
            string mediumPrice = "700 - 2500";
            string highPrice = "2500 - 5000";
            string veryHighPrice = "5000 - 10000";

            PriceL.Items.Add(cheapPrice);
            PriceL.Items.Add(mediumPrice);
            PriceL.Items.Add(highPrice);
            PriceL.Items.Add(veryHighPrice);

            comboBox4.Items.Add("Stockholm");
            comboBox4.Items.Add("Kyiv");
            comboBox4.Items.Add("Shanghai");
            comboBox4.Items.Add("Moscow");



            comboBox5.Items.Add("V1lat");
            comboBox5.Items.Add("GotHunt");
            comboBox5.Items.Add("XBOCT");
            comboBox5.Items.Add("Tonya");
            comboBox5.Items.Add("Dread");
            comboBox5.Items.Add("Goblak");
            comboBox5.Items.Add("Bafik");
            comboBox5.Items.Add("Casper");
            comboBox5.Items.Add("NS");
            comboBox5.Items.Add("Maelstorm");



            comboBox6.Items.Add("VP");
            comboBox6.Items.Add("NaVi");
            comboBox6.Items.Add("OG");
            comboBox6.Items.Add("Liquid");
            comboBox6.Items.Add("Nigma");
            comboBox6.Items.Add("PSG.LGD");
            comboBox6.Items.Add("ViciGaming");
            comboBox6.Items.Add("TeamSecret");
            comboBox6.Items.Add("EvilGeniuses");
            comboBox6.Items.Add("Alliance");
            comboBox6.Items.Add("Infamous");
        }


        private void addItems(XmlNode n)
        {
            if (!comboBox1.Items.Contains(n.SelectSingleNode("@Title").Value))
                comboBox1.Items.Add(n.SelectSingleNode("@Title").Value);

            if (!comboBox2.Items.Contains(n.SelectSingleNode("@Date").Value))
                comboBox2.Items.Add(n.SelectSingleNode("@Date").Value);

            if (!comboBox7.Items.Contains(n.SelectSingleNode("@Type").Value))
                comboBox7.Items.Add(n.SelectSingleNode("@Type").Value);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            search();
        }

        private void search()
        {
            richTextBox1.Text = "";
            Tournaments tournament = new Tournaments();

            tournament.Title = "";
            tournament.Date = "";
            tournament.PriceRange = "";
            tournament.Location = "";
            tournament.Commentators = "";
            tournament.Participants = "";
            tournament.Type = "";
            bool ex = false;

            if (checkBox1.Checked && comboBox1.SelectedItem != null)
            {
                tournament.Title = comboBox1.SelectedItem.ToString();
                ex = true;
            }

            if (checkBox2.Checked && comboBox2.SelectedItem != null)
            { 
                tournament.Date = comboBox2.SelectedItem.ToString();
                ex = true;
            }

            if (Price.Checked && PriceL.SelectedItem != null)
            { 
                tournament.PriceRange = PriceL.SelectedItem.ToString();
                ex = true;
            }
            if (checkBox4.Checked && comboBox4.SelectedItem != null)
            { 
                tournament.Location = comboBox4.SelectedItem.ToString();
                ex = true;
            }
            if (checkBox5.Checked && comboBox5.SelectedItem != null)
            { 
                tournament.Commentators = comboBox5.SelectedItem.ToString();
                ex = true;
            }
            if (checkBox6.Checked && comboBox6.SelectedItem != null)
            { 
                tournament.Participants = comboBox6.SelectedItem.ToString();
                ex = true;
            }
            if (checkBox7.Checked && comboBox7.SelectedItem != null)
            { 
                tournament.Type = comboBox7.SelectedItem.ToString();
                ex = true;
            }
            IStrategy analizator = new DOMStrategy();

            if (DOM.Checked)
                analizator = new DOMStrategy();

            if (radioButton2.Checked)
                analizator = new SAXStrategy();

            if (radioButton3.Checked)
                analizator = new LINQToXMLStrategy();
            if (ex) 
            { 
                List<Tournaments> result = analizator.Search(tournament);

                foreach (Tournaments conc in result)
                {
                    richTextBox1.Text += "Назва: " + conc.Title + "\n";
                    richTextBox1.Text += "Дата: " + conc.Date + "\n";
                    richTextBox1.Text += "Цінова категорія: " + conc.PriceRange + "\n";
                    richTextBox1.Text += "Місце проведення: " + conc.Location + "\n";
                    richTextBox1.Text += "Коментатори: " + conc.Commentators + "\n";
                    richTextBox1.Text += "Учасники: " + conc.Participants + "\n";
                    richTextBox1.Text += "Тип: " + conc.Type + "\n";

                    richTextBox1.Text += "\n\n\n";
                }
            }
            else
            {
                MessageBox.Show("Заповніть хоча б одне поле");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetAllTournaments();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            Price.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;

            comboBox1.Text = null;
            comboBox2.Text = null;
            PriceL.Text = null;
            comboBox4.Text = null;
            comboBox5.Text = null;
            comboBox6.Text = null;
            comboBox7.Text = null;

            richTextBox1.Text = "";

        }

        private void Transform_Click(object sender, EventArgs e)
        {
            transform();
            Process.Start(@"c:\Users\qwert\Desktop\LAB2\LAB2\Tournaments.html");
        }

        private void transform()
        {
            XslCompiledTransform xct = new XslCompiledTransform();
            xct.Load(@"c:\Users\qwert\Desktop\LAB2\LAB2\Tournaments.xsl");
            string fXML = @"c:\Users\qwert\Desktop\LAB2\LAB2\Tournaments.xml";
            string fHTML = @"c:\Users\qwert\Desktop\LAB2\LAB2\Tournaments.html";
            xct.Transform(fXML, fHTML);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void DOM_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Price_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PriceL_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void DOM_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}
