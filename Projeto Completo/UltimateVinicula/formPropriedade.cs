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
    public partial class formPropriedade : Form
    {
        static string conexaoString = "SERVER=localhost;DATABASE=vinicula;UID=root;PWD=;";
        MySqlConnection conexao = new MySqlConnection(conexaoString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();


        public formPropriedade()
        {
            InitializeComponent();

            try
            {
                //datagridview properties
                dataGridView1.ColumnCount = 2;
                dataGridView1.Columns[0].Name = "Nome";
                dataGridView1.Columns[1].Name = "Administrador";

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                //selection mode
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //INSERT INTO DB
        private void add(string nome, string administrador)
        {
            //sql stmt
            string sql = "INSERT INTO propriedade(nome, administrador) VALUES (@NOME,@ADMINISTRADOR)";
            cmd = new MySqlCommand(sql, conexao);

            //ADD PARAMETERS
            cmd.Parameters.AddWithValue("@NOME",nome);
            cmd.Parameters.AddWithValue("@ADMINISTRADOR", administrador);
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
        private void populate(String nome, String administrador)
        {
            dataGridView1.Rows.Add(nome, administrador);
        }

        //RETRIEVE FROM DB
        private void retrieve()
        {
            dataGridView1.Rows.Clear();

            //sql stmt
            string sql = "SELECT * FROM propriedade";
            cmd = new MySqlCommand(sql, conexao);

            //open conex, retrieve, fill dgview
            try
            {
                conexao.Open();

                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                //loop thru dt
                foreach(DataRow row in dt.Rows)
                {
                    populate(row[0].ToString(), row[1].ToString());
                }

                conexao.Close();

                //clear dt
                dt.Rows.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                conexao.Close();
            }
        }

        //update db
        private void update(string nome, string administrador, string old_nome)
        {
            //sql stmt

            //string sql = "UPDATE propriedade SET Nome ='" + nome + "',Administrador='" + administrador + "'WHERE NOME =" + nome + "";
            string sql = "UPDATE `propriedade` SET `Nome` = '"+ nome +"', `Administrador` = '"+ administrador +"' WHERE `propriedade`.`Nome` = '"+ old_nome +"'";
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                conexao.Close();
            }
        }

        //delete from db
        private void delete(string nome)
        {
            //sql stnt
            // string sql = "DELETE FROM propriedade WHERE NOME=" + nome + "";
            string sql = "DELETE FROM `propriedade` WHERE `propriedade`.`Nome` = \'"+nome+"\'";
            cmd = new MySqlCommand(sql,conexao);

            //open con, exect delete, close con
            try{
                conexao.Open();

                adapter = new MySqlDataAdapter(cmd);
                adapter.DeleteCommand = conexao.CreateCommand();
                adapter.DeleteCommand.CommandText = sql;

                //prompt for confirmation
                if(MessageBox.Show("Tem certeza que deseja deletar este registro?", "DELETE",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK)
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
            catch(Exception ex)
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

        //btn de inserir
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                 add(textBox1.Text, textBox2.Text);
            }
            else
            {
                MessageBox.Show("O primeiro campo não pode ficar em branco!","Erro");
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //nome
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                //ademir
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //btn de recuperar
        private void button2_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        //botao de atualizar
        private void button4_Click(object sender, EventArgs e)
        {
            string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string nome = Convert.ToString(selected);
            update(textBox1.Text, textBox2.Text, nome);

        }
        //btn delete
        private void button3_Click(object sender, EventArgs e)
        {
            string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string nome = Convert.ToString(selected);

            Debug.WriteLine("string nome > " + nome);

            delete(nome);
        }
        // botao de limpar
        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}
