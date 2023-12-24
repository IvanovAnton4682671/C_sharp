using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _34_Ivanov_NeuralNetwork.ModelNeuralNetwork
{
    enum TypeNeuron //тип нейрона
    {
        Hidden,
        Output
    }
    enum MemoryMod //режим работы памяти
    {
        GET,
        SET,
        INIT
    }
    enum NetworkMode //режим работы нейросети
    {
        Train,
        Test,
        Recognize
    }
}
