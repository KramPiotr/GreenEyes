using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPanelLerper : MonoBehaviour
{
    [SerializeField]
    private Vector3 minScale;
    [SerializeField]
    private Vector3 maxScale;

    [SerializeField]
    private float duration;

    IEnumerator Start()
    {
        int cycles = 1;
        for (int i = 0; i < cycles; i++) {
            yield return Lerp(minScale, maxScale, duration);
        }
    }

    public IEnumerator Lerp(Vector3 a, Vector3 b, float time) {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f) {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
