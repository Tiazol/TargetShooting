using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text PointsValueText;
    [SerializeField] private Text CratesValueText;
    [SerializeField] private Image WeaponImage;
    [SerializeField] private GameObject PauseMenuPanel;
    [SerializeField] private CratesCounter cratesCounter;
    [SerializeField] private Dropdown LocalizationDropdown;

    private void Start()
    {
        ScoreManager.Instance.ScoreChanged += OnScoreChanged;

        WeaponManager.Instance.WeaponChanged += OnWeaponChanged;
        WeaponImage.sprite = WeaponManager.Instance.CurrentWeapon.Sprite;

        if (cratesCounter == null)
        {
            Debug.LogWarning($"Warning: {typeof(CratesCounter)} not found", this);
        }
        else
        {
            cratesCounter.CurrentCountChanged += OnCratesCountChanged;
            CratesValueText.text = $"0/{cratesCounter.TotalCount}";
        }

        PauseManager.Paused += OnPaused;
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
        CratesValueText.text = $"{currentCount}/{cratesCounter.TotalCount}";
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
