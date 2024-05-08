using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static UnityEngine.ParticleSystem;

// 1. 하나만을 보장
// 2. 어디서든 쉽게 접근 가능
public class ArticleManager : MonoBehaviour
{
    // 게시글 리스트
    private List<Article> _articles = new List<Article>();
    public List<Article> Articles => _articles;

    public static ArticleManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        // 몽고 DB로부터 article 조회
        // 1. 몽고DB 연결
        string connectionString = "mongodb+srv://heesoo:heesoo@cluster0.fqkymmx.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        // 2. 특정 데이터베이스 연결
        IMongoDatabase sampleDB = mongoClient.GetDatabase("metaverse");
        // 3. 특정 콜렉션 연결
        var articleCollection = sampleDB.GetCollection<BsonDocument>("articles");
        // 4. 모든 문서 읽어오기
        // 5. 읽어온 문서만큼 New Article()해서 데이터 채우고
        // _articles에 넣기
        BsonDocument filter = new BsonDocument();
        //filter["ArticleType"] = 1;
        var Bsondocument = articleCollection.Find(filter).ToList();

        _articles = new List<Article>();
        foreach (var article in Bsondocument)
        {
            _articles.Add(new Article()
            {
                ArticleType = (ArticleType)(int)article["ArticleType"],
                Name = article["Name"].ToString(),
                Content = article["Content"].ToString(),
                WriteTime = DateTime.Parse(article["WriteTime"].ToString()),
                Like = (int)article["Like"]
            });
        }
    }
}
