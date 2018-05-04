using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//budova
public class Building : MonoBehaviour
{
    public string objPath, texturePath;
    void Start()
    {

    }

    private void Update()
    {
    }

    //via https://answers.unity.com/questions/432655/loading-texture-file-from-pngjpg-file-on-disk.html
    public static Texture2D LoadTexture(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;
        Debug.Log("filepath = " + filePath);

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    //nastavení budovy
    public void Set( Model model )
    {
        objPath = Path.Combine(Application.streamingAssetsPath, model.file_link);
        texturePath = Path.Combine(Application.streamingAssetsPath, model.texture_link);


        GetComponent<MeshFilter>().mesh = FastObjImporter.Instance.ImportFile(objPath);
        
        GetComponent<Transform>().position = new Vector3(model.position_coords.x, model.position_coords.y, model.position_coords.z);
        GetComponent<Transform>().eulerAngles = new Vector3(model.rotation_coords.x, model.rotation_coords.y, model.rotation_coords.z);
        GetComponent<Transform>().localScale = new Vector3(model.scale_coords.x, model.scale_coords.y, model.scale_coords.z);

        // GetComponent<Renderer>().material = Resources.Load("red", typeof(Material)) as Material;
        Renderer rend = GetComponent<Renderer>();
        rend.material.mainTexture = LoadTexture(texturePath);
    }
}
