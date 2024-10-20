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
    public GameObject pressEObject;
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
    private void Update() 
    {
        
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
            AudioManager.instance.PlaySFX(8);
            yield return new WaitForSeconds(wordSpeed);
        }

        if(dialogText.text == dialogLines[currentLine].dialogLine)
        {   
            if(pressEObject.activeInHierarchy == false)
            {
                DialogManager.instance.pressEObject.SetActive(true);
            }
        }
    }

    public void NextLine()
    {
        if(pressEObject.activeInHierarchy == true)
        {
            DialogManager.instance.pressEObject.SetActive(false);
        }
        
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