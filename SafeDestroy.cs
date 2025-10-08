using UnityEngine;

/// <summary>First deactivate and then destroy</summary>
public static class SafeDestroy
{
    public static void GameObject(GameObject obj)
    {
        obj.SetActive(false);
        Object.Destroy(obj);
    }

    public static void Behaviour(Behaviour beh)
    {
        beh.enabled = false;
        Object.Destroy(beh);
    }
}
