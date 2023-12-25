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

            openFileDialogBaza_GAA.Filter = "Значения, разделенные запятыми(*.csv)|*.csv|Все файлы(*.*)|*.*";
            saveFileDialogBaza_GAA.Filter = "Значения, разделенные запятыми(*.csv)|*.csv|Все файлы(*.*)|*.*";
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
            formInfo.ShowDialog();
        }

        private void FormMain_GAA_Load(object sender, EventArgs e)
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
            dataGridViewBaza_GAA.DataSource = data.Tables["LBaza"];
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogBaza_GAA.ShowDialog();
                openFilePath = openFileDialogBaza_GAA.FileName;

                StreamReader sr = new StreamReader(openFilePath, Encoding.Default);

                string allData = sr.ReadToEnd();
                string[] rows = allData.Split("\r".ToCharArray());

                foreach (string r in rows)
                {
                    string[] items = r.Split(";".ToCharArray());
                    data.Tables["LBaza"].Rows.Add(items);
                }
                this.dataGridViewBaza_GAA.DataSource = data.Tables["LBaza"].DefaultView;

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
                saveFileDialogBaza_GAA.FileName = "OutPutBaza.csv";
                saveFileDialogBaza_GAA.InitialDirectory = Directory.GetCurrentDirectory();
                saveFileDialogBaza_GAA.ShowDialog();

                string path = saveFileDialogBaza_GAA.FileName;

                FileInfo fileInfo = new FileInfo(path);
                bool fileExists = fileInfo.Exists;

                if (fileExists)
                {
                    File.Delete(path);
                }

                int rows = dataGridViewBaza_GAA.RowCount;
                int columns = dataGridViewBaza_GAA.ColumnCount;

                string str = "";

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (j != columns - 1)
                        {
                            str = str + dataGridViewBaza_GAA.Rows[i].Cells[j].Value + ";";
                        }
                        else
                        {
                            str = str + dataGridViewBaza_GAA.Rows[i].Cells[j].Value;
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

        private void dataGridViewBaza_GAA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
                DataGridViewRow row = dataGridViewBaza_GAA.Rows[index];
                textBoxYear_GAA.Text = row.Cells[0].Value.ToString();
                textBoxAuthor_GAA.Text = row.Cells[1].Value.ToString();
                textBoxBookName_GAA.Text = row.Cells[2].Value.ToString();
                textBoxPrice_GAA.Text = row.Cells[3].Value.ToString();
                textBoxFIO_GAA.Text = row.Cells[4].Value.ToString();
                textBoxNumberTicket_GAA.Text = row.Cells[5].Value.ToString();
                textBoxDataGet_GAA.Text = row.Cells[6].Value.ToString();
                textBoxDataGive_GAA.Text = row.Cells[7].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Область не редактируется!", "Внимание!");
            }
        }

        private void buttonAdd_GAA_Click(object sender, EventArgs e)
        {
            for (int item = 0; item < dataGridViewBaza_GAA.Rows.Count; item++)
            {
                if (textBoxNumberTicket_GAA.Text == dataGridViewBaza_GAA.Rows[item].Cells[5].Value.ToString())
                {
                    MessageBox.Show("Номер билета не может быть одинаковым!", "Внимание!");
                    return;
                }
            }
            try
            {
                if (textBoxYear_GAA.Text == "" ||
                    textBoxAuthor_GAA.Text == "" ||
                    textBoxBookName_GAA.Text == "" ||
                    textBoxPrice_GAA.Text == "" ||
                    textBoxFIO_GAA.Text == "" ||
                    textBoxNumberTicket_GAA.Text == "" ||
                    textBoxDataGet_GAA.Text == "" ||
                    textBoxDataGive_GAA.Text == "")
                {
                    MessageBox.Show("Не все данные введены!", "Внимание!");
                    return;
                }
                else
                {
                    table.Rows.Add(textBoxYear_GAA.Text, textBoxAuthor_GAA.Text, textBoxBookName_GAA.Text, textBoxPrice_GAA.Text, textBoxFIO_GAA.Text, textBoxNumberTicket_GAA.Text, textBoxDataGet_GAA.Text, textBoxDataGive_GAA.Text);
                    MessageBox.Show("Данные добавлены!", "Внимание!");
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат ввода!", "Внимание!");
            }
        }

        private void buttonClear_GAA_Click(object sender, EventArgs e)
        {
            textBoxYear_GAA.Text = String.Empty;
            textBoxAuthor_GAA.Text = String.Empty;
            textBoxBookName_GAA.Text = String.Empty;
            textBoxPrice_GAA.Text = String.Empty;
            textBoxFIO_GAA.Text = String.Empty;
            textBoxNumberTicket_GAA.Text = String.Empty;
            textBoxDataGet_GAA.Text = String.Empty;
            textBoxDataGive_GAA.Text = String.Empty;
        }

        private void buttonRemove_GAA_Click(object sender, EventArgs e)
        {
            if (dataGridViewBaza_GAA.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }
            index = dataGridViewBaza_GAA.CurrentCell.RowIndex;
            dataGridViewBaza_GAA.Rows.RemoveAt(index);
        }

        private void buttonSearch_GAA_Click(object sender, EventArgs e)
        {
            DataView author = table.DefaultView;
            author.RowFilter = $"Автор Like '%{textBoxSearch_GAA.Text}%'";
        }

        private void buttonUpdate_GAA_Click(object sender, EventArgs e)
        {
            if (dataGridViewBaza_GAA.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }
            if (textBoxYear_GAA.Text == "" ||
                   textBoxAuthor_GAA.Text == "" ||
                   textBoxBookName_GAA.Text == "" ||
                   textBoxPrice_GAA.Text == "" ||
                   textBoxFIO_GAA.Text == "" ||
                   textBoxNumberTicket_GAA.Text == "" ||
                   textBoxDataGet_GAA.Text == "" ||
                   textBoxDataGive_GAA.Text == "")
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }
            else
            {
                DataGridViewRow newdata = dataGridViewBaza_GAA.Rows[index];
                newdata.Cells[0].Value = textBoxYear_GAA.Text;
                newdata.Cells[1].Value = textBoxAuthor_GAA.Text;
                newdata.Cells[2].Value = textBoxBookName_GAA.Text;
                newdata.Cells[3].Value = textBoxPrice_GAA.Text;
                newdata.Cells[4].Value = textBoxFIO_GAA.Text;
                newdata.Cells[5].Value = textBoxNumberTicket_GAA.Text;
                newdata.Cells[6].Value = textBoxDataGet_GAA.Text;
                newdata.Cells[7].Value = textBoxDataGive_GAA.Text;
                MessageBox.Show("Данные добавлены!", "Внимание!");
            }

        }

        private void buttonGraphic_GAA_Click(object sender, EventArgs e)
        {
            try
            {
                if(radioButtonGist_GAA.Checked)
                {
                    for (int i = 0; i < dataGridViewBaza_GAA.Rows.Count; i++)
                    {
                        chartGraphic_GAA.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                        this.chartGraphic_GAA.ChartAreas[0].AxisX.Title = "Год издания";
                        this.chartGraphic_GAA.ChartAreas[0].AxisY.Title = "Цена";

                        int x = Convert.ToInt32(dataGridViewBaza_GAA.Rows[i].Cells["Год_издания"].Value);
                        int y = Convert.ToInt32(dataGridViewBaza_GAA.Rows[i].Cells["Цена_р."].Value);
                        chartGraphic_GAA.Series[0].Points.AddXY(x, y);

                    }
                }

                if (radioButtonFunct_GAA.Checked)
                {
                    for (int i = 0; i < dataGridViewBaza_GAA.Rows.Count; i++)
                    {
                        chartGraphic_GAA.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                        this.chartGraphic_GAA.ChartAreas[0].AxisX.Title = "Автор";
                        this.chartGraphic_GAA.ChartAreas[0].AxisY.Title = "Кол-во книг";

                        Dictionary<string, int> ExceptionMessages = new Dictionary<string, int>();

                        ExceptionMessages.Add(Convert.ToString(dataGridViewBaza_GAA.Rows[i].Cells["Автор"].Value), 20);

                        foreach (KeyValuePair<string, int> exception in ExceptionMessages)
                            chartGraphic_GAA.Series[0].Points.AddXY(exception.Key, exception.Value);

                    }
                }
                if (radioButtonDiag_GAA.Checked)
                {
                    for (int i = 0; i < dataGridViewBaza_GAA.Rows.Count; i++)
                    {
                        chartGraphic_GAA.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

                        this.chartGraphic_GAA.ChartAreas[0].AxisX.Title = "Год издания";
                        this.chartGraphic_GAA.ChartAreas[0].AxisY.Title = "Цена";

                        int x = Convert.ToInt32(dataGridViewBaza_GAA.Rows[i].Cells["Год_издания"].Value);
                        int y = Convert.ToInt32(dataGridViewBaza_GAA.Rows[i].Cells["Цена_р."].Value);
                        chartGraphic_GAA.Series[0].Points.AddXY(x, y);

                    }
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chartGraphic_GAA_Click(object sender, EventArgs e)
        {

        }
    }
}