using System.Collections.Generic;
using Card;
using UnityEngine;

public class ContainerRegistry
{
    public static Dictionary<string, ICardContainer> registry;

    static ContainerRegistry()
    {
        registry = new Dictionary<string, ICardContainer>();
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        Application.quitting += Application_quitting;
    }

    private static void Application_quitting()
    {
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
        Application.quitting -= Application_quitting;
    }

    public static void RegisterContainer(string name, ICardContainer container)
    {
        registry.Add(name, container);
    }

    public static ICardContainer GetContainer(string name)
    {
        if (registry[name] != null)
        {
            return registry[name];
        }
        else
        {
            UnityEngine.Debug.Log($"There is no container in this scene registered with the name " + name + ".");
            return new Card.Space(UnityEngine.Vector3.zero);
        }
    }
    private static void SceneManager_activeSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
    {
        registry.Clear();
    }
}
