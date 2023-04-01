using DbFormsService;
using DbFormsService.Domain;
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
    public partial class PaymentSearchForm : Form
    {
        HotelDBContext _dbContext { get; set; }
        public PaymentSearchForm(HotelDBContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;

            DataGridService.SetDefaultSettings(dataGridView1);


            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns.Add("Client", "Ф.И.О. клиента");
            dataGridView1.Columns.Add("IsPaid", "Оплачено");

            // Устанавливаем свойство DataPropertyName для каждого столбца
            dataGridView1.Columns["Id"].DataPropertyName = "Id";
            dataGridView1.Columns["Client"].DataPropertyName = "Client";
            dataGridView1.Columns["IsPaid"].DataPropertyName = "IsPaid";

            // Устанавливаем порядок отображения столбцов
            dataGridView1.Columns["Id"].DisplayIndex = 0;
            dataGridView1.Columns["Client"].DisplayIndex = 1;
            dataGridView1.Columns["IsPaid"].DisplayIndex = 2;

            button6.Enabled = false;
        }

        private void PaymentSearchForm_Load(object sender, EventArgs e)
        {

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
                var result = _dbContext.Payments.Include(p => p.Client).Where(r => r.Client.Name == textBox1.Text).ToList();

                if (result != null && result.Count > 0)
                    dataGridView1.DataSource = result;
                else
                    dataGridView1.DataSource = null;

            }

            DataGridService.CheckNotEmptyDataGrid(dataGridView1, button6);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Client"].Index && e.Value != null)
            {
                var client = (Client)e.Value;
                e.Value = client.Name;
                e.FormattingApplied = true;
            }


            if (e.ColumnIndex == dataGridView1.Columns["IsPaid"].Index && e.Value != null)
            {
                if (e.Value != null && e.Value.ToString() == "False")
                {
                    e.Value = "нет";
                    e.FormattingApplied = true;
                }


                if (e.Value != null && e.Value.ToString() == "True")
                {
                    e.Value = "да";
                    e.FormattingApplied = true;
                }
            }
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            var entityToDel = _dbContext.Payments.FirstOrDefault(r => r.Id == id);
            if (entityToDel != null)
            {
                _dbContext.Payments.Remove(entityToDel);
                _dbContext.SaveChanges();
                dataGridView1.DataSource = _dbContext.Payments.Include(p => p.Client).ToList();
            }
        }

        /// <summary>
        /// Закрыть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
