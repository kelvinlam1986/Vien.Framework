<%@ Page Title="" Language="C#" MasterPageFile="~/EditPage.master" AutoEventWireup="true" CodeBehind="EditRole.aspx.cs" Inherits="Vien.Framework.Web.EditRole" %>

<%@ MasterType VirtualPath="~/EditPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PanelHeadingHolder" runat="server">
    Cập nhật vai trò
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EditInfoHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <label for="txtRoleName">Tên tài khoản:</label>
                <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control" />
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>Phân quyền cho vai trò này</h5>
                </div>
                <div class="panel-body">
                    <asp:Table ID="tblCapabilities" runat="server" CssClass="tablestyle">
                    </asp:Table>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label ID="lblUsers" runat="server" Text="Users"></asp:Label>
                        <asp:ListBox ID="lstUnSelectedUsers" SelectionMode="Multiple" Rows="10" CssClass="form-control" runat="server"></asp:ListBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <div class="btn-group-vertical role-button-group">
                            <asp:Button ID="btnMoveToSelected" runat="server" Text=">" CssClass="btn btn-primary" OnClick="btnMoveToSelected_Click" />
                            <asp:Button ID="btnMoveAllToSelected" runat="server" Text=">>" CssClass="btn btn-primary" OnClick="btnMoveAllToSelected_Click" />
                            <asp:Button ID="btnMoveToUnSelected" runat="server" Text="<" CssClass="btn btn-primary"  OnClick="btnMoveToUnSelected_Click"/>
                            <asp:Button ID="btnMoveAllToUnSelected" runat="server" Text="<<" CssClass="btn btn-primary" OnClick="btnMoveAllToUnSelected_Click" />
                        </div>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Users trong vai trò"></asp:Label>
                        <asp:ListBox ID="lstSelectedUsers" SelectionMode="Multiple" Rows="10" CssClass="form-control" runat="server"></asp:ListBox>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
