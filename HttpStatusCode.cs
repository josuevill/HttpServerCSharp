using System.ComponentModel.DataAnnotations;

public enum HttpStatusCode
{
    [Display(Name = "Ok")]
    Ok = 200,
    [Display(Name = "Not Implemented")]
    NotImplemented = 501,
    [Display(Name = "Not Found")]
    NotFound = 404,
}