using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _34_Ivanov_NeuralNetwork
{
    public partial class FormMain : Form
    {
        //поля
        int[] clickData;

        //методы
        private void ChangeClickData(Button b, int ind, Label l)
        {
            if (b.BackColor == Color.LightGray)
            {
                b.BackColor = Color.SeaGreen;
                clickData[ind] = 1;
                l.Text = "1";
            }
            else
            {
                b.BackColor = Color.LightGray;
                clickData[ind] = 0;
                l.Text = "0";
            }
        }

        //свойства

        public FormMain()
        {
            InitializeComponent();
            clickData = new int[15];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeClickData(button1, 0, label1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangeClickData(button2, 1, label2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeClickData(button3, 2, label3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeClickData(button4, 3, label4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeClickData(button5, 4, label5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeClickData(button6, 5, label6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeClickData(button7, 6, label7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChangeClickData(button8, 7, label8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChangeClickData(button9, 8, label9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ChangeClickData(button10, 9, label10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ChangeClickData(button11, 10, label11);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ChangeClickData(button12, 11, label12);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ChangeClickData(button13, 12, label13);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ChangeClickData(button14, 13, label14);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ChangeClickData(button15, 14, label15);
        }
    }
}
