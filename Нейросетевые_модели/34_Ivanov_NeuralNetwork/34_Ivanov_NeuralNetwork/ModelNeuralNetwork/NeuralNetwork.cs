using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace _34_Ivanov_NeuralNetwork.ModelNeuralNetwork
{
    class NeuralNetwork
    {
        Form form;
        int epochK = 0;

        //массив для хранения выходного сигнала нейросети
        public double[] fact = new double[10];

        //все слои нейросети
        //мои слои: 15-71-33-10
        private InputLayer input_layer = null;
        private HiddenLayer hidden_layer1 = new HiddenLayer(71, 15, TypeNeuron.Hidden, nameof(hidden_layer1));
        private HiddenLayer hidden_layer2 = new HiddenLayer(33, 71, TypeNeuron.Hidden, nameof(hidden_layer2));
        private OutputLayer output_layer = new OutputLayer(10, 33, TypeNeuron.Output, nameof(output_layer));

        //средне значение энергии ошибки эпохи обучения
        private double e_error_awr;
        public double E_error_awr
        {
            get => e_error_awr;
            set => e_error_awr = value;
        }

        //конструктор
        public NeuralNetwork(NetworkMode nm)
        {
            input_layer = new InputLayer(nm);
        }

        //прямой проход по нейросети
        public void ForwardPass(NeuralNetwork net, double[] net_input)
        {
            net.hidden_layer1.Data = net_input;
            net.hidden_layer1.Recognize(null, net.hidden_layer2);
            net.hidden_layer2.Recognize(null, net.output_layer);
            net.output_layer.Recognize(net, null);
        }

        private Form createChartForm()
        {
            var errorChartForm = new Form
            {
                Text = "График изменения энергии ошибки",
                Width = 600,
                Height = 400
            };

            Chart errorChart = new Chart
            {
                Dock = DockStyle.Fill
            };
            //errorChart.ChartAreas[0].AxisY.Minimum = 0;
            //errorChart.ChartAreas[0].AxisX.Minimum = 0;

            Series errorSeries = new Series("Ошибка")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2
            };

            errorChart.Series.Add(errorSeries);
            errorChart.ChartAreas.Add(new ChartArea());

            // Добавление Chart на форму
            errorChartForm.Controls.Add(errorChart);

            // Подписка на событие закрытия формы
            errorChartForm.FormClosed += (sender, e) => { form = null; errorChartForm = null; };
            return errorChartForm;
        }

        private void UpdateErrorChartAsync(Form errorChartForm, double error)
        {
            // Вызов метода UpdateChart на форме ErrorChartForm
            errorChartForm.Invoke(new Action(() => UpdateChart(errorChartForm, error)));
        }

        private void UpdateChart(Form form, double error)
        {
            // Получение элемента управления Chart из формы
            Chart errorChart = (Chart)form.Controls[0];

            // Добавление новой точки на график
            errorChart.Series[0].Points.AddXY(epochK++, error);

            // Обновление графика
            errorChart.Update();
        }

        public async Task Train(NeuralNetwork net)
        {
            if (form == null)
            {
                form = createChartForm();
                form.Show();
            }

            int epochs = 100;
            net.input_layer = new InputLayer(NetworkMode.Train);

            double tmpSumError; // Временная переменная суммы ошибок
            double[] errors; //Вектор сигнала ошибки выходного слоя
            double[] tmpGradSums1; //Вектор градиента первого скрытого слоя
            double[] tmpGradSums2; //Вектор градиента второго скрытого слоя

            for (int k = 0; k < epochs; k++)
            {
                e_error_awr = 0; // Обнуляем значение в начале эпохи
                for (int i = 0; i < net.input_layer.Train_set.Length; i++)
                {
                    //Прямой проход
                    ForwardPass(net, net.input_layer.Train_set[i].Item1);

                    //Вычисление ошибки по итерации
                    tmpSumError = 0;
                    errors = new double[net.fact.Length];
                    for (int x = 0; x < errors.Length; x++)
                    {
                        if (x == net.input_layer.Train_set[i].Item2)
                        {
                            errors[x] = 1.0 - net.fact[x];
                        }
                        else
                        {
                            errors[x] = -net.fact[x];
                        }

                        //Собираем энергию ошибки
                        tmpSumError += errors[x] * errors[x] / 2.0;
                    }

                    e_error_awr += tmpSumError / errors.Length; //Суммарное значение энергии оишбки эпох

                    //Обратный проход и коррекция весов
                    tmpGradSums2 = net.output_layer.BackwardPass(errors);
                    tmpGradSums1 = net.hidden_layer2.BackwardPass(tmpGradSums2);
                    net.hidden_layer1.BackwardPass(tmpGradSums1);
                }

                e_error_awr /= net.input_layer.Train_set.Length; //Среднее значение энергии ошибки одной эпохи
                UpdateErrorChartAsync(form, e_error_awr);
                //Console.WriteLine($"Эпоха {k}, ошибка: {e_error_avg}");
            }

            net.input_layer = null; //Обнуление входного слоя

            //Сохранение скорректированных весов
            net.hidden_layer1.WeightInitialize(MemoryMod.SET);
            net.hidden_layer2.WeightInitialize(MemoryMod.SET);
            net.output_layer.WeightInitialize(MemoryMod.SET);

        }

        // Тестирование
        public void Test(NeuralNetwork net)
        {
            net.input_layer = new InputLayer(NetworkMode.Test);
            double m = 0;
            for (int i = 0; i < net.input_layer.Test_set.Length; i++)
            {
                ForwardPass(net, net.input_layer.Test_set[i].Item1);
                m += (fact.ToList().IndexOf(fact.Max()) == net.input_layer.Test_set[i].Item2) ? 1 : 0;
            }
            MessageBox.Show(
                $"Точность: {m / net.input_layer.Test_set.Length}",
            "Предсказание",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.RightAlign);
        }
    }
}
