using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
/*
 Realizado por Manuel Alberto Según López como proyecto de DAM. CES JUAN PABLO II CADIZ 2022
 */
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static string conexionstring = "Data Source=LAPTOP-HTPETU3P\\DAM1SQLSERVER;Initial Catalog = spotify; Integrated Security = True";
        SqlConnection conn = new SqlConnection(conexionstring);
        public Form1()
        {
            InitializeComponent();
            BackgroundImage = Properties.Resources.spotify6;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }
        public void logueo(string Usuario,string Password)
        {
            try
            {
                conn.Open();
                string query = "select * from TUsuario where cUserlogin = @User AND cPassword = @pass";
                SqlCommand comando = new SqlCommand(query, conn);
                comando.Parameters.AddWithValue("User", Usuario);
                comando.Parameters.AddWithValue("pass", Password);
                SqlDataAdapter data = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                data.Fill(tabla);

                if (tabla.Rows.Count == 1)//si encuentra fila con los datos introducidos entonces verdad
                {
                    //MessageBox.Show("Correcto");
                    this.Hide();
                    new Form2(tabla.Rows[0][1].ToString(), tabla.Rows[0][0].ToString(), tabla.Rows[0][3].ToString(), tabla.Rows[0][4].ToString(), tabla.Rows[0][5].ToString(), tabla.Rows[0][6].ToString(), tabla.Rows[0][7].ToString(), tabla.Rows[0][8].ToString(), tabla.Rows[0][9].ToString()).Show();//le pasamos la columna userlog al formulario dos
                }
                else
                {
                    MessageBox.Show("error,usuario y/o password no encontrados");
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);

            }
            finally
            {
                conn.Close();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            logueo(this.textBox1.Text,this.textBox2.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Registrarse
            new Form3().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //salir
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Form4().Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
