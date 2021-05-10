using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text pointsValueText;
    [SerializeField] private Text cratesValueText;
    [SerializeField] private Image weaponImage;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private CratesCounter cratesCounter;
    [SerializeField] private Dropdown localizationDropdown;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        ScoreManager.Instance.ScoreChanged += OnScoreChanged;

        WeaponManager.Instance.WeaponChanged += OnWeaponChanged;
        weaponImage.sprite = WeaponManager.Instance.CurrentWeapon.Sprite;

        if (cratesCounter == null)
        {
            Debug.LogWarning($"Warning: {typeof(CratesCounter)} not found", this);
        }
        else
        {
            cratesCounter.CurrentCountChanged += OnCratesCountChanged;
            cratesValueText.text = $"0/{cratesCounter.TotalCount}";
        }

        PauseManager.Paused += OnPaused;
        pauseMenuPanel.SetActive(false);

        localizationDropdown.onValueChanged.AddListener(OnLocalizationDropdownValueChanged);

        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    private void OnScoreChanged(int score)
    {
        pointsValueText.text = $"{score}";
    }

    private void OnWeaponChanged(WeaponData weapon)
    {
        weaponImage.sprite = weapon.Sprite;
    }

    private void OnCratesCountChanged(int currentCount)
    {
        cratesValueText.text = $"{currentCount}/{cratesCounter.TotalCount}";
    }

    private void OnPaused(bool paused)
    {
        pauseMenuPanel.SetActive(paused);
    }

    private void OnLocalizationDropdownValueChanged(int value)
    {
        var language = localizationDropdown.options[value].text;
        LocalizationManager.Instance.SetLocalization(language);
    }

    private void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
