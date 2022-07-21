using VehicleServiceApp.Models;

namespace VehicleServiceApp
{
    public static class VehicleValidation
    {
        public static bool Validate(Vehicle? vehicle)
        {
            return ValidateMake(vehicle?.Make)
                && ValidateModel(vehicle?.Model)
                && ValidateYear(vehicle?.Year);
        }

        private static bool ValidateMake(string? make) => !string.IsNullOrWhiteSpace(make);
        private static bool ValidateModel(string? model) => !string.IsNullOrWhiteSpace(model);
        private static bool ValidateYear(int? year) => year >= 1950 && year <= 2050;
    }
}
