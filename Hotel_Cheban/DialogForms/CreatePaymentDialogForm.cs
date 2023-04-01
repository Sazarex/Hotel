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

namespace Hotel_Cheban.DialogForms
{
    public partial class CreatePaymentDialogForm : Form
    {
        HotelDBContext _dbContext { get; set; }
        public CreatePaymentDialogForm(HotelDBContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
        }

        private void CreatePaymentDialogForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Отменить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Оформить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckEmptyComponents())
            {
                var isRoomExists = _dbContext.Rooms.Any(r => r.Number == numericUpDown1.Value);
                var isClientExists = _dbContext.Clients.Any(c => c.Name == textBox2.Text);
                var isEmployeeExists = _dbContext.Employees.Any(emp => emp.Name == textBox3.Text);
                var isDateBusy = _dbContext.Payments.Any(p => (dateTimePicker1.Value.CompareTo(p.StartDate.Value) >= 0 && dateTimePicker1.Value.CompareTo(p.EndDate.Value) <= 0) ||
                                                               (dateTimePicker2.Value.CompareTo(p.StartDate.Value) >= 0 && dateTimePicker2.Value.CompareTo(p.EndDate.Value) <= 0));
                
                if (!isRoomExists)
                    MessageBox.Show("Комнаты не существует.");
                else if (!isClientExists)
                    MessageBox.Show("Клиента не существует.");
                else if(!isEmployeeExists)
                    MessageBox.Show("Сотрудника не существует.");
                else if(isDateBusy)
                    MessageBox.Show("Дата занята.");
                else
                {
                    var currentClient = _dbContext.Clients.FirstOrDefault(c => c.Name == textBox2.Text);
                    var roomOfClient = _dbContext.Rooms.FirstOrDefault(c => c.Number == numericUpDown1.Value);
                    
                    var newPayment = new Payment()
                    {
                        Client = currentClient,
                        StartDate = dateTimePicker1.Value,
                        EndDate = dateTimePicker2.Value,
                        IsPaid = true,
                        Room = roomOfClient
                    };
                    _dbContext.Payments.Add(newPayment);
                    _dbContext.SaveChanges();
                    this.Close();
                }
            }

        }

        private bool CheckEmptyComponents()
        {
            if (numericUpDown1.Value <= 0  || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Проверьте введенные данные. Возможно вы ввели пустые значения.");
                return false;
            }
            else if ((dateTimePicker1.Value > dateTimePicker2.Value) || (dateTimePicker1.Value < DateTime.Now))
            {
                MessageBox.Show("Проверьте введенную дату.");
                return false;
            }
            return true;
        }

        private bool IsBusyDate(DateTime? startDate, DateTime? endDate)
        {
            if (startDate != null && endDate != null)
            {
                var result = dateTimePicker1.Value.CompareTo(startDate) >= 0 && dateTimePicker2.Value.CompareTo(endDate) <= 0;
                return result;
            }
            return false;
        }
    }
}
