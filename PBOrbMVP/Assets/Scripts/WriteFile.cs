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
    private Thread writeThread;

    private List<Action> unityFunctionsToRunInMainThread;

    private void Start()
    {
        filePath = Application.persistentDataPath;
        unityFunctionsToRunInMainThread = new List<Action>();
    }

    private void Update()
    {
        while (unityFunctionsToRunInMainThread.Count > 0)
        {
            Action myFunction = unityFunctionsToRunInMainThread[0];
            unityFunctionsToRunInMainThread.RemoveAt(0);
            myFunction();
        }
    }

    public void WriteAudioContentToFile(string path)
    {
        StartThreadedFunction(() => { WriteAudioFile(path); });
    }

    public void StartThreadedFunction(Action threadedFunction)
    {
        Thread t = new Thread(new ThreadStart(threadedFunction));
        t.Start();
    }

    public void WriteTxtFile(string txt)
    {
        string txtFilePath = Path.Combine("GCResponse/", "GCAudioContentText.txt");
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
        
        Action unitySpecificFunction = () =>
        {
            Debug.Log("Audio File Saved: " + mp3FilePath);
            onAudioFileWritten.Raise(mp3FilePath);
        };
        QueueUnityMainThreadFunction(unitySpecificFunction);

    }

    public void QueueUnityMainThreadFunction(Action unityFunction)
    {
        Debug.Log("Queue unity specific function");
        unityFunctionsToRunInMainThread.Add(unityFunction);
    }
}
