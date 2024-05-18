namespace LcTracker.Core.Auth;

public class GetCurrentUserId : IGetCurrentUserId
{
    public static Guid Id = new Guid("018eb88f-3667-7787-9ff4-6024332b04b9");

    public Guid Execute()
    {
        return Id;
    }
}
