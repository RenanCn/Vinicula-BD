using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UltimateVinicula
{
    public partial class formColheita : Form
    {

        static string conexaoString = "SERVER=localhost;DATABASE=vinicula;UID=root;PWD=;";
        MySqlConnection conexao = new MySqlConnection(conexaoString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public formColheita()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Name = "ID Colheita";
            dataGridView1.Columns[1].Name = "ID Parreiral";
            dataGridView1.Columns[2].Name = "Dia";
            dataGridView1.Columns[3].Name = "Mês";
            dataGridView1.Columns[4].Name = "Ano";
            dataGridView1.Columns[5].Name = "Material";
            dataGridView1.Columns[6].Name = "Maturação";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //selection mode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }
        
        
        //INSERT INTO DB
        private void add(int idParreiral, int diaColheita, int mesColheita, int anoColheita, string material, string maturacao)
        {
            //sql stmt   
            string sql = "INSERT INTO `colheita` (`idColheita`,`Parreiral_idParreiral`,`diaColheita`,`mesColheita`, `anoColheita`,`Material`,`Maturação`) VALUES(NULL,'" + idParreiral + "', '" + diaColheita + "', '" + mesColheita + "', '" + anoColheita + "','" + material + "','" + maturacao + "')";
            cmd = new MySqlCommand(sql, conexao);

            //open conex and exec insert
            try
            {
                conexao.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                    clearTxts();
                    MessageBox.Show("Inserido com sucesso!");
                }
                conexao.Close();
                retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conexao.Close();
            }
        }

        //ADD TO DGVIEW
        private void populate(int idColheita, int idParreiral, int diaColheita, int mesColheita, int anoColheita, String material, String maturacao)
        {
            dataGridView1.Rows.Add(idColheita, idParreiral, diaColheita , mesColheita, anoColheita, material, maturacao);
        }

        //RETRIEVE FROM DB
        private void retrieve()
        {
            dataGridView1.Rows.Clear();

            //sql stmt
            string sql = "SELECT * FROM colheita";
            cmd = new MySqlCommand(sql, conexao);

            //open conex, retrieve, fill dgview
            try
            {
                conexao.Open();

                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                //loop thru dt
                foreach (DataRow row in dt.Rows)
                {
                    int row0 = Convert.ToInt32(row[0].ToString());
                    int row1 = Convert.ToInt32(row[1].ToString());
                    int row2 = Convert.ToInt32(row[2].ToString());
                    int row3 = Convert.ToInt32(row[3].ToString());
                    int row4 = Convert.ToInt32(row[4].ToString());
                    populate(row0, row1, row2,row3,row4, row[5].ToString(), row[6].ToString());
                }

                conexao.Close();

                //clear dt
                dt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conexao.Close();
            }
        }

        //update db
        private void update(int idParreiral, int diaColheita, int mesColheita, int anoColheita,string material, string maturacao, int idColheita)
        {
            //sql stmt

            //string sql = "UPDATE propriedade SET Nome ='" + nome + "',Administrador='" + administrador + "'WHERE NOME =" + nome + "";
            string sql = "UPDATE `colheita` SET `Parreiral_idParreiral` = '" + idParreiral + "', `diaColheita` = '" + diaColheita + "', `mesColheita` = '" + mesColheita + "', `anoColheita` = '" + anoColheita + "', `Material` = '" + material + "', `Maturação` = '" + maturacao + "' WHERE `colheita`.`idColheita` = '" + idColheita + "'";
            cmd = new MySqlCommand(sql, conexao);

            //open con, updt, retriev dgview
            try
            {
                conexao.Open();
                adapter = new MySqlDataAdapter(cmd);
                adapter.UpdateCommand = conexao.CreateCommand();
                adapter.UpdateCommand.CommandText = sql;

                if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                {
                    clearTxts();
                    MessageBox.Show("Atualizado com sucesso!");

                }
                conexao.Close();

                retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conexao.Close();
            }
        }

        //delete from db
        private void delete(int idColheita)
        {
            //sql stnt
            string sql = "DELETE FROM `colheita` WHERE `colheita`.`idColheita` = \'" + idColheita + "\'";
            cmd = new MySqlCommand(sql, conexao);

            //open con, exect delete, close con
            try
            {
                conexao.Open();

                adapter = new MySqlDataAdapter(cmd);
                adapter.DeleteCommand = conexao.CreateCommand();
                adapter.DeleteCommand.CommandText = sql;

                //prompt for confirmation
                if (MessageBox.Show("Tem certeza que deseja deletar este registro?", "DELETE", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        clearTxts();
                        MessageBox.Show("Deletado com sucesso!");
                    }
                }
                conexao.Close();

                retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conexao.Close();
            }
        }

        //clear txt
        private void clearTxts()
        {
            textBox1.Text = "";
            numericUpDown2.Value = 2017;
            numericUpDown3.Value = 1;
            numericUpDown4.Value = 1;
            comboBox1.Text = "";
            numericUpDown1.Value = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int box1 = Convert.ToInt32(textBox1.Text);
                add(box1, (int)numericUpDown4.Value, (int)numericUpDown3.Value, (int)numericUpDown2.Value,comboBox1.Text, numericUpDown1.Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            retrieve();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int idColheita = Convert.ToInt32(selected);

            int box1 = Convert.ToInt32(textBox1.Text);
            update(box1, (int)numericUpDown4.Value, (int)numericUpDown3.Value, (int)numericUpDown2.Value, comboBox1.Text, numericUpDown1.Value.ToString(), idColheita);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int idColheita = Convert.ToInt32(selected);

                delete(idColheita);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();



        }

        private void dataGridView1_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {

                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                numericUpDown1.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[6].Value);

                numericUpDown2.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[4].Value);
                numericUpDown3.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value);
                numericUpDown4.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value);

            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}
