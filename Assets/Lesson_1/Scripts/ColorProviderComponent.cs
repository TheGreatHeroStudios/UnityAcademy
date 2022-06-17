using Assets.Lesson_1.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshRenderer))]
public class ColorProviderComponent : MonoBehaviour
{
	private MeshRenderer _meshRenderer;

	public PrimaryColor cubeColor;

	private void Awake()
	{
		_meshRenderer = GetComponent<MeshRenderer>();
	}


	private void OnGUI()
	{
		switch (cubeColor)
		{
			case PrimaryColor.Red:
			{
				_meshRenderer.sharedMaterial.color = Color.red;
				break;
			}

			case PrimaryColor.Green:
			{
				_meshRenderer.sharedMaterial.color = Color.green;
				break;
			}

			case PrimaryColor.Blue:
			{
				_meshRenderer.sharedMaterial.color = Color.blue;
				break;
			}

			case PrimaryColor.None:
			default:
			{
				_meshRenderer.sharedMaterial.color = Color.gray;
				break;
			}
		}
	}
}
