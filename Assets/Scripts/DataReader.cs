using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//informace o modelu
[System.Serializable]
public class Model {
    public int id;
    public string name;
    public string link;
    public float[] matrix;
}

public static class JsonHelper {
    public static T[] FromJson<T>(string json){
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        Debug.Log("wrapper.Items = " + wrapper.Items.Length);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array) {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint) {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T> {
        public T[] Items;
    }
}

public class DataReader { 
   
    List<Model> dataItems = new List<Model>();
    Model[] models;
    string filePath;
   

    public void LoadGameData() {
        string gameDataFileName = "data.json";
        filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        Debug.Log("filepath = " + filePath);

        if (File.Exists(filePath)) {
            // Read the json from the file into a string

            string dataAsJson = File.ReadAllText(filePath);
            Model tmpModel = new Model();

            //mapping the json file onto the Model object
            tmpModel = JsonUtility.FromJson<Model>(dataAsJson);
            dataItems.Add(tmpModel);
        }
        else {
            Debug.LogError("Cannot load game data!");
        }

    }

    public void LoadMultipleGameData() {
        string gameDataFileName = "data2.json";
        filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        Debug.Log("filepath = " + filePath);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string

            string dataAsJson = File.ReadAllText(filePath);
            models = JsonHelper.FromJson<Model>(dataAsJson);
            Debug.Log("models [0] name = " + models[0].name);
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }

    }

    public Model GetModel(int i) {
        return models[i];
    }

    public int GetSize() {
        return models.Length;
    }

    // Update is called once per frame
    void Update() {

    }
}
