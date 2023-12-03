using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public GameObject _hoverPanel;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverPanel.SetActive(false);

    }
}
