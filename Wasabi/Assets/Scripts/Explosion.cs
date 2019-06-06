using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ferr;

public class Explosion : MonoBehaviour {

    Ferr2DT_PathTerrain _terrain;
    List<Vector2> _original = new List<Vector2>();

    public float radius;
    public int numberOfPoints;
    public GameObject empty;

    GameObject point;
    Matrix4x4 mat;

    // Use this for initialization
    void Start () {
        _terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Ferr2DT_PathTerrain>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    List<Transform> GetDestructionRadius()
    {
        List<Transform> lPoints = new List<Transform>();
        PolygonCollider2D colliderTerrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<PolygonCollider2D>();
        point = Instantiate(empty, new Vector3(transform.position.x + radius, transform.position.y), transform.rotation);
        GameObject overlapPoint;
        for (int i = 0; i < numberOfPoints; i++)
        {
            if (colliderTerrain.bounds.Contains(point.transform.position))
            {
                overlapPoint = Instantiate(empty, point.transform.position, point.transform.rotation);
                lPoints.Add(overlapPoint.transform);
                Destroy(overlapPoint);
            }
            point.transform.RotateAround(transform.position, Vector3.forward, 360f / numberOfPoints);
        }
        Destroy(point);

        return lPoints;
    }

    List<Transform> GetPointsToErase()
    {
        List<Transform> lPoints = new List<Transform>();
        Path2D path = _terrain.PathData;

        return lPoints;
    }

    private void OnDrawGizmos()
    {
        foreach (Transform point in GetDestructionRadius())
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(point.position, 0.3f);
        }
    }

    public Ferr2DT_PathTerrain Terrain
    {
        get { return _terrain; }
        set { _terrain = value; Save(); }
    }

    private void Save()
    {
        if (_terrain != null)
        {
            _original = _terrain.PathData.GetPathRawCopy();
        }
        else
        {
            _original.Clear();
        }
    }
}
