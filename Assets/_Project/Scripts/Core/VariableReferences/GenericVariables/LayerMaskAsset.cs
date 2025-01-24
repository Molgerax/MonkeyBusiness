using System;
using UnityEngine;

namespace MonkeyBusiness.Core.VariableReferences.GenericVariables
{
    [Serializable, CreateAssetMenu(fileName = "NewLayerMask", menuName = "BFB/Variables/LayerMask")]
    public class LayerMaskAsset : ValueAsset<LayerMask>
    {
    }
}
