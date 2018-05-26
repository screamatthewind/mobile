using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace HttpClientAndroid1
{
	public class NetHttp
	{
		MainActivity ad;

		public NetHttp (MainActivity ad)
		{
			this.ad = ad;
		}

		public async Task HttpSample (HttpMessageHandler handler = null)
		{
			System.Net.Http.HttpClient client = (handler == null) ?
				new System.Net.Http.HttpClient () :
					new System.Net.Http.HttpClient (handler);
			var stream = await client.GetStreamAsync (MainActivity.WisdomUrl);
			ad.RenderStream (stream);
		}
	}
}