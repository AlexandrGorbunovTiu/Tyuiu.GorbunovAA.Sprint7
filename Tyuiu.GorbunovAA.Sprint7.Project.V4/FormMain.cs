using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tyuiu.GorbunovAA.Sprint7.Project.V4.Lib;

using System.IO;

namespace Tyuiu.GorbunovAA.Sprint7.Project.V4
{
    public partial class FormMain : Form
    {
        DataSet data = new DataSet("HBaza");
        DataTable table = new DataTable("LBaza");
        int index;

        public FormMain()
        {
            InitializeComponent();

            openFileDialogBaza_ZDA.Filter = "Значения, разделенные запятыми(*.csv)|*.csv|Все файлы(*.*)|*.*";
            saveFileDialogBaza_ZDA.Filter = "Значения, разделенные запятыми(*.csv)|*.csv|Все файлы(*.*)|*.*";
        }

        static string openFilePath;

        DataService ds = new DataService();

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void справочнаяИнформацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInfo formInfo = new FormInfo();
        }

        private void FormMain_ZDA_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Год_издания", typeof(int));
            table.Columns.Add("Автор", typeof(string));
            table.Columns.Add("Название", typeof(string));
            table.Columns.Add("Цена_р.", typeof(int));
            table.Columns.Add("ФИО", typeof(string));
            table.Columns.Add("Номер_чит.билета", typeof(string));
            table.Columns.Add("Дата_выдачи", typeof(string));
            table.Columns.Add("Дата_сдачи", typeof(string));
            data.Tables.Add(table);
            dataGridViewBaza_ZDA.DataSource = data.Tables["LBaza"];
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogBaza_ZDA.ShowDialog();
                openFilePath = openFileDialogBaza_ZDA.FileName;

                StreamReader sr = new StreamReader(openFilePath, Encoding.Default);

                string allData = sr.ReadToEnd();
                string[] rows = allData.Split("\r".ToCharArray());

                foreach (string r in rows)
                {
                    string[] items = r.Split(";".ToCharArray());
                    data.Tables["LBaza"].Rows.Add(items);
                }
                this.dataGridViewBaza_ZDA.DataSource = data.Tables["LBaza"].DefaultView;

            }
            catch
            {
                MessageBox.Show("Ошибка", "Что-то пошло не так", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialogBaza_ZDA.FileName = "OutPutBaza.csv";
                saveFileDialogBaza_ZDA.InitialDirectory = Directory.GetCurrentDirectory();
                saveFileDialogBaza_ZDA.ShowDialog();

                string path = saveFileDialogBaza_ZDA.FileName;

                FileInfo fileInfo = new FileInfo(path);
                bool fileExists = fileInfo.Exists;

                if (fileExists)
                {
                    File.Delete(path);
                }

                int rows = dataGridViewBaza_ZDA.RowCount;
                int columns = dataGridViewBaza_ZDA.ColumnCount;

                string str = "";

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (j != columns - 1)
                        {
                            str = str + dataGridViewBaza_ZDA.Rows[i].Cells[j].Value + ";";
                        }
                        else
                        {
                            str = str + dataGridViewBaza_ZDA.Rows[i].Cells[j].Value;
                        }
                    }
                    File.AppendAllText(path, str + Environment.NewLine);
                    str = "";
                }
                MessageBox.Show("Данные сохранены", "Внимание!");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохраниении", "Внимание!");
            }
        }

        private void dataGridViewBaza_ZDA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
                DataGridViewRow row = dataGridViewBaza_ZDA.Rows[index];
                textBoxYear_ZDA.Text = row.Cells[0].Value.ToString();
                textBoxAuthor_ZDA.Text = row.Cells[1].Value.ToString();
                textBoxBookName_ZDA.Text = row.Cells[2].Value.ToString();
                textBoxPrice_ZDA.Text = row.Cells[3].Value.ToString();
                textBoxFIO_ZDA.Text = row.Cells[4].Value.ToString();
                textBoxNumberTicket_ZDA.Text = row.Cells[5].Value.ToString();
                textBoxDataGet_ZDA.Text = row.Cells[6].Value.ToString();
                textBoxDataGive_ZDA.Text = row.Cells[7].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Область не редактируется!", "Внимание!");
            }
        }

        private void buttonAdd_ZDA_Click(object sender, EventArgs e)
        {
            for (int item = 0; item < dataGridViewBaza_ZDA.Rows.Count; item++)
            {
                if (textBoxNumberTicket_ZDA.Text == dataGridViewBaza_ZDA.Rows[item].Cells[5].Value.ToString())
                {
                    MessageBox.Show("Номер билета не может быть одинаковым!", "Внимание!");
                    return;
                }
            }
            try
            {
                if (textBoxYear_ZDA.Text == "" ||
                    textBoxAuthor_ZDA.Text == "" ||
                    textBoxBookName_ZDA.Text == "" ||
                    textBoxPrice_ZDA.Text == "" ||
                    textBoxFIO_ZDA.Text == "" ||
                    textBoxNumberTicket_ZDA.Text == "" ||
                    textBoxDataGet_ZDA.Text == "" ||
                    textBoxDataGive_ZDA.Text == "")
                {
                    MessageBox.Show("Не все данные введены!", "Внимание!");
                    return;
                }
                else
                {
                    table.Rows.Add(textBoxYear_ZDA.Text, textBoxAuthor_ZDA.Text, textBoxBookName_ZDA.Text, textBoxPrice_ZDA.Text, textBoxFIO_ZDA.Text, textBoxNumberTicket_ZDA.Text, textBoxDataGet_ZDA.Text, textBoxDataGive_ZDA.Text);
                    MessageBox.Show("Данные добавлены!", "Внимание!");
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат ввода!", "Внимание!");
            }
        }

        private void buttonClear_ZDA_Click(object sender, EventArgs e)
        {
            textBoxYear_ZDA.Text = String.Empty;
            textBoxAuthor_ZDA.Text = String.Empty;
            textBoxBookName_ZDA.Text = String.Empty;
            textBoxPrice_ZDA.Text = String.Empty;
            textBoxFIO_ZDA.Text = String.Empty;
            textBoxNumberTicket_ZDA.Text = String.Empty;
            textBoxDataGet_ZDA.Text = String.Empty;
            textBoxDataGive_ZDA.Text = String.Empty;
        }

        private void buttonRemove_ZDA_Click(object sender, EventArgs e)
        {
            if (dataGridViewBaza_ZDA.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }
            index = dataGridViewBaza_ZDA.CurrentCell.RowIndex;
            dataGridViewBaza_ZDA.Rows.RemoveAt(index);
        }

        private void buttonSearch_ZDA_Click(object sender, EventArgs e)
        {
            DataView author = table.DefaultView;
            author.RowFilter = $"Автор Like '%{textBoxSearch_ZDA.Text}%'";
        }

        private void buttonUpdate_ZDA_Click(object sender, EventArgs e)
        {
            if (dataGridViewBaza_ZDA.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }
            if (textBoxYear_ZDA.Text == "" ||
                   textBoxAuthor_ZDA.Text == "" ||
                   textBoxBookName_ZDA.Text == "" ||
                   textBoxPrice_ZDA.Text == "" ||
                   textBoxFIO_ZDA.Text == "" ||
                   textBoxNumberTicket_ZDA.Text == "" ||
                   textBoxDataGet_ZDA.Text == "" ||
                   textBoxDataGive_ZDA.Text == "")
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }
            else
            {
                DataGridViewRow newdata = dataGridViewBaza_ZDA.Rows[index];
                newdata.Cells[0].Value = textBoxYear_ZDA.Text;
                newdata.Cells[1].Value = textBoxAuthor_ZDA.Text;
                newdata.Cells[2].Value = textBoxBookName_ZDA.Text;
                newdata.Cells[3].Value = textBoxPrice_ZDA.Text;
                newdata.Cells[4].Value = textBoxFIO_ZDA.Text;
                newdata.Cells[5].Value = textBoxNumberTicket_ZDA.Text;
                newdata.Cells[6].Value = textBoxDataGet_ZDA.Text;
                newdata.Cells[7].Value = textBoxDataGive_ZDA.Text;
                MessageBox.Show("Данные добавлены!", "Внимание!");
            }

        }

        private void buttonGraphic_ZDA_Click(object sender, EventArgs e)
        {
            try
            {
                if(radioButtonGist_ZDA.Checked)
                {
                    for (int i = 0; i < dataGridViewBaza_ZDA.Rows.Count; i++)
                    {
                        chartGraphic_ZDA.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                        this.chartGraphic_ZDA.ChartAreas[0].AxisX.Title = "Год издания";
                        this.chartGraphic_ZDA.ChartAreas[0].AxisY.Title = "Цена";

                        int x = Convert.ToInt32(dataGridViewBaza_ZDA.Rows[i].Cells["Год_издания"].Value);
                        int y = Convert.ToInt32(dataGridViewBaza_ZDA.Rows[i].Cells["Цена_р."].Value);
                        chartGraphic_ZDA.Series[0].Points.AddXY(x, y);

                    }
                }

                if (radioButtonFunct_ZDA.Checked)
                {
                    for (int i = 0; i < dataGridViewBaza_ZDA.Rows.Count; i++)
                    {
                        chartGraphic_ZDA.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                        this.chartGraphic_ZDA.ChartAreas[0].AxisX.Title = "Автор";
                        this.chartGraphic_ZDA.ChartAreas[0].AxisY.Title = "Кол-во книг";

                        Dictionary<string, int> ExceptionMessages = new Dictionary<string, int>();

                        ExceptionMessages.Add(Convert.ToString(dataGridViewBaza_ZDA.Rows[i].Cells["Автор"].Value), 20);

                        foreach (KeyValuePair<string, int> exception in ExceptionMessages)
                            chartGraphic_ZDA.Series[0].Points.AddXY(exception.Key, exception.Value);

                    }
                }
                if (radioButtonDiag_ZDA.Checked)
                {
                    for (int i = 0; i < dataGridViewBaza_ZDA.Rows.Count; i++)
                    {
                        chartGraphic_ZDA.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

                        this.chartGraphic_ZDA.ChartAreas[0].AxisX.Title = "Год издания";
                        this.chartGraphic_ZDA.ChartAreas[0].AxisY.Title = "Цена";

                        int x = Convert.ToInt32(dataGridViewBaza_ZDA.Rows[i].Cells["Год_издания"].Value);
                        int y = Convert.ToInt32(dataGridViewBaza_ZDA.Rows[i].Cells["Цена_р."].Value);
                        chartGraphic_ZDA.Series[0].Points.AddXY(x, y);

                    }
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chartGraphic_ZDA_Click(object sender, EventArgs e)
        {

        }
    }
}