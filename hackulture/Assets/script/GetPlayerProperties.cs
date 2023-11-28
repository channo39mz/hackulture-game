using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerProperties : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Player _player;

    public void SetStat(Player player)
    {
        int Score = player.GetScore();
        //int Position = player\
        _text.text = "" + Score;
    }
}
