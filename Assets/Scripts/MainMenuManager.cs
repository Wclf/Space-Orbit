using UnityEngine;
using TMPro;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private TMP_Text _newBestText;

    private void Awake()
    {
        if(GameManager.instance.IsInitialized)
        {
            StartCoroutine(ShowScore());
        }
        else
        {
            _newBestText.gameObject.SetActive(false);
            _scoreText.gameObject.SetActive(false);
            _bestScoreText.text = GameManager.instance.HighScore.ToString();
        }
    }

    [SerializeField] private float _animationTime;
    [SerializeField] private AnimationCurve _speedCurve;
    
    private IEnumerator ShowScore()
    {
        int tempScore = 0;
        _scoreText.text = tempScore.ToString();

        int currentScore = GameManager.instance.CurrentScore;
        int highScore = GameManager.instance.HighScore;

        if (currentScore > highScore)
        {
            _newBestText.gameObject.SetActive(true);
            GameManager.instance.HighScore = currentScore;
        }
        else
        {
            _newBestText.gameObject.SetActive(false);
        }
        _bestScoreText.text = GameManager.instance.HighScore.ToString();

        float speed = 1 / _animationTime;
        float timeElasped = 0;
        while (timeElasped < 1f)
        {
            timeElasped += speed * Time.deltaTime;
            tempScore = (int)(_speedCurve.Evaluate(timeElasped) * currentScore);
            _scoreText.text = tempScore.ToString();
            yield return null;
        }
        tempScore = currentScore;
        _scoreText.text = tempScore.ToString();
    }

    [SerializeField] private AudioClip _clickClip;
    public void ClickedPlay()
    {
        SoungManager.instance.PlaySound(_clickClip);
        GameManager.instance.GoToGamePlay();
    }

}
