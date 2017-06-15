using UnityEngine;

public static class ComponentExtensions
{
    public static bool TryGetComponent<T>(this Component component, out T tComponent)
        where T : Component
    {
        tComponent = component.GetComponent<T>();
        return tComponent != null;
    }
}