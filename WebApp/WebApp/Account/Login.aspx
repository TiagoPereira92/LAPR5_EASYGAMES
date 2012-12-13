<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApp.Account.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 543px; z-index: 1; left: 443px; top: 497px; position: absolute; width: 1201px">
   
        <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Default.aspx"></asp:Login>

    </div>
</asp:Content>
