
#region Стартовые using не нужны
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
#endregion

using static System.Math;

namespace _34_Ivanov_NeuralNetwork.ModelNeuralNetwork
{
    class Neuron
    {
        //поля
        private TypeNeuron typeNeuron; //тип нейрона
        private double[] input; //вход
        private double[] weights; //синаптические веса нейрона
        private double output; //выход
        private double derivative; //производная

        //свойства
        public double[] Input
        {
            get { return input; }
        }
        public double[] Weights
        {
            get { return weights; }
            set { weights = value; }
        }
        public double Output
        {
            get { return output; }
        }

        //методы
        public Neuron(double[] w, TypeNeuron t)
        {
            typeNeuron = t;
            weights = w;
        }

        private double Logistic(double arg)
        {
            double res = 1 / (1 + Exp(arg * (-1))); //логистическая функция активации
            return res;
        }

        private double Logistic_derivator(double arg)
        {
            double derivRes = Exp(arg) / Pow((Exp(arg) + 1), 2); //первая производная логистической функции
            return derivRes;
        }

        public void Activator(double[] inpt, double[] wght)
        {
            double sum = wght[0];
            for (int i = 0; i < inpt.Length; i++)
            {
                sum += inpt[i] * wght[i + 1];
                switch (typeNeuron)
                {
                    case TypeNeuron.Hidden:
                        output = Logistic(sum);
                        derivative = Logistic_derivator(sum);
                        break;
                    case TypeNeuron.Output:
                        Logistic(sum);
                        break;
                }
            }
        }
    }
}
