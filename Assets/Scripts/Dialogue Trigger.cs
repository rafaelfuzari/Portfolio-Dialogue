using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public GameObject visualCue;
    private bool playerNear;
    public int storyIndex;
    [SerializeField] private TextAsset inkJSON;
    private bool startedDialogue;


    void Start()
    {
        visualCue.SetActive(false);
        storyIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        visualCue.SetActive(playerNear);
        if (playerNear)
        {
            if (Input.GetKeyDown(KeyCode.E ) && !DialogueManager.instance.dialogueIsPlaying)
            {
                DialogueManager.instance.EnterDialogueMode(inkJSON, startedDialogue);
                startedDialogue = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

}


   