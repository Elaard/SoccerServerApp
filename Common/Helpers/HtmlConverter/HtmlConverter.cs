using Common.CustomNaming;
using Common.Helpers;
using Common.Mapping;
using Common.ModelsDTO.HtmlTables;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Helpers.Scr;
using System.Threading.Tasks;

namespace Common.Helpers.HtmlConverter
{
    public abstract class HtmlConverter
    {
        protected HtmlDocument _htmlDoc;
        public Scraper _scraper;


        public HtmlConverter()
        {
            _htmlDoc = new HtmlDocument();
            _scraper = new Scraper();
        }

        public void LoadHtml(string html)
        {
            if (CheckIfHtmlIsNotEmpty(html))
                _htmlDoc.LoadHtml(html);
        }
        public async Task HandleHtmlAsync(string url)
        {
            _scraper.SetUrl(url);
            LoadHtml(await _scraper.GetHtmlAsync());
        }
        protected bool CheckIfHtmlIsCorrect()
        {
            if (_htmlDoc == null)
                throw new InvalidOperationException(ApiException.IncorrectHtml);
            return true;
        }
        protected bool CheckIfHtmlIsNotEmpty(string html)
        {
            if (String.IsNullOrEmpty(html))
                throw new InvalidOperationException(ApiException.IncorrectHtml);
            return true;
        }
    }
}
