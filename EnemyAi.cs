using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform pelaajanSijainti;
    public float seurantaEtäisyys = 10f;
    public float hyökkäysEtäisyys = 2f;
    public float menetäKiinnostusAika = 5f;
    public float lisääNopeusAika = 10f;
    public float näköKulma = 60f; // Vihollisen näkökulma astetta
    public float kuuloEtäisyys = 15f; // Vihollisen kuuloetäisyys

    private NavMeshAgent agent;
    private float aikaIlmanNäköyhteyttä;
    private float aikaSeurannassa;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        aikaIlmanNäköyhteyttä = 0;
        aikaSeurannassa = 0;
    }

    void Update()
    {
        float etaisyysPelaajaan = Vector3.Distance(pelaajanSijainti.position, transform.position);
        Vector3 suuntaPelaajaan = pelaajanSijainti.position - transform.position;
        float kulmaPelaajaan = Vector3.Angle(suuntaPelaajaan, transform.forward);

        // Tarkistetaan, onko pelaaja näkökentässä ja kuuloetäisyydellä
        if (kulmaPelaajaan < näköKulma / 2 && etaisyysPelaajaan <= seurantaEtäisyys || etaisyysPelaajaan <= kuuloEtäisyys)
        {
            aikaSeurannassa += Time.deltaTime;
            agent.SetDestination(pelaajanSijainti.position);

            if (etaisyysPelaajaan <= hyökkäysEtäisyys)
            {
                // Hyökkäystoiminnot
                HyökkääPelaajaan();
            }

            if (aikaSeurannassa >= lisääNopeusAika)
            {
                agent.speed *= 1.5f; // Lisätään nopeutta
            }
        }
        else
        {
            // Pelaaja ei ole havaittavissa
            aikaIlmanNäköyhteyttä += Time.deltaTime;
            if (aikaIlmanNäköyhteyttä >= menetäKiinnostusAika)
            {
                agent.ResetPath();
                aikaSeurannassa = 0;
                agent.speed = /* alkuperäinen nopeus */;
            }
        }
    }

    void HyökkääPelaajaan()
    {
        // Tähän tulee hyökkäyksen logiikka
    }
}
