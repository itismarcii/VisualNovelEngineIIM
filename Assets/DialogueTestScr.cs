using System;
using DialogueSystem;
using UnityEngine;

public class DialogueTestScr : MonoBehaviour
{
    [SerializeField] private DialogueInfoScriptable Dialogue;

    private void Start()
    {
        DialogueHandler.GenerateDialogues();
    }

    public void StartDialogue()
    {
        DialogueManager.StartDialogue(Dialogue, Array.Empty<Canvas>());
    }

    public void ContinueDialogue()
    {
        DialogueManager.ContinueDialogue();
    }
}
