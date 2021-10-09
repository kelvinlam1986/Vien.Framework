<%@ Page Title="" Language="C#" MasterPageFile="~/EditGrid.master" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="Vien.Framework.Web.RoleList" %>
<%@ Register Assembly="MinhLam.Framework.WebControls" Namespace="MinhLam.Framework.WebControls" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/EditGrid.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PanelHeadingHolder" runat="server">
    Danh sách vai trò
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="GridHolder" runat="server">
     <cc1:CustomGridView ID="cgvRoles" runat="server" OnRowDataBound="CustomGridView1_RowDataBound"></cc1:CustomGridView>
</asp:Content>
