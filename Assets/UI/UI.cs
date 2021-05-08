using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text PointsValueText;
    [SerializeField] private Text CratesValueText;
    [SerializeField] private Image WeaponImage;
    [SerializeField] private GameObject PauseMenuPanel;

    [SerializeField] private Dropdown LocalizationDropdown;

    private void Start()
    {
        ScoreManager.Instance.ScoreChanged += OnScoreChanged;

        WeaponManager.Instance.WeaponChanged += OnWeaponChanged;
        WeaponImage.sprite = WeaponManager.Instance.CurrentWeapon.Sprite;

        CratesManager.Instance.CurrentCountChanged += OnCratesCountChanged;
        CratesValueText.text = $"0/{CratesManager.Instance.TotalCount}";

        PauseManager.Instance.Paused += OnPaused;
        PauseMenuPanel.SetActive(false);

        LocalizationDropdown.onValueChanged.AddListener(value => OnLocalizationDropdownValueChanged(value));
    }

    private void OnScoreChanged(int score)
    {
        PointsValueText.text = $"{score}";
    }

    private void OnWeaponChanged(WeaponData weapon)
    {
        WeaponImage.sprite = weapon.Sprite;
    }

    private void OnCratesCountChanged(int currentCount)
    {
        CratesValueText.text = $"{currentCount}/{CratesManager.Instance.TotalCount}";
    }

    private void OnPaused(bool paused)
    {
        PauseMenuPanel.SetActive(paused);
    }

    private void OnLocalizationDropdownValueChanged(int value)
    {
        var language = LocalizationDropdown.options[value].text;
        LocalizationManager.Instance.SetLocalization(language);
    }
}
