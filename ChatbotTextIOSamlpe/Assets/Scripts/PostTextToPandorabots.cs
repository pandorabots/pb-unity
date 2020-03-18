using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*********************************************************************
 * The below code was created by Hourlab LLC for Pandorabots Inc.
 * This script sends Post Requests to Pandorabots API in order to
 * communicate with a user defined Chatbot.
**********************************************************************/

using UnityEngine.Profiling;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;

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
        private string USER_KEY;

        #endregion

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
                }
                else
                {
                    pbResponse = www.downloadHandler.text;
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