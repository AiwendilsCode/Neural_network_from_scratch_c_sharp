# Neural network from scratch in C#
Mnist number recognizing, using neural network from scratch in C#.

## Setup
Open project in visual studio, install all dependencies (Visual studio will figure out which you need) and click run.

## Buttons in GUI
- **Train** -> Train neural network with specified parameters in MNISTform.cs, you have to move files mnist_train.csv and mnist_test.csv to exe folder to succesfully train neural network. (The GUI will freeze when training for some time, for how long, that is affected by choosen parameters and CPU.)

- **Test all** -> tests all test inputs using neural network with specified parameters. Before test you need to train neural network or load weights from pretrained files.

- **Weights from file** -> load weights from pretrained file. Pretrained file must be in exe folder.

## Pretrained file formats
weights = weights\_{numInput}\_{numLayers}\_{numHidden}\_{numOutput}\_{maxPercentage}.txt
biases = biases\_{numLayers}\_{numHidden}\_{numOutput}\_{maxPercentage}.txt

numInput is always 784.
numOutput is always 10.
maxPercentage is percentage which model achieved on training data.

## Onnx model
You can use your custom onnx model for testing or use my pretrained one MNIST.onnx, you have to only uncomment some lines in testPrevButton_Click and testNextButton_Click functions also you have to comment lines which runs test on models from my custom neural net files.