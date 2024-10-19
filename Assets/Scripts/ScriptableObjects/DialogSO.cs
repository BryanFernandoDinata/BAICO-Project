using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog")]
public class DialogSO : ScriptableObject
{
    [Header("Strings")]
    public string speakerName;
    public string dialogLine;
}
