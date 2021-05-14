using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform BulletSpawnPoint => weaponPoint;

    [SerializeField] private Transform weaponPoint;
    private AudioSource shootingAudioSource;
    private BulletsHolder bulletsHolder;

    private bool canMakeNextShot = true;

    private void Awake()
    {
        shootingAudioSource = GetComponent<AudioSource>();
        bulletsHolder = GetComponent<BulletsHolder>();
    }

    private void Update()
    {
        if (!PauseManager.IsPaused)
        {
            CheckInput(); 
        }
    }

    private void CheckInput()
    {
        if (canMakeNextShot)
        {
            if (WeaponManager.Instance.CurrentWeapon.Automatic)
            {
                if (Input.GetMouseButton(0))
                {
                    StartCoroutine(ShootSingle());
                }
            }
            else if (WeaponManager.Instance.CurrentWeapon.HasPellets)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(ShootPellets());
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(ShootSingle());
                }
            }
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            WeaponManager.Instance.NextWeapon();
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            WeaponManager.Instance.PreviousWeapon();
        }
    }

    private IEnumerator ShootSingle()
    {
        canMakeNextShot = false;
        shootingAudioSource.PlayOneShot(WeaponManager.Instance.CurrentWeapon.AudioClip);

        var bullet = bulletsHolder.GetInstantiatedBullet();
        bullet.Run(weaponPoint.right);

        yield return new WaitForSeconds(WeaponManager.Instance.CurrentWeapon.ShootingDelay);
        canMakeNextShot = true;
    }

    private IEnumerator ShootPellets()
    {
        canMakeNextShot = false;
        shootingAudioSource.PlayOneShot(WeaponManager.Instance.CurrentWeapon.AudioClip);

        var bullet1 = bulletsHolder.GetInstantiatedBullet();
        var bullet2 = bulletsHolder.GetInstantiatedBullet();
        var bullet3 = bulletsHolder.GetInstantiatedBullet();
        var bullet4 = bulletsHolder.GetInstantiatedBullet();

        bullet1.transform.rotation = weaponPoint.rotation * Quaternion.Euler(0, 0, -30);
        bullet2.transform.rotation = weaponPoint.rotation * Quaternion.Euler(0, 0, -10);
        bullet3.transform.rotation = weaponPoint.rotation * Quaternion.Euler(0, 0, 10);
        bullet4.transform.rotation = weaponPoint.rotation * Quaternion.Euler(0, 0, 30);

        bullet1.Run(Quaternion.Euler(0, 0, -30) * weaponPoint.right);
        bullet2.Run(Quaternion.Euler(0, 0, -10) * weaponPoint.right);
        bullet3.Run(Quaternion.Euler(0, 0, 10) * weaponPoint.right);
        bullet4.Run(Quaternion.Euler(0, 0, 30) * weaponPoint.right);

        yield return new WaitForSeconds(WeaponManager.Instance.CurrentWeapon.ShootingDelay);
        canMakeNextShot = true;
    }
}
