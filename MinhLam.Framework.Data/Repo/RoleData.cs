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
                         "VALUES(@Name, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy)";
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
                            UpdatedBy = entity.UpdatedBy
                        });
                    return rowEffected;
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
    }
}
