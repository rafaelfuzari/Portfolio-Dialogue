using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue/UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    public Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    public GameObject[] choices;
    public TextMeshProUGUI[] choicesText;
    public bool choiceDialogue;


    string storyState;
    private static DialogueManager _instance;
    public static DialogueManager instance


    {

        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<DialogueManager>();
                if (_instance == null)
                {
                    GameObject AM = Instantiate(Resources.Load<GameObject>("DialogueManager"));
                    _instance = AM.GetComponent<DialogueManager>();
                }

            }

            return _instance;
        }
    }// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueIsPlaying) return;

        if (Input.GetKeyDown(KeyCode.Space) && !choiceDialogue) ContinueStory();
    }

    public void EnterDialogueMode(TextAsset inkJSON, bool started)
    {
        currentStory = new Story(inkJSON.text);
        if (started)
        {
            currentStory.state.LoadJson(storyState);
            currentStory.ChoosePathString("choose");
        }
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }
    
    public void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    public void ContinueStory()
    {
       
        
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            DisplayChoices();
        }
        else ExitDialogueMode();
    }

    public void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        choiceDialogue = currentChoices.Count > 0 ? true : false;
        if (currentChoices.Count > choices.Length) Debug.LogError($"More choices are given than the UI supports, the UI suports {0} choices " + choices.Length);

        int index = 0;

        //setting up the choice buttons and the text
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);

        }

    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);       
        ContinueStory();
        storyState = currentStory.state.ToJson();

    }

   
}
