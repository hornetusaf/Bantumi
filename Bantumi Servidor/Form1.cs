using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;

/*
 * label9 modo:
 * label11 status:
 * label13 conectado a:
 * label7 jugador actual:
 * listBox1 servidores disponibles.
 * textBox1 mensajes.
 * textBox2 nombre.
 */

namespace Bantumi_Servidor
{
    public partial class Form1 : Form
    {
        int[] vec;
        Thread t1, t2, t3;
        bool bandservidor = false, bandcliente = false, disponible = false, conectado = false;
        string[] servidores = new string[50];
        List<string> items = new List<string>();
        int jugada = 0;
        int[] JugadasP_Prioridad;

        Socket receivesocket;
        IPEndPoint receive_ep;
        EndPoint Remote;
        TcpClient servidortcp;
        TcpListener listener;
        TcpClient clientetcp;
        NetworkStream streamservidor;
        NetworkStream streamcliente;

        public Form1()
        {
            InitializeComponent();
            button5.Enabled = false;
            button1.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            vec = new int[14] { 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 3, 3, 3, 0 };
            dibuja();
            JugadasP_Prioridad = new int[6] { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 50; i++)
                servidores[i] = null;
            listBox1.DataSource = items;
        }

        private void dibuja()//cambia imagenes
        {
            for (int i = 0; i < 14; i++)
            {
                if (vec[i] == 0)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen0;
                }

                if (vec[i] == 1)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen1;
                }

                if (vec[i] == 2)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen2;
                }

                if (vec[i] == 3)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen3;
                }

                if (vec[i] == 4)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen4;
                }

                if (vec[i] == 5)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen5;
                }

                if (vec[i] == 6)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen6;
                }

                if (vec[i] == 7)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen7;
                }

                if (vec[i] == 8)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen8;
                }

                if (vec[i] == 9)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen9;
                }

                if (vec[i] == 10)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen10;
                }

                if (vec[i] == 11)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen11;
                }

                if (vec[i] == 12)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen12;
                }

                if (vec[i] == 13)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen13;
                }

                if (vec[i] == 14)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen14;
                }

                if (vec[i] == 15)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen15;
                }

                if (vec[i] == 16)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen16;
                }

                if (vec[i] == 17)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen17;
                }

                if (vec[i] == 18)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen18;
                }

                if (vec[i] == 19)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen19;
                }

                if (vec[i] == 20)
                {
                    if (i == 0)
                        pictureBox1.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 1)
                        pictureBox2.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 2)
                        pictureBox3.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 3)
                        pictureBox4.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 4)
                        pictureBox5.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 5)
                        pictureBox6.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 6)
                        pictureBox7.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 7)
                        pictureBox8.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 8)
                        pictureBox9.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 9)
                        pictureBox10.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 10)
                        pictureBox11.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 11)
                        pictureBox12.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 12)
                        pictureBox13.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                    if (i == 13)
                        pictureBox14.BackgroundImage = global::Bantumi_Servidor.Properties.Resources.Imagen20;
                }
            }
        }

        private void servidor_envia_udp()//funcion modo servidor
        {
            while (bandservidor)
            {
                sendudp("255.255.255.255", "DISPONIBLE-" + label7.Text.ToString());
                Thread.Sleep(5000);
            }
        }

        private void servidor_recibe_udp()//funcion modo servidor
        {
            string comando, jugador, str, ip;
            byte[] data = new byte[1024];

            receivesocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            receive_ep = new IPEndPoint(IPAddress.Any, 50500);
            Remote = (EndPoint)receive_ep;
            receivesocket.Bind(Remote);

            while (bandservidor)
            {
                jugador = null; comando = null; ip = null;
                Console.WriteLine("Servidor esperando mensajes\n");
                int recv = receivesocket.ReceiveFrom(data, ref Remote);
                Console.WriteLine("Mensaje desde {0}:", Remote.ToString());
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                str = Encoding.ASCII.GetString(data, 0, recv);
                char[] seps = { '-', ' ' };
                string[] cadena = str.Split(seps);
                comando = cadena[0];

                if (cadena.Length > 1)
                    jugador = cadena[1];

                cadena = Remote.ToString().Split(':');
                ip = cadena[0];

                if (comando == "JUGAR")
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(
                            delegate()
                            {
                                textBox1.AppendText("Recibido:JUGAR-" + jugador + "\n");
                            }
                            ));
                    }
                    else
                    {
                        textBox1.AppendText("Recibido:" + jugador + "\n");
                    }

                    if (disponible)
                    {
                        sendudp(ip, "ACEPTAR");
                        conectar_tcp_servidor();
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new MethodInvoker(
                                delegate()
                                {
                                    label13.Text = jugador;
                                }
                                ));
                        }
                        else
                        {
                            label13.Text = jugador;
                        }
                    }
                    else
                        sendudp(ip, "RECHAZAR");
                }
            }
        }

        private void cliente_envia_udp()//funcion modo cliente
        {
            int selectedIndex = listBox1.SelectedIndex;
            string[] cadena = new string[10];
            string ip, nombre, str;
            str = items.ElementAt(selectedIndex).ToString();
            cadena = str.Split('-');
            nombre = cadena[0];
            ip = cadena[1];

            sendudp(ip, "JUGAR-" + label7.Text);
        }

        private void cliente_recibe_udp()//funcion modo cliente
        {
            string comando, nombre, str, ip;
            byte[] data = new byte[1024];
            int i;
            bool enc;

            receivesocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            receive_ep = new IPEndPoint(IPAddress.Any, 50500);
            Remote = (EndPoint)receive_ep;
            receivesocket.Bind(Remote);

            while (bandcliente)
            {
                nombre = null; comando = null; ip = null;
                Console.WriteLine("Cliente esperando mensajes\n");
                int recv = receivesocket.ReceiveFrom(data, ref Remote);
                Console.WriteLine("Mensaje desde {0}:", Remote.ToString());
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                str = Encoding.ASCII.GetString(data, 0, recv);
                char[] seps = { '-', ' '};
                string[] cadena = str.Split(seps);
                comando = cadena[0];
                if (cadena.Length > 1)
                    nombre = cadena[1];
                cadena = Remote.ToString().Split(':');
                ip = cadena[0];

                if (comando == "DISPONIBLE" && nombre != null)
                {
                    enc = false;
                    i = 0;
                    while (i < 50 && !enc)
                    {
                        if (nombre == servidores[i])
                        {
                            enc = true;
                            break;
                        }
                        i++;
                    }

                    if (!enc)
                    {
                        i = 0;
                        while (i < 50)
                        {
                            if (servidores[i] == null)
                            {
                                servidores[i] = nombre;
                                items.Add(nombre + '-' + ip);
                                if (this.InvokeRequired)
                                {
                                    this.Invoke(new MethodInvoker(
                                        delegate()
                                        {
                                            listBox1.DataSource = null;
                                            listBox1.DataSource = items;
                                        }
                                        ));
                                }
                                else
                                {
                                    listBox1.DataSource = null;
                                    listBox1.DataSource = items;
                                }
                                break;
                            }
                            i++;
                        }
                    }
                }

                if (comando == "ACEPTAR")
                {
                    int selectedIndex = 0;
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(
                            delegate()
                            {
                                textBox1.AppendText("Recibido:ACEPTAR\n");
                                selectedIndex = listBox1.SelectedIndex;
                                str = items.ElementAt(selectedIndex).ToString();
                                cadena = str.Split('-');
                                nombre = cadena[0];
                                ip = cadena[1];
                                label13.Text = nombre;
                                button1.Enabled = false;
                                button6.Enabled = true;
                            }
                            ));
                    }
                    else
                    {
                        textBox1.AppendText("Recibido:ACEPTAR\n");
                        selectedIndex = listBox1.SelectedIndex;
                        str = items.ElementAt(selectedIndex).ToString();
                        cadena = str.Split('-');
                        nombre = cadena[0];
                        ip = cadena[1];
                        label13.Text = nombre;
                        button1.Enabled = false;
                        button6.Enabled = true;
                    }

                    conectar_tcp_cliente(ip);
                }

                if (comando == "RECHAZAR")
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(
                            delegate()
                            {
                                textBox1.AppendText("Recibido:RECHAZAR\n");
                            }
                            ));
                    }
                    else
                    {
                        textBox1.AppendText("Recibido:RECHAZAR\n");
                    }
                }
            }
        }

        private void conectar_tcp_servidor()//funcion conecta tcp cuando modo servidor
        {
            disponible = false;
            conectado = true;
            servidortcp = new TcpClient();
            listener = new TcpListener(IPAddress.Any, 50400);
            listener.Start();
            servidortcp = listener.AcceptTcpClient();
            t3 = new Thread(new ThreadStart(escuchar_servidor));
            t3.Start();
        }

        private void escuchar_servidor()//funcion escucha crea hilo para recibir mensajes tcp cuando modo servidor
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate()
                    {
                        label11.Text = "conectado";
                    }
                    ));
            }
            else
            {
                label11.Text = "conectado";
            }

            byte[] data = new byte[1024];
            int recibe, cont = 0, i = 0;
            string str = null, comando = null;
            bool bandC = false, chequejo = false;
            streamservidor = servidortcp.GetStream();
            int[] vecpos = new int[15];

            for (i = 0; i < 15; i++)
                vecpos[i] = 0;

            envia_servidor("INICIO\n");

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate()
                    {
                        textBox1.AppendText("INICIANDO PARTIDA\n");
                    }
                    ));
            }
            else
            {
                textBox1.AppendText("INICIANDO PARTIDA\n");
            }

            jugada = 3;
            mover();
            vecpos[cont] = jugada;
            cont++;

            jugada = 4;
            mover();
            vecpos[cont] = jugada;
            cont++;

            for (i = 0; i < cont; i++)
            {
                envia_servidor("JUGADA-" + vecpos[i].ToString() + "\n");
                Thread.Sleep(200);
            }

            envia_servidor("FIN-JUGADA\n");

            while (conectado)
            {
                for (i = 0; i < 15; i++)
                    vecpos[i] = 0;
                cont = 0;

                do
                {
                    recibe = streamservidor.Read(data, 0, 1024);
                    str = Encoding.ASCII.GetString(data, 0, recibe);

                    if (str == "FIN-JUEGO\n")
                        break;

                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(
                            delegate()
                            {
                                textBox1.AppendText("Recibido:" + str + "\n");
                            }
                            ));
                    }
                    else
                    {
                        textBox1.AppendText("Recibido:" + str + "\n");
                    }

                    char[] seps = { '-', '\n' };
                    string[] cadena = str.Split(seps);
                    comando = cadena[0];
                    if (cadena.Length > 1 && cadena[1] != "JUGADA" && cadena[1] != "")
                        jugada = Convert.ToInt32(cadena[1]);

                    if (comando == "JUGADA")
                    {
                        vecpos[cont] = jugada + 7;
                        cont++;
                    }

                } while (str != "FIN-JUGADA\n");

                if (str == "FIN-JUEGO\n")
                {
                    conectado = false;
                    break;
                }

                for (i = 0; i < cont; i++)
                {
                    jugada = vecpos[i];

                    if (jugaroponente() == true)
                    {
                        if (bandservidor)
                            envia_servidor("FIN-JUEGO\n");
                        else
                            envia_cliente("FIN-JUEGO\n");

                        for (i = 0; i < 14; i++)
                        {
                            if (i == 6 || i == 13)
                            {
                                vec[i] = 0;
                            }
                            else
                                vec[i] = 3;
                        }

                        chequejo = true;
                        conectado = false;
                        break;
                    }
                    mover();

                    if (ganar() == true)
                    {
                        if (vec[13] >= 18)
                            MessageBox.Show("GANADOR:" + label13.Text + "\n" + "PERDEDOR:" + label7.Text);

                        if (vec[6] >= 18)
                            MessageBox.Show("GANADOR:" + label7.Text + "\n" + "PERDEDOR:" + label13.Text);

                        for (i = 0; i < 14; i++)
                        {
                            if (i == 6 || i == 13)
                            {
                                vec[i] = 0;
                            }
                            else
                                vec[i] = 3;
                        }

                        dibuja();

                        break;
                    }
                }

                if (ganar() == true)
                {
                    envia_servidor("FIN-JUEGO\n");
                    conectado = false;
                    break;
                }

                if (chequejo == true)
                {
                    break;
                }

                for (i = 0; i < 15; i++)
                    vecpos[i] = 0;
                cont = 0;

                inicializar();
                jugar();
                escogerjugada();
                mover();
                vecpos[cont] = jugada;
                cont++;

                if (ganar() == true)
                {
                    if (vec[13] >= 18)
                        MessageBox.Show("GANADOR:" + label13.Text + "\n" + "PERDEDOR:" + label7.Text);

                    if (vec[6] >= 18)
                        MessageBox.Show("GANADOR:" + label7.Text + "\n" + "PERDEDOR:" + label13.Text);

                    for (i = 0; i < 14; i++)
                    {
                        if (i == 6 || i == 13)
                        {
                            vec[i] = 0;
                        }
                        else
                            vec[i] = 3;
                    }

                    dibuja();

                    conectado = false;
                    envia_servidor("FIN-JUEGO\n");
                    break;
                }
                else
                {
                    do
                    {
                        if (JugadasP_Prioridad[jugada] == 1)
                        {
                            bandC = true;
                            inicializar();
                            jugar();
                            escogerjugada();
                            mover();
                            vecpos[cont] = jugada;
                            cont++;

                            if (ganar() == true)
                            {
                                if (vec[13] >= 18)
                                    MessageBox.Show("GANADOR:" + label13.Text + "\n" + "PERDEDOR:" + label7.Text);

                                if (vec[6] >= 18)
                                    MessageBox.Show("GANADOR:" + label7.Text + "\n" + "PERDEDOR:" + label13.Text);

                                for (i = 0; i < 14; i++)
                                {
                                    if (i == 6 || i == 13)
                                    {
                                        vec[i] = 0;
                                    }
                                    else
                                        vec[i] = 3;
                                }

                                dibuja();

                                conectado = false;
                                envia_servidor("FIN-JUEGO\n");
                                break;
                            }
                        }
                        else
                            bandC = false;

                    } while (bandC == true);
                }

                for (i = 0; i < cont; i++)
                {
                    envia_servidor("JUGADA-" + vecpos[i].ToString()+"\n");
                    Thread.Sleep(200);
                }

                envia_servidor("FIN-JUGADA\n");
            }

            servidortcp.Close();
            listener.Stop();
            disponible = true;

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate()
                    {
                        if (!bandservidor)
                            label11.Text = "********************";
                        else
                            label11.Text = "Disponible";

                        label13.Text = "********************";
                    }
                    ));
            }
            else
            {
                if (!bandservidor)
                    label11.Text = "********************";
                else
                    label11.Text = "Disponible";

                label13.Text = "********************";
            }

            t3.Abort();
        }

        public void envia_servidor(String datos)//funcion envia mensajes tcp cuando modo servidor
        {
            byte[] data = Encoding.ASCII.GetBytes(datos);
            NetworkStream stream = servidortcp.GetStream();
            if (datos == "FIN-JUEGO\n")
            {
                conectado = false;
                stream.Flush();
            }
            else
                stream.Write(data, 0, data.Length);

            stream.Flush();

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate()
                    {
                        textBox1.AppendText("Enviado:" + datos + "\n");
                    }
                    ));
            }
            else
            {
                textBox1.AppendText("Enviado:" + datos + "\n");
            }
        }

        private void conectar_tcp_cliente(string ip)//funcion conecta tcp cuando modo cliente
        {
            conectado = true;
            clientetcp = new TcpClient();
            clientetcp.Connect(ip, 50400);
            t3 = new Thread(new ThreadStart(escuchar_cliente));
            t3.Start();
        }

        public void escuchar_cliente()//funcion escucha crea hilo para recibir mensajes tcp cuando modo cliente
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate()
                    {
                        label11.Text = "conectado";
                    }
                    ));
            }
            else
            {
                label11.Text = "conectado";
            }

            byte[] data = new byte[1024]; ;
            int recibe = 0, i = 0, cont = 0;
            string str = null, comando = null;
            bool bandC = false, chequeojo = false;
            int[] vecpos = new int[15];

            for (i = 0; i < 15; i++)
                vecpos[i] = 0;

            streamcliente = clientetcp.GetStream();

            while (conectado && clientetcp.Connected)
            {
                for (i = 0; i < 15; i++)
                    vecpos[i] = 0;
                cont = 0;

                do
                {
                    recibe = streamcliente.Read(data, 0, 1024);
                    str = Encoding.ASCII.GetString(data, 0, recibe);

                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(
                            delegate()
                            {
                                textBox1.AppendText("Recibido:" + str + "\n");
                            }
                            ));
                    }
                    else
                    {
                        textBox1.AppendText("Recibido:" + str + "\n");
                    }

                    if (str == "FIN-JUEGO\n")
                        break;

                    char[] seps = { '-','\n' };
                    string[] cadena = str.Split(seps);
                    comando = cadena[0];
                    if (cadena.Length > 1 && cadena[1] != "JUGADA" && cadena[1] != "")
                        jugada = Convert.ToInt32(cadena[1]);

                    if (comando == "INICIO")
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new MethodInvoker(
                                delegate()
                                {
                                    textBox1.AppendText("INICIANDO PARTIDA\n");
                                }
                                ));
                        }
                        else
                        {
                            textBox1.AppendText("INICIANDO PARTIDA\n");
                        }
                    }

                    if (comando == "JUGADA")
                    {
                        vecpos[cont] = jugada + 7;
                        cont++;
                    }

                } while (str != "FIN-JUGADA\n");

                if (str == "FIN-JUEGO\n")
                {
                    conectado = false;
                    break;
                }

                for (i = 0; i < cont; i++)
                {
                    jugada = vecpos[i];

                    if (jugaroponente() == true)
                    {
                        if (bandservidor)
                            envia_servidor("FIN-JUEGO\n");
                        else
                            envia_cliente("FIN-JUEGO\n");

                        for (i = 0; i < 14; i++)
                        {
                            if (i == 6 || i == 13)
                            {
                                vec[i] = 0;
                            }
                            else
                                vec[i] = 3;
                        }

                        chequeojo = true;
                        conectado = false;
                        break;
                    }

                    mover();

                    if (ganar() == true)
                    {
                        if (vec[13] >= 18)
                            MessageBox.Show("GANADOR:" + label13.Text + "\n" + "PERDEDOR:" + label7.Text);

                        if (vec[6] >= 18)
                            MessageBox.Show("GANADOR:" + label7.Text + "\n" + "PERDEDOR:" + label13.Text);

                        for (i = 0; i < 14; i++)
                        {
                            if (i == 6 || i == 13)
                            {
                                vec[i] = 0;
                            }
                            else
                                vec[i] = 3;
                        }

                        dibuja();

                        break;
                    }
                }

                if (ganar() == true)
                {
                    envia_cliente("FIN-JUEGO\n");
                    conectado = false;
                    break;
                }

                if (chequeojo == true)
                {
                    break;
                }

                for (i = 0; i < 15; i++)
                    vecpos[i] = 0;
                cont = 0;

                inicializar();
                jugar();
                escogerjugada();
                mover();
                vecpos[cont] = jugada;
                cont++;

                if (ganar() == true)
                {
                    if (vec[13] >= 18)
                        MessageBox.Show("GANADOR:" + label13.Text + "\n" + "PERDEDOR:" + label7.Text);

                    if (vec[6] >= 18)
                        MessageBox.Show("GANADOR:" + label7.Text + "\n" + "PERDEDOR:" + label13.Text);

                    for (i = 0; i < 14; i++)
                    {
                        if (i == 6 || i == 13)
                        {
                            vec[i] = 0;
                        }
                        else
                            vec[i] = 3;
                    }

                    dibuja();

                    conectado = false;
                    envia_cliente("FIN-JUEGO\n");
                    break;
                }
                else
                {
                    do
                    {
                        if (JugadasP_Prioridad[jugada] == 1)
                        {
                            bandC = true;
                            inicializar();
                            jugar();
                            escogerjugada();
                            mover();
                            vecpos[cont] = jugada;
                            cont++;

                            if (ganar() == true)
                            {
                                if (vec[13] >= 18)
                                    MessageBox.Show("GANADOR:" + label13.Text + "\n" + "PERDEDOR:" + label7.Text);

                                if (vec[6] >= 18)
                                    MessageBox.Show("GANADOR:" + label7.Text + "\n" + "PERDEDOR:" + label13.Text);

                                for (i = 0; i < 14; i++)
                                {
                                    if (i == 6 || i == 13)
                                    {
                                        vec[i] = 0;
                                    }
                                    else
                                        vec[i] = 3;
                                }

                                dibuja();

                                conectado = false;
                                envia_cliente("FIN-JUEGO\n");
                                break;
                            }
                        }
                        else
                            bandC = false;

                    } while (bandC == true);
                }

                for (i = 0; i < cont; i++)
                {
                    envia_cliente("JUGADA-" + vecpos[i].ToString() + "\n");
                    Thread.Sleep(200);
                }

                envia_cliente("FIN-JUGADA\n");
            }

            clientetcp.Close();

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate()
                    {
                        if (!bandcliente)
                            label11.Text = "********************";
                        else
                            label11.Text = "Desconectado";

                        label13.Text = "********************";
                        button6.Enabled = false;
                        button1.Enabled = true;
                    }
                    ));
            }
            else
            {
                if (!bandcliente)
                    label11.Text = "********************";
                else
                    label11.Text = "Desconectado";

                label13.Text = "********************";
                button6.Enabled = false;
                button1.Enabled = true;
            }

            t3.Abort();
        }

        public void envia_cliente(String datos)//funcion envia mensajes tcp cuando modo cliente
        {
            byte[] data = Encoding.ASCII.GetBytes(datos);
            NetworkStream stream = clientetcp.GetStream();
            if (datos == "FIN-JUEGO\n")
            {
                conectado = false;
                stream.Flush();
            }
            else
                stream.Write(data, 0, data.Length);
            
            stream.Flush();

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate()
                    {
                        textBox1.AppendText("Enviado:" + datos + "\n");
                    }
                    ));
            }
            else
            {
                textBox1.AppendText("Enviado:" + datos + "\n");
            }
        }

        private void reset()// funcion resetea todo.
        {
            for (int i = 0; i < 14; i++)
            {
                if (i == 6 || i == 13)
                {
                    vec[i] = 0;
                }
                else
                    vec[i] = 3;
            }

            dibuja();

            if (bandservidor)
            {
                bandservidor = false;
                if (conectado)
                {
                    envia_servidor("FIN-JUEGO\n");
                    servidortcp.Close();
                    listener.Stop();
                }

                t1.Abort();
                t2.Abort();
                receivesocket.Close();
            }

            if (bandcliente)
            {
                bandcliente = false;
                if (conectado)
                {
                    envia_cliente("FIN-JUEGO\n");
                    clientetcp.Close();
                }

                t2.Abort();
                receivesocket.Close();
            }

            label9.Text = "********************";
            label11.Text = "********************";
            label13.Text = "********************";
            label7.Text = "********************";
            conectado = false;
            disponible = false;
            button1.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = false;

            for (int i = 0; i < 50; i++)
                servidores[i] = null;

            listBox1.DataSource = null;
            listBox1.Items.Clear();
            items.Clear();
            listBox1.DataSource = items;
            textBox1.Clear();
        }

        private void sendudp(string ip, string mensaje)//funcion envia mensajes udp
        {
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(mensaje);
            Socket sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sendSocket.EnableBroadcast = true;
            IPEndPoint send_ep = new IPEndPoint(IPAddress.Parse(ip), 50500);
            sendSocket.SendTo(data, send_ep);
            sendSocket.Close();
        }

        private void mover()
        {
            int semillas = 0, j = 0;
            bool band = false;

            semillas = vec[jugada];
            j = jugada;
            vec[jugada] = 0;
            Thread.Sleep(1000);
            dibuja();

            do
            {
                j++;
                if (j == 14)
                    j = 0;

                if (jugada > 6 && j == 6 && jugada < 13)
                    j = 7;

                if (jugada < 6 && j == 13 && jugada > -1)
                    j = 0;

                semillas--;

                if (jugada < 6 && jugada > -1)
                {
                    if (vec[j] == 0 && semillas == 0 && j < 6 && j > -1)////Robar al Oponente///// 
                    {
                        band = true;

                        if (j == 0)
                        {
                            if (vec[12] > 0)
                            {
                                vec[6] = vec[6] + vec[12] + 1;
                                vec[12] = 0;
                            }
                            else
                                vec[6]++;
                        }

                        if (j == 1)
                        {
                            if (vec[11] > 0)
                            {
                                vec[6] = vec[6] + vec[11] + 1;
                                vec[11] = 0;
                            }
                            else
                                vec[6]++;
                        }

                        if (j == 2)
                        {
                            if (vec[10] > 0)
                            {
                                vec[6] = vec[6] + vec[10] + 1;
                                vec[10] = 0;
                            }
                            else
                                vec[6]++;
                        }

                        if (j == 3)
                        {
                            if (vec[9] > 0)
                            {
                                vec[6] = vec[6] + vec[9] + 1;
                                vec[9] = 0;
                            }
                            else
                                vec[6]++;
                        }

                        if (j == 4)
                        {
                            if (vec[8] > 0)
                            {
                                vec[6] = vec[6] + vec[8] + 1;
                                vec[8] = 0;
                            }
                            else
                                vec[6]++;
                        }

                        if (j == 5)
                        {
                            if (vec[7] > 0)
                            {
                                vec[6] = vec[6] + vec[7] + 1;
                                vec[7] = 0;
                            }
                            else
                                vec[6]++;
                        }
                    }
                }
                /////////////////////////para jugada>6///////////////////////////////////////

                Thread.Sleep(1000);
                dibuja();

                if (jugada > 6 && jugada < 13)
                {
                    if (vec[j] == 0 && semillas == 0 && j > 6 && j < 13)
                    {
                        band = true;

                        if (j == 7)
                        {
                            if (vec[5] > 0)
                            {
                                vec[13] = vec[13] + vec[5] + 1;
                                vec[5] = 0;
                            }
                            else
                                vec[13]++;
                        }

                        if (j == 8)
                        {
                            if (vec[4] > 0)
                            {
                                vec[13] = vec[13] + vec[4] + 1;
                                vec[4] = 0;
                            }
                            else
                                vec[13]++;
                        }

                        if (j == 9)
                        {
                            if (vec[3] > 0)
                            {
                                vec[13] = vec[13] + vec[3] + 1;
                                vec[3] = 0;
                            }
                            else
                                vec[13]++;
                        }

                        if (j == 10)
                        {
                            if (vec[2] > 0)
                            {
                                vec[13] = vec[13] + vec[2] + 1;
                                vec[2] = 0;
                            }
                            else
                                vec[13]++;
                        }

                        if (j == 11)
                        {
                            if (vec[1] > 0)
                            {
                                vec[13] = vec[13] + vec[1] + 1;
                                vec[1] = 0;
                            }
                            else
                                vec[13]++;
                        }

                        if (j == 12)
                        {
                            if (vec[0] > 0)
                            {
                                vec[13] = vec[13] + vec[0] + 1;
                                vec[0] = 0;
                            }
                            else
                                vec[13]++;
                        }
                    }
                }

                /////////////////finalizar robar oponente y colocar en el recipiente la ultima////////////////////

                if (band == false)
                    vec[j]++;

                Thread.Sleep(1000);
                dibuja();

            } while (semillas > 0);

        }

        private void jugar()
        {
            int semillas = 0;
            bool bandRTO = false, bandROO = false, bandRO = false;

            for (int i = 0; i < 6; i++)
            {
                if (vec[i] > 0)///Jugadas disponibles///
                {
                    bandRTO = false;
                    bandROO = false;
                    bandRO = false;

                    semillas = vec[i];
                    if ((i + semillas) == 6)////Repite Turno ///////
                    {
                        JugadasP_Prioridad[i] = 1;
                    }
                    else
                    {
                        if (i == 0)
                        {
                            if (vec[12] == 0)
                            {
                                int cont = 1;

                                for (int j = 11; j > 6; j--)
                                {
                                    if (vec[j] == cont)///////solo robar oponente///////
                                    {
                                        bandRO = true;
                                        break;
                                    }
                                    cont++;
                                }
                            }

                        }/////para i=0


                        if (i == 1)
                        {
                            if (vec[11] == 2)////////repite turno oponente////////
                            {
                                int cont = 1;
                                bandRTO = true;

                                for (int j = 10; j > 6; j--)
                                {
                                    if (j > 11)
                                    {
                                        if (vec[j] == (11 + cont))
                                        {
                                            bandROO = true;
                                            break;
                                        }
                                    }

                                    if (j < 11)
                                    {
                                        if (vec[j] == cont)///////robar oponente///////
                                        {
                                            bandROO = true;
                                            break;
                                        }
                                    }
                                    if (j == 11)
                                    {
                                        cont = 1;
                                    }

                                    cont++;
                                }
                            }
                            else
                            {
                                if (vec[11] == 0)
                                {
                                    int cont = 1;

                                    for (int j = 12; j > 6; j--)
                                    {
                                        if (j > 11)
                                        {
                                            if (vec[j] == (11 + cont))
                                            {
                                                bandRO = true;
                                                break;
                                            }
                                        }

                                        if (j < 11)
                                        {
                                            if (vec[j] == cont)///////robar oponente///////
                                            {
                                                bandRO = true;
                                                break;
                                            }
                                        }
                                        if (j == 11)
                                        {
                                            cont = 1;
                                        }

                                        cont++;
                                    }
                                }
                            }
                        }/////para i=1

                        if (i == 2)
                        {
                            if (vec[10] == 3)////////repite turno oponente////////
                            {
                                int cont = 1;
                                bandRTO = true;

                                for (int j = 12; j > 6; j--)
                                {
                                    if (j > 10)
                                    {
                                        if (vec[j] == (10 + cont))
                                        {
                                            bandROO = true;
                                            break;
                                        }
                                    }

                                    if (j < 10)
                                    {
                                        if (vec[j] == cont)///////robar oponente///////
                                        {
                                            bandROO = true;
                                            break;
                                        }
                                    }
                                    if (j == 10)
                                    {
                                        cont = 1;
                                    }

                                    cont++;
                                }
                            }
                            else
                            {
                                if (vec[10] == 0)
                                {
                                    int cont = 1;

                                    for (int j = 12; j > 6; j--)
                                    {
                                        if (j > 10)
                                        {
                                            if (vec[j] == (10 + cont))
                                            {
                                                bandRO = true;
                                                break;
                                            }
                                        }

                                        if (j < 10)
                                        {
                                            if (vec[j] == cont)///////robar oponente///////
                                            {
                                                bandRO = true;
                                                break;
                                            }
                                        }
                                        if (j == 10)
                                        {
                                            cont = 1;
                                        }

                                        cont++;
                                    }
                                }
                            }
                        }/////para i=2

                        if (i == 3)
                        {
                            if (vec[9] == 4)////////repite turno oponente////////
                            {
                                int cont = 1;
                                bandRTO = true;

                                for (int j = 12; j > 6; j--)
                                {
                                    if (j > 9)
                                    {
                                        if (vec[j] == (9 + cont))
                                        {
                                            bandROO = true;
                                            break;
                                        }
                                    }

                                    if (j < 9)
                                    {
                                        if (vec[j] == cont)///////robar oponente///////
                                        {
                                            bandROO = true;
                                            break;
                                        }
                                    }
                                    if (j == 9)
                                    {
                                        cont = 1;
                                    }

                                    cont++;
                                }
                            }
                            else
                            {
                                if (vec[9] == 0)
                                {
                                    int cont = 1;

                                    for (int j = 12; j > 6; j--)
                                    {
                                        if (j > 9)
                                        {
                                            if (vec[j] == (9 + cont))
                                            {
                                                bandRO = true;
                                                break;
                                            }
                                        }

                                        if (j < 9)
                                        {
                                            if (vec[j] == cont)///////robar oponente///////
                                            {
                                                bandRO = true;
                                                break;
                                            }
                                        }
                                        if (j == 9)
                                        {
                                            cont = 1;
                                        }

                                        cont++;
                                    }
                                }
                            }
                        }/////para i=3

                        if (i == 4)
                        {
                            if (vec[8] == 5)////////repite turno oponente////////
                            {
                                int cont = 1;
                                bandRTO = true;

                                for (int j = 12; j > 6; j--)
                                {
                                    if (j > 8)
                                    {
                                        if (vec[j] == (8 + cont))
                                        {
                                            bandROO = true;
                                            break;
                                        }
                                    }

                                    if (j < 8)
                                    {
                                        if (vec[j] == cont)///////robar oponente///////
                                        {
                                            bandROO = true;
                                            break;
                                        }
                                    }
                                    if (j == 8)
                                    {
                                        cont = 1;
                                    }

                                    cont++;
                                }
                            }
                            else
                            {
                                if (vec[8] == 0)
                                {
                                    int cont = 1;

                                    for (int j = 12; j > 6; j--)
                                    {
                                        if (j > 8)
                                        {
                                            if (vec[j] == (8 + cont))
                                            {
                                                bandRO = true;
                                                break;
                                            }
                                        }

                                        if (j < 8)
                                        {
                                            if (vec[j] == cont)///////robar oponente///////
                                            {
                                                bandRO = true;
                                                break;
                                            }
                                        }
                                        if (j == 8)
                                        {
                                            cont = 1;
                                        }

                                        cont++;
                                    }
                                }
                            }
                        }/////para i=4

                        if (i == 5)
                        { /////no ocurre ninguna posibilidad de robo//////
                            if (vec[7] == 6)////////repite turno oponente////////
                            {
                                int cont = 1;
                                bandRTO = true;

                                for (int j = 12; j < 6; j++)
                                {
                                    if (vec[j] == (7 + cont))
                                    {
                                        bandROO = true;
                                        break;
                                    }
                                    cont++;
                                }
                            }
                            else
                            {
                                if (vec[7] == 0)
                                {
                                    int cont = 1;

                                    for (int j = 12; j < 6; j++)
                                    {
                                        if (vec[j] == (7 + cont))
                                        {
                                            bandROO = true;
                                            break;
                                        }
                                        cont++;
                                    }
                                }
                            }
                        }

                        if (bandRTO == true && bandROO == true)
                        {
                            JugadasP_Prioridad[i] = 2;
                        }
                        else
                        {
                            if (bandRO == true)
                            {
                                JugadasP_Prioridad[i] = 3;
                            }
                            else
                            { //////posibilidad de robarle al oponente//////
                                if ((i + semillas) < 6)
                                {
                                    if (vec[i + semillas] == 0)
                                    {

                                        if ((i + semillas) == 1)
                                        {
                                            if (vec[11] > 0)////ay robo////
                                            {
                                                JugadasP_Prioridad[i] = 4;
                                            }
                                            else
                                            {
                                                JugadasP_Prioridad[i] = 6;
                                            }
                                        }/////para i+semillas=1

                                        if ((i + semillas) == 2)
                                        {
                                            if (vec[10] > 0)////ay robo////
                                            {
                                                JugadasP_Prioridad[i] = 4;
                                            }
                                            else
                                            {
                                                JugadasP_Prioridad[i] = 6;
                                            }
                                        }/////para i+semillas=2

                                        if ((i + semillas) == 3)
                                        {
                                            if (vec[9] > 0)////ay robo////
                                            {
                                                JugadasP_Prioridad[i] = 4;
                                            }
                                            else
                                            {
                                                JugadasP_Prioridad[i] = 6;
                                            }
                                        }/////para i+semillas=3

                                        if ((i + semillas) == 4)
                                        {
                                            if (vec[8] > 0)////ay robo////
                                            {
                                                JugadasP_Prioridad[i] = 4;
                                            }
                                            else
                                            {
                                                JugadasP_Prioridad[i] = 6;
                                            }
                                        }/////para i+semillas=4

                                        if ((i + semillas) == 5)
                                        {
                                            if (vec[7] > 0)////ay robo////
                                            {
                                                JugadasP_Prioridad[i] = 4;
                                            }
                                            else
                                            {
                                                JugadasP_Prioridad[i] = 6;
                                            }
                                        }/////para i+semillas=5
                                    }
                                }
                                else /////posibilidad de jugar la ultima de la izquierda hacia la derecha.... //////
                                {

                                    for (int j = 0; j < 6; j++)
                                    {
                                        if (JugadasP_Prioridad[j] == 0 && vec[j] > 0)
                                        {
                                            JugadasP_Prioridad[j] = 5;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void escogerjugada()
        {
            int menor = 10, i, j, cont = 0;
            bool band = false;
            for (i = 0; i < 6; i++)
            {
                if (JugadasP_Prioridad[i] < menor && JugadasP_Prioridad[i] > 0)
                {
                    menor = JugadasP_Prioridad[i];
                    jugada = i;
                }
            }

            if (menor == 5)
            {
                int mayor = 0;
                for (j = 0; j < 6; j++)
                {
                    if (JugadasP_Prioridad[j] == 5)
                    {
                        if (vec[j] > mayor)
                            mayor = vec[j];
                    }
                }

                cont = 0;
                for (j = 6; j > 0; j--)
                {
                    if (vec[j] == mayor)
                    {
                        jugada = j;
                        break;
                    }
                    else
                        cont = cont++;
                }

                for (i = 0; i < 6; i++)
                {
                    if (JugadasP_Prioridad[i] == 1)
                    {
                        band = true;
                    }

                    if (band == true)
                    {
                        if (JugadasP_Prioridad[i] == 3)
                        {
                            jugada = i;
                        }
                    }
                }
            }
        }

        private bool jugaroponente()
        {
            if (jugada < 7 || jugada > 12)
            {
                MessageBox.Show("Jugada invalida pos:" + jugada);
                return true;
            }
            else
                if (vec[jugada] == 0)
                {
                    MessageBox.Show("Jugada invalida no tiene semillas");
                    return true;
                }
                else
                    return false;
        }

        private void inicializar()
        {
            JugadasP_Prioridad = new int[6] { 0, 0, 0, 0, 0, 0 };
        }

        private bool ganar()
        {
            bool bandc = false, bando = false, band = false;

            for (int i = 0; i < 6; i++)
            {
                if (vec[i] != 0)
                    bandc = true;
                if (vec[i + 7] != 0)
                    bando = true;
            }

            if (bandc == false || bando == false)
                band = true;
            if (vec[13] >= 18 || vec[6] >= 18)
                band = true;

            return band;
        }

        private void button1_Click(object sender, EventArgs e)//boton conectar
        {
            int selectedindex = listBox1.SelectedIndex;
            if (!conectado && selectedindex > -1)
                cliente_envia_udp();
        }

        private void button3_Click(object sender, EventArgs e)//boton servidor
        {
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = true;
            bandservidor = true;
            disponible = true;
            label9.Text = "Servidor";
            label11.Text = "Desconectado";
            t1 = new Thread(new ThreadStart(servidor_envia_udp));
            t1.Start();
            t2 = new Thread(new ThreadStart(servidor_recibe_udp));
            t2.Start();

        }

        private void button4_Click(object sender, EventArgs e)//boton cliente
        {
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = true;
            button1.Enabled = true;
            button7.Enabled = true;
            bandcliente = true;
            label9.Text = "Cliente";
            label11.Text = "Desconectado";
            t2 = new Thread(new ThreadStart(cliente_recibe_udp));
            t2.Start();
        }

        private void button5_Click(object sender, EventArgs e)//boton reset
        {
            reset();
        }

        private void button2_Click(object sender, EventArgs e)//boton ok
        {
            label7.Text = textBox2.Text;
            textBox2.Clear();
        }

        private void button7_Click(object sender, EventArgs e)// boton refresh
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            items.Clear();
            for (int i = 0; i < 50; i++)
                servidores[i] = null;
            listBox1.DataSource = items;
        }

        private void button6_Click(object sender, EventArgs e)//boton desconectar
        {
            envia_cliente("FIN-JUEGO\n");
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)//tecla enter para la casilla nombre
        {
            if (e.KeyCode == Keys.Enter)
            {
                label7.Text = textBox2.Text;
                textBox2.Clear();
            }
        }
    }
}
