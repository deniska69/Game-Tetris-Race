using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameTetrisRace
{
    public partial class Settings : Form
    {
        #region Переменные

        public int diff; //Переменная для уровня сложности

        public System.Drawing.SolidBrush clrSetka = new System.Drawing.SolidBrush(Color.Brown);
        public System.Windows.Forms.DialogResult clrSetkaResult = new System.Windows.Forms.DialogResult();

        public System.Drawing.SolidBrush clrPlayer = new System.Drawing.SolidBrush(Color.DeepSkyBlue);
        public System.Windows.Forms.DialogResult clrPlayerResult = new System.Windows.Forms.DialogResult();

        public System.Drawing.SolidBrush clrVrag = new System.Drawing.SolidBrush(Color.DarkCyan);
        public System.Windows.Forms.DialogResult clrVragResult = new System.Windows.Forms.DialogResult();

        #endregion

        public Settings()
        {
            InitializeComponent();

            diff = 1; //Переменная для уровня сложности

            pictureBox1.BackColor = Color.DeepSkyBlue;
            pictureBox2.BackColor = Color.DarkCyan;
            pictureBox3.BackColor = Color.Brown;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            diff = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            diff = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            diff = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clrPlayerResult = colorDialog1.ShowDialog();

            if (clrPlayerResult == System.Windows.Forms.DialogResult.OK)
            {
                clrPlayer.Color = colorDialog1.Color;
            }

            pictureBox1.BackColor = colorDialog1.Color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clrVragResult = colorDialog2.ShowDialog();

            if (clrVragResult == System.Windows.Forms.DialogResult.OK)
            {
                clrVrag.Color = colorDialog2.Color;
            }

            pictureBox2.BackColor = colorDialog2.Color;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clrSetkaResult = colorDialog3.ShowDialog();

            if (clrSetkaResult == System.Windows.Forms.DialogResult.OK)
            {
                clrSetka.Color = colorDialog3.Color;
            }

            pictureBox3.BackColor = colorDialog3.Color;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
