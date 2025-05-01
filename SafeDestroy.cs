using UnityEngine;

/// <summary>First deactivate and then destroy</summary>
public static class SafeDestroy
{
    private static void Destroy(Object obj) => Object.Destroy(obj);

    public static void GameObject(GameObject obj)
    {
        obj.SetActive(false);
        Destroy(obj);
    }

    public static void Behaviour(Behaviour beh)
    {
        beh.enabled = false;
        Destroy(beh);
    }

    /// <summary>Safely deactivate a <see cref="UnityEngine.Component"/>, prefer to use <see cref="Behaviour(Behaviour)"/></summary>
    public static void Component<T>(T obj) where T : Component => Component(obj, "enabled");

    /// <summary>Safely deactivate a <see cref="UnityEngine.Component"/>, prefer to use <see cref="Behaviour(Behaviour)"/></summary>
    /// <param name="fieldName">The name of the boolean in <paramref name="obj"/></param>
    public static void Component<T>(T obj, string fieldName) where T : Component
    {
        System.Reflection.PropertyInfo info = typeof(T).GetProperty(fieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        if (info != null && info.PropertyType == typeof(bool))
        {
            info.SetValue(obj, false);
        } else throw new System.Exception($"No boolean property '{fieldName}' was found");
        Destroy(obj);
    }
}
