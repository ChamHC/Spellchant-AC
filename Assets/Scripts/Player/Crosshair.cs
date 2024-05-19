using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public GameObject Player;

    private void Update() {
        float playerSpeed = Player.GetComponent<Rigidbody>().velocity.magnitude;
        float maxSpeed = Player.GetComponent<PlayerStateManager>().SprintSpeed;
        float scale = Mathf.Lerp(0.5f, 0.8f, playerSpeed / maxSpeed);
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
