using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; //생성할 총알 프리팹
    public float spawnRateMin = 0.5f; // 최소 생성 주기
    public float spawnRateMax = 3f;// 최대 생성 주기

    private Transform target;//발사할 대상
    private float spawnRate;//생성 주기
    private float timeAfterSpawn;//최근 생성 시점에서 지난 시간

    void Start()
    {
        //최근 생성 이후의 누적 시간을 0으로 초기화
        timeAfterSpawn = 0f;
        //총알 생성 간격을, Min,Max 사이에서 랜덤 지정
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        //PlayerController 컴포넌트를 가진 게임 오브젝트를 타겟으로 설정
        target = FindObjectOfType<PlayerController>().transform;
    }

    //총알 생성하기
    void Update()
    {
        //timeAfterSpawn 갱신
        timeAfterSpawn += Time.deltaTime;

        //최근 생성 시점에서부터 누적된 시간이 생성주기보다 크거나 같다면
        if(timeAfterSpawn>=spawnRate)
        {
            //누적 시간 초기화
            timeAfterSpawn = 0f;

            //bullet 프리팹 생성
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            //생성된 bullet의 정면 방향이 target을 향하도록 회전
            bullet.transform.LookAt(target);

            //다음 생성 간격을 SpawnRateMin, SpawnRateMax 사이에서 랜덤지정
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}
