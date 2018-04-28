using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public Transform example;
    Transform buildings;

    DataReader sqlc = new DataReader();
    //vykreslení scény
    void Start () {
        sqlc.LoadGameData();

        buildings = new GameObject("Buildings").transform;

        for (int i = 0; i < sqlc.GetSize(); i++)
        {
            Transform newObject = Instantiate(example, example.position, example.rotation);
            newObject.GetComponent<Building>().Set(sqlc.GetModel(i));
            newObject.SetParent(buildings);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
