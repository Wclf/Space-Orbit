using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsInitialized { get; set; }
    public int CurrentScore { get; set; }

    private string highScoreKey = "HighScore";

    public int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt(highScoreKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(highScoreKey, value);
        }
    }

    private void Init()
    {
        IsInitialized = false;
        CurrentScore = 0;
    }

    private const string MainMenu = "MainMenu";
    private const string GamePlay = "GamePlay";

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void GoToGamePlay()
    {
        SceneManager.LoadScene(GamePlay);
    }
}
