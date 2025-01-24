using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonkeyBusiness.Core.VariableReferences.GenericVariables
{
    [Serializable]
    [InlineProperty]
    public abstract class ValueReference<TValue, TAsset> where TAsset : ValueAsset<TValue>
    {
        [HorizontalGroup("Reference", MaxWidth = 100)]
        [ValueDropdown("valueList")]
        [HideLabel]
        [SerializeField]
        protected bool useValue = true;
        
        [ShowIf("useValue", Animate = false)]
        [HorizontalGroup("Reference")]
        [HideLabel]
        [SerializeField]
        protected TValue _value;
        
        [HideIf("useValue", Animate = false)]
        [HorizontalGroup("Reference")]
        [OnValueChanged("UpdateAsset")]
        [HideLabel]
        [SerializeField]
        protected TAsset assetReference;
        
        [ShowIf("@assetReference != null && useValue == false && false")]
        [LabelWidth(100)]
        [SerializeField]
        protected bool editAsset = false;
        
        [ShowIf("@assetReference != null && useValue == false")]
        [EnableIf("editAsset")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [SerializeField]
        protected TAsset _assetReference;

        private static ValueDropdownList<bool> valueList = new ValueDropdownList<bool>()
        {
            {"Value", true},
            {"Reference", false},
        };

        public TValue value
        {
            get
            {
                if (assetReference == null || useValue)
                    return _value;
                else
                    return assetReference.value;
            }
        }

        public static implicit operator TValue(ValueReference<TValue, TAsset> valueRef)
        {
            return valueRef.value;
        }

        protected void UpdateAsset()
        {
            _assetReference = assetReference;
        }
    }
    
    [Serializable]
    public class FloatReference : ValueReference<float, FloatAsset>
    { }
    
    [Serializable]
    public class BoolReference : ValueReference<bool, BoolAsset>
    { }
    
    [Serializable]
    public class IntReference : ValueReference<int, IntAsset>
    { }
    
    [Serializable]
    public class Vector2Reference : ValueReference<Vector2, Vector2Asset>
    { }
    
    [Serializable]
    public class Vector3Reference : ValueReference<Vector3, Vector3Asset>
    { }
    
    [Serializable]
    public class ColorReference : ValueReference<Color, ColorAsset>
    { }

    [Serializable]
    public class LayerMaskReference : ValueReference<LayerMask, LayerMaskAsset>
    {
        public static implicit operator int(LayerMaskReference valueRef)
        {
            return valueRef.value;
        }
    }
    
}