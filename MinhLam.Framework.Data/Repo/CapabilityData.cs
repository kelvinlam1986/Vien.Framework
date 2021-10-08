using Dapper;
using MinhLam.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MinhLam.Framework.Data.Repo
{
    public class CapabilityData : BaseData<Capability>
    {
        public override int Insert(Capability entity)
        {
            string sql = @"INSERT INTO dbo.Capability " +
                         "(Name, MenuItemId, AccessType, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy) " +
                         "VALUES(@Name, @MenuItemId, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy)";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    int rowEffected = connection.Execute(sql,
                        new
                        {
                            Name = entity.Name,
                            MenuItemId = entity.MenuItemId,
                            AccessType = entity.AccessType,
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

        public override int Remove(Capability entity)
        {
            try
            {
                string sql = "DELETE FROM dbo.Capability WHERE Id = @Id";
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

        public override int Update(Capability entity)
        {
            string sql = "UPDATE dbo.Capability " +
                       "SET Name = @Name, " +
                           "MenuItemId = @MenuItemId, " +
                           "AccessType = @AccessType, " +
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
                            MenuItemId = entity.MenuItemId,
                            AccessType = entity.AccessType,
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

        public List<Capability> GetAll()
        {
            string sql = "SELECT * FROM dbo.Capability ORDER BY CreatedDate, UpdatedDate";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var menuItems = connection.Query<Capability>(sql).ToList();
                    return menuItems;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Capability GetById(int id)
        {
            string sql = "SELECT * FROM dbo.UserAccount WHERE Id = @Id";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var capability = connection.QueryFirst<Capability>(sql, new { Id = id });
                    return capability;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
