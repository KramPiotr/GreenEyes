using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnvironmentData : MonoBehaviour
{
    Dictionary<string, string> dataMap = new Dictionary<string, string>();

    // Start is called before the first frame update

    public EnvironmentData() {
        Debug.Log("Let's get data'd");

        TextAsset asset = Resources.Load<TextAsset>("Data/ObjectData");
        ObjectData[] objectDatas = getJsonArray<ObjectData>(asset.text);
        foreach (ObjectData data in objectDatas) {
            dataMap.Add(data.name.ToLower(), data.data);
            Debug.Log("Added data: <" + data.name.ToLower() + ">");
        }
    }

    void Start()
    {
    }

    public string getObjectData(string objectName)
    {
        Debug.Log("Request for: <" + objectName.ToLower() + ">");
        return dataMap[objectName.ToLower()];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static T[] getJsonArray<T>(string json)
    {
        //string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (json);
        return wrapper.objects;
    }
 
    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] objects;
    }

    [System.Serializable]
    class ObjectData
    {
        public string name;
        public string data;
    }
}