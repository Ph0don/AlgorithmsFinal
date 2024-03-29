using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {
    public GameObject boidPrefab;
    public string[] mask;
    public int numberOfBoids = 20;
    public int numberOfObstacles = 10;
    public List<GameObject> boids;
    public float spawnRadius = 3.0f;
    public float speed = 1.0f;
    public float turnspeed = 30.0f;
    public float FOV = 60; // degrees
    public float NeighborDistanceSquared = 64.0f; // avoid sqrt
    public float cohesionWeight = 1.0f;
    public float alignmentWeight = 0.0f;
    public float avoidanceWeight = 1.0f;
    public float noise = 0.1f;
    public float AvoidMininum = 3.0f;
    public GameObject target;
    public List<GameObject> deadBoids;
    public Boom boom;
    public bool player;
    RaycastHit hit;
    // Use this for initialization
    void Start () {
        target = gameObject;
        boids = new List<GameObject>();
        deadBoids = new List<GameObject>();
        //boom = GameObject.FindObjectOfType(typeof(Boom)) as Boom;
        for (int i = 0; i < numberOfBoids; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            pos.y = 0.0f;
            Quaternion rot = Quaternion.Euler(0, Random.Range(0,360), 0);
            boids.Add(Instantiate(boidPrefab, pos,rot));
            boids[i].GetComponent<Boid>().flock = this;
            // TODO - Configure the combat AI on the boid we just built.
            // get the tree, and set any blackbaord variables it may need, such as the mask, the object hit (which will be null to start), the shooting range
        }
	}
	
    public void removeBoid(GameObject b)
    {
        deadBoids.Add(b);
    }
	// Update is called once per frame
	void Update () {
		if (deadBoids.Count != 0)
        {
            foreach (GameObject g in deadBoids)
            {
                boom = GameObject.FindObjectOfType(typeof(Boom)) as Boom;
                boids.Remove(g);
                // TODO - create a boom where the boid was
                // TODO - destroy the boid
                if (!Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, Mathf.Infinity))
                {
                    //Destroy once off track if nothing below boid
                    GameObject.Instantiate(boom, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            deadBoids.Clear();
            if (boids.Count == 0)
            {
                // TODO - destroy this swarm leader
                // unless it's the player, and if it's the player, stop the game.
                if (player)

                {

#if UNITY_EDITOR

                    UnityEditor.EditorApplication.isPlaying = false;



#else

                    Application.Quit();

#endif

                }
            }
        }
	}
}
