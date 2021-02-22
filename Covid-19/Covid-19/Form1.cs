using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Covid_19
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        static List<Cases> CasesList = new List<Cases>();
        OleDbConnection con = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = CovidCases.accdb");

        private void button2_Click(object sender, EventArgs e)
        {
            if ((CasesList.FindIndex( c=>c.Name == textBox2.Text) == -1) | (CasesList.FindIndex(c => c.Fname == textBox3.Text) == -1))
            {
                if ((textBox2.Text != "") & (textBox3.Text != "") & (maskedTextBox1.MaskFull) &
                    (textBox1.Text.Length <= 18) & (textBox2.Text.Length <= 18) & AllowedMail(textBox6.Text) &
                    (richTextBox2.Text != ""))
                {
                    CasesList.Add(new Cases(textBox2.Text, textBox3.Text, maskedTextBox1.Text, textBox6.Text, richTextBox2.Text, richTextBox3.Text,
                                    comboBox1.SelectedIndex == 0 ? "M" : "F", dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, dateTimePicker2.Value.ToString("HH:mm")));
                    
                    int i = CasesList.Count - 1;
                    Edit_Add_Case(i, false);

                    OleDbCommand cmd = con.CreateCommand();
                    con.Open();
                    cmd.CommandText = "insert into Cases ([CaseName], [Fname], [PhoneNum], [Mail], [Address], [Diseases], [Gender], [Bdate], [RecordDate], [Time]) " +
                        "values ('" + CasesList[i].Name+ "','" + CasesList[i].Fname + "','" + CasesList[i].Num + "','" + CasesList[i].Mail + "','" + CasesList[i].Adress + "'," +
                        "'" + CasesList[i].Diseases + "', '" + CasesList[i].Gender + "', '" + CasesList[i].Bdate.ToShortDateString() + "', '" + CasesList[i].RecordDate.ToShortDateString() + "', '" + CasesList[i].Time + "')";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Case added.");

                }
                else
                {
                    MessageBox.Show("Wrong data.");
                }
            }
            else
                MessageBox.Show("This Name , Last name or number is already in the case list.");
        }
        private void listBox1_Click(object sender, EventArgs e)
        {
            
        }
        public bool AllowedMail(string mail)
        {
            try
            {
                var test = new System.Net.Mail.MailAddress(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
        void Edit_Add_Case(int i, bool edit) // edit to define whether it's edit or add.
        {
            string spacez = "    ";
            string spacex = "";
            for (int x = 0; x < 18 - CasesList[i].Name.Length; x++)
                spacex += " ";
            string spacey = "";
            for (int y = 0; y < 18 - CasesList[i].Fname.Length; y++)
                spacey += " ";
            if (edit)
                listBox1.Items.Insert(i,CasesList[i].RecordDate.ToShortDateString() + spacez + CasesList[i].Name + spacex + CasesList[i].Fname +
                    spacey + CasesList[i].Gender + spacez + CasesList[i].Num + spacez + CasesList[i].Mail);
            else
                listBox1.Items.Add(CasesList[i].RecordDate.ToShortDateString() + spacez + CasesList[i].Name + spacex + CasesList[i].Fname +
                    spacey + CasesList[i].Gender + spacez + CasesList[i].Num + spacez + CasesList[i].Mail);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            int i;
            if ((i = listBox1.SelectedIndex) != -1)
            {
                //remove Case
               
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "delete from Cases Where (CaseName = '" + CasesList[i].Name + "' and Fname = '" + CasesList[i].Fname +"')";
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();

                CasesList.RemoveAt(i);
                listBox1.Items.RemoveAt(i);
                MessageBox.Show("Case deleted.");
            }
            else
                MessageBox.Show("Select a Case to delete.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox4.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            textBox2.Text = "";
            textBox3.Text = "";
            maskedTextBox1.Text = "";
            textBox6.Text = "";


            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i;
            if ((i = listBox1.SelectedIndex) != -1)
            {
                if ((CasesList.FindIndex(c => c.Name == textBox2.Text) == -1) | (CasesList.FindIndex(c => c.Fname == textBox3.Text) == -1))
                {
                    if ((textBox2.Text != "") & (textBox3.Text != "") & (maskedTextBox1.MaskFull) &
                        (textBox1.Text.Length <= 18) & (textBox2.Text.Length <= 18) & AllowedMail(textBox6.Text) &
                        (richTextBox2.Text != ""))
                    {
                        string tempname = CasesList[i].Name;
                        string tempFname = CasesList[i].Fname;
                        CasesList.RemoveAt(i);
                        listBox1.Items.RemoveAt(i);

                        CasesList.Insert(i, new Cases(textBox2.Text, textBox3.Text, maskedTextBox1.Text, textBox6.Text, richTextBox2.Text, richTextBox3.Text,
                                        comboBox1.SelectedIndex == 0 ? "M" : "F", dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, dateTimePicker2.Value.ToString("HH:mm")));

                        Edit_Add_Case(i, true);

                        OleDbCommand cmd = con.CreateCommand();
                        con.Open();
                        cmd.CommandText = "update Cases " +
                            "set [CaseName] ='" + CasesList[i].Name + "' , [Fname]='" + CasesList[i].Fname + "', [PhoneNum] ='" + CasesList[i].Num + "', [Mail] = '" + CasesList[i].Mail + "', [Address] ='" + CasesList[i].Adress + "'," +
                            " [Diseases]='" + CasesList[i].Diseases + "' , [Gender]=  '" + CasesList[i].Gender + "', [Bdate] ='" + CasesList[i].Bdate.ToShortDateString() + "', [RecordDate] ='" + CasesList[i].RecordDate.ToShortDateString() + "', [Time]='" + CasesList[i].Time + "'" +
                            "Where(CaseName = '" + tempname + "' and Fname = '" + tempFname +"')";

                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }   
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = CovidCases.accdb");
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM Cases ORDER BY RecordDate";
            cmd.Connection = con;
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    CasesList.Add( new Cases(reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), 
                        reader[6].ToString(), reader[7].ToString(), DateTime.Parse(reader[8].ToString()), DateTime.Parse(reader[9].ToString()), reader[10].ToString()));
                }
                reader.Close();
            }

            con.Close();
            if (CasesList != null)
            {

                for (int i = 0; i < CasesList.Count; i++)
                {
                    Edit_Add_Case(i, false);
                }
            }
            comboBox1.SelectedIndex = 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((listBox1.SelectedIndex) != -1)
            {
                richTextBox1.Clear();
                richTextBox2.Clear();
                richTextBox3.Clear();
                int i = listBox1.SelectedIndex;
                textBox2.Text = CasesList[i].Name;
                textBox3.Text = CasesList[i].Fname;
                maskedTextBox1.Text = CasesList[i].Num;
                textBox6.Text = CasesList[i].Mail;
                textBox7.Text = CasesList[i].RecordDate.ToShortDateString();
                textBox8.Text = CasesList[i].Time;

                dateTimePicker1.Value = CasesList[i].Bdate;
                dateTimePicker2.Value = CasesList[i].RecordDate;

                textBox5.Text = dateTimePicker1.Value.ToShortDateString();
                textBox4.Text = (DateTime.Today.Year - CasesList[i].Bdate.Year).ToString();
                richTextBox1.AppendText(CasesList[i].Adress);
                richTextBox2.AppendText(CasesList[i].Adress);
                comboBox1.SelectedIndex = CasesList[i].Gender == "M" ? 0 : 1  ;
                richTextBox3.AppendText(CasesList[i].Diseases);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index;
            if (radioButton1.Checked == true)
            {
                index = CasesList.FindIndex(c => c.Name == textBox1.Text);
                if (index != - 1)
                {
                    listBox1.SetSelected(index, true);
                    MessageBox.Show(CasesList[index].Name + "  " + CasesList[index].Fname + "  " + CasesList[index].Num +"  "+ CasesList[index].Mail);
                }
                else
                    MessageBox.Show("Name " + textBox1.Text + " isn't in the list");

            }
            else if (radioButton2.Checked == true)
            {
                index = CasesList.FindIndex(c => c.Fname == textBox1.Text);
                if (index != -1)
                {
                    listBox1.SetSelected(index, true);
                    MessageBox.Show(CasesList[index].Name + "  " + CasesList[index].Fname + "  " + CasesList[index].Num + "  " + CasesList[index].Mail);
                }
                else
                    MessageBox.Show("Last Name " + textBox1.Text + " isn't in the list");
            }
            else if (radioButton3.Checked)
            {
                index = CasesList.FindIndex(c => c.Mail == textBox1.Text);
                if (index != -1)
                {
                    listBox1.SetSelected(index, true);
                    MessageBox.Show(CasesList[index].Name + "  " + CasesList[index].Fname + "  " + CasesList[index].Num + "  " + CasesList[index].Mail);
                }
                else
                    MessageBox.Show("Email " + textBox1.Text + " isn't in the list");
            }
            else
                MessageBox.Show("Choose search type.");
        }

        private void deleteSelectedCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button6_Click(sender, e);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }

        private void showStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button5_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox4.Clear();
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT Count(id), gender FROM Cases GROUP BY Gender";
            cmd.Connection = con;
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                richTextBox4.AppendText("Curently Deseased :");
                while (reader.Read())
                {

                    richTextBox4.AppendText( reader[0].ToString()+ "   "+ (reader[1].ToString() == "M" ? "Males" : "Females") + "   ");


                }
                richTextBox4.AppendText("\r\n");
                reader.Close();
            }
            cmd.CommandText = "SELECT Count(id), Year(Bdate) FROM Cases GROUP BY Year(Bdate)";
            cmd.Connection = con;
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                richTextBox4.AppendText("Curently Deseased by Year of Birth: \r\n");
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        richTextBox4.AppendText(reader[i].ToString()+ "   ");
                    richTextBox4.AppendText("\r\n");

                }
                richTextBox4.AppendText("\r\n");
                reader.Close();
            }
            con.Close();

        }
    }
}
