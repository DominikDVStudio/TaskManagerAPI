using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerApi.Controllers;

public class BaseController : ControllerBase
{
    protected int CurrentLoggedUserId
    {
        get
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                throw new Exception("User ID claim not found.");

            return int.Parse(claim.Value);
        }
    }
}