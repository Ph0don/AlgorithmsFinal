using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour {
    public Catmul[] splines;
    public GameObject splinePrefab;
    public GameObject[] swarmleaderPrefab;
    public string[][] maskNames;

    public int numSplines;
    public float radius;

	// Use this for initialization
	void Start () {
        maskNames = new string[5][];

        maskNames[0] = new string[] { "flock1", "flock2", "flock3", "flock4" };
        maskNames[1] = new string[] { "flock0", "flock2", "flock3", "flock4" };
        maskNames[2] = new string[] { "flock0", "flock1", "flock3", "flock4" };
        maskNames[3] = new string[] { "flock0", "flock1", "flock2", "flock4" };
        maskNames[4] = new string[] { "flock0", "flock1", "flock2", "flock3" };
        splines = new Catmul[numSplines]; // TODO - change  
        float angleInc = 2.0f * MathF.PI / (numSplines-1);
        float angleOffset = Mathf.PI / 4.0f;
        for (int i = 0; i < numSplines; ++i)
        {
            Vector3 position;
            if(i == 0)
            {
                position = new Vector3(0, 0, 0);
            }
            else
            {
                position = new Vector3(radius * Mathf.Cos(angleInc * i + angleOffset), 0, radius * Mathf.Sin(angleInc * i + angleOffset));
            }
            splines[i] = Instantiate(splinePrefab, position, Quaternion.identity).GetComponent<Catmul>();
        }

        // TODO add code here

        LineRenderer midLine = splinePrefab.GetComponent<LineRenderer>();
        
        

        for (int i = 0; i < numSplines; i++) // TO DO - change code
        {
            splines[i].GenerateSpline();
        }
        // spawn the flocks on the tracks.  Track 0 is where the player begins.
        for (int i = 0; i < numSplines; i++) // TO DO CHANGE CODE 
        {
            // TO DO - Spawn the swarm leader
            // TODO - Get the follow track script, and tell it about the track manager (so it can find more tracks), and the spline.
            // make sure to set the mask on the flock, and to say which is the player. 
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
 