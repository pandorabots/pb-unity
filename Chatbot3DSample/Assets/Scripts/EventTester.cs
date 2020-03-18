using UnityEngine;
using TMPro;

public class EventTester : MonoBehaviour
{
    public TextMeshProUGUI botTranscript;

    public void DisplaySpaceCount(int spaceCount)
    {
        Debug.Log("Space button has been pressed " + spaceCount.ToString() + " times!");
    }

    public void DisplaySpaceBool(bool spaceBool)
    {
        Debug.Log("Space button has been pressed :" + spaceBool.ToString());
    }

    public void DisplayStringResponse(string stringResponse)
    {
        Debug.Log("String Response: " + stringResponse);
        botTranscript.text = stringResponse;
    }
}
