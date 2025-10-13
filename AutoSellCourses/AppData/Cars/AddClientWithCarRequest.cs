using AutoSellCourses.AppData.Models;

namespace AutoSellCourses.AppData.Cars
{
    public class AddClientWithCarRequest
    {
        public Client Client { get; set; }
        public ClientCar ClientCar { get; set; }

    }
}
