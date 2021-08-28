using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float distance;

    public Color laserColor = new Color(0, 1, 0, 0.5f);
    public float distanceLaser = 50;
    public float finalLength = 0.04f;
    public float initialLength = 0.04f;

    void Start ()
    {
        distance = distanceLaser;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
        lineRenderer.startWidth = initialLength;
        lineRenderer.endWidth = finalLength;
        lineRenderer.positionCount = 2;
    }
    
	void Update ()
    {
        var finalPoint = transform.position + transform.forward * distanceLaser;

        RaycastHit collisionPoint;

        if (Physics.Raycast(transform.position, transform.forward, out collisionPoint, distanceLaser))
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, collisionPoint.point);
            distance = collisionPoint.distance;

        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, finalPoint);
            distance = distanceLaser;
        }
	}

    public float GetDistance()
    {
        return distance / distanceLaser;
    }
}