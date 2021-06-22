using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Tooltip("Tag of the collider we want to collect this item. Usually the player.")]
    [SerializeField] private string activationTag;

    [Tooltip("Type of item that is this collectible.")]
    [SerializeField] private Item item;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == activationTag)
        {
            other.gameObject.GetComponent<PlayerInventory>().AddItem(item);
            gameObject.SetActive(false);
        }
    }
}
