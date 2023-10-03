using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _34_Ivanov_NeuralNetwork.ModelNeuralNetwork;

namespace _34_Ivanov_NeuralNetwork
{
    public partial class FormMain : Form
    {
        //поля
        int[] inputData = new int[15];
        NeuralNetwork neuralNetwork;

        //методы
        private void ChangeInputData(Button b, int ind, Label l)
        {
            if (b.BackColor == Color.LightGray)
            {
                b.BackColor = Color.SeaGreen;
                inputData[ind] = 1;
                l.Text = "1";
            }
            else
            {
                b.BackColor = Color.LightGray;
                inputData[ind] = 0;
                l.Text = "0";
            }
        }

        //свойства

        public FormMain()
        {
            InitializeComponent();
            neuralNetwork = new NeuralNetwork();
        }

        #region Изменения цвета нажатых кнопок и отображение массива
        private void button1_Click(object sender, EventArgs e)
        {
            ChangeInputData(button1, 0, label1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangeInputData(button2, 1, label2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeInputData(button3, 2, label3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeInputData(button4, 3, label4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeInputData(button5, 4, label5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeInputData(button6, 5, label6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeInputData(button7, 6, label7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChangeInputData(button8, 7, label8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChangeInputData(button9, 8, label9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ChangeInputData(button10, 9, label10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ChangeInputData(button11, 10, label11);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ChangeInputData(button12, 11, label12);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ChangeInputData(button13, 12, label13);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ChangeInputData(button14, 13, label14);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ChangeInputData(button15, 14, label15);
        }
        #endregion

        #region Обработка ввода в textBox
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length >= 1 && textBox.SelectionLength == 0 && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        #endregion

        private void buttonSaveTrainSample_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length == 0)
            {
                MessageBox.Show("Вы не ввели цифру!");
            }
            else
            {
                string pathDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string trainExaple = textBox.Text.ToString();
                for (int i = 0; i < inputData.Length; i++)
                {
                    trainExaple += " " + inputData[i].ToString();
                }
                trainExaple += "\n";
                pathDirectory += "TrainSample.txt";
                File.AppendAllText(pathDirectory, trainExaple);
                MessageBox.Show("Обучающая выборка для цифры " + textBox.Text.ToString() + " сохранена.");
                for (int i = 0; i < inputData.Length; i++)
                {
                    inputData[i] = 0;
                }
                label1.Text = "0"; label2.Text = "0"; label3.Text = "0"; label4.Text = "0"; label5.Text = "0";
                label6.Text = "0"; label7.Text = "0"; label8.Text = "0"; label9.Text = "0"; label10.Text = "0";
                label11.Text = "0"; label12.Text = "0"; label13.Text = "0"; label14.Text = "0"; label15.Text = "0";
                button1.BackColor = Color.LightGray; button2.BackColor = Color.LightGray;
                button3.BackColor = Color.LightGray; button4.BackColor = Color.LightGray;
                button5.BackColor = Color.LightGray; button6.BackColor = Color.LightGray;
                button7.BackColor = Color.LightGray; button8.BackColor = Color.LightGray;
                button9.BackColor = Color.LightGray; button10.BackColor = Color.LightGray;
                button11.BackColor = Color.LightGray; button12.BackColor = Color.LightGray;
                button13.BackColor = Color.LightGray; button14.BackColor = Color.LightGray;
                button15.BackColor = Color.LightGray;
                textBox.Text = "";
            }
        }
    }
}
