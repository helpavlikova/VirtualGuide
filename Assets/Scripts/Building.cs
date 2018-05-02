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
        
        GetComponent<Transform>().position = new Vector3(model.matrix[0], model.matrix[1], model.matrix[2]);
        GetComponent<Transform>().eulerAngles = new Vector3(model.matrix[3], model.matrix[4], model.matrix[5]);
        GetComponent<Transform>().localScale = new Vector3(model.matrix[6], model.matrix[7], model.matrix[8]);

       // GetComponent<Renderer>().material = Resources.Load("red", typeof(Material)) as Material;
    }
}
