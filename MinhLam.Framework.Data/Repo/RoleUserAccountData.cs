using Dapper;
using MinhLam.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MinhLam.Framework.Data.Repo
{
    public class RoleUserAccountData : BaseData<RoleUserAccount>
    {
        public override int Insert(RoleUserAccount entity)
        {
            string sql = @"INSERT INTO dbo.RoleUserAccount " +
                        "(RoleId, AccountId, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy) " +
                        "VALUES(@RoleId, @AccountId, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy); " +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    int id = connection.Query<int>(sql,
                        new
                        {
                            RoleId = entity.RoleId,
                            AccountId = entity.AccountId,
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

        public override int Remove(RoleUserAccount entity)
        {
            try
            {
                string sql = "DELETE FROM dbo.RoleUserAccount WHERE Id = @Id";
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

        public override int Update(RoleUserAccount entity)
        {
            string sql = "UPDATE dbo.RoleUserAccount " +
                       "SET RoleId = @RoleId, " +
                           "AccountId = @AccountId, " +
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
                            RoleId = entity.RoleId,
                            AccountId = entity.AccountId,
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

        public List<RoleUserAccount> GetAll()
        {
            string sql = "SELECT * FROM dbo.RoleUserAccount ORDER BY CreatedDate, UpdatedDate";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var roleUserAccount = connection.Query<RoleUserAccount>(sql).ToList();
                    return roleUserAccount;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RoleUserAccount GetById(int id)
        {
            string sql = "SELECT * FROM dbo.RoleUserAccount WHERE Id = @Id";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var roleUserAccount = connection.QueryFirst<RoleUserAccount>(sql, new { Id = id });
                    return roleUserAccount;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RoleUserAccount> GetByRoleId(int roleId)
        {
            string sql = "SELECT * FROM dbo.RoleUserAccount WHERE RoleId = @RoleId ORDER BY CreatedDate, UpdatedDate";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var roleUserAccount = connection.Query<RoleUserAccount>(sql, new { RoleId = roleId }).ToList();
                    return roleUserAccount;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RoleUserAccount> GetByAccountId(int accountId)
        {
            string sql = "SELECT * FROM dbo.RoleUserAccount WHERE AccountId = @AccountId ORDER BY CreatedDate, UpdatedDate";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var roleUserAccount = connection.Query<RoleUserAccount>(sql, new { AccountId = accountId }).ToList();
                    return roleUserAccount;
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

        public bool CheckRoleInUse(int roleId)
        {
            var roleUserAccounts = GetByRoleId(roleId);
            if (roleUserAccounts.Count > 0)
            {
                return true;
            }

            return false;
        }

        public bool CheckAccountInUse(int accountId)
        {
            var roleCapabilities = GetByAccountId(accountId);
            if (roleCapabilities.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
