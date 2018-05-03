using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//budova
public class Building : MonoBehaviour
{
      
    void Start()
    {

    }

    private void Update()
    {
    }

    //nastavení budovy
    public void Set( Model model )
    {
        GetComponent<MeshFilter>().mesh = FastObjImporter.Instance.ImportFile(model.link);
        
        GetComponent<Transform>().position = new Vector3(model.position_coords.x, model.position_coords.y, model.position_coords.z);
        GetComponent<Transform>().eulerAngles = new Vector3(model.rotation_coords.x, model.rotation_coords.y, model.rotation_coords.z);
        GetComponent<Transform>().localScale = new Vector3(model.scale_coords.x, model.scale_coords.y, model.scale_coords.z);

       // GetComponent<Renderer>().material = Resources.Load("red", typeof(Material)) as Material;
    }
}
