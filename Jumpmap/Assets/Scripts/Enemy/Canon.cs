using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Canon : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] anim;
    [SerializeField]
    private GameObject projectilePrefab;

    public float attackSpeed;
    public float projectileSpeed;
    public int direction;
    public float range;

    public float delay;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine("SetDelay");
    }

    private IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine("CanonAttack");
    }

    private IEnumerator CanonAttack()
    {
        for(int i=0;i<anim.Length;i++)
        {
            if (i == 1) { 
                GameObject clone = Instantiate(projectilePrefab);
                clone.GetComponent<CanonProjectile>().SetProjectile(gameObject.transform.position, direction, projectileSpeed, range);
            }
            spriteRenderer.sprite = anim[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(attackSpeed);

        StartCoroutine("CanonAttack");
    }
}
