
using Dapper;
using System.Data;
using MySqlConnector;  // ใช้ MySqlConnector
using admin_customer_api.models;
namespace admin_customer_api.Service
{
    public class DC_service:IDcServices
    {
         private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DC_service( IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
       public async Task<int> SaveDataCustomer(DataCustomer_save dataCustomer)
        {
            string sql ="";

            sql ="insert into datacustomer (firstname,lastname,birthday,address) ";

            sql +=" values(@firstname,@lastname,@birthday,@address) ;";

            sql +=" select last_insert_id() as lastid;";

            using (var connect =new MySqlConnection(_connectionString))
            {
                await connect.OpenAsync();
                return await connect.QueryFirstOrDefaultAsync<int>(sql,new{
                    firstname = dataCustomer.firstname,
                    lastname=dataCustomer.lastname,
                    birthday=dataCustomer.birthday,
                    address=dataCustomer.Address
                });
                
            }

        }
   
        public async Task<List<DataCustomer>> GetDataCustomer()
        {
            
            string sql ="select itemid,firstname,lastname,birthday,address from datacustomer";

            using (var connect =new MySqlConnection(_connectionString))
            {
                await connect.OpenAsync();
                var data = await connect.QueryAsync<DataCustomer>(sql);
                if (data == null)
                {
                    return [];
                }
                
                return data.ToList();
            }

        }
    }
}