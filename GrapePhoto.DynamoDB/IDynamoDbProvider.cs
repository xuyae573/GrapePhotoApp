using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.DynamoDB
{
    public interface IDynamoDbProvider
    {
        DynamoDBContext DBContext { get; }
    }
}
