using UnityEngine;

public class PlanetVerlet : MonoBehaviour
{
    //Referencia al sol, que será la fuente de gravedad
    public Transform sun;

    //Constante gravitatoria en unidades astronómicas
    //distancia en UA
    //tiempo en años
    //masa del Sol = 1
    public float G = 39.478f; //Valor simplificado

    //Paso del tiempo (At).
    public float dt = 0.01f;

    //Módulo de la velocidad inicial del planeta
    public float velocity;
    //Posición actual del planeta en el metodo de Verlet
    private Vector3 currentPosition;
    // Posición anterior
    private Vector3 previousPosition;

    void Start()
    {
        //inicializacion necesaria
        currentPosition = transform.position;

        //Definicion del eje Zpara generar una órbita
        //vector que une planeta con el Sol para obtener una órbita estable.
        Vector3 initialVelocity = new Vector3(0, 0, velocity);

        //En Verlet no guardamos velocidad explícitamente.
        //Lo que hacemos es reconstruir el estado usando posiciones.
        //Esta fórmula viene de aproximar: x(t - dt) = x(t) - v(t)*dt
        previousPosition = currentPosition - initialVelocity * dt;
    }

    void FixedUpdate()
    {
        //Vector desde el planeta hacia el Sol
        Vector3 direction = sun.position - currentPosition;

        //Distancia entre planeta y Sol
        float distance = direction.magnitude;

        //Aceleración gravitatoria:
        //a = G * M / r^2

        //Aquí asumimos M = 1 (masa del Sol), por eso queda:
        //a = G / r^2
        //multiplicamos por direction.normalized para darle dirección al vector.
        Vector3 acceleration = direction.normalized * (G / (distance * distance));

        //fórmula de integración de Verlet
        Vector3 newPosition = 2 * currentPosition - previousPosition + acceleration * dt * dt;

        //actualización de  las posiciones para el siguiente paso temporal
        previousPosition = currentPosition;
        currentPosition = newPosition;

        //Actualizacion de la posicion, nueva, al objeto de unity
        transform.position = currentPosition;
    }
}