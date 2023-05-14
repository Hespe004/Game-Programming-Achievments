using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float rotationSpeed = 10f;

    private Light directionalLight;

    private void Start()
    {
        directionalLight = GetComponent<Light>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0f)
        {
            float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationAmount);
        }
    }
}
