using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Vien.Framework.Data.Entities;


namespace Vien.Framework.Data
{
    public class UserAccountData : BaseData<UserAccount>
    {
        public UserAccountData(string connectionString) : base(connectionString)
        {
        }

        public UserAccountData()
        {
        }

        public List<UserAccount> GetAll()
        {
            string sql = "SELECT * FROM dbo.Accounts";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var accounts = connection.Query<UserAccount>(sql).ToList();
                    return accounts;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserAccount GetByWindowAccountName(string windowAccountName)
        {
            string sql = "SELECT * FROM dbo.Accounts WHERE WindowAccountName = @WindowAccountName";

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var account = connection.QueryFirst<UserAccount>(sql, new { WindowAccountName = windowAccountName });
                    return account;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override int Insert(UserAccount account)
        {
            string sql = @"INSERT INTO dbo.Accounts (WindowAccountName, FullName, Email, IsActive, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy) " +
                        "VALUES(@WindowAccountName, @FullName, @Email, @IsAcvice, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy)";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    int rowEffected = connection.Execute(sql, 
                        new { 
                                WindowAccountName = account.WindowAccountName,
                                FullName = account.FullName,
                                Email = account.Email,
                                IsActive = account.IsActive,
                                CreatedDate = account.CreatedDate,
                                CreatedBy = account.CreatedBy,
                                UpdatedDate = account.UpdatedDate,
                                UpdatedBy = account.UpdatedBy
                            });
                    return rowEffected;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public override int Update(UserAccount account)
        {
            string sql = "UPDATE dbo.Accounts " +
                         "SET FullName = @FullName, " +
                             "Email = @Email, " +
                             "IsActive = @IsActive, " +
                             "CreatedDate = @CreatedDate " +
                             "CreatedBy = @CreatedBy, " +
                             "UpdatedDate = @UpdatedDate, " +
                             "UpdatedBy = @UpdatedBy " +
                        "WHERE WindowAccountName = @WindowAccountName";
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    int rowEffected = connection.Execute(sql,
                        new
                        {
                            FullName = account.FullName,
                            Email = account.Email,
                            IsActive = account.IsActive,
                            CreatedDate = account.CreatedDate,
                            CreatedBy = account.CreatedBy,
                            UpdatedDate = account.UpdatedDate,
                            UpdatedBy = account.UpdatedBy,
                            WindowAccountName = account.WindowAccountName,
                        });
                    return rowEffected;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        public override int Remove(UserAccount account)
        {
            try
            {
                string sql = "DELETE FROM dbo.Accounts WHERE WindowAccountName = @WindowAccountName";
                using (var connection = new SqlConnection(ConnectionString))
                {
                    int rowEffected = connection.Execute(sql,
                        new
                        {
                            WindowAccountName = account.WindowAccountName,
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
