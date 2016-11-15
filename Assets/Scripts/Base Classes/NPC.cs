using UnityEngine;
using Rewired;

public class NPC : MonoBehaviour {

    public string Name;
    PlayerController player;
    bool triggerStay = false;

    #region Start and Update

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

        if (triggerStay)
        {
            if (ReInput.players.GetPlayer(0).GetButtonDown("Jump") && !ConversationManager.Instance._GetTalking())
            {
                ConversationManager.Instance.StartConversation(GetComponent<ConversationComponent>().Conversations[0]);
            }
        }

        if (triggerStay == true && ConversationManager.Instance._GetTalking())
        {
            ConversationManager.Instance.pressToTalk.text = "";
            if (ReInput.players.GetPlayer(0).GetButtonDown("Jump") && ConversationManager.Instance._GetTalking())
            {
                ConversationManager.Instance.stepSpeed = 0.0f;
            }

            if (ReInput.players.GetPlayer(0).GetButtonDown("Jump") && ConversationManager.Instance._GetStepCoroutine() == false && ConversationManager.Instance._GetTalking())
            {
                ConversationManager.Instance._SetNextLine(true);
            }
        }

    }

    #endregion

    #region Triggers

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            ConversationManager.Instance.pressToTalk.text = "";
            player.canJump = true;
            triggerStay = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            triggerStay = true;
            player.canJump = false;
            ConversationManager.Instance.pressToTalk.text = "Press A To Talk";
        }
    }

    #endregion
}
