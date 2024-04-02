using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputListener : MonoBehaviour
{
    private FollowTrack followBT;
    // Start is called before the first frame update
    void Start()
    {
        followBT = GetComponent<FollowTrack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            followBT.SetValue("TurnRequested", Turning.STRAIGHT);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("LEFT");
            followBT.SetValue("TurnRequested", Turning.LEFT);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("RIGHT");
            followBT.SetValue("TurnRequested", Turning.RIGHT);
        }
    }
}
