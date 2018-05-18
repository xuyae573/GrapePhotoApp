using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace GrapePhoto.DynamoDB
{
    public class DynamoDbProvider : IDynamoDbProvider
    {
        private AmazonDynamoDBClient _client;

        public DynamoDbProvider(AmazonDynamoDBClient client)
        {
            _client = client;
        }

        public DynamoDBContext DBContext {
            get
            {
                return new DynamoDBContext(_client);
            }
        }
    }
}
