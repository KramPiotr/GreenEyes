using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPanelLerper : MonoBehaviour
{
    private Vector3 scale0 = new Vector3(0, 1, 1);
    private Vector3 scale1 = new Vector3(1, 1, 1);
    private Vector3 windowScale0 = new Vector3(0.06f, 0, 1);
    private Vector3 windowScale1 = new Vector3(0.06f, 0.09f, 1);

    [SerializeField]
    private float duration;

    private Transform window;
    /*
    IEnumerator Start()
    {
        Debug.Log("Started data panel");
        window = transform.Find("SF Window");
        window.localScale = windowScale0;
        yield return Lerp(transform, scale0, scale1, duration/2);
        yield return Lerp(window, windowScale0, windowScale1, duration/2);
    }
    */
    public IEnumerator Lerp(Transform t, Vector3 a, Vector3 b, float time) {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f) {
            i += Time.deltaTime * rate;
            t.localScale = Vector3.Lerp(a, b, smoother(i));
            yield return null;
        }
    }

    private float smoother(float x)
    {
        return 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
    }
}
