using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    
    public void loadMainScene() {
        SceneManager.LoadScene("mainScene");
    }

    public void setStreet()
    {
        Settings.Path = "data4.json";
        Debug.Log(Settings.Path);
    }

    public void setSquare()
    {
        Settings.Path = "data5.json";
        Debug.Log(Settings.Path);
    }
}
