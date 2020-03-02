using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
	[SerializeField]
/*	public GameObject emptyDrop; // This is our prefab object that will be exposed in the inspector
	public GameObject emptyFire; // This is our prefab object that will be exposed in the inspector
	public GameObject emptyLand; // This is our prefab object that will be exposed in the inspector
	public GameObject fullDrop; // This is our prefab object that will be exposed in the inspector
	public GameObject fullLand; // This is our prefab object that will be exposed in the inspector
	public GameObject fullFire; // This is our prefab object that will be exposed in the inspector*/
	public GameObject[] prefabs;
	public int[] scores;

	//[SerializeField]
	public int numberToCreate; // number of objects to create. Exposed in inspector

	void Start()
	{
	}

	void Update()
	{

	}

	public void Populate()
	{
		GameObject newObj; // Create GameObject instance

		int prefabKind;

		for (int i = 0; i < 3; i++)
		{
			prefabKind = i * 2;
			for(int j = 0; j < 5; j++)
			{
				if(j == scores[i])
				{
					prefabKind++;
				}
				newObj = (GameObject)Instantiate(prefabs[prefabKind], transform);
			}
		}

	}
}