using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject playerSprite;
    private InputActions inputs;
    private CharacterController characterController;
    private Animator animator;

    public int livesCount = 3;
    public int score = 0;
    public float moveSpeed = 1.0f;

    private Vector2 moveInput;
    private float gravity = -9.8f;
    private bool facingRight = true;
    private bool isAttacking = false;
    private bool isMoving = false;
    private bool canBeHurt = true;

    [SerializeField]
    private bool pauseActive = false;

    private void Awake()
    {
        instance = this;
        inputs = new InputActions();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        inputs.Player.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        inputs.Player.Movement.canceled += context => moveInput = Vector2.zero;

        inputs.Player.Attack.performed += context => StartCoroutine(Attack());

        inputs.Player.Pause.performed += context => Pause();
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void FixedUpdate()
    {
        playerSprite.transform.LookAt(Camera.main.transform, -Vector3.up);
        playerSprite.transform.localEulerAngles = new Vector3(-playerSprite.transform.localEulerAngles.x, 0, 0);

        Vector2 inputDir = moveInput.normalized;
        if(!isAttacking && !GameManager.instance.gamePaused)
        {
            Move(new Vector3(inputDir.x, gravity * Time.deltaTime * 2f, inputDir.y));
        }
    }

    private void Move(Vector3 inputDir)
    {
        characterController.Move(inputDir * moveSpeed);
        if(inputDir.x < 0)
        {
            isMoving = true;
            facingRight = false;
        }
        else if(inputDir.x > 0)
        {
            isMoving = true;
            facingRight = true;
        }
        else if(inputDir.z > 0 || inputDir.z < 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("FacingRight", facingRight);
        animator.SetFloat("X", inputDir.x);
        animator.SetFloat("Y", inputDir.z);
    }

    private IEnumerator Attack()
    {
        if(!GameManager.instance.gamePaused)
        {
            isAttacking = true;
            animator.SetBool("IsAttacking", true);
            yield return new WaitForSeconds(0.25f);
            isAttacking = false;
            animator.SetBool("IsAttacking", false);
        }
    }

    public void Pause()
    {
        if(!pauseActive)
        {
            pauseActive = true;
            GameManager.instance.gamePaused = true;
            MainUIHandler.instance.SwapPanels(1);
        }
        else
        {
            pauseActive = false;
            GameManager.instance.gamePaused = false;
            MainUIHandler.instance.SwapPanels(0);
        }
    }

    public void DamageEvent()
    {
        if(canBeHurt)
        {
            if(livesCount > 0)
            {
                livesCount -= 1;
                MainUIHandler.instance.setLivesSprites();
                StartCoroutine(RecoverEvent());
            }
            if(livesCount < 1)
            {
                livesCount -= 1;
                MainUIHandler.instance.setLivesSprites();
                GameManager.instance.finalScore = score;
                EndScreenUIController.isWin = false;
                SceneManager.LoadSceneAsync(2);
            }
        }
    }

    public IEnumerator RecoverEvent()
    {
        canBeHurt = false;
        for (int i = 0; i < 10; i++)
        {
            playerSprite.SetActive(false);
            yield return new WaitForSeconds(0.25f);

            playerSprite.SetActive(true);
            yield return new WaitForSeconds(0.25f);
        }
        canBeHurt = true;
    }
}
