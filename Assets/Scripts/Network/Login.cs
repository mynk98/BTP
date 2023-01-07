using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

namespace Assets.Scripts.Network
{
    public class Login : MonoBehaviour
    {
        public GameObject emailInputField;

        private void Awake()
        {
            /*if (NetworkSingleton.Instance.CheckNetworkConnection())
            {
                StartCoroutine(GetXP());
            }*/
            //Make sure to comment this before building...
            PlayerPrefs.DeleteAll();
        }

        private void Start()
        {
            if (NetworkSingleton.Instance.CheckNetworkConnection())
            {
                if (PlayerPrefs.GetString("player_email", "") != "")
                {
                    print(PlayerPrefs.GetString("player_email", ""));
                    SceneManager.LoadScene("Game Scene", LoadSceneMode.Single);
                }
            }
        }

        public void setEmail()
        {
            string text = emailInputField.GetComponent<TMP_InputField>().text;
            PlayerPrefs.SetString("player_email", text);
        }

        public void onLogin()
        {
            StartCoroutine(onLoginToServer());
        }

        IEnumerator onLoginToServer()
        {
            UnityWebRequest www = UnityWebRequest.Get(AvailableRoutes.checkUser + emailInputField.GetComponent<TMP_InputField>().text);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // CustomNotificationManager.Instance.AddNotification(2, "Login Error");
                print("Login Error");
            }
            else
            {

                // JSONNode data = JSON.Parse(www.downloadHandler.text
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject(www.downloadHandler.text);
                print(data);
                //System.Reflection.PropertyInfo pi = data.GetType().GetProperty("avatar");
                //PlayerPrefs.SetInt("player_avatar", (int)pi.GetValue(data, null));
                //Debug.Log("player_xp: " + data["xp"]);
                setEmail();
                SceneManager.LoadScene("Game Scene", LoadSceneMode.Single);

                // LoadingManager.instance.LoadGame(SceneIndexes.Login, SceneIndexes.AvatarSelection);
            }
        }

        public void onGoogleSignup()
        {
            Application.OpenURL(AvailableRoutes.googleSignup);
        }

    }
}