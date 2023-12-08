using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public GameObject coinPrefab;
    public int initialCoinsCount; // Adjust as needed
    //max coins  in game
    [SerializeField] int maxcoinnumber = 50;

    //Total Coin number
    [SerializeField] int coin;
    //Collected coin number
    int Coinnum;
    public float rotationSpeed = 5f;

    Rigidbody Ri;
    [SerializeField] float movementSpeed = 12f;
    [SerializeField] float force = 6f;
    
    //directions
    float dx, dz;

    //collected coins text
    [SerializeField]
    TextMeshProUGUI Colltext;

    
    

    void Start()
    {
        coin = 0;
        Ri = GetComponent<Rigidbody>();
        initialCoinsCount = Random.Range(1, 5);
        // Instantiate random coins at the start of the game
        for (int i = 0; i < initialCoinsCount; i++)
        {
            SpawnCoin();
            coin++;
        }

    }
    private void Update()
    {
        dz = Input.GetAxis("Vertical");
    }


    private void FixedUpdate()
    {
        // Calculate the forward direction based on the rotation
        Vector3 forwardDirection = transform.forward;
        forwardDirection.y = 0;

        // Normalize the direction to ensure consistent movement speed
        forwardDirection.Normalize();

        // Move the rigidbody in the forward direction
        Ri.velocity = forwardDirection * dz * movementSpeed;

        // Calculate the rotation based on mouse position
        var mousepos = Input.mousePosition;
        var capsulePos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = capsulePos - mousepos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Rotate the character towards the mouse position
        transform.rotation = Quaternion.AngleAxis(-angle - 90, Vector3.up);
    }
    void SpawnCoin()
    {
        // Calculate a random position on the plane
        Vector3 randomPosition = new Vector3(Random.Range(-7f, 7f),0.322f,Random.Range(-9f, 9f));

        // Instantiate a coin at the random position
        Instantiate(coinPrefab, randomPosition, Quaternion.Euler(0f,0f,0f));
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Coin"))
        {
            Coinnum++;
            hit.gameObject.SetActive(false);
            int additionalCoin=Random.Range(1, 5);
            for(int i = 0;i < additionalCoin;i++)
            {
                if (coin <maxcoinnumber)
                {
                    SpawnCoin();
                    coin++;
                }
                

            }
            Debug.Log("Collected coins: "+Coinnum);
            Colltext.text="Coins: "+Coinnum.ToString();
           
        }
        if (hit.gameObject.name == "Soccer Ball")
        {
            var a = (hit.transform.position - transform.position).normalized;
            a.y = 0;
            hit.gameObject.GetComponent<Rigidbody>().AddForce(a.normalized * force, ForceMode.Impulse);
        }

    }
    

}
