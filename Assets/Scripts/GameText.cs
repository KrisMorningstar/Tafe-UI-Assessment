using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameText : MonoBehaviour, IDataPersistence
{
    [System.NonSerialized]
    public string gameText = "> New Game";
    [SerializeField] private TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField.text = gameText;
        inputField.ForceLabelUpdate();
    }

    public void LoadData(GameData _data)
    {
        inputField.text = _data.gameText;
    }

    public void SaveData(ref GameData _data)
    {
        _data.gameText = this.inputField.text;
    }
}
