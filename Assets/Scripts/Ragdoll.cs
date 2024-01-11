using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] rbs;
    private Animator anim;
    private BoxCollider boxCollider;
    private CapsuleCollider capsuleCollider;
    private bool isDead;
    public bool canSpawn;

    [SerializeField] private float dieForce = 50f;
    [SerializeField] private Events events;

    [SerializeField] private GameObject moneyPrefab;

    [SerializeField] private GameObject FollowTarget;
    [SerializeField] private GameObject parentTarget;

    [SerializeField] private StackManager stackManager;

    // Start is called before the first frame update
    void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        capsuleCollider = GetComponentInParent<CapsuleCollider>();
        FollowTarget = GameObject.FindWithTag("Target");
        events = FindAnyObjectByType<Events>().GetComponent<Events>();
        parentTarget = GameObject.FindWithTag("EnemyTarget");
        stackManager = FindAnyObjectByType<StackManager>().GetComponent<StackManager>();

        DeactivateRagdoll();
    }

    public void DeactivateRagdoll()
    {
        foreach (var r in rbs) 
        { 
            r.isKinematic = true;
        }

        anim.enabled = true;
        boxCollider.enabled = true;
        capsuleCollider.enabled = true;
    }

    public void ActivateRagdoll()
    {
        foreach (var r in rbs)
        {
            r.isKinematic = false;
        }

        boxCollider.enabled = false;
        capsuleCollider.enabled = false;
        anim.enabled = false;

        if (stackManager.moneyCount < 30)
        {
           Instantiate(moneyPrefab, transform.position, FollowTarget.transform.rotation);
        }

        isDead = true;
    }


    public void ApplyForce(Vector3 force)
    {
        var rigidBody = anim.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidBody.AddForce(force, ForceMode.VelocityChange);
    }

    void Update()
    {
        if (events.isPuching == true)
        {
            ActivateRagdoll();
            ApplyForce(FollowTarget.transform.forward * dieForce);

            events.isPuching = false;
        }

        if (isDead == true)
        {
            Destroy(parentTarget, 3f);
        }
    }

    private void OnDestroy()
    {
        isDead = false;
        canSpawn = true;
    }
}
