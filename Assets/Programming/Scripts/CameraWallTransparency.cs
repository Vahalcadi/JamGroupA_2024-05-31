using UnityEngine;

public class CameraWallTransparency : MonoBehaviour
{
    [SerializeField] private Transform player;
    //public Vector3 offset;
    private Transform[] obstructions;
    [Header("Obstacles layer")]
    [SerializeField] LayerMask layerMask;

    [Header("0 is completely transparent, 1 is completely visible")]
    [Range(0f, 1f)]
    [SerializeField] private float transparency;

    private int oldHitsNumber;

    void Start()
    {
        oldHitsNumber = 0;
    }

    private void LateUpdate()
    {
        ViewObstructed();
    }

    void ViewObstructed()
    {
        float characterDistance = Vector3.Distance(transform.position, player.transform.position);

        RaycastHit[] hits = Physics.RaycastAll(transform.position, player.position - transform.position, characterDistance, layerMask);

        Debug.DrawRay(transform.position, (player.position - transform.position) * characterDistance, Color.black);

        if (hits.Length > 0)
        {
            // Means that some stuff is blocking the view
            int newHits = hits.Length - oldHitsNumber;

            if (obstructions != null && obstructions.Length > 0 && newHits < 0)
            {
                // Repaint all the previous obstructions. Because some of the stuff might be not blocking anymore
                for (int i = 0; i < obstructions.Length; i++)
                {
                    MeshRenderer obstructionMesh;
                    bool valid = obstructions[i].gameObject.TryGetComponent(out obstructionMesh);

                    if (!valid)
                        obstructionMesh = obstructions[i].gameObject.GetComponentInChildren<MeshRenderer>();

                    foreach (Material material in obstructionMesh.materials)
                    {
                        var color = material.color;
                        material.color = new Color(color.r, color.g, color.b, 1);
                    }
                }
            }
            obstructions = new Transform[hits.Length];
            // Hide the current obstructions
            for (int i = 0; i < hits.Length; i++)
            {
                Transform obstruction = hits[i].transform;
                MeshRenderer obstructionMesh;
                bool valid = obstruction.gameObject.TryGetComponent(out obstructionMesh);
                if (!valid)
                    obstructionMesh = obstruction.gameObject.GetComponentInChildren<MeshRenderer>();

                //obstructionShadowCastingMode.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                foreach (Material material in obstructionMesh.materials)
                {
                    var color = material.color;
                    material.color = new Color(color.r, color.g, color.b, transparency);
                }
                obstructions[i] = obstruction;
            }
            oldHitsNumber = hits.Length;
        }
        else
        {
            // Mean that no more stuff is blocking the view and sometimes all the stuff is not blocking as the same time
            if (obstructions != null && obstructions.Length > 0)
            {
                for (int i = 0; i < obstructions.Length; i++)
                {
                    MeshRenderer obstructionMesh;
                    bool valid = obstructions[i].gameObject.TryGetComponent(out obstructionMesh);

                    if (!valid)
                        obstructionMesh = obstructions[i].gameObject.GetComponentInChildren<MeshRenderer>();

                    foreach (Material material in obstructionMesh.materials)
                    {
                        var color = material.color;
                        material.color = new Color(color.r, color.g, color.b, 1);
                    }
                }
                oldHitsNumber = 0;
                obstructions = null;
            }
        }
    }
}
