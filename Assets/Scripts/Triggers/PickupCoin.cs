using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : Trigger
{
    [Tooltip("Значение, которое прибавится к очкам при сборе предмета.")]
    [SerializeField] private int scores;
    public override void Event()
    {
        PlayerStats.instance.GetScores(scores);
        this.gameObject.SetActive(false);
    }
}
