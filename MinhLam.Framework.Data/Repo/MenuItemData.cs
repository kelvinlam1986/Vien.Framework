﻿using System;
using System.Data.SqlClient;
using MinhLam.Framework.Data.Entities;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace MinhLam.Framework.Data.Repo
{
    public class MenuItemData : BaseData<MenuItem>
    {
        public MenuItemData(string connectionString) : base(connectionString)
        {
        }

        public MenuItemData()
        {
        }

        public List<MenuItem> Search(string keyword)
        {
           
            if (string.IsNullOrWhiteSpace(keyword)  == false)
            {
                string sql = "SELECT * FROM dbo.MenuItem WHERE MenuItemName like @MenuItemName OR DisplayName like @MenuItemName ORDER BY ParentMenuItemId, DisplaySequence ";
                try
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        var menuItems = connection.Query<MenuItem>(sql, new { MenuItemName = $"%{keyword}%" }).ToList();
                        return menuItems;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                string sql = "SELECT * FROM dbo.MenuItem ORDER BY ParentMenuItemId, DisplaySequence";
                try
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        var menuItems = connection.Query<MenuItem>(sql).ToList();
                        return menuItems;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<MenuItem> GetAll()
        {
            string sql = "SELECT * FROM dbo.MenuItem ORDER BY ParentMenuItemId, DisplaySequence";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var menuItems = connection.Query<MenuItem>(sql).ToList();
                    return menuItems;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MenuItem GetById(int id)
        {
            string sql = "SELECT * FROM dbo.MenuItem WHERE Id = @Id";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var menuItem = connection.QueryFirst<MenuItem>(sql, new { Id = id });
                    return menuItem;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override int Insert(MenuItem entity)
        {
            string sql = @"INSERT INTO dbo.MenuItem (MenuItemName, DisplayName, Description, Url, ParentMenuItemId, DisplaySequence, IsAlwaysEnabled, Icon, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy) " +
                        "VALUES(@MenuItemName, @DisplayName, @Description, @Url, @ParentMenuItemId, @DisplaySequence, @IsAlwaysEnabled, @Icon, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy); " +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    int id = connection.Query<int>(sql,
                        new
                        {
                            MenuItemName = entity.MenuItemName,
                            DisplayName = entity.DisplayName,
                            Description = entity.Description,
                            Url = entity.Url,
                            ParentMenuItemId = entity.ParentMenuItemId,
                            DisplaySequence = entity.DisplaySequence,
                            IsAlwaysEnabled = entity.IsAlwaysEnabled,
                            Icon = entity.Icon,
                            CreatedDate = entity.CreatedDate,
                            CreatedBy = entity.CreatedBy,
                            UpdatedDate = entity.UpdatedDate,
                            UpdatedBy = entity.UpdatedBy
                        }).Single();
                    return id;
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public override int Remove(MenuItem entity)
        {
            try
            {
                string sql = "DELETE FROM dbo.MenuItem WHERE Id = @Id";
                using (var connection = new SqlConnection(ConnectionString))
                {
                    int rowEffected = connection.Execute(sql,
                        new
                        {
                            Id = entity.Id,
                        });
                    return rowEffected;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public override int Update(MenuItem entity)
        {
            string sql = "UPDATE dbo.MenuItem " +
                        "SET MenuItemName = @MenuItemName, " +
                            "DisplayName = @DisplayName, " +
                            "Description = @Description, " +
                            "Url = @Url, " +
                            "ParentMenuItemId = @ParentMenuItemId, " +
                            "DisplaySequence = @DisplaySequence, " +
                            "IsAlwaysEnabled = @IsAlwaysEnabled, " +
                            "Icon = @Icon, " +
                            "CreatedDate = @CreatedDate, " +
                            "CreatedBy = @CreatedBy, " +
                            "UpdatedDate = @UpdatedDate, " +
                            "UpdatedBy = @UpdatedBy " +
                       "WHERE Id = @Id";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    int rowEffected = connection.Execute(sql,
                        new
                        {
                            MenuItemName = entity.MenuItemName,
                            DisplayName = entity.DisplayName,
                            Description = entity.Description,
                            Url = entity.Url,
                            ParentMenuItemId = entity.ParentMenuItemId,
                            DisplaySequence = entity.DisplaySequence,
                            IsAlwaysEnabled = entity.IsAlwaysEnabled,
                            Icon = entity.Icon,
                            CreatedDate = entity.CreatedDate,
                            CreatedBy = entity.CreatedBy,
                            UpdatedDate = entity.UpdatedDate,
                            UpdatedBy = entity.UpdatedBy,
                            Id = entity.Id
                        });
                    return rowEffected;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckExisting(int id)
        {
            var existingItem = GetById(id);
            return existingItem != null;
        }

        public bool CheckExistingName(int id, string name)
        {
            string sql = "SELECT * FROM dbo.MenuItem WHERE MenuItemName = @Name AND Id != @Id";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var menuItem = connection.QueryFirstOrDefault<MenuItem>(sql, new { Id = id, Name = name });
                    return menuItem != null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
