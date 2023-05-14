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
using System.Media;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        static string conexionstring = "Data Source=LAPTOP-HTPETU3P\\DAM1SQLSERVER;Initial Catalog = spotify; Integrated Security = True";
        SqlConnection conn = new SqlConnection(conexionstring);
        string nombrecancionn = "";//variableglobal
        int ActivarListaa = 0;
        public Form2(string userlog,string dni, string nombreusuario, string apellidosusuario, string email, string telefono, string direccion, string nacimiento, string alta)
        {
            InitializeComponent();
            




            //datos usuario
            label2.Text = userlog;//pasado por parametro de form1
            label16.Text = dni;//pasado por parametro de form1
            label17.Text = nombreusuario;//pasado por parametro de form1
            label18.Text = apellidosusuario;//pasado por parametro de form1
            label19.Text = email;//pasado por parametro de form1
            label20.Text = telefono;//pasado por parametro de form1
            label21.Text = direccion;//pasado por parametro de form1
            label22.Text = nacimiento;//pasado por parametro de form1
            label23.Text = alta;//pasado por parametro de form1


            dataGridView2.Visible = false;
            dataGridView3.Visible = true;
            button4.Visible = false;
            button1.Visible = false;
            textBox3.Visible = false;
            label5.Visible = false;
            textBox1.Visible = false;
            label4.Visible = false;
            textBox2.Visible = false;
            label3.Visible = false;
            label6.Visible = false;
            textBox4.Visible = false;
            label7.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            label28.Visible = false;


            //boton buscar
            button2.Tag = "cerrado";

            //boton mostrar lista
            button5.Tag = "cerrado";




            //boton crear lista
            button7.Tag = "cerrado";
            textBox7.Visible = false;
            textBox8.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;
            button8.Visible = false;

            //boton anñadir cancion a lista
            button9.Visible = false;
            button9.Tag = "cerrado";
            textBox11.Visible = false;
            textBox12.Visible = false;
            button10.Visible = false;
            label29.Visible = false;
            label30.Visible = false;

            string Date = DateTime.Now.ToString("dd-MM-yyyy");
            //para crear lista, poner datos sin que se puedan modificar en el texbox en el campo dni y fecha
            textBox10.Text = dni;
            textBox9.Text = Date;

            BackgroundImage = Properties.Resources.spotify6;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }
        
        public void crearlista(string signatura, string nombre, string fech, string dni)
        {
           
            string query = "INSERT INTO TLista (cSignatura,cNombre_Lista,dLista,cNif) VALUES (@signatura,@nombre,@fecha,@dni); ";
            SqlCommand comando = new SqlCommand(query, conn);
            comando.Parameters.AddWithValue("signatura", signatura);
            comando.Parameters.AddWithValue("nombre", nombre);
            comando.Parameters.AddWithValue("fecha", fech);
            comando.Parameters.AddWithValue("dni", dni);
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            data.Fill(tabla);
            textBox7.Text = "";
            textBox8.Text = "";
        }
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button4.Visible = false;
            button1.Visible = false;
            textBox3.Visible = false;
            label5.Visible = false;
            textBox1.Visible = false;
            label4.Visible = false;
            textBox2.Visible = false;
            label3.Visible = false;
            label6.Visible = false;
            textBox4.Visible = false;
            label7.Visible = false;
            textBox5.Visible = false;
            button9.Visible = false;

            string valornombrelista = "";
            //resultados listas
            if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView2.CurrentRow.Selected = true;
                valornombrelista = dataGridView2.Rows[e.RowIndex].Cells["cNombre_Lista"].FormattedValue.ToString();
            }
            textBox6.Text = valornombrelista;
            ActivarListaa = 1;//para cambiar valor a la vaiable global y que pueda entrar en datafridview doble click
            ActivarLista(this.textBox6.Text);
        }
        public void mostrarlista(string dni)
        {
            //listas usuario
            string query = "select cNombre_Lista,cSignatura from TLista where cNif = @dni";
            SqlCommand comando = new SqlCommand(query, conn);
            comando.Parameters.AddWithValue("dni", dni);
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            data.Fill(tabla);
            dataGridView2.DataSource = tabla;
            this.dataGridView2.Columns["cSignatura"].Visible = false;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //mostrar listas
            if (button5.Tag.ToString() == "cerrado")
            {
                dataGridView2.Visible = true;
                button5.Tag = "abierto";
                mostrarlista(this.label16.Text);
            }
            else if(button5.Tag.ToString() == "abierto")
            {
                label28.Visible = false;
                dataGridView2.Visible = false;
                button5.Tag = "cerrado";
            }

        }
        public void añadircancion(string idcancion,string signaturalista)
        {
            
            string query = "INSERT INTO TListaCancion (cSignatura,nCancionID) VALUES (@signaturalista,@idcancion); ";
            SqlCommand comando2 = new SqlCommand(query, conn);
            comando2.Parameters.AddWithValue("signaturalista", signaturalista);
            comando2.Parameters.AddWithValue("idcancion", idcancion);
            SqlDataAdapter data2 = new SqlDataAdapter(comando2);
            DataTable tabla2 = new DataTable();
            data2.Fill(tabla2);
            textBox11.Text = "";
            textBox12.Text = "";

        }
        public void ActivarLista(string nombre_lista)
        {
            string query = "select TCancion.cTitulo,TCancion.nAnyoPublicacion,TCancion.cGeneroMusical,TCancion.nValoracion,TCancion.cComentario,TCancion.lEscuchado from TLista inner join TListaCancion on TlistaCancion.cSignatura = TLista.cSignatura " +
                "inner join TCancion on TListaCancion.nCancionID = TCancion.nCancionID " +
                "where TLista.cNombre_Lista = @nombre_lista";
            SqlCommand comando = new SqlCommand(query, conn);
            comando.Parameters.AddWithValue("nombre_lista", nombre_lista);
            SqlDataAdapter data = new SqlDataAdapter(comando);
            DataTable tabla2 = new DataTable();
            data.Fill(tabla2);
            dataGridView3.DataSource = tabla2;
            //restricciones tabla
            dataGridView3.Columns["cTitulo"].ReadOnly = true;// solo lectura
            dataGridView3.Columns["nAnyoPublicacion"].ReadOnly = true; // solo lectura
            dataGridView3.Columns["cGeneroMusical"].ReadOnly = true;// solo lectura

            //para el titulo de la lista 
            string query2 = "select TLista.cNombre_Lista from TLista where TLista.cNombre_Lista = @nombre_lista";
            SqlCommand comando2 = new SqlCommand(query2, conn);
            comando2.Parameters.AddWithValue("nombre_lista", nombre_lista);
            SqlDataAdapter data2 = new SqlDataAdapter(comando2);
            DataTable tabla22 = new DataTable();
            data2.Fill(tabla22);

            //nombrelista titulo zona superior
            string nombrelista = tabla22.Rows[0][0].ToString();
            label28.Text = nombrelista;
            label28.Visible = true;
        }
        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string titulocancion = "";
            //resultados buscar ahora  o lista x
            if (dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView3.CurrentRow.Selected = true;
                titulocancion = dataGridView3.Rows[e.RowIndex].Cells["cTitulo"].FormattedValue.ToString();
            }
            textBox6.Text = titulocancion;
            reproduccion(this.textBox6.Text);
        }
        public void reproduccion(string nombrecancion)
        {

            if (nombrecancion == "Do Ya Wanna Taste It")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Do Ya Wanna Taste It.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Do_Ya_Wanna_Taste_It);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Welcome To The Church Of Rock And Roll")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Welcome To The Church Of Rock And Roll.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Welcome_To_The_Church_Of_Rock_And_Roll);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Come On Come On")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Come On Come On.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Come_On_Come_On);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Summertime Girls")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Summertime Girls.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Summertime_Girls);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Night Of Passion")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Night Of Passion.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Night_Of_Passion);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "I Dont Love You Anymore")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\I Dont Love You Anymore.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.I_Don_t_Love_You_Anymore);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Love Bomb Baby")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Love Bomb Baby.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Love_Bomb_Baby);
                simpleSound.Play();//para reproducir una sola vez
            }

            else if (nombrecancion == "7 OClock")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\7 OClock.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources._7_O_Clock);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Borderline Crazy")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Borderline Crazy.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Borderline_Crazy);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Would You Love a Creature")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Would You Love a Creature.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Would_You_Love_a_Creature);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Dont Treat Me Bad")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Dont Treat Me Bad.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Don_t_Treat_Me_Bad);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Drag Me Down")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Drag Me Down.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Drag_Me_Down);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Boots on Rocks Off")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Boots on Rocks Off.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Boots_on_Rocks_Off);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Pumped Up Kicks")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Pumped Up Kicks.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Pumped_Up_Kicks__feat__Ralph_Saenz___from__Peacemaker__);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Powertrain")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Powertrain.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Powertrain);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Six Feet Under")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Six Feet Under.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Six_Feet_Under);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Beat the Bullet")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Beat the Bullet.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Beat_the_Bullet);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "I Wanna Be With You")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\I Wanna Be With You.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.I_Wanna_Be_With_You);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Jawbreaker")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Jawbreaker.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Jawbreaker);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "House of Pain")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\House of Pain.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.House_of_Pain);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Push Push (Lady Lightning)")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Push Push (Lady Lightning).wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Push_Push__Lady_Lightning_);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Arrancame")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Arrancame.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Arrancame);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Ayo technology")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Ayo technology.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Ayo_technology);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Bounce")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Bounce.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Bounce);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Concierto Salvaje")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Concierto Salvaje.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Concierto_salvaje);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Have a Nice Day")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Have a Nice Day.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Have_a_nice_day);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "In Da Club")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\In Da Club.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.In_Da_Club);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Its My Life")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Its My Life.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Its_My_Life);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "La Lluvia en los Zapatos")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\La Lluvia en los Zapatos.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.La_Lluvia_en_los_Zapatos);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Si tu la quieres")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Si tu la quieres.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Si_Tu_La_Quieres);
                simpleSound.Play();//para reproducir una sola vez
            }
            else if (nombrecancion == "Un violinista en tu tejado")
            {
                SoundPlayer simpleSound = new SoundPlayer(@"E:\dam ciclo\Programacion\Proyectos programacion\3 Trimestre\Proyecto\Nuevo Proyecto BD\Almacen_wav\Un violinista en tu tejado.wav");
                //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Un_violinista_en_tu_tejado);
                simpleSound.Play();//para reproducir una sola vez
            }
        }
            private void label2_Click(object sender, EventArgs e)
        {

        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //mis datos
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //mis listas
            string signatura = "";
           
            if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView2.CurrentRow.Selected = true;
                signatura = dataGridView2.Rows[e.RowIndex].Cells["cSignatura"].FormattedValue.ToString();
            }
            textBox12.Text = signatura;
        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //resultados buscar ahora

            //busqueda sin doble click para que se pueda reproducir con el reproductor de iconos de arriba de la izq
            string idcancion = "";
            string titulocancion = "";
            //resultados buscar ahora  o lista x
            if (dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView3.CurrentRow.Selected = true;
                titulocancion = dataGridView3.Rows[e.RowIndex].Cells["cTitulo"].FormattedValue.ToString();
                
                    if (ActivarListaa == 0)
                {
                    titulocancion = dataGridView3.Rows[e.RowIndex].Cells["cTitulo"].FormattedValue.ToString();

                    idcancion = dataGridView3.Rows[e.RowIndex].Cells["nCancionID"].FormattedValue.ToString();
                }
                else if (ActivarListaa == 1)
                {
                    titulocancion = dataGridView3.Rows[e.RowIndex].Cells["cTitulo"].FormattedValue.ToString();

                }
            }
            textBox6.Text = titulocancion;
            textBox11.Text = idcancion;// este texbox pertenece al de añadir canciones a listas
            nombrecancionn = titulocancion;//para que la variable global tenga el titulo de la cancion señalada

           
        }
        private void label3_Click(object sender, EventArgs e)
        {
            //titulo
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //anyopublicacion
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //titulo
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //anyopublicacion
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //genero musical
        }

        private void label5_Click(object sender, EventArgs e)
        {
            //genero musical
        }

        private void label6_Click(object sender, EventArgs e)
        {
            //nombre autor
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //nombre autor
        }

        private void label7_Click(object sender, EventArgs e)
        {
            //apellidos autor
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //apellidos autor
        }
        public void busqueda1(string titulo, string anyo_pulicacion,string genero_musical,
            string nombre_autor,string apellidos_autor)
            
        {
           
           ActivarListaa = 0;
            if (textBox1.Text!="" && textBox2.Text!="" &&  textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
                {
                string query = "select TCancion.nCancionID,TCancion.cTitulo,TCancion.nAnyoPublicacion,TCancion.cGeneroMusical,TAutor.cNombre_Autor,TAutor.cApellidos_Autor " +
               " from TCancion " +
               " INNER JOIN TCancionAutor on TcancionAutor.nCancionID = TCancion.nCancionID " +
               " INNER JOIN TAutor on TAutor.nAutorID = TCancionAutor.nAutorID " +
               " where TCancion.cTitulo = @titulo AND TCancion.nAnyoPublicacion = @anyo_pulicacion AND TCancion.cGeneroMusical = @genero_musical AND TAutor.cNombre_Autor = @nombre_autor AND TAutor.cApellidos_Autor = @apellidos_autor";
                SqlCommand comando = new SqlCommand(query, conn);
                comando.Parameters.AddWithValue("titulo", titulo);
                comando.Parameters.AddWithValue("anyo_pulicacion", anyo_pulicacion);
                comando.Parameters.AddWithValue("genero_musical", genero_musical);
                comando.Parameters.AddWithValue("nombre_autor", nombre_autor);
                comando.Parameters.AddWithValue("apellidos_autor", apellidos_autor);
                SqlDataAdapter data = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                data.Fill(tabla);
                dataGridView3.DataSource = tabla;

            }
            else if(textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
            {
                string query = "select TCancion.nCancionID,TCancion.cTitulo,TCancion.nAnyoPublicacion,TCancion.cGeneroMusical,TAutor.cNombre_Autor,TAutor.cApellidos_Autor " +
               " from TCancion " +
               " INNER JOIN TCancionAutor on TcancionAutor.nCancionID = TCancion.nCancionID " +
               " INNER JOIN TAutor on TAutor.nAutorID = TCancionAutor.nAutorID ";
                SqlCommand comando = new SqlCommand(query, conn);
                SqlDataAdapter data = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                data.Fill(tabla);
                dataGridView3.DataSource = tabla;
                dataGridView3.Columns["cTitulo"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["nAnyoPublicacion"].ReadOnly = true; // solo lectura
                dataGridView3.Columns["cGeneroMusical"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["cNombre_Autor"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["cApellidos_Autor"].ReadOnly = true; // solo lectura
                this.dataGridView3.Columns["nCancionID"].Visible = false;
            }
            else if (textBox1.Text != "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
            {
                string query = "select TCancion.nCancionID,TCancion.cTitulo,TCancion.nAnyoPublicacion,TCancion.cGeneroMusical,TAutor.cNombre_Autor,TAutor.cApellidos_Autor " +
              " from TCancion " +
              " INNER JOIN TCancionAutor on TcancionAutor.nCancionID = TCancion.nCancionID " +
              " INNER JOIN TAutor on TAutor.nAutorID = TCancionAutor.nAutorID " +
              " where TCancion.cTitulo = @titulo ";
                SqlCommand comando = new SqlCommand(query, conn);
                comando.Parameters.AddWithValue("titulo", titulo);
                SqlDataAdapter data = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                data.Fill(tabla);
                dataGridView3.DataSource = tabla;
                dataGridView3.DataSource = tabla;
                dataGridView3.Columns["cTitulo"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["nAnyoPublicacion"].ReadOnly = true; // solo lectura
                dataGridView3.Columns["cGeneroMusical"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["cNombre_Autor"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["cApellidos_Autor"].ReadOnly = true; // solo lectura
            }
            else if (textBox1.Text == "" && textBox2.Text != "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
            {
                string query = "select TCancion.nCancionID,TCancion.cTitulo,TCancion.nAnyoPublicacion,TCancion.cGeneroMusical,TAutor.cNombre_Autor,TAutor.cApellidos_Autor " +
              " from TCancion " +
              " INNER JOIN TCancionAutor on TcancionAutor.nCancionID = TCancion.nCancionID " +
              " INNER JOIN TAutor on TAutor.nAutorID = TCancionAutor.nAutorID " +
              " where TCancion.nAnyoPublicacion = @anyo_pulicacion ";
                SqlCommand comando = new SqlCommand(query, conn);
                comando.Parameters.AddWithValue("anyo_pulicacion", anyo_pulicacion);
                SqlDataAdapter data = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                data.Fill(tabla);
                dataGridView3.DataSource = tabla;
                dataGridView3.DataSource = tabla;
                dataGridView3.Columns["cTitulo"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["nAnyoPublicacion"].ReadOnly = true; // solo lectura
                dataGridView3.Columns["cGeneroMusical"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["cNombre_Autor"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["cApellidos_Autor"].ReadOnly = true; // solo lectura
            }
            else if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text != "" && textBox4.Text == "" && textBox5.Text == "")
            {
                string query = "select TCancion.nCancionID,TCancion.cTitulo,TCancion.nAnyoPublicacion,TCancion.cGeneroMusical,TAutor.cNombre_Autor,TAutor.cApellidos_Autor " +
              " from TCancion " +
              " INNER JOIN TCancionAutor on TcancionAutor.nCancionID = TCancion.nCancionID " +
              " INNER JOIN TAutor on TAutor.nAutorID = TCancionAutor.nAutorID " +
              " where TCancion.cGeneroMusical = @genero_musical ";
                SqlCommand comando = new SqlCommand(query, conn);
                comando.Parameters.AddWithValue("genero_musical", genero_musical);
                SqlDataAdapter data = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                data.Fill(tabla);
                dataGridView3.DataSource = tabla;
                dataGridView3.DataSource = tabla;
                dataGridView3.Columns["cTitulo"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["nAnyoPublicacion"].ReadOnly = true; // solo lectura
                dataGridView3.Columns["cGeneroMusical"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["cNombre_Autor"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["cApellidos_Autor"].ReadOnly = true; // solo lectura
            }
            else if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text != "" && textBox5.Text == "")
            {
                string query = "select TCancion.nCancionID,TCancion.cTitulo,TCancion.nAnyoPublicacion,TCancion.cGeneroMusical,TAutor.cNombre_Autor,TAutor.cApellidos_Autor " +
              " from TCancion " +
              " INNER JOIN TCancionAutor on TcancionAutor.nCancionID = TCancion.nCancionID " +
              " INNER JOIN TAutor on TAutor.nAutorID = TCancionAutor.nAutorID " +
              " where TAutor.cNombre_Autor = @nombre_autor ";
                SqlCommand comando = new SqlCommand(query, conn);
                comando.Parameters.AddWithValue("nombre_autor", nombre_autor);
                SqlDataAdapter data = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                data.Fill(tabla);
                dataGridView3.DataSource = tabla;
                dataGridView3.DataSource = tabla;
                dataGridView3.Columns["cTitulo"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["nAnyoPublicacion"].ReadOnly = true; // solo lectura
                dataGridView3.Columns["cGeneroMusical"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["cNombre_Autor"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["cApellidos_Autor"].ReadOnly = true; // solo lectura
            }
            else if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text != "")
            {
                string query = "select TCancion.nCancionID,TCancion.cTitulo,TCancion.nAnyoPublicacion,TCancion.cGeneroMusical,TAutor.cNombre_Autor,TAutor.cApellidos_Autor " +
              " from TCancion " +
              " INNER JOIN TCancionAutor on TcancionAutor.nCancionID = TCancion.nCancionID " +
              " INNER JOIN TAutor on TAutor.nAutorID = TCancionAutor.nAutorID " +
              " where TAutor.cApellidos_Autor = @apellidos_autor ";
                SqlCommand comando = new SqlCommand(query, conn);
                comando.Parameters.AddWithValue("apellidos_autor", apellidos_autor);
                SqlDataAdapter data = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                data.Fill(tabla);
                dataGridView3.DataSource = tabla;
                dataGridView3.DataSource = tabla;
                dataGridView3.Columns["cTitulo"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["nAnyoPublicacion"].ReadOnly = true; // solo lectura
                dataGridView3.Columns["cGeneroMusical"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["cNombre_Autor"].ReadOnly = true;// solo lectura
                dataGridView3.Columns["cApellidos_Autor"].ReadOnly = true; // solo lectura
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //boton buscar ahora completa
            

            busqueda1(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox5.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //buscar cancion

            if (button2.Tag.ToString() == "cerrado")
            {
                button4.Visible = true;
                button1.Visible = true;
                textBox3.Visible = true;
                label5.Visible = true;
                textBox1.Visible = true;
                label4.Visible = true;
                textBox2.Visible = true;
                label3.Visible = true;
                label6.Visible = true;
                textBox4.Visible = true;
                label7.Visible = true;
                textBox5.Visible = true;
                button9.Visible = true;
                label28.Visible = false;
                button2.Tag = "abierto";
                
            }
            else if (button2.Tag.ToString() == "abierto")
            {

                button4.Visible = false;
                button1.Visible = false;
                textBox3.Visible = false;
                label5.Visible = false;
                textBox1.Visible = false;
                label4.Visible = false;
                textBox2.Visible = false;
                label3.Visible = false;
                label6.Visible = false;
                textBox4.Visible = false;
                label7.Visible = false;
                textBox5.Visible = false;
                button9.Visible = false;
                button2.Tag = "cerrado";
               
            }

        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //limpiar busqueda
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //cerrar sesion
            SoundPlayer simpleSound = new SoundPlayer();
            simpleSound.Stop();
            new Form1().Show();
            this.Close();
        }
        private void label8_Click(object sender, EventArgs e)
        {
            //NIF
        }
        private void label16_Click(object sender, EventArgs e)
        {
            //NIF
        }

        private void label9_Click(object sender, EventArgs e)
        {
            //Nombre
        }

        private void label17_Click(object sender, EventArgs e)
        {
            //Nombre
        }

        private void label10_Click(object sender, EventArgs e)
        {
            //Apellidos
        }

        private void label18_Click(object sender, EventArgs e)
        {
            //Apellidos
        }

        private void label11_Click(object sender, EventArgs e)
        {
            //email
        }

        private void label19_Click(object sender, EventArgs e)
        {
            //email
        }

        private void label12_Click(object sender, EventArgs e)
        {
            //telefono
        }

        private void label20_Click(object sender, EventArgs e)
        {
            //telefono
        }

        private void label13_Click(object sender, EventArgs e)
        {
            //direccion
        }

        private void label21_Click(object sender, EventArgs e)
        {
            //direccion
        }

        private void label14_Click(object sender, EventArgs e)
        {
            //fecha nacimiento
        }

        private void label22_Click(object sender, EventArgs e)
        {
            //fecha nacimiento
        }

        private void label15_Click(object sender, EventArgs e)
        {
            //fecha alta
        }

        private void label23_Click(object sender, EventArgs e)
        {
            //fecha alta
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //crear lista
            if (button7.Tag.ToString() == "cerrado")
            {
                textBox7.Visible = true;
                textBox8.Visible = true;
                textBox9.Visible = true;
                textBox10.Visible = true;
                label24.Visible = true;
                label25.Visible = true;
                label26.Visible = true;
                label27.Visible = true;
                button8.Visible = true;
                button7.Tag = "abierto";
            }
            else if (button7.Tag.ToString() == "abierto")
            {
                textBox7.Visible = false;
                textBox8.Visible = false;
                textBox9.Visible = false;
                textBox10.Visible = false;
                label24.Visible = false;
                label25.Visible = false;
                label26.Visible = false;
                label27.Visible = false;
                button8.Visible = false;
                button7.Tag = "cerrado";
            }
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //boton crear
            crearlista(this.textBox7.Text, this.textBox8.Text, this.textBox9.Text, this.textBox10.Text);
        }

      

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //stop
            SoundPlayer simpleSound = new SoundPlayer();
            simpleSound.Stop();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //pausa
            SoundPlayer simpleSound = new SoundPlayer();
            simpleSound.Stop();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //play
            reproduccion(nombrecancionn);
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            //nombre cancion a añadir en lista
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            //lista donde se alojara la cancion
        }

        private void label29_Click(object sender, EventArgs e)
        {
            //etiqueta de clickear cancion de añadir cacion a lista
        }

        private void label30_Click(object sender, EventArgs e)
        {
            //etiqueta de clickear lista de añadir cacion a lista
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //boton añadir de añadir cancion a lista

            añadircancion(this.textBox11.Text, this.textBox12.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.Tag.ToString() == "cerrado")
            {
                textBox11.Visible = true;
                textBox12.Visible = true;
                button10.Visible = true;
                label29.Visible = true;
                label30.Visible = true;
                button9.Tag = "abierto";
            }
            else if (button9.Tag.ToString() == "abierto")
            { 
            textBox11.Visible = false;
            textBox12.Visible = false;
            button10.Visible = false;
            label29.Visible = false;
            label30.Visible = false;
            button9.Tag = "cerrado";
            }
        }
    }
}
