using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void BaseInteract(Farmer farmer);
    public void ShowBaseInteractTooltip(Farmer farmer, bool show);
    public void SpecialInteract(Farmer farmer);
    public void ShowSpecialInteractTooltip(Farmer farmer, bool show);
}
