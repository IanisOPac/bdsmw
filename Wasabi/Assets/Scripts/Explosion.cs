using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ferr;

public class Explosion : MonoBehaviour {
    [SerializeField] int damage;
    Ferr2DT_PathTerrain _terrain;
    List<Vector2> _original = new List<Vector2>();

    public float radius;
    public int numberOfPoints;
    public GameObject empty;

    GameObject point;
    Matrix4x4 mat;
    float time;

    // Use this for initialization
    void Start () {
        time = Time.time;
        _terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Ferr2DT_PathTerrain>();
        mat = _terrain.transform.localToWorldMatrix;

        RecreateTerrain();
        DamageOnTargets();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - time >= 0.2f)
        {
            Destroy(this.gameObject);
        }
	}

    void DamageOnTargets()
    {
        foreach(GameObject charact in GameObject.FindGameObjectsWithTag("characters"))
        {
            if(Vector2.Distance(transform.position, charact.transform.position) <= radius)
            {
                charact.GetComponent<Char_script>().TakeDamage(damage);
            }
        }
    }

    List<Transform> GetPointsToAdd()
    {
        List<Transform> lPoints = new List<Transform>();
        PolygonCollider2D colliderTerrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<PolygonCollider2D>();
        point = Instantiate(empty, new Vector3(transform.position.x + radius, transform.position.y), transform.rotation);
        GameObject overlapPoint;
        for (int i = 0; i < numberOfPoints; i++)
        {
            if (colliderTerrain.OverlapPoint(point.transform.position))
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

    List<int> GetPointsToErase()
    {
        List<int> lPointsID = new List<int>();
        Path2D path = _terrain.PathData;

        for (int i = 0; i < path.Count; i++)
        {
            Vector2 world = mat.MultiplyPoint(path[i]);
            //Debug.Log(Vector2.Distance(world, transform.position));
            if (Vector2.Distance(world, transform.position) <= radius)
            {
                lPointsID.Add(i);
            }
        }

        lPointsID.Sort();
        lPointsID.Reverse();

        return lPointsID;
    }

    void RecreateTerrain()
    {
        List<int> erasePoint = GetPointsToErase();
        List<Transform> addPoint = GetPointsToAdd();

        foreach (int item in erasePoint)
        {
            _terrain.RemovePoint(item);
        }

        foreach (Transform point in addPoint)
        {
            Vector2 explosionLocalPosition = _terrain.transform.worldToLocalMatrix.MultiplyPoint(point.transform.position);
            _terrain.AddAutoPoint(explosionLocalPosition);
        }

        _terrain.Build(true);
        _terrain.RecreateCollider();
        Save();
    }

    /*private void OnDrawGizmos()
    {
        foreach (Transform point in GetPointsToAdd())
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(point.position, 0.1f);
        }
        Path2D path = _terrain.PathData;
        foreach (int item in GetPointsToErase())
        {
            Vector2 world = mat.MultiplyPoint(path[item]);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(world, 0.3f);
        }
    }*/

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
