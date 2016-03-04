using UnityEngine;
using System.Collections;
/// <summary>
/// class for all interactables in the environment
/// </summary>
public interface IClicker
{
    /// <summary>
    /// when hovering the object
    /// </summary>
    void OnHover();
    /// <summary>
    /// when no longer hovering the object
    /// </summary>
    void OnExitHover();
    /// <summary>
    /// when clicking the object - interacting 
    /// </summary>
    void EndTurn();
}
