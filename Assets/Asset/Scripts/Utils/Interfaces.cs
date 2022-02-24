public interface IInteractable
{
    void Interact();
}

public interface IGetable : IInteractable
{
    void GetItem();
}