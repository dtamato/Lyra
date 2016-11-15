using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConversationManager : MonoBehaviour {

    #region Public Variables

    //Used to create singleton implementation
    public static ConversationManager Instance { get; private set; }

    [Tooltip("Speed of text step in seconds.")]
    public float stepSpeed;

    //UI variables from scene
    public Text characterName;
    public Text characterDialogue;
    public Image characterImage;
    public Text pressToTalk;
    #endregion

    #region Private Variables

    //stepCoroutine is used to check whether or not the StepConversationText coroutine is running.
    bool stepCoroutine = false;

    //Used to set the speed back to what was defined in the inspector
    float definedSpeed;

    //Used to check if the player is currently in a conversation
    bool talking = false;

    //Used to check if it is possible to proceed to the next line in the conversation
    bool nextLine = false;

    //Used to hold the current conversation line
    ConversationEntry currentConversationLine;

    //Used to keep a reference to the conversation length for use in stepping through an array
    int conversationLengthInCharacters;

    #endregion

    #region Awake, Start and Update

    //Called as soon as the object is instantiated
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Initialize variables to blank
    void Start()
    {
        pressToTalk.text = "";
        characterName.text = "";
        characterDialogue.text = "";
        characterImage.color = Color.clear;
        definedSpeed = stepSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region Getters and Setters

    public bool _GetStepCoroutine()
    {
        return stepCoroutine;
    }

    public void _SetStepCoroutine(bool value)
    {
        stepCoroutine = value;
    }

    public bool _GetTalking()
    {
        return talking;
    }

    public void _SetTalking(bool value)
    {
        talking = value;
    }

    public bool _GetNextLine()
    {
        return nextLine;
    }

    public void _SetNextLine(bool value)
    {
        nextLine = value;
    }

    #endregion

    public void StartConversation(Conversation conversation)
    {
        if(!talking)
        {
            StartCoroutine(DisplayConversation(conversation));
        }
    }

    IEnumerator DisplayConversation(Conversation conversation)
    {
        /*Loop through each conversation line in the conversation lines array and set the UI elements to 
         * equal the variables in the conversation entry. If the sprite that is being passed in is not the
         * player sprite then change the scale to -1 and move it to the right side of the screen.
         * Start the StepConversationText coroutine which will display each character one by one at the previously defined
         * speed. Lastly, wait for player input using the nextLine boolean and until the UI text element equals the conversation line text
         * Set the next line variable back to false and set the step speed back to the defined speed and then clear the UI elements. 
         * Loop if there are anymore conversation lines in the array. 
         * Set talking to false when conversation is complete
         */

        talking = true;

        foreach (var conversationLine in conversation.ConversationLines)
        {
            currentConversationLine = conversationLine;
            conversationLengthInCharacters = currentConversationLine.ConversationText.Length;
            characterName.text = currentConversationLine.SpeakingCharacterName;
            characterImage.sprite = currentConversationLine.CharacterDisplayPicture;


            if (characterImage.sprite.name == "Player")
            {
                characterImage.transform.localScale = new Vector3(1, 1, 1);
                characterImage.rectTransform.anchoredPosition = new Vector2(-Mathf.Abs(characterImage.rectTransform.anchoredPosition.x), characterImage.rectTransform.anchoredPosition.y);
            }
            else
            {
                characterImage.transform.localScale = new Vector3(-1, 1, 1);
                characterImage.rectTransform.anchoredPosition = new Vector2(Mathf.Abs(characterImage.rectTransform.anchoredPosition.x), characterImage.rectTransform.anchoredPosition.y);
            }

            characterImage.color = Color.white;

            StartCoroutine(StepConversationText(conversation, conversationLengthInCharacters));

            yield return new WaitUntil(() => nextLine == true && characterDialogue.text == currentConversationLine.ConversationText);

            nextLine = false;
            stepSpeed = definedSpeed;
            
            characterDialogue.text = "";
            characterName.text = "";
        }
        talking = false;
        characterImage.color = Color.clear;
    }

    IEnumerator StepConversationText(Conversation conversation, int conversationLengthInCharacters)
    {
        /* Set the stepCoroutine variable to true to let the game know that the StepConversationText 
         * coroutine is in progress. Loop through all of the letters in the current conversation line 
         * and add it to the characterDialogue.text UI element one by one. If the variable was not changed 
         * then wait for the defined step speed until stepping through the next letter in the conversation line.
         * If the stepSpeed was changed by the user using the stepSpeed variable then break out of the 
         * coroutine and display the rest of the text.
         */

        stepCoroutine = true;
        for (int i = 0; i < conversationLengthInCharacters; i++)
        {
            characterDialogue.text += currentConversationLine.ConversationText[i];

            if (stepSpeed != 0)
            {
                yield return new WaitForSeconds(stepSpeed);
            }
        }
        stepCoroutine = false;
    }

}
