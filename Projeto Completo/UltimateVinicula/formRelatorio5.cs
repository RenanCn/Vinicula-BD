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
    public partial class formRelatorio5 : Form
    {
        static string conexaoString = "SERVER=localhost;DATABASE=vinicula;UID=root;PWD=;";
        MySqlConnection conexao = new MySqlConnection(conexaoString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public formRelatorio5()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Tipo de Uva";
            dataGridView1.Columns[1].Name = "Ano Produtivo";
            dataGridView1.Columns[2].Name = "Garrafas";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //selection mode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        //ADD TO DGVIEW
        private void populate(String tipoDeUva, int ano, int garrafas)
        {
            dataGridView1.Rows.Add(tipoDeUva, ano, garrafas);
        }

        //RETRIEVE FROM DB
        private void retrieve(string cepa)
        {
            dataGridView1.Rows.Clear();

            //sql stmt
            string sql = "SELECT cepa.Nome, safra.Ano, MAX(safra.Garrafas) FROM cepa, parreiral, colheita, safra WHERE parreiral.idParreiral = cepa.Parreiral_idParreiral AND parreiral.idParreiral = colheita.Parreiral_idParreiral AND colheita.idColheita = safra.Colheita_idColheita AND cepa.Nome = '" + cepa + "' GROUP BY safra.Ano, cepa.Nome";
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
                    int row2 = Convert.ToInt32(row[2].ToString());
                    populate(row[0].ToString(), row1, row2);
                    break;
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
            retrieve(comboBox1.Text.ToString());

        }
    }
}
