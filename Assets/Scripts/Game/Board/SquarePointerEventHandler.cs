using System.Collections.Generic;
using System.Diagnostics;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NeoC.Game.Board
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class SquarePointerEventHandler : ObservableTriggerBase, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
    {
        [SerializeField] private Collider _collider;

        private Dictionary<EventTriggerType, Subject<PointerEventData>> eventSubjects;
        private Dictionary<EventTriggerType, IObservable<PointerEventData>> eventAsObservables;

        public void EnableCollider(bool selectable)
        {
            _collider.enabled = selectable;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerEvent(EventTriggerType.PointerClick, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerEvent(EventTriggerType.PointerDown, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerEvent(EventTriggerType.PointerExit, eventData);
        }

        private void OnPointerEvent(EventTriggerType eventTriggerType, PointerEventData eventData)
        {
            Subject<PointerEventData> subject;
            if (eventSubjects != null && eventSubjects.TryGetValue(eventTriggerType, out subject))
            {
                if (subject != null)
                {
                    subject.OnNext(eventData);
                }
            }
        }

        public IObservable<PointerEventData> OnClickAsObservable()
        {
            return OnPointerEventAsObservable(EventTriggerType.PointerClick);
        }

        public IObservable<PointerEventData> OnDownAsObservable()
        {
            return OnPointerEventAsObservable(EventTriggerType.PointerDown);
        }

        public IObservable<PointerEventData> OnExitAsObservable()
        {
            return OnPointerEventAsObservable(EventTriggerType.PointerExit);
        }

        private IObservable<PointerEventData> OnPointerEventAsObservable(EventTriggerType eventTriggerType)
        {
            if (eventAsObservables == null)
            {
                eventAsObservables = new Dictionary<EventTriggerType, IObservable<PointerEventData>>();
            }

            IObservable<PointerEventData> observable;
            if (eventAsObservables.TryGetValue(eventTriggerType, out observable))
            {
                if (observable != null)
                {
                    return observable;
                }
            }

            var subject = new Subject<PointerEventData>();
            observable = subject.AsObservable();

            if (eventSubjects == null)
            {
                eventSubjects = new Dictionary<EventTriggerType, Subject<PointerEventData>>();
            }

            eventSubjects.Add(eventTriggerType, subject);
            eventAsObservables.Add(eventTriggerType, observable);

            return observable;
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (eventSubjects == null)
            {
                return;
            }

            foreach (var subject in eventSubjects.Values)
            {
                if (subject != null)
                {
                    subject.OnCompleted();
                }
            }
        }

        [Conditional("UNITY_EDITOR")]
        void OnEnable()
        {
            _collider = GetComponent<Collider>();
        }
    }
}
