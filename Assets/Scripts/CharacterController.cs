using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Animator _animator;
    private Rigidbody2D player;
    private bool onAir;
    [SerializeField] Text coinText;
    private int coin = 0;
    [SerializeField] GameObject star;
    [SerializeField] private AudioSource coinSound;
    [SerializeField] private AudioSource levelUpSound;
    [SerializeField] private AudioSource trapSound;

    void Start()
    {
        star.SetActive(false);
        player = gameObject.GetComponent<Rigidbody2D>();
        
    }

    
    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float movement = Input.GetAxis("Horizontal");

        if (movement<0)
        {
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
            transform.localScale = new Vector3(-1, 1, 1);
            _animator.SetBool("IsRunning", true);
        }
        else if (movement>0)
        {
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
            transform.localScale = new Vector3(1, 1, 1);
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && onAir==false)
        {
            player.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
            onAir = true;
            _animator.SetBool("IsJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Background"))
        {
            onAir = false;
            _animator.SetBool("IsJumping", false);

        }
        else if (other.gameObject.CompareTag("Traps"))
        {
            StartCoroutine(TrapSoundCoroutine());
            player.GetComponent<Collider2D>().isTrigger = true;
        }
        else if (other.gameObject.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Coin"))
        {
            coinSound.Play();
            coin++;
            coinText.text = "Coin: "+coin.ToString();
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Star"))
        {
            star.SetActive(true);
            levelUpSound.Play();
            star.GetComponent<Transform>().DORotate(new Vector3(0f, 180f, 0f), 1f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
            star.GetComponent<Transform>().DOLocalMove(new Vector3(85.80915f, 6f, 0f), 2f).SetLoops(1, LoopType.Incremental).SetEase(Ease.Linear);
        }
        
    }
    IEnumerator TrapSoundCoroutine()
    {
        trapSound.Play();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
