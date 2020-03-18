using UnityEngine;
using UnityEngine.UI;

public class EventTester : MonoBehaviour
{
    public Text botTranscript;

    public void DisplayStringResponse(string stringResponse)
    {
        Debug.Log("String Response: " + stringResponse);
        botTranscript.text = stringResponse;
    }
}
