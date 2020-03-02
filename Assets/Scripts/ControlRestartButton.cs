using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlRestartButton : MonoBehaviour
{
    public void Restart()
    {
        Debug.Log("Control Restart Button pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}