using AutoSellCourses.AppData;
using AutoSellCourses.AppData.Cars;
using AutoSellCourses.AppData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoSellCourses.Controllers
{
    [Route("api/clientcars")]
    [ApiController]
    public class ClientCarsController : ControllerBase
    {
        private readonly AutoSellContext _context;

        public ClientCarsController(AutoSellContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var clientcar = _context
                .Database
                .SqlQuery<ClientCarsResponse>($"""
                SELECT	car.clientcar_id as [Id],	
                        c.client_name as [Client],
                        c.client_number as [ClientNumber],
                        car.car_brand as [CarBrand],
                        FORMAT(car.mileage, N'0 км') as [Mileage],
                        FORMAT(car.manufacture_date, N'yyyy г.') as [ManufactureDate],
                        car.description as [Description],
                        car.color_car as [CarsColor],
                        car.transmission_type as [TransmissionType],
                        car.engine_capacity as [EngineCapacity],
                        FORMAT( ROUND(car.price, 0), N'C', N'ru-RU') as [Price]
                        FROM [ClientCars] as [car]
                        JOIN [Clients] as [c] 
                			ON [car].[client_id] = [c].[client_id]

                """).ToList();

            return Ok(clientcar);


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClientCars(int id)
        {
            var clientcar = _context.ClientCars.Find(id);

            if (clientcar is null)
            {
                return NotFound();
            }

            _context.ClientCars.Remove(clientcar);
            _context.SaveChanges();

            return NoContent();

        }


        [HttpPost("with-clients")]
        public async Task<IActionResult> AddClientWithCars([FromBody] AddClientWithCarRequest request)
        {

            if(request == null || request.ClientCar == null || request.Client == null)
                return BadRequest("Невалидное тело запроса");

            

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var newClient = new Client
                {
                    ClientLastName = request.Client.ClientLastName,
                    ClientName = request.Client.ClientName,
                    ClientMiddleName = request.Client.ClientMiddleName,
                    ClientTown = request.Client.ClientTown,
                    ClientAddress = request.Client.ClientAddress,
                    ClientNumber = request.Client.ClientNumber


                };

                _context.Clients.Add(newClient);
                await _context.SaveChangesAsync();

                var newClientsCar = new ClientCar
                {
                    ClientId = newClient.ClientId,
                    CarBrand = request.ClientCar.CarBrand,
                    Mileage = request.ClientCar.Mileage,
                    ManufactureDate = request.ClientCar.ManufactureDate,
                    Description = request.ClientCar.Description,
                    ColorCar = request.ClientCar.ColorCar,
                    TransmissionType = request.ClientCar.TransmissionType,
                    EngineCapacity = request.ClientCar.EngineCapacity,
                    Price = request.ClientCar.Price,
                    Client = null

                };

                _context.ClientCars.Add(newClientsCar);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var response = new AddClientWithCarResponse
                {
                    ClientId = newClient.ClientId,
                    ClientCarId = newClientsCar.ClientcarId,
                    Message = "Клиент и автомобиль зарегестрированы"
                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Ошибка при добавлении: {ex.Message}");
            }




        }
    }
}
