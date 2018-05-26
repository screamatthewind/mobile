using System.ComponentModel;
using Android.Webkit;
using App1;
using App1.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Collections.Generic;

[assembly: ExportRenderer (typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace App1.Droid
{
	public class HybridWebViewRenderer : ViewRenderer<HybridWebView, Android.Webkit.WebView>
	{
		const string JavaScriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";

		protected override void OnElementChanged (ElementChangedEventArgs<HybridWebView> e)
		{
			base.OnElementChanged (e);
            var model = e.NewElement;
            if (model == null) {
                return;
            }

			if (Control == null) {
				var webView = new Android.Webkit.WebView (Forms.Context);
				webView.Settings.JavaScriptEnabled = true;
				SetNativeControl (webView);

            }
			if (e.OldElement != null) {
				Control.RemoveJavascriptInterface ("jsBridge");
				var hybridWebView = e.OldElement as HybridWebView;
				hybridWebView.Cleanup ();

                model.PropertyChanged -= OnElementPropertyChanged;
            }
			if (e.NewElement != null) {
                model.PropertyChanged += OnElementPropertyChanged;

			    Control.AddJavascriptInterface (new JSBridge (this), "jsBridge");
                Control.LoadUrl(Element.Uri);
                InjectJS(JavaScriptFunction);
			}
		}

        string token;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var element = sender as HybridWebView;

            if (e.PropertyName.Equals(HybridWebView.TokenProperty.PropertyName))
            {
                this.token = element.Token;
            }

            if (e.PropertyName.Equals(HybridWebView.UriProperty.PropertyName))
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", "Bearer " + token);

                Control.LoadUrl(element.Uri, headers);
                InjectJS(JavaScriptFunction);
            }
        }

        void InjectJS (string script)
		{
			if (Control != null) {
				Control.LoadUrl (string.Format ("javascript: {0}", script));
			}
		}
	}
}
