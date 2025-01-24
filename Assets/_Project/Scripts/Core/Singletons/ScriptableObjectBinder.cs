using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFB.Core.Singletons
{
    public class ScriptableObjectBinder : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private ScriptableObject[] scriptableObjects;

        #endregion
    }
}
