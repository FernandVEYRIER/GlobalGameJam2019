using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Blocks;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform blockPath;
    public float blockSpeed = 50f;
    public float angularSpeed = 10f;
    public bool left;

    private List<ABlock> m_blockQueue = new List<ABlock>();
    private List<Transform> m_path = new List<Transform>();

    private int m_isInit;
    private float m_slowSpeed;


    // Start is called before the first frame update
    void Start()
    {
        m_slowSpeed = blockSpeed / 10f;
        InitPath();
    }

    //Init path transform point
    void InitPath()
    {
        m_isInit = blockPath.childCount - 1;
        for (int i = 0; i < m_isInit; i++)
        {
            m_path.Add(blockPath.GetChild(i));
        }
    }

    void Update()
    {
        if (m_isInit > 0)
        {
            if (m_blockQueue.Count == 0 || m_blockQueue[m_blockQueue.Count - 1].currentID == m_blockQueue[m_blockQueue.Count - 1].targetID)
            {
               m_blockQueue.Add(InstantiateBlock(--m_isInit));
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeBlock();
        }

        MoveBlocks();
    }

    private void MoveBlocks()
    {
        foreach (var block in m_blockQueue)
        {
            if (block.currentID != block.targetID)
                MoveBlockTowardNext(block);
        }
    }

    private void MoveBlockTowardNext(ABlock block)
    {
        var point = m_path[block.currentID + 1];
        block.transform.position = Vector3.MoveTowards(block.transform.position, point.position,
            blockSpeed * Time.deltaTime);
        block.transform.rotation =
            Quaternion.RotateTowards(block.transform.rotation, point.rotation, angularSpeed * Time.deltaTime);
        if (Vector3.Distance(block.transform.position, point.position) < 0.001f)
            ++block.currentID;
    }

    //void InitSpawner()
    //{
    //    for (int i = 0; i < blockCount; i++)
    //    {
    //        EnqueueBlock(InstantiateBlock());
    //    }
    //}

    //void EnqueueBlock(GameObject block)
    //{
    //    for (int i = 1; i < transform.childCount; i++)
    //    {
    //        transform.GetChild(i).position += Vector3.down * spriteHeight;
    //    }
    //    m_blockQueue.Enqueue(block);
    //}

    ABlock InstantiateBlock(int targetPos)
    {
        var dice = Random.Range(0, prefabs.Length);
        var obj = Instantiate(prefabs[dice], m_path[0].position, Quaternion.identity);
        obj.transform.parent = transform;
        var block = obj.GetComponent<ABlock>();
        block.currentID = 0;
        block.targetID = targetPos;
        return block;
    }

    public ABlock TakeBlock()
    {
        if (m_isInit > 0 || m_blockQueue[0].currentID == m_path.Count) return null;
        blockSpeed = m_slowSpeed;
        var block = m_blockQueue[0];
        m_blockQueue.RemoveAt(0);
        block.transform.SetParent(null);
        MoveAllBlockNext();
        m_blockQueue.Add(InstantiateBlock(0));
        return block;
    }

    void MoveAllBlockNext()
    {
        foreach (var block in m_blockQueue)
        {
            ++block.targetID;
        }
    }

    public Vector3 GetLastPoint()
    {
        return blockPath.GetChild(blockPath.childCount - 1).position;
    }
}
