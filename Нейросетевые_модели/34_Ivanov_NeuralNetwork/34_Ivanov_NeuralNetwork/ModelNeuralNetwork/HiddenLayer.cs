namespace _34_Ivanov_NeuralNetwork.ModelNeuralNetwork
{
    class HiddenLayer : Layer
    {
        public HiddenLayer(int non, int nopn, TypeNeuron nt, string type) : base(non, nopn, nt, type)
        {}

        public override void Recognize(NeuralNetwork net, Layer nextLayer) //заполняем массив выходными сигналами
                                                                           //нейронови передаём в следующий слой
        {
            double[] hiddenOut = new double[Neurons.Length];

            for (int i = 0; i < Neurons.Length; i++)
            {
                hiddenOut[i] = Neurons[i].Output;
            }
            nextLayer.Data = hiddenOut;
        }

        public override double[] BackwardPass(double[] gr_sums)
        {
            double[] gr_sum = new double[numOfPrevNeurons];

            for (int j = 0; j < gr_sum.Length; j++) //вычисление локалных градиентов
            {
                double sum = 0;
                for (int k = 0; k < numOfNeurons; k++)
                {
                    sum += Neurons[k].Weights[j] * Neurons[k].Derivative * gr_sums[k];
                }
                gr_sum[j] = sum;
            }

            for (int i = 0; i < numOfNeurons; i++) //вычисление коррекции синаптических весов
            {
                for (int n = 0; n < numOfPrevNeurons + 1; n++) //цикл корректировки весов
                {
                    double delta_w = 0;
                    if (n == 0) //коррекция порогов
                    {
                        delta_w = momentum * lastDeltaWeights[i, 0] + learningRate * Neurons[i].Derivative * gr_sums[i];
                    }
                    else
                    {
                        delta_w = momentum * lastDeltaWeights[i, n] + learningRate * Neurons[i].Input[n - 1] *
                            Neurons[i].Derivative * gr_sums[i];
                    }
                    lastDeltaWeights[i, n] = delta_w;
                    Neurons[i].Weights[n] += delta_w; //коррекция весов
                }
            }

            return gr_sum;
        }
    }
}