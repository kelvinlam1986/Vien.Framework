using System;
using System.Data.SqlClient;
using Vien.Framework.Data.Entities;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Vien.Framework.Data
{
    public class MenuItemData : BaseData<MenuItem>
    {
        public MenuItemData(string connectionString) : base(connectionString)
        {
        }

        public MenuItemData()
        {
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

        public UserAccount GetById(int id)
        {
            string sql = "SELECT * FROM dbo.MenuItem WHERE Id = @Id";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var account = connection.QueryFirst<UserAccount>(sql, new { Id = id });
                    return account;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override int Insert(MenuItem entity)
        {
            string sql = @"INSERT INTO dbo.MenuItem (MenuItemName, Description, Url, ParentMenuItemId, DisplaySequence, IsAlwaysEnabled, Icon, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy) " +
                        "VALUES(@MenuItemName, @Description, @Url, @ParentMenuItemId, @DisplaySequence, @IsAlwaysEnabled, @Icon, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy)";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    int rowEffected = connection.Execute(sql,
                        new
                        {
                            MenuItemName = entity.MenuItemName,
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
                        });
                    return rowEffected;
                }

            }
            catch (Exception)
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
    }
}
