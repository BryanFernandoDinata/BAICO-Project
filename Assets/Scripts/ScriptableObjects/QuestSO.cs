using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName ="Quest")]
public class QuestSO : ScriptableObject
{
    public string questName;
    public int doneOutOf;
    public int needToDo;
    public bool questIsDone = false;
}
