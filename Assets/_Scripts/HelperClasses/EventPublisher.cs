using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelperClasses {
	/// <summary>
	/// Helper class for publishing events.
	/// Can be used to publish an event and all the subscribers will be notified of the event.
	/// </summary>
	public class EventPublisher  {

		private List<EventListener> listeners = new List<EventListener>();

		//Registering a listener
		public void Register(EventListener listener) {
			listeners.Add(listener);
		}

		public void DeRegister(EventListener listener) {
			listeners.Remove(listener);
		}

		//Publishes the event to all the subscribers.
		public void Publish(int eventCode, object data) {
			List<EventListener> tempListeners = new List<EventListener>(listeners);

			foreach(var listener in tempListeners) {
				listener.callback(eventCode, data);
			}
		}

		//Method overloading for enum objects
		public void Publish(object eventCode, object data) {
			Publish(eventCode.GetHashCode(), data);
		}

	}
}
