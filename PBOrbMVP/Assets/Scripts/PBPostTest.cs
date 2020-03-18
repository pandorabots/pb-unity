// Simple script for developer testing

using UnityEngine;
using TMPro;
using Pandorabots.Events;

public class PBPostTest : MonoBehaviour
{
    public TextMeshProUGUI testText;
    public GameObject pbPost;
    
    public void SendPostToPandorabots()
    {
        pbPost.GetComponent<PostTextToPandorabots>().PostRequest(testText.text);
    }
}
