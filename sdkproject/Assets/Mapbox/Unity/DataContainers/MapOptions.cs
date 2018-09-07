﻿namespace Mapbox.Unity.Map
{
	using System;
	using UnityEngine;
	[Serializable]
	public class MapOptions
	{
		public event Action OnPropertyUpdated = delegate { };

		public void UpdateProperty()
		{
			if (OnPropertyUpdated != null)
			{
				OnPropertyUpdated();
			}
		}

		public MapLocationOptions locationOptions = new MapLocationOptions();
		public MapExtentOptions extentOptions = new MapExtentOptions(MapExtentType.RangeAroundCenter);
		public MapPlacementOptions placementOptions = new MapPlacementOptions();
		public MapScalingOptions scalingOptions = new MapScalingOptions();
		[Tooltip("Texture used while tiles are loading.")]
		public Texture2D loadingTexture = null;
		public Material tileMaterial = null;
	}
}
