using UnityEngine;
using UnityEngine.UI;

namespace Nobi.UiRoundedCorners {
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class ImageWithRoundedCorners : BaseWithRoundedCorners {
		private static readonly int Props = Shader.PropertyToID("_WidthHeightRadius");
		override protected string ShaderName { get { return "UI/RoundedCorners/RoundedCorners"; } }

		public float radius;

		override protected void DoRefresh() {
			var rect = ((RectTransform)transform).rect;
			theMaterial.SetVector(Props, new Vector4(rect.width, rect.height, radius, 0));
		}
	}
}