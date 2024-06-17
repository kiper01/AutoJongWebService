using System.Linq;
using AutoJongWebService.Models;

public static class QuizExtension
{
    public static IQueryable<CarItem> ApplyQuizFilter(this IQueryable<CarItem> carItems, QuizModel quiz)
    {
        return carItems.Where(c =>
            c.Year >= quiz.YearFrom &&
            c.Year <= quiz.YearTo &&
            c.Price >= quiz.PriceFrom &&
            c.Price <= quiz.PriceTo &&
            c.Fuel == quiz.FuelType &&
            c.Country == quiz.CountryType &&
            c.Body == quiz.BodyType &&
            c.Gearbox == quiz.GearboxType)
            .Take(30);
    }
}
