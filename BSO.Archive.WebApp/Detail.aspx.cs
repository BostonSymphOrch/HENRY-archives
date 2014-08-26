using Bso.Archive.BusObj;
using Bso.Archive.BusObj.Utility;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace BSO.Archive.WebApp
{
    public partial class Results : System.Web.UI.Page
    {

        Event currentEvent;
        Event CurrentEvent
        {
            get
            {
                return currentEvent ?? (currentEvent = Event.GetEventByID(GetCurrentID()));
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ProgramListView.DataSource = CurrentEvent.EventWorks.Where(w => !Work.WorkShouldBeExcludedById(w.WorkID));
                ProgramListView.DataBind();

                SetEventInformation();
            }
        }

        protected int GetCurrentID()
        {
            string passedID = Request.QueryString["UniqueKey"].ToString();
            int id;
            int.TryParse(passedID, out id);

            return id;
        }

        private void SetEventInformation()
        {
            int id = GetCurrentID();

            using (BsoArchiveEntities context = BsoArchiveEntities.Current)
            {
                Event evt = context.Events.SingleOrDefault(et => et.EventID == id);

                var conductorFullName = CurrentEvent.Conductor.ConductorFullName;
                var orchestraName = CurrentEvent.Orchestra.OrchestraName;
                var seasonName = CurrentEvent.Season.SeasonName;
                var eventDate = CurrentEvent.EventDate;
                var eventTime = CurrentEvent.EventStart;
                var eventTitle = evt.EventProgramTitle;
                var eventProjectName = CurrentEvent.Project.ProjectName;
                var eventTypeName = CurrentEvent.EventType.TypeName;
                var eventProgramNumber = CurrentEvent.EventProgramNumber;
                var eventNote = CurrentEvent.EventNote;
                var venue = CurrentEvent.Venue;
                var eventSeries = CurrentEvent.EventSeries;

                if (eventProgramNumber != 0)
                {
                    programBookLink.HRef = String.Concat(SettingsHelper.ContentDMProgramLink, eventProgramNumber);
                }
                else
                {
                    programBookLink.Visible = false;
                    programBookLink.Disabled = true;
                    programBookLink.Attributes.Add("enabled", "false");
                    programBookLink.Attributes.Add("class", "disabledPdf");
                    programBookImage.Src = "/images/pdf_grey.png";
                }

                ConductorFullName.Text = conductorFullName;
                ConductorFullName.NavigateUrl = string.Format(ConductorFullName.NavigateUrl, conductorFullName);

                OrchestraName.Text = orchestraName;
                OrchestraName.NavigateUrl = string.Format(OrchestraName.NavigateUrl, orchestraName);

                SeasonName.Text = seasonName;
                SeasonName.NavigateUrl = string.Format(SeasonName.NavigateUrl, seasonName);

                EventDate.Text = eventDate.ToShortDateString();
                EventTime.Text = eventTime;
                EventNote.Text = eventNote;

                //EventTitle.Text = currentEvent.EventProgramTitle;
                //EventDate.NavigateUrl = string.Format(EventDate.NavigateUrl, eventDate.ToShortDateString(), eventDate.ToShortDateString());
                //EventTitle.NavigateUrl = string.Format(EventTitle.NavigateUrl, EventTitle.Text);

                var series = eventSeries.Split(";".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                List<string> listOfLinks = new List<string>();
                //System.Text.StringBuilder listOfLinks = new System.Text.StringBuilder();
                foreach (var singleSeries in series)
                {
                    String link = String.Format("<a href={0}/Search.aspx?searchType=Performance&EventTitle={1} >{2}</a>", SettingsHelper.SiteURL, singleSeries.Replace(" ", "%20"), singleSeries);
                    listOfLinks.Add(link);
                }

                var resultString = String.Join("; ", listOfLinks.ToArray());
                EventTitles.Text = resultString;

                VenueName.Text = venue.VenueName;
                VenueName.NavigateUrl = string.Format(VenueName.NavigateUrl, venue.VenueName);

                VenueLocation.Text = venue.Location;
                VenueLocation.NavigateUrl = string.Format(VenueLocation.NavigateUrl, venue.VenueCity, venue.VenueState, venue.VenueCountry);
            }
        }

        protected void ProgramListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var eventWork = (EventWork)e.Item.DataItem;
            var workTitleControl = (HyperLink)e.Item.FindControl("WorkTitle");
            var composerNameControl = (HyperLink)e.Item.FindControl("ComposerFullName");
            var artistNameControl = (HyperLink)e.Item.FindControl("ArtistFullName");
            var artistRoleControl = (HyperLink)e.Item.FindControl("Role");

            var work = eventWork.Work;

            if (Work.WorkShouldBeExcludedByGroupId(work.WorkGroupID))
                return;

            var workComposer = work.Composer;
            var workArtist = work.WorkArtists.FirstOrDefault(a => a.WorkID == work.WorkID);

            if (workArtist != null)
            {
                var workArtists = work.WorkArtists.Where(a => a.WorkID == work.WorkID);

                if (workArtists.Count() != 0)
                {
                    var programArtistListControl = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("programArtistList");
                    var programRoleListControl = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("programRoleList");
                    foreach (var artist in workArtists)
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl listArtistItem = new System.Web.UI.HtmlControls.HtmlGenericControl("li");
                        HyperLink artistName = new HyperLink();
                        artistName.Text = String.Concat(artist.Artist.ArtistLastName, ", ", artist.Artist.ArtistFirstName);
                        artistName.NavigateUrl = string.Format(artistNameControl.NavigateUrl, artist.Artist.ArtistFullName);
                        listArtistItem.Controls.Add(artistName);
                        programArtistListControl.Controls.Add(listArtistItem);
                        
                        if (workArtist.Instrument != null)
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl listRoleItem = new System.Web.UI.HtmlControls.HtmlGenericControl("li");
                            HyperLink artistRole = new HyperLink();

                            artistRole.Text = artist.Instrument.Instrument1;
                            artistRole.NavigateUrl = string.Format(artistRoleControl.NavigateUrl, artist.Instrument.Instrument1);

                            listRoleItem.Controls.Add(artistRole);
                            programRoleListControl.Controls.Add(listRoleItem);
                        }
                    }
                }
            }
            if (work != null)
            {
                workTitleControl.Text = work.WorkTitle;
                workTitleControl.NavigateUrl = string.Format(workTitleControl.NavigateUrl, work.WorkTitle, workComposer.FullName);

                var workNotesControl = (Label)e.Item.FindControl("WorkNotes");
                var workNotesLabel = (Label)e.Item.FindControl("WorkNotesLabel");
                if (!String.IsNullOrEmpty(work.WorkNote))
                {
                    workNotesLabel.Visible = true;
                    workNotesControl.Text = work.WorkNote;
                }
                else
                {
                    workNotesLabel.Visible = false;
                    workNotesControl.Visible = false;
                }

                var workPremiereControl = (Label)e.Item.FindControl("WorkPremiere");
                var workPremiereLabel = (Label)e.Item.FindControl("WorkPremiereLabel");
                if (!String.IsNullOrEmpty(eventWork.WorkPremiere))
                {
                    workPremiereControl.Text = eventWork.WorkPremiere;
                    workPremiereLabel.Visible = true;
                }
                else
                {
                    workPremiereControl.Visible = false;
                    workPremiereLabel.Visible = false;
                }

            }
            if (workComposer != null)
            {
                composerNameControl.Text = workComposer.ComposerFullName;
                composerNameControl.NavigateUrl = string.Format(composerNameControl.NavigateUrl, workComposer.FullName);
            }
        }
    }
}