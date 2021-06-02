using UnityEngine;
using UnityEngine.UI;

public class OnScreenQuestUi : MonoBehaviour
{
    public QuestUi[] questUi;
    [SerializeField] private Text killGoalText;
    [SerializeField] private Text titleText;
    [SerializeField] private Text counterText;
    private int currentQuestUiIndex = 0;
    public int CurrentQuestUiIndex
    {
        get
        {
            return currentQuestUiIndex;
        }
        set
        {
            if (value <= 0)
            {
                currentQuestUiIndex = 0;
            }
            else
            {
                currentQuestUiIndex = value;
            }

            QuestUI();
        }
    }

    private void Start()
    {
        QuestUI();  // Required to set the first SO, otherwise it won't populate first quest.
    }

    private void QuestUI()
    {
        titleText.text = questUi[currentQuestUiIndex].title;
        counterText.text = questUi[currentQuestUiIndex].counter;
        killGoalText.text = questUi[currentQuestUiIndex].goal.ToString();
    }
}

