using DbFormsService;
using DbFormsService.Domain;
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

namespace Hotel_Cheban.DialogForms.EditDialogForms
{
    public partial class ClientEditForm : Form
    {
        HotelDBContext _dbContext { get; set; }
        StateEdit _stateEdit { get; set; }
        DataGridView _dataGrid { get; set; }
        public ClientEditForm(HotelDBContext dbContext, DataGridView dataGrid, StateEdit state)
        {
            InitializeComponent();


            _stateEdit = state;

            if (_stateEdit == StateEdit.Edit)
                this.Text = "Редактирование";
            else
                this.Text = "Добавление";

            _dataGrid = dataGrid;
            _dbContext = dbContext;

            var employees = _dbContext.Employees.Select(e => e.Name).ToList();
            comboBox1.DataSource = employees;

        }

        private void ClientEditForm_Load(object sender, EventArgs e)
        {

        }
        private void SaveChangesRefreshGrid(DataGridView dataGrid)
        {
            _dbContext.SaveChanges();
            dataGrid.DataSource = _dbContext.Clients.Include(c => c.Employee).ToList();
        }

        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var clientExists = _dbContext.Clients.Any(r => r.Name == textBox1.Text);
            if (_stateEdit == StateEdit.Edit && clientExists)
            {
                var client = _dbContext.Clients.FirstOrDefault(r => r.Name == textBox1.Text);

                var employee = comboBox1.SelectedItem;
                if (employee != null)
                {
                    var employeeFromDb = _dbContext.Employees.FirstOrDefault(emp => emp.Name == employee.ToString());
                    client.Employee = employeeFromDb;
                }

                SaveChangesRefreshGrid(_dataGrid);
            }
            else if (_stateEdit == StateEdit.Adding && !clientExists && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                var newClient = new Client()
                {
                    Name = textBox1.Text,
                };

                var employee = comboBox1.SelectedItem;
                if (employee != null)
                {
                    var employeeFromDb = _dbContext.Employees.FirstOrDefault(emp => emp.Name == employee.ToString());
                    newClient.Employee = employeeFromDb;
                }

                _dbContext.Clients.Add(newClient);
                SaveChangesRefreshGrid(_dataGrid);
            }
            else
            {
                MessageBox.Show("Такой клиент уже существует, либо неверно введно имя.");
            }


            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
