using Common.CustomNaming;
using Common.Mapping;
using Common.ModelsDTO.HtmlTables;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers.HtmlConverter
{
    public class TeamMetaConverter: HtmlConverter
    {
        public async Task<List<MetaTeamDTO>> GetMetaTeamData()
        {
            CheckIfHtmlIsCorrect();

            string potentialWrongUrl = "";
            try
            {
                 List<List<HtmlNode>> table = _htmlDoc.DocumentNode.SelectSingleNode(TableElements.TableClassMain2)
                 .Descendants(TableElements.Tr)
                 .Skip(4)
                 .Where(tr => tr.Elements(TableElements.Td).Count() > 1)
                 .Select(tr => tr.Elements(TableElements.Td).ToList())
                 .ToList();

                List<MetaTeamDTO> meta = new List<MetaTeamDTO>();

                List<string> urls = new List<string>();

                foreach (var r in table)
                {
                    urls.Add(FormatUrl(r[1].InnerHtml));
                }
                foreach(var url in urls)
                {
                    var metaTeam = await Get(url);
                    metaTeam.Url = url;
                    meta.Add(metaTeam);
                }
                return meta;
            }

            catch (NullReferenceException ex)
            {
                throw new InvalidOperationException(ex.Message + ": " + ApiException.IncorrectHtml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + potentialWrongUrl);
            }
        }
        private async Task<MetaTeamDTO> Get(string teamUrl)
        {
            await HandleHtmlAsync(teamUrl);

            List<List<string>> table = _htmlDoc.DocumentNode.SelectNodes(TableElements.TableClassMain)
               .Descendants(TableElements.Tr)
               .Skip(1)
               .Where(tr => tr.Elements(TableElements.Td).Count() > 0)
               .Select(tr => tr.Elements(TableElements.Td).Select(td => td.InnerText.Trim()).ToList())
               .ToList();

            HtmlNode photoNode = _htmlDoc.DocumentNode.SelectNodes(TableElements.TableClassMain)
               .Descendants(TableElements.Tr)
               .Where(tr => tr.Elements(TableElements.Td).Count() > 0)
               .Select(tr => tr.Elements(TableElements.Td).FirstOrDefault())
               .FirstOrDefault();

            var metaTeam = MapMetaTeamToObject.Map(table);
            
            metaTeam.Photo= photoNode.InnerHtml;
            return metaTeam;

        }
        private string FormatUrl(string name)
        {
            return Urls.NinetyMinutes + StringMappingHelper.GetStringBetweenQuatationonMark(name);
        }
    }
}
