using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private readonly float cameraRotation = 0;

    public float animationTime = 1;
    public GameObject objectToFollow;

    public Vector3 initialPosition;
    public float initialTime;

	void Start () {
        initialPosition = transform.position;
    }
    
	void Update () {
        if (objectToFollow != null)
        {
            var timePassed = (Time.time - initialTime);
            var proportion = timePassed / animationTime;

            var currentPosition = proportion < 1 
                ? Vector3.Lerp(initialPosition, objectToFollow.transform.position, proportion) 
                : objectToFollow.transform.position;

            transform.position = new Vector3(currentPosition.x, currentPosition.y + 8f, currentPosition.z - 18f);
            transform.LookAt(currentPosition);
            transform.Translate(Vector3.right * Time.deltaTime * cameraRotation * 5);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            var cars = GameObject.Find("CarController").GetComponent<CarController>().GetCars();
            var index = cars.IndexOf(objectToFollow);

            if(index == cars.Count - 1)
            {
                index = 0;
            }
            else
            {
                index += 1;
            }

            Follow(cars[index]);
        }
	}

    public void Follow(GameObject newFollow)
    {
        if (objectToFollow != null)
        {
            initialPosition = objectToFollow.transform.position;
            initialTime = Time.time;
        }

        objectToFollow = newFollow;
    }

    public GameObject GetFollowing()
    {
        return objectToFollow;
    }
}