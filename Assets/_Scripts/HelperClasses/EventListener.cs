using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelperClasses {
	/// <summary>
	/// This script is used to subscribe to an event
	/// The call back method is passed to the constructor
	/// </summary>
	public class EventListener {
		public EventCallback callback;
		public delegate void EventCallback(int eventCode, object data);

		public EventListener(EventCallback callback) {
			this.callback = callback;
		}
	}
}
