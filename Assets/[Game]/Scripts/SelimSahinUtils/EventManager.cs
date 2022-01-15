using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SelimSahinUtils
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, UnityEvent> eventDictionary;
        private Dictionary<string, TypedEvent> typedEventDictionary;

        private static EventManager instance;
        private static EventManager Instance {
            get {
                if (instance == null)
                {
                    GameObject obj = new GameObject("EventManager");
                    instance = obj.AddComponent<EventManager>();
                    Init();
                }

                return instance;
            }
        }

        private static void Init()
        {
            if (Instance.eventDictionary == null)
            {
                Instance.eventDictionary = new Dictionary<string, UnityEvent>();
                Instance.typedEventDictionary = new Dictionary<string, TypedEvent>();
            }
        }

        public static void AddListener(string eventName, UnityAction listener)
        {

            if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                AddNewEventToDictionary(eventName, listener);
            }
        }

        public static void AddListener(string eventName, UnityAction<object> listener)
        {
            if (Instance.typedEventDictionary.TryGetValue(eventName, out TypedEvent thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                AddNewEventToDictionary(eventName, listener);
            }
        }

        public static void RemoveListener(string eventName, UnityAction listener)
        {
            if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void RemoveListener(string eventName, UnityAction<object> listener)
        {
            if (Instance.typedEventDictionary.TryGetValue(eventName, out TypedEvent thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void NotifyListeners(string eventName)
        {
            if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.Invoke();
            }
            else
            {
                Debug.LogWarning("Couldn't find the requested event: \"" + eventName + "\"");
            }
        }

        public static void NotifyListeners(string eventName, object data)
        {
            if (Instance.typedEventDictionary.TryGetValue(eventName, out TypedEvent thisEvent))
            {
                thisEvent.Invoke(data);
            }
            else
            {
                Debug.LogWarning("Couldn't find the requested event: \"" + eventName + "\"");
            }
        }

        private static void AddNewEventToDictionary(string eventName, UnityAction listener)
        {
            UnityEvent newEvent = new UnityEvent();
            newEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, newEvent);
        }

        private static void AddNewEventToDictionary(string eventName, UnityAction<object> listener)
        {
            TypedEvent newEvent = new TypedEvent();
            newEvent.AddListener(listener);
            Instance.typedEventDictionary.Add(eventName, newEvent);
        }
    }

    public class TypedEvent : UnityEvent<object> { }
}
