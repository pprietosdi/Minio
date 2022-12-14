/*
 * MinIO .NET Library for Amazon S3 Compatible Cloud Storage, (C) 2020 MinIO, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Minio.DataModel;

/// <summary>
///     Stores raw json events generated by ListenBucketNotifications
///     The Minio client doesn't depend on a JSON library so we can let
///     the caller use a library of their choice
/// </summary>
public class MinioNotificationRaw
{
    public string json;

    public MinioNotificationRaw(string json)
    {
        this.json = json;
    }
}

/// <summary>
///     Helper class to deserialize notifications generated
///     from MinioNotificaitonRaw by ListenBucketNotifications
/// </summary>
[Serializable]
public class MinioNotification
{
    public string Err;
    public NotificationEvent[] Records;
}

public class NotificationEvent
{
    public string awsRegion;
    public string eventName;
    public string eventSource;
    public string eventTime;
    public string eventVersion;
    public Dictionary<string, string> requestParameters;
    public Dictionary<string, string> responseElements;
    public EventMeta s3;
    public SourceInfo source;
    public Identity userIdentity;
}

[DataContract]
public class EventMeta
{
    [DataMember] public BucketMeta bucket;

    [DataMember] public string configurationId;

    [DataMember(Name = "object")] public ObjectMeta objectMeta; // C# won't allow the keyword 'object' as a name

    [DataMember] public string schemaVersion;
}

public class ObjectMeta
{
    public string contentType;
    public string etag;
    public string key;
    public string sequencer;
    public int size;
    public Dictionary<string, string> userMetadata;
    public string versionId;
}

public class BucketMeta
{
    public string arn;
    public string name;
    public Identity ownerIdentity;
}

public class Identity
{
    public string principalId;
}

public class SourceInfo
{
    public string host;
    public string port;
    public string userAgent;
}