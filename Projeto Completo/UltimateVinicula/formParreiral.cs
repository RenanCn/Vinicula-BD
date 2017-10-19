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
using System.Diagnostics;

namespace UltimateVinicula
{
    public partial class formParreiral : Form
    {
          

        static string conexaoString = "SERVER=localhost;DATABASE=vinicula;UID=root;PWD=;";
        MySqlConnection conexao = new MySqlConnection(conexaoString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public formParreiral()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Propriedade";
            dataGridView1.Columns[2].Name = "Vinhas";
            dataGridView1.Columns[3].Name = "Área (m²)";
            dataGridView1.Columns[4].Name = "Data do Plantio";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //selection mode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        /****/
        //INSERT INTO DB
        private void add(string propriedade_nome, int vinhas, double area, string plantio)
        {
            //sql stmt
            string sql = "INSERT INTO `parreiral` (`idParreiral`,`Propriedade_Nome`, `Vinhas`, `Área (m²)`, `Plantio`) VALUES(NULL, '" + propriedade_nome + "', '" + vinhas + "', '" + area + "', '" + plantio + "')";
            cmd = new MySqlCommand(sql, conexao);

            //ADD PARAMETERS
            /* cmd.Parameters.AddWithValue("@PRORIEDADE_NOME", propriedade_nome);
             cmd.Parameters.AddWithValue("@ADMINISTRADOR", administrador);*/
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
        private void populate(int id, String propriedade_nome, int vinhas, double area, string plantio)
        {
            dataGridView1.Rows.Add(id, propriedade_nome, vinhas, area, plantio);
        }

        //RETRIEVE FROM DB
        private void retrieve()
        {
            dataGridView1.Rows.Clear();

            //sql stmt
            string sql = "SELECT * FROM parreiral";
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
                    int row1 = Convert.ToInt32(row[0].ToString());
                    int row2 = Convert.ToInt32(row[2].ToString());
                    double row3 = Convert.ToDouble(row[3].ToString());
                    populate(row1, row[1].ToString(), row2, row3,  row[4].ToString());
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
        private void update(string propriedade_nome, int vinhas, double area, string plantio, int id)
        {
            //sql stmt

            string sql = "UPDATE `parreiral` SET `Propriedade_Nome` = '" + propriedade_nome + "', `Vinhas` = '" + vinhas + "', `Área (m²)` = '" + area + "', `Plantio` = '" + plantio + "' WHERE `parreiral`.`idParreiral` = '" + id + "'";
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
        private void delete(int id)
        {
            //sql stnt
            // string sql = "DELETE FROM propriedade WHERE NOME=" + nome + "";
            string sql = "DELETE FROM `parreiral` WHERE `parreiral`.`idParreiral` = \'" + id + "\'";
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
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            
        }



        /****/

        //botao
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int box2 = Convert.ToInt32(textBox2.Text);
                double box3 = Convert.ToDouble(textBox3.Text);

                add(textBox1.Text, box2, box3, textBox4.Text);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int id = Convert.ToInt32(selected);

                delete(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int id = Convert.ToInt32(selected);

                int box2 = Convert.ToInt32(textBox2.Text);
                double box3 = Convert.ToDouble(textBox3.Text);

                update(textBox1.Text, box2, box3, textBox4.Text, id);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox4.Text = "";
                             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
