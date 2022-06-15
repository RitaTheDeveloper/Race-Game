using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRoad : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> blocks; // блоки, из которых будет состоять дорога

    [SerializeField]
    private int numberOfBlocks; // всего блоков в дороге;

    [SerializeField]
    private float lengthOfBlock; // длина блока

    GameObject player; // наш игрок

    private List<GameObject> blocksOfRoad = new List<GameObject>();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        BuildingTheRoad();
    }

    private void FixedUpdate()
    {

         MovingBlocks();
    }

    private void BuildingTheRoad()
    {
        // создаем дорогу из рандомных блоков
        for(int i = 0; i < numberOfBlocks; i++)
        {
            // берем рандомный блок
            int randomBlock = Random.Range(0, blocks.Count - 1);

            // и создаем (по оси х и y позиция блока одинакова, 
            var block = Instantiate(blocks[randomBlock], new Vector3(blocks[0].transform.position.x, blocks[0].transform.position.y, blocks[blocks.Count - 1].transform.position.z + lengthOfBlock), Quaternion.identity);
            block.transform.SetParent(gameObject.transform);
            blocks.Add(block);
            blocksOfRoad.Add(block);

        }
    }

    private void MovingBlocks()
    {
        //перемещаем блоки. Те блоки, которые оказались позади игрока, перемещаем вперед

        float zOfPlayer = player.GetComponent<Rigidbody>().position.z;

        // находим блок самый крайний по z


        for(int i = 0; i < blocksOfRoad.Count; i++)
        {
            bool fetched = blocksOfRoad[i].GetComponent<RoadBlock>().Fetch(zOfPlayer);
            if (fetched)
            {
                blocksOfRoad[i].transform.position = new Vector3(blocksOfRoad[i].transform.position.x, blocksOfRoad[i].transform.position.y, LastBlock().transform.position.z + lengthOfBlock);
            }
        }
    }

    private GameObject LastBlock()
    {
        var block = blocksOfRoad[0];
        for (int i = 0; i < blocksOfRoad.Count; i++)
        {
            if(blocksOfRoad[i].transform.position.z > block.transform.position.z)
            {
                block = blocksOfRoad[i];
            }
        }
        return block;
    }

}
