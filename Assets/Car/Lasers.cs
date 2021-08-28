using UnityEngine;

public class Lasers : MonoBehaviour
{
    private int count;
    private GameObject[] laserObjects;

    public Color colorBeam = new Color(0, 255, 0, 0.5f);
    public int distanceLaser = 50;
    public static int lasers = 7;
    public static int view = 120; 
    public float height = 0.5f;

    void Start ()
    {
        count = view / (lasers - 1);
        laserObjects = new GameObject[lasers];

        for (var i = 0; i < lasers; i++)
        {
            float currentDegree = count * i - view / 2;
            var obj = new GameObject();
            var laser = obj.AddComponent<Laser>();
            laser.finalLength = 0.04f;
            laser.laserColor = colorBeam;
            laser.distanceLaser = distanceLaser;
           
            laser.transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);

            laserObjects[i] = obj;
            laserObjects[i].transform.Rotate(new Vector3(0,currentDegree,0));
            obj.transform.SetParent(transform);
        }
	}

    public float[] GetDistances()
    {
        var lasersArray = new float[laserObjects.Length];

        for (var i = 0; i < laserObjects.Length; i++)
        {

            lasersArray[i] = laserObjects[i].GetComponent<Laser>().GetDistance();
        }

        return lasersArray;
    }
    
	void Update ()
    {
        foreach (var obj in laserObjects)
        {
            obj.transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        }
	}
}