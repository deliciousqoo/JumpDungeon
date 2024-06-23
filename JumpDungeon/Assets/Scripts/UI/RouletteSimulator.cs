using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class RouletteSimulator : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private GameObject roulette;

    private const float start_x = 290f;
    private const float end_x = -160f;

    private void Awake()
    {
        startBtn.onClick.AddListener(() => { StartCoroutine("RotateRoulette"); });
    }

    /*
    public IEnumerator RotateRoulette()
    {
        Vector2 orgPos = roulette.transform.localPosition;
        Vector2 tempPos = orgPos;
        int temp = 1000;
        Debug.Log("check1");
        while(temp-->0)
        {
            tempPos.x -= 5f;
            roulette.transform.localPosition = tempPos;
            if (roulette.transform.localPosition.x <= -160f)
            {
                RoulettePosReset(orgPos);
                tempPos = roulette.transform.position;
            }
            yield return new WaitForSeconds(0.001f);
        }
        Debug.Log("check2");
    }*/

    /*
    private void RoulettePosReset(Vector2 orgPos)
    {
        roulette.transform.position = orgPos;
    }*/
}
