using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI contentText;
    public LayoutElement layoutElement;
    public int wrapLimit;
    public RectTransform rectTransform;

    [SerializeField] private float testingVariableX;
    [SerializeField] private float testingVariableY;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText( string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerText.gameObject.SetActive(false);
        }
        else
        {
            headerText.gameObject.SetActive(true);
            headerText.text = header;
        }

        contentText.text = content;
        
        var headerLength = headerText.text.Length;
        var contentLength = contentText.text.Length;

        layoutElement.enabled = (headerLength > wrapLimit || contentLength > wrapLimit) ? true : false;
    }

    private void Update()
    {
        var position = Input.mousePosition;
        var pivotX = position.x / Screen.width;
        var pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX + testingVariableX, pivotY + + testingVariableY);
        transform.position = position;
        
        if (!Application.isEditor) return;
        var headerLength = headerText.text.Length;
        var contentLength = contentText.text.Length;

        layoutElement.enabled = (headerLength > wrapLimit || contentLength > wrapLimit) ? true : false;
    }
}
