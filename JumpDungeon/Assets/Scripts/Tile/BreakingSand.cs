using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BreakingSand : Tile, ITrapTile
{
    private Coroutine _sandBreakCoroutine;

    private void Update()
    {
        if (_collider == null) return;

        RaycastHit2D rayHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.up, 0.02f, LayerMask.GetMask("Player"));
        if (rayHit.collider != null)
        {
            Debug.Log(rayHit.collider.gameObject.name);

            if(_sandBreakCoroutine == null)
            {
                _sandBreakCoroutine = StartCoroutine("SandBreak");
            }
        }
    }

    private IEnumerator SandBreak()
    {
        yield return new WaitForSeconds(1f);

        GameObject clone = Instantiate(EffectPrefab);
        clone.transform.position = _composite.bounds.center;

        Color tempColor = _spriteRenderer.color;
        tempColor.a = 0f;
        _spriteRenderer.color = tempColor;
        _composite.isTrigger = true;

        yield return new WaitForSeconds(2f);
        
        _spriteRenderer.DOFade(1f, 1f).OnComplete(() =>
        {
            _composite.isTrigger = false;
            _sandBreakCoroutine = null;
        });
    }

    private void OnDrawGizmosSelected()
    {
        if (_collider != null)
        {
            // Gizmos ���� ����
            Gizmos.color = Color.red;

          
            // BoxCast�� �ð�ȭ�� ����� �Ÿ�
            Vector2 direction = Vector2.up;  // �������� ���̸� ��� ����
            float distance = 0.01f;          // ���� �Ÿ�

            // �ڽ��� ��ȯ�� ������ �ð�ȭ (���̰� ����Ǵ� ����)
            Gizmos.DrawWireCube(_collider.bounds.center + (Vector3)direction * distance, _collider.bounds.size);

            // �ð������� ���� ���� ǥ�� (����)
            //Gizmos.DrawLine(_collider.bounds.center, _collider.bounds.center + (Vector3)direction * distance);
        }
    }
}
