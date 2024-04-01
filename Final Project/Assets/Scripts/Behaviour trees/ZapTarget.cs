using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ZapTarget : Task
{
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    public string TargetKey;

    // Use this for initialization

    public override NodeResult Execute()
    {
        if (lineRenderer == null)
            lineRenderer = tree.gameObject.GetComponent<LineRenderer>();


        GameObject target = (GameObject)tree.GetValue("Target");
        if (target == null)
            return NodeResult.FAILURE;

        Material[] mats = (Material[])tree.GetValue("Material");
        lineRenderer.material = mats[Random.Range(0, mats.Length)];
        lineRenderer.positionCount = 11;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 dir = (target.transform.position - tree.transform.position) / lineRenderer.positionCount;
            Vector3 pos = tree.transform.position + dir * i + Random.onUnitSphere * 0.3f;
            lineRenderer.SetPosition(i, pos);
        }



        return NodeResult.SUCCESS;
    }

    public override void Reset()
    {
        base.Reset();
    }
}
