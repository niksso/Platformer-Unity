using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInteractTip : Trigger
{
    public override void Event()
    {
        GetComponent<Animator>().SetTrigger("Start");
    }
}
