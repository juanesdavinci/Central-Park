using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    public GameObject SelectPlayerView;
    public GameObject StartView;
    public List<Transform> _players;
    public float RotationSpeed;

    private Transform _selectedPlayer;
    private int _selectedPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        _selectedPlayerIndex = 0;
        _selectedPlayer = _players[_selectedPlayerIndex];
        
        foreach (var player in _players)
        {
            player.gameObject.SetActive(false);
        }
        _selectedPlayer.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        _selectedPlayer.Rotate(Vector3.up * Time.deltaTime * RotationSpeed);
    }

    public void NextPlayer()
    {
        _selectedPlayerIndex = ++_selectedPlayerIndex % _players.Count;
        _selectedPlayer.gameObject.SetActive(false);
        _selectedPlayer = _players[_selectedPlayerIndex];
        _selectedPlayer.gameObject.SetActive(true);
    }

    public void Select()
    {
        PlayerPrefs.SetInt ("Player_0_CarLastSelection", _selectedPlayerIndex) ;
        SelectPlayerView.SetActive(false);
        StartView.SetActive(true);
        gameObject.SetActive(false);
    }
}
