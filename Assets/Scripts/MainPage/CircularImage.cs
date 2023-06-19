using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace CustomUI
{
    public class CircularImage : BaseImage
    {
        [SerializeField] public float radius = 50;
        [Range(3, 100)]
        [SerializeField] public int segment = 20;

        public override bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, sp, eventCamera, out Vector2 localPos);
            float dis = Vector3.Distance(localPos, Vector3.zero);
            return dis <= radius;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            Color32 color32 = color;
            vh.Clear();

            float deltaRad = 2 * Mathf.PI / segment;
            Vector3 center = Vector3.zero;

            float tw = rectTransform.rect.width;
            float th = rectTransform.rect.height;

            Vector4 uv = overrideSprite != null ? DataUtility.GetOuterUV(overrideSprite) : Vector4.zero;

            float uvCenterX = (uv.x + uv.z) * 0.5f;
            float uvCenterY = (uv.y + uv.w) * 0.5f;
            float uvScaleX = (uv.z - uv.x) / tw;
            float uvScaleY = (uv.w - uv.y) / th;

            for (int i = 0; i < segment; i++)
            {
                float rad = deltaRad * i;
                float sin = Mathf.Sin(rad);
                float cos = Mathf.Cos(rad);
                float x = radius * sin;
                float y = radius * cos;
                vh.AddVert(new Vector3(x, y), color32, new Vector2(x * uvScaleX + uvCenterX, y * uvScaleY + uvCenterY));
            }
            vh.AddVert(center, color32, new Vector2(uvCenterX, uvCenterY));

            for (int i = 0; i < segment; i++)
            {
                if (i == segment - 1)
                {
                    vh.AddTriangle(i, 0, segment);
                }
                else
                {
                    vh.AddTriangle(i, i + 1, segment);
                }
            }
        }
    }
}

