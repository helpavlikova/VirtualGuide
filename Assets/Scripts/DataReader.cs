using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


//informace o modelu
public struct Model {
    public int id;
    public string name;
    public string link;
    public float[] matrix;
}


public class DataReader { 
   
    List<Model> dataItems = new List<Model>();
    string gameDataFileName = "data.json";
    string filePath;



    public void LoadGameData()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        Debug.Log("filepath = " + filePath);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            Model tmpModel = new Model();
            tmpModel = JsonUtility.FromJson<Model>(dataAsJson);
            dataItems.Add(tmpModel);
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }

    }

    public Model GetModel(int i) {
        return dataItems[i];
    }

    public int GetSize() {
        return dataItems.Count;
    }

    // Update is called once per frame
    void Update() {

    }
}
