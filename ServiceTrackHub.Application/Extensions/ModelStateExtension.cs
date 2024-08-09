using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace ServiceTrackHub.Application.Extensions
{
    public static class ModelStateExtension
    {

        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            result = modelState.Values
                      .SelectMany(v => v.Errors)
                      .Select(e => e.ErrorMessage)
                      .ToList();

            return result;
        }
    }
}
