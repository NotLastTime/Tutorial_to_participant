using Sych_scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Game_administrator : MonoBehaviour
{

    internal UnityEvent Win_game_event = new UnityEvent();

    internal UnityEvent Lose_game_event = new UnityEvent();



    internal static Game_administrator Singleton;

    private void Awake()
    {

        Game_Player.Cursor_player(false);

        if (Singleton != null && Singleton != this)
        {
            Destroy(this);
        }
        else
        {
            Singleton = this;
        }
    }


    public void Win_game()
    {
        Win_game_event.Invoke();
    }

    public void Lose_game()
    {
        Lose_game_event.Invoke();
    }

}
