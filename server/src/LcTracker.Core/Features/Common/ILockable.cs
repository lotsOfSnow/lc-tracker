namespace LcTracker.Core.Features.Common;


public interface ILockable
{
    public bool IsLocked { get; set; }
}
