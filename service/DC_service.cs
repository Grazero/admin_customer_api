
using Dapper;
using System.Data;
using MySqlConnector;  // ใช้ MySqlConnector
using admin_customer_api.models;
using System.Globalization;
namespace admin_customer_api.Service
{
    public class DC_service : IDcServices
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DC_service(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            // ถ้ายังไม่ถึงวันเกิดในปีนี้ ให้ลบ 1
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
        public async Task<int> SaveDataCustomer(DataCustomer_save dataCustomer)
        {
            string sql = "";

            // Parse วันที่แบบปลอดภัย
            DateTime birthDate;
            try
            {
                birthDate = DateTime.ParseExact(dataCustomer.birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                // Fallback ถ้า format ไม่ตรง
                birthDate = DateTime.Parse(dataCustomer.birthday);
            }

            // คำนวณอายุ
            int age = CalculateAge(birthDate);

            if(age < 0)
            {
                age = 0;
            }

            string birthday_string =birthDate.Year.ToString()+"-"+birthDate.Month.ToString()+"-"+birthDate.Day.ToString();
            

           
             List<DataZipcode>  datazipcode = await GetZipcode(dataCustomer.province_id,dataCustomer.city_id,dataCustomer.district_id);

           
           if(datazipcode == null)
            {
                dataCustomer.zipcode_id =0;
            }
            else
            {
             dataCustomer.zipcode_id = datazipcode[0].zipcode_id;   
            }

            sql = "insert into datacustomer (firstname,lastname,birthday,age,address";

            sql +=" ,province_id,city_id,district_id,ZipCode_id,Zipcode)";

            sql += " values (@firstname,@lastname,@birthday,@age,@address";

            sql +=" ,@province_id,@city_id,@district_id,@ZipCode_id,@Zipcode) ;";

            sql += " select last_insert_id() as lastid;";

            try
            {
            using (var connect = new MySqlConnection(_connectionString))
            {
                await connect.OpenAsync();
                return await connect.QueryFirstOrDefaultAsync<int>(sql, new
                {
                    firstname = dataCustomer.firstname,
                    lastname = dataCustomer.lastname,
                    birthday = birthday_string,
                    age = age,
                    address = dataCustomer.Address,
                    province_id = dataCustomer.province_id,
                    city_id = dataCustomer.city_id,
                    district_id = dataCustomer.district_id,
                    ZipCode_id = dataCustomer.zipcode_id,
                    Zipcode = dataCustomer.zipcode


                });

            }

            }
            catch (System.Exception ex)
            {
                return 0;
                
            }

  
        }

        public async Task<List<DataCustomer>> GetDataCustomer()
        {

            string sql = "select * from DataCustomer_view";
            try
            {
                using (var connect = new MySqlConnection(_connectionString))
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
            catch (Exception ex)
            {
                return [];
            }


        }

        public async Task<DataCustomer> GetDataCustomerById(int itemid)
        {
            string sql ="select * from DataCustomer_view where itemid = @itemid";

            using (var connect = new MySqlConnection(_connectionString))
            {
                await connect.OpenAsync();
                var data = await connect.QueryFirstOrDefaultAsync<DataCustomer>(sql, new
                {
                    itemid = itemid
                });
                if (data == null)
                {
                    return null;
                }

                return data;
        }
        }

        public async Task<List<Dataprovince>> GetProvice()
        {
            string sql = "select province_id,province_th,province_en from DataProvice";

            try
            {
                using (var connect = new MySqlConnection(_connectionString))
                {
                    await connect.OpenAsync();
                    var data = await connect.QueryAsync<Dataprovince>(sql);
                    if (data == null)
                    {
                        return [];
                    }

                    return data.ToList();
                }
            }
            catch (System.Exception ex)
            {

                Console.WriteLine("error: " + ex.Message);

                return [];
            }


        }

        public async Task<List<DataCity>> GetCity(int province_id)
        {
            string sql = "select city_id,city_th,city_en from DataCity where province_id = @province_id";

            using (var connect = new MySqlConnection(_connectionString))
            {
                await connect.OpenAsync();
                var data = await connect.QueryAsync<DataCity>(sql, new
                {
                    province_id = province_id
                });
                if (data == null)
                {
                    return [];
                }

                return data.ToList();
            }
        }

        public async Task<List<DataDistrict>> GetDistrict(int province_id, int city_id)
        {
            string sql = "select  district_id,district_th,district_en from DataDistrict where  province_id = @province_id and city_id = @city_id";

            using (var connect = new MySqlConnection(_connectionString))
            {
                await connect.OpenAsync();
                var data = await connect.QueryAsync<DataDistrict>(sql, new
                {
                    province_id = province_id,
                    city_id = city_id
                });
                if (data == null)
                {
                    return [];
                }

                return data.ToList();
            }
        }

        public async Task<List<DataZipcode>> GetZipcode(int province_id, int city_id, int district_id)
        {
            string sql = "select zipcode_id,zipcode from DataZipcode where  province_id = @province_id and city_id = @city_id ";

            sql += " and district_id = @district_id";

            using (var connect = new MySqlConnection(_connectionString))
            {
                await connect.OpenAsync();
                var data = await connect.QueryAsync<DataZipcode>(sql, new
                {
                    province_id = province_id,
                    city_id = city_id,
                    district_id = district_id
                });
                if (data == null)
                {
                    return [];
                }

                return data.ToList();
            }
        }

    }


}