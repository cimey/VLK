using Application.Stock;
using Application.Trading;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;
using Model.Requests;
using Model.Responses;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradingController : ControllerBase
    {
        private readonly ITradingService _tradingService;

        public TradingController(ITradingService tradingService)
        {
            _tradingService = tradingService;
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy([FromBody] BuyStockRequest request)
        {
            var order = await _tradingService.BuyStockAsync(request.ClientId, request.StockId, request.Shares);
            return Ok(ApiResponse<StockOrderResponse>.Ok(order, "Stocks bought successfully."));
        }

        [HttpPost("sell")]
        public async Task<IActionResult> Sell([FromBody] SellStockRequest request)
        {
            var order = await _tradingService.SellStockAsync(request.ClientId, request.StockId, request.Shares);
            return Ok(ApiResponse<StockOrderResponse>.Ok(order, "Stocks sold successfully."));
        } 
    }
}
