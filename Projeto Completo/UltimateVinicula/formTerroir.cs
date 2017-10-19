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
    public partial class formTerroir : Form
    {

        static string conexaoString = "SERVER=localhost;DATABASE=vinicula;UID=root;PWD=;";
        MySqlConnection conexao = new MySqlConnection(conexaoString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public formTerroir()
        {
            InitializeComponent();

            //datagridview properties
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Name = "Propriedade";
            dataGridView1.Columns[1].Name = "Tipo de Solo";
            dataGridView1.Columns[2].Name = "Altitude (m)";
            dataGridView1.Columns[3].Name = "Umidade (g/Kg)";
            dataGridView1.Columns[4].Name = "Índice Pluviométrico (%)";
            dataGridView1.Columns[5].Name = "Clima (°C)";
            dataGridView1.Columns[6].Name = "Região";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //selection mode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        //INSERT INTO DB
        private void add(string propriedade_nome, string tipodesolo, double altitude, double umidade, double indice_pluviometrico, double clima, string regiao)
        {
            //sql stmt
            string sql = "INSERT INTO `terroir` (`Propriedade_Nome`, `Tipo de Solo`, `Altitude (m)`, `Umidade`,`Índice Pluviométrico (%)`,`Clima (°C)`,`Região`) VALUES('" + propriedade_nome + "', '" + tipodesolo + "', '" + altitude + "', '" + umidade + "', '" + indice_pluviometrico + "', '" + clima + "', '" + regiao + "')";
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
        private void populate(String propriedade_nome, String tipodesolo, double altitude, double umidade, double indice_pluviometrico, double clima, String regiao)
        {
            dataGridView1.Rows.Add(propriedade_nome, tipodesolo,altitude,umidade,indice_pluviometrico,clima,regiao);
        }

        //RETRIEVE FROM DB
        private void retrieve()
        {
            dataGridView1.Rows.Clear();

            //sql stmt
            string sql = "SELECT * FROM terroir";
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
                    double drow2 = Convert.ToDouble(row[2].ToString());
                    double drow3 = Convert.ToDouble(row[3].ToString());
                    double drow4 = Convert.ToDouble(row[4].ToString());
                    double drow5 = Convert.ToDouble(row[5].ToString());
                    populate(row[0].ToString(), row[1].ToString(), drow2, drow3, drow4, drow5, row[6].ToString());
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
        private void update(string propriedade_nome, string tipodesolo, double altitude, double umidade, double indice_pluviometrico, double clima, string regiao, string old_propriedade)
        {
            //sql stmt

            string sql = "UPDATE `terroir` SET `Propriedade_Nome` = '" + propriedade_nome + "', `Tipo de Solo` = '" + tipodesolo + "', `Altitude (m)` = '" + altitude + "', `Umidade` = '" + umidade + "', `Índice Pluviométrico (%)` = '" + indice_pluviometrico + "', `Clima (°C)` = '" + clima + "', `Região` = '" + regiao + "' WHERE `terroir`.`Propriedade_Nome` = '" + old_propriedade + "'";
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
        private void delete(string propriedade_nome)
        {
            //sql stnt
            // string sql = "DELETE FROM propriedade WHERE NOME=" + nome + "";
            string sql = "DELETE FROM `terroir` WHERE `terroir`.`Propriedade_Nome` = \'" + propriedade_nome + "\'";
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
            textBox6.Text = "";
            comboBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double box3 = Convert.ToDouble(textBox3.Text);
            double box4 = Convert.ToDouble(textBox4.Text);
            double box5 = Convert.ToDouble(textBox5.Text);
            double box6 = Convert.ToDouble(textBox6.Text);
            
            add(textBox1.Text, textBox2.Text, box3, box4, box5, box6, comboBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            retrieve();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string propriedade_old = Convert.ToString(selected);

                double box3 = Convert.ToDouble(textBox3.Text);
                double box4 = Convert.ToDouble(textBox4.Text);
                double box5 = Convert.ToDouble(textBox5.Text);
                double box6 = Convert.ToDouble(textBox6.Text);

                update(textBox1.Text, textBox2.Text, box3, box4, box5, box6, comboBox1.Text, propriedade_old);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string propriedade = Convert.ToString(selected);

                delete(propriedade);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
