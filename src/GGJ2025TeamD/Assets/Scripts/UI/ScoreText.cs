using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text? text;
    [SerializeField]
    private string scoreText = "Score: ";

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
        if (text != null)
        {
            text.text = scoreText + score.ToString();
        }
    }
}
