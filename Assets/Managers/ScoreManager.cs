using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int Score
    {
        get => score;
        private set
        {
            if (score < int.MaxValue)
            {
                score = value;
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
        ScoreChanged?.Invoke(Score);
    }
}
