using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    [Header("Admin")]
    private DataHolder DH;
    public CombatState currentState;
    //public PlayerTurnStateMachine playerTurnStateMachine;
    [SerializeField] MenuController menuController;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] private int currentPlayerIndex = 0;
    [SerializeField] private int currentEnemyIndex = 0;
    [SerializeField] List<Item> items;
    [SerializeField] private List<PlayerCharacter> players;
    [SerializeField] private List<EnemyCharacter> enemies;
    [SerializeField] private List<EnemyData> enemyDatas;
    [SerializeField] private List<Material> materials;

    [Header("Persona")]
    [SerializeField] private GameObject[] personaModels;
    [SerializeField] private AudioClip blockClip;
    [SerializeField] private AudioClip hurtClip;
    public Animator personaAnim;
    public int attackNumber;
    private AudioSource comSource;

    [Header("Particles")]
    [SerializeField] private GameObject particleSpawnPoint;

    [Header("Enemy")]
    [SerializeField] int enemyCount = 2;
    [SerializeField] int currentEnemyTCount = 0;
    [SerializeField] int bottomEnemy = 0;
    [SerializeField] int playersCount = 4;
    [SerializeField] int currentPlayersTCount = 0;


    [Header("Camera")]
    public Camera cam;
    Transform cameraPlayerPos1;
    Transform cameraPlayerPos2;
    Transform cameraPlayerPos3;
    Transform cameraPlayerPos4;
    Transform cameraPlayerPos5;
    [SerializeField] Transform playerLight;

    //public List<PersonaMove> CurrentMove = new List<PersonaMove>(1);
    public PersonaMove CurrentMove;

    public enum CombatState
    {
        CombatStart,
        PlayerTurn,
        Attack,
        Gaurd,
        Heal,
        PersonaAttack,
        GunAttack,
        PlayerTurnEnd,
        EnemyTurn,
        HoldUp,
        AllOutAttack,
        CombatEnd,
        Healing,
        PersonaAttacking
    }



    private void Awake()
    {
        Debug.Log("AWAKESETUP");
        DH = FindAnyObjectByType<DataHolder>();
        if (DH != null)
        {
            switch (DH.GetCurrentCombat())
            {
                case 1:
                    Debug.Log("Combat: " + DH.GetCurrentCombat());
                    enemies[0].enemyData = enemyDatas[0];
                    enemies[1].enemyData = enemyDatas[1];
                    break;
                    ChangeState(1);
                case 2:
                    Debug.Log("Combat: " + DH.GetCurrentCombat());
                    enemies[0].enemyData = enemyDatas[2];
                    Debug.Log("first data uploaded");
                    enemies[1].enemyData = enemyDatas[3];
                    Debug.Log("second data uploaded");
                    ChangeState(1);
                    break;
                case 3:
                    Debug.Log("Combat: " + DH.GetCurrentCombat());
                    enemies[0].enemyData = enemyDatas[4];
                    enemies[1].enemyData = enemyDatas[5];
                    ChangeState(1);
                    break;
                case 4:
                    Debug.Log("Combat: " + DH.GetCurrentCombat());
                    enemies[0].enemyData = enemyDatas[6];
                    enemies[1].enemyData = enemyDatas[7];
                    ChangeState(1);
                    break;
                default:
                    break;
            }


        }
    }
    void Start()
    {

        comSource = GetComponent<AudioSource>();
        //ChangeState(1);
        HandleState();
    }

    void Update()
    {
        //HandleState();


        if (currentState == CombatState.PlayerTurn || currentState == CombatState.PersonaAttack || currentState == CombatState.GunAttack)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D was pressed");
                currentEnemyTCount--;
                if (currentEnemyTCount < bottomEnemy)
                    currentEnemyTCount = enemyCount - 1;

                menuController.UpdateEnemyReticleTarget(currentEnemyTCount);

                //supposed to find the position of the current target for the particle spawn point
                Vector3 enemyPosition = enemies[currentEnemyTCount].transform.position;
                particleSpawnPoint.transform.position = enemyPosition;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A was pressed");
                currentEnemyTCount++;
                if (currentEnemyTCount >= enemyCount)
                    currentEnemyTCount = bottomEnemy;

                menuController.UpdateEnemyReticleTarget(currentEnemyTCount);

                //supposed to find the position of the current target for the particle spawn point
                Vector3 enemyPosition = enemies[currentEnemyTCount].transform.position;
                particleSpawnPoint.transform.position = enemyPosition;
            }
        }

        if (currentState == CombatState.Heal)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A was pressed");
                currentPlayersTCount--;
                if (currentPlayersTCount < 0)
                    currentPlayersTCount = playersCount - 1;

                menuController.UpdatePlayerReticleTarget(currentPlayersTCount);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D was pressed");
                currentPlayersTCount++;
                if (currentPlayersTCount >= playersCount)
                    currentPlayersTCount = 0;

                menuController.UpdatePlayerReticleTarget(currentPlayersTCount);
            }
        }
    }

    void HandleState()
    {
        switch (currentState)
        {
            case CombatState.CombatStart:
                //Debug.Log("Ping0");
                //enemies[currentEnemyTCount].enemyAnim.SetInteger("TakeDamage", 1);
                //enemies[currentEnemyTCount].enemyAnim.SetInteger("EnemyAttack", 1);
                //menuController.UpdateStateText("SetUp");
                StartCoroutine(SetupCombat());
                break;

            case CombatState.PlayerTurn:
                Debug.Log("PlayerTurnStarted");
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} is Aiming");
                players[currentPlayerIndex].playerAnim.SetInteger("CallPersona", 0);
                menuController.ShowMainMenu();
                playerLight.position = new Vector3(players[currentPlayerIndex].transform.position.x, players[currentPlayerIndex].transform.position.y + 3, players[currentPlayerIndex].transform.position.z);

                if (currentPlayerIndex < players.Count)
                {
                    PlayerCharacter currentPlayer = players[currentPlayerIndex];
                    UpdateCameraPosition();
                    //playerTurnStateMachine.StartPlayerTurn(currentPlayer);
                }
                else
                {
                    //ChangeState(CombatState.EnemyTurn);
                }
                break;

            case CombatState.Attack:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} is Attacking");
                StartCoroutine(Attack());

                break;

            case CombatState.Heal:
                menuController.UpdateStateText($"Will {players[currentPlayerIndex].characterName} Heal?");
                break;

            case CombatState.Gaurd:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} is Gaurding");
                StartCoroutine(Gaurd());
                break;

            case CombatState.PersonaAttack:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} summons their Persona!");
                menuController.SetUpPersonaMoves(players[currentPlayerIndex].playerMoves);
                //Debug.Log(players[currentPlayerIndex].playerMoves);
                //StartCoroutine(PersonaAttack());

                //Player Summons Persona Animation
                players[currentPlayerIndex].playerAnim.SetInteger("CallPersona", 1);

                //supposed to find the position of the current target for the particle spawn point
                Vector3 enemyPosition = enemies[currentEnemyTCount].transform.position;
                particleSpawnPoint.transform.position = enemyPosition;
                break;

            case CombatState.GunAttack:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} - GunAttck");
                //StartCoroutine(Attack());
                break;

            case CombatState.PlayerTurnEnd:
                menuController.UpdateStateText("PTurns over");
                StartCoroutine(PlayerTurnEnd());
                break;

            case CombatState.EnemyTurn:
                // Handle enemy turns here
                menuController.UpdateStateText($"E{currentEnemyIndex + 1} - Attacking");
                StartCoroutine(EnemyAttack());

                //ChangeState(5);
                break;

            case CombatState.AllOutAttack:
                PerformAllOutAttack();
                break;

            case CombatState.CombatEnd:
                StartCoroutine(EndCombat());
                break;

            case CombatState.Healing:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} is Healing");
                StartCoroutine(Healing());
                break;

            case CombatState.PersonaAttacking:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} uses Persona Attack!");
                StartCoroutine(PersonaAttacking());
                break;
        }
    }

    public void ChangeState(int stateNum)
    {
        switch (stateNum)
        {
            case 1:
                currentState = CombatState.CombatStart;
                break;
            case 2:
                currentState = CombatState.PlayerTurn;

                personaModels[currentPlayerIndex].SetActive(false);

                break;
            case 3:
                currentState = CombatState.Attack;
                break;
            case 4:
                currentState = CombatState.Gaurd;
                break;
            case 5:
                currentState = CombatState.Heal;
                break;
            case 6:
                currentState = CombatState.PersonaAttack;

                personaModels[currentPlayerIndex].SetActive(true);
                personaAnim = personaModels[currentPlayerIndex].GetComponent<Animator>();

                break;
            case 7:
                currentState = CombatState.GunAttack;
                break;
            case 8:
                currentState = CombatState.PlayerTurnEnd;
                break;
            case 9:
                currentState = CombatState.EnemyTurn;
                break;
            case 12:
                currentState = CombatState.CombatEnd;
                break;
            case 13:
                currentState = CombatState.Healing;
                break;
            case 14:
                currentState = CombatState.PersonaAttacking;
                break;
            default:
                break;
        }


        HandleState();
    }

    //public void ChangeStateToResolve()
    //{
    //    //currentState = CombatState.ResolveAction;
    //    HandleState();
    //}

    IEnumerator SetupCombat()
    {
        enemies[0].UpdateData();
        enemies[1].UpdateData();
        if (enemies[0])
        {
            Debug.Log("E1 Exists");
            switch (enemies[0].EnemyResistance)
            {
                case "FIRE":
                    Debug.Log("E1 res to fire");
                    enemies[0].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = materials[0];
                    break;
                case "ICE":
                    Debug.Log("E1 res to ice");
                    enemies[0].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = materials[1];
                    break;
                case "WIND":
                    Debug.Log("E1 res to wind");
                    enemies[0].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = materials[2];
                    break;
                case "NA":
                    Debug.Log("E1 res to NA");
                    enemies[0].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = materials[3];
                    break;
            }
        }
        else
        {
            Debug.Log("E2 Does NOT Exist");
        }


        if (enemies[1])
        {
            Debug.Log("E2 Exists");
            switch (enemies[1].EnemyResistance)
            {
                case "FIRE":
                    Debug.Log("E2 res to fire");
                    enemies[1].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = materials[0];
                    break;
                case "ICE":
                    Debug.Log("E2 res to ice");
                    enemies[1].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = materials[1];
                    break;
                case "WIND":
                    Debug.Log("E2 res to wind");
                    enemies[1].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = materials[2];
                    break;
                case "NA":
                    Debug.Log("E2 res to NA");
                    enemies[1].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = materials[3];
                    break;
            }
        }
        else
        {
            Debug.Log("E2 Does NOT Exist");
        }





        currentPlayerIndex = 0;
        currentEnemyIndex = 0;

        //Camera Handling
        cameraPlayerPos1 = GameObject.Find("CameraFirstPos").transform;
        cameraPlayerPos2 = GameObject.Find("CameraThirdPos").transform;
        cameraPlayerPos3 = GameObject.Find("CameraForthPos").transform;
        cameraPlayerPos4 = GameObject.Find("CameraFifthPos").transform;
        cameraPlayerPos5 = GameObject.Find("CameraSixthPos").transform;

        yield return new WaitForSeconds(2f);
        Debug.Log("PING");
        playerLight.gameObject.SetActive(true);
        menuController.SetUpItems(items);
        menuController.UpdateEnemyReticleTarget(0);
        ChangeState(2);
    }


    IEnumerator Attack()
    {
        menuController.HideEnemyReticles();
        menuController.HideAllMenus();

        //do the attack animation
        players[currentPlayerIndex].playerAnim.SetInteger("BasicAttack", 1);
        menuController.UpdateEnemyText(currentEnemyTCount, "-" + players[currentPlayerIndex].playerPhysAttack.ToString());

        //Put damage Calc here
        yield return new WaitForSeconds(0.7f);
        enemies[currentEnemyTCount].enemyHealth -= players[currentPlayerIndex].playerPhysAttack;
        //Anim when enemies take damage
        enemies[currentEnemyTCount].enemyAnim.SetInteger("TakeDamage", 1);


        yield return new WaitForSeconds(1.3f);


        //return to idle animation
        players[currentPlayerIndex].playerAnim.SetInteger("BasicAttack", 0);
        enemies[currentEnemyTCount].enemyAnim.SetInteger("TakeDamage", 0);
        menuController.UpdateEnemyText(currentEnemyTCount, "");


        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {
            CheckEnemies();
            currentPlayerIndex = 0;
            ChangeState(8);
        }
        else
        {
            CheckEnemies();
            ChangeState(2);
        }

    }

    IEnumerator Gaurd()
    {
        menuController.HideAllMenus();

        //do guard animation
        players[currentPlayerIndex].playerAnim.SetInteger("Guard", 1);
        players[currentPlayerIndex].isGuarding = true;
        //Put Gaurd Calc here

        yield return new WaitForSeconds(2f);

        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {

            currentPlayerIndex = 0;
            CheckEnemies();
            ChangeState(8);
        }
        else
        {
            CheckEnemies();
            ChangeState(2);
        }

    }

    IEnumerator Healing()
    {
        menuController.HideAllMenus();
        //Debug.Log($"Healing Player: {currentPlayersTCount}");

        //Do heal animation
        players[currentPlayerIndex].playerAnim.SetInteger("Heal", 1);

        //Put Heal Calc here

        yield return new WaitForSeconds(2f);

        //Return to Idle Animation
        players[currentPlayerIndex].playerAnim.SetInteger("Heal", 0);
        menuController.UpdatePlayerText(currentPlayersTCount, "");

        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
            ChangeState(8);
        }
        else
        {
            ChangeState(2);
        }

    }

    IEnumerator PersonaAttacking()
    {
        menuController.HideAllMenus();
        //Put Persona Attack Anim here
        personaAnim.SetInteger("Attack" + attackNumber, 1);

        //Put Damage Calc here


        players[currentPlayerIndex].playerMP -= CurrentMove.mpCost;


        //Play the particle system
        InstantiateParticle();
        //Anim when enemies take damage
        enemies[currentEnemyTCount].enemyAnim.SetInteger("TakeDamage", 1);
        //=--------------------

        yield return new WaitForSeconds(0.7f);

        if (CurrentMove.elementalType == enemies[currentEnemyTCount].EnemyWeakness)
        {
            enemies[currentEnemyTCount].enemyHealth -= (CurrentMove.damageValue + CurrentMove.damageValue / 2);
            enemies[currentEnemyTCount].isDown = true;
            menuController.UpdateEnemyText(currentEnemyTCount, "-" + (CurrentMove.damageValue + CurrentMove.damageValue / 2).ToString() + "!");
        }
        else
        {
            enemies[currentEnemyTCount].enemyHealth -= CurrentMove.damageValue;
            menuController.UpdateEnemyText(currentEnemyTCount, "-" + CurrentMove.damageValue.ToString());

        }



        yield return new WaitForSeconds(1.3f);
        menuController.UpdateEnemyText(currentEnemyTCount, "");

        //Revert to Idle Animation
        players[currentPlayerIndex].playerAnim.SetInteger("CallPersona", 0);
        enemies[currentEnemyTCount].enemyAnim.SetInteger("TakeDamage", 0);
        //Revert Persona to Idle
        personaAnim.SetInteger("Attack" + attackNumber, 0);
        //Despawn Persona
        personaModels[currentPlayerIndex].SetActive(false);

        //revert persona attack number
        attackNumber = 0;

        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
            CheckEnemies();
            ChangeState(8);
        }
        else
        {
            CheckEnemies();
            ChangeState(2);
        }


    }

    IEnumerator EnemyAttack()
    {
        playerLight.GetComponent<Light>().color = Color.red;
        playerLight.position = new Vector3(enemies[currentEnemyIndex].transform.position.x, enemies[currentEnemyIndex].transform.position.y + 3, enemies[currentEnemyIndex].transform.position.z);
        if (currentEnemyIndex == 0)
            yield return new WaitForSeconds(1f);
        //Put Enemy Attack Anim here
        //Do enemy attack animation
        //Randomise Party member to hit
        int temp1 = Random.Range(0, 4);
        int temp2 = Random.Range(0, enemies[currentEnemyIndex].enemyMoveList.Count);

        enemies[currentEnemyIndex].enemyAnim.SetInteger("EnemyAttack", 1);
        yield return new WaitForSeconds(0.6f);
        if (!players[temp1].isGuarding)
            players[temp1].playerAnim.SetInteger("TakeDamage", 1);
        //Put Damage Calc here
        if (players[temp1].isGuarding == false)
        {
            comSource.PlayOneShot(hurtClip);
            players[temp1].playerHealth -= enemies[currentEnemyIndex].enemyMoveList[temp2].damageValue;
            menuController.UpdatePlayerText(temp1, "-" + enemies[currentEnemyIndex].enemyMoveList[temp2].damageValue.ToString());
        }
        else
        {
            comSource.PlayOneShot(blockClip);
            players[temp1].playerHealth -= (enemies[currentEnemyIndex].enemyMoveList[temp2].damageValue / 2);
            menuController.UpdatePlayerText(temp1, "-" + (enemies[currentEnemyIndex].enemyMoveList[temp2].damageValue / 2).ToString());

        }




        //---------------

        yield return new WaitForSeconds(1.4f);
        //Return to Idle Animation
        if (!players[temp1].isGuarding)
            players[temp1].playerAnim.SetInteger("TakeDamage", 0);
        enemies[currentEnemyIndex].enemyAnim.SetInteger("EnemyAttack", 0);
        menuController.UpdatePlayerText(temp1, "");


        currentEnemyIndex++;
        if (currentEnemyIndex >= enemies.Count)
        {
            for (int i = 0; i <= playersCount - 1; i++)
            {

                players[i].isGuarding = false;
                //playerLight.gameObject.SetActive(true);
                playerLight.GetComponent<Light>().color = Color.green;
                players[i].playerAnim.SetInteger("Guard", 0);


            }


            currentEnemyIndex = 0;
            ChangeState(2);
        }
        else
        {
            ChangeState(9);
        }
    }

    IEnumerator PlayerTurnEnd()
    {
        playerLight.GetComponent<Light>().color = Color.red;
        playerLight.position = new Vector3(enemies[currentEnemyIndex].transform.position.x, enemies[currentEnemyIndex].transform.position.y + 3, enemies[currentEnemyIndex].transform.position.z);
        UpdateCameraPosition();
        menuController.HideAllMenus();
        menuController.HideEnemyReticles();
        menuController.HidePlayerReticles();
        //playerLight.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        ChangeState(9);
    }

    void PerformAllOutAttack()
    {
        // Perform all-out attack
        ChangeState(3);
    }

    IEnumerator EndCombat()
    {
        // End combat
        yield return new WaitForSeconds(0.1f);
        playerLight.gameObject.SetActive(false);
        cam.transform.position = cameraPlayerPos5.position;
        cam.transform.rotation = cameraPlayerPos5.rotation;
        menuController.HideAllMenus();
        foreach (var player in players)
        {
            player.transform.GetChild(1).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1f);

        foreach (var player in players)
        {
            player.playerAnim.SetInteger("Heal", 1);
        }
        yield return new WaitForSeconds(1.5f);
        pauseMenu.LoadWorldScene();
    }

    public void UpdateCameraPosition()
    {
        if (currentPlayerIndex == 0)
        {
            cam.transform.position = cameraPlayerPos1.position;
            cam.transform.rotation = cameraPlayerPos1.rotation;
        }

        if (currentPlayerIndex == 1)
        {
            cam.transform.position = cameraPlayerPos2.position;
            cam.transform.rotation = cameraPlayerPos2.rotation;
        }

        if (currentPlayerIndex == 2)
        {
            cam.transform.position = cameraPlayerPos3.position;
            cam.transform.rotation = cameraPlayerPos3.rotation;
        }

        if (currentPlayerIndex == 3)
        {
            cam.transform.position = cameraPlayerPos4.position;
            cam.transform.rotation = cameraPlayerPos4.rotation;
        }


        //menuController.UpdateReticlePlacement();
    }

    public void PButton1()
    {

        CurrentMove = players[currentPlayerIndex].playerMoves[0];
        //Number for the animator
        attackNumber = 1;

        ChangeState(14);


    }

    public void PButton2()
    {
        CurrentMove = players[currentPlayerIndex].playerMoves[1];
        //Number for the animator
        attackNumber = 2;

        ChangeState(14);


    }

    public void PButton3()
    {
        CurrentMove = players[currentPlayerIndex].playerMoves[2];
        //Number for the animator
        attackNumber = 3;

        ChangeState(14);


    }

    public void PButton4()
    {
        CurrentMove = players[currentPlayerIndex].playerMoves[3];
        //Number for the animator
        attackNumber = 4;

        ChangeState(14);



    }


    public void I1ButtonPressed()
    {
        if (items[0].isHP)
        {
            //Debug.Log("Target hp is " + players[currentPlayersTCount].playerHealth);
            //Debug.Log("Item heals: " + items[0].amount);
            players[currentPlayersTCount].playerHealth += items[0].amount;
            //Debug.Log("Target hp is " + players[currentPlayersTCount].playerHealth);
            if (players[currentPlayersTCount].playerHealth > players[currentPlayersTCount].playerMaxHP)
                players[currentPlayersTCount].playerHealth = players[currentPlayersTCount].playerMaxHP;
        }
        else
        {
            players[currentPlayersTCount].playerMP += items[0].amount;
            if (players[currentPlayersTCount].playerMP > players[currentPlayersTCount].playerMaxMP)
                players[currentPlayersTCount].playerMP = players[currentPlayersTCount].playerMaxMP;
        }

        menuController.UpdatePlayerText(currentPlayersTCount, "+" + items[0].amount.ToString());
        menuController.I1ButtonPressed();
    }

    public void I2ButtonPressed()
    {
        if (items[1].isHP)
        {
            players[currentPlayersTCount].playerHealth += items[1].amount;
            if (players[currentPlayersTCount].playerHealth > players[currentPlayersTCount].playerMaxHP)
                players[currentPlayersTCount].playerHealth = players[currentPlayersTCount].playerMaxHP;
        }
        else
        {
            players[currentPlayersTCount].playerMP += items[1].amount;
            if (players[currentPlayersTCount].playerMP > players[currentPlayersTCount].playerMaxMP)
                players[currentPlayersTCount].playerMP = players[currentPlayersTCount].playerMaxMP;
        }
        menuController.UpdatePlayerText(currentPlayersTCount, "+" + items[1].amount.ToString());
        menuController.I2ButtonPressed();

    }

    public void I3ButtonPressed()
    {
        if (items[2].isHP)
        {
            players[currentPlayersTCount].playerHealth += items[2].amount;
            if (players[currentPlayersTCount].playerHealth > players[currentPlayersTCount].playerMaxHP)
                players[currentPlayersTCount].playerHealth = players[currentPlayersTCount].playerMaxHP;
        }
        else
        {
            players[currentPlayersTCount].playerMP += items[2].amount;
            if (players[currentPlayersTCount].playerMP > players[currentPlayersTCount].playerMaxMP)
                players[currentPlayersTCount].playerMP = players[currentPlayersTCount].playerMaxMP;
        }
        menuController.UpdatePlayerText(currentPlayersTCount, "+" + items[2].amount.ToString());
        menuController.I3ButtonPressed();

    }

    public void I4ButtonPressed()
    {
        if (items[3].isHP)
        {
            players[currentPlayersTCount].playerHealth += items[3].amount;
            if (players[currentPlayersTCount].playerHealth > players[currentPlayersTCount].playerMaxHP)
                players[currentPlayersTCount].playerHealth = players[currentPlayersTCount].playerMaxHP;
        }
        else
        {
            players[currentPlayersTCount].playerMP += items[3].amount;
            if (players[currentPlayersTCount].playerMP > players[currentPlayersTCount].playerMaxMP)
                players[currentPlayersTCount].playerMP = players[currentPlayersTCount].playerMaxMP;
        }
        menuController.UpdatePlayerText(currentPlayersTCount, "+" + items[3].amount.ToString());
        menuController.I4ButtonPressed();

    }

    public void CheckEnemies()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i].enemyHealth <= 0)
            {
                if (enemies[i].gameObject.name == "E1")
                {
                    //Debug.Log("E1 if, must die");
                    menuController.UpdateNewReticles();
                }

                enemies[i].gameObject.SetActive(false);
                enemies.RemoveAt(i);
                enemyCount = enemies.Count;

            }
        }

        currentEnemyTCount = 0;
        menuController.UpdateEnemyReticleTarget(0);


        if (enemyCount <= 0)
            ChangeState(12);

    }

    public void InstantiateParticle()
    {
        //find particle number
        int particleNumber = attackNumber - 1;
        //instantiate the correct partice system
        GameObject particleInstance = Instantiate(players[currentPlayerIndex].particleSystems[attackNumber - 1], particleSpawnPoint.transform.position, Quaternion.identity);
        //Get particle system component
        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
        //play particle system
        particleSystem.Play();
        //destroy it
        Destroy(particleInstance, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
    }






}

