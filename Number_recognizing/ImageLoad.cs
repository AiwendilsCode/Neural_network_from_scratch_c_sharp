using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Number_recognizing
{
    class ImageLoad
    {
        public int[][] images;
        public byte[] numbersInImages;
        public ImageLoad(string file)
        {
            string[] rawImages = File.ReadAllLines(file);
            images = new int[rawImages.Length - 1][];
            numbersInImages = new byte[images.Length];

            for (int i = 1; i < rawImages.Length; i++)
            {
                images[i - 1] = ParseArray(rawImages[i].Split(','), out byte number);
                numbersInImages[i - 1] = number;
            }
        }

        private int[] ParseArray(string[] array, out byte number)
        {
            number = 0;
            int[] intArray = new int[array.Length - 1];
            for (int i = 0; i < array.Length; i++)
            {
                if (i == 0)
                    number = byte.Parse(array[i]);
                else
                    intArray[i - 1] = int.Parse(array[i]);
            }

            return intArray;
        }

        public double[][] LoadToDouble()
        {
            double[][] doubleImage = new double[images.Length][];

            for (int k = 0; k < doubleImage.Length; k++)
            {
                doubleImage[k] = new double[images[k].Length];
                for (int i = 0; i < images[k].GetLength(0); i++)
                {
                    doubleImage[k][i] = (double)images[k][i];
                }
            }

            return doubleImage;
        }

        public byte[,,] LoadToImage(int[] image)
        {
            byte[,,] imageRes = new byte[3, 28, 28];

            for (int i = 0; i < imageRes.GetLength(0); i++)
            {
                int index = 0;
                for (int j = 0; j < imageRes.GetLength(1); j++)
                {
                    for (int k = 0; k < imageRes.GetLength(2); k++)
                    {
                        imageRes[i, j, k] = (byte)image[index];
                        index++;
                    }
                }
            }

            return imageRes;
        }

        public byte[][] ConvertToByte()
        {
            byte[][] res = new byte[images.Length][];
            for (int i = 0; i < images.Length; i++)
            {
                res[i] = new byte[images[i].Length];
                for (int j = 0; j < images[i].Length; j++)
                {
                    res[i][j] = (byte)images[i][j];
                }
            }

            return res;
        }
    }
}
