using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    [SerializeField]
    private int vidas;
    

    public float _moveSpeedEnemy = 3.5f;
    private Vector2 _enemyDirection;
    public Rigidbody2D _enemyRB2D;

    public detectionController _detectionArea;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _enemyRB2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _enemyDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public void ReceberDano(){
        this.vidas--;
        if(this.vidas == 0){
            GameObject.Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(_detectionArea.detectedObjs.Count > 0){
            _enemyDirection = (_detectionArea.detectedObjs[0].transform.position - transform.position).normalized;
            _enemyRB2D.MovePosition(_enemyRB2D.position + _enemyDirection * _moveSpeedEnemy * Time.fixedDeltaTime);

            if(_enemyDirection.x > 0){
                _spriteRenderer.flipX = false;
            }
            else if(_enemyDirection.x < 0){
                _spriteRenderer.flipX = true;
            }
        }
    }
}
