/*********************************************************************
 * The multithreading strategy within this code has been derived from
 * Quill18 Productions multithreading approach within Unity.
 *
 * Excellent tutorial for multithreading in Unity:
 * https://www.youtube.com/watch?v=ja63QO1Imck
 *
 * All other code created by Hourlab LLC
**********************************************************************/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Pandorabots.Events;
using System;
using System.Threading;

public class WriteFile : MonoBehaviour
{
    [SerializeField]
    private StringEvent onAudioFileWritten;

    private string filePath;
    private string mp3FilePath;

    private List<Action> functionsToRunInMainThread;

    public GameObject playByteStream;

    private void Start()
    {
        filePath = Application.persistentDataPath;
        functionsToRunInMainThread = new List<Action>();
    }

    private void Update()
    {
        while (functionsToRunInMainThread.Count > 0)
        {
            Action myFunction = functionsToRunInMainThread[0];
            functionsToRunInMainThread.RemoveAt(0);
            myFunction();
        }
    }

    public void WriteAudioContentToFile(string path)
    {
        StartThreadedFunction(() => { WriteAudioFile(path); });
    }

    public void StartThreadedFunction(Action someFunctionWithoutParams)
    {
        Thread t = new Thread(new ThreadStart(someFunctionWithoutParams));
        t.Start();
    }

    public void WriteTxtFile(string txt)
    {
        string txtFilePath = Path.Combine("GCResponse/", "GCAudioContentResponse.txt");
        txtFilePath = Path.Combine(Application.persistentDataPath, txtFilePath);
        Directory.CreateDirectory(Path.GetDirectoryName(txtFilePath));
        File.WriteAllText(txtFilePath, txt);
    }

    public void WriteAudioFile(string txt)
    {

        mp3FilePath = Path.Combine("GCResponse/", "GCAudio.mp3");
        mp3FilePath = Path.Combine(filePath, mp3FilePath);
        Directory.CreateDirectory(Path.GetDirectoryName(mp3FilePath));

        if (txt != null || txt != String.Empty)
        {
            var jsonResponse = SimpleJSON.JSON.Parse(txt);
            //Debug.Log("jsonResponse: " + jsonResponse);
            string jsonParse = jsonResponse["audioContent"].ToString().Trim('"');

            byte[] binaryData = Convert.FromBase64String(jsonParse);
            File.WriteAllBytes(mp3FilePath, binaryData);
        }

        else
        {
            Debug.Log("String is null or empty");
        }

        Action unitySpecificFunction = () =>
        {
            Debug.Log("Audio File Saved: " + mp3FilePath);
            onAudioFileWritten.Raise(mp3FilePath);
        };
        QueueMainThreadFunction(unitySpecificFunction);

    }

    public void QueueMainThreadFunction(Action someFunction)
    {
        Debug.Log("QueueMainThread");
        functionsToRunInMainThread.Add(someFunction);
    }

    public void ConvertAudioContentToBytes(string audContent)
    {
        if (audContent != null || audContent != String.Empty)
        {
            var jsonResponse = SimpleJSON.JSON.Parse(audContent);
            //Debug.Log("jsonResponse: " + jsonResponse);
            string jsonParse = jsonResponse["audioContent"].ToString().Trim('"');

            byte[] binaryData = Convert.FromBase64String(jsonParse);
            PlayBotAudioFile playAud = playByteStream.GetComponent<PlayBotAudioFile>();
            playAud.AudioStream(binaryData);
        }
        else
        {
            Debug.Log("String is null or empty");
        }
        
    }
}
