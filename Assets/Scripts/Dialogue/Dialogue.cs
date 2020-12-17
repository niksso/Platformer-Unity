using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("Имя говорящего.")]
    [SerializeField] private string name;
    public string Name => name;
    
    [TextArea(3,10)]
    [Tooltip("Текст диалога.")]
    [SerializeField] private string[] sentences;
    public string[] Sentences => sentences;
    
    [Tooltip("Скорость появления букв в диалоговом окне.")] 
    [SerializeField] private float typingSpeed;
    public float TypingSpeed => typingSpeed;
}