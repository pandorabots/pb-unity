/*********************************************************************
 * The below code was created by Hourlab LLC for Pandorabots Inc.
 * This script offers an easy way to interface with Google Cloud Text 
 * to Speech API.
**********************************************************************/

using Pandorabots.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleCloudTTS : MonoBehaviour
{
    public string GC_API_KEY;
    private string url;
    private string postData;

    [SerializeField]
    private StringEvent onAudioContentReceived;

    public void GCRequestTTS(string inputText)
    {
        postData = "{" +
                   "\"input\":{\"text\":\"" + inputText + "\"}," +
                   "\"voice\":{\"languageCode\":\"en-gb\",\"name\":\"en-GB-Standard-A\", \"ssmlGender\":\"FEMALE\"}," +
                   "\"audioConfig\":{\"audioEncoding\":\"MP3\"}" +
                   "}";
        url = "https://texttospeech.googleapis.com/v1/text:synthesize?key=" + GC_API_KEY;
        StartCoroutine(PostDataToGC());
    }

    UnityWebRequest GCUnityWebRequest(string url, string jsonParam)
    {
        UnityWebRequest gcWebRequest = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonParam);
        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(bytes);
        gcWebRequest.uploadHandler = uploadHandlerRaw;
        gcWebRequest.SetRequestHeader("Content-Type", "application/json");
        gcWebRequest.downloadHandler = new DownloadHandlerBuffer();
        return gcWebRequest;
    }

    IEnumerator PostDataToGC()
    {
        using (UnityWebRequest www = GCUnityWebRequest(url, postData))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                onAudioContentReceived.Raise(www.downloadHandler.text);
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
