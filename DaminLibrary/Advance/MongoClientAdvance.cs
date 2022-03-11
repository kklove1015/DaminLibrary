using DaminLibrary.Collections;
using DaminLibrary.Expansion;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaminLibrary.Advance
{
    public class MongoClientAdvance
    {
        public MongoClient mongoClient { get; set; }

        public MongoClientAdvance(string host, string account, string password)
        {
            this.mongoClient = new MongoClient("mongodb://" + account + ":" + password + "@" + host + "/?safe=true&maxPoolSize=3000");
        }

        public static string GenerateNewId()
        {
            ObjectId generateNewId = ObjectId.GenerateNewId();
            string generateNewIdToString = generateNewId.ToString();
            return generateNewIdToString;
        }

        private IMongoCollection<T> GetCollection<T>(string name, string collectionName)
        {
            IMongoDatabase getDatabase = this.mongoClient.GetDatabase(name);
            IMongoCollection<T> getCollection = getDatabase.GetCollection<T>(collectionName);
            return getCollection;
        }
        private IMongoCollection<T> GetCollection<T>(string name)
        {
            IMongoDatabase getDatabase = this.mongoClient.GetDatabase(name);
            Type type = typeof(T);
            string createClassName = type.CreateClassName(".");
            IMongoCollection<T> getCollection = getDatabase.GetCollection<T>(createClassName);
            return getCollection;
        }


        #region Create
        private FilterDefinition<T> CreateFilterDefinition<T>(Statuses status, List<Filter> filterList = null, CancellationToken cancellationToken = default)
        {
            string statusToString = status.ToString();
            FilterDefinition<T> eq = Builders<T>.Filter.Eq("Status", statusToString);
            if (filterList.IsNullOrEmpty() != true)
            {
                for (int i = 0; i <= filterList.Count - 1; i++)
                {
                    if (cancellationToken.IsCancellationRequested == true) { return null; }
                    eq = eq & Builders<T>.Filter.Eq(filterList[i].field, filterList[i].value);
                }
            }
            return eq;
        }
        private FilterDefinition<T> CreateFilterDefinition<T>(List<Filter> filterList = null, CancellationToken cancellationToken = default)
        {
            FilterDefinition<T> eq = Builders<T>.Filter.Eq(filterList.First().field, filterList.First().value);
            if (filterList.IsNullOrEmpty() != true)
            {
                for (int i = 1; i <= filterList.Count - 1; i++)
                {
                    if (cancellationToken.IsCancellationRequested == true) { return null; }

                    eq = eq & Builders<T>.Filter.Eq(filterList[i].field, filterList[i].value);
                }
            }
            return eq;
        }
        private SortDefinition<T> CreateSortDefinition<T>(Sorts sort)
        {
            string field = "_id";
            if (sort == Sorts.Asc)
            {
                SortDefinition<T> ascending = Builders<T>.Sort.Ascending(field);
                return ascending;
            }
            SortDefinition<T> descending = Builders<T>.Sort.Descending(field);
            return descending;
        }
        private UpdateDefinition<T> CreateUpdateDefinition<T>(List<Update> updateList)
        {
            UpdateDefinitionBuilder<T> result = Builders<T>.Update;
            UpdateDefinition<T> updateDefinition = result.Set(updateList[0].field, updateList[0].value);
            for (int i = 1; i <= updateList.Count - 1; i++)
            {
                updateDefinition = updateDefinition.Set(updateList[i].field, updateList[i].value);
            }
            return updateDefinition;
        }
        public IndexKeysDefinition<T> CreateIndexText<T>(string field)
        {
            return Builders<T>.IndexKeys.Text(field);
        }

        #endregion

        #region Find
        public List<T> Find<T>(string name, string collectionName, Statuses status, List<Filter> filterList, Sorts sort, int limit, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name, collectionName);
            FilterDefinition<T> createFilterDefinition = this.CreateFilterDefinition<T>(status, filterList, cancellationToken);
            SortDefinition<T> createSortDefinition = this.CreateSortDefinition<T>(sort);
            IFindFluent<T, T> find = getCollection.Find(createFilterDefinition).Sort(createSortDefinition).Limit(limit);
            List<T> findToList = find.ToList();
            return findToList;
        }
        public List<T> Find<T>(string name, Statuses status, List<Filter> filterList, Sorts sort, int limit, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name);
            FilterDefinition<T> createFilterDefinition = this.CreateFilterDefinition<T>(status, filterList, cancellationToken);
            SortDefinition<T> createSortDefinition = this.CreateSortDefinition<T>(sort);
            IFindFluent<T, T> find = getCollection.Find(createFilterDefinition).Sort(createSortDefinition).Limit(limit);
            List<T> findToList = find.ToList();
            return findToList;
        }
        public List<T> Find<T>(string name, string collectionName, Statuses status, int limit, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name, collectionName);
            FilterDefinition<T> createFilterDefinition = this.CreateFilterDefinition<T>(status, null, cancellationToken);
            IFindFluent<T, T> find = getCollection.Find(createFilterDefinition).Limit(limit);
            List<T> findToList = find.ToList();
            return findToList;
        }
        public List<T> Find<T>(string name, string collectionName, List<Filter> filterList, Sorts sort, int limit, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name, collectionName);
            FilterDefinition<T> createFilterDefinition = this.CreateFilterDefinition<T>(filterList, cancellationToken);
            SortDefinition<T> createSortDefinition = this.CreateSortDefinition<T>(sort);
            IFindFluent<T, T> find = getCollection.Find(createFilterDefinition).Sort(createSortDefinition).Limit(limit);
            List<T> findToList = find.ToList();
            return findToList;
        }
        public List<T> Find<T>(string name, Statuses status, List<Filter> filterList, int limit, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name);
            FilterDefinition<T> createFilterDefinition = this.CreateFilterDefinition<T>(status, filterList, cancellationToken);
            IFindFluent<T, T> find = getCollection.Find(createFilterDefinition).Limit(limit);
            List<T> findToList = find.ToList();
            return findToList;
        }
        public List<T> Find<T>(string name, List<Filter> filterList, int limit, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name);
            FilterDefinition<T> createFilterDefinition = this.CreateFilterDefinition<T>(filterList, cancellationToken);
            IFindFluent<T, T> find = getCollection.Find(createFilterDefinition).Limit(limit);
            List<T> findToList = find.ToList();
            return findToList;
        }
        public List<T> Find<T>(string name, Statuses status, Sorts sorts, int limit, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollectionResult = this.GetCollection<T>(name);
            SortDefinition<T> createSortDefinition = this.CreateSortDefinition<T>(sorts);
            FilterDefinition<T> filterDefinition = Builders<T>.Filter.Eq("Status", status.ToString());
            IFindFluent<T, T> findResult = getCollectionResult.Find(filterDefinition).Sort(createSortDefinition).Limit(limit);
            List<T> toListResult = findResult.ToList();
            return toListResult;
        }

        /// <summary>
        /// 모든 컬렉션을 가져오는 메서드입니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public List<T> Find<T>(string name, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollectionResult = this.GetCollection<T>(name);
            IFindFluent<T, T> findResult = getCollectionResult.Find(_ => true);
            List<T> toListResult = findResult.ToList();
            return toListResult;
        }
        public List<T> Find<T>(string name, int limit, List<Filter> filterList = null, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name);
            IFindFluent<T, T> find = null;
            switch (filterList.IsNull() == true)
            {
                case true:
                    find = getCollection.Find(_ => true).Limit(limit);
                    break;
                case false:
                    FilterDefinition<T> createFilterDefinition = this.CreateFilterDefinition<T>(filterList, cancellationToken);
                    find = getCollection.Find(createFilterDefinition).Limit(limit);
                    break;
            }
            List<T> toList = find.ToList();
            //IEnumerable<string> select = objectList.Select(x => x.ToJson());
            //List<string> stringList = select.ToList();
            return toList;
        }
        public List<string> Find(string name, string collectionName, List<Filter> filterList = null, CancellationToken cancellationToken = default)
        {
            IMongoCollection<object> getCollection = this.GetCollection<object>(name, collectionName);
            IFindFluent<object, object> find = null;
            switch (filterList.IsNull() == true)
            {
                case true:
                    find = getCollection.Find(_ => true);
                    break;
                case false:
                    FilterDefinition<object> createFilterDefinition = this.CreateFilterDefinition<object>(filterList, cancellationToken);
                    find = getCollection.Find(createFilterDefinition);
                    break;
            }
            List<object> objectList = find.ToList();
            IEnumerable<string> select = objectList.Select(x => x.ToJson());
            List<string> stringList = select.ToList();
            return stringList;
        }
        public T FindOne<T>(string name, string collectionName, Statuses status, Sorts sorts, CancellationToken cancellationToken = default)
        {
            List<T> find = this.Find<T>(name, collectionName, status, null, sorts, 1, cancellationToken);
            if (find.IsNullOrEmpty() == true) { return default; }
            T first = find.First();
            return first;
        }
        public T FindOne<T>(string name, string collectionName, Filter filter, CancellationToken cancellationToken = default)
        {
            T first = FindOne<T>(name, collectionName, new List<Filter>() { filter }, cancellationToken);
            return first;
        }
        public T FindOne<T>(string name, string collectionName, List<Filter> filterList, CancellationToken cancellationToken = default)
        {
            List<T> find = this.Find<T>(name, collectionName, filterList, Sorts.Desc, 1, cancellationToken);
            if (find.IsNullOrEmpty() == true) { return default; }
            T first = find.First();
            return first;
        }
        public T FindOne<T>(string name, Statuses status, List<Filter> filterList, CancellationToken cancellationToken = default)
        {
            List<T> find = this.Find<T>(name, status, filterList, Sorts.Desc, 1, cancellationToken);
            if (find.IsNullOrEmpty() == true) { return default; }
            T first = find.First();
            return first;
        }
        public T FindOne<T>(string name, List<Filter> filterList, CancellationToken cancellationToken = default)
        {
            List<T> find = this.Find<T>(name, filterList, 1, cancellationToken);
            if (find.IsNullOrEmpty() == true) { return default; }
            return find.First();
        }
        public List<T> FindSkip<T>(string name, int skip, int limit, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollectionResult = this.GetCollection<T>(name);
            IFindFluent<T, T> findResult = getCollectionResult.Find(_ => true).Skip(skip).Limit(limit);
            List<T> toListResult = findResult.ToList();
            return toListResult;
        }
        public List<T> FindSkip<T>(string name, Statuses status, List<Filter> filterList, int skip, int limit, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollectionResult = this.GetCollection<T>(name);
            FilterDefinition<T> createFilterDefinition = this.CreateFilterDefinition<T>(status, filterList, cancellationToken);
            IFindFluent<T, T> find = getCollectionResult.Find(createFilterDefinition).Skip(skip).Limit(limit);
            List<T> result = find.ToList();
            return result;
        }
        public List<T> FindSkip<T>(string database, string collection, List<Filter> filters, Sorts sort, int skip, int limit, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> iMongoCollection = this.GetCollection<T>(database, collection);
            FilterDefinition<T> filterDefinition = CreateFilterDefinition<T>(filters);
            SortDefinition<T> sortDefinition = CreateSortDefinition<T>(sort);
            return iMongoCollection.Find(filterDefinition).Sort(sortDefinition).Skip(skip).Limit(limit).ToList();
        }
        public List<T> FindSkip<T>(string database, string collection, Filter filter, Sorts sort, int skip, int limit, CancellationToken cancellationToken = default)
        {
            var filters = new List<Filter>() { filter };
            return FindSkip<T>(database, collection, filters, sort, skip, limit, cancellationToken);
        }

        /// <summary>
        /// 한가지 필드에 특정 값들 검색
        /// </summary>
        /// <typeparam name="T">컬렉션 타입</typeparam>
        /// <param name="name">데이터 베이스 이름</param>
        /// <param name="filter">Field, ListValue</param>
        /// <param name="sorts">정렬</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public List<T> FindIn<T>(string name, Filter filter, Sorts sorts = Sorts.Desc, CancellationToken cancellationToken = default)
        {
            if (name.IsNullOrWhiteSpace() == true) { return null; }
            if (filter.IsNull() == true) { return null; }
            if (filter.field.IsNullOrWhiteSpace() == true) { return null; }
            if (filter.valueList.IsNullOrEmpty() == true) { return null; }
            IMongoCollection<T> iMongoCollection = GetCollection<T>(name);
            FilterDefinition<T> @in = Builders<T>.Filter.In(filter.field, filter.valueList);
            var sort = this.CreateSortDefinition<T>(sorts);
            IFindFluent<T, T> iFindFluent = iMongoCollection.Find(@in).Sort(sort);
            List<T> result = iFindFluent.ToList();
            return result;
        }
        #endregion

        #region Insert
        public int InsertMany<T>(string name, List<string> classNameList, List<T> documents, CancellationToken cancellationToken = default)
        {
            var createClassName = classNameList.Join("_");
            int insertMany = this.InsertMany(name, createClassName, documents, cancellationToken);
            return insertMany;
        }
        #endregion

        #region Update
        public long UpdateMany<T>(string name, string collectionName, Statuses status, Filter filter, List<Update> updateList, CancellationToken cancellationToken = default)
        {
            var filterList = new List<Filter>() { filter };
            IMongoCollection<T> getCollection = this.GetCollection<T>(name, collectionName);
            FilterDefinition<T> createFilterDefinition = this.CreateFilterDefinition<T>(status, filterList, cancellationToken);
            UpdateDefinition<T> createUpdateDefinition = this.CreateUpdateDefinition<T>(updateList);
            var updateResult = getCollection.UpdateMany(createFilterDefinition, createUpdateDefinition);
            return updateResult.ModifiedCount;
        }
        public long UpdateManyIn<T>(string name, Statuses status, Filter filter, List<Update> updateList, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name);
            FilterDefinition<T> createFilterDefinition = Builders<T>.Filter.In(filter.field, filter.valueList);
            UpdateDefinition<T> createUpdateDefinition = this.CreateUpdateDefinition<T>(updateList);
            var updateResult = getCollection.UpdateMany(createFilterDefinition, createUpdateDefinition);
            return updateResult.ModifiedCount;
        }
        public long UpdateMany<T>(string name, Statuses status, List<Filter> filterList, List<Update> updateList, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name);
            FilterDefinition<T> createFilterDefinition = this.CreateFilterDefinition<T>(status, filterList, cancellationToken);
            UpdateDefinition<T> createUpdateDefinition = this.CreateUpdateDefinition<T>(updateList);
            var updateResult = getCollection.UpdateMany(createFilterDefinition, createUpdateDefinition);
            return updateResult.ModifiedCount;

            //long modifiedCount = 0;
            //updateList.ForEach(x =>
            //{
            //    UpdateDefinition<T> set = Builders<T>.Update.Set(x.Field, x.Value);
            //    UpdateResult updateMany = getCollection.UpdateMany(createFilterDefinition, set);
            //    modifiedCount = modifiedCount + updateMany.ModifiedCount;
            //});
            //return modifiedCount;
        }
        public long UpdateMany<T>(string name, Statuses status, List<Filter> filterList, Update update, CancellationToken cancellationToken = default)
        {
            var updateList = new List<Update>() { update };
            return this.UpdateMany<T>(name, status, filterList, updateList, cancellationToken);
        }
        public long UpdateMany<T>(string name, Statuses status, Filter filter, List<Update> updateList, CancellationToken cancellationToken = default)
        {
            var filterList = new List<Filter>() { filter };
            return this.UpdateMany<T>(name, status, filterList, updateList, cancellationToken);
        }
        public long UpdateMany<T>(string name, Statuses status, Filter filter, Update update, CancellationToken cancellationToken = default)
        {
            var updateList = new List<Update>() { update };
            return this.UpdateMany<T>(name, status, filter, updateList, cancellationToken);
        }
        public long UpdateMany<T>(string name, string id, Statuses status, Update update, CancellationToken cancellationToken = default)
        {
            var filter = new Filter("_id", id);
            return this.UpdateMany<T>(name, status, filter, update, cancellationToken);
        }
        #endregion

        #region Delete
        public long DeleteMany<T>(string name, Statuses status, List<Filter> filterList = null, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name);
            FilterDefinition<T> createFilterDefinition = this.CreateFilterDefinition<T>(status, filterList, cancellationToken);
            DeleteResult deleteMany = getCollection.DeleteMany(createFilterDefinition, cancellationToken);
            return deleteMany.DeletedCount;
        }

        public long DeleteMany<T>(string name, List<Filter> filterList, CancellationToken cancellationToken = default)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name);
            FilterDefinition<T> createFilterDefinition = this.CreateFilterDefinition<T>(filterList, cancellationToken);
            DeleteResult deleteMany = getCollection.DeleteMany(createFilterDefinition);
            return deleteMany.DeletedCount;
        }
        #endregion

        #region Drop
        public void DropCollection(string name, string collectionName, CancellationToken cancellationToken = default)
        {
            IMongoDatabase getDatabase = this.mongoClient.GetDatabase(name);
            getDatabase.DropCollection(collectionName, cancellationToken);
        }
        public void DropCollection<T>(string name, CancellationToken cancellationToken = default)
        {
            var type = typeof(T);
            var createClassName = type.CreateClassName();
            createClassName = createClassName.Replace("_", ".");
            this.DropCollection(name, createClassName, cancellationToken);
        }
        public void DropIndexes<T>(string name)
        {
            var getCollection = this.GetCollection<T>(name);
            getCollection.Indexes.DropAll();
        }
        #endregion

        #region 샤딩
        private BsonDocument EnableSharding(string name, CancellationToken cancellationToken = default)
        {
            IMongoDatabase getDatabase = this.mongoClient.GetDatabase("admin");
            var bsonDocument = new BsonDocument() { { "enableSharding", "{" + name + "}" } };
            var runCommand = getDatabase.RunCommand<BsonDocument>(bsonDocument, null, cancellationToken);
            return runCommand;
        }
        private IndexKeysDefinition<BsonDocument> CreateIndexKeysDefinition(string name, string collectionName)
        {
            IMongoCollection<BsonDocument> getCollection = this.GetCollection<BsonDocument>(name, collectionName);
            IndexKeysDefinition<BsonDocument> ascending = Builders<BsonDocument>.IndexKeys.Ascending("_id");
            return ascending;
        }
        /// <summary>
        /// 샤딩 서버용
        /// </summary>
        /// <param name="name"></param>
        /// <param name="collectionName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private string CreateOne(string name, string collectionName, CancellationToken cancellationToken = default)
        {
            IMongoCollection<BsonDocument> getCollection = this.GetCollection<BsonDocument>(name, collectionName);
            IndexKeysDefinition<BsonDocument> createIndexKeysDefinition = this.CreateIndexKeysDefinition(name, collectionName);
            var createIndexModel = new CreateIndexModel<BsonDocument>(createIndexKeysDefinition);
            string createOne = getCollection.Indexes.CreateOne(createIndexModel, null, cancellationToken);
            return createOne;
        }
        private Dictionary<string, object> CreateShardCollectionKeyDictionary()
        {
            var dictionary = new Dictionary<string, object>() { { "_id", "hashed" } };
            return dictionary;
        }
        private BsonDocument ShardCollection(string name, string collectionName, CancellationToken cancellationToken = default)
        {
            BsonDocument enableSharding = this.EnableSharding(name, cancellationToken);
            string createOne = this.CreateOne(name, collectionName, cancellationToken);
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("shardCollection", name + "." + collectionName);
            Dictionary<string, object> createShardCollectionKeyDictionary = this.CreateShardCollectionKeyDictionary();
            dictionary.Add("key", createShardCollectionKeyDictionary);
            IMongoDatabase getDatabase = this.mongoClient.GetDatabase("admin");
            var bsonDocument = new BsonDocument(dictionary);
            var bsonDocumentCommand = new BsonDocumentCommand<BsonDocument>(bsonDocument);
            BsonDocument runCommand = getDatabase.RunCommand(bsonDocumentCommand, null, cancellationToken);
            return runCommand;
        }
        private int InsertMany<T>(string name, string collectionName, List<T> documents, CancellationToken cancellationToken = default)
        {
            //this.ShardCollection(name, collectionName);
            IMongoCollection<T> getCollection = this.GetCollection<T>(name, collectionName);
            int count = documents.Count;
            var insertManyOptions = new InsertManyOptions() { IsOrdered = false };
            try
            {
                getCollection.InsertMany(documents, insertManyOptions, cancellationToken);
            }
            catch (MongoBulkWriteException mongoBulkWriteException) { count = count - mongoBulkWriteException.WriteErrors.Count; }
            if (count.IsZeroOrUnsign() == true) { return 0; }

            return count;
        }
        public int InsertMany<T>(string name, List<T> documents, CancellationToken cancellationToken = default)
        {
            Type type = typeof(T);
            string createClassName = type.CreateClassName(".");
            int insertMany = this.InsertMany(name, createClassName, documents, cancellationToken);
            return insertMany;
        }
        public bool InsertOne<T>(string name, string collectionName, T document, CancellationToken cancellationToken = default)
        {
            int insertMany = this.InsertMany(name, collectionName, new List<T>() { document }, cancellationToken);
            if (insertMany.IsZeroOrUnsign() == true) { return false; }
            return true;
        }
        public bool InsertOne<T>(string name, T document, CancellationToken cancellationToken = default)
        {
            var documents = new List<T>() { document };
            int insertMany = this.InsertMany(name, documents, cancellationToken);
            if (insertMany.IsZeroOrUnsign() == true) { return false; }
            return true;
        }
        #endregion

        #region ETC

        public long EstimatedDocumentCount<T>(string name, string collectionName)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name, collectionName);
            long estimatedDocumentCount = getCollection.EstimatedDocumentCount();
            return estimatedDocumentCount;
        }
        public List<T> Regex<T>(string name, Filter filter)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(name);
            FilterDefinition<T> filterRegex = Builders<T>.Filter.Regex(filter.field, filter.value);
            IFindFluent<T, T> find = getCollection.Find(filterRegex);
            List<T> list = find.ToList();
            return list;
        }

        public List<T> Search<T>(string name, string text)
        {
            List<T> result = null;
            try
            {
                IMongoCollection<T> getCollection = this.GetCollection<T>(name);
                FilterDefinition<T> filterText = Builders<T>.Filter.Text(text);
                IFindFluent<T, T> find = getCollection.Find(filterText);
                List<T> list = find.ToList();
                result = list;
            }
            catch { }
            return result;
        }

        public List<string> GetIndexNameValueList<T>(string database, params string[] names)
        {
            IMongoCollection<T> getCollection = this.GetCollection<T>(database);
            List<BsonDocument> indexesList = getCollection.Indexes.List().ToList();
            List<string> nameValues = indexesList.Select(x => x.GetValue("name").ToString()).ToList();
            return nameValues;
        }

        #endregion
    }
}
