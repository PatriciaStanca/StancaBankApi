namespace StancaBankApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public partial class AdminController : ControllerBase
{
    private readonly IAdminService adminService;

    public AdminController(IAdminService adminService)
    {
        this.adminService = adminService;
    }
}
