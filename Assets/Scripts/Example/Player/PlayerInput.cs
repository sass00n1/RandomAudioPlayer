using UnityEngine;
using SFTool;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Collider2D groundCollider;

    private CharacterController2D cc2D;
    private Animator animator;
    private RandomAudioPlayer randomAudioPlayer;
    private Transform trans_GroundCheck;

    private float horizontalMove;

    private void Awake()
    {
        cc2D = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        randomAudioPlayer = transform.Find("Footstep").GetComponent<RandomAudioPlayer>();
        trans_GroundCheck = transform.Find("GroundCheck");
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * 40;
        animator.SetFloat("MoveSpeed", Mathf.Abs(horizontalMove));
    }

    private void FixedUpdate()
    {
        cc2D.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }

    //事件，Run动画调用
    public void PlayFootstep()
    {
        //找到脚下的瓦片
        TileBase tileBase = PhysicsHelper.FindTileForOverride(groundCollider, trans_GroundCheck.position, Vector2.down);
        //依据脚下的瓦片播放对应的音频
        randomAudioPlayer.PlayRandomSound(tileBase);
    }
}
