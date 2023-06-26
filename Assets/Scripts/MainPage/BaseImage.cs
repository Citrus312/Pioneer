using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CustomUI
{
    //重写了图片基类，专门用于圆形图片的显示
    public class BaseImage : MaskableGraphic, ISerializationCallbackReceiver, ILayoutElement, ICanvasRaycastFilter
    {
        [FormerlySerializedAs("m_Frame")]
        [SerializeField]
        private Sprite m_Sprite;
        public Sprite sprite
        {
            get
            {
                return m_Sprite;
            }
            set
            {
                if (SetPropertyUtility.SetClass(ref m_Sprite, value))
                {
                    SetAllDirty();
                }
            }
        }

        [NonSerialized]
        private Sprite m_OverrideSprite;
        public Sprite overrideSprite
        {
            get
            {
                return m_OverrideSprite == null ? sprite : m_OverrideSprite;
            }
            set
            {
                if (SetPropertyUtility.SetClass(ref m_OverrideSprite, value))
                {
                    SetAllDirty();
                }
            }
        }

        public override Texture mainTexture
        {
            get
            {
                return overrideSprite == null ? s_WhiteTexture : overrideSprite.texture;
            }
        }

        public float pixelsPerUnit
        {
            get
            {
                float spritePixelsPerUnit = 100;
                if (sprite)
                {
                    spritePixelsPerUnit = sprite.pixelsPerUnit;
                }

                float referencePixelsPerUnit = 100;
                if (canvas)
                {
                    referencePixelsPerUnit = canvas.referencePixelsPerUnit;
                }

                return spritePixelsPerUnit / referencePixelsPerUnit;
            }
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            base.OnPopulateMesh(vh);
        }

        #region ISerializationCallbackReceiver
        public void OnAfterDeserialize()
        {

        }

        public void OnBeforeSerialize()
        {

        }
        #endregion

        #region ILayoutElement
        public virtual void CalculateLayoutInputHorizontal() { }
        public virtual void CalculateLayoutInputVertical() { }

        public virtual float minWidth
        {
            get
            {
                return 0;
            }
        }

        public virtual float preferredWidth
        {
            get
            {
                if (overrideSprite == null)
                {
                    return 0;
                }
                return overrideSprite.rect.size.x / pixelsPerUnit;
            }
        }

        public virtual float flexibleWidth
        {
            get
            {
                return -1;
            }
        }

        public virtual float minHeight
        {
            get
            {
                return 0;
            }
        }

        public virtual float preferredHeight
        {
            get
            {
                if (overrideSprite == null)
                {
                    return 0;
                }
                return overrideSprite.rect.size.y / pixelsPerUnit;
            }
        }

        public virtual float flexibleHeight
        {
            get
            {
                return -1;
            }
        }

        public virtual int layoutPriority
        {
            get
            {
                return 0;
            }
        }
        #endregion

        #region ICanvasRaycastFilter
        public virtual bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            return true;
        }
        #endregion
    }
}

