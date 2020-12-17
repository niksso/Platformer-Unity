using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interaction _interactiveObject;

    void OnInteraction()
    {
        if (_interactiveObject != null)
        {
            _interactiveObject.Event();
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Interactive":
                _interactiveObject = other.GetComponent<Interaction>();
                break;
            case "Trigger":
                other.GetComponent<Trigger>().Event();
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Interactive":
                _interactiveObject = null;
                break;
        }
    }
}
