using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSort : MonoBehaviour {
    public GameManager gm;
    public GameObject[] nodeArray;
    public int n;
    public GameObject pivot;
    public int lIndex, uIndex;
    public bool[] sortBool;
    public bool pivSort = false;
    public bool sorted = false;


    // Update is called once per frame
    void Update() {
        //Make each node uniform before applying step
        for (int i = 0; i < nodeArray.Length; i++) {
            GameObject node = nodeArray[i];
            float width = 16f / n;
            node.transform.position = Vector3.right * (-8f + (width / 2) + (width * i));
            if (gm.compareType == "Height") node.GetComponent<SpriteRenderer>().color = Color.white;
            if (gm.compareType == "Color") node.transform.localScale = new Vector3(node.transform.localScale.x, 9, 1);
        }
        if (sorted) return;
        if (pivSort) {
            sortBool[uIndex] = true;
            while (pivSort) {
                lIndex = 0;
                while (sortBool[lIndex] == true && !sorted) {
                    lIndex++;
                    if (lIndex == n-1) {
                        sorted = true;
                        return;
                    }
                }
                uIndex = lIndex;
                while (sortBool[uIndex] == false && uIndex != n - 1) {
                    uIndex++;
                }
                if (uIndex - lIndex == 1) {
                    sortBool[lIndex] = true;
                } else {
                    pivSort = false;
                    pivot = nodeArray[Random.Range(lIndex, uIndex + 1)];
                }
            }
        } else {
            if (gm.Compare(pivot, nodeArray[lIndex])) lIndex++;
            else if (gm.Compare(nodeArray[uIndex], pivot)) uIndex--;
            else {
                GameObject temp = nodeArray[uIndex];
                nodeArray[uIndex] = nodeArray[lIndex];
                nodeArray[lIndex] = temp;
            }
            if (lIndex == uIndex) pivSort = true;
        }

    }
}