using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDeleteButton : MonoBehaviour
{
    public void CloseDataPanel()
    {
        Destroy(transform.parent.gameObject);
        Debug.Log("close data panel");
    }
}
