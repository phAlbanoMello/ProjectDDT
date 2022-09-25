using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * Input.GetAxis("Horizontal"), transform.position.y, transform.position.z);
        }

        if (Input.GetButton("Vertical"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime * Input.GetAxis("Vertical"));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
        }
    }
}
