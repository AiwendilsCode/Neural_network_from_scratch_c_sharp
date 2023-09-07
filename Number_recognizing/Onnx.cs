using System;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

public static class Onnx
{
	public static int predict(double[] input)
	{
		if (input.Length != 784)
		{
			throw new Exception("Invalid input size.");
		}

		Tensor<float> inputTensor = new DenseTensor<float>(new[] { 1, 784 });

		for (int i = 0; i < 784; i++)
		{
			inputTensor[0, i] = (float)input[i];
		}

		var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor("modelInput", inputTensor) };

		// Run inference
		InferenceSession session = new InferenceSession("MNIST.onnx");
		IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(inputs);

		IEnumerable<float> output = results.First().AsEnumerable<float>();
		float sum = output.Sum(x => (float)Math.Exp(x));
		IEnumerable<float> softmax = output.Select(x => (float)Math.Exp(x) / sum);

		var arr = softmax.ToArray();
		return Array.IndexOf(arr, arr.Max());
	}
}
