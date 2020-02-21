using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnvironmentData : MonoBehaviour
{
    Dictionary<string, string> dataMap = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        TextAsset asset = Resources.Load<TextAsset>("Data/ObjectData");
        string txt = asset.text;
        
        //ObjectDataCollection objCollection = JsonUtility.FromJson<ObjectDataCollection>(txt);
        ObjectData[] objectDatas = getJsonArray<ObjectData>(txt);//objCollection.objectDatas;
        foreach (ObjectData data in objectDatas) {
            dataMap.Add(data.name.ToLower(), data.data);
        }

        TextMeshProUGUI textObj = GameObject.Find("Canvas/EnvironmentDataText").GetComponent<TextMeshProUGUI>();
        textObj.text = "Cube:\n" + getObjectData("Cube");
    }

    string getObjectData(string objectName)
    {
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