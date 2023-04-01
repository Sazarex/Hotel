using DbFormsService;
using DbFormsService.Domain;
using Hotel_Cheban.DialogForms.EditDialogForms;
using Hotel_Cheban.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Cheban.DialogForms.SearchDialogForms
{
    public partial class ClientSearchForm : Form
    {

        HotelDBContext _dbContext { get; set; }
        public ClientSearchForm(HotelDBContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;

            DataGridService.SetDefaultSettings(dataGridView1);


            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("Name", "Ф.И.О.");
            dataGridView1.Columns.Add("Employee", "Зарегистрировавший сотрудник");

            // Устанавливаем свойство DataPropertyName для каждого столбца
            dataGridView1.Columns["Name"].DataPropertyName = "Name";
            dataGridView1.Columns["Employee"].DataPropertyName = "Employee";

            // Устанавливаем порядок отображения столбцов
            dataGridView1.Columns["Name"].DisplayIndex = 0;
            dataGridView1.Columns["Employee"].DisplayIndex = 1;

            button6.Enabled = false;
        }

        /// <summary>
        /// Найти
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                var result = _dbContext.Clients.Include(c => c.Employee).FirstOrDefault(r => r.Name == textBox1.Text);

                if (result != null)
                    dataGridView1.DataSource = new List<Client>() { result };

            }

            DataGridService.CheckNotEmptyDataGrid(dataGridView1, button6);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Employee"].Index && e.Value != null)
            {
                var employee = (Employee)e.Value;
                e.Value = employee.Name;
                e.FormattingApplied = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Редактировать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {

            if (dataGridView1.DataSource != null)
            {
                var clietnEditForm = new ClientEditForm(_dbContext, dataGridView1, StateEdit.Edit);
                clietnEditForm.textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                clietnEditForm.comboBox1.SelectedItem = dataGridView1.SelectedRows[0]?.Cells[1]?.Value?.ToString();
                clietnEditForm.Show();
            }
        }

        private void ClientSearchForm_Load(object sender, EventArgs e)
        {

        }
    }
}
