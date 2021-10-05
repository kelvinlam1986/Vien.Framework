<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/EditGrid.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Vien.Framework.Web._Default" %>

<%@ Register Assembly="View.Framework.WebControls" Namespace="View.Framework.WebControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/EditGrid.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PanelHeadingHolder" runat="server">
    Danh sách menu
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="GridHolder" runat="server">
   <cc1:CustomGridView ID="CustomGridView1" runat="server" OnRowDataBound="CustomGridView1_RowDataBound"></cc1:CustomGridView>
</asp:Content>
