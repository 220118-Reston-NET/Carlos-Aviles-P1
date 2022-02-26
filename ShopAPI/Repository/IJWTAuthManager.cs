using Dapper;
using ShopAPI;

namespace Repository
{
    public interface IJWTAuthManager
    {
        ResponseModel<string> GenerateJWT(UserModel user);
        ResponseModel<T> Execute_Command<T>(string query, DynamicParameters sp_params);
        Task<ResponseModel<List<T>>> getUserList<T>();
    }
} 