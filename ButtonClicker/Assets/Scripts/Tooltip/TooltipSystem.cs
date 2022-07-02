using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem Instance { get; private set; }
    public Tooltip tooltip;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public static void Show(string content, string header = "")
    {
        Instance.tooltip.SetText(content,header);
        Instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        Instance.tooltip.gameObject.SetActive(false);
    }
}
