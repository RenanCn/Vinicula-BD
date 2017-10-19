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
    public partial class formTelefone : Form
    {

        static string conexaoString = "SERVER=localhost;DATABASE=vinicula;UID=root;PWD=;";
        MySqlConnection conexao = new MySqlConnection(conexaoString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public formTelefone()
        {
            InitializeComponent();

            //datagridview properties

            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Propriedade";
            dataGridView1.Columns[2].Name = "Telefone";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //selection mode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        //INSERT INTO DB
        private void add(string propriedade, string telefone)
        {
            //sql stmt
            string sql = "INSERT INTO `telefones` (`idTelefones`,`Contato_Propriedade_Nome`, `Telefone`) VALUES(NULL,'" + propriedade + "', '" + telefone + "')";
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
        private void populate(int id, String propriedade, String telefone)
        {
            dataGridView1.Rows.Add(id,propriedade, telefone);
        }

        //RETRIEVE FROM DB
        private void retrieve()
        {
            dataGridView1.Rows.Clear();

            //sql stmt
            string sql = "SELECT * FROM telefones";
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
                    populate(row0, row[1].ToString(),row[2].ToString());
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
        private void update(string propriedade, string telefone, int id)
        {
            //sql stmt

            //string sql = "UPDATE propriedade SET Nome ='" + nome + "',Administrador='" + administrador + "'WHERE NOME =" + nome + "";
            string sql = "UPDATE `telefones` SET `Contato_Propriedade_Nome` = '" + propriedade + "', `Telefone` = '" + telefone + "' WHERE `telefones`.`idTelefones` = '" + id + "'";
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
            string sql = "DELETE FROM `telefones` WHERE `telefones`.`idTelefones` = \'" + id + "\'";
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
        }


        private void button1_Click(object sender, EventArgs e)
        {
            add(textBox1.Text, textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            retrieve();
        }
        //att
        private void button3_Click(object sender, EventArgs e)
        {
            string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int id = Convert.ToInt32(selected);
            update(textBox1.Text, textBox2.Text, id);
        }
        //del
        private void button4_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
               
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
