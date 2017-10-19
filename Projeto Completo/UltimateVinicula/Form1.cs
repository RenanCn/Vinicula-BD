using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateVinicula
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void propriedadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                formPropriedade formpropriedade = new formPropriedade();
                formpropriedade.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void contatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void terroirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                formTerroir formterroir = new formTerroir();
                formterroir.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void parreiralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            formParreiral formparreiral = new formParreiral();
            formparreiral.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cepaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            formCepa formcepa = new formCepa();
            formcepa.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void endereçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            formContato formcontato = new formContato();
            formcontato.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void telefoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            formTelefone formtelefone = new formTelefone();
            formtelefone.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            formEmail formemail = new formEmail();
            formemail.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void colheitraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            formColheita formcolheita = new formColheita();
            formcolheita.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void safraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            formSafra formsafra = new formSafra();
            formsafra.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void vinhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                formVinho formvinho = new formVinho();
                formvinho.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void vinhosProduzidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                formRelatorio1 formrelatorio1 = new formRelatorio1();
                formrelatorio1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void safrasDeUmVinhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                formRelatorio2 formrelatorio2 = new formRelatorio2();
                formrelatorio2.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                formRelatorio3 formrelatorio3 = new formRelatorio3();
                formrelatorio3.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                formRelatorio4 formrelatorio4 = new formRelatorio4();
                formrelatorio4.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                formRelatorio5 formrelatorio5 = new formRelatorio5();
                formrelatorio5.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
