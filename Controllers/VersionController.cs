using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using webapp.Models;

namespace webapp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VersionController : ControllerBase
{
    // Get api/values
    [HttpGet]
    public ActionResult<string> Get()
    {
        var versionInfo = new Models.Version
        {
            Company = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCompanyAttribute>().Company,
            Product = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product,
            ProductVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion,
        };
        
        return Ok(versionInfo);
    }
}