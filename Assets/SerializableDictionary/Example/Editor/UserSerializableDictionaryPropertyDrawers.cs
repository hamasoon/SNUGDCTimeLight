using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StringStringDictionary))]
[CustomPropertyDrawer(typeof(ObjectColorDictionary))]
[CustomPropertyDrawer(typeof(StringColorArrayDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(ColorArrayStorage))]
public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}

[CustomPropertyDrawer(typeof(SerialilzeDicStringVector3))]
public class Vector3SerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(SoundEffects))]
public class AudioClipSerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(SoundSources))]
public class AudioSourceSerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}
