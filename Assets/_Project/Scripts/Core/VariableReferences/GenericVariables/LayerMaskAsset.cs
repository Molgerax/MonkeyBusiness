using System;
using UnityEngine;

namespace BFB.Core.VariableReferences
{
    [Serializable, CreateAssetMenu(fileName = "NewLayerMask", menuName = "BFB/Variables/LayerMask")]
    public class LayerMaskAsset : ValueAsset<LayerMask>
    {
    }
}
