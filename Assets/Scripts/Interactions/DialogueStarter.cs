using UnityEngine;

public class DialogueStarter : Interaction
{
    [Tooltip("Диалог, запускаемый этим действием.")]
    [SerializeField] private Dialogue dialogue;
    public override void Event()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
        
