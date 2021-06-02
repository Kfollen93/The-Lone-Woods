using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public GameObject questWindow;
    public GameObject completedBackgroundGO;
    public GameObject onScreenQuestUI;
    public UiCounter uiCounter;
    public Text nameText;
    public Text descriptionText;
    public Text completedText;
    public bool isQuestAccepted;
    public bool isQuestComplete;
    public bool isQuestTurnedIn;
    public Quest[] Quests;
    public int currentQuestIndex = 0;
    public OnScreenQuestUi onScreenQuestUi;
    public QuestUi[] questUi;
    public QuestUi[] questGoals;
    public int activeGoalIndex = 0;
    private bool hasGreetedCowyboy;
    private bool playerPressedE;
    private bool playerIsInQuestTriggerSpot;
    private bool QuestIsNotStarted => !isQuestAccepted && !isQuestComplete;
    private bool QuestIsReadyForTurnIn => isQuestAccepted && isQuestComplete && !isQuestTurnedIn;
    private bool PlayerIsAtQuestPickupLocationAndPressedE => playerIsInQuestTriggerSpot && Input.GetKeyDown("e");

    public void Update()
    {
        if (uiCounter.killCount >= questGoals[activeGoalIndex].goal)
        {
            isQuestComplete = true;
        }

        // Handles the first quest.
        if (currentQuestIndex == 0 && hasGreetedCowyboy)
        {
            UiCounter.Instance.killCount++;
            UiCounter.Instance.UpdateUIKills();
        }

        /* This function is for the GetKeyDown() function, which is required to be ran in the Update() function, since the state gets reset each frame.
           Trying to call GetKeyDown() in OnTriggerStay() is not reliable. */
        if (PlayerIsAtQuestPickupLocationAndPressedE)
        {
            playerPressedE = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && QuestIsNotStarted)
        {
            playerIsInQuestTriggerSpot = true;
            OpenQuestWindow();

            if (playerPressedE)
            {
                playerPressedE = false;
                hasGreetedCowyboy = true;
                AcceptQuestCloseWindow();  
            }
        } 
        else if (other.CompareTag("Player") && QuestIsReadyForTurnIn)
        {
            playerIsInQuestTriggerSpot = true;
            QuestComplete();

            if (playerPressedE)
            {
                playerPressedE = false;
                playerIsInQuestTriggerSpot = true;
                onScreenQuestUI.SetActive(false);
                completedBackgroundGO.SetActive(false);
                isQuestTurnedIn = true;
                NextQuest();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInQuestTriggerSpot = false;
            Debug.Log("Close window but do not accept");
            questWindow.SetActive(false);
            completedBackgroundGO.SetActive(false);
        }
    }

    public void OpenQuestWindow()
    {
        var quest = GetCurrentQuest();
        questWindow.SetActive(true);
        nameText.text = quest.questName;
        descriptionText.text = quest.description;
    }

    public void AcceptQuestCloseWindow()
    {
        questWindow.SetActive(false);
        isQuestAccepted = true;
        onScreenQuestUI.SetActive(true);
    }

    public void QuestComplete()
    {
        var quest = GetCurrentQuest();
        completedBackgroundGO.SetActive(true);
        completedText.text = quest.completed;
    }

    Quest GetCurrentQuest()
    {
        if (currentQuestIndex >= Quests.Length) return null;
        else return Quests[currentQuestIndex];
    }

    void NextQuest()
    {
        if (currentQuestIndex < Quests.Length && onScreenQuestUi.CurrentQuestUiIndex < (questUi.Length - 1))
        {
            UiCounter.Instance.killCount = 0;
            isQuestTurnedIn = false;
            isQuestComplete = false;
            isQuestAccepted = false;
            currentQuestIndex++;
            onScreenQuestUi.CurrentQuestUiIndex++; // Updated this to the public getter/setter   
            // This iterates the counter, it's using a different variable, but it still starts at 0 index so it matches with the var below.
            uiCounter.currentGoalIndex++;
            // Iterates Update() method at the top to change the index each time when quest complete (again different variable, but starts at 0 still)
            activeGoalIndex++;
        }
    }
}
