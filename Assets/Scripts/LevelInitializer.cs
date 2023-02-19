using UnityEngine;

[RequireComponent(typeof(PlayerInitializer))]
public class LevelInitializer : MonoBehaviour
{  
    private PlayerInitializer _playerInitializer;

    private void Awake()
    {
        _playerInitializer = this.GetComponent<PlayerInitializer>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _playerInitializer.Init(Vector3.zero, Restart);
    }
    
    private void Restart()
    {
        Init();
    }
}
