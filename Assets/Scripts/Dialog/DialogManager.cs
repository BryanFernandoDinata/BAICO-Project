using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;

    public List<DialogSO> dialogLines = new List<DialogSO>();
    public int currentLine;
    public float wordSpeed;
    private bool justStarted;

    private string questToMark;
    private bool markQuestComplete;
    private bool shouldMarkQuest;

    [HideInInspector] public bool shouldActivateShop = false;

    private void Awake() 
    {
        if(instance == null)
        instance = this;
        else
        Destroy(gameObject);
    }

    void Start()
    {
        dialogText.text = "";
    }

    public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestComplete = markComplete;

        shouldMarkQuest = true;
    }
    public void ShowDialog(List<DialogSO> _lines)
    {
        GameManager.instance.dialogActive = true;

        dialogLines = _lines;
        
        if (!dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(true);
            StartCoroutine(Typing());
        }
        // else if (dialogText.text == dialogLines[currentLine].dialogLine)
        // {
        //     NextLine();
        // }
    }

    public void RemoveText()
    {
        GameManager.instance.dialogActive = false;

        dialogText.text = "";
        currentLine = 0;
        dialogBox.SetActive(false);

        if(shouldActivateShop)
        {
            GameMenu.instance.OpenShop();
            shouldActivateShop = false;   
        }
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogLines[currentLine].dialogLine.ToCharArray())
        {  
            dialogText.text += letter;
            
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (currentLine < dialogLines.Count - 1)
        {
            currentLine++;
            dialogText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }
}