using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpClient1
{
    public class GetData
    {
        public async Task GetDataAsync()
        {
            var baseUrl = "https://www.numberhelper.com";

            var accessToken = "vxLUr5tm0oJX_9ASmeq7bnNJvgS68lAJlNsaK_WIpC-6tjnQMrWBga3POHtZ2LvTsxOdY7xxp9UGRr58kLld5Jg3eXxClzxPj49NQza4xJtBMxWqE2HDCcno4T_4lV6_k21aXRzYDO_8gZa7uZsSFSqrUDjvQZEkehD1WWUSXAaEIbYFzEhYvGSWeknvw4WnzSwuezZqLxGuEtGBQaPyqg9j774xTis3rw4t1LTQXNjuUkfyXwgDzEncUSQzUISj5HbTtB9O2Y-ZJKPb39bR_fWUmItR8zct6YlfDFJ5imW2yQnbJiNogzCRFJbZUd9LUFfPn7oQvAoCV9790PrhH0WJS2JDs54UtdZEeBMMxcm4qhICeQMTzRu6rYBxdGCJVVaSA0R6ZOHs8Mt6mU-J6tn_ZKLhkVX8JmvquB_dJpV0gzfjaUp0tBYvY7IPtillI8_14kaDCnDlUOflmi1iMUDiuESXigqHtRODuWgCLyNZyFIf";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = null;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                try
                {
                    response = await client.GetAsync(baseUrl);
                }
                catch (Exception es)
                {
                    int i = 1;
                }


                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                // object responseData = JsonConvert.DeserializeObject(jsonString);
                int b = 1;
            }
        }
    }
}
