using UnityEngine;

public class CratesManager : MonoBehaviour
{
    public static CratesManager Instance { get; private set; }

    public int TotalCount { get; private set; }
    public int CurrentCount { get; private set; }

    public event System.Action<int> CurrentCountChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var crates = GetComponentsInChildren<Crate>();
        TotalCount = crates.Length;
        foreach(var crate in crates)
        {
            crate.Destroying += () =>
            {
                CurrentCount++;
                CurrentCountChanged?.Invoke(CurrentCount);
            };
        }
    }
}
