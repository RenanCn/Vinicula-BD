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
    public partial class formContato : Form
    {
        static string conexaoString = "SERVER=localhost;DATABASE=vinicula;UID=root;PWD=;";
        MySqlConnection conexao = new MySqlConnection(conexaoString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public formContato()
        {
            InitializeComponent();
           
            //datagridview properties

            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Propriedade";
            dataGridView1.Columns[1].Name = "Endereço";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //selection mode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        /** ZONA DE RISCO **/

        //INSERT INTO DB
        private void add(string propriedade, string endereco)
        {
            //sql stmt
            //string sql = "INSERT INTO contato(propriedade_nome, endereço, telefone, e-mail) VALUES (@PROPRIEDADE_NOME,@ENDEREÇO,@TELEFONE,@E-MAIL)";
            string sql = "INSERT INTO `contato` (`Propriedade_Nome`, `Endereço`) VALUES('"+propriedade+"', '"+endereco+"')";
            cmd = new MySqlCommand(sql, conexao);

            //ADD PARAMETERS
           /* cmd.Parameters.AddWithValue("@PROPRIEDADE_NOME", propriedade);
            cmd.Parameters.AddWithValue("@ENDEREÇO", endereco);
            cmd.Parameters.AddWithValue("@TELEFONE", telefone);
            cmd.Parameters.AddWithValue("@E-MAIL", email);*/

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
        private void populate(String propriedade, String endereco)
        {
            dataGridView1.Rows.Add(propriedade,endereco);
        }

        //RETRIEVE FROM DB
        private void retrieve()
        {
            dataGridView1.Rows.Clear();

            //sql stmt
            string sql = "SELECT * FROM contato";
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
                    populate(row[0].ToString(), row[1].ToString());
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
        private void update(string propriedade, string endereco, string old_propriedade)
        {
            //sql stmt

            //string sql = "UPDATE propriedade SET Nome ='" + nome + "',Administrador='" + administrador + "'WHERE NOME =" + nome + "";
            string sql = "UPDATE `contato` SET `Propriedade_Nome` = '" + propriedade + "', `Endereço` = '" + endereco + "' WHERE `contato`.`Propriedade_Nome` = '" + old_propriedade + "'";
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
        private void delete(string propriedade)
        {
            //sql stnt
            // string sql = "DELETE FROM propriedade WHERE NOME=" + nome + "";
            string sql = "DELETE FROM `contato` WHERE `contato`.`Propriedade_Nome` = \'" + propriedade + "\'";
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

        /** ZONA DE RISCO **/

        private void button1_Click(object sender, EventArgs e)
        {
            add(textBox1.Text,textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string propriedade_old = Convert.ToString(selected);
            update(textBox1.Text, textBox2.Text, propriedade_old);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string propriedade = Convert.ToString(selected);

                delete(propriedade);
            }
            catch(Exception ex)
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
                //prop
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                //end
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                //tel
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    
}
