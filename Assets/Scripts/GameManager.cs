using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
   
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();  
        Debug.Log("PlayerPrefs have been deleted on application quit.");
    }
}
