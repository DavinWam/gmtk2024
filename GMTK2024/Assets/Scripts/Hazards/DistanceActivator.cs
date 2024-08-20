using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceActivator : MonoBehaviour
{
    Trap trapCard;
    // Start is called before the first frame update
    void Start() {
        trapCard = GetComponentInParent<Trap>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("You've activated my trap card");

        trapCard.StartPeriodicActivation();
    }

    public void OnTriggerExit2D(Collider2D col) {
        Debug.Log("I've deactivated my trap card");

        trapCard.StopPeriodicActivation();
    }

}
