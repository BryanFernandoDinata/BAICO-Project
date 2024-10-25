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

    [HideInInspector] public bool shouldActivateShop = false;
    [HideInInspector] public bool shouldActivateQuest = false;
    [HideInInspector] public QuestSO questToGive;
    GameObject questUI;


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

        if(shouldActivateQuest)
        {
            UpdateQuest(questToGive, GameMenu.instance.questItemUI);
        }

        nameText.text = dialogLines[currentLine].speakerName;
    }
    public void UpdateQuest(QuestSO _quest, QuestItemUI _questUI)
    {
        if(PlayerPrefs.HasKey(_quest.questName))
        {
            if(PlayerPrefs.GetString(_quest.questName) == _quest.questName + "Done")
            {
                _quest.questIsDone = true;
            }else
            {
                _quest.questIsDone = false;
            }
        }else
        {
            _quest.questIsDone = false;
            questUI = Instantiate(_questUI.gameObject);

            PlayerPrefs.SetString(_quest.questName, _quest.questName); 
        }
        
        
        if(_quest.questIsDone)
        {
            questUI.transform.parent = GameMenu.instance.questDoneHolder.transform;
        }else
        {
            questUI.transform.parent = GameMenu.instance.questOngoingHolder.transform;
        }
        

        UpdateTextQuest(_quest, _questUI);
    }
    public void UpdateTextQuest(QuestSO _quest, QuestItemUI _questUI)
    {
        _questUI.questNameText.text = _quest.questName;
        _questUI.valueText.text = "(" + _quest.doneOutOf.ToString() + " / " + _quest.needToDo.ToString() + ")";
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

        nameText.text = dialogLines[currentLine].speakerName;
    }
}