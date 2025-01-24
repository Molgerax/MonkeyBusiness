using System.Collections;
using MonkeyBusiness.Core.VariableReferences.GenericVariables;
using UltEvents;
using UnityEngine;

namespace MonkeyBusiness.Core.EventSystem
{
    public class TimerComponent : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private FloatReference timerDuration;
        [SerializeField] private UltEvent onTimerFinish;

        #endregion

        #region Private Fields

        private Coroutine _currentTimer;

        #endregion

        #region Public Methods

        public void SetTimer()
        {
            if(_currentTimer != null) StopCoroutine(_currentTimer);
            _currentTimer = StartCoroutine(TimerRoutine(timerDuration));
        }
        
        public void SetTimer(float duration)
        {
            if(_currentTimer != null) StopCoroutine(_currentTimer);
            _currentTimer = StartCoroutine(TimerRoutine(duration));
        }

        private IEnumerator TimerRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            onTimerFinish?.Invoke();
        }
        
        #endregion
    }
}
