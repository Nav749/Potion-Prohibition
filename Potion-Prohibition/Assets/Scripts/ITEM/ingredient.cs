using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.EventSystems;

public class ingredient : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {

        rectTransform.anchoredPosition += eventData.delta;
        
        //throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
