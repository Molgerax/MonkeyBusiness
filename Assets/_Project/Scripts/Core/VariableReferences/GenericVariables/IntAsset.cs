using System;
using UnityEngine;

namespace MonkeyBusiness.Core.VariableReferences.GenericVariables
{
    [Serializable, CreateAssetMenu(fileName = "Int", menuName = "BFB/Variables/Int")]
    public class IntAsset : ValueAsset<int>
    {
    }
}
