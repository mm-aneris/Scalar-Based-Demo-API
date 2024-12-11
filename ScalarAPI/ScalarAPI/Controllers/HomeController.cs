using Microsoft.AspNetCore.Mvc;
using ScalarAPI;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private static List<Car> Cars = new List<Car>
    {
        new Car { Id = 1, Make = "Toyota", Model = "Corolla", Year = 2020 },
        new Car { Id = 2, Make = "Honda", Model = "Civic", Year = 2021 }
    };

    // Get all cars
    [HttpGet]
    public IActionResult GetAllCars()
    {
        return Ok(Cars);
    }

    // Get a car by ID
    [HttpGet("{id}")]
    public IActionResult GetCarById(int id)
    {
        var car = Cars.FirstOrDefault(c => c.Id == id);
        if (car == null)
            return NotFound(new { Message = "Car not found" });

        return Ok(car);
    }

    // Create a new car
    [HttpPost]
    public IActionResult CreateCar([FromBody] Car newCar)
    {
        if (newCar == null)
            return BadRequest(new { Message = "Invalid car data" });

        newCar.Id = Cars.Count > 0 ? Cars.Max(c => c.Id) + 1 : 1;
        Cars.Add(newCar);
        return CreatedAtAction(nameof(GetCarById), new { id = newCar.Id }, newCar);
    }

    // Update an existing car
    [HttpPut("{id}")]
    public IActionResult UpdateCar(int id, [FromBody] Car updatedCar)
    {
        var car = Cars.FirstOrDefault(c => c.Id == id);
        if (car == null)
            return NotFound(new { Message = "Car not found" });

        car.Make = updatedCar.Make;
        car.Model = updatedCar.Model;
        car.Year = updatedCar.Year;
        return Ok(car);
    }

    // Delete a car
    [HttpDelete("{id}")]
    public IActionResult DeleteCar(int id)
    {
        var car = Cars.FirstOrDefault(c => c.Id == id);
        if (car == null)
            return NotFound(new { Message = "Car not found" });

        Cars.Remove(car);
        return NoContent();
    }
}
