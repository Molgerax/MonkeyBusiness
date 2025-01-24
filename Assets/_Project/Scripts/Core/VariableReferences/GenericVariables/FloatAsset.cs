using System;
using UnityEngine;

namespace BFB.Core.VariableReferences
{
    [Serializable, CreateAssetMenu(fileName = "Float", menuName = "BFB/Variables/Float")]
    public class FloatAsset : ValueAsset<float>
    {
    }
}
