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
    public partial class formSafra : Form
    {

        static string conexaoString = "SERVER=localhost;DATABASE=vinicula;UID=root;PWD=;";
        MySqlConnection conexao = new MySqlConnection(conexaoString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public formSafra()
        {
            InitializeComponent();
        
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "ID Colheita";
            dataGridView1.Columns[1].Name = "Ano";
            dataGridView1.Columns[2].Name = "Garrafas";
            dataGridView1.Columns[3].Name = "Cod. Vinho";
            dataGridView1.Columns[4].Name = "Nome Vinho";
            dataGridView1.Columns[5].Name = "Classificação";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //selection mode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        //INSERT INTO DB
        private void add(int idColheita, int ano, int garrafas, int codVinho, string nomeVinho, int avaliação)
        {
            //sql stmt
            string sql = "INSERT INTO `safra` (`Colheita_idColheita`,`Ano`, `Garrafas`, `Vinho_codVinho`,`Vinho_Nome`,`Avaliação`) VALUES('" + idColheita + "','" + ano + "', '" + garrafas + "','" + codVinho + "','" + nomeVinho + "','" + avaliação + "')";
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
        private void populate(int idColheita, int ano, int garrafas, int codVinho, String nomeVinho, int avaliação)
        {
            dataGridView1.Rows.Add(idColheita, ano, garrafas, codVinho, nomeVinho, avaliação);
        }

        //RETRIEVE FROM DB
        private void retrieve()
        {
            dataGridView1.Rows.Clear();

            //sql stmt
            string sql = "SELECT * FROM safra";
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
                    int row5 = Convert.ToInt32(row[5].ToString());

                    populate(row0, row1, row2, row3, row[4].ToString(), row5);
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
        private void update(int idColheita, int ano, int garrafas, int codVinho, String nomeVinho, int avaliação, int old_idColheita)
        {
            //sql stmt

            //string sql = "UPDATE propriedade SET Nome ='" + nome + "',Administrador='" + administrador + "'WHERE NOME =" + nome + "";
            string sql = "UPDATE `safra` SET `Colheita_idColheita` = '" + idColheita + "', `Ano` = '" + ano + "', `Garrafas` = '" + garrafas + "', `Vinho_codVinho` = '" + codVinho + "', `Vinho_Nome` = '" + nomeVinho + "', `Avaliação` = '" + avaliação + "' WHERE `safra`.`Colheita_idColheita` = '" + old_idColheita + "'";
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
            string sql = "DELETE FROM `safra` WHERE `safra`.`Colheita_idColheita` = \'" + id + "\'";
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
            textBox5.Text = "";
            numericUpDown1.Value = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int box1 = Convert.ToInt32(textBox1.Text);
                int box2 = Convert.ToInt32(textBox2.Text);
                int box3 = Convert.ToInt32(textBox3.Text);
                int box4 = Convert.ToInt32(textBox4.Text);
                int nud1 = Convert.ToInt32(numericUpDown1.Text);

                add(box1, box2, box3, box4,textBox5.Text, nud1);
            }
            catch (Exception ex)
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
            string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int id = Convert.ToInt32(selected);

            string label4Texto = label4.Text.ToString();
            label4Texto = label4Texto.Replace(@"\", @"\\");

            int box1 = Convert.ToInt32(textBox1.Text);
            int box2 = Convert.ToInt32(textBox2.Text);
            int box3 = Convert.ToInt32(textBox3.Text);
            int box4 = Convert.ToInt32(textBox4.Text);
            int nud1 = Convert.ToInt32(numericUpDown1.Text);

            update(box1, box2, box3, box4, textBox5.Text, nud1, id);
        }

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
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            numericUpDown1.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }
    }
}
