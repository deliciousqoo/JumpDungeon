using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : Enemy, IShootable
{
    public GameObject Projectile;

    [field: SerializeField] public float ProjectileSpeed { get; set; }
    [field: SerializeField] public float ShootCycle { get; set; }
    [field: SerializeField] public float ShootRange { get; set; }
    [field: SerializeField] public int Direction { get; set; }
    [field: SerializeField] public float DelayTime { get; set; }

    private Coroutine ShootCoroutine;

    protected override void InitializeInfo()
    {
        base.InitializeInfo();
    }

    private void Start()
    {
        Invoke("Shoot", DelayTime);
    }

    private IEnumerator ShootProjectile()
    {
        GameObject clone = Instantiate(Projectile, gameObject.transform);
        clone.transform.localPosition = Vector3.zero;
        clone.GetComponent<CanonProjectile>().Setup(ProjectileSpeed, Direction, ShootRange);

        yield return new WaitForSeconds(ShootCycle);

        StartCoroutine("ShootProjectile");
    }

    public void Shoot()
    {
        StartCoroutine("ShootProjectile");
    }
}
