using System.Runtime.CompilerServices;

namespace GoDate.API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly dob)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - dob.Year;

            // Adjust if the birthday hasn't occurred yet this year
            if (dob > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
