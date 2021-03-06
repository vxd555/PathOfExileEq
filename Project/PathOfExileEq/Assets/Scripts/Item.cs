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
	[SerializeField] private Image[] _statsCompares;
	[SerializeField] private Item[] _otherItems;
	public bool equiped;
	public int slotID;


	public bool dragOnSurfaces = true;
	public float speed = 1.5f;
	public Vector2 initialPosition;
	private RectTransform rectTransform;
	private CanvasGroup canvasGroup;
	public bool isOver = false;
	public bool showGems = false;
	[Range(0, 1)] public uint width;
	[Range(0, 4)] public uint heigth;

	private Sprite[] Images => !showGems && _imagesNoGems?.Length > 0 ? _imagesNoGems : _images;
	private Sprite ItemImage => equiped ? Images[1] : Images[0];
	public Eqipment backPack;

	private bool _otherEquiped = false;

	void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();
	}

	void Start()
	{
		backPack = FindObjectOfType<Eqipment>();
		initialPosition = transform.position;
		equiped = false;
		for (int i = 0; i < width; i++)
			for (int j = 0; j < heigth; j++)
			{
				backPack.eqipSlot[slotID + j + i * 5] = true;
			}
	}

	void Update()
	{
		if (isOver)
		{
			//Update item stats position
			if (_statsCompares == null || _statsCompares.Length == 0 
				|| _otherItems == null || _otherItems.Length == 0
				|| !_otherEquiped)
			{
				_stats.rectTransform.position = Input.mousePosition + new Vector3(10, 10);
			}
			else
			{
				for (int i = 0; i < _otherItems.Length; i++)
				{
					if (_statsCompares.Length == i) break;

					if (_otherItems[i].equiped)
						_statsCompares[i].rectTransform.position = Input.mousePosition + new Vector3(10, 10);
				}
			}

			//Equip item
			if (Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.LeftShift))
			{
				if (!equiped)
				{
					Equip(_equipPosition.anchoredPosition);
					if (_otherItems.Length != 0)
					{
						for (int i = 0; i < _otherItems.Length; i++)
						{
							if (_otherItems[i].equiped)
								_otherItems[i].Unequip();
						}
					}
				}
				else
				{
					Unequip();
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			Debug.Log("Key down");
			showGems = true;
			gameObject.GetComponent<Image>().sprite = ItemImage;
		}
		else if (Input.GetKeyUp(KeyCode.LeftAlt))
		{
			Debug.Log("Key up");
			showGems = false;
			gameObject.GetComponent<Image>().sprite = ItemImage;
		}
	}

	public void Unequip()
	{

		if (backPack.FindFirstValidSlot(this))
		{
			for (int i = 0; i < width; i++)
				for (int j = 0; j < heigth; j++)
				{
					backPack.eqipSlot[slotID + j + i * 5] = true;
				}
			equiped = false;
			gameObject.GetComponent<Image>().sprite = Images[0];
			Vector3 pos = backPack.GetComponent<RectTransform>().localPosition;
			pos.x += 5 + (slotID / 5) * 50;
			pos.y -= 5 + (slotID % 5) * 50;
			GetComponent<RectTransform>().localPosition = pos;
		}
		else
		{
			backPack.fullInfo.SetActive(true);
			Button[] buttons = backPack.fullInfo.GetComponentsInChildren<Button>();
			buttons[1].onClick.AddListener(() => { backPack.Drop(gameObject); });
		}
	}

	public void Equip(Vector2 pos)
	{
		GetComponent<RectTransform>().anchoredPosition = pos;
		equiped = true;
		gameObject.GetComponent<Image>().sprite = Images[1];
		for (int i = 0; i < width; i++)
			for (int j = 0; j < heigth; j++)
			{
				backPack.eqipSlot[slotID + j + i * 5] = false;
			}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		canvasGroup.alpha = 0.6f;
		canvasGroup.blocksRaycasts = false;
		gameObject.GetComponent<Image>().sprite = Images[1];
	}

	public void OnDrag(PointerEventData eventData)
	{
		rectTransform.anchoredPosition += eventData.delta / _basicObject.scaleFactor;
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

		if (_statsCompares == null || _statsCompares.Length == 0 || _otherItems == null || _otherItems.Length == 0)
		{
			_stats.gameObject.SetActive(true);
		}
		else
		{
			for (int i = 0; i < _otherItems.Length; i++)
			{
				if (_statsCompares.Length == i) break;

				if (_otherItems[i].equiped)
				{
					_otherEquiped = true;
					_statsCompares[i].gameObject.SetActive(true);
				}
			}

			if(!_otherEquiped)
				_stats.gameObject.SetActive(true);
		}

	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Mouse exit");
		isOver = false;

		showGems = false;
		gameObject.GetComponent<Image>().sprite = ItemImage;

		if (_otherItems.Length != 0)
		{
			for (int i = 0; i < _otherItems.Length; i++)
			{
				if (_statsCompares.Length == i) break;

				if (_statsCompares[i].gameObject.activeSelf)
					_statsCompares[i].gameObject.SetActive(false);
			}
		}
		if (_stats.gameObject.activeSelf)
			_stats.gameObject.SetActive(false);
	}
}
