/*********************************************************************
 * The below code was created by Hourlab LLC for Pandorabots Inc.
 * This script plays a previously recorded or converted audio content.
**********************************************************************/

using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class PlayBotAudioFile : MonoBehaviour
{
    private AudioSource audioSource;
    public static string audioFilePath;
    public static bool isAudioFileWritten = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBotAudio(string filePath)
    {
        // Check if file exists before starting coroutine
        if (File.Exists(filePath))
        {
            StartCoroutine(GetAudioClip(filePath));
        }
        
        else
        {
            Debug.Log("File path (" + filePath + ") doesn't exist.");
        }
        
    }

    IEnumerator GetAudioClip(string path)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                audioClip.loadInBackground.Equals(true);
                //audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = audioClip;
                audioSource.Play();
                File.Delete(path);
            }
        }
    }

    // If you want to stream bytes instead
    public void AudioStream(byte[] bytes)
    {
        audioSource.clip = NAudioPlayer.FromMp3Data(bytes);
        audioSource.Play();
    }

}

