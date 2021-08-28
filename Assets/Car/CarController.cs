using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    public List<GameObject> cars;
    public int population = 50;
    public int generation;
    public GameObject car;

    [HideInInspector]
    public DNA winner;
    public DNA secWinner;

    void Start () {
        NewPopulation();
    }
    
	void Update () {
		
	}

    public List<GameObject> GetCars()
    {
        return cars;
    }

    public void NewPopulation()
    {
        cars = new List<GameObject>();

        for(var i = 0; i < population; i++)
        {
            var carObj = (Instantiate(car));
            cars.Add(carObj);
            carObj.GetComponent<Car>().Initialize();
        }

        generation++;

        GameObject.Find("Camera").GetComponent<CameraMovement>().Follow(cars[Random.Range(0, cars.Count - 1)]);
    }

    public void NewPopulation(bool geneticManipulation)
    {
        if (geneticManipulation)
        {
            cars = new List<GameObject>();

            for(var i = 0; i < population; i++)
            {
                var dna = winner.Crossover(secWinner);
                var mutated = dna.Mutate();
                var carObj = Instantiate(car);
                cars.Add(carObj);
                carObj.GetComponent<Car>().Initialize(mutated);
            }
        }

        generation++;
        GameObject.Find("Camera").GetComponent<CameraMovement>().Follow(cars[0]);
    }
}