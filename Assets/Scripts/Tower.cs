using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{    
    private PlayerController player;
    public bool hookClockWise;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        player.HookedTower(this);
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        player.ReleaseHooked();
    }
}
