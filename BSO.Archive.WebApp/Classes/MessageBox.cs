using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BSO.Archive.WebApp.Classes
{
    public enum PageMessageBoxMode
    {
        ErrorMessage = 1,
        Warning = 2,
        Information = 3,
        Question = 4,
        OK = 5
    }

    public interface IMessageBox
    {
        void ShowOK(string msg);
        void ShowInformation(string msg);
        void ShowError(string msg);
        void ShowWarning(string msg);
        void ShowQuestion(string msg);
        void ShowException(Exception ex);

        bool HasErrorMessage();

        string ID
        {
            get;
            set;
        }
    }
    [ToolboxData("<{0}:MessageBox runat=server></{0}:MessageBox>"), Themeable(true)]
    public class MessageBox : Panel, IMessageBox
    {

        // <<Features to implement>>
        //// PageMessageBox existing methods
        //// PageMessageBox... multiple messages... query number of messages... support old behavior
        //// Validation Group functions (UseForAll, Specific Validation Groups)
        //// Add style class for input controls that are in error    
        //// Static methods for finding by validation group or adding messages
        //// JS methods for setting in questions and clearing the messagebox
        //// Allow for rendering as innerHTML without the <a>
        // LATER: Session storage for PageMessageBox messages.  Clear session var on Render

        public MessageBox()
        {
            this.Init += new EventHandler(MessageBox_Init);
            this.Load += new EventHandler(MessageBox_Load);
        }

        void MessageBox_Init(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current != null)
            {
                if (System.Web.HttpContext.Current.Items["Adage.Controls.MessageBox"] == null)
                    System.Web.HttpContext.Current.Items.Add("Adage.Controls.MessageBox", new List<BSO.Archive.WebApp.Classes.IMessageBox>());

                List<BSO.Archive.WebApp.Classes.IMessageBox> currBoxes = (List<BSO.Archive.WebApp.Classes.IMessageBox>)
                    System.Web.HttpContext.Current.Items["Adage.Controls.MessageBox"];

                if (!currBoxes.Contains(this))
                {
                    currBoxes.Add(this);

                    string text1 = "document.getElementById(\"" + this.ClientID + "\")";
                    this.Page.ClientScript.RegisterArrayDeclaration("Page_MessageBoxes", text1);
                }
            }
        }

        public static BSO.Archive.WebApp.Classes.IMessageBox GetMessageBox()
        {
            return GetMessageBox(string.Empty);
        }

        public static BSO.Archive.WebApp.Classes.IMessageBox GetMessageBox(string validationGroup)
        {
            if (System.Web.HttpContext.Current == null)
                return null;

            if (System.Web.HttpContext.Current.Items.Contains("Adage.Controls.MessageBox") == false)
                return null;

            List<BSO.Archive.WebApp.Classes.IMessageBox> currBoxes = System.Web.HttpContext.Current.Items["Adage.Controls.MessageBox"]
                as List<BSO.Archive.WebApp.Classes.IMessageBox>;

            if (currBoxes == null)
                return null;

            BSO.Archive.WebApp.Classes.IMessageBox lastValidBox = null;

            foreach (BSO.Archive.WebApp.Classes.IMessageBox eachBox in currBoxes)
            {
                MessageBox currHelper = eachBox as MessageBox;
                if (currHelper != null)
                {
                    if (currHelper.IsValidValidationGroup(validationGroup))
                        return currHelper;
                }
                else
                {
                    lastValidBox = eachBox;
                }
            }

            return lastValidBox;
        }

        public class ValidationMessage
        {
            PageMessageBoxMode __type;
            string __message;
            public PageMessageBoxMode Type { get { return __type; } set { __type = value; } }
            public string Message { get { return __message; } set { __message = value; } }

            public ValidationMessage()
            { }

            public ValidationMessage(PageMessageBoxMode _type, string _message)
            {
                Type = _type;
                Message = _message;
            }
        }

        #region Javascript

        private string validationScript = @"function ValidationSummaryOnSubmit(validationGroup) {
    var Adage_Controls_MessageBoxFirstInvalid = null;
    for(msgBoxCnt = 0; msgBoxCnt < Page_MessageBoxes.length; msgBoxCnt++)
    {
        var currResults = Page_MessageBoxes[msgBoxCnt];
        var s = '';
        if (!Page_IsValid && IsValidValidationGroup(currResults, validationGroup) ) {
            for (i=0; i<Page_Validators.length; i++) {

                if ( currResults.ErrorFieldCssClass != '' )
                    UpdateCssClass(Page_Validators[i], currResults.ErrorFieldCssClass);

                if (!Page_Validators[i].isvalid) {
                    var currMessage = '';
                    if ( Adage_Controls_MessageBoxFirstInvalid == null )
                        Adage_Controls_MessageBoxFirstInvalid = Page_Validators[i];

                    // look for the error message
                    if ( Page_Validators[i].errormessage != 'undefined' && typeof(Page_Validators[i].errormessage) == 'string')
                        currMessage = Page_Validators[i].errormessage;
                    else
                        currMessage = Page_Validators[i].innerText;

                    // determine if the message should be shown as an anchor
                    if ( currResults.RenderBaseValidatorAsAnchor == 'false' )
                        s += '<li>' + currMessage + '</li>';
                    else
                        s += ""<li><a href='javascript:ValidatorSetFocus(document.getElementById("" + 
                            '""' + Page_Validators[i].id + '""), null);\'>' + currMessage + '</a></li>';
                }
            }
        }
        if (Adage_Controls_MessageBoxFirstInvalid != null)
            ValidatorSetFocus(Adage_Controls_MessageBoxFirstInvalid, null);

        // add the error OL if necessary
        if ( s.length > 0 )
            s = '<ol class=""WebPageMessageError"">' + s + '</ol>';

        if(currResults != null)
            currResults.innerHTML = s;
    }
}

function IsValidValidationGroup(currValidSummary, validationGroup)
    {
        if ( currValidSummary.UseValidationGroups == 'false' )
            return true;
            
        // using groups... look for any exclusions
        var currExcludes = currValidSummary.ExcludeValidationGroups.split(',');
        for( exCnt=0; exCnt< currExcludes.length; exCnt++)
        {
            if ( currExcludes[exCnt] == validationGroup )
                return false;
            
            if ( currExcludes[exCnt] == '.*' )
                return false;
            
            try
            {
                var currExcludesRegEx = new RegExp('^' + currExcludes[exCnt] + '$');
                
                // check to see if the regex matches
                if ( currExcludesRegEx.test(validationGroup) == true )
                    return false;
            } catch (excep)
            {
                alert('Error parsing the regex:' + currExcludes[exCnt]);
            }
        }
        
        // look to see if there are any includes
        var currIncludes = currValidSummary.IncludeValidationGroups.split(',');
            
        // if includes look to see if included
        for( inCnt=0; inCnt< currIncludes.length; inCnt++)
        {
            if ( currIncludes[inCnt] == validationGroup )
                return true;
            
            if ( currIncludes[inCnt] == '.*' )
                return true;
            
            try
            {
                var currIncludesRegEx = new RegExp('^' + currIncludes[inCnt] + '$');
                
                // check to see if the regex matches
                if ( currIncludesRegEx.test(validationGroup) == true )
                    return true;
            } catch (excep)
            {
                alert('Error parsing the regex:' + currIncludes[inCnt]);
            }
        }
        
        // using includes, and not found
        return false;
    }
    
    function AddCssClass(currElement, className)
    {
        if ( currElement.className.indexOf(className) > -1)
            return;
            
        if ( currElement.className.length > 0 )
            currElement.className += ' ';
            
        currElement.className += className;
    }
    
    function RemoveCssClass(currElement, className)
    {
        if ( currElement.className.indexOf(className) == -1)
            return;
        
        var regEx = new RegExp('\s?' + className);
        
        currElement.className = currElement.className.replace(regEx, '');
    }
    
    function UpdateCssClass(currValidator, className)
    {
        ValidatorSetFocus(currValidator, null);
        
        if ( currValidator.isvalid )
            RemoveCssClass(Page_InvalidControlToBeFocused, className);
        else
            AddCssClass(Page_InvalidControlToBeFocused, className);
            
        Page_InvalidControlToBeFocused = null;
        
    }

    function Adage_Controls_FindMessageBox(validationGroup)
    {
        if ( Page_MessageBoxes == null || Page_MessageBoxes == 'undefined' )
            return null;
        
        if ( validationGroup == 'undefined' )
            validationGroup = '';
            
        for(i = 0; i < Page_MessageBoxes.length; i++)
        {
            if ( IsValidValidationGroup(Page_MessageBoxes[i], validationGroup) )
                return Page_MessageBoxes[i];
        }
        
        return null;
    }
    
    function Adage_Controls_MessageBox_Clear(validationGroup)
    {
        var currMessageBox = Adage_Controls_FindMessageBox(validationGroup);
        if ( currMessageBox != null )
            currMessageBox.innerHTML = '';
    }
    
    function Adage_Controls_MessageBox_ShowMessage(validationGroup, message, OLcssClass)
    {
        var currMessageBox = Adage_Controls_FindMessageBox(validationGroup);
        if ( currMessageBox != null )
        {
            if ( OLcssClass == 'undefined' )
                OLcssClass = '';
            currMessageBox.innerHTML = '<ol class=""' + OLcssClass + '""><li>' + message + '</li></ol>';
        }
    }
    
    function Adage_Controls_MessageBox_AddMessage(validationGroup, message, OLcssClass)
    {
        var currMessageBox = Adage_Controls_FindMessageBox(validationGroup);
        if ( currMessageBox != null )
        {
            for(i=0; i<currMessageBox.childNodes.length; i++)
            {
                if ( currMessageBox.childNodes[i].className == OLcssClass )
                {
                    currMessageBox.childNodes[i].innerHTML += '<li>' + message + '</li>';
                    return;
                }
            }
            
            // put the message at the top if the CSS class wasn't found
            currMessageBox.innerHTML = '<ol class=""' + OLcssClass + '""><li>' + message + 
                '</li></ol>' + currMessageBox.innerHTML;
        }
    }
";

        #endregion

        protected void MessageBox_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(),
                "ValidationSummaryOnSubmit", validationScript, true);
        }

        #region Public Properties

        public bool RenderBaseValidatorAsAnchor
        {
            get
            {
                if (ViewState["RenderBaseValidatorAsAnchor"] == null)
                    ViewState["RenderBaseValidatorAsAnchor"] = true;

                return (bool)ViewState["RenderBaseValidatorAsAnchor"];
            }
            set
            {
                ViewState["RenderBaseValidatorAsAnchor"] = value;
            }
        }

        public bool UseValidationGroups
        {
            get
            {
                if (ViewState["UseValidationGroups"] == null)
                    ViewState["UseValidationGroups"] = false;

                return (bool)ViewState["UseValidationGroups"];
            }
            set
            {
                ViewState["UseValidationGroups"] = value;
            }
        }


        public bool OnlyOneMessageGroup
        {
            get
            {
                if (ViewState["OnlyOneMessageGroup"] == null)
                    ViewState["OnlyOneMessageGroup"] = false;

                return (bool)ViewState["OnlyOneMessageGroup"];
            }
            set
            {
                ViewState["OnlyOneMessageGroup"] = value;
            }
        }

        public string IncludeValidationGroups
        {
            get
            {
                if (ViewState["IncludeValidationGroups"] == null)
                    ViewState["IncludeValidationGroups"] = ".*";

                return ViewState["IncludeValidationGroups"].ToString();
            }
            set
            {
                ViewState["IncludeValidationGroups"] = value;
            }
        }

        public string ExcludeValidationGroups
        {
            get
            {
                if (ViewState["ExcludeValidationGroups"] == null)
                    ViewState["ExcludeValidationGroups"] = string.Empty;

                return ViewState["ExcludeValidationGroups"].ToString();
            }
            set
            {
                ViewState["ExcludeValidationGroups"] = value;
            }
        }

        public string ErrorFieldCssClass
        {
            get
            {
                if (ViewState["ErrorFieldCssClass"] == null)
                    ViewState["ErrorFieldCssClass"] = string.Empty;
                return ViewState["ErrorFieldCssClass"].ToString();
            }
            set
            {
                ViewState["ErrorFieldCssClass"] = value;
            }
        }

        List<ValidationMessage> _messages = new List<ValidationMessage>();
        public List<ValidationMessage> MessagesToDisplay
        {
            get
            {
                return _messages;
            }
        }
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(),
            //    "outputDiv", "var outputResults = '" + this.ClientID + "';", true);

            this.Attributes.Add("UseValidationGroups", UseValidationGroups.ToString().ToLower());
            this.Attributes.Add("IncludeValidationGroups", IncludeValidationGroups);
            this.Attributes.Add("ExcludeValidationGroups", ExcludeValidationGroups);
            this.Attributes.Add("ErrorFieldCssClass", ErrorFieldCssClass);
            this.Attributes.Add("RenderBaseValidatorAsAnchor", RenderBaseValidatorAsAnchor.ToString().ToLower());

            base.OnPreRender(e);

            if (Page.IsPostBack)
                SetupValidationMessages();

            ShowMessages();
        }

        private void ShowMessages()
        {
            System.Text.StringBuilder messagesToSend;
            foreach (int eachValue in Enum.GetValues(typeof(PageMessageBoxMode)))
            {
                messagesToSend = new System.Text.StringBuilder();
                foreach (ValidationMessage msg in MessagesToDisplay)
                {
                    if ((int)msg.Type == eachValue)
                    {
                        messagesToSend.AppendFormat("<li>{0}</li>", msg.Message);
                    }
                }

                if (messagesToSend.Length > 0)
                {
                    AddMessages((PageMessageBoxMode)eachValue, messagesToSend);

                    // if we are supporting the old behavior we need to show only the highest level message
                    if (OnlyOneMessageGroup)
                        return;
                }
            }
        }

        private void AddMessages(PageMessageBoxMode messageType, System.Text.StringBuilder messagesToSend)
        {
            string cssClass = string.Empty;
            switch (messageType)
            {
                case PageMessageBoxMode.ErrorMessage:
                    cssClass = "WebPageMessageError";
                    break;
                case PageMessageBoxMode.Information:
                    cssClass = "WebPageMessageInformation";
                    break;
                case PageMessageBoxMode.OK:
                    cssClass = "WebPageMessageOK";
                    break;
                case PageMessageBoxMode.Question:
                    cssClass = "WebPageMessageQuestion";
                    break;
                case PageMessageBoxMode.Warning:
                    cssClass = "WebPageMessageWarning";
                    break;
            }

            Literal currOutput = new Literal();
            currOutput.Text = string.Format("<ol class='{0}'>{1}</ol>", cssClass, messagesToSend);
            this.Controls.Add(currOutput);
        }

        private void SetupValidationMessages()
        {
            foreach (IValidator currentValidator in Page.Validators)
            {
                if (currentValidator is Control && IsValidValidationGroup(currentValidator))
                {
                    if (currentValidator is BaseValidator)
                    {
                        BaseValidator _bValidator = (BaseValidator)currentValidator;
                        Control validatedControl = _bValidator.NamingContainer.FindControl(
                            _bValidator.ControlToValidate);

                        if (currentValidator.IsValid == false)
                        {
                            string formatString = "{0}";
                            if (RenderBaseValidatorAsAnchor)
                                formatString = "<a href=\"javascript:document.getElementById('{1}').focus();\">{0}</a>";

                            ShowError(string.Format(formatString,
                                currentValidator.ErrorMessage, validatedControl.ClientID));

                            AddErrorCss(validatedControl);
                        }
                        else
                        {
                            RemoveErrorCss(validatedControl);
                        }
                    }
                    else
                    {
                        if (!currentValidator.IsValid)
                        {
                            ShowError(currentValidator.ErrorMessage);
                        }
                    }
                }
            }
        }

        private void RemoveErrorCss(Control errControl)
        {
            if (string.IsNullOrEmpty(ErrorFieldCssClass) == false)
            {
                WebControl werrControl = errControl as WebControl;
                if (werrControl != null)
                {
                    if (werrControl.CssClass.Contains(ErrorFieldCssClass))
                        werrControl.CssClass = werrControl.CssClass.Replace(ErrorFieldCssClass, "");
                }
            }
        }

        private void AddErrorCss(Control errControl)
        {
            if (string.IsNullOrEmpty(ErrorFieldCssClass) == false)
            {
                WebControl werrControl = errControl as WebControl;
                if (werrControl != null)
                {
                    if (werrControl.CssClass.Length == 0)
                        werrControl.CssClass = ErrorFieldCssClass;
                    else
                        werrControl.CssClass += " " + ErrorFieldCssClass;
                }
            }
        }

        private bool IsValidValidationGroup(IValidator currValidator)
        {
            if (UseValidationGroups == false)
                return true;

            BaseValidator currBaseValidator = currValidator as BaseValidator;
            if (currBaseValidator == null)
                return false;

            return IsValidValidationGroup(currBaseValidator.ValidationGroup);
        }

        private bool IsValidValidationGroup(string validationGroup)
        {
            if (UseValidationGroups == false)
                return true;

            string[] excludeGroups = ExcludeValidationGroups.Split(",".ToCharArray());
            foreach (string exGroup in excludeGroups)
            {
                if (exGroup == validationGroup)
                    return false;

                if (exGroup == ".*")
                    return false;

                if (Regex.IsMatch(validationGroup, "^" + exGroup + "$"))
                    return false;
            }

            string[] includeGroups = IncludeValidationGroups.Split(",".ToCharArray());
            foreach (string inGroup in includeGroups)
            {
                if (inGroup == validationGroup)
                    return true;

                if (inGroup == ".*")
                    return true;

                if (Regex.IsMatch(validationGroup, "^" + inGroup + "$"))
                    return true;
            }

            return false;
        }

        public void ShowOK(string msg)
        {
            MessagesToDisplay.Add(new ValidationMessage(PageMessageBoxMode.OK, msg));
        }

        public void ShowInformation(string msg)
        {
            MessagesToDisplay.Add(new ValidationMessage(PageMessageBoxMode.Information, msg));
        }

        public void ShowError(string msg)
        {
            MessagesToDisplay.Add(new ValidationMessage(PageMessageBoxMode.ErrorMessage, msg));
        }

        public void ShowWarning(string msg)
        {
            MessagesToDisplay.Add(new ValidationMessage(PageMessageBoxMode.Warning, msg));
        }

        public void ShowQuestion(string msg)
        {
            MessagesToDisplay.Add(new ValidationMessage(PageMessageBoxMode.Question, msg));
        }

        public void ShowException(Exception ex)
        {
            MessagesToDisplay.Add(new ValidationMessage(PageMessageBoxMode.ErrorMessage, ex.Message));
        }

        public bool HasErrorMessage()
        {
            foreach (ValidationMessage msg in MessagesToDisplay)
            {
                if (msg.Type == PageMessageBoxMode.ErrorMessage)
                    return true;
            }

            return false;
        }
    }
}
