using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public Transform example;
    GameObject buildings;
    Transform buildingsTrans;
    DataReader data = new DataReader();


    //draws the scene
    void Start () {
        data.LoadGameData();

        //a parent object for all of the buildings
        buildings = new GameObject("Buildings");
        buildingsTrans = new GameObject("Buildings").transform;

        for (int i = 0; i < data.GetSize(); i++) {
            Transform newObject = Instantiate(example, example.position, example.rotation);
            newObject.GetComponent<Building>().Set(data.GetModel(i));
            newObject.SetParent(buildingsTrans);
        }

        Rigidbody gameObjectsRigidBody = buildings.AddComponent<Rigidbody>(); // Add the rigidbody.
        gameObjectsRigidBody.mass = 5; // Set the GO's mass to 5 via the Rigidbody.
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
