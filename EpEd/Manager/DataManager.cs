using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class DataManager : MonoBehaviour
{
    private void Start()
    {
        Data data = new Data(GameManager.instance.selectLevel, GameManager.instance.clearCheck);
        Save(data, @"\data.json");
    }

    public void Save(Data data, string path)
    {
        JsonData jsonData = JsonMapper.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + path, jsonData.ToString());
    }

    public JsonData Load(string path)
    {
        // @"\data.json"
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + path);
        JsonData jsonData = JsonMapper.ToJson(jsonString);
        return jsonData;
    }
}

public class Data
{
    public int level;
    public bool[] clear;

    public Data(int level, bool[] clear)
    {
        this.level = level;
        this.clear = clear;
    }
}
