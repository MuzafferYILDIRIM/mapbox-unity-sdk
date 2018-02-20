﻿namespace Mapbox.Editor
{
	using UnityEditor;
	using UnityEngine;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.MeshGeneration.Modifiers;
	using Mapbox.Unity.MeshGeneration.Interfaces;
	using Mapbox.Editor.NodeEditor;

	[CustomPropertyDrawer(typeof(CameraBoundsTileProviderOptions))]
	public class CameraBoundsTileProviderOptionsDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			//EditorGUI.indentLevel++;
			foreach (var item in property)
			{
				var subproperty = item as SerializedProperty;
				position.height = lineHeight;
				position.y += lineHeight;
				EditorGUI.PropertyField(position, subproperty, true);
			}
			//EditorGUI.indentLevel--;
			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			// Reserve space for the total visible properties.
			int rows = property.CountInProperty();
			return (float)rows * lineHeight;
		}
	}

	[CustomPropertyDrawer(typeof(RangeTileProviderOptions))]
	public class RangeTileProviderOptionsDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			//EditorGUI.indentLevel++;
			foreach (var item in property)
			{
				var subproperty = item as SerializedProperty;
				position.height = lineHeight;
				position.y += lineHeight;
				EditorGUI.PropertyField(position, subproperty, true);
			}
			//EditorGUI.indentLevel--;
			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			// Reserve space for the total visible properties.
			int rows = property.CountInProperty();
			return (float)rows * lineHeight;
		}
	}

	[CustomPropertyDrawer(typeof(RangeAroundTransformTileProviderOptions))]
	public class RangeAroundTransformTileProviderOptionsDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		bool showPosition = true;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			//EditorGUI.indentLevel++;
			foreach (var item in property)
			{
				var subproperty = item as SerializedProperty;
				position.height = lineHeight;
				position.y += lineHeight;
				EditorGUI.PropertyField(position, subproperty, true);
			}
			//EditorGUI.indentLevel--;
			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			// Reserve space for the total visible properties.
			int rows = property.CountInProperty();
			return (float)rows * lineHeight;
		}
	}

	[CustomPropertyDrawer(typeof(GeometryExtrusionOptions))]
	public class GeometryExtrusionOptionsDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		bool showPosition = true;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			//position.y += lineHeight;
			var typePosition = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Extrusion Type"));
			var extrusionTypeProperty = property.FindPropertyRelative("extrusionType");

			extrusionTypeProperty.enumValueIndex = EditorGUI.Popup(typePosition, extrusionTypeProperty.enumValueIndex, extrusionTypeProperty.enumDisplayNames);
			var sourceTypeValue = (Unity.Map.ExtrusionType)extrusionTypeProperty.enumValueIndex;

			var minHeightProperty = property.FindPropertyRelative("minimumHeight");
			var maxHeightProperty = property.FindPropertyRelative("maximumHeight");

			switch (sourceTypeValue)
			{
				case Unity.Map.ExtrusionType.None:
					break;
				case Unity.Map.ExtrusionType.PropertyHeight:
					position.y += lineHeight;
					EditorGUI.PropertyField(position, property.FindPropertyRelative("extrusionGeometryType"));
					position.y += lineHeight;
					EditorGUI.PropertyField(position, property.FindPropertyRelative("propertyName"));
					break;
				case Unity.Map.ExtrusionType.MinHeight:
					position.y += lineHeight;
					EditorGUI.PropertyField(position, property.FindPropertyRelative("extrusionGeometryType"));
					position.y += lineHeight;
					EditorGUI.PropertyField(position, property.FindPropertyRelative("propertyName"));
					position.y += lineHeight;
					EditorGUI.PropertyField(position, minHeightProperty);
					//maxHeightProperty.floatValue = minHeightProperty.floatValue;
					break;
				case Unity.Map.ExtrusionType.MaxHeight:
					position.y += lineHeight;
					EditorGUI.PropertyField(position, property.FindPropertyRelative("extrusionGeometryType"));
					position.y += lineHeight;
					EditorGUI.PropertyField(position, property.FindPropertyRelative("propertyName"));
					position.y += lineHeight;
					EditorGUI.PropertyField(position, maxHeightProperty);
					//min.floatValue = minHeightProperty.floatValue;
					break;
				case Unity.Map.ExtrusionType.RangeHeight:
					position.y += lineHeight;
					EditorGUI.PropertyField(position, property.FindPropertyRelative("extrusionGeometryType"));
					position.y += lineHeight;
					EditorGUI.PropertyField(position, property.FindPropertyRelative("propertyName"));
					position.y += lineHeight;
					EditorGUI.PropertyField(position, minHeightProperty);
					position.y += lineHeight;
					EditorGUI.PropertyField(position, maxHeightProperty);
					break;
				case Unity.Map.ExtrusionType.AbsoluteHeight:
					position.y += lineHeight;
					EditorGUI.PropertyField(position, property.FindPropertyRelative("extrusionGeometryType"));
					position.y += lineHeight;
					EditorGUI.PropertyField(position, property.FindPropertyRelative("propertyName"));
					position.y += lineHeight;
					EditorGUI.PropertyField(position, maxHeightProperty, new GUIContent { text = "Height" });
					break;
				default:
					break;
			}
			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var extrusionTypeProperty = property.FindPropertyRelative("extrusionType");
			var sourceTypeValue = (Unity.Map.ExtrusionType)extrusionTypeProperty.enumValueIndex;

			int rows = 0;
			switch (sourceTypeValue)
			{
				case Unity.Map.ExtrusionType.None:
					rows = 1;
					break;
				case Unity.Map.ExtrusionType.PropertyHeight:
					rows = 3;
					break;
				case Unity.Map.ExtrusionType.MinHeight:
				case Unity.Map.ExtrusionType.MaxHeight:
				case Unity.Map.ExtrusionType.AbsoluteHeight:
					rows = 4;
					break;
				case Unity.Map.ExtrusionType.RangeHeight:
					rows = 5;
					break;
				default:
					rows = 2;
					break;
			}
			return (float)rows * lineHeight;
		}
	}

	[CustomPropertyDrawer(typeof(MaterialList))]
	public class MaterialListDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			//EditorGUI.indentLevel++;
			position.y += lineHeight;
			EditorGUI.PropertyField(position, property.FindPropertyRelative("Materials"), true);
			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			// Reserve space for the total visible properties.
			var matList = property.FindPropertyRelative("Materials");
			int rows = (matList.isExpanded) ? matList.arraySize + 3 : 1;
			return (float)rows * lineHeight;
		}
	}

	[CustomPropertyDrawer(typeof(GeometryMaterialOptions))]
	public class GeometryMaterialOptionsDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		bool showPosition = true;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			showPosition = EditorGUI.Foldout(position, showPosition, label.text);
			//EditorGUI.indentLevel++;
			if (showPosition)
			{
				position.y += lineHeight;
				var projectMapImg = property.FindPropertyRelative("projectMapImagery");
				var typePosition = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Project Imagery"));
				projectMapImg.boolValue = EditorGUI.Toggle(typePosition, projectMapImg.boolValue);

				//position.y += lineHeight;
				var matList = property.FindPropertyRelative("materials");
				for (int i = 0; i < matList.arraySize; i++)
				{
					var matInList = matList.GetArrayElementAtIndex(i);
					EditorGUI.PropertyField(position, matInList);
					position.y += EditorGUI.GetPropertyHeight(matInList);
				}
			}
			//EditorGUI.indentLevel--;
			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			// Reserve space for the total visible properties.
			float height = 0.0f;
			if (showPosition)
			{
				height += (2.0f * lineHeight);
				var matList = property.FindPropertyRelative("materials");

				for (int i = 0; i < matList.arraySize; i++)
				{
					var matInList = matList.GetArrayElementAtIndex(i);
					height += EditorGUI.GetPropertyHeight(matInList);
				}
			}
			else
			{
				height = EditorGUIUtility.singleLineHeight;
			}
			return height;
		}
	}

	[CustomPropertyDrawer(typeof(GeometryStylingOptions))]
	public class GeometryStylingOptionsDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		bool showPosition = true;
		SerializedProperty isActiveProperty;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			isActiveProperty = property.FindPropertyRelative("isExtruded");

			EditorGUI.BeginProperty(position, label, property);

			EditorGUI.PropertyField(position, property.FindPropertyRelative("extrusionOptions"), false);
			position.y += (EditorGUI.GetPropertyHeight(property.FindPropertyRelative("extrusionOptions")));
			//}
			//position.y += lineHeight;
			EditorGUI.PropertyField(position, property.FindPropertyRelative("materialOptions"), false);
			EditorGUI.EndProperty();

		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float height = 0.0f;
			if (showPosition)
			{
				height += (2.0f * EditorGUIUtility.singleLineHeight);
				height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("extrusionOptions"), false);
				height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("materialOptions"), false);
			}
			else
			{
				height = EditorGUIUtility.singleLineHeight;
			}

			return height;
		}
	}

	[CustomPropertyDrawer(typeof(LayerModifierOptions))]
	public class LayerModifierOptionsDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		bool showPosition = true;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position.height = lineHeight;
			showPosition = EditorGUI.Foldout(position, showPosition, label.text);
			EditorGUI.indentLevel++;
			if (showPosition)
			{
				position.y += lineHeight;
				var typePosition = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Feature Position"));
				var featurePositionProperty = property.FindPropertyRelative("moveFeaturePositionTo");
				featurePositionProperty.enumValueIndex = EditorGUI.Popup(typePosition, featurePositionProperty.enumValueIndex, featurePositionProperty.enumDisplayNames);

				position.y += lineHeight;
				EditorGUI.LabelField(position, "Mesh Modifiers");

				//var meshfac = property.FindPropertyRelative("MeshModifiers");
				//position.y += lineHeight;
				////.BeginArea(new Rect(position.x, position.y, position.width, 200));

				//for (int i = 0; i < meshfac.arraySize; i++)
				//{
				//	var ind = i;
				//	EditorGUILayout.BeginHorizontal();
				//	//EditorGUILayout.BeginVertical();
				//	//GUILayout.Space(5);
				//	meshfac.GetArrayElementAtIndex(ind).objectReferenceValue = EditorGUILayout.ObjectField(meshfac.GetArrayElementAtIndex(i).objectReferenceValue, typeof(MeshModifier), false) as ScriptableObject;
				//	EditorGUILayout.EndVertical();
				//	if (GUILayout.Button(new GUIContent("+"), (GUIStyle)"minibuttonleft", GUILayout.Width(30)))
				//	{
				//		ScriptableCreatorWindow.Open(typeof(MeshModifier), meshfac, ind);
				//	}
				//	if (GUILayout.Button(new GUIContent("-"), (GUIStyle)"minibuttonright", GUILayout.Width(30)))
				//	{
				//		meshfac.DeleteArrayElementAtIndex(ind);
				//	}
				//	EditorGUILayout.EndHorizontal();
				//}

				//EditorGUILayout.Space();
				//EditorGUILayout.BeginHorizontal();
				//if (GUILayout.Button(new GUIContent("Add New Empty"), (GUIStyle)"minibuttonleft"))
				//{
				//	meshfac.arraySize++;
				//	meshfac.GetArrayElementAtIndex(meshfac.arraySize - 1).objectReferenceValue = null;
				//}
				//if (GUILayout.Button(new GUIContent("Find Asset"), (GUIStyle)"minibuttonright"))
				//{
				//	ScriptableCreatorWindow.Open(typeof(MeshModifier), meshfac);
				//}
				//EditorGUILayout.EndHorizontal();

				//EditorGUILayout.Space();
				//EditorGUILayout.LabelField("Game Object Modifiers");
				//var gofac = property.FindPropertyRelative("GoModifiers");
				//for (int i = 0; i < gofac.arraySize; i++)
				//{
				//	var ind = i;
				//	EditorGUILayout.BeginHorizontal();
				//	EditorGUILayout.BeginVertical();
				//	GUILayout.Space(5);
				//	gofac.GetArrayElementAtIndex(ind).objectReferenceValue = EditorGUILayout.ObjectField(gofac.GetArrayElementAtIndex(i).objectReferenceValue, typeof(GameObjectModifier), false) as ScriptableObject;
				//	EditorGUILayout.EndVertical();

				//	if (GUILayout.Button(new GUIContent("+"), (GUIStyle)"minibuttonleft", GUILayout.Width(30)))
				//	{
				//		ScriptableCreatorWindow.Open(typeof(GameObjectModifier), gofac, ind);
				//	}
				//	if (GUILayout.Button(new GUIContent("-"), (GUIStyle)"minibuttonright", GUILayout.Width(30)))
				//	{
				//		gofac.DeleteArrayElementAtIndex(ind);
				//	}
				//	EditorGUILayout.EndHorizontal();
				//}

				//EditorGUILayout.Space();
				//EditorGUILayout.BeginHorizontal();
				//if (GUILayout.Button(new GUIContent("Add New Empty"), (GUIStyle)"minibuttonleft"))
				//{
				//	gofac.arraySize++;
				//	gofac.GetArrayElementAtIndex(gofac.arraySize - 1).objectReferenceValue = null;
				//}
				//if (GUILayout.Button(new GUIContent("Find Asset"), (GUIStyle)"minibuttonright"))
				//{
				//	ScriptableCreatorWindow.Open(typeof(GameObjectModifier), gofac);
				//}
				//EditorGUILayout.EndHorizontal();
				//GUILayout.EndArea();
			}
			EditorGUI.indentLevel--;
			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float height = 0.0f;
			if (showPosition)
			{
				height += (10.0f * EditorGUIUtility.singleLineHeight);
				height += (property.FindPropertyRelative("MeshModifiers").arraySize * lineHeight * 2);
				height += (property.FindPropertyRelative("GoModifiers").arraySize * lineHeight * 2);
				//height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("stylingOptions"));
			}
			else
			{
				height += (3.0f * EditorGUIUtility.singleLineHeight);
			}

			return height;
		}
	}

	[CustomPropertyDrawer(typeof(CoreVectorLayerProperties))]
	public class CoreVectorLayerPropertiesDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		bool showPosition = true;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position.height = lineHeight;
			showPosition = EditorGUI.Foldout(position, showPosition, label.text);
			//EditorGUI.indentLevel++;
			if (showPosition)
			{

				position.y += lineHeight;
				// Draw label.
				var typePosition = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Primitive Type"));
				var sourceTypeProperty = property.FindPropertyRelative("geometryType");
				sourceTypeProperty.enumValueIndex = EditorGUI.Popup(typePosition, sourceTypeProperty.enumValueIndex, sourceTypeProperty.enumDisplayNames);

				position.y += lineHeight;
				EditorGUI.PropertyField(position, property.FindPropertyRelative("layerName"));

				position.y += lineHeight;
				var propertyFilters = property.FindPropertyRelative("propertyValuePairs");
				EditorGUI.PropertyField(position, propertyFilters);

				position.y += ((propertyFilters.arraySize + 1) * lineHeight);
				EditorGUI.PropertyField(position, property.FindPropertyRelative("groupFeatures"));


			}
			//EditorGUI.indentLevel--;
			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float height = 0.0f;
			if (showPosition)
			{
				height += (5.0f * EditorGUIUtility.singleLineHeight);
				height += (property.FindPropertyRelative("propertyValuePairs").arraySize * lineHeight * 2);
				//height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("stylingOptions"));
			}
			else
			{
				height = EditorGUIUtility.singleLineHeight;
			}

			return height;
		}
	}

	[CustomPropertyDrawer(typeof(ImageryRasterOptions))]
	public class ImageryRasterOptionsDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		bool showPosition = true;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position.height = lineHeight;
			showPosition = EditorGUI.Foldout(position, showPosition, label.text);
			//EditorGUI.indentLevel++;
			if (showPosition)
			{
				foreach (var item in property)
				{
					var subproperty = item as SerializedProperty;
					position.height = lineHeight;
					position.y += lineHeight;
					EditorGUI.PropertyField(position, subproperty, true);
				}
			}
			//EditorGUI.indentLevel--;
			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			int rows = (showPosition) ? 4 : 1;
			return (float)rows * lineHeight;
		}
	}

	[CustomPropertyDrawer(typeof(LayerSourceOptions))]
	public class LayerSourceOptionsDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		bool showPosition = true;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position.height = lineHeight;
			showPosition = EditorGUI.Foldout(position, showPosition, label.text);

			EditorGUI.indentLevel++;

			if (showPosition)
			{
				position.y += lineHeight;
				EditorGUI.PropertyField(position, property.FindPropertyRelative("isActive"), true);

				position.y += lineHeight;
				EditorGUI.PropertyField(position, property.FindPropertyRelative("layerSource"), true);
			}

			EditorGUI.indentLevel--;

			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float height = 0.0f;
			if (showPosition)
			{
				height += EditorGUIUtility.singleLineHeight;
				height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("layerSource"), false);
			}
			else
			{
				height = EditorGUIUtility.singleLineHeight;
			}

			return height;
		}
	}

	[CustomPropertyDrawer(typeof(LayerPerformanceOptions))]
	public class LayerPerformanceOptionsDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		bool showPosition = true;

		SerializedProperty isActiveProperty;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			isActiveProperty = property.FindPropertyRelative("isEnabled");

			EditorGUI.BeginProperty(position, label, property);
			position.height = lineHeight;
			var typePosition = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Enable Coroutines"));
			isActiveProperty.boolValue = EditorGUI.Toggle(typePosition, isActiveProperty.boolValue);

			if (isActiveProperty.boolValue == true)
			{
				EditorGUI.indentLevel++;
				position.y += lineHeight;
				EditorGUI.PropertyField(position, property.FindPropertyRelative("entityPerCoroutine"), true);
				EditorGUI.indentLevel--;
			}

			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			float height = 0.0f;
			if (isActiveProperty.boolValue == true)
			{
				height += (2.0f * EditorGUIUtility.singleLineHeight);
				//height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("layerSource"), false);
			}
			else
			{
				height = EditorGUIUtility.singleLineHeight;
			}

			return height;
		}
	}
	[CustomPropertyDrawer(typeof(Style))]
	public class StyleOptionsDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		bool showPosition = true;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			//position.y += lineHeight;
			position.height = lineHeight;
			showPosition = EditorGUI.Foldout(position, showPosition, "Source Details");
			if (showPosition)
			{
				//EditorGUI.indentLevel++;
				foreach (var item in property)
				{
					//Debug.Log("here");
					var subproperty = item as SerializedProperty;
					if (subproperty.name == "UserName" || subproperty.name == "Modified")
					{
						return;
					}
					position.y += lineHeight;
					//position.height = lineHeight;
					EditorGUI.PropertyField(position, subproperty, true);
				}
				//EditorGUI.indentLevel--;
			}

			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			// Reserve space for the total visible properties.
			int rows = (showPosition) ? 4 : 2;
			//Debug.Log("Height - " + rows * lineHeight);
			return (float)rows * lineHeight;
		}
	}

	[CustomPropertyDrawer(typeof(TypeVisualizerTuple))]
	public class TypeVisualizerBaseDrawer : PropertyDrawer
	{
		static float lineHeight = EditorGUIUtility.singleLineHeight;
		bool showPosition = true;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			//position.y += lineHeight;
			position.height = lineHeight;
			EditorGUI.PropertyField(position, property.FindPropertyRelative("Stack"));

			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			// Reserve space for the total visible properties.
			int rows = 2;
			//Debug.Log("Height - " + rows * lineHeight);
			return (float)rows * lineHeight;
		}
	}

}