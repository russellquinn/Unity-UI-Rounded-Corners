using UnityEngine;
using UnityEngine.UI;

namespace Nobi.UiRoundedCorners {
	abstract public class BaseWithRoundedCorners : MonoBehaviour {

        [Tooltip("Do not visually update in Edit Mode. This can help stop constant changes to the Scene file. Does not affect runtime functionality.")]
		public bool doNotRefreshInEditMode = true;

        abstract protected string ShaderName { get; }
		protected Image image;

		//[HideInInspector]
		[SerializeField]
		public Material theMaterial;

		protected void OnValidate() {
			Validate();
			Refresh();
		}

		protected void OnDestroy() {
			if (ImageHasValidMaterial()) {
				image.material = null;
#if UNITY_EDITOR
                if (Application.isPlaying) {
                    Object.Destroy(theMaterial);
                } else {
                    Object.DestroyImmediate(theMaterial);
                }
#else
				Object.Destroy(theMaterial);
#endif
            }
		}

		protected void OnEnable() {
			Validate();
			Refresh();
		}

		protected void OnRectTransformDimensionsChange() {
			if (enabled && ImageHasValidMaterial()) {
				Refresh();
			}
		}

		protected void Validate() {
			if (image == null) {
				TryGetComponent(out image);
			}

			if (image != null && (theMaterial == null || !ImageHasValidMaterial())) {
				theMaterial = new Material(Shader.Find(ShaderName));
				image.material = theMaterial;
				Refresh(true);
			}
		}

		protected void Refresh(bool forceRefresh = false) {
            if ((forceRefresh == true || ShouldRefresh()) && ImageHasValidMaterial()) {
				 DoRefresh();
			}
		}

        abstract protected void DoRefresh();

		protected bool ImageHasValidMaterial() {
			return (image != null && image.material != null && image.material.shader.name == ShaderName);
		}

		protected bool ShouldRefresh() {
#if UNITY_EDITOR
		return !doNotRefreshInEditMode || Application.isPlaying;
#else
		return true;
#endif
		}
	}
}