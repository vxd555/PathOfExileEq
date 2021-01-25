using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Canvas _basicObject;
    [SerializeField] private RectTransform _equipPosition;
    [SerializeField] private Camera _camera;
    [SerializeField] private Sprite[] _images;
    [SerializeField] private Sprite[] _imagesNoGems;
    [SerializeField] private Image _stats;
    [SerializeField] private Image _statsCompare;
    [SerializeField] private Item _otherItem;
    public bool equiped;


    public bool dragOnSurfaces = true;
    public float speed = 1.5f;
    public Vector2 initialPosition;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public bool isOver = false;
    public bool showGems = false;

    private Sprite[] Images  =>  !showGems && _imagesNoGems?.Length > 0 ? _imagesNoGems : _images;
    private Sprite ItemImage => equiped ? Images[1] : Images[0];


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        initialPosition = transform.position;
        equiped = false;
    }

    void Update()
    {
        if (isOver)
        {
            //Update item stats position
            if(_otherItem != null && _otherItem.equiped)
                _statsCompare.rectTransform.position = Input.mousePosition + new Vector3(10, 10);
			else
                _stats.rectTransform.position = Input.mousePosition + new Vector3(10, 10);

            //Equip item
            if (Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.LeftShift))
			{
				if (!equiped)
				{
                    if(_otherItem != null && _otherItem.equiped)
                        _otherItem.Unequip();
					Equip();
				}
				else
				{
					Unequip();
				}
			}
        }
    }

	public void Unequip()
	{
		transform.position = initialPosition;
		equiped = false;
		gameObject.GetComponent<Image>().sprite = Images[0];
	}

	public void Equip()
	{
		transform.position = _equipPosition.position;
		equiped = true;
		gameObject.GetComponent<Image>().sprite = Images[1];
	}

	public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        gameObject.GetComponent<Image>().sprite = Images[1];
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/_basicObject.scaleFactor;
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

        showGems = true;
        gameObject.GetComponent<Image>().sprite = ItemImage;

        if (_otherItem != null && _otherItem.equiped)
            _statsCompare.gameObject.SetActive(true);
        else
            _stats.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        isOver = false;

        showGems = false;
        gameObject.GetComponent<Image>().sprite = ItemImage;

        if (_statsCompare != null && _statsCompare.gameObject.activeSelf)
            _statsCompare.gameObject.SetActive(false);
        else
            _stats.gameObject.SetActive(false);
    }
}
