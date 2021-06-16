using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {
	public bool IsFill { get; private set; }

	public Color defaultColor;
	public Color fillColor;

	private Image cellImage;

	void Start() {
		cellImage = GetComponent<Image>();
		cellImage.color = defaultColor;
	}

	public void Clear() {
		cellImage.color = defaultColor;
		IsFill = false;
	}

	private void Fill() {
		cellImage.color = fillColor;
		IsFill = true;
	}

	public void OnPointerDown(PointerEventData eventData) {
		if (Input.GetMouseButton(0)) {
			Fill();
		} else if (Input.GetMouseButton(1)) {
			Clear();
		}
	}

	public void OnPointerEnter(PointerEventData eventData) {
		if (Input.GetMouseButton(0)) {
			Fill();
		} else if (Input.GetMouseButton(1)) {
			Clear();
		}
	}
}
