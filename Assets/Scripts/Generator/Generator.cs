using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject _player;
    public GameObject _prefabToInstantiate; // ��Ҫ���ɵ������Ԥ����
    public float generationInterval; // ���ɵ�ʱ����
    public int generationCountLimit; // ���ɵ��������ƣ�0��ʾ������
    public Vector2 positionOffset; // ��������λ��ƫ����

    private float generationTimer; // ��ʱ���������ۼ��ϴ����������ʱ��
    public int generationCount; // �����ɵ����������

    public void Start()
    {
        transform.position += (Vector3)positionOffset; // ��λ��ƫ�����ӵ���������λ����
        generationTimer = generationInterval; // ��ʼ����ʱ��Ϊ��ʼʱ�������Ա��ڿ�ʼʱ��������һ������
    }

    void Update()
    {
        transform.position = _player.transform.position;//ÿ�θ��½�������λ����Ϊ���λ��
        positionOffset = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        transform.position += (Vector3)positionOffset; // ��λ��ƫ�����ӵ���������λ����
        if (generationCountLimit > 0 && generationCount >= generationCountLimit) // ���ɵ������ﵽ������
        {
            return;
        }

        generationTimer += Time.deltaTime; // ��ʱ����֡�ۼ�

        if (generationTimer >= generationInterval) // ��ʱ������ʱ��������Ҫ�����µ�����
        {
            GenerateItem(); // ��������ľ������
            generationTimer = 0f; // ���ü�ʱ�����Ա㿪ʼ��һ�ּ�ʱ
        }
    }

    protected virtual void GenerateItem() // ����������鷽�����������override
    {
        GameObject newObject = Instantiate(_prefabToInstantiate, transform.position, Quaternion.identity);
        generationCount++;
    }
}