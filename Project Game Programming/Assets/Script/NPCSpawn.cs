using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    public GameObject NPC;
    public Transform[] SpawnerPoints;
    /*public GameObject[] MejaMakan;*/
    [SerializeField]
    MejaMakan[] _interactMejaMakan;
    int randomSpawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawning", 2f, 1.5f);
        
    }

    // Update is called once per frame
    
    public void Spawning()
    {
        randomSpawn = Random.Range(0, 3);
        if(!_interactMejaMakan[randomSpawn].isNPC)
        {
            PatienceBar temp = this.NPC.GetComponent<PatienceBar>();
            _interactMejaMakan[randomSpawn].npc = temp;
            _interactMejaMakan[randomSpawn].npc._mejaMakan = _interactMejaMakan[randomSpawn];
            Instantiate(NPC, SpawnerPoints[randomSpawn].position, transform.rotation);
        }
        
    }
}
