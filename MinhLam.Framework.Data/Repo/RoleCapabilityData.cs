using Dapper;
using MinhLam.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MinhLam.Framework.Data.Repo
{
    public class RoleCapabilityData : BaseData<RoleCapability>
    {
        public override int Insert(RoleCapability entity)
        {
            string sql = @"INSERT INTO dbo.RoleCapability " +
                        "(RoleId, CapabilityId, AccessFlag, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy) " +
                        "VALUES(@RoleId, @CapabilityId, @AccessFlag, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy)";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    int rowEffected = connection.Execute(sql,
                        new
                        {
                            RoleId = entity.RoleId,
                            CapabilityId = entity.CapabilityId,
                            AccessFlag = entity.AccessFlag,
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

        public override int Remove(RoleCapability entity)
        {
            try
            {
                string sql = "DELETE FROM dbo.RoleCapability WHERE Id = @Id";
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

        public override int Update(RoleCapability entity)
        {
            string sql = "UPDATE dbo.RoleCapability " +
                       "SET RoleId = @RoleId, " +
                           "CapabilityId = @CapabilityId, " +
                           "AccessFlag = @AccessFlag, " +
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
                            CapabilityId = entity.CapabilityId,
                            AccessFlag = entity.AccessFlag,
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

        public List<RoleCapability> GetAll()
        {
            string sql = "SELECT * FROM dbo.RoleCapability ORDER BY CreatedDate, UpdatedDate";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var roleCapability = connection.Query<RoleCapability>(sql).ToList();
                    return roleCapability;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RoleCapability GetById(int id)
        {
            string sql = "SELECT * FROM dbo.RoleCapability WHERE Id = @Id";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var roleCapability = connection.QueryFirst<RoleCapability>(sql, new { Id = id });
                    return roleCapability;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
