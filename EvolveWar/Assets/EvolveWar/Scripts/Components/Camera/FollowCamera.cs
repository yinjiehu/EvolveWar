using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    [SerializeField]
    SpriteRenderer _map;
    float _speed = 3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    GameObject _player;

    void LateUpdate()
    {
        if(_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");

        if(_player != null)
        {
            var playerNextPosition = new Vector3(_player.transform.position.x, _player.transform.position.y, -10);
            Vector3 targetPosition = Vector3.Lerp(transform.position, playerNextPosition, Time.deltaTime * _speed);
            float width = (MapSize.x - CameraSize.x) / 2f;
            float height = (MapSize.y - CameraSize.y) / 2f;
            if (playerNextPosition.x > -width && playerNextPosition.x < width)
            {
                targetPosition.x = playerNextPosition.x;
            }
            else
            {
                targetPosition.x = transform.position.x;
            }
            if(playerNextPosition.y > -height && playerNextPosition.y < height)
            {
                targetPosition.y = playerNextPosition.y;
            }
            else
            {
                targetPosition.y = transform.position.y;
            }
            targetPosition.z = playerNextPosition.z;
            transform.position = targetPosition;
        }
    }

    Vector2 CameraSize
    {
        get
        {
            var camera = GetComponent<Camera>();
            var halfHeight = camera.orthographicSize;
            var halfWidth = halfHeight * camera.aspect;
            return new Vector2(halfWidth * 2, halfHeight * 2);
        }
    }



    public Vector2 MapSize
    {
        get
        {
            if (_map == null)
                return Vector2.zero;
            return _map.bounds.size;
        }
    }

    void OnDrawGizmosSelected()
    {
        DrawGizmos();
    }
    void DrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(CameraSize.x, CameraSize.y, 0f));


        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(MapSize.x, MapSize.y, 0f));

        Debug.Log("CameraSize:" + CameraSize + "    MapSize:" + MapSize);
    }
}
