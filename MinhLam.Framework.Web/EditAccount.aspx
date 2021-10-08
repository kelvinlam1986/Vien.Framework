<%@ Page Title="" Language="C#" MasterPageFile="~/EditPage.master" AutoEventWireup="true" CodeBehind="EditAccount.aspx.cs" Inherits="MinhLam.Framework.Web.EditAccount" %>
<%@ MasterType virtualPath="~/EditPage.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="PanelHeadingHolder" runat="server">
    Cập nhật thông tin tài khoản
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EditInfoHolder" runat="server">
     <div class="form-group">
        <label for="txtWindowAccountName">Tên tài khoản:</label>
        <asp:TextBox ID="txtWindowAccountName" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label for="txtFullName">Họ tên:</label>
        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label for="txtEmail">Email:</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
    </div>
    <div class="checkbox">
        <label>
            <asp:CheckBox ID="ckIsActive" runat="server" />
            Kích hoạt</label>
    </div>
</asp:Content>
