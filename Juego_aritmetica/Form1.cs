using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Juego_aritmetica
{
    public enum Modo
    {
        Individual,
        DosJugadores
    }

    public sealed record Operacion(int A, char Operador, int B, int Resultado)
    {
        public string Simbolo => Operador switch
        {
            '*' => "×",
            '/' => "÷",
            _ => Operador.ToString()
        };
    }

    public static class MotorMatematico
    {
        public const int AciertosPorNivel = 3;
        public const int NivelMaximo = 10;

        static readonly char[] Simbolos = { '+', '-', '*', '/' };

        public static int CalcularNivel(int aciertos) =>
            Math.Min(NivelMaximo, 1 + aciertos / AciertosPorNivel);

        public static int PuntosPorAcierto(int nivel) => 5 * nivel;

        public static Operacion Generar(int nivel)
        {
            int limite = 10 * nivel;
            int factor = Math.Min(4 + 2 * nivel, 20);
            int disponibles = Math.Min(nivel + 1, Simbolos.Length);
            char operador = Simbolos[Random.Shared.Next(disponibles)];
            int a, b;

            switch (operador)
            {
                case '+':
                    a = Random.Shared.Next(1, limite + 1);
                    b = Random.Shared.Next(1, limite + 1);
                    return new Operacion(a, operador, b, a + b);
                case '-':
                    a = Random.Shared.Next(1, limite + 1);
                    b = Random.Shared.Next(1, limite + 1);
                    if (a < b)
                    {
                        (a, b) = (b, a);
                    }
                    return new Operacion(a, operador, b, a - b);
                case '*':
                    a = Random.Shared.Next(2, factor + 1);
                    b = Random.Shared.Next(2, factor + 1);
                    return new Operacion(a, operador, b, a * b);
                default:
                    b = Random.Shared.Next(2, factor + 1);
                    int cociente = Random.Shared.Next(1, factor + 1);
                    return new Operacion(b * cociente, operador, b, cociente);
            }
        }
    }

    public sealed class Jugador
    {
        public Jugador(string nombre, int vidas)
        {
            Nombre = nombre;
            Vidas = vidas;
        }

        public string Nombre { get; }
        public int Vidas { get; private set; }
        public int Puntos { get; private set; }
        public int Aciertos { get; private set; }
        public int Errores { get; private set; }
        public int Nivel => MotorMatematico.CalcularNivel(Aciertos);
        public bool Activo => Vidas > 0;

        public void RegistrarAcierto()
        {
            Puntos += MotorMatematico.PuntosPorAcierto(Nivel);
            Aciertos++;
        }

        public void RegistrarError()
        {
            Errores++;
            Vidas--;
        }
    }

    public sealed class Marcador
    {
        public const int Capacidad = 50;

        readonly string[] nombres = new string[Capacidad];
        readonly int[] puntajes = new int[Capacidad];
        readonly int[] aciertos = new int[Capacidad];
        int total;

        public int Total => total;

        public string NombreEn(int indice) => nombres[indice];
        public int PuntajeEn(int indice) => puntajes[indice];
        public int AciertosEn(int indice) => aciertos[indice];

        public void Registrar(string nombre, int puntos, int cantidadAciertos)
        {
            if (total == Capacidad)
            {
                for (int i = 1; i < Capacidad; i++)
                {
                    nombres[i - 1] = nombres[i];
                    puntajes[i - 1] = puntajes[i];
                    aciertos[i - 1] = aciertos[i];
                }
                total--;
            }

            nombres[total] = nombre;
            puntajes[total] = puntos;
            aciertos[total] = cantidadAciertos;
            total++;
        }

        public int MejorPuntaje(string nombre)
        {
            int mejor = -1;
            for (int i = 0; i < total; i++)
            {
                if (string.Equals(nombres[i], nombre, StringComparison.OrdinalIgnoreCase) && puntajes[i] > mejor)
                {
                    mejor = puntajes[i];
                }
            }
            return mejor;
        }

        public int[] Clasificacion()
        {
            int[] orden = new int[total];
            for (int i = 0; i < total; i++)
            {
                orden[i] = i;
            }

            for (int i = 1; i < total; i++)
            {
                int actual = orden[i];
                int j = i - 1;
                while (j >= 0 && EsMejor(actual, orden[j]))
                {
                    orden[j + 1] = orden[j];
                    j--;
                }
                orden[j + 1] = actual;
            }

            return orden;
        }

        bool EsMejor(int x, int y) =>
            puntajes[x] > puntajes[y] || (puntajes[x] == puntajes[y] && aciertos[x] > aciertos[y]);
    }

    public partial class Form1 : Form
    {
        static readonly string NL = Environment.NewLine;

        readonly Marcador marcador = new();
        Jugador[] jugadores = Array.Empty<Jugador>();
        Operacion operacion = new(0, '+', 0, 0);
        Modo modo = Modo.Individual;
        int turno;

        readonly Panel pnlInicio = Pantalla();
        readonly Panel pnlJuego = Pantalla();
        readonly Panel pnlResultados = Pantalla();

        readonly Panel tableroInicio = Tablero(21, 143, 918, 511);
        readonly Label lblModo = Etiqueta("Modo de juego:", 40, 30, 250, 44, Fuente("Times New Roman", 13.8F), alineacion: ContentAlignment.MiddleLeft);
        readonly RadioButton radIndividual = Opcion("Individual", 320, 40, true);
        readonly RadioButton radDos = Opcion("Dos jugadores", 560, 40, false);
        readonly Label lblNombre1 = Etiqueta("Jugador 1:", 40, 100, 250, 44, Fuente("Times New Roman", 13.8F), alineacion: ContentAlignment.MiddleLeft);
        readonly TextBox txtNombre1 = Campo("Jugador 1", 320, 103, 360);
        readonly Label lblNombre2 = Etiqueta("Jugador 2:", 40, 170, 250, 44, Fuente("Times New Roman", 13.8F), alineacion: ContentAlignment.MiddleLeft);
        readonly TextBox txtNombre2 = Campo("Jugador 2", 320, 173, 360);
        readonly Label lblVidas = Etiqueta("Vidas por jugador:", 40, 240, 250, 44, Fuente("Times New Roman", 13.8F), alineacion: ContentAlignment.MiddleLeft);
        readonly NumericUpDown numVidas = new()
        {
            Location = new Point(320, 243),
            Width = 100,
            Minimum = 1,
            Maximum = 10,
            Value = 3,
            TextAlign = HorizontalAlignment.Center,
            Font = Fuente("Segoe UI", 13.8F)
        };
        readonly Button btnIniciar = Boton("Iniciar partida", 40, 320, 400, 90);
        readonly Button btnClasificacion = Boton("Ver clasificación", 470, 320, 400, 90);
        readonly Label lblAyuda = Etiqueta(
            "Cada 3 aciertos subes de nivel: los números crecen y se desbloquean nuevas operaciones." + "\n" +
            "En dos jugadores los turnos se alternan y gana quien termine con más puntos.",
            40, 430, 830, 56, Fuente("Segoe UI", 12F), alineacion: ContentAlignment.MiddleLeft);

        readonly Label lblTituloJuego = Titulo("Juego de Aritmética", 604);
        readonly Panel tableroOperacion = Tablero(21, 143, 615, 511);
        readonly Label lblEnunciado = Etiqueta("Operación:", 29, 25, 322, 80, Fuente("Times New Roman", 36F));
        readonly Label lblNum1 = Etiqueta(string.Empty, 26, 135, 78, 70, Fuente("Segoe UI", 19.8F), Color.Red);
        readonly Label lblOp = Etiqueta(string.Empty, 148, 135, 78, 70, Fuente("Segoe UI", 19.8F), Color.Red);
        readonly Label lblNum2 = Etiqueta(string.Empty, 273, 135, 78, 70, Fuente("Segoe UI", 19.8F), Color.Red);
        readonly Label lblIgual = Etiqueta("= ?", 417, 135, 101, 70, Fuente("Segoe UI", 24F), Color.Red);
        readonly Label lblRespuestaTitulo = Etiqueta("Respuesta:", 26, 218, 322, 80, Fuente("Times New Roman", 36F));
        readonly TextBox txtRespuesta = new()
        {
            Location = new Point(26, 368),
            Width = 179,
            MaxLength = 9,
            TextAlign = HorizontalAlignment.Center,
            ForeColor = Color.Red,
            Font = Fuente("Segoe UI", 28F)
        };
        readonly Button btnResponder = Boton("=", 211, 334, 140, 119, 28F);
        readonly Label lblFeedback = Etiqueta(string.Empty, 365, 334, 220, 119, Fuente("Segoe UI", 12F));

        readonly Panel tableroEstado = Tablero(642, 12, 306, 642);
        readonly Label lblTurno = Etiqueta("Turno actual:", 29, 25, 244, 44, Fuente("Times New Roman", 13.8F));
        readonly RadioButton radJ1 = Opcion("Jugador 1", 29, 90, false);
        readonly RadioButton radJ2 = Opcion("Jugador 2", 29, 125, false);
        readonly Label lblPuntajeT = Rotulo("Puntaje:", 20, 180);
        readonly Label lblPuntaje = Valor(20, 228);
        readonly Label lblVidasT = Rotulo("Vidas:", 169, 180);
        readonly Label lblVidasV = Valor(169, 228);
        readonly Label lblAciertosT = Rotulo("Aciertos:", 20, 290);
        readonly Label lblAciertos = Valor(20, 338);
        readonly Label lblErroresT = Rotulo("Errores:", 169, 290);
        readonly Label lblErrores = Valor(169, 338);
        readonly Label lblNivelT = Rotulo("Nivel:", 20, 400);
        readonly Label lblNivel = Valor(20, 448);
        readonly Label lblExtraT = Rotulo("Récord:", 169, 400);
        readonly Label lblExtra = Valor(169, 448);
        readonly Button btnTerminar = Boton("Terminar partida", 20, 520, 253, 70, 14F);

        readonly Label lblTituloRes = Titulo("Resultados", 918);
        readonly Panel tableroResultados = Tablero(21, 143, 918, 511);
        readonly Label lblResumen = Etiqueta(string.Empty, 29, 20, 850, 150, Fuente("Segoe UI", 14F), alineacion: ContentAlignment.MiddleLeft);
        readonly Label lblSubtitulo = Etiqueta("Clasificación de la sesión", 29, 185, 850, 34, Fuente("Times New Roman", 16F), alineacion: ContentAlignment.MiddleLeft);
        readonly ListView lvClasificacion = new()
        {
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            HideSelection = false,
            Location = new Point(29, 225),
            Size = new Size(850, 190),
            Font = Fuente("Segoe UI", 12F)
        };
        readonly Button btnJugarDeNuevo = Boton("Jugar de nuevo", 29, 430, 410, 60);
        readonly Button btnVolver = Boton("Volver al inicio", 469, 430, 410, 60);

        public Form1()
        {
            SuspendLayout();
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(960, 666);
            BackColor = Color.SlateGray;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Juego de Aritmética";

            tableroInicio.Controls.AddRange(new Control[]
            {
                lblModo, radIndividual, radDos, lblNombre1, txtNombre1, lblNombre2, txtNombre2,
                lblVidas, numVidas, btnIniciar, btnClasificacion, lblAyuda
            });
            pnlInicio.Controls.AddRange(new Control[] { Titulo("Juego de Aritmética", 918), tableroInicio });

            tableroOperacion.Controls.AddRange(new Control[]
            {
                lblEnunciado, lblNum1, lblOp, lblNum2, lblIgual, lblRespuestaTitulo,
                txtRespuesta, btnResponder, lblFeedback
            });
            tableroEstado.Controls.AddRange(new Control[]
            {
                lblTurno, radJ1, radJ2, lblPuntajeT, lblPuntaje, lblVidasT, lblVidasV,
                lblAciertosT, lblAciertos, lblErroresT, lblErrores, lblNivelT, lblNivel,
                lblExtraT, lblExtra, btnTerminar
            });
            pnlJuego.Controls.AddRange(new Control[] { lblTituloJuego, tableroOperacion, tableroEstado });

            lvClasificacion.Columns.Add("#", 60);
            lvClasificacion.Columns.Add("Jugador", 380);
            lvClasificacion.Columns.Add("Puntos", 190);
            lvClasificacion.Columns.Add("Aciertos", 190);
            tableroResultados.Controls.AddRange(new Control[]
            {
                lblResumen, lblSubtitulo, lvClasificacion, btnJugarDeNuevo, btnVolver
            });
            pnlResultados.Controls.AddRange(new Control[] { lblTituloRes, tableroResultados });

            Controls.AddRange(new Control[] { pnlInicio, pnlJuego, pnlResultados });

            txtNombre2.Enabled = false;
            radJ1.AutoCheck = false;
            radJ2.AutoCheck = false;

            radDos.CheckedChanged += (_, _) => txtNombre2.Enabled = radDos.Checked;
            txtRespuesta.KeyPress += (_, e) => e.Handled = !(char.IsControl(e.KeyChar) || e.KeyChar is >= '0' and <= '9');
            btnIniciar.Click += (_, _) => IniciarPartida();
            btnClasificacion.Click += (_, _) => MostrarClasificacion();
            btnResponder.Click += (_, _) => Responder();
            btnTerminar.Click += (_, _) => TerminarAnticipadamente();
            btnJugarDeNuevo.Click += (_, _) => IniciarPartida();
            btnVolver.Click += (_, _) => MostrarPantalla(pnlInicio);
            Shown += (_, _) => txtNombre1.Focus();

            MostrarPantalla(pnlInicio);
            ResumeLayout(false);
        }

        static Font Fuente(string familia, float tam, FontStyle estilo = FontStyle.Regular) =>
            new(familia, tam, estilo, GraphicsUnit.Point);

        static Panel Pantalla() => new()
        {
            Dock = DockStyle.Fill,
            BackColor = Color.SlateGray
        };

        static Panel Tablero(int x, int y, int ancho, int alto) => new()
        {
            Location = new Point(x, y),
            Size = new Size(ancho, alto),
            BackColor = SystemColors.AppWorkspace,
            BorderStyle = BorderStyle.Fixed3D
        };

        static Label Titulo(string texto, int ancho) => new()
        {
            Text = texto,
            Location = new Point(21, 12),
            Size = new Size(ancho, 113),
            BackColor = SystemColors.AppWorkspace,
            BorderStyle = BorderStyle.FixedSingle,
            Font = Fuente("Bodoni MT Condensed", 48F, FontStyle.Bold | FontStyle.Italic),
            TextAlign = ContentAlignment.MiddleCenter
        };

        static Label Etiqueta(string texto, int x, int y, int ancho, int alto, Font fuente,
            Color color = default, ContentAlignment alineacion = ContentAlignment.MiddleCenter) => new()
        {
            Text = texto,
            Location = new Point(x, y),
            Size = new Size(ancho, alto),
            Font = fuente,
            ForeColor = color.IsEmpty ? SystemColors.ControlText : color,
            TextAlign = alineacion,
            BorderStyle = BorderStyle.FixedSingle
        };

        static Label Rotulo(string texto, int x, int y) =>
            Etiqueta(texto, x, y, 104, 44, Fuente("Times New Roman", 13.8F));

        static Label Valor(int x, int y) =>
            Etiqueta("0", x, y, 104, 40, Fuente("Segoe UI", 19.8F), Color.Red);

        static Button Boton(string texto, int x, int y, int ancho, int alto, float tam = 16F) => new()
        {
            Text = texto,
            Location = new Point(x, y),
            Size = new Size(ancho, alto),
            BackColor = Color.Beige,
            UseVisualStyleBackColor = false,
            Font = Fuente("Segoe UI", tam, FontStyle.Bold)
        };

        static RadioButton Opcion(string texto, int x, int y, bool marcado) => new()
        {
            Text = texto,
            Location = new Point(x, y),
            AutoSize = true,
            Checked = marcado,
            Font = Fuente("Times New Roman", 13.8F)
        };

        static TextBox Campo(string sugerencia, int x, int y, int ancho) => new()
        {
            PlaceholderText = sugerencia,
            Location = new Point(x, y),
            Width = ancho,
            MaxLength = 14,
            Font = Fuente("Segoe UI", 13.8F)
        };

        static string LimpiarNombre(string texto, string porDefecto)
        {
            string limpio = texto.Trim();
            return limpio.Length == 0 ? porDefecto : limpio;
        }

        void MostrarPantalla(Panel pantalla)
        {
            pnlInicio.Visible = pantalla == pnlInicio;
            pnlJuego.Visible = pantalla == pnlJuego;
            pnlResultados.Visible = pantalla == pnlResultados;

            AcceptButton = pantalla == pnlInicio ? btnIniciar
                : pantalla == pnlJuego ? btnResponder
                : btnJugarDeNuevo.Visible ? btnJugarDeNuevo
                : btnVolver;
        }

        void IniciarPartida()
        {
            bool dos = radDos.Checked;
            string nombre1 = LimpiarNombre(txtNombre1.Text, "Jugador 1");
            string nombre2 = dos ? LimpiarNombre(txtNombre2.Text, "Jugador 2") : string.Empty;

            if (dos && string.Equals(nombre1, nombre2, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Los nombres de los jugadores deben ser distintos.", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MostrarPantalla(pnlInicio);
                txtNombre2.Focus();
                return;
            }

            int vidas = (int)numVidas.Value;
            modo = dos ? Modo.DosJugadores : Modo.Individual;
            jugadores = dos
                ? new[] { new Jugador(nombre1, vidas), new Jugador(nombre2, vidas) }
                : new[] { new Jugador(nombre1, vidas) };
            turno = 0;

            radJ1.Text = nombre1;
            radJ2.Text = nombre2;
            radJ2.Visible = dos;
            lblTurno.Text = dos ? "Turno actual:" : "Jugador:";
            lblExtraT.Text = dos ? "Rival:" : "Récord:";
            lblFeedback.Text = string.Empty;

            MostrarPantalla(pnlJuego);
            SiguienteOperacion();
            ActualizarPanel();
        }

        void SiguienteOperacion()
        {
            operacion = MotorMatematico.Generar(jugadores[turno].Nivel);
            lblNum1.Text = operacion.A.ToString();
            lblOp.Text = operacion.Simbolo;
            lblNum2.Text = operacion.B.ToString();
            txtRespuesta.Clear();
            txtRespuesta.Focus();
        }

        void Responder()
        {
            if (!int.TryParse(txtRespuesta.Text.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out int valor))
            {
                lblFeedback.ForeColor = Color.Black;
                lblFeedback.Text = "Escribe tu respuesta usando solo números.";
                txtRespuesta.SelectAll();
                txtRespuesta.Focus();
                return;
            }

            Jugador jugador = jugadores[turno];
            string mensaje;

            if (valor == operacion.Resultado)
            {
                int nivelAntes = jugador.Nivel;
                int ganados = MotorMatematico.PuntosPorAcierto(nivelAntes);
                jugador.RegistrarAcierto();
                lblFeedback.ForeColor = Color.DarkGreen;
                mensaje = $"¡Correcto!{NL}+{ganados} puntos";
                if (jugador.Nivel > nivelAntes)
                {
                    mensaje += $"{NL}¡Subiste al nivel {jugador.Nivel}!";
                }
            }
            else
            {
                jugador.RegistrarError();
                lblFeedback.ForeColor = Color.DarkRed;
                mensaje = $"Incorrecto.{NL}Resultado: {operacion.Resultado}";
            }

            if (Array.TrueForAll(jugadores, j => !j.Activo))
            {
                FinalizarPartida();
                return;
            }

            AvanzarTurno();
            if (modo == Modo.DosJugadores)
            {
                mensaje += $"{NL}Turno de {jugadores[turno].Nombre}";
            }

            lblFeedback.Text = mensaje;
            SiguienteOperacion();
            ActualizarPanel();
        }

        void AvanzarTurno()
        {
            for (int i = 1; i <= jugadores.Length; i++)
            {
                int candidato = (turno + i) % jugadores.Length;
                if (jugadores[candidato].Activo)
                {
                    turno = candidato;
                    return;
                }
            }
        }

        void ActualizarPanel()
        {
            Jugador jugador = jugadores[turno];

            radJ1.Checked = turno == 0;
            radJ2.Checked = turno == 1;
            lblPuntaje.Text = jugador.Puntos.ToString();
            lblVidasV.Text = jugador.Vidas.ToString();
            lblAciertos.Text = jugador.Aciertos.ToString();
            lblErrores.Text = jugador.Errores.ToString();
            lblNivel.Text = jugador.Nivel.ToString();

            if (modo == Modo.DosJugadores)
            {
                lblExtra.Text = jugadores[1 - turno].Puntos.ToString();
            }
            else
            {
                int mejor = marcador.MejorPuntaje(jugador.Nombre);
                lblExtra.Text = mejor < 0 ? "—" : mejor.ToString();
            }
        }

        void TerminarAnticipadamente()
        {
            if (MessageBox.Show("¿Terminar la partida ahora?", Text,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FinalizarPartida();
            }
        }

        string DeterminarGanador()
        {
            Jugador a = jugadores[0];
            Jugador b = jugadores[1];

            if (a.Puntos == b.Puntos)
            {
                return "¡Empate!";
            }

            return $"Ganador: {(a.Puntos > b.Puntos ? a : b).Nombre}";
        }

        void FinalizarPartida()
        {
            string[] lineas = new string[jugadores.Length];

            for (int i = 0; i < jugadores.Length; i++)
            {
                Jugador j = jugadores[i];
                int previo = marcador.MejorPuntaje(j.Nombre);
                string nota = previo >= 0 && j.Puntos > previo ? "  (¡nuevo récord!)" : string.Empty;
                marcador.Registrar(j.Nombre, j.Puntos, j.Aciertos);
                lineas[i] = $"{j.Nombre}: {j.Puntos} pts, {j.Aciertos} aciertos, {j.Errores} errores, nivel {j.Nivel}{nota}";
            }

            string cabecera = modo == Modo.Individual ? "Fin de la partida" : DeterminarGanador();
            MostrarResultados(cabecera + NL + NL + string.Join(NL, lineas), true);
        }

        void MostrarClasificacion()
        {
            string resumen = marcador.Total == 0
                ? "Aún no hay partidas registradas en esta sesión."
                : $"Partidas registradas en esta sesión: {marcador.Total}";
            MostrarResultados(resumen, false);
        }

        void MostrarResultados(string resumen, bool permitirRepetir)
        {
            lblResumen.Text = resumen;
            btnJugarDeNuevo.Visible = permitirRepetir;
            CargarClasificacion();
            MostrarPantalla(pnlResultados);
        }

        void CargarClasificacion()
        {
            int[] orden = marcador.Clasificacion();

            lvClasificacion.BeginUpdate();
            lvClasificacion.Items.Clear();
            for (int posicion = 0; posicion < orden.Length; posicion++)
            {
                int i = orden[posicion];
                lvClasificacion.Items.Add(new ListViewItem(new[]
                {
                    (posicion + 1).ToString(),
                    marcador.NombreEn(i),
                    marcador.PuntajeEn(i).ToString(),
                    marcador.AciertosEn(i).ToString()
                }));
            }
            lvClasificacion.EndUpdate();
        }
    }
}
