namespace Juego_aritmetica
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            op = new Label();
            label10 = new Label();
            rresultado = new Label();
            button1 = new Button();
            label2 = new Label();
            label7 = new Label();
            n2 = new Label();
            num1 = new Label();
            label3 = new Label();
            rpuntaje = new Label();
            label1 = new Label();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            panel2 = new Panel();
            label8 = new Label();
            label6 = new Label();
            label4 = new Label();
            label5 = new Label();
            label11 = new Label();
            raciertos = new Label();
            rerrores = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.AppWorkspace;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(op);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(rresultado);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(n2);
            panel1.Controls.Add(num1);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(21, 143);
            panel1.Name = "panel1";
            panel1.Size = new Size(615, 511);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint_1;
            // 
            // op
            // 
            op.BorderStyle = BorderStyle.FixedSingle;
            op.Font = new Font("Segoe UI", 19.8000011F);
            op.ForeColor = Color.Red;
            op.Location = new Point(148, 135);
            op.Name = "op";
            op.Size = new Size(78, 70);
            op.TabIndex = 10;
            op.Click += label9_Click;
            // 
            // label10
            // 
            label10.BorderStyle = BorderStyle.FixedSingle;
            label10.Font = new Font("Times New Roman", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(26, 218);
            label10.Name = "label10";
            label10.Size = new Size(322, 80);
            label10.TabIndex = 9;
            label10.Text = "Opreración:";
            // 
            // rresultado
            // 
            rresultado.BorderStyle = BorderStyle.FixedSingle;
            rresultado.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rresultado.ForeColor = Color.Red;
            rresultado.Location = new Point(26, 334);
            rresultado.Name = "rresultado";
            rresultado.Size = new Size(179, 119);
            rresultado.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new Point(211, 334);
            button1.Name = "button1";
            button1.Size = new Size(140, 119);
            button1.TabIndex = 7;
            button1.Text = "=";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Font = new Font("Times New Roman", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(29, 25);
            label2.Name = "label2";
            label2.Size = new Size(322, 80);
            label2.TabIndex = 6;
            label2.Text = "Opreración:";
            label2.Click += label2_Click_1;
            // 
            // label7
            // 
            label7.BorderStyle = BorderStyle.FixedSingle;
            label7.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Red;
            label7.Location = new Point(417, 135);
            label7.Name = "label7";
            label7.Size = new Size(101, 70);
            label7.TabIndex = 5;
            label7.Text = "= ?";
            label7.Click += label7_Click;
            // 
            // n2
            // 
            n2.BorderStyle = BorderStyle.FixedSingle;
            n2.Font = new Font("Segoe UI", 19.8000011F);
            n2.ForeColor = Color.Red;
            n2.Location = new Point(273, 135);
            n2.Name = "n2";
            n2.Size = new Size(78, 70);
            n2.TabIndex = 4;
            // 
            // num1
            // 
            num1.BorderStyle = BorderStyle.FixedSingle;
            num1.Font = new Font("Segoe UI", 19.8000011F);
            num1.ForeColor = Color.Red;
            num1.Location = new Point(26, 135);
            num1.Name = "num1";
            num1.Size = new Size(78, 70);
            num1.TabIndex = 0;
            num1.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(82, 105);
            label3.Name = "label3";
            label3.Size = new Size(0, 20);
            label3.TabIndex = 1;
            // 
            // rpuntaje
            // 
            rpuntaje.BorderStyle = BorderStyle.FixedSingle;
            rpuntaje.Font = new Font("Segoe UI", 19.8000011F);
            rpuntaje.ForeColor = Color.Red;
            rpuntaje.Location = new Point(20, 330);
            rpuntaje.Name = "rpuntaje";
            rpuntaje.Size = new Size(104, 37);
            rpuntaje.TabIndex = 3;
            // 
            // label1
            // 
            label1.BackColor = SystemColors.AppWorkspace;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Font = new Font("Bodoni MT Condensed", 48F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(21, 12);
            label1.Name = "label1";
            label1.Size = new Size(604, 113);
            label1.TabIndex = 1;
            label1.Text = "Juego de Aritmerica";
            label1.Click += label1_Click;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radioButton1.Location = new Point(29, 156);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(86, 21);
            radioButton1.TabIndex = 3;
            radioButton1.TabStop = true;
            radioButton1.Text = "Jugador 2";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radioButton2.Location = new Point(29, 113);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(86, 21);
            radioButton2.TabIndex = 4;
            radioButton2.TabStop = true;
            radioButton2.Text = "Jugador 1";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.AppWorkspace;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(rerrores);
            panel2.Controls.Add(raciertos);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(radioButton1);
            panel2.Controls.Add(radioButton2);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(rpuntaje);
            panel2.Controls.Add(label11);
            panel2.Location = new Point(642, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(306, 642);
            panel2.TabIndex = 5;
            // 
            // label8
            // 
            label8.BorderStyle = BorderStyle.FixedSingle;
            label8.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(169, 383);
            label8.Name = "label8";
            label8.Size = new Size(104, 44);
            label8.TabIndex = 9;
            label8.Text = "Errores:";
            label8.Click += label8_Click;
            // 
            // label6
            // 
            label6.BorderStyle = BorderStyle.FixedSingle;
            label6.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(20, 383);
            label6.Name = "label6";
            label6.Size = new Size(104, 44);
            label6.TabIndex = 8;
            label6.Text = "Aciertos:";
            label6.Click += label6_Click;
            // 
            // label4
            // 
            label4.BorderStyle = BorderStyle.FixedSingle;
            label4.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(20, 276);
            label4.Name = "label4";
            label4.Size = new Size(104, 44);
            label4.TabIndex = 7;
            label4.Text = "Puntaje:";
            // 
            // label5
            // 
            label5.BorderStyle = BorderStyle.FixedSingle;
            label5.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(29, 25);
            label5.Name = "label5";
            label5.Size = new Size(244, 44);
            label5.TabIndex = 6;
            label5.Text = "Cantidad de variables:";
            label5.Click += label5_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(82, 105);
            label11.Name = "label11";
            label11.Size = new Size(0, 20);
            label11.TabIndex = 1;
            // 
            // raciertos
            // 
            raciertos.BorderStyle = BorderStyle.FixedSingle;
            raciertos.Font = new Font("Segoe UI", 19.8000011F);
            raciertos.ForeColor = Color.Red;
            raciertos.Location = new Point(20, 439);
            raciertos.Name = "raciertos";
            raciertos.Size = new Size(104, 37);
            raciertos.TabIndex = 10;
            // 
            // rerrores
            // 
            rerrores.BorderStyle = BorderStyle.FixedSingle;
            rerrores.Font = new Font("Segoe UI", 19.8000011F);
            rerrores.ForeColor = Color.Red;
            rerrores.Location = new Point(169, 439);
            rerrores.Name = "rerrores";
            rerrores.Size = new Size(104, 37);
            rerrores.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateGray;
            ClientSize = new Size(960, 666);
            Controls.Add(panel2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Label num1;
        private Label label3;
        private Label label7;
        private Label n2;
        private Label rpuntaje;
        private Label label2;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Panel panel2;
        private Label label5;
        private Label label11;
        private Label label8;
        private Label label6;
        private Label label4;
        private Button button1;
        private Label label10;
        private Label rresultado;
        private Label label13;
        private Label label12;
        private Label op;
        private Label rerrores;
        private Label raciertos;
    }
}
