using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MarksAssets.LaunchURLWebGL;


public class HomeController : MonoBehaviour
{
    public GameObject Loader;
    public AudioClip MusicTrack;

    public GameObject returnToMainMenuPanel;
    public GameObject exitPanel;

    private void Awake()
    {
        if (MasterController.get == null)
        {
           GameObject Go = Instantiate(Resources.Load("MasterController", typeof(GameObject))) as GameObject;
        }
    }

    void Start()
    {
        InitMe();
    }

    public void LoadGame()
    {
        Loader.SetActive(true);
    }

    void InitMe()
    {
        //Loader.SetActive(false);
        if (MasterController.get.MusicClip == null) MasterController.get.AssignMusicClip(MusicTrack);
        MasterController.get.PlayMusic();
    }

    public void ShowHomeWarningMessage()
    {
        returnToMainMenuPanel.SetActive(true);
        returnToMainMenuPanel.GetComponent<Messager>().ShowMessage("Are you sure?\nDo you want to return to the home page?");
    }

    public void ShowExitWarningMessage()
    {
        exitPanel.SetActive(true);
        exitPanel.GetComponent<Messager>().ShowMessage("Are you sure?\nDo you want to exit?");
    }

    public void ExitGame()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            LaunchURLWebGL.instance.launchURLSelf("https://test.upsurgefi.com/games");
            //Application.OpenURL("https://test.upsurgefi.com/games");
        }
        else
        {
            Application.Quit();
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
