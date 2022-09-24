using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void BaseInteract(Farmer farmer);
    public void ShowInteractTooltip(Farmer farmer, bool show);
}
