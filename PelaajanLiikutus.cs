using UnityEngine;

public class PelaajanLiikutus : MonoBehaviour
{
    public float kävelyNopeus = 5f;
    public float juoksuNopeus = 10f;
    public float hyppyVoima = 7f;
    private float liikeNopeus;
    private Rigidbody rb;
    private Vector3 liike;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        liikeNopeus = kävelyNopeus;
    }

    void Update()
    {
        float liikeVaaka = Input.GetAxis("Horizontal");
        float liikePysty = Input.GetAxis("Vertical");

        liike = new Vector3(liikeVaaka, 0.0f, liikePysty);

        // Vaihda kävelyn ja juoksun välillä
        if (Input.GetKey(KeyCode.LeftShift))
        {
            liikeNopeus = juoksuNopeus;
        }
        else
        {
            liikeNopeus = kävelyNopeus;
        }

        // Hyppytoiminto
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(new Vector3(0, hyppyVoima, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        LiikutaHahmoa(liike);
    }

    void LiikutaHahmoa(Vector3 suunta)
    {
        rb.MovePosition(transform.position + suunta * liikeNopeus * Time.fixedDeltaTime);
    }
}
