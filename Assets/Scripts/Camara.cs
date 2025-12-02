using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField] private float sensibilidad = 100;
    public Transform Player;
    public float RotacionHorizontal = 0;
    public float RotacionVertical = 0;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float ValorX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        float ValorY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        RotacionHorizontal += ValorX;
        RotacionVertical -= ValorY;

        RotacionVertical = Mathf.Clamp(RotacionVertical, -90, 90);

        transform.localRotation = Quaternion.Euler(RotacionVertical,0f ,0f);

        Player.Rotate(Vector3.up * ValorX);

    }
}
