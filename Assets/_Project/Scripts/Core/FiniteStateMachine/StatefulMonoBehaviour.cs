using System;
using UnityEngine;

namespace BFB.Core.FSM
{
    public class StatefulMonoBehaviour<T> : MonoBehaviour where T : StatefulMonoBehaviour<T>
    {
        protected FSMMono<T> fsmMono;
        
        public void ChangeState(FSMBaseMonoState<T> e) {
            fsmMono.ChangeState(e);
        }
        
        public void ChangeState(FSMBaseMonoState<T> e, bool ignoreSameState) {
            fsmMono.ChangeState(e, ignoreSameState);
        }

        protected virtual void Update() {
            fsmMono.Update(Time.deltaTime);
        }
    }
}
