using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.GData.Spreadsheets;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using Google.GData.Client;
using WindowsInput;
using System.Xml.Serialization;
using System.IO;

namespace HSDeckImporter
{
    public partial class Form1 : Form
    {
        const int SEARCHBOX_X = 17000;
        const int SEARCHBOX_Y = 60000;
        const int CARD_X = 6500;
        const int CARD_Y = 17000;
        const string GOOGLE_CLIENT_ID = "13053879592-sqqe3ui6uusfmkt0q8hthpisob243rs9.apps.googleusercontent.com";
        const string GOOGLE_CLIENT_SECRET = "09ciowTKxl_YlmCiAY6jMCi7";
        //const string DECK_SPREADSHEET_ID = "1estKkHfw7lhn7B9mz5GCjullt4186qMVD869FQIcWK8";
        const string GOOGLE_SCOPE = "https://spreadsheets.google.com/feeds https://docs.google.com/feeds";
        const string GOOGLE_REDIRECT_URI = "urn:ietf:wg:oauth:2.0:oob";
        //const string GOOGLE_ACCESS_CODE = "4/Qe_R7fNu52iBeujxDDWJp3bKw9O8T3guWfwU4SgmVq0";
        //const string GOOGLE_REFRESH_TOKEN = "1/pHqhYqSZ57eWmU3bw6m08WI7qJVZ6zvnYDiIzWGGciRIgOrJDtdun6zK6XiATCKT";
        const string GOOGLE_PARAMETER_FILE = "parameters.xml";
        const string SPREADSHEET_NAME = "John's Decks";
        const string GOOGLE_APPLICATION_NAME = "HSDeckImporter";
        const string EXTERNAL_PROCESS_NAME = "Hearthstone";
        const int MOUSE_MOVE_DELAY = 50;
        const int MOUSE_CLICK_DELAY = 50;
        const int SEARCH_DELAY = 250;
        const int CARD_ADD_DELAY = 250;

        OAuth2Parameters parameters = new OAuth2Parameters();

        public Form1()
        {
            InitializeComponent();


            if (System.IO.File.Exists(GOOGLE_PARAMETER_FILE))
            {
                bImportDeck.Visible = true;
                cboDecks.Visible = true;

                parameters = GoogleDrive.LoadParameters(GOOGLE_PARAMETER_FILE);

                LoadDeckList(parameters);
            }
            else
            {
                string authorizationUrl = GoogleDrive.GetAuthorizationUrl(parameters, GOOGLE_CLIENT_ID, GOOGLE_CLIENT_SECRET, GOOGLE_REDIRECT_URI, GOOGLE_SCOPE);

                tbGoogleAccessCode.Visible = true;
                lbAccessCode.Visible = true;
                bAuthorize.Visible = true;

                Process.Start(authorizationUrl);
            }
        }

        private void LoadDeckList(OAuth2Parameters parameters)
        {
            List<string> deckList = DeckSpreadsheet.GetDeckList(parameters, GOOGLE_APPLICATION_NAME, SPREADSHEET_NAME);

            foreach (string deck in deckList)
            {
                cboDecks.Items.Add(deck);
            }
            cboDecks.SelectedIndex = 0;
        }

        private void bImportDeck_Click(object sender, EventArgs e)
        {
            SpreadsheetsService service = GoogleDrive.GetSpreadsheetService(parameters, GOOGLE_APPLICATION_NAME);
            
            SpreadsheetQuery query = new SpreadsheetQuery();
            SpreadsheetFeed feed = service.Query(query);

            SpreadsheetEntry spreadsheet = (from entry in feed.Entries
                                            where entry.Title.Text == SPREADSHEET_NAME
                                            select entry).Cast<SpreadsheetEntry>().Single();

            WorksheetFeed wsFeed = spreadsheet.Worksheets;

            WorksheetEntry worksheet = (from entry in wsFeed.Entries
                                        where entry.Title.Text == cboDecks.Text
                                        select entry).Cast<WorksheetEntry>().Single();

            AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
            ListFeed listFeed = service.Query(listQuery);

            List<Card> cardList = new List<Card>();

            int rowCount = 1;
            foreach (ListEntry row in listFeed.Entries)
            {
                // first 3 rows are Title, Deck Author, and Headers
                // the listFeed automatically skips the first one, though
                if (rowCount < 3)
                {
                    rowCount++;
                    continue;
                }

                // Columns:
                // 0: Mana
                // 1: Name
                // 2: Qty
                // 3: Alt Mana
                // 4: Alt Name
                // 5: Alt Qty

                // blank row means we're done, just checking the Mana value
                if (row.Elements.Count == 0)
                    break;

                // card name
                string cardName = row.Elements[1].Value.ToString();
                
                // qty
                int cardQty = Convert.ToInt32(row.Elements[2].Value.ToString());

                // alt name
                if (row.Elements.Count > 3)
                {
                    string altName = row.Elements[4].Value.ToString();
                    if (!string.IsNullOrEmpty(altName))
                        cardName = altName;

                    if (row.Elements.Count > 4)
                    {
                        // alt qty
                        string altQty = row.Elements[5].Value.ToString();
                        if (!string.IsNullOrEmpty(altQty))
                            cardQty = Convert.ToInt32(altQty);
                    }
                }
                Card card = new Card();
                card.Name = cardName;
                card.Qty = cardQty;

                cardList.Add(card);

                rowCount++;
            }


            ProcessHelper.SetFocusToExternalApp(EXTERNAL_PROCESS_NAME);
            InputSimulator sim = new InputSimulator();

            foreach(Card card in cardList)
            {
                sim.Mouse.MoveMouseToPositionOnVirtualDesktop((double)SEARCHBOX_X, (double)SEARCHBOX_Y);
                Thread.Sleep(MOUSE_MOVE_DELAY);
                sim.Mouse.LeftButtonClick();
                Thread.Sleep(MOUSE_CLICK_DELAY);
                sim.Keyboard.TextEntry(card.Name);
                sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.RETURN);
                Thread.Sleep(SEARCH_DELAY);
                sim.Mouse.MoveMouseToPositionOnVirtualDesktop((double)CARD_X, (double)CARD_Y);
                for(int i=0; i < card.Qty ; i++)
                {
                    sim.Mouse.LeftButtonClick();
                    Thread.Sleep(CARD_ADD_DELAY);
                }
            }
        }

        private void bAuthorize_Click(object sender, EventArgs e)
        {
            GoogleDrive.CreateParameters(parameters, GOOGLE_PARAMETER_FILE, tbGoogleAccessCode.Text);

            bImportDeck.Visible = true;
            cboDecks.Visible = true;

            lbAccessCode.Visible = false;
            bAuthorize.Visible = false;
            tbGoogleAccessCode.Visible = false;

            LoadDeckList(parameters);
        }
    }
}
