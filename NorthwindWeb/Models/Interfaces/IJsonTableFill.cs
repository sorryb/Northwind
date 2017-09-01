using System.Web.Mvc;

namespace NorthwindWeb.Models.Interfaces
{
    public interface IJsonTableFillServerSide
    {
        JsonResult JsonTableFill(int draw, int start, int length);
    }

    public interface IJsonTableFill
    {
        JsonResult JsonTableFill();
    }
}
