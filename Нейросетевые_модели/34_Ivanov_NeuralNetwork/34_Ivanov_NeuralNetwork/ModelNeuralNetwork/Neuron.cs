using System;
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
        public double[] Input { get { return input; } set { input = value; } }
        public double[] Weights { get { return weights; } set { weights = value; } }
        public double Output { get { return output; } }
        public double Derivative { get { return derivative; } }

        //методы
        public Neuron(double[] w, TypeNeuron t)
        {
            this.typeNeuron = t;
            this.weights = w;
        }

        private double Logistic(double arg)
        {
            return 1 / (1 + Math.Exp(-arg)); //логистическая функция активации
        }

        private double Logistic_derivator(double arg)
        {
            return Math.Exp(-arg) / Math.Pow((1 + Math.Exp(-arg)), 2); //производная логистической функции
        }

        public void Activator()
        {
            double sum = weights[0];
            for (int i = 0; i < input.Length; i++)
            {
                sum += input[i] * weights[i + 1];
            }

            switch (typeNeuron)
            {
                case TypeNeuron.Output:
                    output = Exp(sum);
                    break;
                case TypeNeuron.Hidden:
                    output = Logistic(sum);
                    derivative = Logistic_derivator(sum);
                    break;
                default:
                    break;
            }
        }
    }
}
