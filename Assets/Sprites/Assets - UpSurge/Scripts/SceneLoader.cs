using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public int WaitBeforeLoading = 3;
    public int SceneIndex = 0;
    public bool LoadAtStart;
    public bool LoadOnEnable;
    public TMP_Text LoaderText;
    public RectTransform ProgressBar;

    Vector2 ProgressSize;

    void Start()
    {
        if (LoadAtStart && LoadOnEnable)
        {
            LoadAtStart = false;
            LoadOnEnable = false;
        }

        if (ProgressBar != null)
        {
            ProgressSize = ProgressBar.sizeDelta;
            ProgressBar.sizeDelta = new Vector2(0, ProgressSize.y);
        }

        if(LoadAtStart) StartCoroutine(ILoadScene());
    }

    private void OnEnable()
    {
        if (LoadOnEnable) StartCoroutine(ILoadScene());
    }

    public void LoadScene()
    {
        StartCoroutine(ILoadScene());
    }

    IEnumerator ILoadScene()
    {
        yield return new WaitForSeconds(WaitBeforeLoading);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneIndex);

        while (!asyncOperation.isDone)
        {
            if(LoaderText!=null) LoaderText.text = "Loading Game ... " + (asyncOperation.progress * 100) + "%";
            if (ProgressBar != null) ProgressBar.sizeDelta = new Vector2(asyncOperation.progress * ProgressSize.x, ProgressSize.y);
            yield return null;
        }
    }
}