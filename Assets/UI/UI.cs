using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text PointsText;
    [SerializeField] private Text CratesText;
    [SerializeField] private Image WeaponImage;

    private void Start()
    {
        ScoreManager.Instance.ScoreChanged += OnScoreChanged;
        WeaponManager.Instance.WeaponChanged += OnWeaponChanged;
        WeaponImage.sprite = WeaponManager.Instance.CurrentWeapon.Sprite;
        CratesManager.Instance.CurrentCountChanged += OnCratesCountChanged;
        CratesText.text = $"Crates: 0/{CratesManager.Instance.TotalCount}";
    }


    private void OnScoreChanged(int score)
    {
        PointsText.text = $"Points: {score}";
    }

    private void OnWeaponChanged(WeaponData weapon)
    {
        WeaponImage.sprite = weapon.Sprite;
    }

    private void OnCratesCountChanged(int currentCount)
    {
        CratesText.text = $"Crates: {currentCount}/{CratesManager.Instance.TotalCount}";
    }
}
