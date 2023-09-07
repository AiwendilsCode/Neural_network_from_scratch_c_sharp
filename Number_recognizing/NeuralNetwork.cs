using System;
using System.Linq;
using System.IO;

namespace Number_recognizing
{
    class NeuralNetwork
    {
        Random rnd = new Random();
        int numHidden;
        int numOutput;
        double[][][] hWeights;
        double[][] hoWeights;
        double[][] input;
        int numInput;
        double[] oSums;
        double[] hoBiases;
        double[][] prevOutputDeltas;
        double[][][] prevHiddenDeltas;
        double[] oGrads;
        double learnRate;
        double momentum;
        double[] prevOBiasesDeltas;
        byte[] desiredOutputs;
        public int epochs;
        public double trainPercentage;
        int batchSize;
        public double accuracy;
        int numLayers;
        double[][] hSums;
        double[][] hBiases;
        double[][] prevHiddenBiasesDeltas;
        double[][] hGrads;
        double mse;

        public NeuralNetwork(double[][] Input, int numHidden, int NumLayers, double momentum, double learnRate, int Epochs, double TrainPercentage, int BatchSize, byte[] DesiredOutputs, int numOutput = 10)
        {
            this.input = Input;

            for (int i = 0; i < input.Length; i++)
            {
                Normalize.MinMaxNormal(input[i]);
            }

            this.numHidden = numHidden;
            this.numOutput = numOutput;
            this.epochs = Epochs;
            this.trainPercentage = TrainPercentage; // ending accuracy
            this.batchSize = BatchSize;
            this.desiredOutputs = DesiredOutputs;
            this.numLayers = NumLayers;
            this.momentum = momentum;
            this.learnRate = learnRate;
            numInput = input[0].Length;
            hoWeights = new double[numHidden][];
            hWeights = new double[NumLayers][][];
            oSums = new double[numOutput];
            hSums = new double[NumLayers][];
            hoBiases = new double[numOutput];
            hBiases = new double[numLayers][];
            prevOutputDeltas = new double[numHidden][];
            prevOBiasesDeltas = new double[numOutput];
            oGrads = new double[numOutput];
            hGrads = new double[NumLayers][];
            prevHiddenBiasesDeltas = new double[numLayers][];
            prevHiddenDeltas = new double[numLayers][][];
            for (int i = 0; i < hWeights.Length; i++) // init hidden weights
            {
                hWeights[i] = new double[numHidden][];
                prevHiddenDeltas[i] = new double[numHidden][];
                for (int j = 0; j < hWeights[i].Length; j++)
                {
                    if (i != 0)
                    {
                        hWeights[i][j] = new double[numHidden];
                        prevHiddenDeltas[i][j] = new double[numHidden];
                    }
                    else
                    {
                        hWeights[i][j] = new double[numInput];
                        prevHiddenDeltas[i][j] = new double[numInput];
                    }
                }
            }
            InitArray(hSums, numHidden);
            InitArray(hoWeights, numOutput);
            InitArray(prevOutputDeltas, numOutput);
            InitArray(hBiases, numHidden);
            InitArray(prevHiddenBiasesDeltas, numHidden);
            InitArray(hGrads, numHidden);
            SetWeightsAndBiases();
            /*InitWeightsAndBiases(hBiases, 0.1);
            InitWeightsAndBiases(hWeights, 0.1);
            InitWeightsAndBiases(hoBiases, 0.1);
            InitWeightsAndBiases(hoWeights, 0.1);*/
        }

        private void InitArray(double[][] array, int value) // make arrays in size value
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new double[value];
            }
        }

        public void SetWeightsAndBiases() // set random weights and biases
        {

            for (int i = 0; i < hBiases.Length; i++) // layers
            {
                for (int j = 0; j < hBiases[i].Length; j++) // nodes
                {
                    hBiases[i][j] = (0.01 - -0.01) * rnd.NextDouble() + -0.01;
                }
            }

            for (int i = 0; i < hWeights.Length; i++) // layers
            {
                for (int j = 0; j < hWeights[i].Length; j++) // nodes
                {
                    for (int k = 0; k < hWeights[i][j].Length; k++) // weights
                    {
                        hWeights[i][j][k] = (0.01 - -0.01) * rnd.NextDouble() + -0.01;
                    }
                }
            }

            for (int i = 0; i < hoWeights.Length; i++)
            {
                for (int j = 0; j < hoWeights[i].Length; j++)
                {
                    hoWeights[i][j] = (0.01 - -0.01) * rnd.NextDouble() + -0.01;
                }
            }

            for (int i = 0; i < hoBiases.Length; i++)
            {
                hoBiases[i] = (0.01 - -0.01) * rnd.NextDouble() + -0.01;
            }
        }

        public int ComputeOutput(double[] input) // compute output and return computed value
        {

            for (int i = 0; i < hWeights.Length; i++) // layers
            {
                for (int j = 0; j < hWeights[i].Length; j++) // nodes
                {
                    for (int k = 0; k < hWeights[i][j].Length; k++) // inputs
                    {
                        if (i != 0)
                        {
                            hSums[i][j] += hWeights[i][j][k] * hSums[i - 1][j];
                        }
                        else
                        {
                            hSums[i][j] += hWeights[i][j][k] * input[k];
                        }
                    }
                    hSums[i][j] += hBiases[i][j];
                }
            }

            Activation.HyperTan(hSums);

            for (int i = 0; i < hoWeights.Length; i++)
            {
                for (int j = 0; j < hoWeights[i].Length; j++)
                {
                    oSums[j] += hSums[numLayers - 1][i] * hoWeights[i][j];
                }
            }

            for (int i = 0; i < oSums.Length; i++)
            {
                oSums[i] += hoBiases[i];
            }

            Activation.SoftMax(oSums);

            return Array.IndexOf(oSums, oSums.Max());
        }

        public void Train()
        {
            int[] numbers = new int[numOutput]; // how many samples of all digits are there
            foreach (byte b in desiredOutputs)
            {
                numbers[b]++;
            }
            int correct;
            int[] numCorrect = new int[numOutput]; // how many times correct predicted every digit
            for (int e = 1; e <= epochs && accuracy < trainPercentage; e++)
            {
                correct = 0;
                ToZeros(numCorrect);
                int[] outputs = new int[input.Length];
                int[] indexes = ShuffleIndexes(input.Length);
                for (int i = 0; i < input.Length; i++)
                {
                    ToZeros(oSums);
                    for (int j = 0; j < hSums.Length; j++)
                    {
                        ToZeros(hSums[j]);
                    }

                    double[] currInput = input[indexes[i]];
                    outputs[i] = ComputeOutput(currInput);
                    //if (desiredOutputs[indexes[i]] != outputs[i]) // adjust weights and biases only if predicted incorrectly
                    //{
                        GetGrads(desiredOutputs[indexes[i]], "normal");
                        UpdateWeights(currInput);
                    //}
                    if (desiredOutputs[indexes[i]] == outputs[i])
                    {
                        correct++;
                        numCorrect[desiredOutputs[indexes[i]]]++;
                    }
                    int[] desired = new int[numOutput];
                    desired[desiredOutputs[indexes[i]]] = 1;
                    mse = MeanSquaredError(desired);
                }
                accuracy = (double)correct / input.Length * 100;
            }

            LoadWeightsToFile($"weights_{numInput}_{numLayers}_{numHidden}_{numOutput}_{trainPercentage}.txt");
            LoadBiasesToFile($"biases_{numLayers}_{numHidden}_{numOutput}_{trainPercentage}.txt");
        }

        public int Test(double[] input) // compute output for only one test image and return computed value
        {
            ToZeros(oSums);
            for (int j = 0; j < hSums.Length; j++)
            {
                ToZeros(hSums[j]);
            }
            Normalize.MinMaxNormal(input);
            int computed;
            computed = ComputeOutput(input);
            return computed;
        } 

        public double TestAll(double[][] input, byte[] desOuts) // test all images and return accuracy in percentage
        {
            for (int i = 0; i < input.Length; i++)
            {
                Normalize.MinMaxNormal(input[i]);
            }

            double correct = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int comp = ComputeOutput(input[i]);
                if (comp == desOuts[i])
                {
                    correct++;
                }
            }

            return correct / (double)input.Length * 100;
        }

        private int[] ShuffleIndexes(int num) // return shaffled indexes from zero to num
        {
            int[] shufIndex = new int[num];
            int temp;
            int rand;

            for (int i = 0; i < num; i++)
            {
                shufIndex[i] = i;
            }

            for (int i = 0; i < num; i++)
            {
                rand = rnd.Next(0, num);
                temp = shufIndex[rand];
                shufIndex[rand] = shufIndex[i];
                shufIndex[i] = temp;
            }

            return shufIndex;
        }

        public void InitWeightsAndBiases(double[][] weights, double value) // set weights and biases to value
        {
            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    weights[i][j] = value;
                }
            }
        }

        public void InitWeightsAndBiases(double[] biases, double value) // set weights and biases to value
        {
            for (int i = 0; i < biases.Length; i++)
            {
                biases[i] = value;
            }
        }

        public void InitWeightsAndBiases(double[][][] biases, double value) // set weights and biases to value
        {
            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    for (int k = 0; k < biases[i][j].Length; k++)
                    {
                        biases[i][j][k] = value;
                    }
                }
            }
        }

        private void GetGrads(int desired, string mode) // derivation of function softmax y(1 - y) and multiply with delta | tanh derivation (1 - y)(1 + y)
        {
            double[] desValues = new double[numOutput];
            switch (mode)
            {
                case "mse": // not working
                    double difference;
                    for (int i = 0; i < hGrads.Length; i++)
                    {
                        ToZeros(hGrads[i]);
                    }
                    ToZeros(oGrads);
                    desValues[desired] = 1;
                    for (int i = 0; i < numOutput; i++)
                    {
                        double derivative = oSums[i] * (1 - oSums[i]);
                        oGrads[i] = derivative * (desValues[i] - oSums[i]) * mse;
                    }

                    for (int i = 0; i < hWeights[hWeights.Length - 1].Length; i++)
                    {
                        double derivative = (1 - hSums[numLayers - 1][i]) * (1 + hSums[numLayers - 1][i]);
                        double sum = 0.0;
                        for (int j = 0; j < oGrads.Length; j++)
                        {
                            double x = oGrads[j] * hoWeights[i][j];
                            sum += x;
                        }
                        hGrads[numLayers - 1][i] = sum * derivative;
                    }

                    for (int i = numLayers - 2; i > -1; i--)
                    {
                        for (int j = 0; j < hGrads[i].Length; j++)
                        {
                            double derivative = (1 - hSums[i][j]) * (1 + hSums[i][j]);
                            hGrads[i][j] = derivative * hGrads[i + 1][j];
                        }
                    }
                    break;

                case "normal":
                    for (int i = 0; i < hGrads.Length; i++)
                    {
                        ToZeros(hGrads[i]);
                    }
                    ToZeros(oGrads);
                    desValues[desired] = 1;
                    for (int i = 0; i < numOutput; i++)
                    {
                        double derivative = oSums[i] * (1 - oSums[i]);
                        oGrads[i] = derivative * (desValues[i] - oSums[i]);
                    }

                    for (int i = 0; i < hWeights[hWeights.Length - 1].Length; i++)
                    {
                        double derivative = (1 - hSums[numLayers - 1][i]) * (1 + hSums[numLayers - 1][i]);
                        double sum = 0.0;
                        for (int j = 0; j < oGrads.Length; j++)
                        {
                            double x = oGrads[j] * hoWeights[i][j];
                            sum += x;
                        }
                        hGrads[numLayers - 1][i] = sum * derivative;
                    }

                    for (int i = numLayers - 2; i > -1; i--)
                    {
                        for (int j = 0; j < hGrads[i].Length; j++)
                        {
                            double derivative = (1 - hSums[i][j]) * (1 + hSums[i][j]);
                            hGrads[i][j] = derivative * hGrads[i + 1][j];
                        }
                    }
                    break;
            }

        }

        private void UpdateWeights(double[] input) // adjust weights by gradients
        {

            double delta;
            for (int i = 0; i < hoWeights.Length; i++)
            {
                for (int j = 0; j < hoWeights[i].Length; j++)
                {
                    delta = learnRate * oGrads[j] * hSums[numLayers - 1][i];
                    hoWeights[i][j] += delta;
                    hoWeights[i][j] += prevOutputDeltas[i][j] * momentum;
                    prevOutputDeltas[i][j] = delta;
                }
            }

            for (int i = 0; i < hoBiases.Length; i++)
            {
                delta = learnRate * oGrads[i];
                hoBiases[i] += delta;
                hoBiases[i] += prevOBiasesDeltas[i] * momentum;
                prevOBiasesDeltas[i] = delta;
            }

            for (int i = 0; i < hWeights.Length; i++) // layers
            {
                for (int j = 0; j < hWeights[i].Length; j++) // neurons
                {
                    for (int k = 0; k < hWeights[i][j].Length; k++) // weights
                    {
                        if (i != 0)
                        {
                            delta = learnRate * hGrads[i][j] * hSums[i - 1][j];
                            hWeights[i][j][k] += delta;
                            hWeights[i][j][k] += prevHiddenDeltas[i][j][k] * momentum;
                            prevHiddenDeltas[i][j][k] = delta;
                        }
                        else
                        {
                            delta = learnRate * hGrads[i][j] * input[k];
                            hWeights[i][j][k] += delta;
                            hWeights[i][j][k] += prevHiddenDeltas[i][j][k] * momentum;
                            prevHiddenDeltas[i][j][k] = delta;
                        }
                    }
                }
            }

            for (int i = 0; i < hBiases.Length; i++) // layers
            {
                for (int j = 0; j < hBiases[i].Length; j++) // neurons
                {
                    delta = learnRate * hGrads[i][j];
                    hBiases[i][j] += delta;
                    hBiases[i][j] += prevHiddenBiasesDeltas[i][j] * momentum;
                    prevHiddenBiasesDeltas[i][j] = delta;
                }
            }
        }

        private double MeanSquaredError(int[] desiredOutput) // return mean squared error
        {
            double mse = 0;
            for (int i = 0; i < numOutput; i++)
            {
                double error = desiredOutput[i] - oSums[i];
                mse += error * error;
            }

            return mse / input.Length;
        }

        private void ToZeros(double[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }

        private void ToZeros(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }

        void LoadWeightsToFile(string path)
        {
            for (int i = 0; i < hWeights.Length; i++)
            {
                for (int j = 0; j < hWeights[i].Length; j++)
                {
                    for (int k = 0; k < hWeights[i][j].Length; k++)
                    {
                        File.AppendAllText(path, hWeights[i][j][k].ToString() + "\n");
                    }
                }
            }

            for (int i = 0; i < hoWeights.Length; i++)
            {
                for (int j = 0; j < hoWeights[i].Length; j++)
                {
                    File.AppendAllText(path, hoWeights[i][j].ToString() + "\n");
                }
            }
        }

        void LoadBiasesToFile(string path)
        {
            for (int i = 0; i < hBiases.Length; i++)
            {
                for (int j = 0; j < hBiases[i].Length; j++)
                {
                    File.AppendAllText(path, hBiases[i][j].ToString() + '\n');
                }
            }

            for (int i = 0; i < hoBiases.Length; i++)
            {
                File.AppendAllText(path, hoBiases[i].ToString() + '\n');
            }
        }

        public void LoadWeightsAndBiasesFromFile(string weightsPath, string biasesPath)
        {
            string[] rawWeights = File.ReadAllLines(weightsPath);
            int index = 0;

            for (int i = 0; i < hWeights.Length; i++)
            {
                for (int j = 0; j < hWeights[i].Length; j++)
                {
                    for (int k = 0; k < hWeights[i][j].Length; k++)
                    {
                        hWeights[i][j][k] = double.Parse(rawWeights[index]);
                        index++;
                    }
                }
            }

            for (int i = 0; i < hoWeights.Length; i++)
            {
                for (int j = 0; j < hoWeights[i].Length; j++)
                {
                    hoWeights[i][j] = double.Parse(rawWeights[index]);
                    index++;
                }
            }

            string[] rawBiases = File.ReadAllLines(biasesPath);
            index = 0;

            for (int i = 0; i < hBiases.Length; i++)
            {
                for (int j = 0; j < hBiases[i].Length; j++)
                {
                    hBiases[i][j] = double.Parse(rawBiases[index]);
                    index++;
                }
            }

            for (int i = 0; i < hoBiases.Length; i++)
            {
                hoBiases[i] = double.Parse(rawBiases[index]);
                index++;
            }
        }
    }

    class Normalize
    {
        public static void MinMaxNormal(double[] data)
        {
            double min = data[0];
            double max = data[0];
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i] < min)
                    min = data[i];
                if (data[i] > max)
                    max = data[i];
            }
            double range = max - min;
            if (range == 0.0)
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    data[i] = 0.5;
                }
                return;
            }

            for (int i = 0; i < data.GetLength(0); ++i)
            {
                data[i] = (data[i] - min) / range;
            }
        }
    }

    class Activation
    {
        public static void HyperTan(double[][] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] > 10.0)
                    {
                        input[i][j] = 1;
                    }
                    else if (input[i][j] < -10.0)
                    {
                        input[i][j] = -1;
                    }
                    else
                    {
                        input[i][j] = Math.Tanh(input[i][j]);
                    }
                }
            }
        }

        public static void SoftMax(double[] input)
        {
            double max = input[0];

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] > max)
                {
                    max = input[i];
                }
            }

            double scale = 0.0;

            for (int i = 0; i < input.Length; i++)
            {
                scale += Math.Exp(input[i] - max);
            }

            for (int i = 0; i < input.Length; i++)
            {
                input[i] = Math.Exp(input[i] - max) / scale;
            }
        }

        public static void LogSigmoid(double[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] > 30.0)
                {
                    input[i] = 1;
                }
                else
                {
                    input[i] = 1 / (1 + Math.Exp(input[i]));
                }
            }
        }
    }
}
