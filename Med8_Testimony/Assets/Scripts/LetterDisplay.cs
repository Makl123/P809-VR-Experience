using TMPro;
using UnityEngine;

public class LetterDisplay : MonoBehaviour
{
   
    [SerializeField] private TextMeshPro theText;
    [TextArea]
    [SerializeField] string theLetter;
    
    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        theText.text = theLetter;
    }
}
