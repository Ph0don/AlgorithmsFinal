﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boid : MonoBehaviour
{
    public Flock flock;
    public Boom boom;
    public float speed = 0;
    public float turnspeed = 0;
    public Vector3 direction = Vector3.zero;
    public Vector3 forward = Vector3.zero;
    public int NeighborCount = 0;
    Vector3 noise = Vector3.zero;
    LayerMask ground;
    RaycastHit hit;
    // Use this for initialization
    void Start()
    {
        speed = flock.speed;
        turnspeed = flock.turnspeed;
        ground = LayerMask.GetMask("Tracks");
    }

    // Update is called once per frame
    void Update()
    {
        //line.enabled = true;
        forward = gameObject.transform.forward;
        Motivate();
        //if (!Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, Mathf.Infinity))
        //{
        //    //Destroy once off track if nothing below boid
        //    //GameObject.Instantiate(boom, transform.position, Quaternion.identity);
        //    Destroy(gameObject);
        //}
        /*Vector3 dir = (target.transform.position - target.transform.position) / segmentCount;
        for (int i = 0; i < segmentCount + 1; i++)
        {
            Vector3 pos = target.transform.position + dir * i + Random.onUnitSphere * chaos;
            line.SetPosition(i, pos);
        }
        if (hit.collider.gameObject != null)
        {
            return NodeResult.SUCCESS;
        }*/
    }
    private void LateUpdate()
    {
        Move();
    }

    bool isNeighBor(Boid b)
    {
        if (b == this)
        {
            return false;
        }
        // distance check
        Vector3 offset = b.gameObject.transform.position - transform.position;
        float ds = offset.sqrMagnitude;
        if (ds > flock.NeighborDistanceSquared)
        {
            return false;
        }
        // line of sight check
        float angle = Vector3.Angle(offset, transform.forward);
        if (angle > flock.FOV)
        {
            return false;
        }

        return true;
    }

    void Move()
    {
        Quaternion turnDirection = Quaternion.FromToRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, turnDirection, Time.deltaTime * turnspeed);
        // then move

        Vector3 pos = transform.position + transform.forward * speed * Time.deltaTime;
        pos.y = 0.05f;
        transform.position = pos;
        // did I fall off the track?
        if (!Physics.Raycast(pos, -Vector3.up, 5, ground))
        {
            flock.removeBoid(gameObject);

        }
    }

    void Motivate()
    {
        //// not run into stuff is top priority
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward * 2, out hit))
        //{
        //    Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
        //    direction = Vector3.Reflect(transform.forward, hit.normal);
        //    return;
        //    // we need to turn away from it
        //}
        Vector3 Alignment = Vector3.zero;
        Vector3 Cohesion = Vector3.zero;
        Vector3 Avoidance = Vector3.zero;
        NeighborCount = 0;
        foreach (GameObject g in flock.boids)
        {
            Boid b = g.GetComponent<Boid>();
            if (isNeighBor(b))
            {
                NeighborCount++;
                Cohesion += g.transform.position;
                Alignment += g.transform.forward;
                if (Vector3.Distance(g.transform.position, transform.position) < flock.AvoidMininum)
                {
                    Avoidance += (transform.position - g.transform.position);
                }
            }
        }
        if (NeighborCount != 0)
        {
            Cohesion = Cohesion / NeighborCount;
            Alignment = Alignment / NeighborCount;
            Cohesion = Cohesion - transform.position;
        }
        // normalize the three motivations
       

        Vector3 SwarmDir = flock.target.transform.position - transform.position;
        
        
        //Alignment = Vector3.Normalize(Alignment);
        //Avoidance = Vector3.Normalize(Avoidance);
        //if (Random.Range(0, 100) < 5)
        //{
        //    noise = UnityEngine.Random.onUnitSphere * flock.noise * (flock.boids.Length - NeighborCount);
        //}
        // and add and scale them
        direction = SwarmDir + Cohesion * flock.cohesionWeight + Alignment * flock.alignmentWeight + Avoidance * flock.avoidanceWeight;
        
        if(direction.y > 0.00f || direction.y < 0.00f) 
        {
            direction.y = 0.000f;
        }

        // then renormalize again
        direction = Vector3.Normalize(direction);
    }
}
