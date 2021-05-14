using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxValue = 100f;
    private float currentValue;
    private IDestroyable destroyable;

    public float CurrentValue
    {
        get => currentValue;
        set
        {
            if (currentValue != value)
            {
                CurrentValueChanged?.Invoke(currentValue);

                if (value == 0)
                {
                    Destroy();
                }

                if (value > 0 && value <= maxValue)
                {
                    currentValue = value;
                }
            }
        }
    }

    public event System.Action<float> CurrentValueChanged;

    private void Awake()
    {
        currentValue = maxValue;
        destroyable = GetComponent<IDestroyable>();
    }

    private void Destroy()
    {
        if (destroyable != null)
        {
            destroyable.DestroySelf();
        }
    }
}
