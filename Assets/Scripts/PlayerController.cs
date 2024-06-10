using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     [SerializeField]
    private Transform pontoAtaque;

    [SerializeField]
    private float raioAtaque;
    private Rigidbody2D _playerRigidibody2D;
    private Animator _playerAnimator;
    public float _playerSpeed;
    public float _playerInitialSpeed;
    private Vector2 _playerDirection;
    private bool _isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidibody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();

        _playerInitialSpeed = _playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        OnAttack();
    }

    void FixedUpdate()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(_playerDirection.sqrMagnitude > 0.1){
            MovePlayer();

            _playerAnimator.SetFloat("AxisX",_playerDirection.x);
            _playerAnimator.SetFloat("AxisY",_playerDirection.y);
            _playerAnimator.SetInteger("Movimento", 1);
        }
        else{
            _playerAnimator.SetInteger("Movimento", 0);
        }

        if(_isAttack == true){
            _playerAnimator.SetInteger("Movimento",2);
        }
    }

    void MovePlayer(){
        _playerRigidibody2D.MovePosition(_playerRigidibody2D.position + _playerDirection.normalized * _playerSpeed * Time.fixedDeltaTime);
    }

    void Flip(){
        if(_playerDirection.x > 0){
            transform.eulerAngles = new Vector2(0f,0f);
        }
        else if(_playerDirection.x < 0){
            transform.eulerAngles = new Vector2(0f,180f);
        }
    }

    void OnAttack(){
        if(Input.GetKeyDown(KeyCode.Space)){
            _isAttack = true;
            _playerSpeed = 0;
            
            Collider2D colliderInimigo = Physics2D.OverlapCircle(this.pontoAtaque.position, this.raioAtaque);
            if(colliderInimigo != null){
                enemyScript inimigo = colliderInimigo.GetComponent<enemyScript>();
                if(inimigo != null){
                    inimigo.ReceberDano();
                }
            }
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            _isAttack = false;
            _playerSpeed = _playerInitialSpeed;
        }
    }

     private void OnDrawGizmos(){
        if(this.pontoAtaque != null){
        Gizmos.DrawWireSphere(this.pontoAtaque.position, this.raioAtaque);
        }
    }

}
