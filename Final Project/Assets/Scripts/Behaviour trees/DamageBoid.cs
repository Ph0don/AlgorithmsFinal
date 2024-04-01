using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoid : Task
{
    // Start is called before the first frame update
    
    public string TargetKey;
    public string SpeedKey;
    public string TurnSpeedKey;
    public string AccuracyKey;
    // Use this for initialization
    public float Speed = 5.0f;
    public override NodeResult Execute()
    {
        GameObject target = (GameObject)tree.GetValue("Target");
        if (target == null)
        {
            return NodeResult.FAILURE;
        }
        else
        {
            target.GetComponent<Boid>().speed *= 1.1f;
            target.GetComponent<Boid>().turnspeed *= 0.9f;
        }
        
        return NodeResult.SUCCESS;
    }
    public override void Reset()
    {
        base.Reset();
    }
}
