using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace BSO.Archive.WebApp.Classes
{
    public class BaseUserControl : UserControl
    {
        protected PageClass CurrentPage
        {
            get { return this.Page as PageClass; }
        }

        public ICacheManager CacheManager { get { return CacheFactory.GetCacheManager(); } }

        #region Query String Values
        protected bool IsPerformanceSearch
        {
            get
            {
                return
                    String.Compare(Request.QueryString["SearchType"], "Performance",
                                   StringComparison.InvariantCultureIgnoreCase) == 0;
            }
        }

        protected bool IsArtistSearch
        {
            get
            {
                return
                    String.Compare(Request.QueryString["SearchType"], "Artist",
                                 StringComparison.InvariantCultureIgnoreCase) == 0;
            }
        }

        protected bool IsRepertoireSearch
        {
            get
            {
                return
                    String.Compare(Request.QueryString["SearchType"], "Repertoire",
                                   StringComparison.InvariantCultureIgnoreCase) == 0;
            }
        }

        protected string ComposerValue
        {
            get
            {
                string composer = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Composer"]))
                    composer = Request.QueryString["Composer"];

                return composer;
            }
        }

        protected string WorkValue
        {
            get
            {
                string work = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Work"]))
                    work = Request.QueryString["Work"];

                return work;
            }
        }

        protected string OrchestraValue
        {
            get
            {
                string orchestra = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Orchestra"]))
                    orchestra = Request.QueryString["Orchestra"];

                return orchestra;
            }
        }

        protected string SeasonValue
        {
            get
            {
                string season = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Season"]))
                    season = Request.QueryString["Season"];

                return season;
            }
        }

        protected string ConductorValue
        {
            get
            {
                string conductor = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Conductor"]))
                    conductor = Request.QueryString["Conductor"];

                return conductor;
            }
        }

        protected string SoloistValue
        {
            get
            {
                string soloist = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Soloist"]))
                    soloist = Request.QueryString["Soloist"];

                return soloist;
            }
        }

        protected string EnsembleValue
        {
            get
            {
                string soloist = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Ensemble"]))
                    soloist = Request.QueryString["Ensemble"];

                return soloist;
            }
        }

        protected string InstrumentValue
        {
            get
            {
                string instrument = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Instrument"]))
                    instrument = Request.QueryString["Instrument"];

                return instrument;
            }
        }

        protected string ParticipantValue
        {
            get
            {
                string participant = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Participant"]))
                    participant = Request.QueryString["Participant"];

                return participant;
            }
        }

        protected string ArrangerValue
        {
            get
            {
                string arranger = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Arranger"]))
                    arranger = Request.QueryString["Arranger"];

                return arranger;
            }
        }

        protected string EventTitleValue
        {
            get
            {
                string eventTitle = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["EventTitle"]))
                    eventTitle = Request.QueryString["EventTitle"];

                return eventTitle;
            }
        }

        protected string VenueNameValue
        {
            get
            {
                string venueName = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Venue"]))
                    venueName = Request.QueryString["Venue"];

                return venueName;
            }
        }

        protected string VenueCityValue
        {
            get
            {
                string venueCity = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["City"]))
                    venueCity = Request.QueryString["City"];

                return venueCity;
            }
        }

        protected string VenueStateValue
        {
            get
            {
                string venueState = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["State"]))
                    venueState = Request.QueryString["State"];

                return venueState;
            }
        }

        protected string VenueCountryValue
        {
            get
            {
                string venueCountry = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Country"]))
                    venueCountry = Request.QueryString["Country"];

                return venueCountry;
            }
        }

        protected string WorkPremiereValue
        {
            get
            {
                string workPremiere = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Premiere"]))
                    workPremiere = Request.QueryString["Premiere"];

                return workPremiere;
            }
        }

        protected string WorkCommissionValue
        {
            get
            {
                string workCommission = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["Commission"]))
                    workCommission = Request.QueryString["Commission"];

                return workCommission;
            }
        }

        protected string StartTimeValue
        {
            get
            {
                string startTime = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["startTime"]))
                    startTime = Request.QueryString["startTime"];

                return startTime;
            }
        }

        protected string EndTimeValue
        {
            get
            {
                string endTime = String.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["endTime"]))
                    endTime = Request.QueryString["endTime"];

                return endTime;
            }
        }

        protected bool HasEndTime
        {
            get { return !String.IsNullOrEmpty(EndTimeValue); }
        }

        protected bool HasStartTime
        {
            get { return !String.IsNullOrEmpty(StartTimeValue); }
        }

        protected bool HasWorkCommission
        {
            get { return !String.IsNullOrEmpty(WorkCommissionValue); }
        }

        protected bool HasWorkPremiere
        {
            get { return !String.IsNullOrEmpty(WorkPremiereValue); }
        }

        protected bool HasVenueCountry
        {
            get { return !String.IsNullOrEmpty(VenueCountryValue); }
        }

        protected bool HasVenueState
        {
            get { return !String.IsNullOrEmpty(VenueStateValue); }
        }

        protected bool HasVenueCity
        {
            get { return !String.IsNullOrEmpty(VenueCityValue); }
        }

        protected bool HasVenueName
        {
            get { return !String.IsNullOrEmpty(VenueNameValue); }
        }

        protected bool HasEventTitle
        {
            get { return !String.IsNullOrEmpty(EventTitleValue); }
        }

        protected bool HasArranger
        {
            get { return !String.IsNullOrEmpty(ArrangerValue); }
        }

        protected bool HasParticipant
        {
            get { return !String.IsNullOrEmpty(ParticipantValue); }
        }

        protected bool HasEnsemble
        {
            get { return !String.IsNullOrEmpty(EnsembleValue); }
        }

        protected bool HasInstrument
        {
            get { return !String.IsNullOrEmpty(InstrumentValue); }
        }

        protected bool HasSoloist
        {
            get { return !String.IsNullOrEmpty(SoloistValue); }
        }

        protected bool HasConductor
        {
            get { return !String.IsNullOrEmpty(ConductorValue); }
        }

        protected bool HasSeason
        {
            get { return !String.IsNullOrEmpty(SeasonValue); }
        }

        protected bool HasOrchestra
        {
            get { return !String.IsNullOrEmpty(OrchestraValue); }
        }

        protected bool HasWork
        {
            get { return !String.IsNullOrEmpty(WorkValue); }
        }

        protected bool HasComposer
        {
            get { return !String.IsNullOrEmpty(ComposerValue); }
        }
        #endregion


        protected bool CheckEmptySearch(Panel panel)
        {
            bool searchOnCommission = true;
            bool searchOnPremiere = true;//string.IsNullOrEmpty(premiereControl.SelectedValue.Trim());

            var commissionControl = (DropDownList)panel.FindControl("WorkCommissions");
            if (commissionControl != null) searchOnCommission = string.IsNullOrEmpty(commissionControl.SelectedValue.Trim());

            var premiereControl = (DropDownList)panel.FindControl("WorkPremiere");
            if (premiereControl != null) searchOnPremiere = string.IsNullOrEmpty(premiereControl.SelectedValue.Trim());

            return panel.Controls.OfType<TextBox>().All(input => String.IsNullOrEmpty(input.Text.Trim())) && searchOnCommission && searchOnPremiere;
        }
    }
}