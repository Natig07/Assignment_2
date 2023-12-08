using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Balltouch : MonoBehaviour
{
    Vector3 pos;
    [SerializeField]
    TextMeshProUGUI walltext;
    [SerializeField]
    TextMeshProUGUI goaltext;
    int goals=0;
    private void Start()
    {
        pos = transform.position;
    }
    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Wall"))
        {
            walltext.text =hit.gameObject.name;
        }
        
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("goal"))
        {
            goals++;
            goaltext.text ="Score: "+goals.ToString();
            Invoke("Res_T", 1.6f);
        }
    }

    void Res_T()
    {
        transform.position = pos;
    }
}
