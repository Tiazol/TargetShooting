using System.Collections.Generic;

using UnityEngine;

public class BulletsHolder : MonoBehaviour
{
    private readonly int bulletsCount = 20;
    private Stack<Bullet> bullets;
    private Transform bulletSpawnPoint;

    private void Start()
    {
        bulletSpawnPoint = GetComponent<Shooting>().BulletSpawnPoint;

        bullets = new Stack<Bullet>(bulletsCount);
        for (int i = 0; i < bulletsCount; i++)
        {
            var bullet = WeaponManager.Instance.CurrentWeapon.BulletPrefab.GetComponent<Bullet>();
            var instantiatedBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation, bulletSpawnPoint);
            instantiatedBullet.Speed = WeaponManager.Instance.CurrentWeapon.ShootingSpeed;
            instantiatedBullet.Damage = WeaponManager.Instance.CurrentWeapon.ShootingDamage;
            instantiatedBullet.Holder = this;
            instantiatedBullet.gameObject.SetActive(false);
            bullets.Push(instantiatedBullet);
        }
    }

    public Bullet GetInstantiatedBullet()
    {
        var bullet = bullets.Pop();
        bullet.gameObject.SetActive(true);
        return bullet;
    }

    public void ReturnBulletToHolder(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullets.Push(bullet);
    }
}
