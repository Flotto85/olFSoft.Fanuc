namespace olFSoft.Focas
{
    public interface IFanucControlReadWriteActionsProvider
    {
        FanucControlReadWriteActionsProvider.ReadWriteAction GetReadWriteActions();
    }
}