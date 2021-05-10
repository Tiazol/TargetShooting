using UnityEngine;

public class CratesCounter : MonoBehaviour
{
    public int TotalCount { get; private set; }
    public int CurrentCount { get; private set; }

    public event System.Action<int> CurrentCountChanged;

    private void Awake()
    {
        var crates = GetComponentsInChildren<Crate>();
        TotalCount = crates.Length;
        foreach (var crate in crates)
        {
            crate.Destroying += () => CurrentCountChanged?.Invoke(++CurrentCount);
        }
    }
}
