using UnityEngine;
using TMPro;

public class EventTester : MonoBehaviour
{
    public TextMeshProUGUI botTranscript;

    public void DisplayStringResponse(string stringResponse)
    {
        Debug.Log("String Response: " + stringResponse);
        botTranscript.text = stringResponse;
    }
}
