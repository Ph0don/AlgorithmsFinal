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

        foreach(string strMask in tree.gameObject.GetComponent<Flock>().mask)
        {
            mask |= LayerMask.NameToLayer(strMask);
        }

        if (Physics.Raycast(tree.transform.position, tree.transform.forward,out hit, Mathf.Infinity, mask))
        {
            //hi :3
            tree.SetValue("Target", target);
            return NodeResult.SUCCESS;
        }
        
        tree.SetValue("Target", null);
        //bye :(
        return NodeResult.FAILURE;
        


  
    }
}
