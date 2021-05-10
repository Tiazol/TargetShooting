using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private AudioSource shootingAudioSource;
    private bool canMakeNextShot = true;

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

        GameObject bulletPrefab = WeaponManager.Instance.CurrentWeapon.BulletPrefab;
        GameObject bullet = Instantiate(bulletPrefab, weaponPoint.position, weaponPoint.rotation, weaponPoint);
        float speed = WeaponManager.Instance.CurrentWeapon.ShootingSpeed;
        bullet.GetComponent<Rigidbody2D>().AddForce(weaponPoint.right * speed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(WeaponManager.Instance.CurrentWeapon.ShootingDelay);
        canMakeNextShot = true;
    }

    private IEnumerator ShootPellets()
    {
        canMakeNextShot = false;
        shootingAudioSource.PlayOneShot(WeaponManager.Instance.CurrentWeapon.AudioClip);

        GameObject bulletPrefab = WeaponManager.Instance.CurrentWeapon.BulletPrefab;
        GameObject bullet1 = Instantiate(bulletPrefab, weaponPoint.position, weaponPoint.rotation * Quaternion.Euler(0, 0, -30), weaponPoint);
        GameObject bullet2 = Instantiate(bulletPrefab, weaponPoint.position, weaponPoint.rotation * Quaternion.Euler(0, 0, -10), weaponPoint);
        GameObject bullet3 = Instantiate(bulletPrefab, weaponPoint.position, weaponPoint.rotation * Quaternion.Euler(0, 0, 10), weaponPoint);
        GameObject bullet4 = Instantiate(bulletPrefab, weaponPoint.position, weaponPoint.rotation * Quaternion.Euler(0, 0, 30), weaponPoint);

        float speed = WeaponManager.Instance.CurrentWeapon.ShootingSpeed;
        bullet1.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0, 0, -30) * weaponPoint.right * speed, ForceMode2D.Impulse);
        bullet2.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0, 0, -10) * weaponPoint.right * speed, ForceMode2D.Impulse);
        bullet3.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0, 0, 10) * weaponPoint.right * speed, ForceMode2D.Impulse);
        bullet4.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0, 0, 30) * weaponPoint.right * speed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(WeaponManager.Instance.CurrentWeapon.ShootingDelay);
        canMakeNextShot = true;
    }
}
