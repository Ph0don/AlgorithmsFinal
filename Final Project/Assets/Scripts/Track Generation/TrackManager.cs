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

        for (int i = 0; i < numSplines; i++) // TO DO - change code
        {
            splines[i].GenerateSpline();
        }
        // spawn the flocks on the tracks.  Track 0 is where the player begins.
        for (int i = 0; i < numSplines; i++) // TO DO CHANGE CODE 
        {
            Vector3 direction = splines[i].points[1] - splines[i].points[0];

            Quaternion rotation = Quaternion.LookRotation(direction, new Vector3(0,1,0));

            GameObject spawnedObject = GameObject.Instantiate(swarmleaderPrefab[i], splines[i].points[0] + new Vector3(0.0f,0.05f,0.0f), rotation);

            FollowTrack followTrack = spawnedObject.GetComponent<FollowTrack>();
            followTrack.SetValue("TrackManager", this);
            followTrack.SetValue("TrackIndex", i);
            followTrack.SetValue("Waypoints", splines[i].sp);
            followTrack.SetValue("Waypoint", splines[i].sp[0]);
            followTrack.SetValue("Index", 0);
            followTrack.SetValue("Direction", 1);


            Flock flockComponent = spawnedObject.GetComponent<Flock>();
            flockComponent.mask = maskNames[i];
            flockComponent.player = i == 0;

            // TO DO - Spawn the swarm leader
            // TODO - Get the follow track script, and tell it about the track manager (so it can find more tracks), and the spline.
            // make sure to set the mask on the flock, and to say which is the player. 
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
 