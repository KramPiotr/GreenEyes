using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeCursor : MonoBehaviour
{
    /// <summary>
    /// The cursor (this object) mesh renderer
    /// </summary>
    private MeshRenderer meshRenderer;

    public static GameObject FocusedButton;

    /// <summary>
    /// Runs at initialization right after the Awake method
    /// </summary>
    void Start()
    {
        // Grab the mesh renderer that is on the same object as this script.
        meshRenderer = gameObject.GetComponent<MeshRenderer>();

        // Set the cursor reference
        SceneOrganiser.Instance.cursor = gameObject;
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        // If you wish to change the size of the cursor you can do so here
        gameObject.transform.localScale = new Vector3(0.003f, 0.003f, 0.003f);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    /// 

    void outOfFocus(GameObject FocusedObject)
    {
            FocusedButton.GetComponent<Image>().color = Color.white;
    }

    void Update()
    {
        if(GazeCursor.FocusedButton != null || ControlInfoButton.infoPanel != null)
        {
            gameObject.transform.localScale = new Vector3(0.003f, 0.003f, 0.003f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
        // Do a raycast into the world based on the user's head position and orientation.
        Vector3 headPosition = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;

        RaycastHit gazeHitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out gazeHitInfo, 30.0f, SpatialMapping.PhysicsRaycastMask))
        {
            // If the raycast hit a hologram, display the cursor mesh.
            meshRenderer.enabled = true;
            // Move the cursor to the point where the raycast hit.
            transform.position = gazeHitInfo.point;
            // Rotate the cursor to hug the surface of the hologram.
            transform.rotation = Quaternion.FromToRotation(Vector3.up, gazeHitInfo.normal);

        }
        else
        {
            // If the raycast did not hit a hologram, hide the cursor mesh.
            meshRenderer.enabled = false;
        }

        if(Physics.Raycast(headPosition, gazeDirection, out gazeHitInfo, 30.0f, (1<<8)))
        {
            if(FocusedButton != null)
            {
                outOfFocus(FocusedButton);
            }
            FocusedButton = gazeHitInfo.collider.gameObject;
            if (FocusedButton.name.Equals("DeleteButton"))
            {
                FocusedButton.GetComponent<Image>().color = Color.red;
            }
            else
            {
                FocusedButton.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
            }
        }
        else
        {
            if(FocusedButton != null)
            {
                outOfFocus(FocusedButton);
                FocusedButton = null;
            }
        }
    }
}
