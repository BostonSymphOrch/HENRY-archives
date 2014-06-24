using Bso.Archive.BusObj.Interface;
using Bso.Archive.BusObj.Properties;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Bso.Archive.BusObj
{
    public partial class OPASUpdate
    {
        IOPASData opasData;
        enum Table { EVENT, ARTIST, EVENTTYPE, CONDUCTOR, ORCHESTRA, PARTICIPANT, PROJECT, SEASON, EVENTTYPEGROUP, VENUE, WORK, WORKARTIST }


        /// <summary>
        /// Update OPAS Data
        /// </summary>
        public void UpdateOPASData()
        {
            XDocument loadDocument = XDocument.Load(Settings.Default.XMLUpdate);
            var recordsToUpdate = BsoArchiveEntities.Current.OPASUpdates.Where(d => !d.HasBeenUpdated);
            var entitesToUpdate = recordsToUpdate.GroupBy(d => d.TableName);
            string columnName;
            string tagName;

            foreach (var entity in entitesToUpdate)
            {
                columnName = entity.FirstOrDefault().ColumnName;
                tagName = entity.FirstOrDefault().TagName;
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

            }
            BsoArchiveEntities.Current.Save();

        }
    }
}
