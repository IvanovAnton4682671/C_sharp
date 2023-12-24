using System;
using System.IO;

namespace _34_Ivanov_NeuralNetwork.ModelNeuralNetwork
{
    class InputLayer
    {
        private Random random = new Random();

        //поля
        private (double[], int)[] train_set = new (double[], int)[100];
        //private (double[], int)[] train_set;
        public (double[], int)[] Train_set { get => train_set; }
        private (double[], int)[] test_set;
        public (double[], int)[] Test_set { get => test_set; }

        //конструктор
        public InputLayer(NetworkMode nm)
        {
            switch (nm)
            {
                case NetworkMode.Train:
                    string path = AppDomain.CurrentDomain.BaseDirectory + "TrainSample.txt";
                    string[] dataSetStr = File.ReadAllLines(path);
                    train_set = new (double[], int)[dataSetStr.Length];
                    string[] dataElem;
                    double[] inputs;
                    for (int i = 0; i < dataSetStr.Length; i++)
                    {
                        dataElem = dataSetStr[i].Split(' ');
                        inputs = new double[dataElem.Length - 1];
                        for (int j = 1; j < dataElem.Length; j++)
                        {
                            inputs[j - 1] = int.Parse(dataElem[j],
                                System.Globalization.CultureInfo.InvariantCulture);
                        }
                        train_set[i] = (inputs, int.Parse(dataElem[0],
                                System.Globalization.CultureInfo.InvariantCulture));
                    }
                    // Перетасовка методом Фишера-Йетса
                    for (int i = train_set.Length - 1; i > 0; i--)
                    {
                        int j = random.Next(i + 1);
                        var temp = train_set[i];
                        train_set[i] = train_set[j];
                        train_set[j] = temp;
                    }
                    break;

                case NetworkMode.Test:
                    path = AppDomain.CurrentDomain.BaseDirectory + "TestSample.txt";
                    dataSetStr = File.ReadAllLines(path);
                    test_set = new (double[], int)[dataSetStr.Length];
                    for (int i = 0; i < dataSetStr.Length; i++)
                    {
                        dataElem = dataSetStr[i].Split(' ');
                        inputs = new double[dataElem.Length - 1];
                        for (int j = 1; j < dataElem.Length; j++)
                        {
                            inputs[j - 1] = int.Parse(dataElem[j],
                                System.Globalization.CultureInfo.InvariantCulture);
                        }
                        test_set[i] = (inputs, int.Parse(dataElem[0],
                                System.Globalization.CultureInfo.InvariantCulture));
                    }
                    break;

                case NetworkMode.Recognize:
                    break;
            }
        }
    }
}
