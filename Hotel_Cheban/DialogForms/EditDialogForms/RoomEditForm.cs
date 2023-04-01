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
    public enum StateEdit
    {
        Edit,
        Adding
    }
    public partial class RoomEditForm : Form
    {
        HotelDBContext _dbContext { get; set; }
        StateEdit _stateEdit { get; set; }
        DataGridView _dataGrid { get; set; }
        public RoomEditForm(HotelDBContext dbContext, DataGridView dataGrid, StateEdit state)
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(Occupied));
            comboBox1.DisplayMember = "ToString";

            comboBox2.DataSource = Enum.GetValues(typeof(Occupied));
            comboBox2.DisplayMember = "ToString";

            _stateEdit = state;

            if (_stateEdit == StateEdit.Edit)
                this.Text = "Редактирование";
            else
                this.Text = "Добавление";

            _dataGrid = dataGrid;
            _dbContext = dbContext;
        }

        private void RoomEditForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пустое поле номера.");

            }
            else
            {

                var roomExists = _dbContext.Rooms.Any(r => r.Number == Convert.ToInt32(textBox1.Text));
                if (_stateEdit == StateEdit.Edit && roomExists)
                {
                    var room = _dbContext.Rooms.FirstOrDefault(r => r.Number == Convert.ToInt32(textBox1.Text));
                    room.IsReserved = GetBoolOccupied(comboBox1);
                    room.IsTaken = GetBoolOccupied(comboBox2);
                    SaveChangesRefreshGrid(_dataGrid);
                }
                else if (_stateEdit == StateEdit.Adding && !roomExists && !string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    var newRoom = new Room()
                    {
                        IsReserved = GetBoolOccupied(comboBox1),
                        IsTaken = GetBoolOccupied(comboBox2),
                        Number = Convert.ToInt32(textBox1.Text)
                    };
                    _dbContext.Rooms.Add(newRoom);
                    SaveChangesRefreshGrid(_dataGrid);
                }
                else
                {
                    MessageBox.Show("Такой номер уже существует.");
                }
            }
                
                
            this.Close();
        }

        private bool GetBoolOccupied(ComboBox comboBox)
        {
            var result = comboBox.SelectedValue.ToString();
            if (result == Occupied.Занято.ToString())
                return true;
            else
                return false;

        }

        /// <summary>
        /// Закрыть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveChangesRefreshGrid(DataGridView dataGrid)
        {
            _dbContext.SaveChanges();
            dataGrid.DataSource = _dbContext.Rooms.ToList();
        }
    }
}
