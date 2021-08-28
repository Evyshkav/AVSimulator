using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class NeuralNetwork
{
    private readonly List<List<float>> neurons;
    private readonly List<float[][]> weights;

    public int hiddenLayers = 2;
    public int sizeHiddenLayers = 20;
    public int outputs = 2;
    public int inputs = 7;
    public float maxInitialValue = 5f;

    public NeuralNetwork()
    {
        var totalLayers = hiddenLayers + 2;
        weights = new List<float[][]>();
        neurons = new List<List<float>>();

        for (var i = 0; i < totalLayers; i++)
        {
            var layer = new List<float>();
            var sizeLayer = GetSizeLayer(i);

            if (i != 1 + hiddenLayers)
            {
                var layerWeights = new float[sizeLayer][];
                var nextSizeLayer = GetSizeLayer(i + 1);

                for(var j = 0; j < sizeLayer; j++)
                {
                    layerWeights[j] = new float[nextSizeLayer];

                    for(var k = 0; k < nextSizeLayer; k++)
                    {
                        layerWeights[j][k] = GenerateRandomValue();
                    }
                }

                weights.Add(layerWeights);
            }

            for(var j = 0; j < sizeLayer; j++)
            {
                layer.Add(0);
            }

            neurons.Add(layer);
        }
    }

    public NeuralNetwork(DNA dna)
    {
        var weightsDna = dna.GetDna();
        var totalLayers = hiddenLayers + 2;

        weights = new List<float[][]>();
        neurons = new List<List<float>>();

        for (var i = 0; i < totalLayers; i++)
        {
            var layer = new List<float>();
            var sizeLayer = GetSizeLayer(i);

            if (i != 1 + hiddenLayers)
            {
                var weightsDnaLayer = weightsDna[i];
                var layerWeights = new float[sizeLayer][];
                var nextSizeLayer = GetSizeLayer(i + 1);

                for (var j = 0; j < sizeLayer; j++)
                {
                    layerWeights[j] = new float[nextSizeLayer];

                    for (var k = 0; k < nextSizeLayer; k++)
                    {
                        layerWeights[j][k] = weightsDnaLayer[j][k];
                    }
                }

                weights.Add(layerWeights);
            }

            for (var j = 0; j < sizeLayer; j++)
            {
                layer.Add(0);
            }

            neurons.Add(layer);
        }
    }

    public void ApplyFeedForward(float[] inputs)
    {
        var inputLayer = neurons[0];

        for(var i = 0; i < inputs.Length; i++)
        {
            inputLayer[i] = inputs[i];
        }

        for (var layer = 0; layer < neurons.Count - 1; layer++)
        {
            var weightsLayer = weights[layer];
            var nextLayer = layer + 1;
            var neuronsLayer = neurons[layer];
            var neuronsNextLayer = neurons[nextLayer];

            for(var i = 0; i < neuronsNextLayer.Count; i++)
            {
                var sum = neuronsLayer.Select((t, j) => weightsLayer[j][i] * t).Sum();
                neuronsNextLayer[i] = CalculateSigmoidFunction(sum);
            }
        }
    }

    public int GetSizeLayer(int i)
    {
        int sizeLayer;

        switch (i)
        {
            case 0:
                sizeLayer = inputs;
                break;
            default:
                sizeLayer = i == hiddenLayers + 1 ? outputs : sizeHiddenLayers;
                break;
        }

        return sizeLayer;
    }

    public List<float> GetNeuralNetworkOutputs()
    {
        return neurons[neurons.Count - 1];
    }

    public float CalculateSigmoidFunction(float x)
    {
        return 1 / (1 + Mathf.Pow((float)Math.E, -x));
    }

    public float GenerateRandomValue()
    {
        return Random.Range(-maxInitialValue, maxInitialValue);
    }

    public List<float[][]> GetNeuralNetworkWeights()
    {
        return weights;
    }
}