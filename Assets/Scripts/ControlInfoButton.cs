using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInfoButton : MonoBehaviour
{
    public GameObject infoPanelPrefab;

    static public GameObject infoPanel;
    private void Start()
    {
        ShowInfo();
    }
    public void ShowInfo()
    {
        if(infoPanel == null)
        {
            Debug.Log("Control Info Button pressed.");
            Vector3 position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 0.5f);
            infoPanel = GameObject.Instantiate(infoPanelPrefab, position, Camera.main.transform.rotation);
            infoPanel.transform.localPosition = position;
        }

    }
}
