using UnityEngine;
using UnityEngine.EventSystems;

public class Pot : MonoBehaviour, IDropHandler
{

    private Ingredient droped;
    public void OnDrop(PointerEventData eventData)
    {
        droped = eventData.pointerDrag.GetComponent<Ingredient>();
        Debug.Log(droped.getItem().getName());

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
