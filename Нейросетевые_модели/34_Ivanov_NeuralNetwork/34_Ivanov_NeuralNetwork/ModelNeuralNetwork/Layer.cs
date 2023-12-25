using System;
using System.IO;
using System.Windows.Forms;

namespace _34_Ivanov_NeuralNetwork.ModelNeuralNetwork
{
    abstract class Layer
    {
        //поля
        protected string name_Layer; //имя слоя
        string pathDirWeights; //путь к директории файла весов
        string pathFileWeights; //путь к файлу весов
        protected int numOfNeurons; //число нейронов
        protected int numOfPrevNeurons; //число нейронов на предыдущем слое
        protected const double learningRate = 0.005; //скорость обучения
        protected const double momentum = 0.05d; //настройка метода оптимизации (d значит, что тип точно double)
        protected double[,] lastDeltaWeights; //последнее изменение весов
        private Neuron[] neurons;

        //свойства
        public Neuron[] Neurons
        {
            get { return neurons; }
            set { neurons = value; }
        }
        public double[] Data
        {
            set
            {
                for (int i = 0; i < Neurons.Length; i++)
                {
                    Neurons[i].Input = value;
                    Neurons[i].Activator();
                }
            }
        }
        protected Layer(int non, int nopn, TypeNeuron nt, string nm_Layer)
        {
            numOfNeurons = non;
            numOfPrevNeurons = nopn;
            Neurons = new Neuron[non];
            name_Layer = nm_Layer;
            pathDirWeights = AppDomain.CurrentDomain.BaseDirectory + "memory\\";
            pathFileWeights = pathDirWeights + name_Layer + "_memory.csv";

            double[,] Weights; //временный массив синаптических весов текущего слоя

            if (File.Exists(pathFileWeights))
            {
                Weights = WeightInitialize(MemoryMod.GET);
            }
            else
            {
                Directory.CreateDirectory(pathDirWeights);
                File.Create(pathFileWeights).Close();
                Weights = WeightInitialize(MemoryMod.INIT);
            }

            lastDeltaWeights = new double[non, nopn + 1];

            for (int i = 0; i < non; i++)
            {
                double[] temp_weights = new double[nopn + 1];
                for (int j = 0; j < nopn + 1; j++)
                {
                    temp_weights[j] = Weights[i, j];
                }
                Neurons[i] = new Neuron(temp_weights, nt);
            }
        }

        abstract public void Recognize(NeuralNetwork net, Layer nextLayer); //метод прямого прохода
        abstract public double[] BackwardPass(double[] stuff); //метод обратного прохода

        public double[,] WeightInitialize(MemoryMod mm)
        {
            char[] delim = new char[] { ';', ' ' }; //разделители слов
            string tempStr; //временная строка для чтения
            string[] tempStrWeights; //временный массив строк
            double[,] weights = new double[numOfNeurons, numOfPrevNeurons + 1];

            switch (mm)
            {
                case MemoryMod.GET:
                    tempStrWeights = File.ReadAllLines(pathFileWeights);
                    string[] memory_element;
                    for (int i = 0; i < numOfNeurons; i++)
                    {
                        memory_element = tempStrWeights[i].Split(delim[0]);
                        for (int j = 0; j < numOfPrevNeurons + 1; j++)
                        {
                            weights[i, j] = double.Parse(memory_element[j].Replace(',', '.'),
                                System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    break;

                case MemoryMod.SET:
                    tempStrWeights = new string[numOfNeurons];

                    for (int i = 0; i < numOfNeurons; i++)
                    {
                        tempStr = Neurons[i].Weights[0].ToString();
                        for (int j = 1; j < numOfPrevNeurons + 1; j++)
                        {
                            tempStr += delim[0] + Neurons[i].Weights[j].ToString();
                        }
                        tempStrWeights[i] = tempStr;
                    }
                    File.WriteAllLines(pathFileWeights, tempStrWeights);
                    break;

                case MemoryMod.INIT:
                    Random random = new Random();
                    tempStrWeights = new string[numOfNeurons];

                    // инициализация весов:
                    // 1. веса инициализируются случайными величинами
                    // 2. мат ожидание всех весов нейрона должно равняться 0
                    // 3. среднее квадратическое значение должно равняться 1

                    for (int i = 0; i < numOfNeurons; i++)
                    {
                        // вычисляем мат. ожидание
                        double sum = 0;
                        for (int j = 0; j < numOfPrevNeurons + 1; j++)
                        {
                            weights[i, j] = random.NextDouble();
                            sum += weights[i, j];
                        }
                        double mean = sum / (numOfPrevNeurons + 1);
                        sum = 0;
                        // вычисляем среднее квадратичное отклонение
                        for (int j = 0; j < numOfPrevNeurons + 1; j++)
                            sum += Math.Pow(weights[i, j] - mean, 2);
                        double std = Math.Sqrt(sum / (numOfPrevNeurons + 1));
                        // нормализуем веса
                        for (int j = 0; j < numOfPrevNeurons + 1; j++)
                            weights[i, j] = (weights[i, j] - mean) / std;

                        tempStr = weights[i, 0].ToString();
                        for (int j = 1; j < numOfPrevNeurons + 1; j++)
                        {
                            tempStr += delim[0] + weights[i, j].ToString();
                        }
                        tempStrWeights[i] = tempStr;

                    }
                    File.WriteAllLines(pathFileWeights, tempStrWeights);
                    break;

                default:
                    break;
            }

            return weights;
        }
    }
}
