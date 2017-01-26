


using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Common;
using System.ComponentModel;
using Adage.EF.Interfaces;
using System.Data;

namespace Bso.Archive.BusObj
{

    public partial class Conductor : IBusinessObject
    {

        public Conductor()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(EventsRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject EventsRelatedEnd = new RelatedObjectGen<Event>(
                "FK_Event_Conductor",
                "Event",
                "Events",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.Event.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (ConductorID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(ConductorID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static Conductor GetByID(Int32 ConductorID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("ConductorID", ConductorID); return (Conductor)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.Conductor", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static Conductor NewConductor()
        {
            Conductor newObject = new Conductor();

            BsoArchiveEntities.Current.AddToConductors(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static Conductor CreateNewConductor(int id)
		{
			Conductor newObject = new Conductor();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToConductors(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("ConductorID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.Conductors", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class EventArtist : IBusinessObject
    {

        public EventArtist()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(InstrumentRelatedEnd);
                _relatedObjects.Add(ArtistRelatedEnd);
                _relatedObjects.Add(EventRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject InstrumentRelatedEnd = new RelatedObjectGen<Instrument>(
                "FK_EventArtist_Instrument",
                "Instrument",
                "Instrument",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Instrument.GetEntityKey));



        internal static readonly RelatedObject ArtistRelatedEnd = new RelatedObjectGen<Artist>(
                "FK_EventArtist_Artist",
                "Artist",
                "Artist",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Artist.GetEntityKey));



        internal static readonly RelatedObject EventRelatedEnd = new RelatedObjectGen<Event>(
                "FK_EventArtist_Event",
                "Event",
                "Event",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Event.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (EventArtistID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(EventArtistID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static EventArtist GetByID(Int32 EventArtistID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("EventArtistID", EventArtistID); return (EventArtist)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.EventArtist", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static EventArtist NewEventArtist()
        {
            EventArtist newObject = new EventArtist();

            BsoArchiveEntities.Current.AddToEventArtists(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static EventArtist CreateNewEventArtist(int id)
		{
			EventArtist newObject = new EventArtist();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToEventArtists(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("EventArtistID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.EventArtists", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class EventParticipant : IBusinessObject
    {

        public EventParticipant()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(ParticipantRelatedEnd);
                _relatedObjects.Add(EventRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject ParticipantRelatedEnd = new RelatedObjectGen<Participant>(
                "FK_EventParticipant_Participant",
                "Participant",
                "Participant",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Participant.GetEntityKey));



        internal static readonly RelatedObject EventRelatedEnd = new RelatedObjectGen<Event>(
                "FK_EventParticipant_Event",
                "Event",
                "Event",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Event.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (EventParticipantID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(EventParticipantID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static EventParticipant GetByID(Int32 EventParticipantID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("EventParticipantID", EventParticipantID); return (EventParticipant)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.EventParticipant", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static EventParticipant NewEventParticipant()
        {
            EventParticipant newObject = new EventParticipant();

            BsoArchiveEntities.Current.AddToEventParticipants(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static EventParticipant CreateNewEventParticipant(int id)
		{
			EventParticipant newObject = new EventParticipant();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToEventParticipants(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("EventParticipantID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.EventParticipants", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class Orchestra : IBusinessObject
    {

        public Orchestra()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(EventsRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject EventsRelatedEnd = new RelatedObjectGen<Event>(
                "FK_Event_Orchestra",
                "Event",
                "Events",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.Event.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (OrchestraID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(OrchestraID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static Orchestra GetByID(Int32 OrchestraID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("OrchestraID", OrchestraID); return (Orchestra)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.Orchestra", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static Orchestra NewOrchestra()
        {
            Orchestra newObject = new Orchestra();

            BsoArchiveEntities.Current.AddToOrchestras(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static Orchestra CreateNewOrchestra(int id)
		{
			Orchestra newObject = new Orchestra();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToOrchestras(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("OrchestraID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.Orchestras", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class Project : IBusinessObject
    {

        public Project()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(EventsRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject EventsRelatedEnd = new RelatedObjectGen<Event>(
                "FK_Event_Project",
                "Event",
                "Events",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.Event.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (ProjectID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(ProjectID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static Project GetByID(Int32 ProjectID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("ProjectID", ProjectID); return (Project)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.Project", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static Project NewProject()
        {
            Project newObject = new Project();

            BsoArchiveEntities.Current.AddToProjects(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static Project CreateNewProject(int id)
		{
			Project newObject = new Project();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToProjects(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("ProjectID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.Projects", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class Season : IBusinessObject
    {

        public Season()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(EventsRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject EventsRelatedEnd = new RelatedObjectGen<Event>(
                "FK_Event_Season",
                "Event",
                "Events",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.Event.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (SeasonID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(SeasonID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static Season GetByID(Int32 SeasonID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("SeasonID", SeasonID); return (Season)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.Season", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static Season NewSeason()
        {
            Season newObject = new Season();

            BsoArchiveEntities.Current.AddToSeasons(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static Season CreateNewSeason(int id)
		{
			Season newObject = new Season();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToSeasons(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("SeasonID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.Seasons", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class Venue : IBusinessObject
    {

        public Venue()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(EventsRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject EventsRelatedEnd = new RelatedObjectGen<Event>(
                "FK_Event_Venue",
                "Event",
                "Events",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.Event.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (VenueID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(VenueID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static Venue GetByID(Int32 VenueID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("VenueID", VenueID); return (Venue)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.Venue", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static Venue NewVenue()
        {
            Venue newObject = new Venue();

            BsoArchiveEntities.Current.AddToVenues(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static Venue CreateNewVenue(int id)
		{
			Venue newObject = new Venue();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToVenues(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("VenueID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.Venues", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class Participant : IBusinessObject
    {

        public Participant()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(EventParticipantsRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject EventParticipantsRelatedEnd = new RelatedObjectGen<EventParticipant>(
                "FK_EventParticipant_Participant",
                "EventParticipant",
                "EventParticipants",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.EventParticipant.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (ParticipantID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(ParticipantID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static Participant GetByID(Int32 ParticipantID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("ParticipantID", ParticipantID); return (Participant)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.Participant", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static Participant NewParticipant()
        {
            Participant newObject = new Participant();

            BsoArchiveEntities.Current.AddToParticipants(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static Participant CreateNewParticipant(int id)
		{
			Participant newObject = new Participant();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToParticipants(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("ParticipantID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.Participants", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class OPASUpdate : IBusinessObject
    {

        public OPASUpdate()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();

                return _relatedObjects;
            }
        }


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (OPASUpdateId == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(OPASUpdateId.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static OPASUpdate GetByID(Int32 OPASUpdateId, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("OPASUpdateId", OPASUpdateId); return (OPASUpdate)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.OPASUpdate", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static OPASUpdate NewOPASUpdate()
        {
            OPASUpdate newObject = new OPASUpdate();

            BsoArchiveEntities.Current.AddToOPASUpdates(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static OPASUpdate CreateNewOPASUpdate(int id)
		{
			OPASUpdate newObject = new OPASUpdate();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToOPASUpdates(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("OPASUpdateId", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.OPASUpdates", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class Composer : IBusinessObject
    {

        public Composer()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(WorkComposersRelatedEnd);
                _relatedObjects.Add(WorksRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject WorkComposersRelatedEnd = new RelatedObjectGen<WorkComposer>(
                "FK_WorkComposer_Composer",
                "WorkComposer",
                "WorkComposers",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.WorkComposer.GetEntityKey));



        internal static readonly RelatedObject WorksRelatedEnd = new RelatedObjectGen<Work>(
                "FK_Work_Composer",
                "Work",
                "Works",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.Work.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (ComposerID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(ComposerID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static Composer GetByID(Int32 ComposerID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("ComposerID", ComposerID); return (Composer)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.Composer", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static Composer NewComposer()
        {
            Composer newObject = new Composer();

            BsoArchiveEntities.Current.AddToComposers(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static Composer CreateNewComposer(int id)
		{
			Composer newObject = new Composer();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToComposers(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("ComposerID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.Composers", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class Instrument : IBusinessObject
    {

        public Instrument()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(EventArtistsRelatedEnd);
                _relatedObjects.Add(WorkArtistsRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject EventArtistsRelatedEnd = new RelatedObjectGen<EventArtist>(
                "FK_EventArtist_Instrument",
                "EventArtist",
                "EventArtists",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.EventArtist.GetEntityKey));



        internal static readonly RelatedObject WorkArtistsRelatedEnd = new RelatedObjectGen<WorkArtist>(
                "FK_WorkArtist_Instrument",
                "WorkArtist",
                "WorkArtists",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.WorkArtist.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (InstrumentID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(InstrumentID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static Instrument GetByID(Int32 InstrumentID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("InstrumentID", InstrumentID); return (Instrument)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.Instrument", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static Instrument NewInstrument()
        {
            Instrument newObject = new Instrument();

            BsoArchiveEntities.Current.AddToInstruments(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static Instrument CreateNewInstrument(int id)
		{
			Instrument newObject = new Instrument();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToInstruments(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("InstrumentID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.Instruments", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class Artist : IBusinessObject
    {

        public Artist()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(EventArtistsRelatedEnd);
                _relatedObjects.Add(WorkArtistsRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject EventArtistsRelatedEnd = new RelatedObjectGen<EventArtist>(
                "FK_EventArtist_Artist",
                "EventArtist",
                "EventArtists",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.EventArtist.GetEntityKey));



        internal static readonly RelatedObject WorkArtistsRelatedEnd = new RelatedObjectGen<WorkArtist>(
                "FK_WorkArtist_Artist",
                "WorkArtist",
                "WorkArtists",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.WorkArtist.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (ArtistID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(ArtistID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static Artist GetByID(Int32 ArtistID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("ArtistID", ArtistID); return (Artist)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.Artist", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static Artist NewArtist()
        {
            Artist newObject = new Artist();

            BsoArchiveEntities.Current.AddToArtists(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static Artist CreateNewArtist(int id)
		{
			Artist newObject = new Artist();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToArtists(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("ArtistID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.Artists", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class Search : IBusinessObject
    {

        public Search()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();

                return _relatedObjects;
            }
        }


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (SearchID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(SearchID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static Search GetByID(Int32 SearchID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("SearchID", SearchID); return (Search)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.Search", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static Search NewSearch()
        {
            Search newObject = new Search();

            BsoArchiveEntities.Current.AddToSearches(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static Search CreateNewSearch(int id)
		{
			Search newObject = new Search();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToSearches(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("SearchID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.Searches", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class EventParticipantType : IBusinessObject
    {

        public EventParticipantType()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(EventRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject EventRelatedEnd = new RelatedObjectGen<Event>(
                "FK_EventParticipantType_Event",
                "Event",
                "Event",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Event.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (EventParticipantTypeID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(EventParticipantTypeID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static EventParticipantType GetByID(Int32 EventParticipantTypeID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("EventParticipantTypeID", EventParticipantTypeID); return (EventParticipantType)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.EventParticipantType", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static EventParticipantType NewEventParticipantType()
        {
            EventParticipantType newObject = new EventParticipantType();

            BsoArchiveEntities.Current.AddToEventParticipantTypes(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static EventParticipantType CreateNewEventParticipantType(int id)
		{
			EventParticipantType newObject = new EventParticipantType();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToEventParticipantTypes(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("EventParticipantTypeID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.EventParticipantTypes", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class EventWork : IBusinessObject
    {

        public EventWork()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(EventRelatedEnd);
                _relatedObjects.Add(WorkRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject EventRelatedEnd = new RelatedObjectGen<Event>(
                "FK_EventWork_Event",
                "Event",
                "Event",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Event.GetEntityKey));



        internal static readonly RelatedObject WorkRelatedEnd = new RelatedObjectGen<Work>(
                "FK_EventWork_Work",
                "Work",
                "Work",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Work.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (EventWorkID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(EventWorkID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static EventWork GetByID(Int32 EventWorkID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("EventWorkID", EventWorkID); return (EventWork)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.EventWork", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static EventWork NewEventWork()
        {
            EventWork newObject = new EventWork();

            BsoArchiveEntities.Current.AddToEventWorks(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static EventWork CreateNewEventWork(int id)
		{
			EventWork newObject = new EventWork();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToEventWorks(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("EventWorkID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.EventWorks", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class WorkArtist : IBusinessObject
    {

        public WorkArtist()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(ArtistRelatedEnd);
                _relatedObjects.Add(InstrumentRelatedEnd);
                _relatedObjects.Add(WorkRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject ArtistRelatedEnd = new RelatedObjectGen<Artist>(
                "FK_WorkArtist_Artist",
                "Artist",
                "Artist",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Artist.GetEntityKey));



        internal static readonly RelatedObject InstrumentRelatedEnd = new RelatedObjectGen<Instrument>(
                "FK_WorkArtist_Instrument",
                "Instrument",
                "Instrument",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Instrument.GetEntityKey));



        internal static readonly RelatedObject WorkRelatedEnd = new RelatedObjectGen<Work>(
                "FK_WorkArtist_Work",
                "Work",
                "Work",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Work.GetEntityKey));



        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (WorkArtistID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(WorkArtistID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static WorkArtist GetByID(Int32 WorkArtistID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("WorkArtistID", WorkArtistID); return (WorkArtist)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.WorkArtist", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static WorkArtist NewWorkArtist()
        {
            WorkArtist newObject = new WorkArtist();

            BsoArchiveEntities.Current.AddToWorkArtists(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static WorkArtist CreateNewWorkArtist(int id)
		{
			WorkArtist newObject = new WorkArtist();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToWorkArtists(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("WorkArtistID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.WorkArtists", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class WorkDocument : IBusinessObject
    {

        public WorkDocument()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(WorkRelatedEnd);

                return _relatedObjects;
            }
        }
        internal static readonly RelatedObject WorkRelatedEnd = new RelatedObjectGen<Work>(
                "FK_WorkDocument_Work",
                "Work",
                "Work",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Work.GetEntityKey));



        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                return "";
                //List<string> keyValues = new List<string>();

                //if (WorkArtistID == null)
                //    keyValues.Add(string.Empty);
                //else
                //    keyValues.Add(WorkArtistID.ToString());
                //return string.Join("|", keyValues.ToArray());
            }
        }

        public static WorkDocument GetByID(Int32 WorkArtistID, ObjectContext currentContext)
        {
            //Dictionary<string, object> keyValues = new Dictionary<string, object>();
            //keyValues.Add("WorkArtistID", WorkArtistID); return (WorkArtist)currentContext.GetObjectByKey(
            //    new System.Data.EntityKey("BsoArchiveEntities.WorkArtist", keyValues));
            throw new NotImplementedException();
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static WorkDocument NewWorkDocument()
        {
            WorkDocument newObject = new WorkDocument();

            //BsoArchiveEntities.Current.AddToWorkArtists(newObject);
            //BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("WorkArtistID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.WorkArtists", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);
    }
    public partial class EventType : IBusinessObject
    {

        public EventType()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(EventsRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject EventsRelatedEnd = new RelatedObjectGen<Event>(
                "FK_Event_EventType",
                "Event",
                "Events",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.Event.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (TypeID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(TypeID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static EventType GetByID(Int32 TypeID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("TypeID", TypeID); return (EventType)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.EventType", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static EventType NewEventType()
        {
            EventType newObject = new EventType();

            BsoArchiveEntities.Current.AddToEventTypes(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static EventType CreateNewEventType(int id)
		{
			EventType newObject = new EventType();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToEventTypes(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("TypeID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.EventTypes", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class EventTypeGroup : IBusinessObject
    {

        public EventTypeGroup()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(EventsRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject EventsRelatedEnd = new RelatedObjectGen<Event>(
                "FK_Event_EventTypeGroup",
                "Event",
                "Events",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.Event.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (TypeGroupID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(TypeGroupID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static EventTypeGroup GetByID(Int32 TypeGroupID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("TypeGroupID", TypeGroupID); return (EventTypeGroup)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.EventTypeGroup", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static EventTypeGroup NewEventTypeGroup()
        {
            EventTypeGroup newObject = new EventTypeGroup();

            BsoArchiveEntities.Current.AddToEventTypeGroups(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static EventTypeGroup CreateNewEventTypeGroup(int id)
		{
			EventTypeGroup newObject = new EventTypeGroup();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToEventTypeGroups(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("TypeGroupID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.EventTypeGroups", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class WorkComposer : IBusinessObject
    {

        public WorkComposer()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(ComposerRelatedEnd);
                _relatedObjects.Add(WorkRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject ComposerRelatedEnd = new RelatedObjectGen<Composer>(
                "FK_WorkComposer_Composer",
                "Composer",
                "Composer",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Composer.GetEntityKey));



        internal static readonly RelatedObject WorkRelatedEnd = new RelatedObjectGen<Work>(
                "FK_WorkComposer_Work",
                "Work",
                "Work",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Work.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (WorkComposer1 == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(WorkComposer1.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static WorkComposer GetByID(Int32 WorkComposer1, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("WorkComposer1", WorkComposer1); return (WorkComposer)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.WorkComposer", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static WorkComposer NewWorkComposer()
        {
            WorkComposer newObject = new WorkComposer();

            BsoArchiveEntities.Current.AddToWorkComposers(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static WorkComposer CreateNewWorkComposer(int id)
		{
			WorkComposer newObject = new WorkComposer();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToWorkComposers(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("WorkComposer1", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.WorkComposers", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class Event : IBusinessObject
    {

        public Event()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(ConductorRelatedEnd);
                _relatedObjects.Add(EventTypeRelatedEnd);
                _relatedObjects.Add(EventTypeGroupRelatedEnd);
                _relatedObjects.Add(OrchestraRelatedEnd);
                _relatedObjects.Add(ProjectRelatedEnd);
                _relatedObjects.Add(SeasonRelatedEnd);
                _relatedObjects.Add(VenueRelatedEnd);
                _relatedObjects.Add(EventArtistsRelatedEnd);
                _relatedObjects.Add(EventParticipantsRelatedEnd);
                _relatedObjects.Add(EventParticipantTypesRelatedEnd);
                _relatedObjects.Add(EventWorksRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject ConductorRelatedEnd = new RelatedObjectGen<Conductor>(
                "FK_Event_Conductor",
                "Conductor",
                "Conductor",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Conductor.GetEntityKey));



        internal static readonly RelatedObject EventTypeRelatedEnd = new RelatedObjectGen<EventType>(
                "FK_Event_EventType",
                "EventType",
                "EventType",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.EventType.GetEntityKey));



        internal static readonly RelatedObject EventTypeGroupRelatedEnd = new RelatedObjectGen<EventTypeGroup>(
                "FK_Event_EventTypeGroup",
                "EventTypeGroup",
                "EventTypeGroup",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.EventTypeGroup.GetEntityKey));



        internal static readonly RelatedObject OrchestraRelatedEnd = new RelatedObjectGen<Orchestra>(
                "FK_Event_Orchestra",
                "Orchestra",
                "Orchestra",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Orchestra.GetEntityKey));



        internal static readonly RelatedObject ProjectRelatedEnd = new RelatedObjectGen<Project>(
                "FK_Event_Project",
                "Project",
                "Project",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Project.GetEntityKey));



        internal static readonly RelatedObject SeasonRelatedEnd = new RelatedObjectGen<Season>(
                "FK_Event_Season",
                "Season",
                "Season",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Season.GetEntityKey));



        internal static readonly RelatedObject VenueRelatedEnd = new RelatedObjectGen<Venue>(
                "FK_Event_Venue",
                "Venue",
                "Venue",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Venue.GetEntityKey));



        internal static readonly RelatedObject EventArtistsRelatedEnd = new RelatedObjectGen<EventArtist>(
                "FK_EventArtist_Event",
                "EventArtist",
                "EventArtists",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.EventArtist.GetEntityKey));



        internal static readonly RelatedObject EventParticipantsRelatedEnd = new RelatedObjectGen<EventParticipant>(
                "FK_EventParticipant_Event",
                "EventParticipant",
                "EventParticipants",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.EventParticipant.GetEntityKey));



        internal static readonly RelatedObject EventParticipantTypesRelatedEnd = new RelatedObjectGen<EventParticipantType>(
                "FK_EventParticipantType_Event",
                "EventParticipantType",
                "EventParticipantTypes",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.EventParticipantType.GetEntityKey));



        internal static readonly RelatedObject EventWorksRelatedEnd = new RelatedObjectGen<EventWork>(
                "FK_EventWork_Event",
                "EventWork",
                "EventWorks",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.EventWork.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (EventID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(EventID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static Event GetByID(Int32 EventID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("EventID", EventID); return (Event)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.Event", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static Event NewEvent()
        {
            Event newObject = new Event();

            BsoArchiveEntities.Current.AddToEvents(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static Event CreateNewEvent(int id)
		{
			Event newObject = new Event();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToEvents(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("EventID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.Events", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class Work : IBusinessObject
    {

        public Work()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();
                _relatedObjects.Add(ComposerRelatedEnd);
                _relatedObjects.Add(EventWorksRelatedEnd);
                _relatedObjects.Add(WorkArtistsRelatedEnd);
                _relatedObjects.Add(WorkDocumentRelatedEnd);
                _relatedObjects.Add(Work1RelatedEnd);
                _relatedObjects.Add(Work2RelatedEnd);
                _relatedObjects.Add(WorkComposersRelatedEnd);

                return _relatedObjects;
            }
        }



        internal static readonly RelatedObject ComposerRelatedEnd = new RelatedObjectGen<Composer>(
                "FK_Work_Composer",
                "Composer",
                "Composer",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Composer.GetEntityKey));



        internal static readonly RelatedObject EventWorksRelatedEnd = new RelatedObjectGen<EventWork>(
                "FK_EventWork_Work",
                "EventWork",
                "EventWorks",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.EventWork.GetEntityKey));



        internal static readonly RelatedObject WorkArtistsRelatedEnd = new RelatedObjectGen<WorkArtist>(
                "FK_WorkArtist_Work",
                "WorkArtist",
                "WorkArtists",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.WorkArtist.GetEntityKey));

        internal static readonly RelatedObject WorkDocumentRelatedEnd = new RelatedObjectGen<WorkDocument>(
              "FK_WorkDocument_Work",
              "WorkDocument",
              "WorkDocument",
              RelatedEnum.ZeroOne,
              new GetKey(Bso.Archive.BusObj.WorkDocument.GetEntityKey));

        internal static readonly RelatedObject Work1RelatedEnd = new RelatedObjectGen<Work>(
                "FK_Work_Work",
                "Work1",
                "Work1",
                RelatedEnum.ZeroOne,
                new GetKey(Bso.Archive.BusObj.Work.GetEntityKey));



        internal static readonly RelatedObject Work2RelatedEnd = new RelatedObjectGen<Work>(
                "FK_Work_Work",
                "Work",
                "Work2",
                RelatedEnum.One,
                new GetKey(Bso.Archive.BusObj.Work.GetEntityKey));



        internal static readonly RelatedObject WorkComposersRelatedEnd = new RelatedObjectGen<WorkComposer>(
                "FK_WorkComposer_Work",
                "WorkComposer",
                "WorkComposers",
                RelatedEnum.Many,
                new GetKey(Bso.Archive.BusObj.WorkComposer.GetEntityKey));


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (WorkID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(WorkID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static Work GetByID(Int32 WorkID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("WorkID", WorkID); return (Work)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.Work", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static Work NewWork()
        {
            Work newObject = new Work();

            BsoArchiveEntities.Current.AddToWorks(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static Work CreateNewWork(int id)
		{
			Work newObject = new Work();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToWorks(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("WorkID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.Works", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class CommissionType : IBusinessObject
    {

        public CommissionType()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();

                return _relatedObjects;
            }
        }


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (CommissionTypeID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(CommissionTypeID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static CommissionType GetByID(Int32 CommissionTypeID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("CommissionTypeID", CommissionTypeID); return (CommissionType)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.CommissionType", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static CommissionType NewCommissionType()
        {
            CommissionType newObject = new CommissionType();

            BsoArchiveEntities.Current.AddToCommissionTypes(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static CommissionType CreateNewCommissionType(int id)
		{
			CommissionType newObject = new CommissionType();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToCommissionTypes(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("CommissionTypeID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.CommissionTypes", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class PremiereType : IBusinessObject
    {

        public PremiereType()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();

                return _relatedObjects;
            }
        }


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (PremiereTypeID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(PremiereTypeID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static PremiereType GetByID(Int32 PremiereTypeID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("PremiereTypeID", PremiereTypeID); return (PremiereType)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.PremiereType", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static PremiereType NewPremiereType()
        {
            PremiereType newObject = new PremiereType();

            BsoArchiveEntities.Current.AddToPremiereTypes(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static PremiereType CreateNewPremiereType(int id)
		{
			PremiereType newObject = new PremiereType();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToPremiereTypes(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("PremiereTypeID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.PremiereTypes", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class EventDetail : IBusinessObject
    {

        public EventDetail()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();

                return _relatedObjects;
            }
        }


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (EventDetailID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(EventDetailID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static EventDetail GetByID(Int32 EventDetailID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("EventDetailID", EventDetailID); return (EventDetail)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.EventDetail", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static EventDetail NewEventDetail()
        {
            EventDetail newObject = new EventDetail();

            BsoArchiveEntities.Current.AddToEventDetails(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static EventDetail CreateNewEventDetail(int id)
		{
			EventDetail newObject = new EventDetail();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToEventDetails(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("EventDetailID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.EventDetails", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }

    public partial class ArtistDetail : IBusinessObject
    {

        public ArtistDetail()
        {
            OnInit();
        }

        partial void OnInit();


        #region IBusinessObject Members
        /** We are not using Broken Rules at this moment **/
        public string BrokenRulesString { get { return String.Empty; } }
        public bool IsValid { get { throw new Exception("Not Implemented"); } }
        public bool CheckIsValid(List<IBusinessObject> checkEntities) { throw new Exception("Not Implemented"); }
        public void FindBrokenRules(List<IBusinessObject> checkEntities, List<string> currentMessages, EntityObject parent)
        { throw new Exception("Not Implemented"); }
        public string GetCurrentBrokenRules() { throw new Exception("Not Implemented"); }

        static List<BusinessObjectStructure> _tblFields = null;

        List<BusinessObjectStructure> IGenericBusinessObj.GetTableFields(ObjectStateEntry entry)
        {
            if (_tblFields != null)
                return _tblFields;

            return _tblFields = Adage.EF.BusObj.BusinessObjectHelper.GetTableFields(this.GetType(), entry);
        }

        private List<RelatedObject> _relatedObjects;

        public List<RelatedObject> RelatedObjects
        {
            get
            {
                if (_relatedObjects != null)
                    return _relatedObjects;


                _relatedObjects = new List<RelatedObject>();

                return _relatedObjects;
            }
        }


        List<RelatedObject> IGenericBusinessObj.GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentContext)
        {
            return RelatedObjects.Where(relObj =>
                relObj.RelatedType != RelatedEnum.Many).ToList();
        }

        [DataObjectField(false)]
        public string UniqueKey
        {
            get
            {
                List<string> keyValues = new List<string>();

                if (ArtistDetailID == null)
                    keyValues.Add(string.Empty);
                else
                    keyValues.Add(ArtistDetailID.ToString());
                return string.Join("|", keyValues.ToArray());
            }
        }

        public static ArtistDetail GetByID(Int32 ArtistDetailID, ObjectContext currentContext)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("ArtistDetailID", ArtistDetailID); return (ArtistDetail)currentContext.GetObjectByKey(
new System.Data.EntityKey("BsoArchiveEntities.ArtistDetail", keyValues));
        }

        object IGenericBusinessObj.ReadProperty(string name, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, name, currentEntry);
        }

        object IGenericBusinessObj.ReadProperty(int index, ObjectStateEntry currentEntry)
        {
            return Adage.EF.BusObj.BusinessObjectHelper.ReadObjectValue(this, index, currentEntry);
        }

        void IBusinessObject.UpdateProperty(string name, object value, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = ((IBusinessObject)this).GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.PropertyInfo.CanWrite)
                fieldToGet.PropertyInfo.SetValue(this, value, null);
        }

        bool IGenericBusinessObj.CanReadProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldReadAccess(this, name);
        }

        bool IGenericBusinessObj.CanWriteProperty(string name)
        {
            return Adage.EF.BusObj.FieldSecurity.CurrentSecurity.FieldWriteAccess(this, name);
        }


        public static ArtistDetail NewArtistDetail()
        {
            ArtistDetail newObject = new ArtistDetail();

            BsoArchiveEntities.Current.AddToArtistDetails(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
        }
        /**
		public static ArtistDetail CreateNewArtistDetail(int id)
		{
			ArtistDetail newObject = new ArtistDetail();
						
			newObject.EntityKey = GetEntityKey(id.ToString());

            BsoArchiveEntities.Current.AddToArtistDetails(newObject);
            BsoArchiveEntities.SetDefaultValue(newObject);
            return newObject;
		}
		**/
        public bool IsNew
        {
            get { return UniqueKey == "0"; }
        }

        void IBusinessObject.Save(ObjectContext context)
        {
            OnSave(context);
        }

        System.Data.EntityKey IBusinessObject.GetEntityKey(string currentValues)
        {
            return GetEntityKey(currentValues);
        }

        public static System.Data.EntityKey GetEntityKey(string currentValues)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            string[] sepCurrValues = currentValues.Split("|".ToCharArray());
            keyValues.Add("ArtistDetailID", int.Parse(sepCurrValues[0]));
            return new System.Data.EntityKey("BsoArchiveEntities.ArtistDetails", keyValues);
        }

        #endregion

        partial void OnSave(ObjectContext context);

    }



}