using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AttackAreaTower : MonoBehaviour
{
    public TowerController towerController;
    private BulletController bulletController;
    [SerializeField] private int segments = 50;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float lineWidth;
    private Transform target;

    private LineRenderer lineRenderer;

    void Start()
    {
        bulletController = FindObjectOfType<BulletController>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;

        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        CreatePoints();
    }

    void Update()
    {
        if (towerController != null)
        {
            towerController.SetTarget(target);
        }
        else if (bulletController != null)
        {
            bulletController.SetTarget(target);
        }
    }

    void CreatePoints()
    {
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));

            angle += (360f / segments);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            lineRenderer.material.color = Color.red;
            target = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            lineRenderer.material.color = Color.green;
            target = null;
        }
    }
}