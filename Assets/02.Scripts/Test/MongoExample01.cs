using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MongoExample01 : MonoBehaviour
{
    private void Start()
    {
        // var: 암시적 타입으로써 데이터의 자료형을 자동으로 설정하는 키워드
        // r-value로부터 자동으로 타입을 유추
        // 장점: 간단하다.
        // 단점: 자료형이 명확하지 않아서 휴먼 에러가 날 수 있다.
        // 언제쓰면 좋냐?: 1.자료형이 너무 길 경우
        //               2.foreach 반복문에서 명확할 경우
        List<Article> articles = new List<Article>();
        foreach (Article article in articles)
        {

        }


        // 몽고 데이터베이스에 연결
        // 연결 문자열: 데이터베이스 연결을 위한 정보가 담겨있는 문자열
        string connectionString = "mongodb+srv://heesoo:heesoo@cluster0.fqkymmx.mongodb.net/";
        // - 프로토콜: mongodb+srv
        // - 아이디: heesoo
        // - 비밀번호: heesoo
        // - 주소: cluster0.fqkymmx.mongodb.net

        // 1. 접속
        MongoClient mongoClient = new MongoClient(connectionString);

        // 2. 데이터베이스가 뭐뭐있지? -> 데이터베이스 조회
        List<BsonDocument> dbList = mongoClient.ListDatabases().ToList(); 
        foreach (BsonDocument db in dbList)
        {
            Debug.Log(db["name"]);
        }

        // 3. 특정 데이터베이스에 연결
        IMongoDatabase sampleDB = mongoClient.GetDatabase("sample_mflix");

        // 4. 콜렉션들 이름 확인
        List<string> collectionNames = sampleDB.ListCollectionNames().ToList();
        foreach(string name in collectionNames)
        {
            Debug.Log(name);
        }

        // 5. 콜렉션 연결 및 도큐먼트 개수 출력
        var movieCollection = sampleDB.GetCollection<BsonDocument>("movies");
        long count = movieCollection.CountDocuments(new BsonDocument());
        Debug.Log($"영화 개수: {count}");
        
        // 6. 도큐먼트 하나만 읽기
        var firstDoc = movieCollection.Find(new BsonDocument()).FirstOrDefault();
        Debug.Log(firstDoc["plot"]);
    }
}
