using AutoSellCourses.AppData;
using AutoSellCourses.AppData.Cars;
using AutoSellCourses.AppData.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoSellCourses.Controllers
{       [Route("api/contract")]
        [ApiController]
    public class ContractsController : Controller
    {

        


        private readonly AutoSellContext _context;


        public ContractsController(AutoSellContext context)
        {
            _context = context;
        }



        [HttpGet("stats")]
        public IActionResult GetStats() {

            var stats = new
            {
                TotalContracts = _context.Contracts.Count(),
                TodayContracts = _context.Contracts
                    .Count(c => c.ContractDate.Date == DateTime.Today),
                ThisMonthContracts = _context.Contracts
                    .Count(c => c.ContractDate.Year == DateTime.Now.Year
                                && c.ContractDate.Month == DateTime.Now.Month)

            };

            return Ok(stats);
        
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddContract(int id)
        {

             using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                Random rand = new Random(); 
                var currentCar = _context.ClientCars.Find(id);


                if(currentCar == null)
                {
                    return NotFound();
                }
                
                var newContract = new Contract
                {
                    ClientId = currentCar.ClientId,
                    ContractDate = DateTime.Today,
                    DillerId = rand.Next(1,3),
                    CarBrand = currentCar.CarBrand,
                    ManufactureDate = currentCar.ManufactureDate,
                    Mileage = currentCar.Mileage,
                    SalePrice = currentCar.Price,
                    Commission = rand.Next(5,10)
                };


                _context.Contracts.Add(newContract);
                _context.ClientCars.Remove(currentCar);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var response = new AddContractResponse
                {
                    ContractId = newContract.ContractId,
                    Message = $"Контракт на автомобиль {currentCar.CarBrand} был создан"
                };
                return Ok(response);

            }
            catch (Exception ex)
            {

                await transaction.RollbackAsync();
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine($"Inner: {ex.InnerException?.Message}");
                Console.WriteLine($"Stack: {ex.StackTrace}");

                return StatusCode(500, $"Ошибка при добавлении: {ex.Message}");


            }

        }

        
    }
}
