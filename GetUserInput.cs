
namespace ORM
{
    public class GetUserInput
    {        
        public static bool ConvertInput(string input)
        {
            if (input != null)
            {
                switch (input.ToLower())
                {
                    case "yes":
                    case "true":
                    case "indeed":
                    case "please":
                        return true;
                    default:
                        return false;
                }
            }
            return false;
        }
    }
}
