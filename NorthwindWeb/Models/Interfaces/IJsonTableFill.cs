using System.Web.Mvc;

namespace NorthwindWeb.Models.Interfaces
{
    public interface IJsonTableFill
    {
        JsonResult JsonTableFill(string search = "");
    }
}
