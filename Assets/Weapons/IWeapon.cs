using UnityEngine;

interface IWeapon
{
    WeaponType Type { get; }
    Sprite Sprite { get; }
    Sprite BulletSprite { get; }
    bool Automatic { get; }
    float Damage { get; }
    void Shoot();
}
