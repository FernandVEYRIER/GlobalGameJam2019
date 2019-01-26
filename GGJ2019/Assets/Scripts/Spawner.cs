using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform blockPath;
    public float blockSpeed = 50f;
    public float angularSpeed = 10f;

    private List<Block> m_blockQueue = new List<Block>();
    private float spriteHeight;
    private List<Transform> m_path = new List<Transform>();

    private int m_isInit;
    private float m_slowSpeed;

    public class Block
    {
        public GameObject obj;
        public Transform transform;
        public int currentID;
        public int targetID;
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteHeight = prefabs[0].GetComponent<SpriteRenderer>().bounds.size.y;
        m_slowSpeed = blockSpeed / 10f;
        InitPath();
    }

    //Init path transform point
    void InitPath()
    {
        m_isInit = blockPath.childCount;
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

    private void MoveBlockTowardNext(Block block)
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

    Block InstantiateBlock(int targetPos)
    {
        var dice = Random.Range(0, prefabs.Length);
        var obj = Instantiate(prefabs[dice], m_path[0].position, Quaternion.identity);
        obj.transform.parent = transform;
        var block = new Block() { obj = obj, transform = obj.transform, currentID = 0, targetID = targetPos };
        return block;
    }

    Block TakeBlock()
    {
        if (m_isInit > 0 || m_blockQueue[0].currentID == m_path.Count) return null;
        blockSpeed = m_slowSpeed;
        var block = m_blockQueue[0];
        m_blockQueue.RemoveAt(0);
        block.transform.SetParent(null);
        block.transform.position += Vector3.down * spriteHeight * 3;
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
}
