using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnvironmentData : MonoBehaviour
{
    Dictionary<string, ObjectData> dataMap = new Dictionary<string, ObjectData>();

    // Start is called before the first frame update

    public EnvironmentData() {
        Debug.Log("Let's get data'd");

        TextAsset asset = Resources.Load<TextAsset>("Data/ObjectData");
        ObjectData[] objectDatas = getJsonArray(asset.text);
        foreach (ObjectData data in objectDatas) {
            dataMap.Add(data.name.ToLower(), data);
        }
    }

    public ObjectData getObjectData(string objectName)
    {
        if (!dataMap.ContainsKey(objectName.ToLower()))
        {
            return new ObjectData(objectName, "No data found");
        }
        return dataMap[objectName.ToLower()];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static ObjectData[] getJsonArray(string json)
    {
        //string newJson = "{ \"array\": " + json + "}";
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        return wrapper.objects;
    }
 
    [System.Serializable]
    private class Wrapper
    {
        public ObjectData[] objects;
    }

    [System.Serializable]
    public class ObjectData
    {
        public string name;
        public string ClimateChange;
        public string LandUse;
        public string WaterUse;
        public double probability;

        public ObjectData(string name, string text)
        {
            this.name = name;
            this.ClimateChange = text;
        }
    }
}