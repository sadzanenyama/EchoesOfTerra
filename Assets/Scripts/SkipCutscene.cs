using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour
{
    public string SceneToLoad;
    public float loadAfter;

    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadNext());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            StopAllCoroutines();
            SceneManager.LoadScene(SceneToLoad);
        }
    }

    IEnumerator LoadNext ()
    {
        yield return new WaitForSeconds(loadAfter);

        SceneManager.LoadScene(SceneToLoad);
    }
}
