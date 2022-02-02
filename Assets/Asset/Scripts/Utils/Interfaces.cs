using System.Collections;
using System.Collections.Generic;

public interface IInteractable
{
    void Interact();
}

public interface IGetable : IInteractable
{
    void getItem();
}