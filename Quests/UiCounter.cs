using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiCounter : MonoBehaviour
{
    private static UiCounter instance;
    public static UiCounter Instance { get { return instance; } }
    public Text counter;
    public int killCount;
    public QuestUi[] questGoal; 
    public int currentGoalIndex = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void UpdateUIKills()
    {   
        if (killCount <= questGoal[currentGoalIndex].goal)
        {
            counter.text = killCount.ToString();
        }
    }
}
