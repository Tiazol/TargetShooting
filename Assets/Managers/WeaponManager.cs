using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    public WeaponData CurrentWeapon { get; private set; }
    public event System.Action<WeaponData> WeaponChanged;

    [SerializeField] private List<WeaponData> weapons;

    private void Awake()
    {
        Instance = this;
        CurrentWeapon = weapons[0];
    }

    public void NextWeapon()
    {
        var index = weapons.IndexOf(CurrentWeapon);
        CurrentWeapon = (index < weapons.Count - 1) ? weapons[++index] : weapons[0];
        WeaponChanged?.Invoke(CurrentWeapon);
    }

    public void PreviousWeapon()
    {
        var index = weapons.IndexOf(CurrentWeapon);
        CurrentWeapon = (index != 0) ? weapons[--index] : weapons[weapons.Count - 1];
        WeaponChanged?.Invoke(CurrentWeapon);
    }
}
