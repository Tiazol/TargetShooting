using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int pointsLimit = 1000;

    public static ScoreManager Instance { get; private set; }
    public int Score
    {
        get => score;
        private set
        {
            if (score != value)
            {
                if (value >= 0 && value <= pointsLimit)
                {
                    score = value;
                    ScoreChanged?.Invoke(Score);
                }
            }
        }
    }

    public event System.Action<int> ScoreChanged;

    private int score;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseScore()
    {
        Score++;
    }

    public void IncreaseScoreBy(int points)
    {
        Score += points;
    }
}
