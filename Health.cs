using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
public class Health : MonoBehaviourPunCallbacks
{
    public Image HPcircle;

    public int health;
    public bool isLocalPlayer;
    [Header("UI")]
    public TextMeshProUGUI healthText;


    void setHP()
    {
        HPcircle.fillAmount = (float)health / 100;
    }

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;
        setHP();

        healthText.text = health.ToString();

        if(health <= 0)
        {
            if (isLocalPlayer)
            {
                RoomManager.instance.SpawnPlayer();
                RoomManager.instance.deaths++;

                RoomManager.instance.SetHashes();
            }

            Destroy(gameObject);
        }
    }
}
