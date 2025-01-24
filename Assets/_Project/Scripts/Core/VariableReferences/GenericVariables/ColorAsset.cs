using System;
using UnityEngine;

namespace BFB.Core.VariableReferences
{
    [Serializable, CreateAssetMenu(fileName = "NewColorAsset", menuName = "BFB/Variables/Color")]
    public class ColorAsset : ValueAsset<Color>
    {
    }
}
