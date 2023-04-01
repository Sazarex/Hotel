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
    public partial class RoomSearchForm : Form
    {
        HotelDBContext _dbContext { get; set; }
        public RoomSearchForm(HotelDBContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;

        }

        /// <summary>
        /// Свободные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var result = _dbContext.Rooms.Where(r => !r.IsReserved.Value && !r.IsTaken.Value);

            if (result != null)
                dataGridView1.DataSource = result.ToList();

            DataGridService.CheckNotEmptyDataGrid(dataGridView1, button6);
        }

        private void RoomSearchForm_Load(object sender, EventArgs e)
        {

            DataGridService.SetDefaultSettings(dataGridView1);

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

            button6.Enabled = false ;
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
                var result = _dbContext.Rooms.FirstOrDefault(r => r.Number == Convert.ToInt32(textBox1.Text));

                if (result != null)
                    dataGridView1.DataSource = new List<Room>() { result };
            }

            DataGridService.CheckNotEmptyDataGrid(dataGridView1, button6);

        }


        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                var roomEditForm = new RoomEditForm(_dbContext, dataGridView1, StateEdit.Edit);
                roomEditForm.textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                roomEditForm.comboBox1.SelectedItem = GetFreeInfo(dataGridView1.SelectedRows[0].Cells[1], roomEditForm.comboBox1);
                roomEditForm.comboBox2.SelectedItem = GetFreeInfo(dataGridView1.SelectedRows[0].Cells[2], roomEditForm.comboBox2);
                roomEditForm.Show();
            }
        }

        private object GetFreeInfo(DataGridViewCell occupiedCell, ComboBox comboBoxCanBeOccupied)
        {
            var resultFromCell = Convert.ToBoolean(occupiedCell.Value);

            if (resultFromCell)
                return comboBoxCanBeOccupied.Items[0];

            return comboBoxCanBeOccupied.Items[1];
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

        /// <summary>
        /// Зарезервированные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            var result = _dbContext.Rooms.Where(r => r.IsReserved.Value);

            if (result != null)
                dataGridView1.DataSource = result.ToList();

            DataGridService.CheckNotEmptyDataGrid(dataGridView1, button6);
        }

        /// <summary>
        /// Занятые
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

            var result = _dbContext.Rooms.Where(r => r.IsTaken.Value);

            if (result != null)
                dataGridView1.DataSource = result.ToList();

            DataGridService.CheckNotEmptyDataGrid(dataGridView1, button6);
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
