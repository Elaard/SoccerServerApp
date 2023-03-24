using Common.CustomNaming;
using Common.Mapping;
using Common.ModelsDTO.HtmlTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Helpers.HtmlConverter
{
    public class MainDataConverter:HtmlConverter
    {
        public List<MatchDTO> GetMatchSchedule()
        {
            CheckIfHtmlIsCorrect();

            try
            {
                List<List<string>> table = _htmlDoc.DocumentNode.SelectNodes(TableElements.TableClassMain)
                .Descendants(TableElements.Tr)
                .Where(tr => tr.Elements(TableElements.Td).Count() > 1)
                .Select(tr => tr.Elements(TableElements.Td).Select(td => td.InnerText.Trim()).ToList())
                .ToList();

                return MapTableToObject.MapFromTableMainClass2(table);
            }

            catch (NullReferenceException ex)
            {
                throw new InvalidOperationException(ex.Message + ": " + ApiException.IncorrectHtml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public TeamDTO GetTeamStatistics()
        {
            CheckIfHtmlIsCorrect();

            try
            {
                List<List<string>> table = _htmlDoc.DocumentNode.SelectSingleNode(TableElements.TableClassMain2)
                .Descendants(TableElements.Tr)
                .Skip(4)
                .Where(tr => tr.Elements(TableElements.Td).Count() > 1)
                .Select(tr => tr.Elements(TableElements.Td).Select(td => td.InnerText.Trim()).ToList())
                .ToList();

                var teamStats = MapTableToObject.MapFromTableMainClass(table);

                return new TeamDTO { Stats = teamStats };
            }
            catch (NullReferenceException ex)
            {
                throw new InvalidOperationException(ex.Message + ": " + ApiException.IncorrectHtml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public LeagueDTO GetLeague()
        {
            try
            {
                var leagueTitle = _htmlDoc.DocumentNode.SelectSingleNode(TableElements.Title).InnerText.ToString();

                return new LeagueDTO { Title = leagueTitle };
            }

            catch (NullReferenceException ex)
            {
                throw new InvalidOperationException(ex.Message + ": " + ApiException.IncorrectHtml);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
