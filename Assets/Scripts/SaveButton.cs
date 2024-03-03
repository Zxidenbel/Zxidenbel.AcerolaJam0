using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Card;

public class SaveButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SaveName;
    [SerializeField] private TextMeshProUGUI LevelName;
    [SerializeField] private string FileName;
    private GameSave SaveData;

    void Awake()
    {
        SaveData = new GameSave(FileName);
        List<CharacterSave> _CharList = new(SaveData.Party);
        string _PartyName = string.Empty;
        foreach (CharacterSave characterSave in _CharList)
        {
            _PartyName = _PartyName + ", " + characterSave.name;
        }
        _PartyName.TrimStart(',', ' ');
        if(_PartyName != string.Empty)
        {
            SaveName.text = _PartyName;
            LevelName.text = SaveData.LevelName + "\n" + SaveData.Progress;
        }
        else
        {
            SaveName.text = "New Game";
            LevelName.text = "";
        }
     }
}
