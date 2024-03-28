using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmDirectionAI : BehaviorTree {
    
    public int index;
    public float Speed;
    public float TurnDirection;
    public float TurnSpeed;
    public float WaitTime;
    
	//construct the actual tree
	void Start () {
        
        // create nodes
        Selector TreeRoot = new Selector();
        Sequence Swarm = new Sequence();
        Wait Wait = new Wait();
        // pick random direction task

        
        // create blackboard keys and initialize them with values
        // NOTE - SHOULD BE USING CONSTANTS
        TurnSpeed = 2.0f;
        Speed = 5.0f;
        
        SetValue("TurnDirection", TurnDirection);
        SetValue("WaitTime", WaitTime);
        
        SetValue("WaypointIndex", 0);
        SetValue("Speed", Speed);      
       
        SetValue("TurnSpeed", TurnSpeed);
       
        // set node parameters - connect them to the blackboard
        
        
        
       
        
        // connect nodes
        
        TreeRoot.children.Add(Swarm);
        Swarm.children.Add(Wait);
        Swarm.tree = this;
        TreeRoot.tree = this;
       
        root = TreeRoot;
        
	}

    // we don't need an update - the base class will deal with that.
    // but, since we have one, we can use it to set some of the moveto parameters on the fly
    // which lets us tweak them in the inspector
    public override void Update()
    {
        SetValue("Speed", Speed);
        SetValue("TurnSpeed", TurnSpeed);
        
        base.Update();
    }
}
