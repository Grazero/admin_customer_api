using System.Net;
using admin_customer_api.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace admin_customer_api.Controllers
{
     [ApiController]
    [Route("api/[controller]")]
    public class DatacustomerController: ControllerBase
    {

        private readonly IDcServices    _dc_service;
        public DatacustomerController(IDcServices dc_service)
        {
            _dc_service = dc_service;
        }

        [HttpGet("getdatacustomer")]
        public async Task<IActionResult> GetDataCustomer()
        {
            var response = new Dictionary<string, object>();

            var data = await _dc_service.GetDataCustomer();

            if(data == null)
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "ไม่พบข้อมูล";
                response["data"] = null;
            }
            else
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "";
                response["data"]= data;
            }

            return Ok(response);
        }

        [HttpPost("getdatacustomerbyid")]

        public async Task<IActionResult> GetDataCustomerById(int itemid)
        {
            var response = new Dictionary<string, object>();

            var data = await _dc_service.GetDataCustomerById(itemid);

            if(data == null)
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "ไม่พบข้อมูล";
                response["data"] = null;
            }
            else
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "";
                response["data"] = data;
            }

            return Ok(response);
        }

        [HttpPost("savedatacustomer")]
        public async Task<IActionResult> SaveDataCustomer(DataCustomer_save dataCustomer)
        {
            var response = new Dictionary<string, object>();

            int result = await _dc_service.SaveDataCustomer(dataCustomer);

            if(result == 0)
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "";
                response["data"] = null;
            }
            else
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "เพิ่มข้อมูลสำเร็จ";
                response["data"] = null;
            }

            return Ok(response);

        }
   
        [HttpGet("getProvice")]
        public async Task<IActionResult> GetProvice()
        {
            var response = new Dictionary<string, object>();

            var data = await _dc_service.GetProvice();

            if(data == null)
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "ไม่พบข้อมูล";
                response["data"] = null;
            }
            else
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "";
                response["data"]= data;
            }

            return Ok(response);
        }

        [HttpPost("getCity")]
        public async Task<IActionResult> GetCity(int province_id)
        {
            var response = new Dictionary<string, object>();

            var data = await _dc_service.GetCity(province_id);

            if(data == null)
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "ไม่พบข้อมูล";
                response["data"] = null;
            }
            else
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "";
                response["data"]= data;
            }

            return Ok(response);
        }

        [HttpPost("getDistrict")]
        public async Task<IActionResult> GetDistrict(int province_id,int city_id)
        {
            var response = new Dictionary<string, object>();

            var data = await _dc_service.GetDistrict(province_id,city_id);

            if(data == null)
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "ไม่พบข้อมูล";
                response["data"] = null;
            }
            else
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "";
                response["data"]= data;
            }

            return Ok(response);
        }

        [HttpPost("getZipcode")]
        public async Task<IActionResult> GetZipcode(int province_id,int city_id,int district_id)
        {
            var response = new Dictionary<string, object>();

            var data = await _dc_service.GetZipcode(province_id,city_id,district_id);

            if(data == null)
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "ไม่พบข้อมูล";
                response["data"] = null;
            }
            else
            {
                response["status"] = HttpStatusCode.OK;
                response["message"] = "";
                response["data"]= data;
            }

            return Ok(response);
        }

    }
}