using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectTarget : Task
{// Start is called before the first frame update

    public string TargetKey;
    RaycastHit hit;
    public override NodeResult Execute()
    {
        GameObject target = (GameObject)tree.GetValue("Target");
        LayerMask mask = 0;

        foreach(string strMask in tree.gameObject.GetComponent<Boid>().flock.mask)
        {
            mask |= LayerMask.GetMask(strMask);
        }

        if (Physics.Raycast(tree.transform.position, tree.transform.forward,out hit, 20.0f,mask))
        {
            //hi :3
            tree.SetValue("Target", hit.transform.gameObject);
            return NodeResult.SUCCESS;
        }
        
        tree.SetValue("Target", null);
        //bye :(
        tree.gameObject.GetComponent<LineRenderer>().positionCount = 0;
        return NodeResult.FAILURE;




    }
    public override void Reset()
    {
        base.Reset();
    }
}
