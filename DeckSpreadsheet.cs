using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.GData.Client;
using Google.GData.Spreadsheets;

namespace HSDeckImporter
{
    public class DeckSpreadsheet
    {
        public static List<string> GetDeckList(OAuth2Parameters parameters, string applicationName, string spreadsheetName)
        {
            SpreadsheetsService service = GoogleDrive.GetSpreadsheetService(parameters, applicationName);

            SpreadsheetQuery query = new SpreadsheetQuery();
            SpreadsheetFeed feed = service.Query(query);

            SpreadsheetEntry spreadsheet = (from entry in feed.Entries
                                            where entry.Title.Text == spreadsheetName
                                            select entry).Cast<SpreadsheetEntry>().Single();

            WorksheetFeed wsFeed = spreadsheet.Worksheets;

            List<string> deckList = new List<string>();
            foreach (WorksheetEntry entry in wsFeed.Entries)
            {
                deckList.Add(entry.Title.Text);
            }

            deckList.Sort();

            return deckList;
        }
    }
}
