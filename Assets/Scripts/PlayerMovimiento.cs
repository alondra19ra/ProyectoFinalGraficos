using UnityEngine;

public class PlayerMovimiento : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform camara;
    private CharacterController controlador;

    [Header("Movimiento")]
    [SerializeField] private bool UsarGetAxisRaw;
    [SerializeField] private float velocidadMovimiento = 5f;

    [Header("Gravedad")]
    [SerializeField] private float Gravedad = -9f;
    private Vector3 velocidadVertical;
    void Start()
    {
        
    }

    private void Awake()
    {
        controlador = GetComponent<CharacterController>();

        if (camara == null && Camera.main != null)
            camara = Camera.main.transform;
    }

    void Update()
    {
        MoverJugadorEnPlano();
        AplicarGravedad();
    }

    private void MoverJugadorEnPlano()
    {
        float ValorHorizontal = UsarGetAxisRaw ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal");
        float ValorVertical = UsarGetAxisRaw ? Input.GetAxis("Vertical") : Input.GetAxis("Vertical");

        Vector3 adelanteCamara = camara.forward;
        Vector3 derechaCamara = camara.right;

        adelanteCamara.y = 0f;
        derechaCamara.y = 0f;

        adelanteCamara.Normalize();
        derechaCamara.Normalize();

        Vector3 direccionPlano = (derechaCamara * ValorHorizontal + adelanteCamara * ValorVertical);

        if (direccionPlano.sqrMagnitude > 0.0001f)
            direccionPlano.Normalize();

        Vector3 desplazamientoXZ = direccionPlano * (velocidadMovimiento * Time.deltaTime);

        controlador.Move(desplazamientoXZ);
    }

    private void AplicarGravedad()
    {
        velocidadVertical.y += Gravedad * Time.deltaTime;
        controlador.Move(velocidadVertical * Time.deltaTime);

        if(controlador.isGrounded && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f; 
        }
    }
}
