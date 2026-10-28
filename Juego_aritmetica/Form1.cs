using System.Runtime.Intrinsics.X86;

namespace Juego_aritmetica
{
    public partial class Form1 : Form
    {
        float a = 0, b = 0, resultado = 0;
        int opee = 0, limite = 11, puntos = 0;
        char operador = ' ';
        public Form1()
        {
            InitializeComponent();
        }

        private void Seleccíon_operacion()
        {
            Random random = new Random();
            a = Random.Shared.Next(1, limite);
            b = Random.Shared.Next(1, limite);
            opee = Random.Shared.Next(1, 5);

            switch (opee)
            {
                case 1:
                    operador = '+';
                    resultado = a + b;
                    break;
                case 2:
                    operador = '-';
                    resultado = a - b;
                    break;
                case 3:
                    operador = '*';
                    resultado = a * b;
                    break;
                case 4:
                    operador = '/';
                    resultado = a / b;
                    break;
                default:
                    operador = ' ';
                    resultado = 0;
                    break;
            }
            n1.Text = a.ToString();
            n2.Text = b.ToString();
            rpuntaje.Text = operador.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Seleccíon_operacion();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (float.Parse(rresultado.Text).Equals(resultado))
            {
                MessageBox.Show("¡Correcto!");
                raciertos++;
                puntos += 5;
            }
            else
            {
                MessageBox.Show($"Incorrecto. \nEl resultado correcto es: {resultado}");
                rerrores++;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
