using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        static string conexionstring = "Data Source=LAPTOP-HTPETU3P\\DAM1SQLSERVER;Initial Catalog = spotify; Integrated Security = True";
        SqlConnection conn = new SqlConnection(conexionstring);
        public Form3()
        {
            InitializeComponent();
            BackgroundImage = Properties.Resources.spotify6;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

            //Para el campo fecha de hoy
            string Date = DateTime.Now.ToString("dd-MM-yyyy");
            textBox10.Text = Date;
        }
        public void reg(string dni, string userlog, string password, string nombre, string apellidos, string email, string telefono, string direccion, string nacimiento, string alta)
        {

            string query = "INSERT INTO TUsuario (cNif,cUserlogin,cPassword,cNombre_Usuario,cApellidos_Usuario,cEmail_Usuario,cTelefono_Usuario,cDireccion_Usuario,dNacimiento_Usuario,dAlta_Usuario) VALUES (@dni,@userlog,@password,@nombre,@apellidos,@email,@telefono,@direccion,@nacimiento,@alta);";
            SqlCommand comando = new SqlCommand(query, conn);
            comando.Parameters.AddWithValue("dni", dni);
            comando.Parameters.AddWithValue("userlog", userlog);
            comando.Parameters.AddWithValue("password", password);
            comando.Parameters.AddWithValue("nombre", nombre);
            comando.Parameters.AddWithValue("apellidos", apellidos);
            comando.Parameters.AddWithValue("email", email);
            comando.Parameters.AddWithValue("telefono", telefono);
            comando.Parameters.AddWithValue("direccion", direccion);
            comando.Parameters.AddWithValue("nacimiento", nacimiento);
            comando.Parameters.AddWithValue("alta", alta);
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            data.Fill(tabla);




        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //boton registrar
            reg(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox5.Text, this.textBox6.Text, this.textBox7.Text, this.textBox8.Text, this.textBox9.Text,this.textBox10.Text);
            this.Close();
            new Form1().Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form1().Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
