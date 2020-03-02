using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image _pb1, _pb2;

    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
        
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation mainScene = SceneManager.LoadSceneAsync(1);
        while(mainScene.progress < 1)
        {
            _pb1.fillAmount = mainScene.progress;
            _pb2.fillAmount = mainScene.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
