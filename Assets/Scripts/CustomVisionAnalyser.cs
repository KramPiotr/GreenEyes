using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class CustomVisionAnalyser : MonoBehaviour
{
    /// <summary>
    /// Unique instance of this class
    /// </summary>
    public static CustomVisionAnalyser Instance;

    /// <summary>
    /// Insert your prediction key here
    /// </summary>
    private string predictionKey = "552c3dd08bb54734b8fdb676cbc4ed59";

    /// <summary>
    /// Insert your prediction endpoint here
    /// </summary>
    /// Oh nice!
    private string predictionEndpoint = "https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/f88eff33-dc95-49e5-a06e-a6d27ac38909/detect/iterations/Iteration2/image";

    /// <summary>
    /// Bite array of the image to submit for analysis
    /// </summary>
    [HideInInspector] public byte[] imageBytes;

    /// <summary>
    /// Initializes this class
    /// </summary>
    private void Awake()
    {
        // Allows this instance to behave like a singleton
        Instance = this;
    }

    /// <summary>
    /// Call the Computer Vision Service to submit the image.
    /// </summary>
    public IEnumerator AnalyseLastImageCaptured(string imagePath)
    {
        Debug.Log("Analyzing...");

        WWWForm webForm = new WWWForm();

        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(predictionEndpoint, webForm))
        {
            // Gets a byte array out of the saved image
            imageBytes = GetImageAsByteArray(imagePath);

            unityWebRequest.SetRequestHeader("Content-Type", "application/octet-stream");
            unityWebRequest.SetRequestHeader("Prediction-Key", predictionKey);

            // The upload handler will help uploading the byte array with the request
            unityWebRequest.uploadHandler = new UploadHandlerRaw(imageBytes);
            unityWebRequest.uploadHandler.contentType = "application/octet-stream";

            // The download handler will help receiving the analysis from Azure
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            // Send the request
            yield return unityWebRequest.SendWebRequest();

            string jsonResponse = unityWebRequest.downloadHandler.text;

            Debug.Log("response: " + jsonResponse);

            // Create a texture. Texture size does not matter, since
            // LoadImage will replace with the incoming image size.
            Texture2D tex = new Texture2D(1, 1);
            tex.LoadImage(imageBytes);
            SceneOrganiser.Instance.quadRenderer.material.SetTexture("_MainTex", tex);

            // The response will be in JSON format, therefore it needs to be deserialized
            AnalysisRootObject analysisRootObject = new AnalysisRootObject();
            analysisRootObject = JsonConvert.DeserializeObject<AnalysisRootObject>(jsonResponse);

            SceneOrganiser.Instance.FinaliseLabel(analysisRootObject);
            for(int i = 0; i<analysisRootObject.predictions.Count; i++)
            {
                Debug.Log("Prediction " + (i + 1) + " in this frame: " + analysisRootObject.predictions[i].probability 
                        + " for object " + analysisRootObject.predictions[i].tagName + " in the bounding box of data "
                        + " left: " + analysisRootObject.predictions[i].boundingBox.left
                        + " top: " + analysisRootObject.predictions[i].boundingBox.top
                        + " width: " + analysisRootObject.predictions[i].boundingBox.width
                        + " height: " + analysisRootObject.predictions[i].boundingBox.height);
            }
            
        }
    }

    /// <summary>
    /// Returns the contents of the specified image file as a byte array.
    /// </summary>
    static byte[] GetImageAsByteArray(string imageFilePath)
    {
        FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);

        BinaryReader binaryReader = new BinaryReader(fileStream);

        return binaryReader.ReadBytes((int)fileStream.Length);
    }
}
