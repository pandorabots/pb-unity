/*********************************************************************
 * The below code was created by Hourlab LLC for Pandorabots Inc.
 * This script sends Post Requests to Pandorabots API in order to 
 * communicate with a user defined Chatbot.
**********************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using TMPro;
using UnityEngine.Networking;

namespace Pandorabots.Events
{
    public class PostTextToPandorabots : MonoBehaviour
    {
        #region PB API Key Variables
        private const string URL = "https://api.pandorabots.com";

        [SerializeField]
        private string BOT_KEY;
        [SerializeField]
        private string APP_ID;
        [SerializeField]
        private string USER_Key;
        #endregion

        public TextMeshProUGUI pbResponseText;
        public static string pbResponse;
        public static string botResponse;

        [SerializeField]
        private StringEvent onPBResponse;
        
        public void PostRequest(string resp)
        {
            StartCoroutine(PostRequest(URL + "/talk?botkey=" + BOT_KEY + "&input=", resp));
        }

        IEnumerator PostRequest(string url, string json)
        {
            WWWForm form = new WWWForm();
            form.AddField("myField", "myData");

            using (UnityWebRequest www = UnityWebRequest.Post(url + json, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    pbResponseText.text = www.downloadHandler.text;
                }
                else
                {
                    pbResponse = www.downloadHandler.text;
                    
                    pbResponseText.text = pbResponse;
                    var jsonBotResponse = SimpleJSON.JSON.Parse(pbResponse);
                    if (jsonBotResponse != null)
                    {
                        botResponse = jsonBotResponse["responses"][0].ToString().Trim('"');
                        onPBResponse.Raise(botResponse);
                        www.Dispose();
                    }
                }
            }
            
        }
    }

}