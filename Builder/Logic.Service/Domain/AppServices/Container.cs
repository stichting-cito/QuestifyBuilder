namespace Questify.Builder.Logic.Service.Domain.AppServices
{
    public class Container
    {
        public static IGlobalContext GlobalContext => AppServices.GlobalContext.Instance();

        public static IRequestContext RequestContext { get; set; }
    }
}
