using Common.CustomNaming;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers.Scr
{
    public class Scraper
    { 
        private string _url;

        public void SetUrl(string url)
        {
            if (CheckIfUrlIsCorrect(url))
            {
                _url = String.Empty;
                _url = url;
            }
                
        }
        public async Task<string> GetHtmlAsync()
        {
            string response = String.Empty;

            if (CheckIfUrlIsCorrect(_url))
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);

                response = await client.GetStringAsync(_url);
            }

            return response;
        }
        private bool CheckIfUrlIsCorrect(string url)
        {
            if (String.IsNullOrEmpty(url))
                throw new InvalidOperationException(ApiException.IncorrectUrl);
            return true;
        }


    }
}
