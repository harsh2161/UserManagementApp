using UserManagementApp.ExceptionHandler;

namespace UserManagementApp.Validator.SearchSortValidation
{
    public class PageIndexValidation
    {
        public static bool IsPageIndexValid(int pageIndex)
        {
            if (pageIndex < 0)
            {
                throw new ValidationException("Page Index Should Not Less Than 0");
            }
            return true;
        }
    }
}