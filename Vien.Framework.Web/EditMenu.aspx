<%@ Page Title="" Language="C#" MasterPageFile="~/EditPage.master" AutoEventWireup="true" CodeBehind="EditMenu.aspx.cs" Inherits="Vien.Framework.Web.EditMenu" %>
<%@ MasterType virtualPath="~/EditPage.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="PanelHeadingHolder" runat="server">
    Cập nhật Menu
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="EditInfoHolder" runat="server">
    <div class="form-group">
        <label for="txtMenuItemName">Tên menu:</label>
        <asp:TextBox ID="txtMenuItemName" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label for="txtDescription">Mô tả:</label>
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label for="txtUrl">Url:</label>
        <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label for="txtDisplaySequence">Thứ tự:</label>
        <asp:DropDownList ID="ddlSequence" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>
    <div class="form-group">
        <label for="ddlParentMenuItemId">Menu cha:</label>
        <asp:DropDownList ID="ddlParentMenuItemId" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>
    <div class="form-group">
        <label for="txtIcon">Icon:</label>
        <asp:TextBox ID="txtIcon" runat="server" CssClass="form-control" />
    </div>
    <div class="checkbox">
        <label>
            <asp:CheckBox ID="ckIsAlwaysEnabled" runat="server" />
            Luôn hiển thị</label>
    </div>
    
</asp:Content>
