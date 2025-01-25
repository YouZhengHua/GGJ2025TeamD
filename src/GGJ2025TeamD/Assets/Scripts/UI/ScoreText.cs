using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text? scoreText;

    private void Start()
    {
        GlobalEvent.OnScoreChange += SetScore;
    }

    private void OnDestroy()
    {
        GlobalEvent.OnScoreChange -= SetScore;
    }

    private void SetScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
