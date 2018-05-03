using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json;

//a root clas encompassing the array of all models
public class Root
{
    public Model[] models;
}


public class Coords {
    public float x;
    public float y;
    public float z;
}

//informace o modelu
[System.Serializable]
public class Model {
    public int id;
    public string name;
    public string link;
    public Coords position_coords;
    public Coords rotation_coords;
    public Coords scale_coords;
}

public class DataReader { 
   
    Root root;
    string filePath;
    string uri = "https://virtserver.swaggerhub.com/pavlihel9/VirtualGuide/1.0.4/models/42";
    string gameDataFileName = "data3.json";

    //via https://answers.unity.com/questions/792342/how-to-validate-ssl-certificates-when-using-httpwe.html
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
        filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        Debug.Log("filepath = " + filePath);

        if (File.Exists(filePath)) {
            // Read the json from the file into a string

            string dataAsJson = File.ReadAllText(filePath);
            
            root = JsonConvert.DeserializeObject<Root>(dataAsJson);

            Debug.Log("models [0] name = " + root.models[0].name);
        }
        else {
            Debug.LogError("Cannot load game data!");
        }

        GetJSONfromAPI();

    }

    public Model GetModel(int i) {
        return root.models[i];
    }

    public int GetSize() {
        return root.models.Length;
    }

    private void GetJSONfromAPI() {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format(uri));

        //a special function to add a certificate (otherwise Mono won't allow any connections]
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
