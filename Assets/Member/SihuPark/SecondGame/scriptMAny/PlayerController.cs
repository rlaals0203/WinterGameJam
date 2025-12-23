using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stat")]
    [SerializeField] private float moveSpeed = 5f; // 이동 속도

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriter;
    [SerializeField] private Animator animator;

    private Vector2 moveInput; // 입력값 저장

    private void Awake()
    {
        // 혹시 인스펙터에서 연결 안 했을까봐 자동으로 찾아줌
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!spriter) spriter = GetComponent<SpriteRenderer>();
        if (!animator) animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 1. 입력 받기 (PC: WASD / 방향키)
        // 나중에 조이스틱으로 바꿀 때 이 부분만 수정하면 됨
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // 대각선 이동 시 속도가 빨라지는 것 방지 (.normalized)
        moveInput = new Vector2(x, y).normalized;

        // 2. 바라보는 방향 전환 (탕탕특공대 스타일: 좌우 반전)
        if (moveInput.x != 0)
        {
            // 왼쪽(-1)이면 true(반전), 오른쪽(1)이면 false(원본)
            spriter.flipX = moveInput.x < 0;
        }

        // 3. 애니메이션 처리 (달리기 vs 대기)
        if (animator != null)
        {
            // 움직임 벡터의 길이(magnitude)가 0보다 크면 달리는 중
            animator.SetBool("IsRun", moveInput.magnitude > 0);
        }
    }

    private void FixedUpdate()
    {
        // 4. 실제 이동 (물리 연산은 FixedUpdate에서 해야 끊김이 없음)
        // 현재 위치 + (방향 * 속도 * 시간)
        Vector2 nextPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPos);
    }
}