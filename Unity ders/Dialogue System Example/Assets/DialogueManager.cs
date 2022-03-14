using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialoguText;
    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {

        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialoguText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeStence(sentence));
    }
    
    IEnumerator TypeStence (string sentence)
    {
        dialoguText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialoguText.text += letter;
            yield return null;
        }
    }
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Debug.Log("End of conversattion");
    }
}
