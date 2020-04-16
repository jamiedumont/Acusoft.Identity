using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Grpc.Core;
using Acusoft.Identity.Protos;

namespace Acusoft.Identity.Server.Services
{
    public class UsersService : Users.UsersBase
    {


        private readonly string _connectionString;
        private readonly ILogger<UsersService> _logger;


        public UsersService(IConfiguration config, ILogger<UsersService> logger)
        {
            _connectionString = config["DatabaseConnectionString"];
            _logger = logger;
        }

        

        public override Task<UserQueryResponse> FindById(FindByIdLookup request, ServerCallContext context)
        {
            _logger.LogInformation($"Requesting user with ID: {request.Id} from the database");
            var response = new UserQueryResponse();
            //using (var conn = new SqlConnection(_connectionString))
            //{
            //    conn.Open();
            //    _logger.LogDebug("Database connection open");

            //    using (var cmd = new SqlCommand("Select * From Web_Third_Parties WHERE Sequence = @Sequence", conn))
            //    {
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Parameters.AddWithValue("@Sequence", request.Id);
            //        using (var reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                user = SqlReaderToUserModel(reader);
            //            }
            //        }
            //    }
            //}

            //_logger.LogInformation($"User with ID {user.Id} and email {user.Email} found, returning to gRPC client");

            return Task.FromResult(response);
        }

        //public override Task<UserQueryResponse> FindByNormalizedName(FindByNormalizedNameLookup request, ServerCallContext context)
        //{
        //    _logger.LogInformation($"Requesting user with normalized username: {request.NormalizedUserName} from the database");
        //    GrpcUser user = new GrpcUser();
        //    using (var conn = new SqlConnection(_connectionString))
        //    {
        //        conn.Open();
        //        _logger.LogDebug("Database connection open");
        //        using (var cmd = new SqlCommand("Select *, dbo.fn_Find_Address(Contact_Code, 1, 0) as FullName  From Web_Third_Parties WHERE E_Mail = @Email", conn))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@Email", request.NormalizedUserName);
        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    user = SqlReaderToUserModel(reader);
        //                }
        //            }
        //        }
        //    }

        //    _logger.LogInformation($"User with ID {user.Id} and email {user.Email} found, returning to gRPC client");
        //    return Task.FromResult(user);
        //}

        //public override Task<UserQueryResponse> FindByNormalizedEmail(FindByNormalizedEmailLookup request, ServerCallContext context)
        //{
        //    _logger.LogInformation($"Requesting user with normalized email: {request.NormalizedEmail} from the database");
        //    UserModel user = new UserModel();
        //    using (var conn = new SqlConnection(_connectionString))
        //    {
        //        conn.Open();
        //        _logger.LogDebug("Database connection open");
        //        using (var cmd = new SqlCommand("SELECT *, dbo.fn_Find_Address(Contact_Code, 1, 0) as FullName  FROM Web_Third_Parties WHERE Normalized_E_Mail = @Email", conn))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@Email", request.NormalizedEmail);
        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    user = SqlReaderToUserModel(reader);
        //                }
        //            }
        //        }
        //    }
        //    _logger.LogInformation($"User with ID {user.Id} and email {user.Email} found, returning to gRPC client");
        //    return Task.FromResult(user);
        //}

        //public override Task<DatabaseOperation> UpdateUser(GrpcUser request, ServerCallContext context)
        //{
        //    _logger.LogInformation($"Updating user with email: {request.Email}");
        //    var response = new DatabaseOperation();
        //    using (var conn = new SqlConnection(_connectionString))
        //    {
        //        conn.Open();
        //        _logger.LogDebug("Database connection open");
        //        using (var cmd = new SqlCommand("UPDATE Web_Third_Parties SET " +
        //            "E_Mail = @Email, " +
        //            "Normalized_E_Mail = @NormalEmail, " +
        //            "E_Mail_Confirmed = @EmailConfirmed, " +
        //            "Password_Hash = @Password, " +
        //            "Two_Factor_Enabled = @TFA, " +
        //            "Access_Failed_Count = @AccessFailedCount, " +
        //            "Concurrency_Stamp = @ConStamp, " +
        //            "Lockout_Enabled = @LockoutEnabled, " +
        //            "Lockout_End = @LockoutEnd, " +
        //            "Security_Stamp = @SecStamp, " +
        //            "Two_Factor_Key = @TwoFactorKey, " +
        //            "Two_Factor_Recovery_Codes = @TwoFactorRecoveryCodes " +
        //            "WHERE Sequence = @Sequence", conn))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            //cmd.Parameters.AddWithValue("@ContactCode", request.ContactCode);
        //            cmd.Parameters.AddWithValue("@Email", request.Email);
        //            cmd.Parameters.AddWithValue("@NormalEmail", request.NormalizedEmail);
        //            cmd.Parameters.AddWithValue("@EmailConfirmed", request.EmailConfirmed);
        //            cmd.Parameters.AddWithValue("@Password", request.PasswordHash);
        //            cmd.Parameters.AddWithValue("@TFA", request.TwoFactorEnabled);
        //            cmd.Parameters.AddWithValue("@AccessFailedCount", request.AccessFailedCount);
        //            cmd.Parameters.AddWithValue("@ConStamp", request.ConcurrencyStamp);
        //            cmd.Parameters.AddWithValue("@LockoutEnabled", request.LockoutEnabled);
        //            cmd.Parameters.AddWithValue("@LockoutEnd", request.LockoutEnd);
        //            cmd.Parameters.AddWithValue("@SecStamp", request.SecurityStamp);
        //            cmd.Parameters.AddWithValue("@Sequence", request.Id);
        //            cmd.Parameters.AddWithValue("@TwoFactorKey", request.TwoFactorKey);
        //            cmd.Parameters.AddWithValue("@TwoFactorRecoveryCodes", request.TwoFactorRecoveryCodes);
        //            var rowsChanged = cmd.ExecuteNonQuery();

        //            _logger.LogInformation($"Rows changed during user update: {rowsChanged}");

        //            if (rowsChanged > 0)
        //            {
        //                response.Successful = true;
        //            }
        //        }
        //    }

        //    return Task.FromResult(response);
        //}

        

        //public override Task<DatabaseOperation> CreateNewUser(GrpcUser request, ServerCallContext context)
        //{
        //    _logger.LogInformation("CreateNewUser called");
        //    var response = new DatabaseOperation();
        //    using (var conn = new SqlConnection(_connectionString))
        //    {
        //        conn.Open();
        //        _logger.LogDebug("Database connection open");
        //        using (var cmd = new SqlCommand("INSERT INTO Web_Third_Parties ( " +
        //            "Contact_Code, E_Mail, Normalized_E_Mail, Password_Hash, Primary_Contact, Posting_Date " +
        //            ") VALUES ( " +
        //            "@ContactCode, @Email, @NormalEmail, @Password, 0, getdate())", conn))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@ContactCode", request.ContactCode);
        //            cmd.Parameters.AddWithValue("@Email", request.Email);
        //            cmd.Parameters.AddWithValue("@NormalEmail", request.NormalizedEmail);
        //            cmd.Parameters.AddWithValue("@Password", request.PasswordHash);
        //            var rowsChanged = cmd.ExecuteNonQuery();
        //            _logger.LogInformation($"Rows changed during CreateNewUser: {rowsChanged}");
        //            if (rowsChanged > 0)
        //            {
        //                response.Created = true;
        //            }
        //        }
        //    }
        //    return Task.FromResult(response);
        //}

        //public override Task<DatabaseOperation> DeleteUser(GrpcUser request, ServerCallContext context)
        //{
        //    _logger.LogInformation($"Deleting user with GUID: {request.GUID} from the database");
        //    var response = new DatabaseOperation();
        //    using (var conn = new SqlConnection(_connectionString))
        //    {
        //        conn.Open();
        //        _logger.LogDebug("Database connection open");
        //        using (var cmd = new SqlCommand("DELETE FROM Web_Third_Parties WHERE GUID = @GUID", conn))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@GUID", request.GUID);
        //            var rowsChanged = cmd.ExecuteNonQuery();

        //            if (rowsChanged > 0)
        //            {
        //                _logger.LogInformation($"User with GUID: {request.GUID} deleted from the database");
        //                response.Status = Grpc.Status.Succeeded;
        //            }
        //        }
        //    }
        //    return Task.FromResult(response);
        //}


        


        private IdentityUser SqlReaderToUserModel(SqlDataReader reader)
        {
            return new IdentityUser
            {
                Id = reader["Id"].ToString(),

                UserName = reader["User_Name"].ToString(),
                NormalizedUserName = reader["Normalized_User_Name"].ToString(),
                NormalizedEmail = reader["Normalized_E_Mail"].ToString(),

                Email = reader["E_Mail"].ToString(),
                EmailConfirmationTime = reader["E_Mail_Confirmation_Time"].ToString(),

                PasswordHash = reader["Password_Hash"].ToString(),
                TwoFactorEnabled = Convert.ToBoolean(reader["Two_Factor_Enabled"]),
                SecurityStamp = reader["Security_Stamp"].ToString(),
                AccessFailedCount = Convert.ToInt32(reader["Access_Failed_Count"])

                //Lockout = new LockoutInfo
                //{
                //    Placeholder = "nothing here!"
                //}
            };
        }

        private Exception NotImplementedException()
        {
            throw new NotImplementedException();
        }
    }
}

