using DbFormsService;
using DbFormsService.Domain;
using Hotel_Cheban.DialogForms;
using Hotel_Cheban.DialogForms.EditDialogForms;
using Hotel_Cheban.DialogForms.SearchDialogForms;
using Hotel_Cheban.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DbFormsService.GlobalVariables;

namespace Hotel_Cheban
{
    public partial class Form1 : Form
    {
        HotelDBContext _dbContext { get; set; }
        public Form1(HotelDBContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfiguringAllDataGrid();
        }

        /// <summary>
        /// Настройка всех DataGridView
        /// </summary>
        private void ConfiguringAllDataGrid()
        {
            dataGridView1.AutoGenerateColumns = false;

            #region Rooms
            // Добавляем три столбца в DataGridView
            dataGridView1.Columns.Add("Number", "Номер");
            dataGridView1.Columns.Add("IsReserved", "Зарезервированно");
            dataGridView1.Columns.Add("IsTaken", "Занято");

            // Устанавливаем свойство DataPropertyName для каждого столбца
            dataGridView1.Columns["Number"].DataPropertyName = "Number";
            dataGridView1.Columns["IsReserved"].DataPropertyName = "IsReserved";
            dataGridView1.Columns["IsTaken"].DataPropertyName = "IsTaken";

            // Устанавливаем порядок отображения столбцов
            dataGridView1.Columns["Number"].DisplayIndex = 0;
            dataGridView1.Columns["IsReserved"].DisplayIndex = 1;
            dataGridView1.Columns["IsTaken"].DisplayIndex = 2;


            dataGridView1.DataSource = _dbContext.Rooms.ToList() ?? null;
            #endregion

            #region Employees
            // Добавляем три столбца в DataGridView

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.Columns.Add("Name", "Ф.И.О.");
            dataGridView2.Columns.Add("Post", "Должность");

            // Устанавливаем свойство DataPropertyName для каждого столбца
            dataGridView2.Columns["Name"].DataPropertyName = "Name";
            dataGridView2.Columns["Post"].DataPropertyName = "Post";

            // Устанавливаем порядок отображения столбцов
            dataGridView2.Columns["Name"].DisplayIndex = 0;
            dataGridView2.Columns["Post"].DisplayIndex = 1;

            dataGridView2.DataSource = _dbContext.Employees.ToList() ?? null;

            DataGridService.SetDefaultSettings(dataGridView2);
            #endregion

            #region Client
            // Добавляем три столбца в DataGridView

            dataGridView3.AutoGenerateColumns = false;
            dataGridView3.Columns.Add("Name", "Ф.И.О.");
            dataGridView3.Columns.Add("Employee", "Зарегистрировавший сотрудник");

            // Устанавливаем свойство DataPropertyName для каждого столбца
            dataGridView3.Columns["Name"].DataPropertyName = "Name";
            dataGridView3.Columns["Employee"].DataPropertyName = "Employee";

            // Устанавливаем порядок отображения столбцов
            dataGridView3.Columns["Name"].DisplayIndex = 0;
            dataGridView3.Columns["Employee"].DisplayIndex = 1;

            dataGridView3.DataSource = _dbContext.Clients.Include(c => c.Employee).ToList() ?? null;

            DataGridService.SetDefaultSettings(dataGridView3);
            #endregion

            #region Payment

            dataGridView4.AutoGenerateColumns = false;
            dataGridView4.Columns.Add("Id", "Id");
            dataGridView4.Columns.Add("Client", "Ф.И.О. клиента");
            dataGridView4.Columns.Add("IsPaid", "Оплачено");
            dataGridView4.Columns.Add("StartDate", "Дата начала");
            dataGridView4.Columns.Add("EndDate", "Дата окончания");
            dataGridView4.Columns.Add("Room", "Комната");

            // Устанавливаем свойство DataPropertyName для каждого столбца
            dataGridView4.Columns["Id"].DataPropertyName = "Id";
            dataGridView4.Columns["Client"].DataPropertyName = "Client";
            dataGridView4.Columns["IsPaid"].DataPropertyName = "IsPaid";
            dataGridView4.Columns["StartDate"].DataPropertyName = "StartDate";
            dataGridView4.Columns["EndDate"].DataPropertyName = "EndDate";
            dataGridView4.Columns["Room"].DataPropertyName = "Room";

            // Устанавливаем порядок отображения столбцов
            dataGridView4.Columns["Id"].DisplayIndex = 0;
            dataGridView4.Columns["Client"].DisplayIndex = 1;
            dataGridView4.Columns["IsPaid"].DisplayIndex = 2;
            dataGridView4.Columns["StartDate"].DisplayIndex = 3;
            dataGridView4.Columns["EndDate"].DisplayIndex = 4;
            dataGridView4.Columns["Room"].DisplayIndex = 5;

            dataGridView4.DataSource = _dbContext.Payments.Include(p => p.Client).Include(p => p.Room).ToList() ?? null;

            DataGridService.SetDefaultSettings(dataGridView4);

            #endregion
        }

        #region Rooms
        private object GetFreeInfo(DataGridViewCell occupiedCell, ComboBox comboBoxCanBeOccupied)
        {
            var resultFromCell = Convert.ToBoolean(occupiedCell.Value);

            if (resultFromCell)
                return comboBoxCanBeOccupied.Items[0];

            return comboBoxCanBeOccupied.Items[1];
        }

        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var roomEditForm = new RoomEditForm(_dbContext, dataGridView1,StateEdit.Edit);
            roomEditForm.textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            roomEditForm.comboBox1.SelectedItem = GetFreeInfo(dataGridView1.SelectedRows[0].Cells[1], roomEditForm.comboBox1);
            roomEditForm.comboBox2.SelectedItem = GetFreeInfo(dataGridView1.SelectedRows[0].Cells[2], roomEditForm.comboBox2);
            roomEditForm.Show();

        }

        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            var roomSearchForm = new RoomSearchForm(_dbContext);
            roomSearchForm.Show();
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            var number = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            var entityToDel = _dbContext.Rooms.FirstOrDefault(r => r.Number == number);
            if (entityToDel != null)
            {
                _dbContext.Rooms.Remove(entityToDel);
                _dbContext.SaveChanges();
                dataGridView1.DataSource = _dbContext.Rooms.ToList();
            }

        }

        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            var roomAddForm = new RoomEditForm(_dbContext, dataGridView1, StateEdit.Adding);
            roomAddForm.textBox1.ReadOnly = false;
            roomAddForm.Show();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "IsTaken")
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


            if (dataGridView1.Columns[e.ColumnIndex].Name == "IsReserved")
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

        #endregion

        #region Employees
        /// <summary>
        /// Редактировать сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            var emplEditForm = new EmployeeEditForm(_dbContext, dataGridView2, StateEdit.Edit);
            emplEditForm.textBox1.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            emplEditForm.comboBox1.SelectedItem = GetPostInfo(dataGridView2.SelectedRows[0].Cells[1], emplEditForm.comboBox1);
            emplEditForm.Show();
        }

        private object GetPostInfo(DataGridViewCell occupiedCell, ComboBox comboBoxPost)
        {
            var resultFromCell = occupiedCell.Value;

            if (resultFromCell != null)
                return resultFromCell;
            return null;

        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            var name = dataGridView2.SelectedRows[0].Cells[0].Value;
            var entityToDel = _dbContext.Employees.FirstOrDefault(r => r.Name == name);
            if (entityToDel != null)
            {
                _dbContext.Employees.Remove(entityToDel);
                _dbContext.SaveChanges();
                dataGridView2.DataSource = _dbContext.Employees.ToList();
            }
        }

        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            var empAddForm = new EmployeeEditForm(_dbContext, dataGridView2, StateEdit.Adding);
            empAddForm.textBox1.ReadOnly = false;
            empAddForm.Show();
        }

        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            var empSearchForm = new EmployeeSearchForm(_dbContext);
            empSearchForm.Show();
        }

        #endregion

        #region Clients
        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            var clietnEditForm = new ClientEditForm(_dbContext, dataGridView3, StateEdit.Edit);
            clietnEditForm.textBox1.Text = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            clietnEditForm.comboBox1.SelectedItem = dataGridView3.SelectedRows[0]?.Cells[1]?.Value?.ToString();
            clietnEditForm.Show();
        }


        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView3.Columns["Employee"].Index && e.Value != null)
            {
                var employee = (Employee)e.Value;
                e.Value = employee.Name;
                e.FormattingApplied = true;
            }
        }

        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            var clietnEditForm = new ClientEditForm(_dbContext, dataGridView3, StateEdit.Adding);
            clietnEditForm.textBox1.ReadOnly = false;
            clietnEditForm.Show();
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            var name = dataGridView3.SelectedRows[0].Cells[0].Value;
            var entityToDel = _dbContext.Clients.FirstOrDefault(r => r.Name == name);
            if (entityToDel != null)
            {
                _dbContext.Clients.Remove(entityToDel);
                _dbContext.SaveChanges();
                dataGridView3.DataSource = _dbContext.Clients.Include(emp => emp.Employee).ToList();
            }
        }

        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            var clientSearchForm = new ClientSearchForm(_dbContext);
            clientSearchForm.Show();
        }


        #endregion

        #region Payments
        private void dataGridView4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView4.Columns["Client"].Index && e.Value != null)
            {
                var client = (Client)e.Value;
                e.Value = client.Name;
                e.FormattingApplied = true;
            }

            if (e.ColumnIndex == dataGridView4.Columns["Room"].Index && e.Value != null)
            {
                var room = (Room)e.Value;
                e.Value = room.Number.ToString();
                e.FormattingApplied = true;
            }

            if (e.ColumnIndex == dataGridView4.Columns["IsPaid"].Index && e.Value != null)
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

        private void button14_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Создать оплату.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button16_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0 && dataGridView3.DataSource != null)
            {
                var addingPaymentDialog = new CreatePaymentDialogForm(_dbContext);
                addingPaymentDialog.textBox2.Text = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                addingPaymentDialog.Show();
            }
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(dataGridView4.SelectedRows[0].Cells[0].Value);
            var entityToDel = _dbContext.Payments.FirstOrDefault(r => r.Id == id);
            if (entityToDel != null)
            {
                _dbContext.Payments.Remove(entityToDel);
                _dbContext.SaveChanges();
                dataGridView4.DataSource = _dbContext.Payments.Include(p => p.Client).ToList();
            }
        }

        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            var paymentSearchForm = new PaymentSearchForm(_dbContext);
            paymentSearchForm.Show();
        }
        
        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click_1(object sender, EventArgs e)
        {
            dataGridView4.DataSource = _dbContext.Payments.Include(p => p.Client).Include(p => p.Room).ToList();
        }
        #endregion

        /// <summary>
        /// Не занятые комнаты на дату
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            var freeNumbers = _dbContext.Payments.Where(p => dateTimePicker1.Value >= p.StartDate && dateTimePicker1.Value <= p.EndDate).Select(p => p.Room.Number).ToList();
            var rooms = _dbContext.Rooms.Where(r => !freeNumbers.Any(n => n == r.Number)).ToList();

            dataGridView1.DataSource = rooms;
        }
    }
}
