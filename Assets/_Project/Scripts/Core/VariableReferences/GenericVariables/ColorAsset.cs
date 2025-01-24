using System;
using UnityEngine;

namespace MonkeyBusiness.Core.VariableReferences.GenericVariables
{
    [Serializable, CreateAssetMenu(fileName = "NewColorAsset", menuName = "BFB/Variables/Color")]
    public class ColorAsset : ValueAsset<Color>
    {
    }
}
