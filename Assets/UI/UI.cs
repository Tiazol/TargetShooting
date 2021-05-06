using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text PointsText;
    [SerializeField] private Image WeaponImage;

    private void Start()
    {
        ScoreManager.Instance.ScoreChanged += OnScoreChanged;
        WeaponManager.Instance.WeaponChanged += OnWeaponChanged;
        WeaponImage.sprite = WeaponManager.Instance.CurrentWeapon.Sprite;
    }

    private void OnScoreChanged(int score)
    {
        PointsText.text = $"Points: {score}";
    }

    private void OnWeaponChanged(WeaponData weapon)
    {
        WeaponImage.sprite = weapon.Sprite;
    }
}
