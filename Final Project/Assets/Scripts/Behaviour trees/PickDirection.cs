using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickDirection : Task {

    private FollowTrack followBT;

    // Use this for initialization

    // Update is called once per frame
    public override NodeResult Execute()
    {

        followBT = tree.gameObject.GetComponent<FollowTrack>();

        int direction = Random.Range(0, 3);

        if (direction == 0)
        {
            followBT.SetValue("TurnRequested", Turning.STRAIGHT);
        }
        if (direction == 1)
        {
            followBT.SetValue("TurnRequested", Turning.LEFT);
        }
        if (direction == 2)
        {
            followBT.SetValue("TurnRequested", Turning.RIGHT);
        }

        return NodeResult.SUCCESS;
    }
      

    public override void Reset()
    {

        
    }

}
