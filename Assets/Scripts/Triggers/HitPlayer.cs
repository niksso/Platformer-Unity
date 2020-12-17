using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : Trigger
{
    [Tooltip("Сколько нанесёт урона при контакте с игроком.")]
    [SerializeField] private float damage;

    [SerializeField] private bool isMortalDamage;

    public override void Event()
    {
        PlayerStats.instance.GetDamage(damage,isMortalDamage);
        
        if (!isMortalDamage)
        {
            PlayerStats.instance.GetImmortalBuff();
        }
    }
}
