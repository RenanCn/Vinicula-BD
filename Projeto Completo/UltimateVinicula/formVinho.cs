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
    public partial class formVinho : Form
    {
        static string conexaoString = "SERVER=localhost;DATABASE=vinicula;UID=root;PWD=;";
        MySqlConnection conexao = new MySqlConnection(conexaoString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public formVinho()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Nome";
            dataGridView1.Columns[2].Name = "Rótulo";
            dataGridView1.Columns[3].Name = "Classificação";


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //selection mode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        //INSERT INTO DB
        private void add(string nome, string rotulo, string classificacao)
        {
            //sql stmt   
            string sql = "INSERT INTO `vinho` (`codVinho`,`Nome`,`Rótulo`,`Classificação`) VALUES(NULL,'" + nome + "', '" + rotulo + "', '" + classificacao + "')";
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
        private void populate(int codVinho, String nome, String rotulo, String classificacao)
        {
            dataGridView1.Rows.Add(codVinho, nome, rotulo, classificacao);
        }

        //RETRIEVE FROM DB
        private void retrieve()
        {
            dataGridView1.Rows.Clear();

            //sql stmt
            string sql = "SELECT * FROM vinho";
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
                    populate(row0, row[1].ToString(), row[2].ToString(),row[3].ToString());
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
        private void update(string nome, string rotulo, string classificacao, int codVinho)
        {
            //sql stmt

            string sql = "UPDATE `vinho` SET `Nome` = '" + nome + "', `Rótulo` = '" + rotulo + "', `Classificação` = '" + classificacao + "' WHERE `vinho`.`codVinho` = '" + codVinho + "'";
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
        private void delete(int codVinho)
        {
            //sql stnt
            string sql = "DELETE FROM `vinho` WHERE `vinho`.`codVinho` = \'" + codVinho + "\'";
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
            comboBox1.Text = "";
            label4.Text = "";
        }

        //add
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string label4Texto = label4.Text.ToString();
                label4Texto = label4Texto.Replace(@"\",@"\\");
                add(textBox1.Text, @""+label4Texto+"", comboBox1.Text);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //::
        private void label3_Click(object sender, EventArgs e)
        {

        }


        //carregar foto
        private void button6_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Abrir Imagem de Rótulo";
            dlg.Filter = "PNG (*.png)|*.png"
                + "|All files (*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(dlg.OpenFile());
                    label4.Text = dlg.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possivel carregar a foto: " + ex.Message);
                }
            }

            dlg.Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int id = Convert.ToInt32(selected);

            string label4Texto = label4.Text.ToString();
            label4Texto = label4Texto.Replace(@"\", @"\\");

            update(textBox1.Text, label4Texto, comboBox1.Text, id);
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

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void button5_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                label4.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                pictureBox1.Image = Image.FromFile(label4.Text);

                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
