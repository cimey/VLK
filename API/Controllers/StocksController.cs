using Application.Stock;
using Microsoft.AspNetCore.Mvc;
using Model.Requests;
using Model.Responses;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController( IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var stocks = await _stockService.GetStocksAsync();


            return Ok(ApiResponse<IEnumerable<StockResponse>>.Ok(stocks, "Stocks fetched successfully."));
        }


        [HttpPatch]
        public async Task<IActionResult> UpdateStockPrice([FromBody] UpdateStockRequest request)
        {
              await _stockService.UpdateStockAsync(request);


            return Ok(ApiResponse<bool>.Ok(true, "Stock updated successfully."));
        }
    }
}
