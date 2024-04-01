using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmDirectionAI : BehaviorTree {
    
    
    public float WaitTime = 3.0f;
    
	//construct the actual tree
	void Start () {
        
        // create nodes
        Selector TreeRoot = new Selector();
        Sequence Swarm = new Sequence();
        Wait Wait = new Wait();
        PickDirection Pick = new PickDirection();
        // pick random direction task

        
        // create blackboard keys and initialize them with values
        // NOTE - SHOULD BE USING CONSTANTS
              
       
        SetValue("WaitTime", WaitTime);



        // set node parameters - connect them to the blackboard
        Wait.TimeToWaitKey = "WaitTime";
        
        
       
        
        // connect nodes
        
        TreeRoot.children.Add(Swarm);
        Swarm.children.Add(Wait);
        Swarm.children.Add(Pick);

        TreeRoot.tree = this;
        Swarm.tree = this;
        
        Wait.tree = this;
        Pick.tree = this;

        root = TreeRoot;
        
	}

    // we don't need an update - the base class will deal with that.
    // but, since we have one, we can use it to set some of the moveto parameters on the fly
    // which lets us tweak them in the inspector
    public override void Update()
    {       
        
        base.Update();
    }
}
