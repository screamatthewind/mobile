using System;
using Xamarin.Forms;

namespace App1
{
	public class HybridWebView : View
	{
		Action<string> action;

		public static readonly BindableProperty UriProperty = BindableProperty.Create (
			propertyName: "Uri",
			returnType: typeof(string),
			declaringType: typeof(HybridWebView),
			defaultValue: default(string),
            propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var control = bindable as Label;
                    var changingFrom = oldValue as string;
                    var changingTo = newValue as string;
                }
        );            

		public static readonly BindableProperty TokenProperty = BindableProperty.Create (
			propertyName: "Token",
			returnType: typeof(string),
			declaringType: typeof(HybridWebView),
			defaultValue: default(string),
            propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var control = bindable as Label;
                    var changingFrom = oldValue as string;
                    var changingTo = newValue as string;
                }
        );            

		public string Uri {
			get { return (string)GetValue (UriProperty); }
			set { SetValue (UriProperty, value); }
		}

        public string Token {
			get { return (string)GetValue (TokenProperty); }
			set { SetValue (TokenProperty, value); }
		}

        public void RegisterAction (Action<string> callback)
		{
			action = callback;
		}

		public void Cleanup ()
		{
			action = null;
		}

		public void InvokeAction (string data)
		{
			if (action == null || data == null) {
				return;
			}

			action.Invoke (data);
		}
	}
}
