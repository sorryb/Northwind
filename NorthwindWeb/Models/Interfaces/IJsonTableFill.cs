using System.Web.Mvc;

namespace NorthwindWeb.Models.Interfaces
{
    public interface IJsonTableFill
    {
        JsonResult JsonTableFill(int draw, int start, int length);
    }
}
