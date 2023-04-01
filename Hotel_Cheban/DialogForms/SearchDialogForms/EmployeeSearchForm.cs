using DbFormsService;
using DbFormsService.Domain;
using Hotel_Cheban.DialogForms.EditDialogForms;
using Hotel_Cheban.Services;
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
    public partial class EmployeeSearchForm : Form
    {
        HotelDBContext _dbContext { get; set; }
        public EmployeeSearchForm(HotelDBContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;

        }

        private void EmployeeSearchForm_Load(object sender, EventArgs e)
        {
            DataGridService.SetDefaultSettings(dataGridView1);


            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("Name", "Ф.И.О.");
            dataGridView1.Columns.Add("Post", "Должность");

            // Устанавливаем свойство DataPropertyName для каждого столбца
            dataGridView1.Columns["Name"].DataPropertyName = "Name";
            dataGridView1.Columns["Post"].DataPropertyName = "Post";

            // Устанавливаем порядок отображения столбцов
            dataGridView1.Columns["Name"].DisplayIndex = 0;
            dataGridView1.Columns["Post"].DisplayIndex = 1;

            button6.Enabled = false;
        }

        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                var result = _dbContext.Employees.FirstOrDefault(r => r.Name == textBox1.Text);

                if (result != null)
                    dataGridView1.DataSource = new List<Employee>() { result };

            }

            DataGridService.CheckNotEmptyDataGrid(dataGridView1,button6);
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

        /// <summary>
        /// Редактировать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                var emplEditForm = new EmployeeEditForm(_dbContext, dataGridView1, StateEdit.Edit);
                emplEditForm.textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                emplEditForm.comboBox1.SelectedItem = GetPostInfo(dataGridView1.SelectedRows[0].Cells[1], emplEditForm.comboBox1);
                emplEditForm.Show();
            }
        }
        private object GetPostInfo(DataGridViewCell occupiedCell, ComboBox comboBoxPost)
        {
            var resultFromCell = occupiedCell.Value;

            if (resultFromCell != null)
                return resultFromCell;
            return null;

        }

    }
}
