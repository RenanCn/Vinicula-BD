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
    public partial class formCepa : Form
    {

        static string conexaoString = "SERVER=localhost;DATABASE=vinicula;UID=root;PWD=;";
        MySqlConnection conexao = new MySqlConnection(conexaoString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public formCepa()
        {
            InitializeComponent();

            //datagridview properties
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Nome";
            dataGridView1.Columns[1].Name = "ID do Parreiral";
            dataGridView1.Columns[2].Name = "Região de Origem";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //selection mode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

        }


        //INSERT INTO DB
        private void add(string nome, int idParreiral, string regiaoOrigem)
        {
            //sql stmt
            //string sql = "INSERT INTO contato(propriedade_nome, endereço, telefone, e-mail) VALUES (@PROPRIEDADE_NOME,@ENDEREÇO,@TELEFONE,@E-MAIL)";
            string sql = "INSERT INTO `cepa` (`Nome`, `Parreiral_idParreiral`, `Região de Origem`) VALUES('" + nome + "', '" + idParreiral + "', '" + regiaoOrigem + "')";
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
        private void populate(String nome, int idParreiral, String regiaoOrigem)
        {
            dataGridView1.Rows.Add(nome, idParreiral, regiaoOrigem);
        }

        //RETRIEVE FROM DB
        private void retrieve()
        {
            dataGridView1.Rows.Clear();

            //sql stmt
            string sql = "SELECT * FROM cepa";
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
                    int row1 = Convert.ToInt32(row[1].ToString()); 
                    populate(row[0].ToString(), row1, row[2].ToString());
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
        private void update(string nome, int idParreiral, string regiaoOrigem, string nome_old)
        {
            //sql stmt

            //string sql = "UPDATE propriedade SET Nome ='" + nome + "',Administrador='" + administrador + "'WHERE NOME =" + nome + "";
            string sql = "UPDATE `cepa` SET `Nome` = '" + nome + "', `Parreiral_idParreiral` = '" + idParreiral + "', `Região de Origem` = '" + regiaoOrigem + "' WHERE `cepa`.`Nome` = '" + nome_old + "'";
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
        private void delete(string nome)
        {
            //sql stnt
            string sql = "DELETE FROM `cepa` WHERE `cepa`.`Nome` = \'" + nome + "\'";
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
            comboBox2.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int box2 = Convert.ToInt32(textBox2.Text);
            add(comboBox2.Text, box2, comboBox1.Text);
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                comboBox2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string nome_old = Convert.ToString(selected);

                int box2 = Convert.ToInt32(textBox2.Text);
                update(comboBox2.Text, box2, comboBox1.Text, nome_old);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //deleta
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string nome = Convert.ToString(selected);

                delete(nome);
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
    }
}
