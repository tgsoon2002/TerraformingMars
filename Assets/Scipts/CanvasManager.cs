using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public int col, row, spacing;
    // Use this for initialization
    void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        GridLayoutGroup grid = GetComponent<GridLayoutGroup>();

        grid.spacing = new Vector2(spacing, spacing);
        float length = Screen.height / row - (2 * spacing);
        grid.cellSize = new Vector2(length, length);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
