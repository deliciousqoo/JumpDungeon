using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumPrint : MonoBehaviour
{
    public List<Sprite> numList;
    public List<Image> printNums;
    private int printingNum;

    public void printNum(int num)
    {
        numClear();
        string str_num = num.ToString();
        int numLength = str_num.Length - 1;

        for (int i = printNums.Count - 1; i >= 0; i--)
        {
            printNums[i].sprite = numList[int.Parse(str_num[numLength--].ToString())];
            if (numLength == -1) break;
        }
    }

    public void numClear()
    {
        for (int i = 0; i < printNums.Count; i++) printNums[i].sprite = numList[10];
    }

    public void setPrintingNum(int num) { this.printingNum = num; }
}
