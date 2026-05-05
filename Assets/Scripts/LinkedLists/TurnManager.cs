using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{
    public DoubleLinkedList<GameState> timeline = new DoubleLinkedList<GameState>();

    public Transform player;
    public List<Transform> enemies;

    private Vector2 moveInput;

    void Start()
    {
        SaveState();
        ApplyState();
    }

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        player.position += move * Time.deltaTime * 5f;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSaveTurn(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SaveState();

        // Lógica simple enemigos (se acercan)
        foreach (var e in enemies)
        {
            e.position = Vector3.MoveTowards(e.position, player.position, 1f);
        }
    }

    public void OnNextTurn(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        NextTurn();
    }

    public void OnPrevTurn(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        PrevTurn();
    }

    public void SaveState()
    {
        if (timeline.Pivot != timeline.tail)
        {
            timeline.RemoveFuture();
        }

        GameState state = new GameState();

        state.playerPosition = player.position;
        state.health = 100;
        state.attack = 10;

        state.enemyPositions = new List<Vector3>();

        foreach (var e in enemies)
        {
            state.enemyPositions.Add(e.position);
        }

        timeline.Add(state);
    }

    public void ApplyState()
    {
        if (timeline.Pivot == null) return;

        GameState state = timeline.Pivot.Value;

        player.position = state.playerPosition;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].position = state.enemyPositions[i];
        }

        Debug.Log("Aplicando turno");
        Debug.Log($"Turno -> Pos: {state.playerPosition} | HP: {state.health}");
    }

    public void NextTurn()
    {
        timeline.MoveNext();
        ApplyState();
    }

    public void PrevTurn()
    {
        timeline.MovePrev();
        ApplyState();
    }
}