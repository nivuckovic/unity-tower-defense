using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    public GameState GameState;

    public MapController MapController;
    public GameController GameController;
    public BuilderController BuilderController;
    public EnemyController EnemyController;
    public UIController UIController;
    public ParticlesController ParticlesController;

    public Scheduler Scheduler;

    [Header("Particle Controller")]
    public ParticlesData Particles;
    public Transform ParticlesParent;

    [Header("UI Controller")]
    public TMP_Text CashLabel;
    public BuildMenu BuildMenu;
    public UpgradeMenu UpgradeMenu;
    public Button StartButton;

    // Start is called before the first frame update
    void Start()
    {
        GameState = new GameState(this);

        GameController = new GameController(this);
        MapController = new MapController(this);
        BuilderController = new BuilderController(this);
        EnemyController = new EnemyController(this);
        UIController = new UIController(this);
        ParticlesController = new ParticlesController(this, Particles, ParticlesParent);


        Scheduler.SetGameSystem(this);

        GameController.Start();
    }

    // Update is called once per frame
    void Update()
    {
        GameController.Update();
    }
}
