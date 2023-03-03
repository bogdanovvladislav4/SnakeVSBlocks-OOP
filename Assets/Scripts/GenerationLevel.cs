using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GenerationLevel : MonoBehaviour
{
    public GameObject[] VariantPrefabs;
    public GameObject DefaultPrefab;
    public GameObject FinishPrefab;
    public GameObject[] VariantFoods;
    public float DistanceBetweenPlatform;
    public Transform EdgeRight;
    public Transform EdgeLeft;
    public Game game;
    public Material material;

    private int _MinElements = 5;
    private int _MaxElemtnts = 10;


    private void Awake()
    {
        int level = game.LevelIndex;
        Random random = new Random(level);
        int prefabsCount = RandomRange(random, _MinElements + level * 2, _MaxElemtnts + level * 2 + 2);
        Random rnd = new Random();

        for (int i = 0; i < prefabsCount; i++)
        {
            int prefabIndex = RandomRange(random, 0, VariantPrefabs.Length);
            GameObject variantPrefab = i == 0 ? DefaultPrefab : VariantPrefabs[prefabIndex];
            GameObject variant = Instantiate(variantPrefab, transform);
            GameObject variantFood = VariantFoods[prefabIndex];
            GameObject food = Instantiate(variantFood, transform);
            variant.transform.localPosition = CalculatePrefabPosition(i);
            food.transform.localPosition = CalculateFoodPosition(i, rnd);
            if(i == 0){
                food.transform.localPosition = new Vector3(CalculateFoodPosition(i, rnd).x, CalculateFoodPosition(i, rnd).y,
                    CalculateFoodPosition(i, rnd).z + 2f);
            }
            foreach (Transform child in variant.GetComponentsInChildren<Transform>())
            {
                
                if (child.TryGetComponent(out TextMesh textMesh)) {   
                    textMesh.text = RandomTextOnCube(rnd, 1, 8).ToString();
                }

            }
            for(int j = 0; j < variant.transform.childCount - 1; j++)
            {
                if(variant.transform.GetChild(j).TryGetComponent(out MeshRenderer meshRenderer) && i != 0) 
                {
                    MeshRenderer renderer = variant.transform.GetChild(j).GetComponent<MeshRenderer>();
                    TextMesh textMesh = variant.transform.GetChild(j).GetChild(0).GetChild(0).GetComponent<TextMesh>();
                    float randomValue = float.Parse(textMesh.text);
                    Material m = new Material(material);
                    renderer.material = m;
                    renderer.material.SetFloat("Vector1_dd0c450485a64c40a5882fdd7a696cea", randomValue / 8);
                }
            }
            
            foreach (Transform child in food.GetComponentsInChildren<Transform>())
            {
                if (child.TryGetComponent(out TextMesh textMesh)) textMesh.text = RandomTextOnCube(rnd, 1, 8).ToString();
            }
        }
        GameObject finish = Instantiate(FinishPrefab, transform);
        finish.transform.localPosition = CalculatePrefabPosition(prefabsCount + 1);
        finish.transform.localPosition = new Vector3(finish.transform.position.x, finish.transform.position.y - 0.9f, finish.transform.position.z);
    }

    private int RandomRange(Random random, int min, int maxExclusive)
    {
        int number = random.Next();
        int length = maxExclusive - min;
        length = length == 0 ? length + 1 : length;
        number %= length;
        return min + number;
    }
    private Vector3 CalculatePrefabPosition(int i)
    {
        return new Vector3(EdgeRight.position.x - EdgeLeft.position.x + 0.5f, 1, DistanceBetweenPlatform * i + 35);
    }

    private int RandomTextOnCube(Random rnd, int min, int max)
    {
        int randomValue = rnd.Next(min, max);
        return randomValue;
    }

    private Vector3 CalculateFoodPosition(int i, Random rnd)
    {
        
        return new Vector3(RandomPositionFood(rnd, EdgeLeft.position.x + 2f, EdgeRight.position.x - 0.5f), 0.5f, DistanceBetweenPlatform * i + 25);
    }

    private float RandomPositionFood(Random rnd, float min, float max)
    {
        float randomValue = (float) rnd.NextDouble() * (min - max) + max;
        return randomValue;
    }

}
