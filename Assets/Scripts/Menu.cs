using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Card;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private GameObject CharacterMenu;
    [SerializeField] private GameObject LevelSelectMenu;
    [SerializeField] private GameObject SaveSelectMenu;
    [SerializeField] private Button PartyFinishButton;
    [SerializeField] private Button CharacterSaveButton;
    [SerializeField] private Image FadeDrop;
    [SerializeField] private GameObject CharacterCard;
    private float FadeLerp = 0f;
    private Options options;
    private bool NewGame = false;
    private bool characterMenu = false;
    private CharacterCard editingCard;
    private List<CharacterCard> completeCards = new();
    private List<CharacterCard> incompleteCards = new();

    void Start()
    {
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        CharacterMenu.SetActive(false);
        LevelSelectMenu.SetActive(false);
        SaveSelectMenu.SetActive(false);
        FadeDrop.gameObject.SetActive(true);
        FadeDrop.color = Color.clear;
        options = Options.Get();
        GameManager.OptionsReload();
    }
    private void Update()
    {
        FadeDrop.color = Color.Lerp(Color.clear, Color.black, FadeLerp);
        if (characterMenu)
        {
            PartyFinishButton.interactable = completeCards.Count > 3;
        }
    }
    public void StartGame()
    {
        FadeOut(1000);
        GameManager.StartGame();
    }
    public void ToMain()
    {
        Wait(500);
        FadeOut(500);
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        CharacterMenu.SetActive(false);
        LevelSelectMenu.SetActive(false);
        SaveSelectMenu.SetActive(false);
        FadeIn(500);
    }
    public void ToOptions()
    {
        Wait(500);
        FadeOut(500);
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        CharacterMenu.SetActive(false);
        LevelSelectMenu.SetActive(false);
        SaveSelectMenu.SetActive(false);
        FadeIn(500);
    }
    public void ToLevelSelect()
    {
        Wait(500);
        FadeOut(500);
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        CharacterMenu.SetActive(false);
        LevelSelectMenu.SetActive(false);
        SaveSelectMenu.SetActive(false);
        FadeIn(500);
    }
    public void ToSaveSelect()
    {
        Wait(500);
        FadeOut(500);
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        CharacterMenu.SetActive(false);
        LevelSelectMenu.SetActive(false);
        SaveSelectMenu.SetActive(true);
        FadeIn(500);
    }
    public void ToCharacterMenu()
    {
        Wait(500);
        FadeOut(500);
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        CharacterMenu.SetActive(true);
        LevelSelectMenu.SetActive(false);
        SaveSelectMenu.SetActive(false);
        CharacterSaveButton.interactable = false;
        PartyFinishButton.interactable = false;
        FadeIn(500);
        characterMenu = true;
        CharacterMenuStart();
    }
    public void SaveSelected(string path)
    {
        options.SavePath = path;
        GameManager.OptionsReload();
        if(NewGame)
        {
            GameManager.NewGameInFile();
            ToCharacterMenu();
        }
        else
        {
            GameManager.LoadGameFromFile();
            StartGame();
        }
    }
    public void Quit()
    {
        Wait(500);
        FadeOut(500);
        Wait(500);
        Application.Quit();
    }
    public void SaveOptionsClicked()
    {
        Options.Apply(options);
        ToMain();
    }
    public void NewGameClicked()
    {
        NewGame = true;
        ToSaveSelect();
    }
    public void LevelSelected(string LevelName)
    {
        GameManager.LevelName = LevelName;
        StartGame();
    }
    public void CharacterMenuStart()
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject _cardObject = Instantiate(CharacterCard, new Vector3(0, -10, 0), Quaternion.identity);
            CharacterCard _newCard = _cardObject.GetComponent<CharacterCard>();
            (_newCard as ICard).MoveToPosition(new Position(ContainerRegistry.GetContainer("Stack_Incomplete"), 0));
            incompleteCards.Add(_newCard);
            Debug.Log(incompleteCards);
            _newCard.Character.BasePower = 1;
            _newCard.Character.BaseResolve = 3;
            _newCard.Character.BaseCondition = 4;
            _newCard.Character.BaseHumanity = 5;
            _newCard.Character.orderInParty = i;
        }
        (incompleteCards[0] as ICard).MoveToPosition(new Position(ContainerRegistry.GetContainer("Space_Editing"), 0));
        editingCard = incompleteCards[0];
        incompleteCards.RemoveAt(0);
    }
    public void CancelCharacterMenu()
    {
        FadeOut(500);
        foreach (CharacterCard CharCard in incompleteCards)
        {
            Destroy(CharCard.gameObject);
            Destroy(CharCard);
        }
        incompleteCards = new List<CharacterCard>();
        if (editingCard != null)
        {
            Destroy(editingCard.gameObject);
            Destroy(editingCard);
        }
        editingCard = null;
        foreach (CharacterCard CharCard in completeCards)
        {
            Destroy(CharCard.gameObject);
            Destroy(CharCard);
        }
        completeCards = new List<CharacterCard>();
        CharacterMenu.SetActive(false);
        SaveSelectMenu.SetActive(true);
        Wait(500);
        FadeIn(500);
    }
    public void CompleteCharacterCard()
    {
        completeCards.Add(editingCard);
        (editingCard as ICard).MoveToPosition(new Position(ContainerRegistry.GetContainer("Space_Complete"), 0));
        if(incompleteCards.Count > 0)
        {
            (incompleteCards[0] as ICard).MoveToPosition(new Position(ContainerRegistry.GetContainer("Space_Editing"), 0));
            editingCard = incompleteCards[0];
            incompleteCards.RemoveAt(0);
        }
        CharacterSaveButton.interactable = false;
    }
    public void FinishCharacterMenu()
    {
        FadeOut(500);
        foreach (CharacterCard CharCard in completeCards)
        {
            GameManager.Party.Add(CharCard.Character);
        }
        foreach (CharacterCard CharCard in incompleteCards)
        {
            Destroy(CharCard.gameObject);
        }
        incompleteCards = new List<CharacterCard>();
        Destroy(editingCard.gameObject);
        editingCard = null;
        foreach (CharacterCard CharCard in completeCards)
        {
            Destroy(CharCard.gameObject);
        }
        completeCards = new List<CharacterCard>();
        CharacterMenu.SetActive(false);
        LevelSelectMenu.SetActive(true);
        Wait(500);
        FadeIn(500);
    }
    public void CharacterNameInput(TMPro.TMP_InputField field)
    {
        if(editingCard != null)
        {
            editingCard.Character.name = field.text;
        }
    }
    public void EditCharacterPortrait(string fileName)
    {
        if(editingCard != null)
        {
            editingCard.Character.PortraitFileName = fileName;
        }
    }
    private IEnumerator Wait(int Delay)
    {
        yield return new WaitForSeconds(Delay / 1000);
    }
    private IEnumerator FadeOut(int Delay)
    {
        for(int i = 0; i < Delay; i++) 
        {
            FadeLerp = Mathf.Lerp(0, 1, i / Delay);
            yield return new WaitForSeconds(1 / 1000);
        }
    }
    private IEnumerator FadeIn(int Delay)
    {
        for (int i = 0; i < Delay; i++)
        {
            FadeLerp = Mathf.Lerp(1, 0, i / Delay);
            yield return new WaitForSeconds(1 / 1000);
        }
    }
}

[Serializable]
public struct Options
{
    public string SavePath;
    public Options(string SavePath)
    {
        this.SavePath = SavePath;
    }
    public static void Apply(Options options)
    {
        string OptionsData = JsonUtility.ToJson(options);
        File.WriteAllText("User/options.txt", OptionsData);
        GameManager.OptionsReload();
    }
    public static Options Get()
    {
        string OptionsData = File.ReadAllText("User/options.txt");
        return JsonUtility.FromJson<Options>(OptionsData);
    }
}
