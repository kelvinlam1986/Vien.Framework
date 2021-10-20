using Dapper;
using MinhLam.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MinhLam.Framework.Data.Repo
{
    public class RoleData : BaseData<Role>
    {
        public override int Insert(Role entity)
        {
            string sql = @"INSERT INTO dbo.Role " +
                         "(Name, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy) " +
                         "VALUES(@Name, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy); " +
                         "SELECT CAST(SCOPE_IDENTITY() as int)";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    int id = connection.Query<int>(sql,
                        new
                        {
                            Name = entity.Name,
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

        public override int Remove(Role entity)
        {
            try
            {
                string sql = "DELETE FROM dbo.Role WHERE Id = @Id";
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

        public override int Update(Role entity)
        {
            string sql = "UPDATE dbo.Role " +
                        "SET Name = @Name, " +
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
                            Name = entity.Name,
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

        public List<Role> GetAll()
        {
            string sql = "SELECT * FROM dbo.Role ORDER BY CreatedDate, UpdatedDate";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var role = connection.Query<Role>(sql).ToList();
                    return role;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Role GetById(int id)
        {
            string sql = "SELECT * FROM dbo.Role WHERE Id = @Id";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var role = connection.QueryFirst<Role>(sql, new { Id = id });
                    return role;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Role> GetByUserAccountId(int userAccountId)
        {
            string sql = "SELECT Role.Id, Role.Name, Role.CreatedDate, " +
                             "Role.CreatedBy, Role.UpdatedDate, " +
                             "Role.UpdatedBy " +
                         "FROM Role " +
                         "INNER JOIN RoleUserAccount " +
                            "ON Role.Id = RoleUserAccount.RoleId " +
                         "WHERE AccountId = @AccountId";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var roles = connection.Query<Role>(sql, new { AccountId = userAccountId }).ToList();
                    return roles;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Role> Search(string keyword)
        {

            if (string.IsNullOrWhiteSpace(keyword) == false)
            {
                string sql = "SELECT * FROM dbo.Role WHERE Name like @Keyword ORDER BY CreatedDate, UpdatedDate ";
                try
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        var roles = connection.Query<Role>(sql, new { Keyword = $"%{keyword}%" }).ToList();
                        return roles;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                string sql = "SELECT * FROM dbo.Role ORDER BY CreatedDate, UpdatedDate";
                try
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        var roles = connection.Query<Role>(sql).ToList();
                        return roles;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool CheckExisting(int id)
        {
            var existingItem = GetById(id);
            return existingItem != null;
        }

        public bool CheckExistingName(int id, string name)
        {
            string sql = "SELECT * FROM dbo.Role WHERE Name = @Name AND Id != @Id";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var menuItem = connection.QueryFirstOrDefault<UserAccount>(sql, new { Name = name, Id = id });
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
