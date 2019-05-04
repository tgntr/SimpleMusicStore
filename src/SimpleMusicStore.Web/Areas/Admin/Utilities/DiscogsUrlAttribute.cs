using SimpleMusicStore.Web.Areas.Admin.Models;
using SimpleMusicStore.Web.Areas.Admin.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class DiscogsUrlAttribute : ValidationAttribute
{
    private string _discogsUrl;

    public DiscogsUrlAttribute()
    {
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        _discogsUrl = ((AddRecordBindingModel)validationContext.ObjectInstance).DiscogsUrl;

        if (!DiscogsUtilities.IsValidDiscogsUrl(_discogsUrl))
        {
            return new ValidationResult(ErrorMessage);
        }
        return ValidationResult.Success;
    }
}