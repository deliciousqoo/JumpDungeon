using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Canon : Enemy, IShootableEnemy
{
    [SerializeField] private Sprite[] anim;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float projectileSpeed;
    [SerializeField] private float range;
    [SerializeField] private float delay;

    private void Start()
    {
        StartCoroutine("SetDelay");
    }

    private IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine("ShootAttack");
    }

    public IEnumerator ShootAttack()
    {
        for (int i = 0; i < anim.Length; i++)
        {
            if (i == 1)
            {
                GameObject clone = Instantiate(projectilePrefab);
                clone.GetComponent<CanonProjectile>().SetProjectile(gameObject.transform.position, Direction, projectileSpeed, range);
            }
            sr.sprite = anim[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(Speed);

        StartCoroutine("ShootAttack");
    }
}
