using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFlask : Trigger
{
    [Tooltip("Сколько востанавливает здоровья это зелье.")]
    [SerializeField] private float helath;

    public override void Event()
    {
        this.gameObject.SetActive(!PlayerStats.instance.GetHealing(helath));
    }
}
