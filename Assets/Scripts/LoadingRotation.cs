using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingRotation : MonoBehaviour
{
    [SerializeField]
    private float period;

    private Transform rotate;
    private Transform question;
    private TextMeshProUGUI questionMesh;

    private bool done = false;

    void Awake()
    {
        rotate = transform.Find("Rotate");
        question = transform.Find("Question");
        Transform questionLbl = question.Find("QuestionLbl");
        questionMesh = questionLbl.GetComponent<TextMeshProUGUI>();
        questionMesh.text = "";
    }

    //IEnumerator Start()
    //{
        //while (true) {
            //yield return Lerp(new Quaternion(0, 0, 0, 1), new Quaternion(0, 0, 360, 1), period);
        //}
    //}

    void Update()
    {
        if (!done)
        {
            rotate.Rotate(0, 0, 6.0f * 60.0f * Time.deltaTime / period);
        }
    }

    public void Failed()
    {
        done = true;
        rotate.localScale = new Vector3(0,0,0);
        questionMesh.text = "?";
        ///transform.Rotate(0, 0, 0);
    }
}
