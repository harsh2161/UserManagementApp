using UserManagementApp.ExceptionHandler;

namespace UserManagementApp.Validator.SearchSortValidation
{
    public class PageSizeValidation
    {
        public static bool IsPageSizeValid(int pageSize)
        {
            if (pageSize <= 0 || pageSize > 50)
            {
                throw new ValidationException("Page Size Should between 0 To 50");
            }
            return true;
        }
    }
}