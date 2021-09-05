using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Dapper;

namespace PasswordKeeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int selectedRow = 0;
        string selectedIndex = null;
        private void Form1_Load(object sender, EventArgs e)
        {

            DBManager dBManager = new DBManager();
            dBManager.CreateDatabase(); 
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            fillTable();

                
         }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(btnSave.Text.ToString() == "Save")
            {

                if ((textAlias.Text.ToString() != "") && (textPassword.Text.ToString() != "") && (textUsername.Text.ToString() != ""))
                {
                    dataGrid.DataSource = null;
                    DBManager dBManager = new DBManager();
                    dBManager.InsertData(textAlias.Text.ToString(), textUsername.Text.ToString(), textPassword.Text.ToString());

                    ClearScreen();
                    RefreshTable();
                }
                else
                {
                    MessageBox.Show("Does not acceptable empty field!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if( btnSave.Text.ToString() == "Update")
            {
                dataGrid.DataSource = null;
                DBManager dBManager = new DBManager();
                dBManager.UpdateData(selectedIndex ,textAlias.Text.ToString(), textPassword.Text.ToString(), textUsername.Text.ToString());

                ClearScreen();
                RefreshTable();
            }

        }

        private void fillTable()
        {
            SQLiteConnection Connection;
            string dbName = "database";
            string dbFilePath = "./data/" + dbName.ToString() + ".pkd";
            Connection = new SQLiteConnection(string.Format(
            "Data Source={0};Version=3;New=true;Compress=True", dbFilePath));
            Connection.Open();

            dataGrid.DataSource = Connection.Query<Keepmypass>("Select * from KEEPMYPASS");
        }

        private void dataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 3 && e.Value != null)
            {
                if (!checkBoxShowAll.Checked)
                {
                    dataGrid.Rows[e.RowIndex].Tag = e.Value;
                    e.Value = new String('\u25CF', e.Value.ToString().Length);
                }
                if (checkBoxShowSelected.Checked)
                {
                    int ix = dataGrid.Rows[e.RowIndex].Index;
                    if (selectedRow != ix)
                    {
                        dataGrid.Rows[e.RowIndex].Tag = e.Value;
                        e.Value = new String('\u25CF', e.Value.ToString().Length);

                    }
                    else
                    {
                        dataGrid.Rows[e.RowIndex].Tag = e.Value;
                        e.Value = dataGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                        dataGrid.Focus();
                        dataGrid.Rows[selectedRow].Selected = true;
                        //dataGrid.CurrentRow.Selected = true;
                    }
                }
            }
        }

        private void checkBoxShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowSelected.Checked)
                checkBoxShowSelected.Checked = false;
            dataGrid.DataSource = null;
            dataGrid.Refresh();
            fillTable();
        }

        private void checkBoxShowSelected_CheckedChanged(object sender, EventArgs e)
        {
            selectedRow = dataGrid.CurrentCell.RowIndex;
            dataGrid.DataSource = null;
            dataGrid.Refresh();
            fillTable();
        }

        private void dataGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textAlias.Text = dataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            textPassword.Text = dataGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
            textUsername.Text = dataGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
            selectedIndex = dataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            btnSave.Text = "Update";
            btnDelete.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            ClearScreen();
            dataGrid.DataSource = null;
            fillTable();
            btnSave.Text = "Save";
            selectedIndex = null;
            if (btnDelete.Enabled)
            {
                btnDelete.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DBManager dBManager = new DBManager();
            dBManager.DeleteEntry(selectedIndex);
            selectedIndex = null;
            dataGrid.DataSource = null;
            ClearScreen();
            RefreshTable();
        }
        public void ClearScreen()
        {
            textAlias.Clear();
            textPassword.Clear();
            textUsername.Clear();
        }
        public void RefreshTable()
        {
            dataGrid.DataSource = null;
            fillTable();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(textAlias.TextLength > 0)
            {
                if (SearchEntry(textAlias.Text) == 0)
                {
                    MessageBox.Show("Does not mached any Alias", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    
                }
            }
        }
        public int SearchEntry(string Alias)
        {
            SQLiteConnection Connection;
            string dbName = "database";
            string dbFilePath = "./data/" + dbName.ToString() + ".pkd";
            Connection = new SQLiteConnection(string.Format(
            "Data Source={0};Version=3;New=true;Compress=True", dbFilePath));
            Connection.Open();

            dataGrid.DataSource = Connection.Query<Keepmypass>("Select * from KEEPMYPASS where Alias = '"+Alias+"'");
            if (dataGrid.DataSource.ToString().Length > 0)
            {
                return 1;
            }
            else
                return 0;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature will be added soon", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
