using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : Task {
    public string CooldownKey;
    public float cooldown;
    public float elapsedTime = 0.0f;
    public override NodeResult Execute()
    {
        
        LineRenderer line = tree.gameObject.GetComponent<LineRenderer>();
        

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= cooldown)
        {
            Reset();
            return NodeResult.SUCCESS;
        }
        else if(Mathf.Abs(elapsedTime - 0.1f) < 0.01f)
        {
            if (line)
            {
                line.positionCount = 0;
            }
            return NodeResult.RUNNING;
        }
        else
        {
            return NodeResult.RUNNING;
        }
    }

    public override void Reset()
    {
        cooldown = (float)(tree.GetValue(CooldownKey));
        elapsedTime = 0.0f;
        base.Reset();
    }
}
