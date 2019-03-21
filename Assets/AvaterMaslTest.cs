using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Playables;
using UnityEngine.Animations;

public class AvaterMaslTest : MonoBehaviour
{
    public AnimationClip clip1;
    public AnimationClip clip2;
    public Transform leftShoulder;

    PlayableGraph m_Graph;
    AnimationLayerMixerPlayable m_Mixer;

    public float mixLevel = 0.5f;

    public Animator animator;
    public AvatarMask mask;
    public AvatarMask enemyMask;
    public AvatarMask fullBody;

    public bool dothis;

    public List<GameObject> enemyBones;
    public List<GameObject> hitBones;

    public GameObject objectPushed;
    public GameObject childBone;
    public GameObject parentBone;
    public GameObject mainParent;

    public void Start()
    {
        enemyBones = new List<GameObject>();
        hitBones = new List<GameObject>();
        animator = GetComponent<Animator>();
        CreateAgain();
    }

    public void Update()
    {
        m_Mixer.SetInputWeight(1, mixLevel);
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Clicked");
            FindEnemyBones();
            CreateMask();
            CreateAgain();


            fullBody = mask;
        }
    }

    public void CreateAgain()
    {
        mask = enemyMask;

        m_Graph = PlayableGraph.Create();
        var playableOutput = AnimationPlayableOutput.Create(m_Graph, "LayerMixer", animator);
        playableOutput.SetSourcePlayable(m_Mixer);
        
        var clipPlayable1 = AnimationClipPlayable.Create(m_Graph, clip1);
        var clipPlayable2 = AnimationClipPlayable.Create(m_Graph, clip2);

        // Create mixer playable
        m_Mixer = AnimationLayerMixerPlayable.Create(m_Graph, 2);


        m_Mixer.ConnectInput(0, clipPlayable1, 0, 1.0f);
        m_Mixer.ConnectInput(1, clipPlayable2, 0, mixLevel);

        m_Mixer.SetLayerMaskFromAvatarMask(1, mask);

        m_Graph.Play();
    }

    public void FindEnemyBones()
    {
        enemyBones.Clear();
        hitBones.Clear();

        mainParent = objectPushed.transform.parent.GetComponentInParent<EnemyScript>().gameObject;


        parentBone = objectPushed.transform.parent.gameObject;
        childBone = objectPushed.transform.transform.GetChild(0).gameObject;

        AddAllChildren(mainParent.transform, enemyBones, hitBones);
        CreateMask();
    }

    private void AddAllChildren(Transform parent, List<GameObject> fullList, List<GameObject> hitList)
    {

        foreach (Transform child in parent)
        {

            if (child.gameObject.GetComponent<Rigidbody>() && (child.gameObject == parentBone || child.gameObject == childBone || child.gameObject == objectPushed))
            {
                hitList.Add(child.gameObject);
            }

            fullList.Add(child.gameObject);

            AddAllChildren(child, fullList, hitList);
        }
    }

    public void CreateMask()
    {
        enemyMask = new AvatarMask();
        enemyMask.AddTransformPath(mainParent.transform);

        for (int i = 0; i < enemyBones.Count; i++)
        {
            if (enemyBones[i].gameObject.name == parentBone.name || enemyBones[i].gameObject.name == childBone.name || enemyBones[i].gameObject.name == objectPushed.name)
            {
                enemyMask.SetTransformActive(i, false);
            }

            //for (int x = 0; x < hitBones.Count; x++)
            //{
            //    enemyMask.SetTransformActive(i, false);
            //}
        }

    }

    private void OnDestroy()
    {
        m_Graph.Destroy();
    }
}
