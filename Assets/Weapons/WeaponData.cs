using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] string weaponName;
    [SerializeField] WeaponType weaponType;
    [SerializeField] Sprite weaponSprite;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootingSpeed;
    [SerializeField] float shootingDamage;
    [SerializeField] float shootingDelay;
    [SerializeField] bool automatic;
    [SerializeField] bool hasPellets;
    [SerializeField] AudioClip shotAudioClip;

    public string Name => weaponName;
    public WeaponType Type => weaponType;
    public Sprite Sprite => weaponSprite;
    public GameObject BulletPrefab => bulletPrefab;
    public float ShootingSpeed => shootingSpeed;
    public float ShootingDamage => ShootingDamage;
    public bool Automatic => automatic;
    public float ShootingDelay => shootingDelay;
    public bool HasPellets => hasPellets;
    public AudioClip AudioClip => shotAudioClip;
}
    