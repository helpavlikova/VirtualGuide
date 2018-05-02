using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

//informace o modelu
[System.Serializable]
public class Model {
    public int id;
    public string name;
    public string link;
    public float[] matrix;
}



public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        Debug.Log("wrapper.Items = " + wrapper.Items.Length);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

public class DataReader { 
   
    List<Model> dataItems = new List<Model>();
    Model[] models;
    string filePath;
    string uri = "https://virtserver.swaggerhub.com/pavlihel9/VirtualGuide/1.0.4/models/42";

    public bool MyRemoteCertificateValidationCallback(System.Object sender,
   X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        // If there are errors in the certificate chain,
        // look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status == X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    continue;
                }
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                bool chainIsValid = chain.Build((X509Certificate2)certificate);
                if (!chainIsValid)
                {
                    isOk = false;
                    break;
                }
            }
        }
        return isOk;
    }


    public void LoadMultipleGameData() {
        string gameDataFileName = "data2.json";
        filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        Debug.Log("filepath = " + filePath);

        if (File.Exists(filePath)) {
            // Read the json from the file into a string

            string dataAsJson = File.ReadAllText(filePath);
            models = JsonHelper.FromJson<Model>(dataAsJson);
            Debug.Log("models [0] name = " + models[0].name);
        }
        else {
            Debug.LogError("Cannot load game data!");
        }

        GetJSONfromAPI();

    }

    public Model GetModel(int i) {
        return models[i];
    }

    public int GetSize() {
        return models.Length;
    }

    private void GetJSONfromAPI() {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format(uri));
        ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        Debug.Log("API response = " + jsonResponse);
    }


    // Update is called once per frame
    void Update() {

    }
}
