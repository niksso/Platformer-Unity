using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region  Singleton
    
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }
    
    #endregion

    private Queue<string> _sentences;

    [Header("Ссылки на элементы интерфейса.")] 
    [Tooltip("Ссылка на бокс текста для имени.")] 
    [SerializeField] private Text name;

    [Tooltip("Ссылка на бокс текста для речи.")] 
    [SerializeField] private Text speech;

    [Tooltip("Аниматор, управляющий окошком диалога.")]
    [SerializeField] private Animator dialogueBoxAnimator;

    private float _typingBreakTime;

    private void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBoxAnimator.SetBool("Speeching",true);

        _sentences.Clear();
        name.text = dialogue.Name;
        _typingBreakTime = dialogue.TypingSpeed;

        foreach (string sentence in (dialogue.Sentences))
        {
            _sentences.Enqueue((sentence));
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentencs)
    {
        speech.text = "";
        foreach (char letter in sentencs.ToCharArray())
        {
            speech.text += letter;

            yield return new WaitForSeconds(_typingBreakTime);
        }
    }

    void EndDialogue()
    {
        dialogueBoxAnimator.SetBool("Speeching",false); 
    }
}
