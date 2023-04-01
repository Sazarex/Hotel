using DbFormsService;
using DbFormsService.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DbFormsService.GlobalVariables;

namespace Hotel_Cheban.DialogForms.EditDialogForms
{
    public partial class EmployeeEditForm : Form
    {
        HotelDBContext _dbContext { get; set; }
        StateEdit _stateEdit { get; set; }
        DataGridView _dataGrid { get; set; }
        public EmployeeEditForm(HotelDBContext dbContext, DataGridView dataGrid, StateEdit state)
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(Post));
            comboBox1.DisplayMember = "ToString";

            _stateEdit = state;

            if (_stateEdit == StateEdit.Edit)
                this.Text = "Редактирование";
            else
                this.Text = "Добавление";

            _dataGrid = dataGrid;
            _dbContext = dbContext;

        }

        private void SaveChangesRefreshGrid(DataGridView dataGrid)
        {
            _dbContext.SaveChanges();
            dataGrid.DataSource = _dbContext.Employees.ToList();
        }

        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {

            var empExists = _dbContext.Employees.Any(r => r.Name == textBox1.Text);
            if (_stateEdit == StateEdit.Edit && empExists)
            {
                var employee = _dbContext.Employees.FirstOrDefault(r => r.Name == textBox1.Text);
                var post = comboBox1.SelectedItem;

                if (post is Post post1)
                {
                    employee.Post = post1;
                }

                SaveChangesRefreshGrid(_dataGrid);
            }
            else if (_stateEdit == StateEdit.Adding && !empExists && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                var newEmployee = new Employee()
                {
                    Name = textBox1.Text,
                };

                var post = comboBox1.SelectedItem;

                if (post is Post post1)
                {
                    newEmployee.Post = post1;
                }

                _dbContext.Employees.Add(newEmployee);
                SaveChangesRefreshGrid(_dataGrid);
            }
            else
            {
                MessageBox.Show("Такой сотрудник уже существует, либо неверно введно имя.");
            }


            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
