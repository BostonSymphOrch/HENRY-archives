using Bso.Archive.BusObj.Interface;
using Bso.Archive.BusObj.Properties;
using log4net;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Bso.Archive.BusObj
{
    public partial class OPASUpdate
    {
        private IOPASData opasData;

        private enum Table
        { EVENT, ARTIST, EVENTTYPE, CONDUCTOR, ORCHESTRA, PARTICIPANT, PROJECT, SEASON, EVENTTYPEGROUP, VENUE, WORK, WORKARTIST }

        private static readonly ILog Log = LogManager.GetLogger(typeof(OPASUpdate));

        public void UpdateOPASData()
        {
            Log.Debug(string.Format("Started update processing of file: {0}", Settings.Default.XMLUpdate));
            XDocument loadDocument = XDocument.Load(Settings.Default.XMLUpdate);
            UpdateOPASData(loadDocument);
            Log.Debug(string.Format("Finished update processing of file: {0}", Settings.Default.XMLUpdate));
        }

        public void UpdateBatchOPASData()
        {
            foreach (var file in Directory.GetFiles(Settings.Default.XMLBatchFolder))
            {
                Log.Debug(string.Format("Started update processing of file: {0}", file));
                var loadDocument = XDocument.Load(file);
                UpdateOPASData(loadDocument);
                Log.Debug(string.Format("Finished update processing of file: {0}", file));
            }
        }

        /// <summary>
        /// Update OPAS Data
        /// </summary>
        public void UpdateOPASData(XDocument loadDocument)
        {
            var recordsToUpdate = BsoArchiveEntities.Current.OPASUpdates.Where(d => !d.HasBeenUpdated);
            var entitesToUpdate = recordsToUpdate.GroupBy(d => d.TableName);

            foreach (var entity in entitesToUpdate)
            {
                Log.Debug(string.Format("Started update processing of entity: {0}", entity.Key));
                string columnName = entity.FirstOrDefault().ColumnName;
                string tagName = entity.FirstOrDefault().TagName;
                switch ((Table)Enum.Parse(typeof(Table), entity.Key.ToUpper()))
                {
                    case Table.EVENT:
                        opasData = Event.NewEvent();
                        break;

                    case Table.ARTIST:
                        opasData = Artist.NewArtist();
                        break;

                    case Table.EVENTTYPE:
                        opasData = EventType.NewEventType();
                        break;

                    case Table.CONDUCTOR:
                        opasData = Conductor.NewConductor();
                        break;

                    case Table.ORCHESTRA:
                        opasData = Orchestra.NewOrchestra();
                        break;

                    case Table.PARTICIPANT:
                        opasData = Participant.NewParticipant();
                        break;

                    case Table.PROJECT:
                        opasData = Project.NewProject();
                        break;

                    case Table.SEASON:
                        opasData = Season.NewSeason();
                        break;

                    case Table.EVENTTYPEGROUP:
                        opasData = EventTypeGroup.NewEventTypeGroup();
                        break;

                    case Table.VENUE:
                        opasData = Venue.NewVenue();
                        break;

                    case Table.WORK:
                        opasData = Work.NewWork();
                        break;

                    case Table.WORKARTIST:
                        opasData = WorkArtist.NewWorkArtist();
                        break;
                }
                opasData.UpdateData(loadDocument, columnName, tagName);
                entity.FirstOrDefault().HasBeenUpdated = true;
                BsoArchiveEntities.Current.Detach(opasData);

                Log.Debug(string.Format("Finished update processing of entity: {0}", entity.Key));
            }
            BsoArchiveEntities.Current.Save();
        }
    }
}