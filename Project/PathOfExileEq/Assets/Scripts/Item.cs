using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Canvas _basicObcjet;
    [SerializeField] private RectTransform _equipPosition;
    [SerializeField] private Camera _camera;
    [SerializeField] private Sprite[] _images;
    public bool equip;


    public bool dragOnSurfaces = true;
    public float speed = 1.5f;
    public Vector2 initialPosition;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public bool isOver = false;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        initialPosition = transform.position;
        equip = false;
    }

    void Update()
    {

        if (isOver && Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.LeftShift))
        {
            if (!equip)
            {
                transform.position = _equipPosition.position;
                equip = true;
                gameObject.GetComponent<Image>().sprite = _images[1];
            }
            else
            {
                transform.position = initialPosition;
                equip = false;
                gameObject.GetComponent<Image>().sprite = _images[0];
            }

        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        gameObject.GetComponent<Image>().sprite = _images[1];
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/_basicObcjet.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter");
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        isOver = false;
    }
}
