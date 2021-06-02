using UnityEngine;

[CreateAssetMenu(fileName = "New On Screen Quest UI", menuName = "On Screen Quest UI")]
public class QuestUi : ScriptableObject
{
    public string title;
    public string counter;
    public int goal;
}