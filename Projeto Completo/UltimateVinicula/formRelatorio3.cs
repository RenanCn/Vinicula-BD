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
    public partial class formRelatorio3 : Form
    {

        static string conexaoString = "SERVER=localhost;DATABASE=vinicula;UID=root;PWD=;";
        MySqlConnection conexao = new MySqlConnection(conexaoString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public formRelatorio3()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Propriedade";
            dataGridView1.Columns[1].Name = "Garrafas";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //selection mode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

        }

        //ADD TO DGVIEW
        private void populate(string propriedade, int garrafas)
        {
            dataGridView1.Rows.Add(propriedade, garrafas);
        }

        //RETRIEVE FROM DB
        private void retrieve(int ano)
        {
            dataGridView1.Rows.Clear();

            string sql = "SELECT propriedade.nome, SUM(safra.Garrafas) FROM propriedade, parreiral, colheita, safra WHERE propriedade.Nome = parreiral.Propriedade_Nome AND parreiral.idParreiral = colheita.Parreiral_idParreiral AND colheita.idColheita = safra.Colheita_idColheita AND safra.Ano = '"+ano+ "' GROUP BY propriedade.nome";

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
                    populate(row[0].ToString(), row1);
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                retrieve(Convert.ToInt32(textBox1.Text));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
