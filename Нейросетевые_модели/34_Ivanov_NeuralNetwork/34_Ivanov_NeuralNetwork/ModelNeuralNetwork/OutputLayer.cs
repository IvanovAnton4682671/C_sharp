namespace _34_Ivanov_NeuralNetwork.ModelNeuralNetwork
{
    class OutputLayer : Layer
    {
        public OutputLayer(int non, int nopn, TypeNeuron nt, string type) : base(non, nopn, nt, type)
        {}

        public override void Recognize(NeuralNetwork net, Layer nextLayer) //заполняем массив выходными сигналами
                                                                           //нейронов и передаём в следующий слой
        {
            double e_sum = 0;

            for (int i = 0; i < numOfNeurons; i++)
            {
                e_sum += Neurons[i].Output;
            }
            for (int i = 0; i < numOfNeurons; i++)
            {
                net.fact[i] = Neurons[i].Output / e_sum; //расчёт вектора выходных сигналов

            }
        }

        public override double[] BackwardPass(double[] errors)
        {
            double[] gr_sum = new double[numOfPrevNeurons + 1];

            for (int j = 0; j < numOfPrevNeurons + 1; j++) //вычисление градиентных сумм выходного слоя
            {
                double sum = 0;

                for (int k = 0; k < numOfNeurons; k++)
                {
                    sum += Neurons[k].Weights[j] * errors[k];
                }

                gr_sum[j] = sum;
            }

            for (int i = 0; i < numOfNeurons; i++) //коррекция синаптических весов
            {
                for (int n = 0; n < numOfPrevNeurons + 1; n++)
                {
                    double delta_w;
                    if (n == 0) //коррекция порогов
                    {
                        delta_w = momentum * lastDeltaWeights[i, 0] + learningRate * errors[i];
                    }
                    else //коррекция синаптических весов
                    {
                        delta_w = momentum * lastDeltaWeights[i, n] + learningRate * Neurons[i].Input[n - 1] * errors[i];
                    }

                    lastDeltaWeights[i, n] = delta_w;
                    Neurons[i].Weights[n] += delta_w; //коррекция весов
                }
            }

            return gr_sum;
        }
    }
}
