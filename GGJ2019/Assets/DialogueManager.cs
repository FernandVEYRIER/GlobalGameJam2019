using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshPro tmPro;
    public Transform P1;
    public Transform P2;
    public DialogueTrigger Dialogue;
    private Queue<string> _sentences;

    private bool tooglePlayer = false;
    private Vector3 P1_initPos;
    private Vector3 P2_initPos;
    private Vector3 P1_maxPos;
    private Vector3 P2_maxPos;

    void Start()
    {
        _sentences = new Queue<string>();
        P1_initPos = P1.position;
        P2_initPos = P2.position;
        P1_maxPos = P1_initPos + Vector3.up * 0.7f;
        P2_maxPos = P2_initPos + Vector3.up * 0.7f;
        StartDialogue(Dialogue.dialog);
    }

    public void StartDialogue(Dialog dialog)
    {
        _sentences.Clear();

        foreach (var sentence in dialog.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();
        tmPro.text = sentence;
        tooglePlayer = !tooglePlayer;
    }

    private void EndDialogue()
    {
        Debug.Log("This is the end");
    }

    void Update()
    {
        if (Input.GetButtonDown("NextText"))
        {
            DisplayNextSentence();
        }
        if (tooglePlayer)
        {
            P1.position = Vector3.Slerp(P1.position, P1_maxPos, 10f * Time.deltaTime);
            P2.position = Vector3.Slerp(P2.position, P2_initPos, 10f * Time.deltaTime);
        } else
        {
            P2.position = Vector3.Slerp(P2.position, P2_maxPos, 10f * Time.deltaTime);
            P1.position = Vector3.Slerp(P1.position, P1_initPos, 10f * Time.deltaTime);
        }
    }
}
