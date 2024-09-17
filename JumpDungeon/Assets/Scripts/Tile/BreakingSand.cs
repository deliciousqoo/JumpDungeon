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
            // Gizmos 색상 설정
            Gizmos.color = Color.red;

          
            // BoxCast를 시각화할 방향과 거리
            Vector2 direction = Vector2.up;  // 위쪽으로 레이를 쏘는 방향
            float distance = 0.01f;          // 레이 거리

            // 박스의 변환된 영역을 시각화 (레이가 투사되는 영역)
            Gizmos.DrawWireCube(_collider.bounds.center + (Vector3)direction * distance, _collider.bounds.size);

            // 시각적으로 레이 방향 표시 (라인)
            //Gizmos.DrawLine(_collider.bounds.center, _collider.bounds.center + (Vector3)direction * distance);
        }
    }
}
