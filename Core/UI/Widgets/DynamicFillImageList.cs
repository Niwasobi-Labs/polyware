using System;
using PolyWare.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Alpaca.UI {
	public class DynamicFillImageList : Widget {
		[SerializeField] private GameObject listParent;
		[SerializeField] private GameObject imagePrefab;
		[SerializeField] private int unitsPerImage;
		
		private readonly List<IFillable> imageList = new List<IFillable>();
		
		private int currentImageCount;
		private int currentMaxUnits;

		private void Awake() {
			GetComponentsInChildren<IFillable>(imageList);
		}

		public void Initialize(int currentUnits, int totalUnits) {
			currentMaxUnits = totalUnits;
			
			currentImageCount = currentMaxUnits / unitsPerImage;
			
			if (currentImageCount > imageList.Count) GrowList(currentImageCount - imageList.Count);
			
			ShowHideImages();
			
			Refresh(currentUnits);
		}

		private void GrowList(int addAmount) {
			for (int i = 0; i < addAmount; i++) {
				if (!Instantiate(imagePrefab, listParent.transform).TryGetComponent(out IFillable fillable)) {
					throw new ArgumentException($"{imagePrefab.name} does not implement IFillable interface");
				}
				imageList.Add(fillable);
			}
		}

		private void ShowHideImages() {
			for (int i = 0; i < imageList.Count; i++) {
				imageList[i].GameObject.SetActive(i < currentImageCount);
			}
		}
		
		public void Refresh(int current) {
			for (int i = 0; i < currentImageCount; i++) {
				if (current <= 0) {
					imageList[i].SetFillAmount(0);
					break;
				}
				if (current < unitsPerImage) {
					imageList[i].SetFillAmount(current / (float)unitsPerImage);
					break;
				}
				imageList[i].SetFillAmount(1);
				current -= unitsPerImage;
			}
		}
	}
}