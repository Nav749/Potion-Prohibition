using System.Diagnostics.Contracts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Ingredient : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    #region instance vars
    
    [SerializeField] private Item item;

    //used for draging
    private RectTransform rectTransform;

    #endregion

    private void Start()
    { 
        rectTransform = GetComponent<RectTransform>();
        GetComponent<Image>().sprite = item.getImage();
    }


    #region getters & setters
    public Item getItem() 
    { 
        return item; 
    }

    public void setItem(Item item) 
    { 
        this.item = item;
    }


    #endregion

    #region drag functionality
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {

        rectTransform.anchoredPosition += eventData.delta;
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    #endregion

}
