using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAI : BehaviorTree
{

    public float cooldown = 5.0f;

    public Material[] materials;
    // Use this for initialization
    void Start()
    {
        AddKey("Target");
        //AddKey("Damage");
        AddKey("Material");
        SetValue("Material", materials);

        Selector selector = new Selector();
        Sequence sequence = new Sequence();
        DamageBoid damageBoid = new DamageBoid();
        DetectTarget detectTarget = new DetectTarget();
        ZapTarget zapTarget = new ZapTarget();
        Cooldown wait = new Cooldown();

        SetValue("Cooldown", cooldown);

        wait.CooldownKey = "Cooldown";


        selector.children.Add(sequence);
        sequence.children.Add(detectTarget);
        sequence.children.Add(zapTarget);
        sequence.children.Add(damageBoid);
        
        sequence.children.Add(wait);
               


        selector.tree = this;
        sequence.tree = this;
        detectTarget.tree = this;
        zapTarget.tree = this;
        damageBoid.tree = this;
        wait.tree = this;
       

        root = selector;
    }

    // Update is called once per frame
    public override void Update()
    {
        SetValue("Material", materials);
        base.Update();
    }
}
