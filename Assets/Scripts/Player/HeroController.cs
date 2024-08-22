using Fusion;
using UnityEngine;

public class HeroController : NetworkBehaviour
{
    private NetworkCharacterController networkCharacter;

    private void Awake()
    {
        networkCharacter = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            Debug.Log($"Direction: {data.direction}");
            data.direction.Normalize();
            networkCharacter.Move(5 * data.direction * Runner.DeltaTime);
        }
    }
}
