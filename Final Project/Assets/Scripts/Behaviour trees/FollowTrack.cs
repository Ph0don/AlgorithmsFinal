using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrack : BehaviorTree
{
    public float Speed = 5.0f;
    public float TurnSpeed = 2.0f;
    public float Accuracy = 1.5f;
    // Use this for initialization
    void Start()
    {
        AddKey("TrackManager");
        AddKey("TrackIndex");
        AddKey("Waypoints");
        AddKey("Waypoint");
        AddKey("Index");

        AddKey("Speed");
        AddKey("TurnSpeed");
        AddKey("Accuracy");
        AddKey("Direction");

        AddKey("TurnRequested");

        SetValue("Speed", Speed);
        SetValue("TurnSpeed", TurnSpeed);
        SetValue("Accuracy", Accuracy);
        SetValue("TurnRequested", Turning.STRAIGHT);

        Selector selector = new Selector();
        Sequence sequence = new Sequence();
        MoveTo move = new MoveTo(); 
        SelectNextGameObject selectNextGameObject = new SelectNextGameObject();
        PlayerChangeLane playerChangeLane = new PlayerChangeLane();

        move.TargetKey = "Waypoint";
        move.SpeedKey = "Speed";
        move.TurnSpeedKey = "TurnSpeed";
        move.AccuracyKey = "Accuracy";

        selectNextGameObject.ArrayKey = "Waypoints";
        selectNextGameObject.IndexKey = "Index";
        selectNextGameObject.GameObjectKey = "Waypoint";
        selectNextGameObject.DirectionKey = "Direction";

        playerChangeLane.TurnRequestedKey = "TurnRequested";
        playerChangeLane.WaypointKey = "Waypoint";
        playerChangeLane.TrackManagerKey = "TrackManager";
        playerChangeLane.TrackIndexKey = "TrackIndex";
        playerChangeLane.WaypointsKey = "Waypoints";
        playerChangeLane.IndexKey = "Index";
        playerChangeLane.DirectionKey = "Direction";

        selector.children.Add(sequence);

        sequence.children.Add(move);
        sequence.children.Add(selectNextGameObject);
        sequence.children.Add(playerChangeLane);

        selector.tree = this;
        sequence.tree = this;
        move.tree = this;
        selectNextGameObject.tree = this;
        playerChangeLane.tree = this;

        root = selector;
    }

    // Update is called once per frame
    public override void Update()
    {

        base.Update();
    }
}
