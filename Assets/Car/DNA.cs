using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    private readonly List<float[][]> dna;
    private readonly float mutationProb = 0.05f;
    private readonly float maxVariation = 1f;

    public DNA(List<float[][]> weights)
    {
        dna = weights;
    }

    public List<float[][]> GetDna()
    {
        return dna;
    }

    public DNA Mutate()
    {
        var newDna = new List<float[][]>();

        foreach (var weightsLayer in dna)
        {
            for (var j = 0; j < weightsLayer.Length; j++)
            {
                for (var k = 0; k < weightsLayer[j].Length; k++)
                {
                    var rand = Random.Range(0f, 1f);
                    if (rand < mutationProb)
                    {
                        weightsLayer[j][k] = Random.Range(-maxVariation, maxVariation);
                    }
                }
            }

            newDna.Add(weightsLayer);
        }

        return new DNA(newDna);
    }

    public DNA Crossover(DNA otherParent)
    {
        var child = new List<float[][]>();
        for(var i = 0; i < dna.Count; i++)
        {
            var otherParentLayer = otherParent.GetDna()[i];
            var parentLayer = dna[i];

            for (var j = 0; j < parentLayer.Length; j++)
            {
                for (var k = 0; k < parentLayer[j].Length; k++)
                {
                    var rand = Random.Range(0f, 1f);
                    if (rand < 0.5f)
                    {
                        parentLayer[j][k] = otherParentLayer[j][k];
                    }
                    else
                    {
                        parentLayer[j][k] = parentLayer[j][k];
                    }

                }
            }

            child.Add(parentLayer);
        }

        return new DNA(child);
    }
}