using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Vien.Framework.Data.Entities;

namespace Vien.Framework.Data.Repo
{
    public class UserAccountData : BaseData<UserAccount>
    {
        public UserAccountData(string connectionString) : base(connectionString)
        {
        }

        public UserAccountData()
        {
        }

        public List<UserAccount> Search(string keyword)
        {

            if (string.IsNullOrWhiteSpace(keyword) == false)
            {
                string sql = "SELECT * FROM dbo.UserAccount WHERE WindowAccountName like @Keyword " +
                    "OR FullName like @Keyword ORDER BY CreatedDate, UpdatedDate ";
                try
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        var menuItems = connection.Query<UserAccount>(sql, new { Keyword = $"%{keyword}%" }).ToList();
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
                string sql = "SELECT * FROM dbo.UserAccount ORDER BY CreatedDate, UpdatedDate";
                try
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        var menuItems = connection.Query<UserAccount>(sql).ToList();
                        return menuItems;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<UserAccount> GetAll()
        {
            string sql = "SELECT * FROM dbo.UserAccount ORDER BY CreatedDate, UpdatedDate";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var menuItems = connection.Query<UserAccount>(sql).ToList();
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
            string sql = "SELECT * FROM dbo.UserAccount WHERE Id = @Id";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var menuItem = connection.QueryFirst<UserAccount>(sql, new { Id = id });
                    return menuItem;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override int Insert(UserAccount entity)
        {
            string sql = @"INSERT INTO dbo.UserAccount (WindowAccountName, FullName, Email, IsActive, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy) " +
                        "VALUES(@WindowAccountName, @FullName, @Email, @IsActive, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy)";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    int rowEffected = connection.Execute(sql,
                        new
                        {
                            WindowAccountName = entity.WindowAccountName,
                            FullName = entity.FullName,
                            Email = entity.Email,
                            IsActive = entity.IsActive,
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

        public override int Remove(UserAccount entity)
        {
            try
            {
                string sql = "DELETE FROM dbo.UserAccount WHERE Id = @Id";
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

        public override int Update(UserAccount entity)
        {
            string sql = "UPDATE dbo.UserAccount " +
                        "SET WindowAccountName = @WindowAccountName, " +
                            "FullName = @FullName, " +
                            "Email = @Email, " +
                            "IsActive = @IsActive, " +
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
                            WindowAccountName = entity.WindowAccountName,
                            FullName = entity.FullName,
                            Email = entity.Email,
                            IsActive = entity.IsActive,
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

        public bool CheckExistingName(int id, string windowAccountName)
        {
            string sql = "SELECT * FROM dbo.UserAccount WHERE WindowAccountName = @WindowAccountName AND Id != @Id";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var menuItem = connection.QueryFirstOrDefault<UserAccount>(sql, new { WindowAccountName = id, Name = windowAccountName });
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
