using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnvironmentData : MonoBehaviour
{
    Dictionary<string, ObjectData> dataMap = new Dictionary<string, ObjectData>();

    public double[,] usageBounds = new double[3, 6];

    // Start is called before the first frame update

    public EnvironmentData() {
        Debug.Log("Let's get data'd");

        TextAsset asset = Resources.Load<TextAsset>("Data/ObjectData");
        ObjectData[] objectDatas = getJsonArray(asset.text);
        foreach (ObjectData data in objectDatas) {
            data.ProcessDoubles();
            dataMap.Add(data.name.ToLower(), data);
        }
        ComputeBounds(objectDatas);
        foreach (ObjectData data in objectDatas)
        {
            data.ComputeOwnBounds(usageBounds);
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
        public double carbonUsage;
        public string LandUse;
        public double landUsage;
        public string WaterUse;
        public double waterUsage;
        public double probability;

        public int carbonScore = 0, landScore = 0, waterScore = 0; 
        public void ProcessDoubles()
        {
            carbonUsage = Math.Log(Double.Parse(ClimateChange, System.Globalization.NumberStyles.Float));
            landUsage = Math.Log(Double.Parse(LandUse, System.Globalization.NumberStyles.Float));
            waterUsage = Math.Log(Double.Parse(WaterUse, System.Globalization.NumberStyles.Float));
        }

        public void ComputeOwnBounds(double[,] usageBounds)
        {
            for(int i = 1; i<6; i++)
            {
                if(carbonUsage>usageBounds[0, i])
                {
                    carbonScore++;
                }
                if (landUsage > usageBounds[1, i])
                {
                    landScore++;
                }
                if (waterUsage > usageBounds[2, i])
                {
                    waterScore++;
                }
            }
        }
        public ObjectData(string name, string text)
        {
            this.name = name;
            this.ClimateChange = text;
        }
    }

    private void ComputeBounds(ObjectData[] objects)
    {
        for(int i = 0; i<3; i++)
        {
            usageBounds[i, 5] = 0;
            usageBounds[i, 0] = 10000;
        }
        for(int i = 0; i<10; i++)
        {
            usageBounds[0, 0] = Math.Min(usageBounds[0, 0], objects[i].carbonUsage);
            usageBounds[1, 0] = Math.Min(usageBounds[1, 0], objects[i].landUsage);
            usageBounds[2, 0] = Math.Min(usageBounds[2, 0], objects[i].waterUsage);

            usageBounds[0, 5] = Math.Max(usageBounds[0, 5], objects[i].carbonUsage);
            usageBounds[1, 5] = Math.Max(usageBounds[1, 5], objects[i].landUsage);
            usageBounds[2, 5] = Math.Max(usageBounds[2, 5], objects[i].waterUsage);
        }

        for(int i = 0; i<3; i++)
        {
            for(int j = 0; j<10; j+=5)
            {
                usageBounds[i, j] -= 0.000001;
            }

            double difference = (usageBounds[i, 5] - usageBounds[i, 0])/5;

            for(int j = 1; j<5; j++)
            {
                usageBounds[i, j] = usageBounds[i, j - 1] + difference;
            }
        }

    }
}