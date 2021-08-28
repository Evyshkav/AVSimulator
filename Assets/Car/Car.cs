using UnityEngine;

public class Car : MonoBehaviour
{
    private DNA dna;
    private NeuralNetwork neuralNetwork;
    private float objectSpeed;
    private bool initialized;

	void Start () {
		
	}

    public void Initialize()
    {
        neuralNetwork = new NeuralNetwork();
        dna = new DNA(neuralNetwork.GetNeuralNetworkWeights());
        initialized = true;
    }

	public void Initialize(DNA dna)
    {
        neuralNetwork = new NeuralNetwork(dna);
        this.dna = dna;

        initialized = true;
    }

	void Update () {
        if (initialized)
        {
            var inputs = GetComponent<Lasers>().GetDistances();
            neuralNetwork.ApplyFeedForward(inputs);

            var outputs = neuralNetwork.GetNeuralNetworkOutputs();
            GetComponent<CarMovement>().UpdateMovement(outputs);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        ChangeCamera();
    }

    public DNA GetDna()
    {
        return dna;
    }

    public void ChangeCamera()
    {
        var controller = GameObject.Find("CarController").GetComponent<CarController>();

        var cars = controller.GetCars();

        if (cars.Count == 2)
        {
            controller.winner = cars[0].GetComponent<Car>().GetDna();
            controller.secWinner = cars[1].GetComponent<Car>().GetDna();
        }

        if (cars.Count == 1)
        {
            if (!controller.winner.Equals(cars[0].GetComponent<Car>().GetDna())){
                var inter = controller.secWinner;
                controller.secWinner = controller.winner;
                controller.winner = inter;
            }

            cars.Remove(gameObject);
            controller.NewPopulation(true);
            Destroy(gameObject);
        }
        else
        {
            var rand = Random.Range(0, cars.Count);
            if (cars[rand] == gameObject)
            {
                ChangeCamera();
            }
            else
            {
                if(gameObject == GameObject.Find("Camera").GetComponent<CameraMovement>().GetFollowing())
                {
                    GameObject.Find("Camera").GetComponent<CameraMovement>().Follow(cars[rand]);
                }

                cars.Remove(gameObject);

                Destroy(gameObject);
            }
        }
    }
}