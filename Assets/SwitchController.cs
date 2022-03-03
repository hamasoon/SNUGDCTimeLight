using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour, IInteractable
{
    private Animator animator;
    [SerializeField] private Renderer[] renderers;
    Material[] materials;

    private bool interacted = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        List<Material> totalSharedMaterials = new List<Material>();

        for (int i = 0; i < renderers.Length; i++)
        {
            List<Material> sharedMaterials = new List<Material>();
            renderers[i].GetSharedMaterials(sharedMaterials);

            totalSharedMaterials.AddRange(sharedMaterials);
        }

        materials = totalSharedMaterials.ToArray();

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetColor("_MainColor", new Color(0.1f, 0.1f, 0.1f));
        }
    }

    public void Interact()
    {
        if (interacted) return;
        interacted = true;

        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        animator.SetTrigger("Trigger");
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            if (elapsed > 1f) elapsed = 1f;

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetColor("_MainColor",Color.Lerp(new Color(0.1f, 0.1f, 0.1f), Color.white, elapsed / 1f));
            }

            yield return null;
        }

        FindObjectOfType<PlayerController>().canMove = true;
    }
}
