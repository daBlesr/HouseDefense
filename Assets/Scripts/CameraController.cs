using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
	[SerializeField] private float offset = 10;
    // Update is called once per frame
    void Update()
    {
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.position.x, Time.deltaTime * 2f), offset, transform.position.z);
    }
}