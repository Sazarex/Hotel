using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Cheban.Services
{
    public static class DataGridService
    {
        /// <summary>
        /// Дефолтные значения для датагрид
        /// </summary>
        /// <param name="dataGrid"></param>
        public static void SetDefaultSettings(DataGridView dataGrid)
        {
            dataGrid.AutoGenerateColumns = false;
            dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGrid.MultiSelect = false;
            dataGrid.ReadOnly = true;
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Проверяем на наличие данных в датагрид
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="buttonNeedBlock"></param>
        public static bool CheckNotEmptyDataGrid(DataGridView dataGrid, Button buttonNeedBlock)
        {
            if (dataGrid.Rows.Count == 0)
            {
                buttonNeedBlock.Enabled = false;
                return false;
            }
            else
            {
                buttonNeedBlock.Enabled = true;
                return true;
            }
        }
    }
}
