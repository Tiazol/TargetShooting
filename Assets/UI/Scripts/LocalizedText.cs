using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string stringID;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        LoadLocalization();
        LocalizationManager.Instance.LocalizationChanged += LoadLocalization;
    }

    private void LoadLocalization()
    {
        text.text = LocalizationManager.Instance.GetLocalizedString(stringID);
    }
}
