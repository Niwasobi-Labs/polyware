using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class RadarSettings {
		public enum DistantUpdateType {
			Never,
			Always,
			WhenNoLocalsAreNotFound
		}

		public enum SortType {
			None,
			ClosestFirst,
			FarthestFirst,
		}
		
		public LayerMask LayerMask;
		
		[Header("Local Settings")]
		public float LocalRange = 20f;
		public int MaxLocalResults = 512;
		public SortType LocalSortingMethod;
			
		[Header("Distant Settings")]
		public DistantUpdateType DistantUpdateMethod = DistantUpdateType.Never;
		public float DistantRange = 100f;
		public int MaxDistantResults = 10;
		public SortType DistantSortingMethod;
	}
}