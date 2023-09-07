using System;
using System.Drawing;
using System.Windows.Forms;

namespace Number_recognizing
{
    public partial class MNISTForm : Form
    {
        int[][] TrainImages;
        int TrainCurrImage = -1;
        byte[] TrainOutputs;
        double[][] TrainImagesInDouble;

        int[][] TestImages;
        int TestCurrImage = -1;
        byte[] TestOutputs;
        double[][] TestImagesInDouble;
        byte[][,] TrainImage;
        byte[][,] TestImage;
        NeuralNetwork nn;
        int numHidden = 25; // number of neurons in hidden layers
        int numLayers = 1;
        double momentum = 0.0049;
        double learnRate = 0.00395;
        int batchSize = 30; // not implemented
        double maxPercentage = 80;
        int epochs = 100;

        public MNISTForm()
        {
            InitializeComponent();
            ImageLoad imLoadTrain = new ImageLoad("mnist_train.csv");
            TrainImages = imLoadTrain.images;
            TrainOutputs = imLoadTrain.numbersInImages;
            TrainImagesInDouble = imLoadTrain.LoadToDouble();
            TrainImage = ToByteArray(TrainImages);
            
            ImageLoad imLoadTest = new ImageLoad("mnist_test.csv");
            TestImages = imLoadTest.images;
            TestOutputs = imLoadTest.numbersInImages;
            TestImagesInDouble = imLoadTest.LoadToDouble();
            TestImage = ToByteArray(TestImages);
            nn = new NeuralNetwork(TrainImagesInDouble, numHidden, numLayers, momentum, learnRate, epochs, maxPercentage, batchSize, TrainOutputs);
        }

        void trainButton_Click(Object sender, EventArgs e)
        {
            nn.SetWeightsAndBiases();
            nn.Train();
            accuracyTextBox.Text = String.Format(nn.accuracy+"%");
        }

        private byte[][,] ToByteArray(int[][] images)
        {
            byte[][,] res1 = new byte[images.Length][,];
            for (int i = 0; i < images.Length; i++)
            {
                res1[i] = new byte[28, 28];
                int index = 0;
                for (int j = 0; j < 28; j++)
                {
                    for (int k = 0; k < 28; k++)
                    {
                        res1[i][k, j] = (byte)images[i][index];
                        index++;
                    }
                }
            }

            return res1;
        }

        private Bitmap ToBitmap(byte[,] array)
        {
            Bitmap bitmap = new Bitmap(array.GetLength(0), array.GetLength(1));

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    bitmap.SetPixel(i, j, Color.FromArgb(array[i, j], array[i, j], array[i, j]));
                }
            }

            return bitmap;
        }
       
        private void TestAllButton_Click(Object sender, EventArgs e)
        {
            TestAccuracy.Text = nn.TestAll(TestImagesInDouble, TestOutputs).ToString() + "%";
        }

        private void fileButton_Click(Object sender, EventArgs e)
        {
            nn.LoadWeightsAndBiasesFromFile($"weights_{784}_{numLayers}_{numHidden}_{10}_{maxPercentage}.txt", $"biases_{numLayers}_{numHidden}_{10}_{maxPercentage}.txt");
        }

        private void prevTrainButton_Click(Object sender, EventArgs e)
        {
            TrainCurrImage--;
            if (TrainCurrImage < 0)
            {
                TrainCurrImage = TrainImages.Length - 1;
            }

            numPicture.Image = ToBitmap(TrainImage[TrainCurrImage]);
        }

        private void nextTrainButton_Click(Object sender, EventArgs e)
        {
            TrainCurrImage++;
            if (TrainCurrImage >= TrainImages.Length)
            {
                TrainCurrImage = 0;
            }

            numPicture.Image = ToBitmap(TrainImage[TrainCurrImage]);
        }

        private void testPrevButton_Click(Object sender, EventArgs e)
        {
            TestCurrImage--;
            if (TestCurrImage < 0)
            {
                TestCurrImage = TestImages.Length - 1;
            }

            numPicture.Image = ToBitmap(TestImage[TestCurrImage]);

            // using custom files model for predicting
            compNumber.Text = nn.Test(TestImagesInDouble[TestCurrImage]).ToString();
            desiredText.Text = TestOutputs[TestCurrImage].ToString();

            // using onnx model for predicting
            //compNumber.Text = Onnx.predict(TestImagesInDouble[TestCurrImage]).ToString();
            //desiredText.Text = TestOutputs[TestCurrImage].ToString();
        }

        private void testNextButton_Click(Object sender, EventArgs e)
        {
            TestCurrImage++;
            if (TestCurrImage >= TestImages.Length)
            {
                TestCurrImage = 0;
            }

            numPicture.Image = ToBitmap(TestImage[TestCurrImage]);

            // using custom files model for predicting
            compNumber.Text = nn.Test(TestImagesInDouble[TestCurrImage]).ToString();
            desiredText.Text = TestOutputs[TestCurrImage].ToString();

            // using onnx model for predicting
            // compNumber.Text = Onnx.predict(TestImagesInDouble[TestCurrImage]).ToString();
            // desiredText.Text = TestOutputs[TestCurrImage].ToString();
        }
    }
}