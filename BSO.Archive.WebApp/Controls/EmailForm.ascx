<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailForm.ascx.cs" Inherits="BSO.Archive.WebApp.Controls.EmailForm" %>

<asp:HiddenField ID="searchIdForEmail" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="searchTypeForEmail" runat="server" ClientIDMode="Static" />

<div class="modalWrap">
    <div class="modal">
        <a href="#" class="close">x</a>
        <h2>Share Search Results via Email</h2>
        <label>To</label>
        <asp:TextBox ID="toName" runat="server" placeholder="Recipient Name" />
        
        <%--<asp:RequiredFieldValidator ID="toNameValidator" runat="server" ControlToValidate="toName" />--%>
        <asp:TextBox ID="toEmail" runat="server" placeholder="Recipient Email" Type="Email" ClientIDMode="Static" />
            
        <%--<asp:RegularExpressionValidator ID="recipientEmailValidate" runat="server" ErrorMessage="Please enter a valid recipient email address" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ControlToValidate="toEmail" />--%>
        <label>From</label>
        <asp:TextBox ID="fromName" runat="server" placeholder="Sender Name" />
        <%--<asp:RequiredFieldValidator ID="fromNameValidator" runat="server" ControlToValidate="fromName" />--%>
        <asp:TextBox ID="fromEmail" runat="server" placeholder="Sender Email" Type="Email" ClientIDMode="Static" />
        <%--<asp:CustomValidator runat="server" ID="fromEmailCustom" ErrorMessage="Please enter a valid sender email address" ControlToValidate="fromEmail" ClientValidationFunction="validateEmail" />--%>

        <%--<asp:RegularExpressionValidator ID="senderEmailValidate" runat="server" ErrorMessage="Please enter a valid sender email address" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ControlToValidate="fromEmail" />--%>
        <label>Message</label>
        <asp:TextBox ID="message" runat="server" TextMode="MultiLine" />
        <asp:Button ID="sendEmail" runat="server" Text="Send" CssClass="btn" OnClientClick="return validateEmail()" />
        
        <%--<asp:ValidationSummary runat="server" ID="summaryValidation" ShowSummary="true" />--%> 
        <div>
            <label id="emailErrorMessage" style="display:none;">
                Please enter both sender and recipient email addresses.
            </label>
        </div>
    </div>
    <div class="modalOverlay"></div>
</div>

<script src="../Scripts/ShareSearch.js"></script>
