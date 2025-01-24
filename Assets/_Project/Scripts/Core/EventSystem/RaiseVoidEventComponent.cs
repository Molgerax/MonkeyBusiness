using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFB.Core.Events
{
    public class RaiseVoidEventComponent : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private VoidEventSO[] voidEvents;

        #endregion


        #region Public Methods

        public void RaiseEventByIndex(int index)
        {
            if(voidEvents is null) return;

            int i = Mathf.Clamp(index, 0, voidEvents.Length - 1);
            
            if(voidEvents[i]) voidEvents[i].Raise();
        }

        #endregion
    }
}
