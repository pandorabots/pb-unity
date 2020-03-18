/*********************************************************************
Simple test script to confirm communication with Pandorabots API
*********************************************************************/

using UnityEngine;
using Pandorabots.Events;
using UnityEngine.UI;
using System.Linq;

public class PBPostTest : MonoBehaviour
{
    public Text testText;
    public GameObject pbPost;
    public Button sendButton;
    

    public void SendPostToPandorabots()
    {
        // Quick correction for questions asked
        // A more indepth check/correction should be implemented if your code relies on text input
        string pbText = testText.text;
        if (pbText.Contains("?"))
        {
            pbText = pbText.Replace("?", ".");
            Debug.Log(pbText);
        }
        pbPost.GetComponent<PostTextToPandorabots>().PostRequest(pbText);
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            sendButton.onClick.Invoke();
        }
    }
}
