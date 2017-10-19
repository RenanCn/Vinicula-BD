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
    public partial class formRelatorio4 : Form
    {

        static string conexaoString = "SERVER=localhost;DATABASE=vinicula;UID=root;PWD=;";
        MySqlConnection conexao = new MySqlConnection(conexaoString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public formRelatorio4()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Propriedade";
            dataGridView1.Columns[1].Name = "Tipos de Uva";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //selection mode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        //ADD TO DGVIEW
        private void populate(String propriedade, String tiposDeUva)
        {
            dataGridView1.Rows.Add(propriedade,tiposDeUva);
        }

        //RETRIEVE FROM DB
        private void retrieve(string propriedade)
        {
            dataGridView1.Rows.Clear();

            //sql stmt
            string sql = "SELECT prop.nome, c.nome FROM Propriedade prop, Cepa c, Parreiral par WHERE c.Parreiral_idParreiral = par.idParreiral AND par.Propriedade_Nome = prop.Nome AND prop.Nome = '"+propriedade+"'";
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



        private void button1_Click(object sender, EventArgs e)
        {
            retrieve(textBox1.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
