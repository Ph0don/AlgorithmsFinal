using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : BehaviorTree {
    public GameObject[] waypoints;
    public int index;
    public float Speed;
    public float TurnSpeed;
    public float Accuracy;
	//construct the actual tree
	void Start () {
        
        // create nodes
        Selector TreeRoot = new Selector();
        Sequence Patrol = new Sequence();
        MoveTo MoveToWP = new MoveTo();
        Spin turn = new Spin();
        SelectNextGameObject PickNextWP = new SelectNextGameObject();
        // create blackboard keys and initialize them with values
        // NOTE - SHOULD BE USING CONSTANTS
        TurnSpeed = 2.0f;
        Speed = 5.0f;
        Accuracy = 1.5f;
        SetValue("Waypoints", waypoints);
        SetValue("CurrentWaypoint", waypoints[0]);
        SetValue("WaypointIndex", 0);
        SetValue("Speed", Speed);
        SetValue("SpinSpeed", 180.0f);
        SetValue("SpinAmount", 90.0f);
        SetValue("TurnSpeed", TurnSpeed);
        SetValue("Accuracy", Accuracy);
        // set node parameters - connect them to the blackboard
        MoveToWP.TargetName = "CurrentWaypoint";
        turn.rotateSpeedKey = "SpinSpeed";
        turn.rotateAmountKey = "SpinAmount";
        PickNextWP.ArrayKey = "Waypoints";
        PickNextWP.GameObjectKey = "CurrentWaypoint";
        PickNextWP.IndexKey = "WaypointIndex";
        // connect nodes
        Patrol.children.Add(MoveToWP);
        Patrol.children.Add(turn);
        Patrol.children.Add(turn);
        Patrol.children.Add(PickNextWP);
        TreeRoot.children.Add(Patrol);
        Patrol.tree = this;
        TreeRoot.tree = this;
        MoveToWP.tree = this;
        turn.tree = this;
        PickNextWP.tree = this;
        root = TreeRoot;
        
	}

    // we don't need an update - the base class will deal with that.
    // but, since we have one, we can use it to set some of the moveto parameters on the fly
    // which lets us tweak them in the inspector
    public override void Update()
    {
        SetValue("Speed", Speed);
        SetValue("TurnSpeed", TurnSpeed);
        SetValue("Accuracy", Accuracy);
        base.Update();
    }
}
