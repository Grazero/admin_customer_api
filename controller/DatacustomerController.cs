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
   
    }
}